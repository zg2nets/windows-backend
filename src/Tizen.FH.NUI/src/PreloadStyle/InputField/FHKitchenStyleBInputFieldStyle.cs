using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class KitchenStyleBInputFieldStyle : StyleBase
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
                BackgroundImage = CommonResource.Instance.GetFHResourcePath() + "1. Action bar/search_bg.png",
                BackgroundImageBorder = new Rectangle(45, 45, 0, 0),
                InputBox = new TextFieldStyle
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
                AddButtonBackground = new ImageViewStyle
                {
                    Size = new Size(92, 92),
                    ResourceUrl = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/[Input Field] App Primary Color/field_btn_add_bg_9762d9.png",
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/[Input Field] App Primary Color/field_btn_add_bg_9762d9.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/[Input Field] App Primary Color/field_btn_add_bg_dim_9762d9.png",
                    }
                },
                AddButtonForeground = new ImageViewStyle
                {
                    ResourceUrl = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/html/input_btn_add_9762d9_normal.png",
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/html/input_btn_add_9762d9_press.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/html/input_btn_add_9762d9_dim.png",
                    }
                },
                AddButtonOverlay = new ImageViewStyle
                {
                    ResourceUrl = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/field_btn_add_bg_press_overlay.png",
                        Other = "",
                    }
                },
                DeleteButton = new ImageViewStyle
                {
                    Size = new Size(92, 92),
                    ResourceUrl = new StringSelector
                    {
                        Normal = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/field_btn_ic_delete.png",
                        Pressed = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/field_btn_ic_delete_press.png",
                        Disabled = CommonResource.Instance.GetFHResourcePath() + "7. Input Field/field_btn_ic_delete_dim.png",
                    }
                },
            };

            return style;
        }
    }
}
