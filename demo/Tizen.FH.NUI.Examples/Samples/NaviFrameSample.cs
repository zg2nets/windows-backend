﻿using System;
using System.Collections.Generic;
using Tizen.FH.NUI.Controls;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.CommonUI;

namespace Tizen.FH.NUI.Samples
{
    public class NaviFrameSample : IExample
    {
        private SampleLayout root;
        private Button NextButton;
        private Button BackButton;
        private NaviFrame  navi;
        private Header h;
        private TextLabel c;
        private int i;
        public void Activate()
        {

            i = 1;
            root = new SampleLayout(false);
            root.HeaderText = "NaviFrame";

            navi = new NaviFrame("DefaultNaviFrame");
            root.Add(navi);

            BackButton = new Button()
            {
                Size2D = new Size2D(90, 60),
                BackgroundColor = Color.Cyan,
                Text = "Push",
            };
            BackButton.PositionUsesPivotPoint = true;
            BackButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
            BackButton.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
            BackButton.ClickEvent += ClickPush;

            root.Add(BackButton);
            BackButton.RaiseToTop();
            NextButton = new Button()
            {
                Text = "Pop",
                Size2D = new Size2D(90, 60),
                BackgroundColor = Color.Cyan,
            };
            NextButton.PositionUsesPivotPoint = true;
            NextButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
            NextButton.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
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
                    root.Remove(navi);
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
        private Header MakeHeader(string txt)
        {
            Header head = new Header("DefaultHeader");
            head.BackgroundColor = new Color(255, 255, 255, 0.7f);
            head.HeaderText = "Title " + txt;
            return head;
        }
        private TextLabel MakeLabel(string txt)
        {
            TextLabel content = new TextLabel()
            {
                Text = txt,
                PointSize = 90,
                BackgroundColor = new Color(255, 255, 255, 0.7f),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
            };

            return content;
        }

        private void ClickPush(object sender, Button.ClickEventArgs e)
        {
            string head = "header" + i;
            string lable = "lable" + i;
            h = MakeHeader(head);
            c = MakeLabel(lable);
            i++;
            if (navi!=null)
            {
                navi.NaviFrameItemPush(h, c);
            }
           
        }
        private void ClickPop(object sender, Button.ClickEventArgs e)
        {
            if (navi != null)
            {
                navi.NaviFrameItemPop();
            }
        }
    }
}
