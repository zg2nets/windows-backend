using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class VideoItem : Item
    {
        public VideoItem()
        {
            this.Type = "Video";
            myView = new VideoView();
            myView.BackgroundColor = Tizen.NUI.Color.White;
            myView.Opacity = 0.0f;

            myView.HeightResizePolicy = Tizen.NUI.ResizePolicyType.FillToParent;
            myView.WidthResizePolicy = Tizen.NUI.ResizePolicyType.FillToParent;


            this.HeightResizePolicy = Tizen.NUI.ResizePolicyType.FitToChildren;
            this.WidthResizePolicy = Tizen.NUI.ResizePolicyType.FitToChildren;
            this.Size2D = new Tizen.NUI.Size2D(480, 272);
            this.Add(myView);
        }

        public void Play()
        {
            (myView as VideoView).Muted = true;
            (myView as VideoView).Underlay = false;
            (myView as VideoView).Looping = true;
            (myView as VideoView).Play();
        }
        public void Stop()
        {
            (myView as VideoView).Stop();
        }

        public override string ResourceUrl
        {
            get
            {
                return (myView as VideoView).ResourceUrl;
            }
            set
            {
                (myView as VideoView).ResourceUrl = value;
            }
        }

    }
}
