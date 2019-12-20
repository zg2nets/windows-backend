using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class FoodToggleButtonStyle : TextButtonStyle
    {
        protected override ViewStyle GetAttributes()
        {
            if (Content != null)
            {
                ViewStyle contentStyle = (ViewStyle)global::System.Activator.CreateInstance(Content.GetType());
                contentStyle.CopyFrom(Content as ViewStyle);
                return contentStyle;
            }
            ButtonStyle style = base.GetAttributes() as ButtonStyle;
            style.BackgroundImage = new Selector<string>
            {
                Normal = CommonResource.Instance.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_toggle_btn_normal_ec7510.png",
                Selected = CommonResource.Instance.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_point_btn_normal_ec7510.png",

            };
            style.BackgroundImageBorder = new Rectangle(5, 5, 5, 5);
            style.Shadow.ResourceUrl.All = CommonResource.Instance.GetFHResourcePath() + "3. Button/rectangle_btn_shadow.png";
            style.Shadow.Border = new Rectangle(5, 5, 5, 5);
            style.Text.TextColor = new ColorSelector
            {
                Normal = Utility.Hex2Color(Constants.AppColorFood, 1),
                Selected = new Color(1, 1, 1, 1),
            };
            return style;
        }
    }
}
