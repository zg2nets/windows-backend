using System;
using Tizen.NUI.Xaml;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Tizen.NUI.BaseComponents;
using System.Reflection;
using System.Linq;

namespace Tizen.NUI.Examples
{
    public class BindingMarkUpExtensionDemo : NUIApplication
    {
        private ContentPage myPage;
        private Window window;

        protected override void OnCreate() 
        {
            base.OnCreate();

            window = Window.Instance;
            window.BackgroundColor = Color.White;

            myPage = new BindingMarkUpExtensionDemoPage(window);
            Extensions.LoadFromXaml(myPage, typeof(BindingMarkUpExtensionDemoPage));


            myPage.SetFocus();


        }

    }
}
