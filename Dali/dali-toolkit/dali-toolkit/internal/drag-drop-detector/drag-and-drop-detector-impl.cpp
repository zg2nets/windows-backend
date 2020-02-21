/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd.
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
#include <dali-toolkit/internal/drag-drop-detector/drag-and-drop-detector-impl.h>

#include <dali/public-api/events/point-state.h>
#include <dali/public-api/events/touch-data.h>

namespace Dali
{

namespace Toolkit
{

namespace Internal
{

Dali::Toolkit::DragAndDropDetector DragAndDropDetector::New()
{
  Dali::Toolkit::DragAndDropDetector detector = Dali::Toolkit::DragAndDropDetector(new DragAndDropDetector());

  return detector;
}

void DragAndDropDetector::Attach(Dali::Toolkit::Control& control)
{
  if(control)
  {
    if(!mControls.empty())
    {
      auto match = std::find(mControls.begin(), mControls.end(), control);
      if(match != mControls.end())
      {
        return;
      }
    }
    mControls.push_back(control);
    control.TouchSignal().Connect(this, &DragAndDropDetector::OnDrag);
    mFirstEnter.push_back(control.GetId());
    mPanGestureDetector.Attach(control);
    mPanGestureDetector.DetectedSignal().Connect(this, &DragAndDropDetector::OnPan);
  }

}

void DragAndDropDetector::Detach(Dali::Toolkit::Control& control)
{
  if(!mControls.empty())
  {
    if(!control)
    {
      return;
    }

    auto match = std::find(mControls.begin(), mControls.end(), control);

    if(match != mControls.end())
    {
      match->TouchSignal().Disconnect(this, &DragAndDropDetector::OnDrag);
      mPanGestureDetector.Detach(*match);
      mFirstEnter.erase(std::find(mFirstEnter.begin(), mFirstEnter.end(), control.GetId()));
      mControls.erase(match);
    }
  }
}

void DragAndDropDetector::DetachAll()
{
  if(!mControls.empty())
  {
    auto iter = mControls.begin();
    for(;iter != mControls.end();)
    {
      iter->TouchSignal().Disconnect(this, &DragAndDropDetector::OnDrag);
      mPanGestureDetector.Detach(*iter);
      iter = mControls.erase(iter);
    }
  }

  if(!mFirstEnter.empty())
  {
    auto iter = mFirstEnter.begin();
    for(;iter != mFirstEnter.end();)
    {
      iter = mFirstEnter.erase(iter);
    }
  }
}

uint32_t DragAndDropDetector::GetAttachedControlCount() const
{
  return mControls.size();
}

Dali::Toolkit::Control DragAndDropDetector::GetAttachedControl(uint32_t index) const
{
  Dali::Toolkit::Control control;

  if(index < mControls.size())
  {
    control = mControls[index];
  }

  return control;
}

void DragAndDropDetector::OnPan(Dali::Actor actor, const PanGesture& gesture)
{
  Dali::Toolkit::Control control = Dali::Toolkit::Control::DownCast(actor);

  if(gesture.state == Gesture::Started)
  {
    mDragLocalPosition = gesture.position;
    mPointDown = true;
    mDragControl = control;
    mFirstEnter.clear();
    for( auto&& control : mControls)
    {
      mFirstEnter.push_back(control.GetId());
    }
    float width = control.GetProperty<float>(Dali::Actor::Property::SIZE_WIDTH);
    float height = control.GetProperty<float>(Dali::Actor::Property::SIZE_HEIGHT);
    Vector3 actorPos = control.GetProperty<Vector3>(Dali::Actor::Property::POSITION);

    mShadowControl = Dali::Toolkit::Control::New();
    mShadowControl.SetPosition(actorPos);
    mShadowControl.SetSize(width, height);
    mShadowControl.SetBackgroundColor(Vector4(0.3f, 0.3f, 0.3f, 0.7f));
    mShadowControl.SetParentOrigin(control.GetCurrentParentOrigin());
    mShadowControl.SetAnchorPoint(control.GetCurrentAnchorPoint());
    control.GetParent().Add(mShadowControl);
    SetPosition(gesture.screenPosition);
    EmitStartedSignal(control);
  }
  if(gesture.state == Gesture::Continuing)
  {
      Vector2 screenPosition = gesture.screenPosition;
      control.GetParent().ScreenToLocal(mLocalPosition.x, mLocalPosition.y, screenPosition.x, screenPosition.y);
      mShadowControl.SetPosition(mLocalPosition.x - mDragLocalPosition.x, mLocalPosition.y - mDragLocalPosition.y);
  }
  if(gesture.state == Gesture::Finished)
  {
    mDragControl.GetParent().Remove(mShadowControl);
    EmitEndedSignal(control);
  }
}

bool DragAndDropDetector::OnDrag(Dali::Actor actor, const Dali::TouchData& data)
{
  Dali::Toolkit::Control control = Dali::Toolkit::Control::DownCast(actor);
  PointState::Type type = data.GetState(0);

  if(type == PointState::MOTION)
  {
    if(mDragControl != control && mPointDown)
    {
      auto found = std::find(mFirstEnter.begin(), mFirstEnter.end(), control.GetId());
      if(mFirstEnter.end() != found)
      {
        SetPosition(data.GetScreenPosition(0));
        mFirstEnter.erase(found);
        EmitEnteredSignal(control);
      }
      else
      {
        SetPosition(data.GetScreenPosition(0));
        EmitMovedSignal(control);
      }
    }
  }

  if(type == PointState::LEAVE)
  {
    if(mDragControl != control && mPointDown)
    {
      mFirstEnter.push_back(control.GetId());
      EmitExitedSignal(control);
    }
  }

  if(type == PointState::UP)
  {
    if(mDragControl != control && mPointDown)
    {
      SetPosition(data.GetScreenPosition(0));
      ClearContent();
      SetContent(mDragControl.GetName());
      EmitDroppedSignal(control);
    }

    if(mShadowControl)
    {
    control.GetParent().Remove(mShadowControl);
    }
    mPointDown = false;
  }
  return true;
}

const std::string& DragAndDropDetector::GetContent() const
{
  return mContent;
}

const Vector2& DragAndDropDetector::GetCurrentScreenPosition() const
{
  return mScreenPosition;
}

void DragAndDropDetector::SetContent( const std::string& content )
{
  mContent = content;
}

void DragAndDropDetector::ClearContent()
{
  mContent.clear();
}

void DragAndDropDetector::SetPosition( const Vector2& screenPosition )
{
  mScreenPosition = screenPosition;
}

void DragAndDropDetector::EmitStartedSignal(Dali::Toolkit::Control& control)
{
  if( !mStartedSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mStartedSignal.Emit( control, handle );
  }
}
void DragAndDropDetector::EmitEnteredSignal(Dali::Toolkit::Control& control)
{
  if ( !mEnteredSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mEnteredSignal.Emit( control, handle );
  }
}

void DragAndDropDetector::EmitExitedSignal(Dali::Toolkit::Control& control)
{
  if ( !mExitedSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mExitedSignal.Emit( control, handle );
  }
}

void DragAndDropDetector::EmitMovedSignal(Dali::Toolkit::Control& control)
{
  if ( !mMovedSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mMovedSignal.Emit( control, handle );
  }
}

void DragAndDropDetector::EmitDroppedSignal(Dali::Toolkit::Control& control)
{
  if ( !mDroppedSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mDroppedSignal.Emit( control, handle );
  }
}

void DragAndDropDetector::EmitEndedSignal(Dali::Toolkit::Control& control)
{
  if( !mEndedSignal.Empty() )
  {
    Dali::Toolkit::DragAndDropDetector handle( this );
    mEndedSignal.Emit( control, handle );
  }
}

DragAndDropDetector::DragAndDropDetector()
: mContent(),
  mScreenPosition()
{
  mPanGestureDetector = Dali::PanGestureDetector::New();
  mPointDown = false;
}

DragAndDropDetector::~DragAndDropDetector()
{
}

} // namespace Internal

} //namespace Toolkit

} // namespace Dali
