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
    /// ListItem is a component which is the item of list
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.    
    public class ListItem : Control
    {
        private View textRootView = null;
        private TextLabel mainTextLabel = null;
        private TextLabel[] subTextLabelArray = null;
        private ImageView dividerView = null;
        private View startItemRootView = null;
        private View endItemRootView = null;
        private ImageView startIcon = null;
        private ImageView endIcon = null;
        private TextLabel endText = null;

        private uint subTextCount = 0;
        private uint? startSpace = null;
        private uint? endSpace = null;
        private uint? spaceBetweenStartItemAndText = null;
        private uint? spaceBetweenEndItemAndText = null;
        private Size startItemRootSize = null;
        private Size endItemRootSize = null;

        private bool isSelected = false;
        private bool isEnabled = true;
        private bool isPressed = false;
        private ItemAlignTypes itemAlignType = ItemAlignTypes.Icon;
        private GroupIndexTypes groupIndexType = GroupIndexTypes.None;
        private StyleTypes styleType = StyleTypes.None;
        private CheckBox checkBoxObj = null;
        private Switch switchObj = null;

        /// <summary>
        /// Initializes a new instance of the ListItem class.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public ListItem() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the ListItem class.
        /// </summary>
        /// <param name="style">Create Header by special style defined in UX.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public ListItem(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Type for item align style
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public enum ItemAlignTypes
        {
            /// <summary>
            /// Icon type for item align style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            Icon,
            /// <summary>
            /// Check Icon type for item align style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            CheckIcon
        }

        /// <summary>
        /// Type for group index style
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public enum GroupIndexTypes
        {
            /// <summary>
            /// Have no group index style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            None,
            /// <summary>
            /// Drop down type for group index style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            DropDown,
            /// <summary>
            /// Next type for group index style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            Next,
            /// <summary>
            /// Switch type for group index style
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            Switch
        }

        /// <summary>
        /// Type for style
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public enum StyleTypes
        {
            /// <summary>
            /// have no  style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            None,
            /// <summary>
            /// default style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            Default,
            /// <summary>
            /// mutil sub text style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            MultiSubText,
            /// <summary>
            /// effect style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            Effect,
            /// <summary>
            /// item align style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            ItemAlign,
            /// <summary>
            /// next depth style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            NextDepth,
            /// <summary>
            /// group index style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            GroupIndex,
            /// <summary>
            /// drop down style type
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            DropDown
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new ListItemStyle Style => ViewStyle as ListItemStyle;

        /// <summary>
        /// Property for type in item align style, only useful in ItemAlignListItem style
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public ItemAlignTypes ItemAlignType
        {
            set
            {
                if (value == itemAlignType)
                {
                    return;
                }
                itemAlignType = value;
                if (styleType == StyleTypes.ItemAlign)
                {
                    RelayoutComponents();
                }
            }
        }

        /// <summary>
        /// Property for type in group index style, only useful in GroupIndexListItem style
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public GroupIndexTypes GroupIndexType
        {
            set
            {
                if (value == groupIndexType)
                {
                    return;
                }
                groupIndexType = value;
            }
        }

        /// <summary>
        /// Property for selected state
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public bool StateSelectedEnabled
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected == value)
                {
                    return;
                }
                isSelected = value;
                UpdateState();
            }
        }

        /// <summary>
        /// Property for enabled state
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public bool StateEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled == value)
                {
                    return;
                }
                isEnabled = value;
                UpdateState();
            }
        }

        /// <summary>
        /// Property for the content of the main textlabel
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public string MainText
        {
            get
            {
                if (mainTextLabel != null)
                {
                    return mainTextLabel?.Text;
                }
                return null;
            }
            set
            {
                if (mainTextLabel != null)
                {
                    mainTextLabel.Text = value;
                    RelayoutTextComponents();
                }
            }
        }

        /// <summary>
        /// Property for the content of the main textlabel
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public float MainTextPointSize
        {
            get
            {
                if (styleType == StyleTypes.GroupIndex && groupIndexType == GroupIndexTypes.None)
                {
                    return Style?.MainText2?.PointSize?.All ?? 0;
                }
                else
                {
                    return Style?.MainText?.PointSize?.All ?? 0;
                }
            }
            set
            {
                if (styleType == StyleTypes.GroupIndex && groupIndexType == GroupIndexTypes.None)
                {
                    Style.MainText2.PointSize = new Selector<float?>() { All = value };
                }
                else
                {
                    Style.MainText.PointSize = new Selector<float?>() { All = value };
                }
            }
        }

        /// <summary>
        /// Property for the content of the sub textlabel
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public float SubTextPointSize
        {
            get
            {
                return Style?.SubText?.PointSize?.All ?? 0;
            }
            set
            {
                Style.SubText.PointSize = new Selector<float?>() { All = value };
            }
        }

        /// <summary>
        /// Property for the count of the sub textlabel
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public uint SubTextCount
        {
            get
            {
                return subTextCount;
            }
            set
            {
                if (value == 0 || subTextCount > 0)
                {
                    return;
                }
                subTextCount = value;
                InitializeSubText();
                ApplySubTextAttributes();
            }
        }

        /// <summary>
        /// Property for the string array of the array of the sub textlabel
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public string[] SubTextContentArray
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                int length = value.Length;
                if (subTextCount != length)
                {
                    return;
                }
                for (int i = 0; i < subTextCount; ++i)
                {
                    if (subTextLabelArray[i] != null)
                    {
                        subTextLabelArray[i].Text = value[i];
                    }
                }
                RelayoutTextComponents();
            }
        }

        /// <summary>
        /// Property for the enabled state of the divider
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public bool StateDividerEnabled
        {
            set
            {
                if (dividerView != null)
                {
                    if (value)
                    {
                        dividerView.Show();
                    }
                    else
                    {
                        dividerView.Hide();
                    }
                }
            }
        }

        /// <summary>
        /// Property for the start space
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public uint StartSpace
        {
            set
            {
                if (startSpace == value)
                {
                    return;
                }
                startSpace = value;
                RelayoutTextComponents();
                RelayoutStartItemRootView();
            }
        }

        /// <summary>
        /// Property for the end space
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public uint EndSpace
        {
            set
            {
                if (endSpace == value)
                {
                    return;
                }
                endSpace = value;
                RelayoutTextComponents();
                RelayoutEndItemRootView();
            }
        }

        /// <summary>
        /// Property for the size of the start item root view
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Size StartItemRootViewSize
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                startItemRootSize = value;
                ResizeStartItemRootView();
                RelayoutTextComponents();
            }
        }

        /// <summary>
        /// Property for the size of the end item root view
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Size EndItemRootViewSize
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                endItemRootSize = value;
                ResizeEndItemRootView();
                RelayoutTextComponents();
            }
        }

        /// <summary>
        /// Property for the space between start item and text
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public uint SpaceBetweenStartItemAndText
        {
            set
            {
                if (spaceBetweenStartItemAndText == value)
                {
                    return;
                }
                spaceBetweenStartItemAndText = value;
                RelayoutTextComponents();
            }
        }

        /// <summary>
        /// Property for the space between end item and text
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public uint SpaceBetweenEndItemAndText
        {
            set
            {
                if (spaceBetweenEndItemAndText == value)
                {
                    return;
                }
                spaceBetweenEndItemAndText = value;
                RelayoutTextComponents();
            }
        }

        /// <summary>
        /// Property for the resource url of the start icon, only useful in ItemAlign style in icon type
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public string StartIconURL
        {
            set
            {
                InitializeStartIcon();

                if (startIcon != null)
                {
                    startIcon.ResourceUrl = value;
                }
            }
        }

        /// <summary>
        /// Property for the content of the end text, only useful in ItemAlign style in CheckIcon type
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public string EndText
        {
            set
            {
                InitializeEndText();
                if (endText != null)
                {
                    endText.Text = value;
                }
            }
        }

        /// <summary>
        /// Function for user to add object to the start item root view customized
        /// </summary>
        /// <param name="obj"> The object will be added </param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public void BindObjectToStart(object obj)
        {
            if (startItemRootView == null || obj == null)
            {
                return;
            }
            startItemRootView.Add(obj as View);
        }

        /// <summary>
        /// Function for user to add object to the end item root view customized
        /// </summary>
        /// <param name="obj"> The object will be added </param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public void BindObjectToEnd(object obj)
        {
            if (endItemRootView == null || obj == null)
            {
                return;
            }
            endItemRootView.Add(obj as View);
        }

        /// <summary>
        /// Dispose List Item and all children on it.
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
                TouchEvent -= OnTouchEvent;
                Utility.Dispose(mainTextLabel);
                if (subTextLabelArray != null)
                {
                    for (int i = 0; i < subTextCount; ++i)
                    {
                        if (subTextLabelArray[i] != null)
                        {
                            Utility.Dispose(subTextLabelArray[i]);
                        }
                    }
                    subTextLabelArray = null;
                }
                Utility.Dispose(textRootView);
                Utility.Dispose(dividerView);
                Utility.Dispose(startIcon);
                Utility.Dispose(endIcon);
                Utility.Dispose(endText);
                Utility.Dispose(checkBoxObj);
                Utility.Dispose(switchObj);
                if (startItemRootView != null)
                {
                    uint childCount = startItemRootView.ChildCount;
                    for (uint i = 0; i < childCount; ++i)
                    {
                        View childObj = startItemRootView.GetChildAt(i);
                        if (childObj != null)
                        {
                            Utility.Dispose(childObj);
                        }
                    }
                    Utility.Dispose(startItemRootView);
                }
                if (endItemRootView != null)
                {
                    uint childCount = endItemRootView.ChildCount;
                    for (uint i = 0; i < childCount; ++i)
                    {
                        View childObj = endItemRootView.GetChildAt(i);
                        if (childObj != null)
                        {
                            Utility.Dispose(childObj);
                        }
                    }
                    Utility.Dispose(endItemRootView);
                }
            }
            base.Dispose(type);
        }

        /// <summary>
        /// Get List Item attribues.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        
        protected override ViewStyle GetViewStyle()
        {
            return new ListItemStyle();
        }

        /// <summary>
        /// Update List Item by attributes.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        protected override void OnUpdate()
        {
            CurrentStyleType();
            RelayoutComponents();
            OnLayoutDirectionChanged();
        }

        private void Initialize()
        {
            textRootView = new View()
            {
                ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                PositionUsesPivotPoint = true
            };
            this.Add(textRootView);

            if (Style.MainText != null)
            {
                mainTextLabel = new TextLabel();
                textRootView.Add(mainTextLabel);
                mainTextLabel.ApplyStyle(Style.MainText);
            }
            if (Style.DividerLine != null)
            {
                dividerView = new ImageView()
                {
                    ParentOrigin = Tizen.NUI.ParentOrigin.BottomCenter,
                    PivotPoint = Tizen.NUI.PivotPoint.BottomCenter,
                    PositionUsesPivotPoint = true
                };
                this.Add(dividerView);
                dividerView.ApplyStyle(Style.DividerLine);
            }
            if (Style.StartItemRoot != null)
            {
                startItemRootView = new View()
                {
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                    PositionUsesPivotPoint = true
                };
                this.Add(startItemRootView);
                startItemRootView.ApplyStyle(Style.StartItemRoot);
            }
            if (Style.EndItemRoot != null)
            {
                endItemRootView = new View()
                {
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                    PositionUsesPivotPoint = true
                };
                this.Add(endItemRootView);
                endItemRootView.ApplyStyle(Style.EndItemRoot);
            }
            InitializeEndIcon();
            this.TouchEvent += OnTouchEvent;
        }

        private void OnLayoutDirectionChanged()
        {
            if (Style == null)
            {
                return;
            }
            if (LayoutDirection == ViewLayoutDirectionType.LTR)
            {
                for (int i = 0; i < subTextCount; ++i)
                {
                    if(subTextLabelArray[i])
                    {
                        subTextLabelArray[i].LayoutDirection = ViewLayoutDirectionType.LTR;
                        subTextLabelArray[i].HorizontalAlignment = HorizontalAlignment.Begin;
                    }
                }
                if (Style.SubText != null)
                {
                    Style.SubText.HorizontalAlignment = HorizontalAlignment.Begin;
                }
                if (textRootView)
                {
                    textRootView.LayoutDirection = ViewLayoutDirectionType.LTR;
                    textRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    textRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    textRootView.PositionUsesPivotPoint = true;
                }
                if(mainTextLabel)
                {
                    if (styleType == StyleTypes.GroupIndex && groupIndexType == GroupIndexTypes.None)
                    {
                        if (Style.MainText2 != null)
                        {
                            Style.MainText2.HorizontalAlignment = HorizontalAlignment.Begin;
                        }
                    }
                    else
                    {
                        if (Style.MainText != null)
                        {
                            Style.MainText.HorizontalAlignment = HorizontalAlignment.Begin;
                        }
                    }
                    mainTextLabel.LayoutDirection = ViewLayoutDirectionType.LTR;
                    mainTextLabel.HorizontalAlignment = HorizontalAlignment.Begin;
                }

                if (startItemRootView)
                {
                    if(Style.StartItemRoot != null)
                    {
                        Style.StartItemRoot.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.StartItemRoot.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.StartItemRoot.PositionUsesPivotPoint = true;
                    }
                    startItemRootView.LayoutDirection = ViewLayoutDirectionType.LTR;
                    startItemRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    startItemRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    startItemRootView.PositionUsesPivotPoint = true;
                }
                if(endItemRootView)
                {
                    if(Style.EndItemRoot != null)
                    {
                        Style.EndItemRoot.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.EndItemRoot.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.EndItemRoot.PositionUsesPivotPoint = true;
                    }
                    endItemRootView.LayoutDirection = ViewLayoutDirectionType.LTR;
                    endItemRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    endItemRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    endItemRootView.PositionUsesPivotPoint = true;
                }
            }
            else
            {
                for (int i = 0; i < subTextCount; ++i)
                {
                    if (subTextLabelArray[i])
                    {
                        subTextLabelArray[i].LayoutDirection = ViewLayoutDirectionType.RTL;
                        subTextLabelArray[i].HorizontalAlignment = HorizontalAlignment.End;
                    }
                }
                if (Style.SubText != null)
                {
                    Style.SubText.HorizontalAlignment = HorizontalAlignment.End;
                }
                if (textRootView)
                {
                    textRootView.LayoutDirection = ViewLayoutDirectionType.RTL;
                    textRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    textRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    textRootView.PositionUsesPivotPoint = true;
                }
                if (mainTextLabel)
                {
                    if (styleType == StyleTypes.GroupIndex && groupIndexType == GroupIndexTypes.None)
                    {
                        if (Style.MainText2 != null)
                        {
                            Style.MainText2.HorizontalAlignment = HorizontalAlignment.End;
                        }
                    }
                    else
                    {
                        if (Style.MainText != null)
                        {
                            Style.MainText.HorizontalAlignment = HorizontalAlignment.End;
                        }
                    }
                    mainTextLabel.LayoutDirection = ViewLayoutDirectionType.RTL;
                    mainTextLabel.HorizontalAlignment = HorizontalAlignment.End;
                }
                if (startItemRootView)
                {
                    if (Style.StartItemRoot != null)
                    {
                        Style.StartItemRoot.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.StartItemRoot.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.StartItemRoot.PositionUsesPivotPoint = true;
                    }
                    startItemRootView.LayoutDirection = ViewLayoutDirectionType.RTL;
                    startItemRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    startItemRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    startItemRootView.PositionUsesPivotPoint = true;
                }
                if (endItemRootView)
                {
                    if (Style.EndItemRoot != null)
                    {
                        Style.EndItemRoot.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.EndItemRoot.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.EndItemRoot.PositionUsesPivotPoint = true;
                    }
                    endItemRootView.LayoutDirection = ViewLayoutDirectionType.RTL;
                    endItemRootView.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    endItemRootView.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    endItemRootView.PositionUsesPivotPoint = true;
                }
            }
        }

        private void InitializeSubText()
        {
            if (subTextLabelArray != null || subTextCount == 0)
            {
                return;
            }
            subTextLabelArray = new TextLabel[subTextCount];
            for (int i = 0; i < subTextCount; ++i)
            {
                subTextLabelArray[i] = new TextLabel();
                if (textRootView != null)
                {
                    textRootView.Add(subTextLabelArray[i]);
                }
            }
        }

        private void InitializeStartIcon()
        {
            if (Style.StartIcon != null && startIcon == null && startItemRootView != null)
            {
                startIcon = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                startItemRootView.Add(startIcon);
            }
        }

        private void InitializeEndIcon()
        {
            if (Style.EndIcon != null && endItemRootView != null)
            {
                endIcon = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                endItemRootView.Add(endIcon);
                endIcon.ApplyStyle(Style.EndIcon);
            }
        }

        private void InitializeEndText()
        {
            if (Style.EndText != null && endItemRootView != null)
            {
                endText = new TextLabel()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                endItemRootView.Add(endText);
                endText.ApplyStyle(Style.EndText);
            }
        }

        private void ApplySubTextAttributes()
        {
            if (subTextCount == 0 || subTextLabelArray == null || Style == null)
            {
                return;
            }
        }

        private void RelayoutComponents()
        {
            RelayoutTextComponents();
            ResizeStartItemRootView();
            RelayoutStartItemRootView();
            ResizeEndItemRootView();
            RelayoutEndItemRootView();
            RelayoutDivider();

            UpdateTypeForItemAlignStyle();
            UpdateTypeForGroupIndexStyle();
        }

        private void RelayoutTextComponents()
        {
            int heightSum = 0;
            int rootWidth = TextRootViewWidth();
            if (mainTextLabel != null)
            {
                int height = (int)mainTextLabel.NaturalSize.Height;
                mainTextLabel.Size = new Size(rootWidth, height);
                mainTextLabel.Position = new Position(0, 0);
                heightSum += height;
            }
            if (subTextLabelArray != null)
            {
                int length = subTextLabelArray.Length;
                for (int i = 0; i < length; ++i)
                {
                    if (subTextLabelArray[i] != null)
                    {
                        int height = (int)subTextLabelArray[i].NaturalSize.Height;
                        subTextLabelArray[i].Size = new Size(rootWidth, height);
                        subTextLabelArray[i].Position = new Position(0, heightSum);
                        heightSum += height;
                    }
                }
            }
            if (textRootView != null)
            {
                int testrootx = StartSpaceValue() + StartItemRootViewWidth() + SpaceBetweenStartItemAndTextValue();
                textRootView.Size = new Size(rootWidth, heightSum);
                if (this.LayoutDirection == ViewLayoutDirectionType.LTR)
                    textRootView.Position = new Position(testrootx, 0);
                else
                    textRootView.Position = new Position(-testrootx, 0);
            }
        }

        private void RelayoutDivider()
        {
            if (dividerView == null)
            {
                return;
            }
            Size size = this.Size;
            dividerView.Size = new Size(size.Width - StartSpaceValue() - EndSpaceValue(), DividerSize().Height);
        }

        private void ResizeStartItemRootView()
        {
            if (startItemRootView == null || startItemRootSize == null)
            {
                return;
            }
            startItemRootView.Size = startItemRootSize;
        }

        private void RelayoutStartItemRootView()
        {
            if (startItemRootView == null)
            {
                return;
            }
            int startSpace = StartSpaceValue();
            if (this.LayoutDirection == ViewLayoutDirectionType.LTR)
                startItemRootView.Position = new Position(startSpace, 0);
            else
                startItemRootView.Position = new Position(-startSpace, 0);
        }

        private void ResizeEndItemRootView()
        {
            if (endItemRootView == null || endItemRootSize == null)
            {
                return;
            }
            endItemRootView.Size = endItemRootSize;
        }

        private void RelayoutEndItemRootView()
        {
            if (endItemRootView == null)
            {
                return;
            }
            int endSpace = EndSpaceValue();
            if (this.LayoutDirection == ViewLayoutDirectionType.LTR)
                endItemRootView.Position = new Position(-endSpace, 0);
            else
                endItemRootView.Position = new Position(endSpace, 0);
        }

        private int TextRootViewWidth()
        {
            int width = 0;
            int startPartSpace = 0;
            if (startItemRootView != null)
            {
                startPartSpace = StartItemRootViewWidth() + SpaceBetweenStartItemAndTextValue();
            }
            int endPartSpace = 0;
            if (styleType == StyleTypes.ItemAlign)
            {
                if (itemAlignType == ItemAlignTypes.CheckIcon)
                {
                    if (endItemRootView != null)
                    {
                        endPartSpace = EndItemRootViewWidth() + SpaceBetweenEndItemAndTextValue();
                    }
                }
                else if (itemAlignType == ItemAlignTypes.Icon)
                {
                    // in icon type, there is no right part
                }
            }
            else if (styleType == StyleTypes.GroupIndex)
            {
                if (groupIndexType == GroupIndexTypes.None)
                {
                    // in none type, there is no right part
                }
                else
                {
                    if (endItemRootView != null)
                    {
                        endPartSpace = EndItemRootViewWidth();
                    }
                }
            }
            else
            {
                if (endItemRootView != null)
                {
                    endPartSpace = EndItemRootViewWidth() + SpaceBetweenEndItemAndTextValue();
                }
            }

            width = (int)(this.Size.Width - StartSpaceValue() - EndSpaceValue() - startPartSpace - endPartSpace);
            return width;
        }

        private int StartSpaceValue()
        {
            int space = 0;
            if (startSpace != null)
            {
                space = (int)startSpace.Value;
            }
            else
            {
                if (Style != null && Style.StartSpace != null)
                {
                    space = (int)Style.StartSpace.Value;
                }
            }
            return space;
        }

        private int EndSpaceValue()
        {
            int space = 0;
            if (endSpace != null)
            {
                space = (int)endSpace.Value;
            }
            else
            {
                if (Style != null && Style.EndSpace != null)
                {
                    space = (int)Style.EndSpace.Value;
                }
            }
            return space;
        }

        private int SpaceBetweenStartItemAndTextValue()
        {
            int space = 0;
            if (spaceBetweenStartItemAndText != null)
            {
                space = (int)spaceBetweenStartItemAndText.Value;
            }
            else
            {
                if (Style != null && Style.SpaceBetweenStartItemAndText != null)
                {
                    space = (int)Style.SpaceBetweenStartItemAndText.Value;
                }
            }
            return space;
        }

        private int SpaceBetweenEndItemAndTextValue()
        {
            int space = 0;
            if (spaceBetweenEndItemAndText != null)
            {
                space = (int)spaceBetweenEndItemAndText.Value;
            }
            else
            {
                if (Style != null && Style.SpaceBetweenEndItemAndText != null)
                {
                    space = (int)Style.SpaceBetweenEndItemAndText.Value;
                }
            }
            return space;
        }

        private int StartItemRootViewWidth()
        {
            int width = 0;
            if (startItemRootSize != null)
            {
                width = (int)startItemRootSize.Width;
            }
            else
            {
                if (Style != null && Style.StartItemRoot != null
                    && Style.StartItemRoot.Size != null)
                {
                    width = (int)Style.StartItemRoot.Size.Width;
                }
            }
            return width;
        }

        private int EndItemRootViewWidth()
        {
            int width = 0;
            if (endItemRootSize != null)
            {
                width = (int)endItemRootSize.Width;
            }
            else
            {
                if (Style != null && Style.EndItemRoot != null
                    && Style.EndItemRoot.Size != null)
                {
                    width = (int)Style.EndItemRoot.Size.Width;
                }
            }
            return width;
        }

        private Size DividerSize()
        {
            Size size = new Size(0, 0);
            if (Style != null && Style.DividerLine != null
                && Style.DividerLine.Size != null)
            {
                size = Style.DividerLine.Size;
            }
            return size;
        }

        private bool OnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (!isEnabled)
            {
                return false;
            }
            if (state == PointStateType.Down)
            {
                isPressed = true;
                UpdateState();
            }
            else if (state == PointStateType.Finished)
            {
                isPressed = false;
                UpdateState();
            }
            return false;
        }

        private void UpdateTypeForItemAlignStyle()
        {
            if (styleType != StyleTypes.ItemAlign || Style == null)
            {
                return;
            }

            if (itemAlignType == ItemAlignTypes.Icon)
            {
                if (startIcon != null)
                {
                    startIcon.Show();
                }
                if (checkBoxObj != null)
                {
                    checkBoxObj.Hide();
                }

                if (endItemRootView != null)
                {
                    endItemRootView.Hide();
                }
            }
            else
            {
                if (checkBoxObj == null && Style.CheckBoxStyle != null && startItemRootView != null)
                {
                    checkBoxObj = new CheckBox(Style.CheckBoxStyle)
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    startItemRootView.Add(checkBoxObj);
                }
                if (checkBoxObj != null)
                {
                    checkBoxObj.Show();
                }
                if (startIcon != null)
                {
                    startIcon.Hide();
                }

                if (endItemRootView != null)
                {
                    endItemRootView.Show();
                }
            }
        }

        private void UpdateTypeForGroupIndexStyle()
        {
            if (styleType != StyleTypes.GroupIndex || Style == null)
            {
                return;
            }
            if (groupIndexType == GroupIndexTypes.None)
            {
                // 56 + text + 56
                if (endItemRootView != null)
                {
                    endItemRootView.Hide();
                }
            }
            else if (groupIndexType == GroupIndexTypes.Next)
            {
                // 56 + text + right icon(48) + 56
                if (endItemRootView != null)
                {
                    endItemRootView.Show();
                }
                if (switchObj != null)
                {
                    switchObj.Hide();
                }
                if (endIcon != null)
                {
                    endIcon.Show();
                }
            }
            else if (groupIndexType == GroupIndexTypes.Switch)
            {
                if (switchObj == null && Style.SwitchStyle != null && endItemRootView != null)
                {
                    switchObj = new Switch(Style.SwitchStyle)
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    endItemRootView.Add(switchObj);
                }
                // 56 + text + switch(72) + 56
                if (endItemRootView != null)
                {
                    endItemRootView.Show();
                }
                if (endIcon != null)
                {
                    endIcon.Hide();
                }
                if (switchObj != null)
                {
                    switchObj.Show();
                }
            }
            else if (groupIndexType == GroupIndexTypes.DropDown)
            {
                // 56 + text + drop down(48) + 56
                if (endItemRootView != null)
                {
                    endItemRootView.Show();
                }
                if (endIcon != null)
                {
                    endIcon.Hide();
                }
                if (switchObj != null)
                {
                    switchObj.Hide();
                }
            }
        }

        private void CurrentStyleType()
        {
            if (Style != null)
            {
                styleType = Style.StyleType;
            }
        }

        private void UpdateState()
        {
            if (styleType != StyleTypes.Effect)
            {
                return;
            }
            if (mainTextLabel == null || Style == null)
            {
                return;
            }
            if (!isEnabled)
            {
                if (Style.MainText != null && Style.MainText.TextColor != null)
                {
                    mainTextLabel.TextColor = Style.MainText.TextColor.Disabled;
                }
                if (Style.BackgroundColor != null)
                {
                    BackgroundColor = Style.BackgroundColor.Disabled;
                }
            }
            else
            {
                if (isPressed)
                {
                    if (Style.MainText != null && Style.MainText.TextColor != null)
                    {
                        mainTextLabel.TextColor = Style.MainText.TextColor.Pressed;
                    }
                    if (Style.BackgroundColor != null)
                    {
                        BackgroundColor = Style.BackgroundColor.Pressed;
                    }
                }
                else
                {
                    if (isSelected)
                    {
                        if (Style.MainText != null && Style.MainText.TextColor != null)
                        {
                            mainTextLabel.TextColor = Style.MainText.TextColor.Selected;
                        }
                        if (Style.BackgroundColor != null)
                        {
                            BackgroundColor = Style.BackgroundColor.Selected;
                        }
                    }
                    else
                    {
                        if (Style.MainText != null && Style.MainText.TextColor != null)
                        {
                            mainTextLabel.TextColor = Style.MainText.TextColor.Normal;
                        }
                        if (Style.BackgroundColor != null)
                        {
                            BackgroundColor = Style.BackgroundColor.Normal;
                        }
                    }
                }
            }
        }
    }
}
