/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System;
using System.Collections.Generic;
using Tizen.FH.NUI.Components;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Accessibility
{
    public class CommonResource
    {
        public static string GetFHResourcePath()
        {
            //return Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/images/accessibility/";

            return @"../../../demo/Tizen.FH.Accessibility.Settings/res/images/accessibility/";
        }
    }

    public class AccessibilityApplication : FHNUIApplication
    {
        public delegate string SWIGStringDelegate(string message);

        private static AccessibilityApplication instance = null;

        private IViewAdapter main_view = null;
        private NaviFrame navi_frame;
        private Navigation back_navigation;
        private Stack<IViewAdapter> view_stack = new Stack<IViewAdapter>();

        public static AccessibilityApplication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccessibilityApplication();
                }
                return instance;
            }
        }

        protected override void OnCreate()
        {
            base.OnCreate();

            Window.Instance.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            navi_frame = new NaviFrame();

            main_view = CreateView("Main");

            back_navigation = new Navigation("Back");
            back_navigation.Position = new Position(0, Window.Instance.Size.Height / 2);
            back_navigation.ItemTouchEvent += OnBackNaviTouchEvent;

            Navigation.NavigationDataItem backItem = new Navigation.NavigationDataItem("WhiteBackItem");
            back_navigation.AddItem(backItem);

            Window.Instance.GetDefaultLayer().Add(back_navigation);
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
        }

        public IViewAdapter CreateView(string view_name)
        {
            IViewAdapter view = null;

            if (view_name.Equals("Main"))
            {
                view = new AccessibilityMain();
            }
            else if (view_name.Equals("Screen Reader"))
            {
                view = new AccessibilityScreenReader();
            }
            else if (view_name.Equals("Text-to-speech Option"))
            {
                view = new AccessibilityTTS();
            }
            else if (view_name.Equals("Font Size"))
            {
                view = new AccessibilityFontSize();
            }
            else if (view_name.Equals("Accessible Screen"))
            {
                view = new AccessibilityScreen();
            }
            else
            {
                return view;
            }

            view.Activate();
            view_stack.Push(view);
            navi_frame.NaviFrameItemPush(view.GetHeader(), view.GetContent());

            return view;
        }

        public void RemoveView()
        {
            navi_frame.NaviFrameItemPop();
            IViewAdapter lastSample = view_stack.Pop();
            lastSample.Deactivate();
            //FullGC();

            if (view_stack.Count == 0)
            {
                if (navi_frame != null)
                {
                    navi_frame.Dispose();
                    navi_frame = null;
                }

                if (back_navigation != null)
                {
                    Window.Instance.GetDefaultLayer().Remove(back_navigation);
                    back_navigation.Dispose();
                    back_navigation = null;
                }

                Environment.Exit(0);
            }
        }

        private void FullGC()
        {
            global::System.GC.Collect();
            global::System.GC.WaitForPendingFinalizers();
            global::System.GC.Collect();
        }

        private void OnBackNaviTouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                RemoveView();
            }
        }

        [global::System.Runtime.InteropServices.DllImport("libcapi-appfw-app-common.so.0", EntryPoint = "RegisterCreateStringCallback")]
        public static extern void RegisterCreateStringCallback(SWIGStringDelegate stringDelegate);

        [STAThread]
        static void Main(string[] args)
        {
            RegisterCreateStringCallback(new SWIGStringDelegate((string message) =>
            {
                return message;
            }));

            Instance.Run(args);
        }
    }
}

