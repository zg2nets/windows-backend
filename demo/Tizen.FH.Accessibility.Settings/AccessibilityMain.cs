using System;
using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Accessibility
{
    internal class AccessibilityListItemData
    {
        private string main_text;
        private string sub_text;

        public AccessibilityListItemData(string main_t, string sub_t)
        {
            main_text = main_t;
            sub_text = sub_t;
        }

        public string MainText
        {
            get
            {
                return main_text;
            }
        }

        public string SubText
        {
            get
            {
                return sub_text;
            }
        }
    }

    internal class MainSwitchListItemView : View
    {
        private View root_view;
        private TextLabel main_label;
        private Switch switch_control;
        private View divider_view;

        public MainSwitchListItemView(string main_text)
        {
            float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            float height_ratio = fs_ratio;
            if (height_ratio < 1.0f)
            {
                height_ratio = 1.0f;
            }

            root_view = new View();
            root_view.Position2D = new Position2D(56, 0);
            root_view.HeightResizePolicy = ResizePolicyType.FillToParent;
            root_view.Size2D = new Size2D(1080 - 56 - 56, (int)(164 * height_ratio));

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.FontFamily = "SamsungOne 400";
            main_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            main_label.PositionUsesPivotPoint = true;
            main_label.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            main_label.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            root_view.Add(main_label);

            switch_control = new Switch("Switch");
            switch_control.Size2D = new Size2D(96, 60);
            switch_control.PositionUsesPivotPoint = true;
            switch_control.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
            switch_control.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
            root_view.Add(switch_control);

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

        public Switch SwitchControl
        {
            get
            {
                return switch_control;
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (main_label != null)
                {
                    root_view.Remove(main_label);
                    main_label.Dispose();
                    main_label = null;
                }
                if (switch_control != null)
                {
                    root_view.Remove(switch_control);
                    switch_control.Dispose();
                    switch_control = null;
                }
                if (divider_view != null)
                {
                    root_view.Remove(divider_view);
                    divider_view.Dispose();
                    divider_view = null;
                }
                if (root_view != null)
                {
                    Remove(root_view);
                    root_view = null;
                }
            }

            base.Dispose(type);
        }
    }

    internal class AccessibilityListBridge : FlexibleView.Adapter
    {
        private FlexibleView mFlexibleView;
        private List<AccessibilityListItemData> mDatas;
        private Dictionary<int, FlexibleView.ViewHolder> mViewHolders;

        public AccessibilityListBridge(FlexibleView view, List<AccessibilityListItemData> datas)
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

            // multi sub
            ListItem listItemView0 = mViewHolders[0].ItemView as ListItem;
            listItemView0.MainTextPointSize = 44 * fs_ratio;
            listItemView0.SubTextPointSize = 30 * fs_ratio;
            listItemView0.SizeHeight = 164 * height_ratio;

            // default
            ListItem listItemView1 = mViewHolders[1].ItemView as ListItem;
            listItemView1.MainTextPointSize = 44 * fs_ratio;
            listItemView1.SizeHeight = 164 * height_ratio;

            // multi sub
            ListItem listItemView2 = mViewHolders[2].ItemView as ListItem;
            listItemView2.MainTextPointSize = 44 * fs_ratio;
            listItemView2.SubTextCount = 1;
            AccessibilityController.FontSize fs = AccessibilityController.Instance.accessibility_font_size_get();
            listItemView2.SubTextContentArray = new string[1] { AccessibilityController.Instance.accessibility_convert_font_size_to_text(fs) };
            listItemView2.SubTextPointSize = 30 * fs_ratio;
            listItemView2.SizeHeight = 164 * height_ratio;

            // text switch
            MainSwitchListItemView listItemView3 = mViewHolders[3].ItemView as MainSwitchListItemView;
            listItemView3.MainTextLabel.PointSize = 44 * fs_ratio;
            listItemView3.SizeHeight = 164 * height_ratio;

            // text switch
            MainSwitchListItemView listItemView4 = mViewHolders[4].ItemView as MainSwitchListItemView;
            listItemView4.MainTextLabel.PointSize = 44 * fs_ratio;
            listItemView4.SizeHeight = 164 * height_ratio;

            // default
            ListItem listItemView5 = mViewHolders[5].ItemView as ListItem;
            listItemView5.MainTextPointSize = 44 * fs_ratio;
            listItemView5.SizeHeight = 164 * height_ratio;
        }

        public override int GetItemViewType(int position)
        {
            return position;  // position is used as item view type.
        }

        public override FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
        {
            View item_view = null;

            AccessibilityListItemData listItemData = mDatas[viewType];

            switch (viewType)
            {
                case 0:
                case 2:
                    item_view = new ListItem("MultiSubTextListItem");
                    break;
                case 1:
                case 5:
                    item_view = new ListItem("DefaultListItem");
                    break;
                case 3:
                    {
                        MainSwitchListItemView negative_color_item_view = new MainSwitchListItemView(listItemData.MainText);
                        bool state = AccessibilityController.Instance.accessibility_grayscale_state_get();
                        negative_color_item_view.SwitchControl.SelectedEvent += OnGrayScaleSelectedEvent;
                        negative_color_item_view.SwitchControl.IsSelected = state;
                        item_view = negative_color_item_view;
                        break;
                    }
                case 4:
                    {
                        MainSwitchListItemView negative_color_item_view = new MainSwitchListItemView(listItemData.MainText);
                        bool state = AccessibilityController.Instance.accessibility_negative_color_state_get();
                        negative_color_item_view.SwitchControl.SelectedEvent += OnNegativeColorSelectedEvent;
                        negative_color_item_view.SwitchControl.IsSelected = state;
                        item_view = negative_color_item_view;
                        break;
                    }
                default:
                    break;
            }

            FlexibleView.ViewHolder viewHolder = new FlexibleView.ViewHolder(item_view);
            mViewHolders[viewType] = viewHolder;

            return viewHolder;
        }

        private void OnGrayScaleSelectedEvent(object sender, Switch.SelectEventArgs args)
        {
            Switch nc = sender as Switch;
            if (nc != null)
            {
                Tizen.Log.Info("accessibility", "grayscale selected: " + nc.IsSelected);
                AccessibilityController.Instance.accessibility_grayscale_state_set(nc.IsSelected);
            }
        }

        private void OnNegativeColorSelectedEvent(object sender, Switch.SelectEventArgs args)
        {
            Switch nc = sender as Switch;
            if (nc != null)
            {
                Tizen.Log.Info("accessibility", "negativecolor selected: " + nc.IsSelected);
                AccessibilityController.Instance.accessibility_negative_color_state_set(nc.IsSelected);
            }
        }

        public override void OnBindViewHolder(FlexibleView.ViewHolder holder, int position)
        {
            float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            float height_ratio = fs_ratio;
            if (height_ratio < 1.0f)
            {
                height_ratio = 1.0f;
            }

            AccessibilityListItemData listItemData = mDatas[position];
            switch (position)
            {
                case 0:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        listItemView.Name = "Item" + position;
                        listItemView.Size2D = new Size2D(1080, (int)(164 * height_ratio));
                        listItemView.MainText = listItemData.MainText;
                        listItemView.MainTextPointSize = 44 * fs_ratio;
                        listItemView.Margin = new Extents(0, 0, 0, 0);

                        bool srs = AccessibilityController.Instance.accessibility_screen_reader_state_get();
                        string sr_state = srs ? "On" : "Off";

                        listItemView.SubTextCount = 1;
                        listItemView.SubTextContentArray = new string[1] { sr_state };
                        listItemView.SubTextPointSize = 30 * fs_ratio;

                        //Interop.Accessibility.accessibility_add_changed_cb(Interop.Accessibility.accessibility_key_e.ACCESSIBILITY_KEY_SCREEN_READER,
                        //    new Interop.Accessibility.accessibility_changed_cb(OnAccessibilityChanged), (IntPtr)0);

                        break;
                    }
                case 2:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        listItemView.Name = "Item" + position;
                        listItemView.Size2D = new Size2D(1080, (int)(164 * height_ratio));
                        listItemView.MainText = listItemData.MainText;
                        listItemView.MainTextPointSize = 44 * fs_ratio;

                        listItemView.SubTextCount = 1;
                        listItemView.SubTextContentArray = new string[1] { listItemData.SubText };
                        listItemView.SubTextPointSize = 30 * fs_ratio;
                        break;
                    }
                case 1:
                case 5:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        listItemView.Name = "Item" + position;
                        listItemView.MainText = listItemData.MainText;
                        listItemView.MainTextPointSize = 44 * fs_ratio;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164 * height_ratio;
                        break;
                    }
                case 3:
                case 4:
                    {
                        MainSwitchListItemView listItemView = holder.ItemView as MainSwitchListItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.MainTextLabel.PointSize = 44 * fs_ratio;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164 * height_ratio;
                        break;
                    }
                default:
                    break;
            }
        }

        private void OnAccessibilityChanged(Interop.Accessibility.accessibility_key_e key, IntPtr user_data)
        {
            bool srs = AccessibilityController.Instance.accessibility_screen_reader_state_get();
            string sr_state = srs ? "On" : "Off";

            FlexibleView.ViewHolder vh = mFlexibleView.FindViewHolderForAdapterPosition(0);
            ListItem listItemView = vh.ItemView as ListItem;
            listItemView.MainText = sr_state;
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

    public class AccessibilityMain : IViewAdapter
    {
        public void Activate()
        {
            root_view = new View();
            Window.Instance.GetDefaultLayer().Add(root_view);

            header = new Header("DefaultHeader");
            header.BackgroundColor = new Color(1f, 1f, 1f, 0.7f);
            header.Title = "Accessibility";
            root_view.Add(header);

            content_list = new FlexibleView();
            content_list.Name = "Accessibility List";
            content_list.Position = new Position(0, (int)header.SizeHeight);
            content_list.Size = new Size(1080, 1920 - (int)header.SizeHeight);
            content_list.Padding = new Extents(0, 0, 0, 0);
            content_list.ItemClickEvent += OnListItemClickEvent;
            content_list.StyleChanged += OnStyleChanged;

            List<AccessibilityListItemData> dataList = new List<AccessibilityListItemData>();

            dataList.Add(new AccessibilityListItemData("Screen Reader", ""));
            dataList.Add(new AccessibilityListItemData("Text-to-speech Option", ""));

            AccessibilityController.FontSize fs = AccessibilityController.Instance.accessibility_font_size_get();
            dataList.Add(new AccessibilityListItemData("Font Size", AccessibilityController.Instance.accessibility_convert_font_size_to_text(fs)));

            dataList.Add(new AccessibilityListItemData("Grayscale", "On"));
            dataList.Add(new AccessibilityListItemData("Negative color", "On"));
            dataList.Add(new AccessibilityListItemData("Accessible Screen", ""));
            //dataList.Add(new AccessibilityListItemData("Show side back key", "On"));

            adapter = new AccessibilityListBridge(content_list, dataList);
            content_list.SetAdapter(adapter);

            LinearLayoutManager layoutManager = new LinearLayoutManager(LinearLayoutManager.Orientation.VERTICAL);
            content_list.SetLayoutManager(layoutManager);

            root_view.Add(content_list);
            Window.Instance.KeyEvent += Instance_Key;
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
                switch (index)
                {
                    case 0:
                    case 2:
                        {
                            string sampleName = (e.ClickedView.ItemView as ListItem)?.MainText;
                            AccessibilityApplication.Instance.CreateView(sampleName);
                            break;
                        }
                    case 1:
                    case 5:
                        {
                            string sampleName = (e.ClickedView.ItemView as ListItem)?.MainText;
                            AccessibilityApplication.Instance.CreateView(sampleName);
                            break;
                        }
                    case 3:
                    case 4:
                        {
                            MainSwitchListItemView itemView = e.ClickedView.ItemView as MainSwitchListItemView;
                            //itemView.SwitchControl.ChangeSelectedState(!itemView.SwitchControl.IsSelected);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        private void Instance_Key(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "BackSpace"
                || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                AccessibilityApplication.Instance.RemoveView();
            }
        }

        private View root_view;
        private Header header;
        private FlexibleView content_list;
        private AccessibilityListBridge adapter;
    }
}

