using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class UtilityCheckBoxStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            ButtonStyle style = new ButtonStyle
            {
                IsSelectable = true,
                Icon = new ImageControlStyle
                {
                    Size = new Size(48, 48),
                    Position = new Position(0, 0),
                    BackgroundImage = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_off.png",
                        Selected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_on.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_off.png",
                        DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_on.png",
                    },
                    ResourceUrl = new StringSelector
                    {
                        Normal = "",
                        Selected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check.png",
                        Disabled = "",
                        DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check.png",
                    },
                    Shadow = new ImageViewStyle
                    {
                        ResourceUrl = new StringSelector
                        {
                            Normal = "",
                            Selected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_shadow.png",
                            Disabled = "",
                            DisabledSelected = CommonResource.Instance.GetFHResourcePath() + "9. Controller/controller_btn_check_shadow.png",
                        },
                    },
                    Opacity = new FloatSelector
                    {
                        Normal = 1.0f,
                        Selected = 1.0f,
                        Disabled = 0.4f,
                        DisabledSelected = 0.4f
                    },
                },
            };

            return style;
        }
    }
}
