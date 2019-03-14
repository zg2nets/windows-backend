﻿using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Controls;

namespace Tizen.NUI.Samples
{
    public class TabSample : IExample
    {
        private View root;

        private TextLabel[] createText = new TextLabel[2];

        private Tab tab = null;
        private Tab tab2 = null;

        private Button button = null;
        private int index = 0;

        private static string[] mode = new string[]
        {
            "Utility Tab",
            "Family Tab",
            "Food Tab",
            "Kitchen Tab",
        };
        private static Color[] color = new Color[]
        {
        new Color(0.05f, 0.63f, 0.9f, 1),//#ff0ea1e6 Utility
        new Color(0.14f, 0.77f, 0.28f, 1),//#ff24c447 Family
        new Color(0.75f, 0.46f, 0.06f, 1),//#ffec7510 Food
        new Color(0.59f, 0.38f, 0.85f, 1),//#ff9762d9 Kitchen
        };
        public void Activate()
        {
            Window window = Window.Instance;

            root = new View()
            {
                Size2D = new Size2D(1920, 1080),
            };
            window.Add(root);

            ///////////////////////////////////////////////Create by Property//////////////////////////////////////////////////////////
            createText[0] = new TextLabel();
            createText[0].Text = "Create Tab just by properties";
            createText[0].Size2D = new Size2D(450, 100);
            createText[0].Position2D = new Position2D(200, 100);
            createText[0].MultiLine = true;
            root.Add(createText[0]);

            tab = new Tab();
            tab.Size2D = new Size2D(700, 108);
            tab.Position2D = new Position2D(100, 300);
            tab.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            tab.IsNatureTextWidth = true;
            tab.ItemGap = 40;
            tab.LeftSpace = 56;
            tab.RightSpace = 56;
            tab.TopSpace = 1;
            tab.BottomSpace = 0;
            tab.UnderLineSize2D = new Size2D(1, 3);
            tab.UnderLineBackgroundColor = color[0];
            tab.PointSize = 25;
            tab.TextColorSelector = new ColorSelector
            {
                Normal = Color.Black,
                Selected = color[0],
            };
            tab.ItemChangedEvent += TabItemChangedEvent;
            root.Add(tab);

            for (int i = 0; i < 3; i++)
            {
                Tab.TabItem item = new Tab.TabItem();
                item.Text = "Tab " + i;
                if(i==1)
                {
                    item.Text = "Long Tab " + i;
                }
                tab.AddItem(item);
            }
            tab.SelectedItemIndex = 0;

            ///////////////////////////////////////////////Create by Attributes//////////////////////////////////////////////////////////
            createText[1] = new TextLabel();
            createText[1].Text = "Create Tab just by Attributes";
            createText[1].Size2D = new Size2D(450, 100);
            createText[1].Position2D = new Position2D(1000, 100);
            createText[1].MultiLine = true;
            root.Add(createText[1]);

            TabAttributes attrs = new TabAttributes
            {
                IsNatureTextWidth = false,
                Space = new Vector4(56, 56, 1, 0),
                UnderLineAttributes = new ViewAttributes
                {
                    Size2D = new Size2D(1, 3),
                    PositionUsesPivotPoint = true,
                    ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.BottomLeft,
                    BackgroundColor = new ColorSelector { All = color[0]},
                },
                TextAttributes = new TextAttributes
                {
                    PointSize = new FloatSelector { All = 25 },
                    TextColor = new ColorSelector
                    {
                        Normal = Color.Black,
                        Selected = color[0],
                    },
                },                
            };

            tab2 = new Tab(attrs);
            tab2.Size2D = new Size2D(500, 108);
            tab2.Position2D = new Position2D(900, 300);
            tab2.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            tab2.ItemChangedEvent += Tab2ItemChangedEvent;
            root.Add(tab2);

            for (int i = 0; i < 3; i++)
            {
                Tab.TabItem item = new Tab.TabItem();
                item.Text = "Tab " + i;
                tab2.AddItem(item);
            }
            tab2.SelectedItemIndex = 0;

            button = new Button();
            button.BackgroundImageURL = CommonReosurce.GetTVResourcePath() + "component/c_buttonbasic/c_basic_button_white_bg_normal_9patch.png";
            button.BackgroundImageBorder = new Rectangle(4, 4, 5, 5);
            button.Size2D = new Size2D(280, 80);
            button.Position2D = new Position2D(500, 700);
            button.Text = mode[index];
            button.ClickEvent += ButtonClickEvent;
            root.Add(button);
        }

        private void TabItemChangedEvent(object sender, Tab.ItemChangeEventArgs e)
        {
            createText[0].Text = "Create Tab just by properties, Selected index from " + e.PreviousIndex + " to " + e.CurrentIndex;
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (button != null)
                {
                    root.Remove(button);
                    button.Dispose();
                    button = null;
                }

                if (tab != null)
                {
                    root.Remove(tab);
                    tab.Dispose();
                    tab = null;
                }
                if (tab2 != null)
                {
                    root.Remove(tab2);
                    tab2.Dispose();
                    tab2 = null;
                }

                if (createText[0] != null)
                {
                    root.Remove(createText[0]);
                    createText[0].Dispose();
                    createText[0] = null;
                }
                if (createText[1] != null)
                {
                    root.Remove(createText[1]);
                    createText[1].Dispose();
                    createText[1] = null;
                }

                Window.Instance.Remove(root);
                root.Dispose();
                root = null;
            }
        }

        private void Tab2ItemChangedEvent(object sender, Tab.ItemChangeEventArgs e)
        {
            createText[1].Text = "Create Tab just by Attributes, Selected index from " + e.PreviousIndex + " to " + e.CurrentIndex;
        }

        private void ButtonClickEvent(object sender, Button.ClickEventArgs e)
        {
            index = (index + 1) % 4;
            button.Text = mode[index];
            tab.UnderLineBackgroundColor = color[index];
            tab.TextColorSelector = new ColorSelector
            {
                Normal = Color.Black,
                Selected = color[index],
            };
            tab2.UnderLineBackgroundColor = color[index];
            tab2.TextColorSelector = new ColorSelector
            {
                Normal = Color.Black,
                Selected = color[index],
            };
        }
    }
}
