/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd.
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
using Tizen.FH.NUI;
using Tizen.NUI;

namespace Tizen.FH.FamilyBoard
{
    public class CommonResource
    {
        public static string GetResourcePath()
        {
            //return Applications.Application.Current.DirectoryInfo.Resource + "/images/";

            return @"../../../demo/Tizen.FH.FamilyBoard/res/images/";
        }
    }

    public class FamilyBoardApplication : FHNUIApplication
    {
        public delegate string SWIGStringDelegate(string message);

        private static FamilyBoardApplication instance = null;

        private ILifecycleObserver main_view = null;
        private Stack<ILifecycleObserver> view_stack = new Stack<ILifecycleObserver>();

        public static FamilyBoardApplication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FamilyBoardApplication();
                }
                return instance;
            }
        }

        private FamilyBoardApplication()
        {
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            Window.Instance.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Window.Instance.KeyEvent += Instance_KeyEvent;

            main_view = CreateView("Main");
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
        }

        public void ChangeMainBackground()
        {
            FamilyBoardMain fb = main_view as FamilyBoardMain;
            if (fb != null)
            {
                fb.ChangeBackground();
            }
        }

        public ILifecycleObserver CreateView(string view_name)
        {
            ILifecycleObserver view = null;

            if (view_name.Equals("Main"))
            {
                view = new FamilyBoardMain();
            }
            else if (view_name.Equals("Picture Wizard"))
            {
                view = PictureWizard.Instance;
            }
            else if (view_name.Equals("Stickers"))
            {
                view = new StickerChooser();
            }
            else if (view_name.Equals("Text"))
            {
                view = new TextChooser();
            }
            else if (view_name.Equals("Background Image"))
            {
                view = new BackgroundImageChooser();
            }
            else
            {
                return view;
            }

            view.Activate();
            view_stack.Push(view);

            return view;
        }

        public void RemoveView()
        {
            ILifecycleObserver lastView = view_stack.Pop();
            lastView.Deactivate();

            if (view_stack.Count == 0)
            {
                Environment.Exit(0);
            }

            ILifecycleObserver currentView = view_stack.Peek();
            currentView.Reactivate();
        }
		
		private void Instance_KeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Up)
            {
                if (e.Key.KeyPressedName == "Escape" || e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "BackSpace")
                {
                    RemoveView();
                }
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

