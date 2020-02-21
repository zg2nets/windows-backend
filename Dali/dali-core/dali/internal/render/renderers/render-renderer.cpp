/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

// CLASS HEADER
#include <dali/internal/render/renderers/render-renderer.h>

// INTERNAL INCLUDES
#include <dali/internal/common/image-sampler.h>
#include <dali/internal/render/gl-resources/context.h>
#include <dali/internal/render/renderers/render-sampler.h>
#include <dali/internal/render/shaders/scene-graph-shader.h>
#include <dali/internal/render/shaders/program.h>
#include <dali/internal/render/data-providers/node-data-provider.h>

namespace Dali
{

namespace Internal
{

namespace
{

static Matrix gModelViewProjectionMatrix( false ); ///< a shared matrix to calculate the MVP matrix, dont want to store it in object to reduce storage overhead
static Matrix3 gNormalMatrix; ///< a shared matrix to calculate normal matrix, dont want to store it in object to reduce storage overhead

/**
 * Helper to set view and projection matrices once per program
 * @param program to set the matrices to
 * @param modelMatrix to set
 * @param viewMatrix to set
 * @param projectionMatrix to set
 * @param modelViewMatrix to set
 * @param modelViewProjectionMatrix to set
 */
inline void SetMatrices( Program& program,
                         const Matrix& modelMatrix,
                         const Matrix& viewMatrix,
                         const Matrix& projectionMatrix,
                         const Matrix& modelViewMatrix )
{
  GLint loc = program.GetUniformLocation( Program::UNIFORM_MODEL_MATRIX );
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    program.SetUniformMatrix4fv( loc, 1, modelMatrix.AsFloat() );
  }
  loc = program.GetUniformLocation( Program::UNIFORM_VIEW_MATRIX );
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    if( program.GetViewMatrix() != &viewMatrix )
    {
      program.SetViewMatrix( &viewMatrix );
      program.SetUniformMatrix4fv( loc, 1, viewMatrix.AsFloat() );
    }
  }
  // set projection matrix if program has not yet received it this frame or if it is dirty
  loc = program.GetUniformLocation( Program::UNIFORM_PROJECTION_MATRIX );
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    if( program.GetProjectionMatrix() != &projectionMatrix )
    {
      program.SetProjectionMatrix( &projectionMatrix );
      program.SetUniformMatrix4fv( loc, 1, projectionMatrix.AsFloat() );
    }
  }
  loc = program.GetUniformLocation(Program::UNIFORM_MODELVIEW_MATRIX);
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    program.SetUniformMatrix4fv( loc, 1, modelViewMatrix.AsFloat() );
  }

  loc = program.GetUniformLocation( Program::UNIFORM_MVP_MATRIX );
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    Matrix::Multiply( gModelViewProjectionMatrix, modelViewMatrix, projectionMatrix );
    program.SetUniformMatrix4fv( loc, 1, gModelViewProjectionMatrix.AsFloat() );
  }

  loc = program.GetUniformLocation( Program::UNIFORM_NORMAL_MATRIX );
  if( Program::UNIFORM_UNKNOWN != loc )
  {
    gNormalMatrix = modelViewMatrix;
    gNormalMatrix.Invert();
    gNormalMatrix.Transpose();
    program.SetUniformMatrix3fv( loc, 1, gNormalMatrix.AsFloat() );
  }
}

}

namespace Render
{

Renderer* Renderer::New( SceneGraph::RenderDataProvider* dataProvider,
                         Render::Geometry* geometry,
                         uint32_t blendingBitmask,
                         const Vector4& blendColor,
                         FaceCullingMode::Type faceCullingMode,
                         bool preMultipliedAlphaEnabled,
                         DepthWriteMode::Type depthWriteMode,
                         DepthTestMode::Type depthTestMode,
                         DepthFunction::Type depthFunction,
                         StencilParameters& stencilParameters )
{
  return new Renderer( dataProvider, geometry, blendingBitmask, blendColor,
                       faceCullingMode, preMultipliedAlphaEnabled, depthWriteMode, depthTestMode,
                       depthFunction, stencilParameters );
}

Renderer::Renderer( SceneGraph::RenderDataProvider* dataProvider,
                    Render::Geometry* geometry,
                    uint32_t blendingBitmask,
                    const Vector4& blendColor,
                    FaceCullingMode::Type faceCullingMode,
                    bool preMultipliedAlphaEnabled,
                    DepthWriteMode::Type depthWriteMode,
                    DepthTestMode::Type depthTestMode,
                    DepthFunction::Type depthFunction,
                    StencilParameters& stencilParameters )
: mRenderDataProvider( dataProvider ),
  mContext( NULL),
  mGeometry( geometry ),
  mUniformIndexMap(),
  mAttributesLocation(),
  mStencilParameters( stencilParameters ),
  mBlendingOptions(),
  mIndexedDrawFirstElement( 0 ),
  mIndexedDrawElementsCount( 0 ),
  mDepthFunction( depthFunction ),
  mFaceCullingMode( faceCullingMode ),
  mDepthWriteMode( depthWriteMode ),
  mDepthTestMode( depthTestMode ),
  mUpdateAttributesLocation( true ),
  mPremultipledAlphaEnabled( preMultipliedAlphaEnabled ),
  mShaderChanged( false )
{
  if( blendingBitmask != 0u )
  {
    mBlendingOptions.SetBitmask( blendingBitmask );
  }

  mBlendingOptions.SetBlendColor( blendColor );
}

void Renderer::Initialize( Context& context )
{
  mContext = &context;
}

Renderer::~Renderer()
{
}

void Renderer::SetGeometry( Render::Geometry* geometry )
{
  mGeometry = geometry;
  mUpdateAttributesLocation = true;
}

void Renderer::SetBlending( Context& context, bool blend )
{
  context.SetBlend( blend );
  if( blend )
  {
    // Blend color is optional and rarely used
    const Vector4* blendColor = mBlendingOptions.GetBlendColor();
    if( blendColor )
    {
      context.SetCustomBlendColor( *blendColor );
    }
    else
    {
      context.SetDefaultBlendColor();
    }

    // Set blend source & destination factors
    context.BlendFuncSeparate( mBlendingOptions.GetBlendSrcFactorRgb(),
                               mBlendingOptions.GetBlendDestFactorRgb(),
                               mBlendingOptions.GetBlendSrcFactorAlpha(),
                               mBlendingOptions.GetBlendDestFactorAlpha() );

    // Set blend equations
    context.BlendEquationSeparate( mBlendingOptions.GetBlendEquationRgb(),
                                   mBlendingOptions.GetBlendEquationAlpha() );
  }
}

void Renderer::GlContextDestroyed()
{
  mGeometry->GlContextDestroyed();
}

void Renderer::GlCleanup()
{
}

void Renderer::SetUniforms( BufferIndex bufferIndex, const SceneGraph::NodeDataProvider& node, const Vector3& size, Program& program )
{
  // Check if the map has changed
  DALI_ASSERT_DEBUG( mRenderDataProvider && "No Uniform map data provider available" );

  const SceneGraph::UniformMapDataProvider& uniformMapDataProvider = mRenderDataProvider->GetUniformMap();

  if( uniformMapDataProvider.GetUniformMapChanged( bufferIndex ) ||
      node.GetUniformMapChanged(bufferIndex) ||
      mUniformIndexMap.Count() == 0 ||
      mShaderChanged )
  {
    // Reset shader pointer
    mShaderChanged = false;

    const SceneGraph::CollectedUniformMap& uniformMap = uniformMapDataProvider.GetUniformMap( bufferIndex );
    const SceneGraph::CollectedUniformMap& uniformMapNode = node.GetUniformMap( bufferIndex );

    uint32_t maxMaps = static_cast<uint32_t>( uniformMap.Count() + uniformMapNode.Count() ); // 4,294,967,295 maps should be enough
    mUniformIndexMap.Clear(); // Clear contents, but keep memory if we don't change size
    mUniformIndexMap.Resize( maxMaps );

    uint32_t mapIndex = 0;
    for(; mapIndex < uniformMap.Count() ; ++mapIndex )
    {
      mUniformIndexMap[mapIndex].propertyValue = uniformMap[mapIndex]->propertyPtr;
      mUniformIndexMap[mapIndex].uniformIndex = program.RegisterUniform( uniformMap[mapIndex]->uniformName );
    }

    for( uint32_t nodeMapIndex = 0; nodeMapIndex < uniformMapNode.Count() ; ++nodeMapIndex )
    {
      uint32_t uniformIndex = program.RegisterUniform( uniformMapNode[nodeMapIndex]->uniformName );
      bool found(false);
      for( uint32_t i = 0; i<uniformMap.Count(); ++i )
      {
        if( mUniformIndexMap[i].uniformIndex == uniformIndex )
        {
          mUniformIndexMap[i].propertyValue = uniformMapNode[nodeMapIndex]->propertyPtr;
          found = true;
          break;
        }
      }

      if( !found )
      {
        mUniformIndexMap[mapIndex].propertyValue = uniformMapNode[nodeMapIndex]->propertyPtr;
        mUniformIndexMap[mapIndex].uniformIndex = uniformIndex;
        ++mapIndex;
      }
    }

    mUniformIndexMap.Resize( mapIndex );
  }

  // Set uniforms in local map
  for( UniformIndexMappings::Iterator iter = mUniformIndexMap.Begin(),
         end = mUniformIndexMap.End() ;
       iter != end ;
       ++iter )
  {
    SetUniformFromProperty( bufferIndex, program, *iter );
  }

  GLint sizeLoc = program.GetUniformLocation( Program::UNIFORM_SIZE );
  if( -1 != sizeLoc )
  {
    program.SetSizeUniform3f( sizeLoc, size.x, size.y, size.z );
  }
}

void Renderer::SetUniformFromProperty( BufferIndex bufferIndex, Program& program, UniformIndexMap& map )
{
  GLint location = program.GetUniformLocation(map.uniformIndex);
  if( Program::UNIFORM_UNKNOWN != location )
  {
    // switch based on property type to use correct GL uniform setter
    switch ( map.propertyValue->GetType() )
    {
      case Property::INTEGER:
      {
        program.SetUniform1i( location, map.propertyValue->GetInteger( bufferIndex ) );
        break;
      }
      case Property::FLOAT:
      {
        program.SetUniform1f( location, map.propertyValue->GetFloat( bufferIndex ) );
        break;
      }
      case Property::VECTOR2:
      {
        Vector2 value( map.propertyValue->GetVector2( bufferIndex ) );
        program.SetUniform2f( location, value.x, value.y );
        break;
      }

      case Property::VECTOR3:
      {
        Vector3 value( map.propertyValue->GetVector3( bufferIndex ) );
        program.SetUniform3f( location, value.x, value.y, value.z );
        break;
      }

      case Property::VECTOR4:
      {
        Vector4 value( map.propertyValue->GetVector4( bufferIndex ) );
        program.SetUniform4f( location, value.x, value.y, value.z, value.w );
        break;
      }

      case Property::ROTATION:
      {
        Quaternion value( map.propertyValue->GetQuaternion( bufferIndex ) );
        program.SetUniform4f( location, value.mVector.x, value.mVector.y, value.mVector.z, value.mVector.w );
        break;
      }

      case Property::MATRIX:
      {
        const Matrix& value = map.propertyValue->GetMatrix(bufferIndex);
        program.SetUniformMatrix4fv(location, 1, value.AsFloat() );
        break;
      }

      case Property::MATRIX3:
      {
        const Matrix3& value = map.propertyValue->GetMatrix3(bufferIndex);
        program.SetUniformMatrix3fv(location, 1, value.AsFloat() );
        break;
      }

      default:
      {
        // Other property types are ignored
        break;
      }
    }
  }
}

bool Renderer::BindTextures( Context& context, Program& program, Vector<GLuint>& boundTextures )
{
  uint32_t textureUnit = 0;
  bool result = true;

  GLint uniformLocation(-1);
  std::vector<Render::Sampler*>& samplers( mRenderDataProvider->GetSamplers() );
  std::vector<Render::Texture*>& textures( mRenderDataProvider->GetTextures() );
  for( uint32_t i = 0; i < static_cast<uint32_t>( textures.size() ) && result; ++i ) // not expecting more than uint32_t of textures
  {
    if( textures[i] )
    {
      result = textures[i]->Bind(context, textureUnit, samplers[i] );
      boundTextures.PushBack( textures[i]->GetId() );
      if( result && program.GetSamplerUniformLocation( i, uniformLocation ) )
      {
        program.SetUniform1i( uniformLocation, textureUnit );
        ++textureUnit;
      }
    }
  }

  return result;
}

void Renderer::SetFaceCullingMode( FaceCullingMode::Type mode )
{
  mFaceCullingMode =  mode;
}

void Renderer::SetBlendingBitMask( uint32_t bitmask )
{
  mBlendingOptions.SetBitmask( bitmask );
}

void Renderer::SetBlendColor( const Vector4& color )
{
  mBlendingOptions.SetBlendColor( color );
}

void Renderer::SetIndexedDrawFirstElement( uint32_t firstElement )
{
  mIndexedDrawFirstElement = firstElement;
}

void Renderer::SetIndexedDrawElementsCount( uint32_t elementsCount )
{
  mIndexedDrawElementsCount = elementsCount;
}

void Renderer::EnablePreMultipliedAlpha( bool enable )
{
  mPremultipledAlphaEnabled = enable;
}

void Renderer::SetDepthWriteMode( DepthWriteMode::Type depthWriteMode )
{
  mDepthWriteMode = depthWriteMode;
}

void Renderer::SetDepthTestMode( DepthTestMode::Type depthTestMode )
{
  mDepthTestMode = depthTestMode;
}

DepthWriteMode::Type Renderer::GetDepthWriteMode() const
{
  return mDepthWriteMode;
}

DepthTestMode::Type Renderer::GetDepthTestMode() const
{
  return mDepthTestMode;
}

void Renderer::SetDepthFunction( DepthFunction::Type depthFunction )
{
  mDepthFunction = depthFunction;
}

DepthFunction::Type Renderer::GetDepthFunction() const
{
  return mDepthFunction;
}

void Renderer::SetRenderMode( RenderMode::Type renderMode )
{
  mStencilParameters.renderMode = renderMode;
}

RenderMode::Type Renderer::GetRenderMode() const
{
  return mStencilParameters.renderMode;
}

void Renderer::SetStencilFunction( StencilFunction::Type stencilFunction )
{
  mStencilParameters.stencilFunction = stencilFunction;
}

StencilFunction::Type Renderer::GetStencilFunction() const
{
  return mStencilParameters.stencilFunction;
}

void Renderer::SetStencilFunctionMask( int stencilFunctionMask )
{
  mStencilParameters.stencilFunctionMask = stencilFunctionMask;
}

int Renderer::GetStencilFunctionMask() const
{
  return mStencilParameters.stencilFunctionMask;
}

void Renderer::SetStencilFunctionReference( int stencilFunctionReference )
{
  mStencilParameters.stencilFunctionReference = stencilFunctionReference;
}

int Renderer::GetStencilFunctionReference() const
{
  return mStencilParameters.stencilFunctionReference;
}

void Renderer::SetStencilMask( int stencilMask )
{
  mStencilParameters.stencilMask = stencilMask;
}

int Renderer::GetStencilMask() const
{
  return mStencilParameters.stencilMask;
}

void Renderer::SetStencilOperationOnFail( StencilOperation::Type stencilOperationOnFail )
{
  mStencilParameters.stencilOperationOnFail = stencilOperationOnFail;
}

StencilOperation::Type Renderer::GetStencilOperationOnFail() const
{
  return mStencilParameters.stencilOperationOnFail;
}

void Renderer::SetStencilOperationOnZFail( StencilOperation::Type stencilOperationOnZFail )
{
  mStencilParameters.stencilOperationOnZFail = stencilOperationOnZFail;
}

StencilOperation::Type Renderer::GetStencilOperationOnZFail() const
{
  return mStencilParameters.stencilOperationOnZFail;
}

void Renderer::SetStencilOperationOnZPass( StencilOperation::Type stencilOperationOnZPass )
{
  mStencilParameters.stencilOperationOnZPass = stencilOperationOnZPass;
}

StencilOperation::Type Renderer::GetStencilOperationOnZPass() const
{
  return mStencilParameters.stencilOperationOnZPass;
}

void Renderer::Upload( Context& context )
{
  mGeometry->Upload( context );
}

void Renderer::Render( Context& context,
                       BufferIndex bufferIndex,
                       const SceneGraph::NodeDataProvider& node,
                       const Matrix& modelMatrix,
                       const Matrix& modelViewMatrix,
                       const Matrix& viewMatrix,
                       const Matrix& projectionMatrix,
                       const Vector3& size,
                       bool blend,
                       Vector<GLuint>& boundTextures )
{
  // Get the program to use:
  Program* program = mRenderDataProvider->GetShader().GetProgram();
  if( !program )
  {
    DALI_LOG_ERROR( "Failed to get program for shader at address %p.\n", reinterpret_cast< void* >( &mRenderDataProvider->GetShader() ) );
    return;
  }

  //Set cull face  mode
  context.CullFace( mFaceCullingMode );

  //Set blending mode
  SetBlending( context, blend );

  // Take the program into use so we can send uniforms to it
  program->Use();

  if( DALI_LIKELY( BindTextures( context, *program, boundTextures ) ) )
  {
    // Only set up and draw if we have textures and they are all valid

    // set projection and view matrix if program has not yet received them yet this frame
    SetMatrices( *program, modelMatrix, viewMatrix, projectionMatrix, modelViewMatrix );

    // set color uniform
    GLint loc = program->GetUniformLocation( Program::UNIFORM_COLOR );
    if( Program::UNIFORM_UNKNOWN != loc )
    {
      const Vector4& color = node.GetRenderColor( bufferIndex );
      if( mPremultipledAlphaEnabled )
      {
        float alpha = color.a * mRenderDataProvider->GetOpacity( bufferIndex );
        program->SetUniform4f( loc, color.r * alpha, color.g * alpha, color.b * alpha, alpha );
      }
      else
      {
        program->SetUniform4f( loc, color.r, color.g, color.b, color.a * mRenderDataProvider->GetOpacity( bufferIndex ) );
      }
    }

    SetUniforms( bufferIndex, node, size, *program );

    if( mUpdateAttributesLocation || mGeometry->AttributesChanged() )
    {
      mGeometry->GetAttributeLocationFromProgram( mAttributesLocation, *program, bufferIndex );
      mUpdateAttributesLocation = false;
    }

    mGeometry->Draw( context,
                     bufferIndex,
                     mAttributesLocation,
                     mIndexedDrawFirstElement,
                     mIndexedDrawElementsCount );
  }
}

void Renderer::SetSortAttributes( BufferIndex bufferIndex,
                                  SceneGraph::RenderInstructionProcessor::SortAttributes& sortAttributes ) const
{
  sortAttributes.shader = &( mRenderDataProvider->GetShader() );
  sortAttributes.geometry = mGeometry;
}

void Renderer::SetShaderChanged( bool value )
{
  mShaderChanged = value;
}

} // namespace SceneGraph

} // namespace Internal

} // namespace Dali
