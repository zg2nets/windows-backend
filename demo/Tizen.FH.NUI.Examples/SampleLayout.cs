using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;
using StyleManager = Tizen.NUI.Components.DA.StyleManager;

namespace Tizen.FH.NUI.Examples
{
    public class SampleLayout : ImageView
    {
        private Components.Header LayoutHeader;

        private bool isThemeButtonVisible = true;
        private Tizen.NUI.Components.DA.Button UtilityButton;
        private Tizen.NUI.Components.DA.Button FoodButton;
        private Tizen.NUI.Components.DA.Button FamilyButton;
        private Tizen.NUI.Components.DA.Button KitchenButton;

        private View Content;
        private View LayoutContent;
        public string HeaderText
        {
            get
            {
                return LayoutHeader.Title;
            }
            set
            {
                LayoutHeader.Title = value;
            }
        }


        public new void Add(View view)
        {
            Content.Add(view);
        }


        public SampleLayout(bool isThemeButtonVisiable = true)
        {
            Size2D = new Size2D(Window.Instance.Size.Width, Window.Instance.Size.Height);
            //Window.Instance.Add(this);
            LayoutHeader = new Tizen.FH.NUI.Components.Header("DefaultHeader");
            LayoutHeader.PositionY = 0;

            LayoutContent = new View
            {
                Size2D = new Size2D(Window.Instance.Size.Width, Window.Instance.Size.Height - 128),
                Position2D = new Position2D(0, 128),
            };

            Content = new View
            {
                Size2D = new Size2D(Window.Instance.Size.Width, Window.Instance.Size.Height - 128 - 150),
                Position2D = new Position2D(0, 150),
            };
            LayoutContent.Add(Content);

            if (isThemeButtonVisiable)
            {
                ButtonStyle buttonAttributes = new ButtonStyle
                {
                    IsSelectable = true,
                    BackgroundImage =  new StringSelector { All = CommonResource.GetFHResourcePath() + "3. Button/rectangle_point_btn_normal.png" },
                    BackgroundImageBorder = new RectangleSelector { All = new Rectangle(5, 5, 5, 5) },
                    Shadow = new ImageViewStyle
                    {
                        ResourceUrl = new StringSelector { All = CommonResource.GetFHResourcePath() + "3. Button/rectangle_btn_shadow.png" },
                        Border = new RectangleSelector { All = new Rectangle(5, 5, 5, 5) }
                    },

                    Overlay = new ImageViewStyle
                    {
                        ResourceUrl = new StringSelector { Pressed = CommonResource.GetFHResourcePath() + "3. Button/rectangle_btn_press_overlay.png", Other = "" },
                        Border = new RectangleSelector { All = new Rectangle(5, 5, 5, 5) },
                    },

                    Text = new TextLabelStyle
                    {
                        PointSize = new FloatSelector { All = 30 },
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,

                        TextColor = new ColorSelector
                        {
                            All = new Color(0, 0, 0, 1),
                        },
                    }
                };

                UtilityButton = new Tizen.NUI.Components.DA.Button(buttonAttributes);
                UtilityButton.Size2D = new Size2D(200, 80);
                UtilityButton.Position2D = new Position2D(56, 32);
                UtilityButton.Text = "Utility";
                UtilityButton.ClickEvent += UtilityButton_ClickEvent;
                LayoutContent.Add(UtilityButton);

                buttonAttributes.BackgroundImage.All = CommonResource.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_point_btn_normal_ec7510.png";
                FoodButton = new Tizen.NUI.Components.DA.Button(buttonAttributes);
                FoodButton.Size2D = new Size2D(200, 80);
                FoodButton.Position2D = new Position2D(312, 32);
                FoodButton.Text = "Food";
                FoodButton.ClickEvent += FoodButton_ClickEvent;
                LayoutContent.Add(FoodButton);

                buttonAttributes.BackgroundImage.All = CommonResource.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_point_btn_normal_24c447.png";
                FamilyButton = new Tizen.NUI.Components.DA.Button(buttonAttributes);
                FamilyButton.Size2D = new Size2D(200, 80);
                FamilyButton.Position2D = new Position2D(578, 32);
                FamilyButton.Text = "Family";
                FamilyButton.ClickEvent += FamilyButton_ClickEvent;
                LayoutContent.Add(FamilyButton);

                buttonAttributes.BackgroundImage.All = CommonResource.GetFHResourcePath() + "3. Button/[Button] App Primary Color/rectangle_point_btn_normal_9762d9.png";
                KitchenButton = new Tizen.NUI.Components.DA.Button(buttonAttributes);
                KitchenButton.Size2D = new Size2D(200, 80);
                KitchenButton.Position2D = new Position2D(834, 32);
                KitchenButton.Text = "Kitchen";
                KitchenButton.ClickEvent += KitchenButton_ClickEvent;
                LayoutContent.Add(KitchenButton);
            }

            this.isThemeButtonVisible = isThemeButtonVisiable;
            Window.Instance.Add(LayoutHeader);
            Window.Instance.Add(LayoutContent);

            //SampleMain.SampleNaviFrame.NaviFrameItemPush(LayoutHeader, LayoutContent);

            //this.ResourceUrl = CommonResource.GetFHResourcePath() + "0. BG/background_default_overlay.png";
        }

        private void KitchenButton_ClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            StyleManager.Instance.Theme = "Kitchen";
        }

        private void FamilyButton_ClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            StyleManager.Instance.Theme = "Family";
        }

        private void FoodButton_ClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            StyleManager.Instance.Theme = "Food";
        }

        private void UtilityButton_ClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            StyleManager.Instance.Theme = "Utility";
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (LayoutContent != null)
                {
                    LayoutContent.Remove(Content);
                    Content.Dispose();
                    Content = null;

                    if (isThemeButtonVisible)
                    {
                        LayoutContent.Remove(UtilityButton);
                        UtilityButton.Dispose();
                        LayoutContent.Remove(FoodButton);
                        FoodButton.Dispose();
                        LayoutContent.Remove(FamilyButton);
                        FamilyButton.Dispose();
                        LayoutContent.Remove(KitchenButton);
                        KitchenButton.Dispose();
                    }

                    LayoutContent.GetParent().Remove(LayoutContent);
                    LayoutContent.Dispose();
                    LayoutContent = null;
                }

                if (LayoutHeader != null)
                {
                    LayoutHeader.GetParent().Remove(LayoutHeader);
                    LayoutHeader.Dispose();
                    LayoutHeader = null;
                }
            }

            base.Dispose(type);
        }
    }
}
