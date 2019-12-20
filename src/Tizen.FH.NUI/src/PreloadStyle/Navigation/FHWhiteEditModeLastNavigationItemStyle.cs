﻿using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class WhiteEditModeLastNavigationItemStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            NavigationItemStyle style = new NavigationItemStyle
            {
                Padding = new Extents(24, 24, 58, 0),
                Text = new TextLabelStyle
                {
                    Size = new Size(130, 52),
                    TextColor = new ColorSelector { All = new Color(0, 0, 0, 0.85f) },
                    PointSize = new FloatSelector { All = 8 },
                    FontFamily = "SamsungOneUI 500C",
                    Text = new StringSelector { All = "Cancel" },
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                },
                Icon = new ImageControlStyle
                {
                    Size = new Size(56, 56),
                    ResourceUrl = new StringSelector
                    {
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_press.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                        DisabledFocused = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                        DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel_dim.png",
                        Other = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/sidenavi_btn_cancel.png"
                    },
                },
                DividerLine = new ViewStyle
                {
                    Size = new Size(178, 2),
                    BackgroundColor = new ColorSelector { All = new Color(0, 0, 0, 0.1f) },
                    Position = new Position(0, 16),
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                },
            };

            return style;
        }
    }
}
