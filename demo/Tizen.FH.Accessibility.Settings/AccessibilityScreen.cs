using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
//using Tizen.NUI.Components;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Accessibility
{
    public class AccessibleScreenListItemData
    {
        private string main_text;

        public AccessibleScreenListItemData(string main_t)
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

    public class ScreenOnOffItemView : View
    {
        private View root_view;
        private TextLabel main_label;
        private Switch on_off_switch;
        private View divider_view;
        private FlexibleView flexible_view;

        public ScreenOnOffItemView(FlexibleView list_view, string main_text)
        {
            flexible_view = list_view;

            root_view = new View();
            root_view.Position2D = new Position2D(56, 0);
            root_view.Size2D = new Size2D(1080 - 56 - 56, 164);

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.FontFamily = "SamsungOne 500C";
            main_label.PointSize = 40 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
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

            bool state = AccessibilityController.Instance.accessibility_accessible_screen_get();
            on_off_switch.IsSelected = state;
            on_off_switch.SelectedEvent += OnScreenOnOffSelectedEvent;

            divider_view = new View();
            divider_view.Position2D = new Position2D(0, 164);
            divider_view.Size2D = new Size2D(986, 1);
            divider_view.BackgroundColor = new Color(0f, 0f, 0f, 0.1f);
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

        private void OnScreenOnOffSelectedEvent(object sender, Switch.SelectEventArgs args)
        {
            Switch nc = sender as Switch;
            if (nc != null)
            {
                Tizen.Log.Info("accessibility", "screen on or off: " + nc.IsSelected);
                AccessibilityController.Instance.accessibility_accessible_screen_set(nc.IsSelected);

                // change the state of slider where postion is 1
                FlexibleView.ViewHolder view_holder = flexible_view.FindViewHolderForAdapterPosition(1);
                ScreenSliderItemView slider_view = view_holder.ItemView as ScreenSliderItemView;
                if (slider_view != null)
                {
                    if (!nc.IsSelected)
                        slider_view.RateSlider.ControlState = Tizen.NUI.Components.ControlStates.Disabled;
                }
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
                flexible_view = null;

                if (main_label != null)
                {
                    root_view.Remove(main_label);
                    main_label.Dispose();
                    main_label = null;
                }
                if (main_label != null)
                {
                    root_view.Remove(main_label);
                    main_label.Dispose();
                    main_label = null;
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

    public class ScreenSliderItemView : View
    {
        private View root_view;
        private Slider rate_slider;
        private View text_view;
        private TextLabel[] percent_label;
        private View divider_view;

        public ScreenSliderItemView()
        {
            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            rate_slider = new Slider("DefaultSlider");
            rate_slider.Direction = Slider.DirectionType.Horizontal;
            rate_slider.Position2D = new Position2D(64, 52);
            rate_slider.Size2D = new Size2D(952, 60);
            rate_slider.MinValue = 10;
            rate_slider.MaxValue = 50;
            root_view.Add(rate_slider);

            rate_slider.StateChangedEvent += OnStateChanged;
            rate_slider.SlidingFinishedEvent += OnSlidingFinishedEvent;

            rate_slider.CurrentValue = AccessibilityController.Instance.accessibility_accessible_screen_percentage_get();

            bool disabled = !AccessibilityController.Instance.accessibility_accessible_screen_get();
            if (disabled)
                rate_slider.ControlState = Tizen.NUI.Components.ControlStates.Disabled;

            text_view = new View();
            text_view.Position2D = new Position2D(64, 0);
            text_view.Size2D = new Size2D(1080 - 64 * 2, (164 - 64) / 2);

            percent_label = new TextLabel[5];
            int distance = 1080 - 64 * 2 - 100;
            for (int i = 0; i < 5; i++)
            {
                percent_label[i] = new TextLabel();
                percent_label[i].FontFamily = "SamsungOne 400";
                percent_label[i].Text = "" + 10 * (i + 1) + "%";
                percent_label[i].PointSize = 30;
                percent_label[i].TextColor = new Color(0.0f, 0.0f, 0.0f, 0.75f);
                percent_label[i].Position2D = new Position2D(i * distance / 4, 10);
                text_view.Add(percent_label[i]);
            }

            root_view.Add(text_view);

            divider_view = new View();
            divider_view.Position2D = new Position2D(56, 164);
            divider_view.Size2D = new Size2D(986, 1);
            divider_view.BackgroundColor = new Color(0f, 0f, 0f, 0.1f);
            root_view.Add(divider_view);

            Add(root_view);
        }

        private void OnStateChanged(object sender, Slider.StateChangedArgs args)
        {
            if (sender is Slider)
            {
                Slider slider = sender as Slider;
                if (slider != null)
                {
                    //Tizen.Log.Info("accessibility", "screen on or off: " + slider.CurrentValue);
                }
            }
        }

        private void OnSlidingFinishedEvent(object sender, Slider.SlidingFinishedArgs args)
        {
            if (sender is Slider)
            {
                Slider slider = sender as Slider;

                slider.CurrentValue = (int)(args.CurrentValue / 10 + 0.5) * 10;

                // set
                AccessibilityController.Instance.accessibility_accessible_screen_percentage_set((int)slider.CurrentValue);
            }
        }

        public Slider RateSlider
        {
            get
            {
                return rate_slider;
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
                if (rate_slider != null)
                {
                    root_view.Remove(rate_slider);
                    rate_slider.Dispose();
                    rate_slider = null;
                }
                for (int i = 0; i < 5; i++)
                {
                    text_view.Remove(percent_label[i]);
                    percent_label[i].Dispose();
                    percent_label[i] = null;
                }
                if (text_view != null)
                {
                    root_view.Remove(text_view);
                    text_view.Dispose();
                    text_view = null;
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

    public class ImageItemView : View
    {
        private View root_view;
        private TextLabel main_label;
        private ImageView background_view;
        private ImageView image_view;
        private TextLabel test_label;

        public ImageItemView(string main_text)
        {
            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            main_label = new TextLabel();
            main_label.Text = main_text;
            main_label.MultiLine = true;
            main_label.LineWrapMode = LineWrapMode.Word;
            main_label.FontFamily = "SamsungOne 500";
            main_label.PointSize = 28;
            main_label.Position2D = new Position2D(56, 32);
            main_label.SizeWidth = 1080 - 56 * 2;
            main_label.SizeHeight = main_label.GetHeightForWidth(main_label.SizeWidth);
            main_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            root_view.Add(main_label);

            background_view = new ImageView();
            background_view.BackgroundImage = CommonResource.GetFHResourcePath() + "setting_img_device.png";
            background_view.Position2D.X = 234 + 50; // originally 234
            background_view.Position2D.Y = main_label.Position2D.Y + (int)main_label.SizeHeight + 56; // originally 156.
            root_view.Add(background_view);

            image_view = new ImageView();
            image_view.BackgroundImage = CommonResource.GetFHResourcePath() + "setting_device_sample_img.png";
            image_view.Position2D.X = 272 + 50; // originally 272
            image_view.Position2D.Y = background_view.Position2D.Y + 116;
            root_view.Add(image_view);

            test_label = new TextLabel();
            test_label.Text = "Test screen";
            test_label.FontFamily = "SamsungOne 400";
            test_label.PointSize = 34;
            test_label.Position2D.X = 540 - 125; // originally 250
            test_label.Position2D.Y = background_view.Position2D.Y + 1000 + 12;
            test_label.SizeWidth = 500;
            test_label.SizeHeight = test_label.GetHeightForWidth(test_label.SizeWidth);
            root_view.Add(test_label);

            Add(root_view);
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
                if (background_view != null)
                {
                    root_view.Remove(background_view);
                    background_view.Dispose();
                    background_view = null;
                }
                if (image_view != null)
                {
                    root_view.Remove(image_view);
                    image_view.Dispose();
                    image_view = null;
                }
                if (test_label != null)
                {
                    root_view.Remove(test_label);
                    test_label.Dispose();
                    test_label = null;
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

    public class AccessibleScreenListBridge : FlexibleView.Adapter
    {
        private List<AccessibleScreenListItemData> mDatas;
        private FlexibleView mListView;
        private Dictionary<int, FlexibleView.ViewHolder> mViewHolders;

        public AccessibleScreenListBridge(FlexibleView list_view, List<AccessibleScreenListItemData> datas)
        {
            mListView = list_view;
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

            // update font size and height of accessible screen switch.
            ScreenOnOffItemView listItemView0 = mViewHolders[0].ItemView as ScreenOnOffItemView;
            listItemView0.MainTextLabel.PointSize = 40 * fs_ratio;
            listItemView0.SizeHeight = 164 * height_ratio;
        }

        public override int GetItemViewType(int position)
        {
            return position;  // position is used as item view type.
        }

        public override FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
        {
            View item_view = null;

            AccessibleScreenListItemData listItemData = mDatas[viewType]; // viewType is postion actully.

            switch (viewType)
            {
                case 0:
                    {
                        item_view = new ScreenOnOffItemView(mListView, listItemData.MainText);
                        break;
                    }
                case 1:
                    {
                        item_view = new ScreenSliderItemView();
                        break;
                    }
                case 2:
                    {
                        item_view = new ImageItemView(listItemData.MainText);
                        break;
                    }
                default:
                    break;
            }

            FlexibleView.ViewHolder viewHolder = new FlexibleView.ViewHolder(item_view);

            return viewHolder;
        }

        public override void OnBindViewHolder(FlexibleView.ViewHolder holder, int position)
        {
            switch (position)
            {
                case 0:
                    {
                        float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
                        float height_ratio = fs_ratio;
                        if (height_ratio < 1.0f)
                        {
                            height_ratio = 1.0f;
                        }
                        ScreenOnOffItemView listItemView = holder.ItemView as ScreenOnOffItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164 * height_ratio;
                        break;
                    }
                case 1:
                    {
                        ScreenSliderItemView listItemView = holder.ItemView as ScreenSliderItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164;
                        break;
                    }
                case 2:
                    {
                        ImageItemView listItemView = holder.ItemView as ImageItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 1336;
                        break;
                    }
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
    public class AccessibilityScreen : IViewAdapter
    {
        public void Activate()
        {
            root_view = new View();
            Window.Instance.GetDefaultLayer().Add(root_view);

            header = new Header("DefaultHeader");
            header.BackgroundColor = new Color(1f, 1f, 1f, 0.7f);
            header.Title = "Accessible Screen";
            root_view.Add(header);

            content_list = new FlexibleView();
            content_list.Name = "Sample List";
            content_list.Position2D = new Position2D(0, (int)header.SizeHeight);
            content_list.Size2D = new Size2D(1080, 1920 - (int)header.SizeHeight);
            content_list.Padding = new Extents(0, 0, 0, 0);
            content_list.ItemClickEvent += OnListItemClickEvent;
            content_list.StyleChanged += OnStyleChanged;

            List<AccessibleScreenListItemData> dataList = new List<AccessibleScreenListItemData>();
            dataList.Add(new AccessibleScreenListItemData("Accessible Screen On/Off"));
            dataList.Add(new AccessibleScreenListItemData(""));
            dataList.Add(new AccessibleScreenListItemData("Pull or extend the screen down by tapping the home key in quick succession. " +
                                                          "The accessible screen puts the control area within reach of all users."));

            adapter = new AccessibleScreenListBridge(content_list, dataList);
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
            }
        }

        private View root_view;
        private Header header;
        private FlexibleView content_list;
        private AccessibleScreenListBridge adapter;
    }
}
