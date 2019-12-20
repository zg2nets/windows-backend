﻿using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class BasicShortToasStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            ToastStyle style = new ToastStyle
            {
                Size = new Size(512, 132),
                BackgroundImage = CommonResource.Instance.GetFHResourcePath() + "12. Toast Popup/toast_background.png",
                BackgroundImageBorder = new Rectangle(64, 64, 4, 4),
                Text = new TextLabelStyle
                {
                    Padding = new Extents(96, 96, 38, 38),
                    PointSize = new Selector<float?> { All = 26, },
                    TextColor = new Selector<Color> { All = Color.White, }
                }
            };
            return style;
        }
    }
}
