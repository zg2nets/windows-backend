using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class BGEffectView : View
    {
        private Animation firstAnimation;
        public BGEffectView(Color color)
        {
            BackgroundColor = Color.Black;
            WidthResizePolicy = ResizePolicyType.FillToParent;
            HeightResizePolicy = ResizePolicyType.FillToParent;

            firstAnimation = new Animation(1100);
            firstAnimation.DefaultAlphaFunction = new AlphaFunction(new Vector2(0.175f, 0.885f), new Vector2(0.32f, 1.275f));
            firstAnimation.AnimateTo(this, "ColorAlpha", 0.0f);
        }

        public void Play()
        {
            firstAnimation.Play();
        }

    }
}
