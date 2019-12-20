/*
 * Copyright(c) 2019 Samsung Electronics Co., Ltd.
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
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// The Header  is a component that contain a lable and a 1 pixel line  under it
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.    
    public class Header : Control
    {
        private TextLabel title;
        private View bottomLine;

        /// <summary>
        /// Initializes a new instance of the Header class.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.       
        public Header() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Header class.
        /// </summary>
        /// <param name="style">Create Header by special style defined in UX.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Header(string style) : base(style)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Header class.
        /// </summary>
        /// <param name="style">Create Header by attributes customized by user.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Header(HeaderStyle style) : base(style)
        {
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new HeaderStyle Style => ViewStyle as HeaderStyle;

        /// <summary>
        ///The text showed in the header
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public string Title
        {
            get
            {
                return Style?.Title?.Text?.All;
            }
            set
            {
                Style.Title.Text = new Selector<string>() { All = value };
            }
        }

        /// <summary>
        ///The color of text showed in the header
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Color TitleColor
        {
            get
            {
                return Style?.Title?.TextColor?.All;
            }
            set
            {
                Style.Title.TextColor = new Selector<Color> { All = value };
            }
        }

        /// <summary>
        ///The color of one pixel line under the header
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Color BottomLineColor
        {
            get
            {
                return Style?.BottomLine?.BackgroundColor?.All;
            }
            set
            {
                Style.BottomLine.BackgroundColor = new Selector<Color> { All = value };
            }
        }

        /// <summary>
        /// Get Header attribues.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override ViewStyle GetViewStyle()
        {
            return new HeaderStyle();
        }

        /// <summary>
        /// Dispose Header and all children on it.
        /// </summary>
        /// <param name="type">Dispose type.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                Utility.Dispose(title);
                Utility.Dispose(bottomLine);
            }
            base.Dispose(type);
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public override void ApplyStyle(ViewStyle viewStyle)
        {
            base.ApplyStyle(viewStyle);

            HeaderStyle style = viewStyle as HeaderStyle;
            if (style != null)
            {
                if (title == null)
                {
                    title = new TextLabel
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Add(title);
                }

                if (bottomLine == null)
                {
                    bottomLine = new View
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        Size = new Size(1080, 1),
                        PositionUsesPivotPoint = true,
                        ParentOrigin = Tizen.NUI.ParentOrigin.BottomLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft
                    };
                    Add(bottomLine);
                }

                title.ApplyStyle(Style.Title);
                bottomLine.ApplyStyle(Style.BottomLine);
            }
        }
    }
}
