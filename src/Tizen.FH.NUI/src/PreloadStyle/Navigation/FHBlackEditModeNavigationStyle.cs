using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class BlackEditModeNavigationStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            NavigationStyle style = new NavigationStyle
            {
                Shadow = new ImageViewStyle
                {
                    Size = new Size(6, 800),
                    ResourceUrl = new StringSelector { All = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_editmode_shadow_b.png" },
                },
                BackgroundImage = new StringSelector { All = CommonResource.Instance.GetFHResourcePath() + "2. Side Navigation/[Black ver.]/sidenavi_editmode_bg_b.png" },
                BackgroundColor = new ColorSelector { All = new Color(1, 1, 1, 0.9f) },
                IsFitWithItems = false,
            };

            return style;
        }
    }
}
