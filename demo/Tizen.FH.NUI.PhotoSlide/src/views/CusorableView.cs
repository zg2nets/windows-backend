using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class CusorableView : View
    {
        private Cursor cursor;
        public CusorableView() : base()
        {
            this.HeightResizePolicy = ResizePolicyType.FillToParent;
            this.WidthResizePolicy = ResizePolicyType.FillToParent;
            
            //this.TouchEvent += CusorableView_TouchEvent;
        }

        private bool CusorableView_TouchEvent(object source, TouchEventArgs e)
        {
            Vector2 current = e.Touch.GetLocalPosition(0);
            if ((e.Touch.GetState(0) == PointStateType.Down))
            {
                Touch_Down(current);
            }
            if ((e.Touch.GetState(0) == PointStateType.Motion))
            {
                Touch_Motion(current);
            }

            if ((e.Touch.GetState(0) == PointStateType.Up))
            {
                Touch_Up(current);
            }
            return false;
        }

        public void Touch_Motion(Vector2 position)
        {
            if (cursor)
            {
                cursor.Position2D = new Position2D((int)position.X, (int)position.Y);
            }
        }

        public void Touch_Down(Vector2 position)
        {
            cursor = new Cursor();
            this.Add(cursor);

            cursor.Position2D = new Position2D((int)position.X, (int)position.Y);
            Animation playAnimation = new Animation(100);
            playAnimation.AnimateTo(cursor, "Opacity", 0.7f, 0, 100);
            playAnimation.AnimateTo(cursor, "Scale", new Size(1.4f, 1.4f, 1.0f));
            playAnimation.Play();
        }

        public void Touch_Up(Vector2 position)
        {
            if(cursor)
            {
                Animation playAnimation = new Animation(100);
                playAnimation.AnimateTo(cursor, "Opacity", 0.0f, 0, 100);
                playAnimation.AnimateTo(cursor, "Scale", new Size(1.0f, 1.0f, 1.0f));
                playAnimation.Play();

                cursor.Unparent();
                cursor.Dispose();
                cursor = null;
            }
        }
    }
}
