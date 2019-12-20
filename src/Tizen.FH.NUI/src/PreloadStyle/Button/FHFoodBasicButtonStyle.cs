using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class FoodBasicButtonStyle : TextButtonStyle
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
            style.Shadow.ResourceUrl.All = CommonResource.Instance.GetFHResourcePath() + "3. Button/rectangle_btn_shadow.png";
            style.Shadow.Border = new Rectangle(5, 5, 5, 5);
            style.Text.TextColor = new Selector<Tizen.NUI.Color>
            {
                Selected = Utility.Hex2Color(Constants.AppColorFood, 1),
            };
                
            return style;
        }
    }
}
