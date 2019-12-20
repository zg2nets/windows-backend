using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class IconButtonStyle : StyleBase
    {
        public IconButtonStyle()
        {   
        }

        protected override ViewStyle GetAttributes()
        {
            ButtonStyle style = new ButtonStyle
            {
                IsSelectable = true,
                Overlay = new ImageViewStyle
                {
                    ResourceUrl = new Selector<string> { Pressed = CommonResource.Instance.GetFHResourcePath() + "3. Button/oval_toggle_btn_press_overlay.png", Other = "" },
                    Border = new Selector<Rectangle> { All = new Rectangle(5, 5, 5, 5) }
                },

                Icon = new ImageControlStyle
                {
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                    PivotPoint = Tizen.NUI.PivotPoint.Center,
                    WidthResizePolicy = ResizePolicyType.FitToChildren,
                    HeightResizePolicy = ResizePolicyType.FitToChildren,
                }
            };
            return style;
        }
    }
}
