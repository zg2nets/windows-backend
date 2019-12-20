using System;
using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Accessibility
{
    public class ScreenReaderListItemData
    {
        private string main_text;

        public ScreenReaderListItemData(string main_t)
        {
            main_text = main_t;
        }

        public string MainText
        {
            get
            {
                return main_text;
            }
        }
    }

    public class ScreenReaderOnffItemView : View
    {
        private View root_view;
        private TextLabel main_label;
        private Switch on_off_switch;
        private View divider_view;

        public ScreenReaderOnffItemView(string main_text)
        {
            float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            float height_ratio = fs_ratio;
            if (height_ratio < 1.0f)
            {
                height_ratio = 1.0f;
            }

            root_view = new View();
            root_view.Position2D = new Position2D(48, 0);
            root_view.Size2D = new Size2D(1080 - 48 - 48, (int)(164 * height_ratio));

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.FontFamily = "SamsungOne 400";
            main_label.PointSize = 44 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            main_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            main_label.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            main_label.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            main_label.PositionUsesPivotPoint = true;
            root_view.Add(main_label);

            on_off_switch = new Switch("Switch");
            on_off_switch.Size2D = new Size2D(96, 60);
            on_off_switch.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
            on_off_switch.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
            on_off_switch.PositionUsesPivotPoint = true;
            root_view.Add(on_off_switch);

            divider_view = new View();
            divider_view.Size2D = new Size2D(1080 - 56 - 56, 1);
            divider_view.BackgroundColor = new Color(0f, 0f, 0f, 0.1f);
            divider_view.PositionUsesPivotPoint = true;
            divider_view.ParentOrigin = Tizen.NUI.ParentOrigin.BottomCenter;
            divider_view.PivotPoint = Tizen.NUI.PivotPoint.BottomCenter;
            root_view.Add(divider_view);

            Add(root_view);
        }

        public TextLabel MainTextLabel
        {
            get
            {
                return main_label;
            }
        }

        public Switch OnOffSwitch
        {
            get
            {
                return on_off_switch;
            }
        }
    }

    public class DescriptionItemView : View
    {
        private View root_view;
        public TextLabel main_label;

        public DescriptionItemView(string main_text)
        {
            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.FontFamily = "SamsungOne 400";
            main_label.PointSize = 30;
            main_label.MultiLine = true;
            main_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            main_label.Position2D = new Position2D(48, 36);
            main_label.SizeWidth = 984;
            main_label.SizeHeight = main_label.GetHeightForWidth(main_label.SizeWidth);
            root_view.Add(main_label);

            Add(root_view);
        }

        public float MainLabelHeight
        {
            get
            {
                return main_label.SizeHeight;
            }
        }
    }

    public class InteractionItemView : View
    {
        private View root_view;
        private TextLabel main_label;
        private View divider_view;

        public InteractionItemView(string main_text)
        {
            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.FontFamily = "SamsungOne 400";
            main_label.PointSize = 30;
            main_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            main_label.Position2D = new Position2D(48, 36);
            main_label.MultiLine = true;
            main_label.EnableMarkup = true;
            main_label.LineWrapMode = LineWrapMode.Word;
            main_label.SizeWidth = 984;
            main_label.SizeHeight = main_label.GetHeightForWidth(main_label.SizeWidth);
            root_view.Add(main_label);

            divider_view = new View();
            divider_view.Position2D = new Position2D(48, (int)(36 + main_label.SizeHeight + 36));
            divider_view.Size2D = new Size2D(1080 - 48 - 48, 1);
            divider_view.BackgroundColor = new Color(0f, 0f, 0f, 0.1f);
            root_view.Add(divider_view);

            Add(root_view);
        }

        public float MainLabelHeight
        {
            get
            {
                return main_label.SizeHeight;
            }
        }
    }

    public class ScreenReaderListBridge : FlexibleView.Adapter
    {
        private FlexibleView mFlexibleView;
        private List<ScreenReaderListItemData> mDatas;
        private Dictionary<int, FlexibleView.ViewHolder> mViewHolders;

        public ScreenReaderListBridge(FlexibleView view, List<ScreenReaderListItemData> datas)
        {
            mFlexibleView = view;
            mDatas = datas;
            mViewHolders = new Dictionary<int, FlexibleView.ViewHolder>();
        }

        public void UpdateItemViews()
        {
            float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            float height_ratio = fs_ratio;
            if (height_ratio < 1.0f)
            {
                height_ratio = 1.0f;
            }

            // update font size and height of screen reader switch.
            ScreenReaderOnffItemView listItemView0 = mViewHolders[0].ItemView as ScreenReaderOnffItemView;
            listItemView0.MainTextLabel.PointSize = 44 * fs_ratio;
            listItemView0.SizeHeight = 164 * height_ratio;

            // update font size of basic interaction.
            ListItem listItemView1 = mViewHolders[2].ItemView as ListItem;
            listItemView1.MainTextPointSize = 24 * fs_ratio;
        }

        public override int GetItemViewType(int position)
        {
            return position;  // position is used as item view type.
        }

        public override FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
        {
            View item_view = null;

            ScreenReaderListItemData listItemData = mDatas[viewType]; // viewType is position actually.

            switch (viewType)
            {
                case 0:
                    {
                        ScreenReaderOnffItemView on_off_item_view = new ScreenReaderOnffItemView(listItemData.MainText);
                        bool state = AccessibilityController.Instance.accessibility_screen_reader_state_get();
                        on_off_item_view.OnOffSwitch.IsSelected = state;
                        on_off_item_view.OnOffSwitch.SelectedEvent += OnScreenOnOffSelectedEvent;

                        //Interop.Accessibility.accessibility_add_changed_cb(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SCREEN_READER,
                        //    new Interop.Accessibility.accessibility_changed_cb(OnAccessibilityChanged), (IntPtr)0);

                        item_view = on_off_item_view;
                        break;
                    }
                case 1:
                    item_view = new DescriptionItemView(listItemData.MainText);
                    break;
                case 2:
                    item_view = new ListItem("GroupIndexListItem");
                    break;
                case 3:
                    item_view = new InteractionItemView(listItemData.MainText);
                    break;
                default:
                    break;
            }

            FlexibleView.ViewHolder viewHolder = new FlexibleView.ViewHolder(item_view);
            mViewHolders[viewType] = viewHolder;

            return viewHolder;
        }

        private void OnAccessibilityChanged(Interop.Accessibility.accessibility_key_e key, IntPtr user_data)
        {
            bool srs = AccessibilityController.Instance.accessibility_screen_reader_state_get();
            string sr_state = srs ? "On" : "Off";

            FlexibleView.ViewHolder vh = mFlexibleView.FindViewHolderForAdapterPosition(0);
            ListItem listItemView = vh.ItemView as ListItem;
            listItemView.MainText = sr_state;
        }

        private void OnScreenOnOffSelectedEvent(object sender, Switch.SelectEventArgs args)
        {
            Switch nc = sender as Switch;
            if (nc != null)
            {
                Tizen.Log.Info("accessibility", "grayscale selected: " + nc.IsSelected);
                AccessibilityController.Instance.accessibility_screen_reader_state_set(nc.IsSelected);
            }
        }

        public override void OnBindViewHolder(FlexibleView.ViewHolder holder, int position)
        {
            switch(position)
            {
                case 0:
                    {
                        float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
                        float height_ratio = fs_ratio;
                        if (height_ratio < 1.0f)
                        {
                            height_ratio = 1.0f;
                        }
                        ScreenReaderOnffItemView listItemView = holder.ItemView as ScreenReaderOnffItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164 * height_ratio;
                        break;
                    }
                case 1:
                    {
                        DescriptionItemView listItemView = holder.ItemView as DescriptionItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = listItemView.MainLabelHeight + 72;
                        break;
                    }
                case 2:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        ScreenReaderListItemData listItemData = mDatas[position];
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 48;
                        listItemView.StartSpace = 48;
                        listItemView.MainText = listItemData.MainText;
                        break;
                    }
                case 3:
                    {
                        InteractionItemView listItemView = holder.ItemView as InteractionItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = listItemView.MainLabelHeight + 36;
                        break;
                    }
                default:
                    break;
            }
        }

        public override void OnDestroyViewHolder(FlexibleView.ViewHolder holder)
        {
            if (holder.ItemView != null)
            {
                holder.ItemView.Dispose();
            }
        }

        public override int GetItemCount()
        {
            return mDatas.Count;
        }
    }
    public class AccessibilityScreenReader : IViewAdapter
    {
        public void Activate()
        {
            root_view = new View();
            Window.Instance.GetDefaultLayer().Add(root_view);

            header = new Header("DefaultHeader");
            header.BackgroundColor = new Color(1f, 1f, 1f, 0.7f);
            header.Title = "Screen Reader";
            root_view.Add(header);

            content_list = new FlexibleView();
            content_list.Name = "Screen Reader List";
            content_list.Position2D = new Position2D(0, (int)header.SizeHeight);
            content_list.Size2D = new Size2D(1080, 1596);
            content_list.Padding = new Extents(0, 0, 0, 0);
            content_list.ItemClickEvent += OnListItemClickEvent;
            content_list.StyleChanged += OnStyleChanged;

            List<ScreenReaderListItemData> dataList = new List<ScreenReaderListItemData>();
            dataList.Add(new ScreenReaderListItemData("Screen Reader On/Off"));
            dataList.Add(new ScreenReaderListItemData("When Screen reader is on, your device provides spoken feedback to help " +
                                                      "blind and low-vision users.\n" + 
                                                      "For example, it describes what you touch, select and activate.\n" +
                                                      "Before you use, Please check the basic interaction of Screen reader."));
            dataList.Add(new ScreenReaderListItemData("Basic interaction"));
            dataList.Add(new ScreenReaderListItemData("<b>Tap</b> - Select an object\n" +
                                                      "<b>Double tap</b> - Run the object\n" +
                                                      "<b>Double tap and hold</b> - Execute a long tap function\n" +
                                                      "<b>Tap and move</b> - Select an object and move focus\n" +
                                                      "<b>Swipe</b> - Select the previous or next object\n" +
                                                      "<b>Two-finger swipe</b> - Scroll the page or unlock\n" + 
                                                      "<b>Two-finger tap and move</b> - Pan"));

            adapter = new ScreenReaderListBridge(content_list, dataList);
            content_list.SetAdapter(adapter);

            LinearLayoutManager layoutManager = new LinearLayoutManager(LinearLayoutManager.Orientation.VERTICAL);
            content_list.SetLayoutManager(layoutManager);
            root_view.Add(content_list);
        }

        private void OnStyleChanged(object sender, Tizen.NUI.StyleManager.StyleChangedEventArgs args)
        {
            if (args.StyleChange == StyleChangeType.DefaultFontSizeChange)
            {
                adapter.UpdateItemViews();
            }
        }

        public void Deactivate()
        {
            if (content_list != null)
            {
                root_view.Remove(content_list);
                content_list.Dispose();
                content_list = null;
            }
            if (header != null)
            {
                root_view.Remove(header);
                header.Dispose();
                header = null;
            }
            if (root_view != null)
            {
                Window.Instance.GetDefaultLayer().Remove(root_view);
                root_view.Dispose();
                root_view = null;
            }
        }

        public View GetHeader()
        {
            return header;
        }

        public View GetContent()
        {
            return content_list;
        }

        private void OnListItemClickEvent(object sender, FlexibleView.ItemClickEventArgs e)
        {
            if (e.ClickedView != null)
            {
                int index = e.ClickedView.AdapterPosition;

                DescriptionItemView item_view = e.ClickedView.ItemView as DescriptionItemView;
                if (item_view)
                {
                    int w = item_view.main_label.NaturalSize2D.Width;
                    int h = item_view.main_label.NaturalSize2D.Height;
                    int aaa = w * h;
                }

            }
        }

        private View root_view;
        private Header header;
        private FlexibleView content_list;
        private ScreenReaderListBridge adapter;
    }
}
