using System;
using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using static Tizen.NUI.BaseComponents.View;

namespace NUIPhotoSlide
{
    public class CommonResource
    {
        public static string GetResourcePath()
        {
            //return Applications.Application.Current.DirectoryInfo.Resource + "/images/";

            return @"../../../demo/Tizen.FH.NUI.PhotoSlide/res/";
        }
    }

    class Program : NUIApplication
    {
        public delegate string SWIGStringDelegate(string message);

        private BGEffectView bgEffectView;
        private GroupLayerView groupLayerView;
        private OptionPageView optPageView;

        private ImageView settingIcon;
        private PreViewManager previewManager;

        private ImageView topShadow;
        private ImageView bottomShadow;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            //Window.Instance.BackgroundColor = Color.White;
            groupLayerView = new GroupLayerView();
            bgEffectView = new BGEffectView(Color.Black);
            optPageView = new OptionPageView(groupLayerView);

            settingIcon = new ImageView();
            settingIcon.ResourceUrl = CommonResource.GetResourcePath() + "/images/" + "screensaver_ic_slideshow_settings.png";
            settingIcon.ParentOrigin = ParentOrigin.BottomLeft;
            settingIcon.PivotPoint = PivotPoint.BottomLeft;
            settingIcon.PositionUsesPivotPoint = true;
            settingIcon.Size2D = new Size2D(70, 70);
            settingIcon.Position2D = new Position2D(50, -100);
            settingIcon.TouchEvent += SettingIcon_TouchEvent;

            topShadow = new ImageView();
            topShadow.ResourceUrl = CommonResource.GetResourcePath() + "/shadow/" + "shadow_top.png";
            topShadow.ParentOrigin = ParentOrigin.TopLeft;
            topShadow.PivotPoint = PivotPoint.TopLeft;
            
            bottomShadow = new ImageView();
            bottomShadow.ResourceUrl = CommonResource.GetResourcePath() + "/shadow/" + "shadow_bottom.png";
            bottomShadow.Position2D = new Position2D(0, 1920 - bottomShadow.NaturalSize2D.Height);
            bottomShadow.ParentOrigin = ParentOrigin.TopLeft;
            bottomShadow.PivotPoint = PivotPoint.TopLeft;


            previewManager = new PreViewManager(Window.Instance, groupLayerView);
            
            Window.Instance.Add(groupLayerView);

            Window.Instance.Add(bgEffectView);
            Window.Instance.Add(topShadow);
            Window.Instance.Add(bottomShadow);

            Window.Instance.Add(optPageView);
            Window.Instance.Add(settingIcon);


            bgEffectView.Play();
        }

        private bool SettingIcon_TouchEvent(object source, TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                if (optPageView.Opacity == 1.0f)
                {
                    //groupLayerView.ShowTextView();
                    optPageView.PlayHideAnimation();
                }
                else
                {
                    //groupLayerView.HideTextView();
                    optPageView.PlayShowAnimation();
                }
            }
            return false;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        [global::System.Runtime.InteropServices.DllImport("libcapi-appfw-app-common.so.0", EntryPoint = "RegisterCreateStringCallback")]
        public static extern void RegisterCreateStringCallback(SWIGStringDelegate stringDelegate);

        static void Main(string[] args)
        {
            RegisterCreateStringCallback(new SWIGStringDelegate((string message) =>
            {
                return message;
            }));

            var app = new Program();
            app.Run(args);
        }
    }
}
