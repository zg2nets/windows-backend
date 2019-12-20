using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class FamilyServiceButtonStyle : TextButtonStyle
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
            style.IsSelectable = false;
            style.BackgroundImage.All = CommonResource.Instance.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_point_btn_normal_24c447.png";
            style.BackgroundImageBorder = new Rectangle(5, 5, 5, 5);
            style.Shadow.ResourceUrl.All = CommonResource.Instance.GetFHResourcePath() + "3. Button/rectangle_btn_shadow.png";
            style.Shadow.Border = new Rectangle(5, 5, 5, 5);
            style.Text.TextColor = new ColorSelector
            {
                Normal = new Color(1, 1, 1, 1),
                Pressed = new Color(1, 1, 1, 0.7f),
                Disabled = new Color(1, 1, 1, 0.4f),
            };
            return style;
        }
    }
}
