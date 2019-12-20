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
using System.Collections.Generic;
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// Navigation is one kind of common component, it can be used as instruction, guide or direction.
    /// User can handle Navigation by adding/inserting/deleting NavigationItem.
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
    public class Navigation : Control
    {
        private const int aniTime = 100; // will be defined in const file later
        private List<NavigationDataItem> itemList = new List<NavigationDataItem>();
        private List<View> dividerLineList = new List<View>();
        private int curIndex = -1;
        private View rootView;
        private EventHandler<TouchEventArgs> itemTouchHander;

        /// <summary>
        /// Creates a new instance of a Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public Navigation() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Navigation with style.
        /// </summary>
        /// <param name="style">Create Navigation by special style defined in UX.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public Navigation(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Navigation with attributes.
        /// </summary>
        /// <param name="attributes">Create Navigation by attributes customized by user.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public Navigation(NavigationStyle attributes) : base(attributes)
        {
            Initialize();
        }

        /// <summary>
        /// An event for the item changed signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public event EventHandler<ItemChangeEventArgs> ItemChangedEvent;

        /// <summary>
        /// An event for the item touch signal which can be used to subscribe or unsubscribe the event handler provided by the user.<br />
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public event EventHandler<TouchEventArgs> ItemTouchEvent
        {
            add
            {
                itemTouchHander += value;
            }
            remove
            {
                itemTouchHander -= value;
            }
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new NavigationStyle Style => ViewStyle as NavigationStyle;

        /// <summary>
        /// Selected item's index in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public int SelectedItemIndex
        {
            get
            {
                return curIndex;
            }
            set
            {
                if (value < itemList.Count)
                {
                    UpdateSelectedItem(itemList[value]);
                }
            }
        }

        /// <summary>
        /// Gap between items.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public int ItemGap
        {
            get
            {
                if (Style == null)
                {
                    return 0;
                }
                return Style.ItemGap;
            }
            set
            {
                Style.ItemGap = value;
            }
        }  

        /// <summary>
        /// Shadow image's size in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public Size ShadowImageSize
        {
            get
            {
                return Style.Shadow.Size;
            }
            set
            {
                if (value != null)
                {
                    Style.Shadow.Size = value;
                }
            }
        }

        /// <summary>
        /// Shadow image's resource url in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public string ShadowImageUrl
        {
            get
            {
                return Style.Shadow.ResourceUrl?.All;
            }
            set
            {
                if (value != null)
                {
                    Style.Shadow.ResourceUrl = new Selector<string>() { All = value };
                }
            }
        }

        /// <summary>
        /// Background image's resource url in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public string BackgroundImageUrl
        {
            get
            {
                return Style.BackgroundImage?.All;
            }
            set
            {
                if (value != null)
                {
                    Style.BackgroundImage = new Selector<string>() { All = value };
                }
            }
        }

        /// <summary>
        /// Divider line's color in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public Color DividerLineColor
        {
            get
            {
                return Style.DividerLineColor;
            }
            set
            {
                Style.DividerLineColor = value;
            }
        }

        /// <summary>
        /// Padding in Navigation.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Extents Padding
        {
            get
            {
                return Style.Padding;
            }
            set
            {
                Style.Padding = value;
            }
        }

        /// <summary>
        /// Flag to decide Navigation's size is fill with all navigation items' size or not.
        /// True is fit, false is none. If false, then Navigation's size can be updated by user.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public bool IsFitWithItems
        {
            get
            {
                return Style.IsFitWithItems;
            }
            set
            {
                Style.IsFitWithItems = value;
            }
        }

        /// <summary>
        /// Add navigation item by item data. The added item will be added to end of all items automatically.
        /// </summary>
        /// <param name="itemData">Item data which will apply to navigaiton item view.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public void AddItem(NavigationDataItem itemData)
        {
            AddItemByIndex(itemData, itemList.Count);
        }

        /// <summary>
        /// Insert navigation item by item data. The inserted item will be added to the special position by index automatically.
        /// </summary>
        /// <param name="itemData">Item data which will apply to navigaiton item view.</param>
        /// <param name="index">Position index where will be inserted.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public void InsertItem(NavigationDataItem itemData, int index)
        {
            AddItemByIndex(itemData, index);
        }

        /// <summary>
        /// Delete navigation item by index.
        /// </summary>
        /// <param name="itemIndex">Position index where will be deleted.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public void DeleteItem(int itemIndex)
        {
            if (itemList == null || itemIndex < 0 || itemIndex >= itemList.Count)
            {
                return;
            }


            if (curIndex > itemIndex || (curIndex == itemIndex && itemIndex == itemList.Count - 1))
            {
                curIndex--;
            }

            Remove(itemList[itemIndex]);
            itemList[itemIndex].Dispose();
            itemList.RemoveAt(itemIndex);

            UpdateItem();
            if (curIndex != -1)
            {
                itemList[curIndex].ControlState = Tizen.NUI.Components.ControlStates.Selected;
            }
        }

        protected override void OnUpdate()
        {
            UpdateItem();
        }

        /// <summary>
        /// Dispose Navigation and all children on it.
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
                if (dividerLineList != null)
                {
                    for (int i = 0; i < dividerLineList.Count; i++)
                    {
                        if (dividerLineList[i] != null)
                        {
                            Utility.Dispose(dividerLineList[i]);
                        }
                    }
                    dividerLineList.Clear();
                    dividerLineList = null;
                }
                if (itemList != null)
                {
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        if (itemList[i] != null)
                        {
                            Utility.Dispose(itemList[i]);
                        }
                    }
                    itemList.Clear();
                    itemList = null;
                }
                Utility.Dispose(rootView);
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Get Navigation attribues.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        protected override ViewStyle GetViewStyle()
        {
            return new NavigationStyle();
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ApplyStyle(ViewStyle viewStyle)
        {
            base.ApplyStyle(viewStyle);

            NavigationStyle style = viewStyle as NavigationStyle;
            if (style != null)
            {                
            }
        }

        private void Initialize()
        {
            rootView = new View();
            rootView.Name = "RootView";
            Add(rootView);
        }

        private void AddItemByIndex(NavigationDataItem itemData, int index)
        {
            NavigationDataItem item = itemData;
            item.TouchEvent += OnItemTouchEvent;
            rootView.Add(item);
            if (itemData.Style.Size != null)
            {
                item.Size = itemData.Style.Size;
            }
            if (index >= itemList.Count)
            {
                itemList.Add(item);
            }
            else
            {
                itemList.Insert(index, item);
            }

            AddDividerLine();

            UpdateItem();
            if (curIndex != -1)
            {
                itemList[curIndex].ControlState = Tizen.NUI.Components.ControlStates.Selected;
            }
        }

        private void AddDividerLine()
        {
            View dividerLine = new View()
            {
                BackgroundColor = Style.DividerLineColor,
                PositionUsesPivotPoint = true,
                ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                Position = new Position(0, 0)
            };
            dividerLine.Name = "DividerLine " + dividerLineList.Count;
            rootView.Add(dividerLine);
            dividerLineList.Add(dividerLine);
        }

        private void UpdateItem()
        {
            int totalNum = itemList.Count;
            if (totalNum == 0)
            {
                return;
            }
            int leftSpace = Style.Padding.Start;
            int rightSpace = Style.Padding.End;
            int topSpace = Style.Padding.Top;
            int bottomSpace = Style.Padding.Bottom;

            int preX = leftSpace;
            int preY = topSpace;
            int parentW = (int)itemList[0].Size.Width + leftSpace + rightSpace;
            int parentH = topSpace + bottomSpace;
            int itemGap = Style.ItemGap;
            for (int i = 0; i < totalNum; i++)
            {
                itemList[i].Index = i;
                itemList[i].Name = "Item" + i;
                itemList[i].Position = new Position(preX, preY);
                dividerLineList[i].Size = new Size(itemList[i].Size.Width, itemGap);
                dividerLineList[i].Position = new Position(preX, preY + itemList[i].Size.Height);
                parentH += (int)itemList[i].Size.Height;
                preY += (int)itemList[i].Size.Height + itemGap;

                dividerLineList[i].BackgroundColor = Style.DividerLineColor;
                dividerLineList[i].Show();
            }
            dividerLineList[totalNum - 1].Hide();

            if (rootView.Size.EqualTo(new Size(parentW, parentH)) == false)
            {
                rootView.Size = new Size(parentW, parentH);
            }

            if (Style.IsFitWithItems == true)
            {
                if (Size.EqualTo(new Size(parentW, parentH)) == false)
                {
                    Size = new Size(parentW, parentH);
                }
            }
            else
            {
                rootView.PositionY = (Size.Height - rootView.Size.Height) / 2;
            }

            UpdateShadowImage();
        }

        private void UpdateSelectedItem(NavigationDataItem item)
        {
            if (item == null || curIndex == item.Index)
            {
                return;
            }

            ItemChangeEventArgs e = new ItemChangeEventArgs
            {
                PreviousIndex = curIndex,
                CurrentIndex = item.Index
            };
            ItemChangedEvent?.Invoke(this, e);

            if (curIndex != -1)
            {
                itemList[curIndex].ControlState = Tizen.NUI.Components.ControlStates.Normal;
            }
            curIndex = item.Index;
            itemList[curIndex].ControlState = Tizen.NUI.Components.ControlStates.Selected;
        }

        private void UpdateShadowImage()
        {
            if (Style.Shadow == null)
            {
                return;
            }
            Style.Shadow.Position = new Position(-Style.Shadow.Size?.Width??0, 0);
        }

        private bool OnItemTouchEvent(object source, TouchEventArgs e)
        {
            NavigationDataItem item = source as NavigationDataItem;
            if (item == null)
            {
                return false;
            }
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Up)
            {
                UpdateSelectedItem(item);
            }

            itemTouchHander?.Invoke(this, e);

            return true;
        }   

        /// <summary>
        /// NavigationItemData is a class to record all data which will be applied to Navigation item.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public class NavigationDataItem : Button
        {
            private TextLabel subText;
            private View dividerLine;

            /// <summary>
            /// Creates a new instance of a NavigationItemData.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public NavigationDataItem() : base()
            {
                Initalize();
            }

            /// <summary>
            /// Creates a new instance of a NavigationItemData with style.
            /// </summary>
            /// <param name="style">Create NavigationItemData by special style defined in UX.</param>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public NavigationDataItem(string style) : base(style)
            {
                Initalize();
            }

            /// <summary>
            /// Creates a new instance of a NavigationItemData with attributes.
            /// </summary>
            /// <param name="attributes">Create NavigationItemData by attributes customized by user.</param>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public NavigationDataItem(NavigationItemStyle attributes) : base(attributes)
            {
                Initalize();
            }

            /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
            [EditorBrowsable(EditorBrowsableState.Never)]
            public new NavigationItemStyle Style => ViewStyle as NavigationItemStyle;

            /// <summary>
            /// Navigation item size.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public new Size Size
            {
                get
                {
                    return Style.Size;
                }
                set
                {
                    Style.Size = value;
                }
            }

            /// <summary>
            /// Sub text string in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public string SubText
            {
                get
                {
                    return Style.SubText.Text?.All;
                }
                set
                {
                    if (value != null)
                    {
                        Style.SubText.Text = new Selector<string>() { All = value };
                    }
                }
            }

            /// <summary>
            /// Text size in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Size TextSize
            {
                get
                {
                    return Style.Text?.Size;
                }
                set
                {
                    Style.Text.Size = value;
                }
            }

            /// <summary>
            /// Sub text size in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Size SubTextSize
            {
                get
                {
                    return Style.SubText?.Size;
                }
                set
                {
                    Style.SubText.Size = value;
                }
            }

            /// <summary>
            /// Sub text point size in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public float SubTextPointSize
            {
                get
                {
                    return Style.SubText?.PointSize?.All ?? 0;
                }
                set
                {
                    Style.SubText.PointSize = new Selector<float?>() { All = value };
                }
            }

            /// <summary>
            /// Sub text font family in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public string SubTextFontFamily
            {
                get
                {
                    return Style.SubText.FontFamily.All;
                }
                set
                {
                    Style.SubText.FontFamily = new Selector<string>() { All = value };
                }
            }

            /// <summary>
            /// Sub text color in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Color SubTextColor
            {
                get
                {
                    return Style.SubText?.TextColor?.All;
                }
                set
                {
                    Style.SubText.TextColor = new Selector<Color>() { All = value };
                }
            }

            /// <summary>
            /// Sub text color selector in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Selector<Color> SubTextColorSelector
            {
                get
                {
                    return Style.SubText?.TextColor;
                }
                set
                {
                    if (value != null)
                    {
                        Style.SubText.TextColor = value;
                    }
                }
            }

            /// <summary>
            /// Icon size in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Size IconSize
            {
                get
                {
                    return Style.Icon?.Size;
                }
                set
                {
                    Style.Icon.Size = value;
                }
            }

            /// <summary>
            /// Divider line color in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Color DividerLineColor
            {
                get
                {
                    return Style.DividerLine?.BackgroundColor?.All;
                }
                set
                {
                    Style.DividerLine.BackgroundColor =  new Selector<Color>() { All = value };
                }
            }

            /// <summary>
            /// Divider line size in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Size DividerLineSize
            {
                get
                {
                    return Style.DividerLine?.Size;
                }
                set
                {
                    Style.DividerLine.Size = value;
                }
            }

            /// <summary>
            /// Divider line's position in Navigation item view.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public Position DividerLinePosition
            {
                get
                {
                    return Style.DividerLine?.Position;
                }
                set
                {
                    Style.DividerLine.Position = value;
                }
            }

            /// <summary>
            /// Padding in Navigation Item.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            [EditorBrowsable(EditorBrowsableState.Never)]
            public new Extents Padding
            {
                get
                {
                    return Style.Padding;
                }
                set
                {
                    Style.Padding = value;
                }
            }

            /// <summary>
            /// Flag to decide icon is in center or not in Navigation item view.
            /// If true, icon image will in the center of NavigationItem, if false, it will be decided by TopSpace.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public bool EnableIconCenter
            {
                get
                {
                    return Style.EnableIconCenter;
                }
                set
                {
                    Style.EnableIconCenter = value;
                }
            }

            internal int Index
            {
                get;
                set;
            }

            /// <summary>
            /// Get attributes.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            protected override ViewStyle GetViewStyle()
            {
                return new NavigationItemStyle();
            }

            /// <summary>
            /// Layout children.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            protected override void LayoutChild()
            {
                // important! avoid layout child again.
            }

            /// <summary>
            /// Measure text.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            protected override void MeasureText()
            {
                // important! avoid measuring text again.
            }

            /// <summary>
            /// Dispose.
            /// </summary>
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
                    if (subText != null)
                    {
                        Utility.Dispose(subText);
                    }
                    if (dividerLine != null)
                    {
                        Utility.Dispose(dividerLine);
                    }
                }

                base.Dispose(type);
            }

            protected override void OnUpdate()
            {
                int leftSpace = Style.Padding.Start;
                int rightSpace = Style.Padding.End;
                int topSpace = Style.Padding.Top;
                int bottomSpace = Style.Padding.Bottom;

                Style.Icon.PositionUsesPivotPoint = true;
                if (Style.EnableIconCenter == true)
                {
                    Style.Icon.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
                    Style.Icon.PivotPoint = Tizen.NUI.PivotPoint.Center;
                }
                else
                {
                    Style.Icon.ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft;
                    Style.Icon.PivotPoint = Tizen.NUI.PivotPoint.TopLeft;

                    int w = (int)Size.Width;
                    int h = (int)Size.Height;
                    int iconX = (int)((w - Style.Icon.Size?.Width ?? 0) / 2);
                    int iconY = topSpace;
                    Style.Icon.Position = new Position(iconX, iconY);

                    int textPosX = leftSpace;
                    int textPosY = (int)(iconY + Style.Icon.Size?.Height ?? 0);
                    if (Style.Text != null)
                    {
                        Style.Text.Position = new Position(textPosX, textPosY);
                        if (Style.Text.Size != null)
                        {
                            textPosY += (int)Style.Text.Size.Height;
                        }
                    }
                    if (Style.SubText != null)
                    {
                        Style.SubText.Position = new Position(textPosX, textPosY);
                    }
                }
            }

            /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
            [EditorBrowsable(EditorBrowsableState.Never)]
            public override void ApplyStyle(ViewStyle viewStyle)
            {
                base.ApplyStyle(viewStyle);

                NavigationItemStyle itemStyle = viewStyle as NavigationItemStyle;
                if (subText == null)
                {
                    subText = new TextLabel()
                    {
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                        PositionUsesPivotPoint = true,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Add(subText);
                }
                subText.ApplyStyle(itemStyle.SubText);

                if (dividerLine == null)
                {
                    dividerLine = new View()
                    {
                        ParentOrigin = Tizen.NUI.ParentOrigin.TopLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.TopLeft,
                        PositionUsesPivotPoint = true,
                    };
                    Add(dividerLine);
                }
                dividerLine.ApplyStyle(itemStyle.DividerLine);
            }

            private void Initalize()
            {
            }
        }

        /// <summary>
        /// ItemChangeEventArgs is a class to record item change event arguments which will sent to user.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        public class ItemChangeEventArgs : EventArgs
        {
            /// <summary> previous selected index of Navigation </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public int PreviousIndex;

            /// <summary> current selected index of Navigation </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
            public int CurrentIndex;
        }
    }
}
