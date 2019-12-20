using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Accessibility
{
    public class TtsListItemData
    {
        private string main_text;

        public TtsListItemData(string main_t)
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

    public class SpeechRateItemView : View
    {
        private View root_view;
        private Slider rate_slider;
        private View[] slider_dots;
        private View text_view;
        private TextLabel left_label;
        private TextLabel right_label;
        private int last_speed_rate;
        private AccessibilityTTS tts_player;

        public SpeechRateItemView(AccessibilityTTS tts)
        {
            tts_player = tts;

            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;

            text_view = new View();
            text_view.Position2D = new Position2D(28, 52);
            text_view.Size2D = new Size2D(1080-28-36, 40);

            left_label = new TextLabel();
            left_label.FontFamily = "SamsungOne 400";
            left_label.PointSize = 30;
            left_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            left_label.HorizontalAlignment = HorizontalAlignment.Begin;
            left_label.VerticalAlignment = VerticalAlignment.Center;
            left_label.Text = "Slow";
            text_view.Add(left_label);

            right_label = new TextLabel();
            right_label.FontFamily = "SamsungOne 400";
            right_label.PointSize = 30;
            right_label.TextColor = new Color(0f, 0f, 0f, 0.75f);
            right_label.HorizontalAlignment = HorizontalAlignment.End;
            right_label.VerticalAlignment = VerticalAlignment.Center;
            right_label.Text = "Fast";
            text_view.Add(right_label);

            root_view.Add(text_view);

            rate_slider = new Slider("DefaultSlider");
            rate_slider.Direction = Slider.DirectionType.Horizontal;
            rate_slider.Position2D = new Position2D(62, 99);
            rate_slider.Size2D = new Size2D(956, 105);
            rate_slider.MaxValue = (int)AccessibilityController.TTSRate.TTS_RATE_END - 1;
            rate_slider.MinValue = 0;
            root_view.Add(rate_slider);

            // (TODO) current value of slider is not set.

            rate_slider.StateChangedEvent += OnStateChanged;
            rate_slider.SlidingFinishedEvent += OnSlidingFinishedEvent;

            slider_dots = new ImageView[(int)AccessibilityController.TTSRate.TTS_RATE_END - 2];
            for (int i = 0; i < (int)AccessibilityController.TTSRate.TTS_RATE_END - 2; i++)
            {
                slider_dots[i] = new ImageView();
                slider_dots[i].BackgroundImage = CommonResource.GetFHResourcePath() + "slider_dot.png";
                slider_dots[i].Position2D = new Position2D(62 + (i + 1) * 234 + i * 7, 92);
                root_view.Add(slider_dots[i]);
            }

            Add(root_view);
        }

        private void OnSlidingFinishedEvent(object sender, Slider.SlidingFinishedArgs args)
        {
            if (sender is Slider)
            {
                Slider slider = sender as Slider;

                slider.CurrentValue  = (int)(args.CurrentValue + 0.5);

                // set
                if (last_speed_rate != (int)slider.CurrentValue)
                {
                    AccessibilityController.Instance.accessibility_tts_rate_set((AccessibilityController.TTSRate)slider.CurrentValue);
                    tts_player.TestTtsPlay((int)slider.CurrentValue);
                    last_speed_rate = (int)slider.CurrentValue;
                }

                // dot
                for (int i = 0; i < (int)AccessibilityController.TTSRate.TTS_RATE_END - 2; i++)
                {
                    if (i == slider.CurrentValue - 1)
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

        private void OnStateChanged(object sender, Slider.StateChangedArgs args)
        {
            if (sender is Slider)
            {
                Slider slider = sender as Slider;
                if (slider != null)
                {
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
                if (rate_slider != null)
                {
                    root_view.Remove(rate_slider);
                    rate_slider.Dispose();
                    rate_slider = null;
                }
                for (int i = 0; i < (int)AccessibilityController.TTSRate.TTS_RATE_END - 2; i++)
                {
                    root_view.Remove(slider_dots[i]);
                    slider_dots[i].Dispose();
                    slider_dots[i] = null;
                }
                if (left_label != null)
                {
                    text_view.Remove(left_label);
                    left_label.Dispose();
                    left_label = null;
                }
                if (right_label != null)
                {
                    text_view.Remove(right_label);
                    right_label.Dispose();
                    right_label = null;
                }
                if (text_view != null)
                {
                    root_view.Remove(text_view);
                    text_view.Dispose();
                    text_view = null;
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

    public class LanguageItemView : View
    {
        private View root_view;
        private RadioButton language_radio;
        private TextLabel automatic_label;
        private TextLabel language_label;
        private View divider_view;

        public LanguageItemView()
        {
            float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
            float height_ratio = fs_ratio;
            if (height_ratio < 1.0f)
            {
                height_ratio = 1.0f;
            }

            root_view = new View();
            root_view.WidthResizePolicy = ResizePolicyType.FillToParent;
            root_view.Position2D = new Position2D(56, 0);
            root_view.Size2D = new Size2D(1080 - 56 - 56, (int)(164 * height_ratio));

            language_radio = new RadioButton("RadioButton");
            language_radio.Size2D = new Size2D(48, 48);
            language_radio.PositionUsesPivotPoint = true;
            language_radio.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            language_radio.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            language_radio.IsSelected = true;
            root_view.Add(language_radio);

            automatic_label = new TextLabel();
            automatic_label.Position2D = new Position2D(128 - 56, (int)(18 *height_ratio));
            automatic_label.Size2D = new Size2D(600, 100);
            automatic_label.FontFamily = "SamsungOne 400";
            automatic_label.PointSize = 44 * fs_ratio;
            automatic_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
            root_view.Add(automatic_label);

            language_label = new TextLabel();
            language_label.Position2D = new Position2D(128 - 56, (int)(88 * height_ratio));
            language_label.Size2D = new Size2D(600, 80);
            language_label.FontFamily = "SamsungOne 400";
            language_label.PointSize = 30 * fs_ratio;
            language_label.TextColor = new Color(0.0f, 0.0f, 0.0f, 0.75f);
            root_view.Add(language_label);

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
                return automatic_label;
            }
        }

        public TextLabel SubTextLabel
        {
            get
            {
                return language_label;
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
                if (language_radio != null)
                {
                    root_view.Remove(language_radio);
                    language_radio.Dispose();
                    language_radio = null;
                }
                if (automatic_label != null)
                {
                    root_view.Remove(automatic_label);
                    automatic_label.Dispose();
                    automatic_label = null;
                }
                if (language_label != null)
                {
                    root_view.Remove(language_label);
                    language_label.Dispose();
                    language_label = null;
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

    public class TtsListBridge : FlexibleView.Adapter
    {
        private List<TtsListItemData> mDatas;
        private AccessibilityTTS mTtsPlayer;
        private Dictionary<int, FlexibleView.ViewHolder> mViewHolders;

        public TtsListBridge(AccessibilityTTS view, List<TtsListItemData> datas)
        {
            mTtsPlayer = view;
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

            // update font size of speed rate.
            ListItem listItemView0 = mViewHolders[0].ItemView as ListItem;
            listItemView0.MainTextPointSize = 24 * fs_ratio;

            // update font size of language
            ListItem listItemView2 = mViewHolders[2].ItemView as ListItem;
            listItemView2.MainTextPointSize = 24 * fs_ratio;

            // update font size of language options
            LanguageItemView listItemView3 = mViewHolders[3].ItemView as LanguageItemView;
            listItemView3.MainTextLabel.PointSize = 44 * fs_ratio;
            listItemView3.SubTextLabel.PointSize = 30 * fs_ratio;
            listItemView3.SizeHeight = 164 * height_ratio;
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
                    item_view = new ListItem("GroupIndexListItem");
                    break;
                case 1:
                    item_view = new SpeechRateItemView(mTtsPlayer);
                    break;
                case 2:
                    item_view = new ListItem("GroupIndexListItem");
                    break;
                case 3:
                    item_view = new LanguageItemView();
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
            TtsListItemData listItemData = mDatas[position];
            switch (position)
            {
                case 0:
                case 2:
                    {
                        ListItem listItemView = holder.ItemView as ListItem;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 48;
                        listItemView.MainText = listItemData.MainText;
                        break;
                    }
                case 1:
                    {
                        SpeechRateItemView listItemView = holder.ItemView as SpeechRateItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 204;
                        break;
                    }
                case 3:
                    {
                        float fs_ratio = AccessibilityController.Instance.accessibility_default_font_size_ratio_get();
                        float height_ratio = fs_ratio;
                        if (height_ratio < 1.0f)
                        {
                            height_ratio = 1.0f;
                        }
                        LanguageItemView listItemView = holder.ItemView as LanguageItemView;
                        listItemView.Name = "Item" + position;
                        listItemView.SizeWidth = 1080;
                        listItemView.SizeHeight = 164 * height_ratio;
                        listItemView.MainTextLabel.Text = listItemData.MainText;
                        listItemView.SubTextLabel.Text = "English(US)";
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

    public class AccessibilityTTS : IViewAdapter
    {
        public void Activate()
        {
            root_view = new View();
            Window.Instance.GetDefaultLayer().Add(root_view);

            header = new Header("DefaultHeader");
            header.BackgroundColor = new Color(1f, 1f, 1f, 0.7f);
            header.Title = "Text-to-speech";
            root_view.Add(header);

            content_list = new FlexibleView();
            content_list.Name = "Sample List";
            content_list.Position2D = new Position2D(0, (int)header.SizeHeight);
            content_list.Size2D = new Size2D(1080, 896);
            content_list.Padding = new Extents(0, 0, 0, 0);
            content_list.ItemClickEvent += OnListItemClickEvent;
            content_list.StyleChanged += OnStyleChanged;

            List<TtsListItemData> dataList = new List<TtsListItemData>();
            dataList.Add(new TtsListItemData("Speech rate"));
            dataList.Add(new TtsListItemData(""));
            dataList.Add(new TtsListItemData("Language"));
            dataList.Add(new TtsListItemData("Automatic"));

            adapter = new TtsListBridge(this, dataList);
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

        public void TestTtsPlay(int speed)
        {
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
        private TtsListBridge adapter;
    }
}
