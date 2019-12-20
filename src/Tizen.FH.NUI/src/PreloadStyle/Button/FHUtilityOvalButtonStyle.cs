﻿using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    internal class UtilityOvalButtonStyle : IconButtonStyle
    {
        protected override ViewStyle GetAttributes()
        {
            if (Content != null)
            {
                ViewStyle contentStyle = (ViewStyle)global::System.Activator.CreateInstance(Content.GetType());
                contentStyle.CopyFrom(Content as ViewStyle);
                return contentStyle;
            }

            ButtonStyle style = base.GetAttributes() as ButtonStyle;
            style.BackgroundImage = new Selector<string>
            {
                Normal = CommonResource.Instance.GetFHResourcePath() + "3. Button/oval_toggle_btn_normal.png",
                Selected = CommonResource.Instance.GetFHResourcePath() + "3. Button/oval_toggle_btn_select.png",
            };
            style.Shadow.ResourceUrl.All = CommonResource.Instance.GetFHResourcePath() + "3. Button/oval_toggle_btn_shadow.png";
            return style;
        }
    }
}
