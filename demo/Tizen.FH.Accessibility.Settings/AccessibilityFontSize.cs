using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Accessibility
{
    public class FontSizeListItemData
    {
        private string main_text;

        public FontSizeListItemData(string main_t)
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

    public class FontSizeLabelItemView : View
    {
        private TextLabel main_label;

        public FontSizeLabelItemView()
        {
            main_label = new TextLabel();
            main_label.WidthResizePolicy = ResizePolicyType.FillToParent;
            main_label.HeightResizePolicy = ResizePolicyType.FillToParent;
            main_label.MultiLine = true;
            main_label.HorizontalAlignment = HorizontalAlignment.Center;
            main_label.VerticalAlignment = VerticalAlignment.Center;
            Add(main_label);
        }

        public TextLabel MainLabel
        {
            get
            {
                return main_label;
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
                    Remove(main_label);
                    main_label.Dispose();
                    main_label = null;
                }
            }

            base.Dispose(type);
        }
    }

    public class FontSizeSliderItemView : View
    {
        private View root_view;
        private View top_text_view;
        private TextLabel left_top_label;
        private TextLabel right_top_label;
        private View bottom_text_view;
        private TextLabel left_bottom_label;
        private TextLabel right_bottom_label;
        private View[] slider_dots;
        private Slider font_size_slider;
        private View divider_view;
        private int last_font_size;

        public FontSizeSliderItemView()
        {
            // root view
            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            // top text view
            top_text_view = new View();
            top_text_view.Position2D = new Position2D(28, 44); // originally it is 64 in GUI.
            root_view.Add(top_text_view);

            left_top_label = new TextLabel();
            left_top_label.HeightResizePolicy = ResizePolicyType.FillToParent;
            left_top_label.FontFamily = "SamsungOne 400";
            left_top_label.PointSize = 30;
            left_top_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            left_top_label.HorizontalAlignment = HorizontalAlignment.Begin;
            left_top_label.VerticalAlignment = VerticalAlignment.Center;
            left_top_label.Text = "Extra";
            left_top_label.SizeWidth = (1080 - 28 - 28) / 2;
            left_top_label.SizeHeight = left_top_label.NaturalSize2D.Height;
            top_text_view.Add(left_top_label);

            right_top_label = new TextLabel();
            right_top_label.HeightResizePolicy = ResizePolicyType.FillToParent;
            right_top_label.FontFamily = "SamsungOne 400";
            right_top_label.PointSize = 30;
            right_top_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            right_top_label.HorizontalAlignment = HorizontalAlignment.End;
            right_top_label.VerticalAlignment = VerticalAlignment.Center;
            right_top_label.Text = "Extra";
            right_top_label.PositionX = left_top_label.SizeWidth;
            right_top_label.SizeWidth = (1080 - 28 - 28) / 2;
            right_top_label.SizeHeight = right_top_label.NaturalSize2D.Height;
            top_text_view.Add(right_top_label);

            // bottom text view
            bottom_text_view = new View();
            bottom_text_view.Position2D = new Position2D(28, 44 + (int)left_top_label.SizeHeight - 8);
            root_view.Add(bottom_text_view);

            left_bottom_label = new TextLabel();
            left_bottom_label.HeightResizePolicy = ResizePolicyType.FillToParent;
            left_bottom_label.FontFamily = "SamsungOne 400";
            left_bottom_label.PointSize = 30;
            left_bottom_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            left_bottom_label.HorizontalAlignment = HorizontalAlignment.Begin;
            left_bottom_label.VerticalAlignment = VerticalAlignment.Center;
            left_bottom_label.Text = "small";
            left_bottom_label.SizeWidth = (1080 - 28 - 28) / 2;
            left_bottom_label.SizeHeight = left_bottom_label.NaturalSize2D.Height;
            bottom_text_view.Add(left_bottom_label);

            right_bottom_label = new TextLabel();
            right_bottom_label.HeightResizePolicy = ResizePolicyType.FillToParent;
            right_bottom_label.FontFamily = "SamsungOne 400";
            right_bottom_label.PointSize = 30;
            right_bottom_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            right_bottom_label.HorizontalAlignment = HorizontalAlignment.End;
            right_bottom_label.VerticalAlignment = VerticalAlignment.Center;
            right_bottom_label.Text = "small";
            right_bottom_label.PositionX = left_bottom_label.SizeWidth;
            right_bottom_label.SizeWidth = (1080 - 28 - 28) / 2;
            right_bottom_label.SizeHeight = right_bottom_label.NaturalSize2D.Height;
            bottom_text_view.Add(right_bottom_label);

            // slider
            font_size_slider = new Slider("DefaultSlider");
            font_size_slider.Direction = Slider.DirectionType.Horizontal;
            font_size_slider.Position2D = new Position2D(62, 137);
            font_size_slider.Size2D = new Size2D(968, 60);
            font_size_slider.MaxValue = (int)AccessibilityController.FontSize.FONT_SIZE_END - 1;
            font_size_slider.MinValue = 0;
            root_view.Add(font_size_slider);

            font_size_slider.CurrentValue = (int)AccessibilityController.Instance.accessibility_font_size_get();
            last_font_size = (int)font_size_slider.CurrentValue;

            slider_dots = new ImageView[(int)AccessibilityController.FontSize.FONT_SIZE_END - 2];
            for (int i = 0; i < (int)AccessibilityController.FontSize.FONT_SIZE_END - 2; i++)
            {
                slider_dots[i] = new ImageView();
                if (i == (int)font_size_slider.CurrentValue - 1)
                {
                    slider_dots[i].BackgroundImage = CommonResource.GetFHResourcePath() + "slider_dot_select.png";
                }
                else
                {
                    slider_dots[i].BackgroundImage = CommonResource.GetFHResourcePath() + "slider_dot.png";
                }
                slider_dots[i].Position2D = new Position2D(62 + (i + 1) * 156 + i * 7, 130);
                root_view.Add(slider_dots[i]);
            }

            font_size_slider.SlidingFinishedEvent += OnSlidingFinishedEvent;

            // divider
            divider_view = new View();
            divider_view.Position2D = new Position2D(56, 164);
            divider_view.Size2D = new Size2D(986, 1);
            divider_view.BackgroundColor = new Color(0f, 0f, 0f, 0.1f);
            root_view.Add(divider_view);

            Add(root_view);
        }

        public Slider FontSizeSlider
        {
            get
            {
                return font_size_slider;
            }
        }

        private void OnSlidingFinishedEvent(object sender, Slider.SlidingFinishedArgs args)
        {
            if (sender is Slider)
            {
                Slider slider = sender as Slider;

                slider.CurrentValue = (int)(args.CurrentValue + 0.5);

                // set
                if (last_font_size != (int)slider.CurrentValue)
                {
                    AccessibilityController.Instance.accessibility_font_size_set((AccessibilityController.FontSize)slider.CurrentValue);
                    last_font_size = (int)slider.CurrentValue;
                }

                // dot
                for (int i = 0; i < (int)AccessibilityController.FontSize.FONT_SIZE_END - 2; i++)
                {
                    if (i == (int)slider.CurrentValue - 1)
                    {
                        slider_dots[i].BackgroundImage = CommonResource.GetFHResourcePath() + "slider_dot_select.png";
                    }
                    else
                    {
                        slider_dots[i].BackgroundImage = CommonResource.GetFHResourcePath() + "slider_dot.png";
                    }
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
                if (font_size_slider != null)
                {
                    root_view.Remove(font_size_slider);
                    font_size_slider.Dispose();
                    font_size_slider = null;
                }
                for (int i = 0; i < (int)AccessibilityController.FontSize.FONT_SIZE_END - 2; i++)
                {
                    root_view.Remove(slider_dots[i]);
                    slider_dots[i].Dispose();
                    slider_dots[i] = null;
                }
                if (left_top_label != null)
                {
                    top_text_view.Remove(left_top_label);
                    left_top_label.Dispose();
                    left_top_label = null;
                }
                if (right_top_label != null)
                {
                    top_text_view.Remove(right_top_label);
                    right_top_label.Dispose();
                    right_top_label = null;
                }
                if (top_text_view != null)
                {
                    root_view.Remove(top_text_view);
                    top_text_view.Dispose();
                    top_text_view = null;
                }
                if (left_bottom_label != null)
                {
                    bottom_text_view.Remove(left_bottom_label);
                    left_bottom_label.Dispose();
                    left_bottom_label = null;
                }
                if (right_bottom_label != null)
                {
                    bottom_text_view.Remove(right_bottom_label);
                    right_bottom_label.Dispose();
                    right_bottom_label = null;
                }
                if (bottom_text_view != null)
                {
                    root_view.Remove(bottom_text_view);
                    bottom_text_view.Dispose();
                    bottom_text_view = null;
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

    public class FontSizeListBridge : FlexibleView.Adapter
    {
        private List<FontSizeListItemData> mDatas;
        private Dictionary<int, FlexibleView.ViewHolder> mViewHolders;

        public FontSizeListBridge(List<FontSizeListItemData> datas)
        {
            mDatas = datas;
            mViewHolders = new Dictionary<int, FlexibleView.ViewHolder>();
        }

        public void UpdateItemViews()
        {
            FontSizeLabelItemView listItemView0 = mViewHolders[0].ItemView as FontSizeLabelItemView;
            listItemView0.MainLabel.PointSize = 48 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();

            ListItem listItemView1 = mViewHolders[1].ItemView as ListItem;
            listItemView1.MainTextPointSize = 24 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();

            // update slider value.
            FontSizeSliderItemView listItemView2 = mViewHolders[2].ItemView as FontSizeSliderItemView;
            listItemView2.FontSizeSlider.CurrentValue = (int)AccessibilityController.Instance.accessibility_font_size_get();
        }

        public override int GetItemViewType(int position)
        {
            return position;  // position is used as item view type.
        }

        public override FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
        {
            View item_view = null;

            switch (viewType)
            {
                case 0:
                    item_view = new FontSizeLabelItemView();
                    break;
                case 1:
                    item_view = new ListItem("GroupIndexListItem");
                    break;
                case 2:
                    item_view = new FontSizeSliderItemView();
                    break;
                default:
                    break;
            }

            FlexibleView.ViewHolder viewHolder = new FlexibleView.ViewHolder(item_view);
            mViewHolders[viewType] = viewHolder;

            return viewHolder;
        }

        public override void OnBindViewHolder(FlexibleView.ViewHolder holder, int position)
        {
            FontSizeListItemData listItemData = mDatas[position];
            switch (position)
            {
                case 0:
                    {
                        FontSizeLabelItemView listItemView = holder.ItemView as FontSizeLabelItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 648;
                        listItemView.MainLabel.Text = listItemData.MainText;
                        listItemView.MainLabel.PointSize = 40 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
                        break;
                    }
                case 1:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 48;
                        listItemView.MainText = listItemData.MainText;
                        listItemView.MainTextPointSize = 24 * AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
                        break;
                    }
                case 2:
                    {
                        FontSizeSliderItemView listItemView = holder.ItemView as FontSizeSliderItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 256;
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
    public class AccessibilityFontSize : IViewAdapter
    {
        public void Activate()
        {
            root_view = new View();
            Window.Instance.GetDefaultLayer().Add(root_view);

            header = new Header("DefaultHeader");
            header.BackgroundColor = new Color(1f, 1f, 1f, 0.7f);
            header.Title = "Font Size";
            root_view.Add(header);

            content_list = new FlexibleView();
            content_list.Name = "Sample List";
            content_list.Position2D = new Position2D(0, (int)header.SizeHeight);
            content_list.Size2D = new Size2D(1080, 1920 - (int)header.SizeHeight);
            content_list.Padding = new Extents(0, 0, 0, 0);
            content_list.ItemClickEvent += OnListItemClickEvent;
            content_list.StyleChanged += OnStyleChanged;

            List<FontSizeListItemData> dataList = new List<FontSizeListItemData>();
            dataList.Add(new FontSizeListItemData("ABCDE\nabcde\n12345!@#$"));
            dataList.Add(new FontSizeListItemData("Font Size"));
            dataList.Add(new FontSizeListItemData(""));

            adapter = new FontSizeListBridge(dataList);
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
        private FontSizeListBridge adapter;
    }
}
