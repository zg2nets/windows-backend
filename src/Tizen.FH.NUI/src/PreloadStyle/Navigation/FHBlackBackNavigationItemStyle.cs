using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class BlackBackNavigationItemStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {            
            NavigationItemStyle style = new NavigationItemStyle
            {
                Size = new Size(120, 140),
                Icon = new ImageControlStyle
                {
                    Size = new Size(56, 56),
                    ResourceUrl = new StringSelector
                    {
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_back_b_press.png",
                        Other = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_btn_back_b.png"
                    },
                },
                BackgroundImage =  new StringSelector { All = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_back_bg_b.png" },
                EnableIconCenter = true
            };

            return style;
        }
    }
}
