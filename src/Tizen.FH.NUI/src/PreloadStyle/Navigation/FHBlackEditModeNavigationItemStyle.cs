using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class BlackEditModeNavigationItemStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            NavigationItemStyle style = new NavigationItemStyle
            {
                Text = new TextLabelStyle
                {
                    Size = new Size(130, 52),
                    TextColor = new ColorSelector
                    {
                        Pressed = new Color(1, 1, 1, 0.85f),
                        Disabled = new Color(0, 0, 0, 0.4f),
                        Other = new Color(1, 1, 1, 0.85f),
                    },
                    PointSize = new FloatSelector { All = 8 },
                    FontFamily = "SamsungOneUI 500C",
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                },
                Icon = new ImageControlStyle
                {
                    Size = new Size(56, 56),
                },
                Padding = new Extents(24, 24, 24, 24),
            };

            return style;
        }
    }
}
