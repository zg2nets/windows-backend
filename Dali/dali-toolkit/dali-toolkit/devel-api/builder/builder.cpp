/*
 * Copyright (c) 2015 Samsung Electronics Co., Ltd.
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

#include "builder.h"

// EXTERNAL INCLUDES

// INTERNAL INCLUDES

#include <dali-toolkit/internal/builder/builder-impl.h>

namespace Dali
{

namespace Toolkit
{

Builder::Builder()
{
}

Builder::~Builder()
{
}

Builder Builder::New(void)
{
  return Builder(new Internal::Builder());
}

Builder::Builder(Internal::Builder *impl)
  : BaseHandle(impl)
{
}

void Builder::LoadFromString( const std::string &data, UIFormat rep )
{
  GetImpl(*this).LoadFromString( data );
}

void Builder::AddConstants( const Property::Map& map )
{
  GetImpl(*this).AddConstants( map );
}

void Builder::AddConstant( const std::string& key, const Property::Value& value )
{
  GetImpl(*this).AddConstant( key, value );
}

const Property::Map& Builder::GetConfigurations() const
{
  return GetImpl(*this).GetConfigurations();
}

const Property::Map& Builder::GetConstants() const
{
  return GetImpl(*this).GetConstants();
}

const Property::Value& Builder::GetConstant( const std::string& key ) const
{
  return GetImpl(*this).GetConstant( key );
}

Animation Builder::CreateAnimation( const std::string& animationName )
{
  return GetImpl(*this).CreateAnimation( animationName );
}

Animation Builder::CreateAnimation( const std::string& animationName, const Property::Map& map )
{
  return GetImpl(*this).CreateAnimation( animationName, map );
}

Animation Builder::CreateAnimation( const std::string& animationName, Dali::Actor sourceActor )
{
  return GetImpl(*this).CreateAnimation( animationName, sourceActor );
}

Animation Builder::CreateAnimation( const std::string& animationName, const Property::Map& map, Dali::Actor sourceActor )
{
  return GetImpl(*this).CreateAnimation( animationName, map, sourceActor );
}

BaseHandle Builder::Create( const std::string& templateName )
{
  return GetImpl(*this).Create( templateName );
}

BaseHandle Builder::Create( const std::string& templateName, const Property::Map& map )
{
  return GetImpl(*this).Create( templateName, map );
}

BaseHandle Builder::CreateFromJson( const std::string& json )
{
  return GetImpl(*this).CreateFromJson( json );
}

bool Builder::ApplyStyle( const std::string& styleName, Handle& handle )
{
  return GetImpl(*this).ApplyStyle( styleName, handle );
}

bool Builder::ApplyFromJson( Handle& handle, const std::string& json )
{
  return GetImpl(*this).ApplyFromJson( handle, json );
}

void Builder::AddActors( Actor toActor )
{
  GetImpl(*this).AddActors( toActor );
}

void Builder::AddActors( const std::string &sectionName, Actor toActor )
{
  GetImpl(*this).AddActors( sectionName, toActor );
}

void Builder::CreateRenderTask( const std::string &name )
{
  GetImpl(*this).CreateRenderTask( name );
}

Path Builder::GetPath( const std::string &name )
{
  return GetImpl(*this).GetPath( name );
}

PathConstrainer Builder::GetPathConstrainer( const std::string& pathConstrainerName )
{
  return GetImpl(*this).GetPathConstrainer( pathConstrainerName );
}

LinearConstrainer Builder::GetLinearConstrainer( const std::string& linearConstrainerName )
{
  return GetImpl(*this).GetLinearConstrainer( linearConstrainerName );
}

Builder::BuilderSignalType& Builder::QuitSignal()
{
  return GetImpl( *this ).QuitSignal();
}

} // namespace Toolkit

} // namespace Dali

