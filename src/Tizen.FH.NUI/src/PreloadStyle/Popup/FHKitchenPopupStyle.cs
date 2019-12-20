﻿using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class KitchenPopupStyle : BasePopupStyle
    {
        protected override ViewStyle GetAttributes()
        {
            if (Content != null)
            {
                ViewStyle contentStyle = (ViewStyle)global::System.Activator.CreateInstance(Content.GetType());
                contentStyle.CopyFrom(Content as ViewStyle);
                return contentStyle;
            }
            PopupStyle style = base.GetAttributes() as PopupStyle;
            style.Buttons.Text.TextColor.All = Utility.Hex2Color(Constants.AppColorKitchen, 1);
            return style;
        }
    }
}
