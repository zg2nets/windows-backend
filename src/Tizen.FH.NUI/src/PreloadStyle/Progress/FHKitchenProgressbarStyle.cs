using Tizen.NUI.Components.DA;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Components
{
    internal class KitchenProgressbarStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            ProgressStyle style = new ProgressStyle
            {
                Track = new ImageViewStyle
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = new Color(0.0f, 0.0f, 0.0f, 0.1f),
                    }
                },
                Progress = new ImageViewStyle
                {
                    BackgroundColor = new ColorSelector
                    {
                        All = Utility.Hex2Color(Constants.AppColorKitchen, 1)
                    }
                },
            };
            return style;
        }
    }
}
