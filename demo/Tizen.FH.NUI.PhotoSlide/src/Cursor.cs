using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class Cursor : ImageView
    {
        public Cursor()
        {
            ResourceUrl = CommonResource.GetResourcePath() + "/images2/" + "cursor.png";
            ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
            Size2D = new Tizen.NUI.Size2D(60, 60);

            Opacity = 0.0f;
        }
    }
}
