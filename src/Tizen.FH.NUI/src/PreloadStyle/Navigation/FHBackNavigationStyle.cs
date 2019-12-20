using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class BackNavigationStyle : StyleBase
    {
        protected override ViewStyle GetAttributes()
        {
            NavigationStyle style = new NavigationStyle
            {
                IsFitWithItems = true,
            };

            return style;
        }
    }
}
