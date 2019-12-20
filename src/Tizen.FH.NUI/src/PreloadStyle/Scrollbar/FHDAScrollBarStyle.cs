using Tizen.NUI.Components.DA;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Components
{
    internal class DAScrollBarStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            ScrollBarStyle style = new ScrollBarStyle
            {
                Track = new ImageViewStyle
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = new Color(0.43f, 0.43f, 0.43f, 0.6f),
                    }
                },
                Thumb = new ImageViewStyle
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = new Color(0.0f, 0.0f, 0.0f, 0.2f)
                    }
                },
            };
            return style;
        }
    }
}
