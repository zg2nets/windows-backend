
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Examples
{
    public class NaviFrame : IExample
    {
        private SampleLayout root;
        private Tizen.NUI.Components.DA.Button NextButton;
        private Tizen.NUI.Components.DA.Button BackButton;
        private Components.NaviFrame navi;
        private Components.Header h;
        private TextLabel c;
        private int i;

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            i = 1;
            root = new SampleLayout(false);
            root.HeaderText = "NaviFrame";

            navi = new Components.NaviFrame();
            navi.SetNotifier(new NaviItemLifecycle(root));

            BackButton = new Tizen.NUI.Components.DA.Button()
            {
                Size2D = new Size2D(90, 60),
                BackgroundColor = Color.Cyan,
                Text = "Push",
                PointSize = 14,
            };
            BackButton.PositionUsesPivotPoint = true;
            BackButton.ParentOrigin = ParentOrigin.CenterLeft;
            BackButton.PivotPoint = PivotPoint.CenterLeft;
            BackButton.ClickEvent += ClickPush;

            root.Add(BackButton);
            BackButton.RaiseToTop();
            NextButton = new Tizen.NUI.Components.DA.Button()
            {
                Text = "Pop",
                Size2D = new Size2D(90, 60),
                BackgroundColor = Color.Cyan,
                PointSize = 14,
            };
            NextButton.PositionUsesPivotPoint = true;
            NextButton.ParentOrigin = ParentOrigin.CenterRight;
            NextButton.PivotPoint = PivotPoint.CenterRight;
            NextButton.ClickEvent += ClickPop;

            root.Add(NextButton);
            NextButton.RaiseToTop();

            ClickPush(null, null);
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (navi != null)
                {
                    navi.Dispose();
                }
                if (BackButton != null)
                {
                    root.Remove(BackButton);
                    BackButton.Dispose();
                }
                if (NextButton != null)
                {
                    root.Remove(NextButton);
                    NextButton.Dispose();
                }

                root.Dispose();
            }
        }

        private class NaviItemLifecycle : Components.NaviFrame.Notifier
        {
            private View root;

            public NaviItemLifecycle(View v)
            {
                root = v;
            }

            public override void OnNaviItemCreated(Components.NaviFrame.NaviItem item)
            {
                // nothing
            }

            public override void OnNaviItemDestroyed(Components.NaviFrame.NaviItem item)
            {
                if (item.Header != null)
                {
                    root.Remove(item.Header);
                    item.Header.Dispose();
                }

                if (item.Content != null)
                {
                    root.Remove(item.Content);
                    item.Content.Dispose();
                }
            }
        }

        private Components.Header MakeHeader(string txt)
        {
            Components.Header head = new Components.Header("DefaultHeader");
            head.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.7f);
            head.Title = "Title " + txt;
            head.Position = new Position(0, 0);
            head.Size = new Size(1080, 128);
            head.PositionUsesPivotPoint = true;
            head.ParentOrigin = ParentOrigin.TopCenter;
            head.PivotPoint = PivotPoint.TopCenter;
            root.Add(head);
            return head;
        }

        private TextLabel MakeLabel(string txt)
        {
            TextLabel content = new TextLabel()
            {
                Text = txt,
                PointSize = 90,
                BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.7f),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
            };
            content.Position = new Position(0, 128);
            content.Size = new Size(1080, 128);
            content.HeightResizePolicy = ResizePolicyType.FillToParent;
            content.WidthResizePolicy = ResizePolicyType.FillToParent;
            root.Add(content);
            return content;
        }

        private void ClickPush(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            string head = "header" + i;
            string lable = "lable" + i;
            h = MakeHeader(head);
            c = MakeLabel(lable);
            i++;
            if (navi != null)
            {
                navi.NaviFrameItemPush(h, c);
            }
        }

        private void ClickPop(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            if (navi != null)
            {
                navi.NaviFrameItemPop();
            }
        }
    }
}
