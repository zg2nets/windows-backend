using Tizen.NUI.BaseComponents;
using System;
using Tizen.NUI;

namespace Tizen.FH.NUI.Examples
{
    public class ListItem : IExample
    {
        private SampleLayout root;
        private const int COUNT = 12;
        private View rootView = null;
        private Tizen.FH.NUI.Components.ListItem[] listItemArray = null;
        private int itemPosY, itemPosX;
        private int itemPosYOffset;
        private uint index;
        private Tizen.NUI.Components.DA.Button button;

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            CreateRootView();

            itemPosX = 100;
            itemPosY = -80;
            itemPosYOffset = 20;
            listItemArray = new Tizen.FH.NUI.Components.ListItem[COUNT];

            index = 0;
            CreateListItem(index, "DefaultListItem", 90, 0, "default");

            index++;
            CreateListItem(index, "MultiSubTextListItem", 150, 1, "sub text 1line");

            index++;
            CreateListItem(index, "MultiSubTextListItem", 220, 2, "sub text 2line");
            listItemArray[index].StateDividerEnabled = false;

            index++;
            // checkBox + text
            CreateListItem(index, "ItemAlignListItem", 90, 0, "item align");
            listItemArray[index].StartItemRootViewSize = new Size(48, 48);
            listItemArray[index].ItemAlignType = Tizen.FH.NUI.Components.ListItem.ItemAlignTypes.CheckIcon;
            listItemArray[index].EndItemRootViewSize = new Size(200, 90);
            listItemArray[index].EndText = "Sub text";

            index++;
            // icon
            CreateListItem(index, "ItemAlignListItem", 90, 0, "item align, icon");
            listItemArray[index].StartItemRootViewSize = new Size(48, 48);
            listItemArray[index].ItemAlignType = Tizen.FH.NUI.Components.ListItem.ItemAlignTypes.Icon;
            listItemArray[index].StartIconURL = CommonResource.GetFHResourcePath() + "0. Softkey/softkey_ic_home.png";

            index++;
            CreateListItem(index, "EffectListItem", 90, 0, "effect");

            index++;
            //itemPosX = 100 + 800 + 100;
            //itemPosY = 50;
            CreateListItem(index, "NextDepthListItem", 90, 0, "next depth");

            index++;
            CreateListItem(index, "GroupIndexListItem", 90, 0, "group index, default");
            listItemArray[index].GroupIndexType = Tizen.FH.NUI.Components.ListItem.GroupIndexTypes.None;

            index++;
            CreateListItem(index, "GroupIndexListItem", 90, 0, "group index, next button");
            listItemArray[index].GroupIndexType = Tizen.FH.NUI.Components.ListItem.GroupIndexTypes.Next;
            listItemArray[index].EndItemRootViewSize = new Size(48, 48);

            index++;
            CreateListItem(index, "GroupIndexListItem", 90, 0, "group index, switch");
            listItemArray[index].GroupIndexType = Tizen.FH.NUI.Components.ListItem.GroupIndexTypes.Switch;
            listItemArray[index].EndItemRootViewSize = new Size(72, 48);

            index++;
            CreateListItem(index, "GroupIndexListItem", 90, 0, "group index, drop down");
            listItemArray[index].GroupIndexType = Tizen.FH.NUI.Components.ListItem.GroupIndexTypes.DropDown;
            listItemArray[index].EndItemRootViewSize = new Size(48, 48);

            button = new Tizen.NUI.Components.DA.Button();
            button.PointSize = 14;
            button.Size2D = new Size2D(300, 80);
            button.BackgroundColor = Color.Green;
            button.Position2D = new Position2D(itemPosX + 10, itemPosY);
            button.Text = "LTR/RTL";
            button.ClickEvent += OnLayoutChanged;
            rootView.Add(button);
            Window.Instance.KeyEvent += OnWindowsKeyEvent;
        }
        private void OnLayoutChanged(object sender, global::System.EventArgs e)
        {
            for (int i = 0; i < COUNT; ++i)
            {
                if (listItemArray[i])
                {
                    if (listItemArray[i].LayoutDirection == ViewLayoutDirectionType.LTR)
                    {
                        listItemArray[i].LayoutDirection = ViewLayoutDirectionType.RTL;
                    }
                    else
                    {
                        listItemArray[i].LayoutDirection = ViewLayoutDirectionType.LTR;

                    }
                }
            }
        }

        private void CreateRootView()
        {
            root = new SampleLayout(false);
            root.HeaderText = "List Item";
            rootView = new View();
            rootView.WidthResizePolicy = ResizePolicyType.FillToParent;
            rootView.HeightResizePolicy = ResizePolicyType.FillToParent;
            rootView.BackgroundColor = Color.White;// new Color(78.0f / 255.0f, 216.0f / 255.0f, 231.0f / 255.0f, 1.0f);
            rootView.Focusable = true;
            root.Add(rootView);
        }

        private void CreateListItem(uint idx, string str, int height, uint subTextCount, string mainTextStr)
        {
            listItemArray[idx] = new Components.ListItem(str);
            rootView.Add(listItemArray[idx]);
            listItemArray[idx].MainText = mainTextStr;
            listItemArray[idx].Size2D = new Size2D(800, height);
            listItemArray[idx].Position2D = new Position2D(itemPosX, itemPosY);
            itemPosY += height;
            itemPosY += itemPosYOffset;
            Random randomGenerator = new Random();
            float r = (float)randomGenerator.NextDouble();
            float g = (float)randomGenerator.NextDouble();
            float b = (float)randomGenerator.NextDouble();
            listItemArray[idx].BackgroundColor = new Color(r, g, b, 0.2f);
            listItemArray[idx].SubTextCount = subTextCount;
            string[] strArray = new string[subTextCount];
            for (int i = 0; i < subTextCount; ++i)
            {
                strArray[i] = "sub text" + (i + 1).ToString();
            }
            listItemArray[idx].SubTextContentArray = strArray;
        }

        private void OnWindowsKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Right")
                {
                    listItemArray[5].StateSelectedEnabled = !listItemArray[5].StateSelectedEnabled;
                }
                else if (e.Key.KeyPressedName == "Left")
                {
                    listItemArray[5].StateEnabled = !listItemArray[5].StateEnabled;
                }
            }
        }

        public void Deactivate()
        {
            Window.Instance.KeyEvent -= OnWindowsKeyEvent;
            if (listItemArray != null)
            {
                for (int i = 0; i < COUNT; ++i)
                {
                    if (listItemArray[i] != null)
                    {
                        rootView.Remove(listItemArray[i]);
                        listItemArray[i].Dispose();
                        listItemArray[i] = null;
                    }
                }
                listItemArray = null;
            }
            if (button != null)
            {
                rootView.Remove(button);
                button.Dispose();
                button = null;
            }
            if (rootView != null)
            {
                root.Remove(rootView);
                rootView.Dispose();
                rootView = null;
            }

            root.Dispose();
        }
    }
}
