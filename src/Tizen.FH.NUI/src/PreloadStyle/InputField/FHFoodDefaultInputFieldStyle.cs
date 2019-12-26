using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class FoodDefaultInputFieldStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            if (Content != null)
            {
                ViewStyle contentStyle = (ViewStyle)global::System.Activator.CreateInstance(Content.GetType());
                contentStyle.CopyFrom(Content as ViewStyle);
                return contentStyle;
            }
            InputFieldStyle style = new InputFieldStyle
            {
                Space = 24,
                SpaceBetweenTextFieldAndRightButton = 56,
                BackgroundImageAttributes = new ImageViewStyle
                {
                    ResourceUrl = new StringSelector
                    {
                        All = CommonResource.Instance.GetFHResourcePath() + "1. Action bar/search_bg.png",
                    },
                    Border = new RectangleSelector
                    {
                        All = new Rectangle(45, 45, 0, 0),
                    },
                },
                InputBoxAttributes = new TextFieldStyle
                {
                    TextColor = new ColorSelector
                    {
                        Normal = new Color(0, 0, 0, 1),
                        Pressed = new Color(0, 0, 0, 1),
                        Disabled = new Color(0, 0, 0, 0.4f)
                    },
                    PlaceholderTextColor = new Selector<Vector4>
                    {
                        All = new Color(0, 0, 0, 0.4f),
                    },
                    PrimaryCursorColor = new Selector<Vector4>
                    {
                        All = Utility.Hex2Color(0x0aaaf5, 1),
                    },
                    //SecondaryCursorColor = new ColorSelector
                    //{
                    //    All = Utility.Hex2Color(0x0aaaf5, 1),
                    //},
                    HorizontalAlignment = HorizontalAlignment.Begin,
                    VerticalAlignment = VerticalAlignment.Center,
                    PointSize = new FloatSelector
                    {
                        All = 38,
                    },
                    FontFamily = "SamsungOne 500",
                    CursorWidth = 2,
                },
                CancelButton = new ImageViewStyle
                {
                    Size = new Size(56, 56),
                    ResourceUrl = new StringSelector
                    {
                        All = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/[Input Field] App Primary Color/field_ic_cancel_ec7510.png",
                    }
                },
            };

            return style;
        }
    }
}
