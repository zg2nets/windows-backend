/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd.
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
#include <dali/internal/event/rendering/frame-buffer-impl.h>

// INTERNAL INCLUDES
#include <dali/internal/update/manager/update-manager.h>
#include <dali/internal/render/renderers/render-frame-buffer.h>
#include <dali/internal/render/renderers/render-texture-frame-buffer.h>
#include <dali/internal/render/renderers/render-surface-frame-buffer.h>
#include <dali/integration-api/render-surface.h>

namespace Dali
{
namespace Internal
{

FrameBufferPtr FrameBuffer::New( uint32_t width, uint32_t height, Mask attachments  )
{
  FrameBufferPtr frameBuffer( new FrameBuffer( width, height, attachments ) );
  frameBuffer->Initialize();
  return frameBuffer;
}

FrameBufferPtr FrameBuffer::New( Dali::Integration::RenderSurface& renderSurface, Mask attachments )
{
  Dali::PositionSize positionSize = renderSurface.GetPositionSize();
  FrameBufferPtr frameBuffer( new FrameBuffer( positionSize.width, positionSize.height, attachments ) );
  frameBuffer->Initialize( &renderSurface );
  return frameBuffer;
}

Render::FrameBuffer* FrameBuffer::GetRenderObject() const
{
  return mRenderObject;
}

FrameBuffer::FrameBuffer( uint32_t width, uint32_t height, Mask attachments )
: mEventThreadServices( EventThreadServices::Get() ),
  mRenderObject( NULL ),
  mColor{ nullptr },
  mWidth( width ),
  mHeight( height ),
  mAttachments( attachments ),
  mColorAttachmentCount( 0 ),
  mIsSurfaceBacked( false )
{
}

void FrameBuffer::Initialize( Integration::RenderSurface* renderSurface )
{
  mIsSurfaceBacked = ( renderSurface != nullptr );

  // If render surface backed, create a different scene object
  // Make Render::FrameBuffer as a base class, and implement Render::TextureFrameBuffer & Render::WindowFrameBuffer
  if ( mIsSurfaceBacked )
  {
    mRenderObject = new Render::SurfaceFrameBuffer( renderSurface );
  }
  else
  {
    mRenderObject = new Render::TextureFrameBuffer( mWidth, mHeight, mAttachments );
  }

  OwnerPointer< Render::FrameBuffer > transferOwnership( mRenderObject );
  AddFrameBuffer( mEventThreadServices.GetUpdateManager(), transferOwnership );
}

void FrameBuffer::AttachColorTexture( TexturePtr texture, uint32_t mipmapLevel, uint32_t layer )
{
  if ( mIsSurfaceBacked )
  {
    DALI_LOG_ERROR( "Attempted to attach color texture to a render surface backed FrameBuffer \n" );
  }
  else
  {
    if( ( texture->GetWidth() / ( 1u << mipmapLevel ) != mWidth ) ||
        ( texture->GetHeight() / ( 1u << mipmapLevel ) != mHeight ) )
    {
      DALI_LOG_ERROR( "Failed to attach color texture to FrameBuffer: Size mismatch \n" );
    }
    else if ( mColorAttachmentCount >= Dali::DevelFrameBuffer::MAX_COLOR_ATTACHMENTS )
    {
      DALI_LOG_ERROR( "Failed to attach color texture to FrameBuffer: Exceeded maximum supported color attachments.\n" );
    }
    else
    {
      mColor[mColorAttachmentCount] = texture;
      ++mColorAttachmentCount;

      AttachColorTextureToFrameBuffer( mEventThreadServices.GetUpdateManager(), *mRenderObject, texture->GetRenderObject(), mipmapLevel, layer );
    }
  }
}

Texture* FrameBuffer::GetColorTexture(uint8_t index) const
{
  return ( mIsSurfaceBacked || index >= mColorAttachmentCount ) ? nullptr : mColor[index].Get();
}

void FrameBuffer::SetSize( uint32_t width, uint32_t height )
{
  mWidth = width;
  mHeight = height;

  if( mRenderObject->IsSurfaceBacked() )
  {
    SetFrameBufferSizeMessage( mEventThreadServices.GetUpdateManager(), static_cast<Render::SurfaceFrameBuffer*>( mRenderObject ), width, height );
  }
}

void FrameBuffer::MarkSurfaceAsInvalid()
{
  if ( mIsSurfaceBacked )
  {
    Render::SurfaceFrameBuffer* renderObject = static_cast<Render::SurfaceFrameBuffer*>( mRenderObject );
    renderObject->MarkSurfaceAsInvalid();
  }
}

FrameBuffer::~FrameBuffer()
{
  if( EventThreadServices::IsCoreRunning() && mRenderObject )
  {
    RemoveFrameBuffer( mEventThreadServices.GetUpdateManager(), *mRenderObject );
  }
}


} // namespace Internal
} // namespace Dali
