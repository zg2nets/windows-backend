/*
 * Copyright(c) 2018 Samsung Electronics Co., Ltd.
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
using Tizen.NUI.BaseComponents;
using Tizen.NUI;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// Pagination shows the number of pages available and the currently active page.
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.    
    public class Pagination : Tizen.NUI.Components.DA.Pagination
    {
        private ImageView returnArrow;
        private ImageView nextArrow;

        private int indicatorCount;
        private int selectedIndex;
        private int maxCountOnePage = 10;

        private TapGestureDetector tapGestureDetector;

        private EventHandler<SelectChangeEventArgs> selectChangeEventHandlers;

        /// <summary>
        /// Creates a new instance of a Pagination.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Pagination() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Pagination using style.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Pagination(string style) : base(style)
        {
            Initialize();
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new PaginationStyle Style => ViewStyle as PaginationStyle;

        /// <summary>
        /// Select indicator changed event.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public event EventHandler<SelectChangeEventArgs> SelectChangeEvent
        {
            add
            {
                selectChangeEventHandlers += value;
            }
            remove
            {
                selectChangeEventHandlers -= value;
            }
        }

        /// <summary>
        /// Gets or sets the resource of return arrow button.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Selector<string> ReturnArrowUrl
        {
            get
            {
                return Style?.ReturnArrow?.ResourceUrl;
            }
            set
            {
                if (value == null || Style == null)
                {
                    return;
                }
                Style.ReturnArrow.ResourceUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the resource of next arrow button.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Selector<string> NextArrowUrl
        {
            get
            {
                return Style?.NextArrow?.ResourceUrl;
            }
            set
            {
                if (value == null || Style == null)
                {
                    return;
                }
                Style.NextArrow.ResourceUrl = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of return arrow button.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Size ReturnArrowSize
        {
            get
            {
                return Style?.ReturnArrow?.Size;
            }
            set
            {
                if (value == null || Style == null)
                {
                    return;
                }
                Style.ReturnArrow.Size = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of next arrow button.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Size NextArrowSize
        {
            get
            {
                return Style?.NextArrow?.Size;
            }
            set
            {
                if (value == null || Style == null)
                {
                    return;
                }
                Style.NextArrow.Size = value;
            }
        }

        /// <summary>
        /// Gets or sets the count of the pages/indicators.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public new int IndicatorCount
        {
            get
            {
                return indicatorCount;
            }
            set
            {
                if (indicatorCount == value)
                {
                    return;
                }
                indicatorCount = value;
                LayoutUpdate();
            }
        }

        /// <summary>
        /// Gets or sets the index of the select indicator.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public new int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (selectedIndex == value)
                {
                    return;
                }
                int previousIndex = selectedIndex;
                selectedIndex = value;
                LayoutUpdate();
                SelectChangeEventArgs args = new SelectChangeEventArgs();
                args.PreviousIndex = previousIndex;
                args.CurrentIndex = selectedIndex;
                OnSelectChangeEvent(this, args);
            }
        }

        /// <summary>
        /// you can override it to clean-up your own resources.
        /// </summary>
        /// <param name="type">DisposeTypes</param>
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
                if (tapGestureDetector != null)
                {
                    tapGestureDetector.Detected -= OnTapGestureDetected;
                    tapGestureDetector.Dispose();
                    tapGestureDetector = null;
                }
                Utility.Dispose(returnArrow);
                Utility.Dispose(nextArrow);
            }

            base.Dispose(type);
        }

        /// <summary>
        /// you can override it to create your own default attributes.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override ViewStyle GetViewStyle()
        {
            return new PaginationStyle();
        }

        /// <summary>
        /// you can override it to handle the tap gesture.
        /// </summary>
        /// <param name="source">TapGestureDetector</param>
        /// <param name="e">DetectedEventArgs</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void OnTapGestureDetected(object source, TapGestureDetector.DetectedEventArgs e)
        {
            if (e.View == returnArrow)
            {
                if (selectedIndex > 0)
                {
                    SelectedIndex = selectedIndex - 1;
                }
            }
            else if (e.View == nextArrow)
            {
                if (selectedIndex < indicatorCount - 1)
                {
                    SelectedIndex = selectedIndex + 1;
                }
            }
            else
            {
                Position selectIndicatorPosition = GetIndicatorPosition(selectedIndex % maxCountOnePage);
                if (e.TapGesture.LocalPoint.X < selectIndicatorPosition.X)
                {
                    if (selectedIndex > 0)
                    {
                        SelectedIndex = selectedIndex - 1;
                    }
                }
                else if (e.TapGesture.LocalPoint.X > selectIndicatorPosition.X + Style.IndicatorSize.Width)
                {
                    if (selectedIndex < indicatorCount - 1)
                    {
                        SelectedIndex = selectedIndex + 1;
                    }
                }
            }
        }

        private void Initialize()
        {
            returnArrow = new ImageView()
            {
                Name = "ReturnArrow",
                //BackgroundColor = Color.Cyan
            };
            Add(returnArrow);
            returnArrow.ApplyStyle(Style.ReturnArrow);

            nextArrow = new ImageView()
            {
                Name = "NextArrow",
                //BackgroundColor = Color.Cyan
            };
            Add(nextArrow);
            nextArrow.ApplyStyle(Style.NextArrow);

            tapGestureDetector = new TapGestureDetector();
            tapGestureDetector.Detected += OnTapGestureDetected;
            tapGestureDetector.Attach(returnArrow);
            tapGestureDetector.Attach(nextArrow);
        }

        private void LayoutUpdate()
        {
            int maxCount = GetMaxCountOnePage();
            if (maxCountOnePage != maxCount)
            {
                maxCountOnePage = maxCount;
            }

            int pageCount = (indicatorCount + maxCountOnePage - 1) / maxCountOnePage;
            int pageIndex = selectedIndex / maxCountOnePage;
            if (pageIndex == pageCount - 1)
            {
                base.IndicatorCount = indicatorCount % maxCountOnePage;
            }
            else
            {
                base.IndicatorCount = maxCountOnePage;
            }
            base.SelectedIndex = selectedIndex % maxCountOnePage;

            if (pageIndex == 0)
            {
                returnArrow.Hide();
            }
            else
            {
                returnArrow.Show();
            }

            if (pageIndex == pageCount - 1)
            {
                nextArrow.Hide();
            }
            else
            {
                nextArrow.Show();
            }
        }

        private int GetMaxCountOnePage()
        {
            int count = 10;
            if (Style != null)
            {
                int returnArrowWidth = 0, nextArrowWidth = 0;
                if (Style.ReturnArrow != null && Style.ReturnArrow.Size != null)
                {
                    returnArrowWidth = (int)Style.ReturnArrow.Size.Width;
                }
                if (Style.NextArrow != null && Style.NextArrow.Size != null)
                {
                    nextArrowWidth = (int)Style.NextArrow.Size.Width;
                }
                int indicatorWidth = 0, indicatorHeight = 0;
                int indicatorSpacing = 0;
                if (Style.IndicatorSize != null)
                {
                    indicatorWidth = (int)Style.IndicatorSize.Width;
                    indicatorHeight = (int)Style.IndicatorSize.Height;
                }
                indicatorSpacing = Style.IndicatorSpacing;
                if (indicatorWidth + indicatorSpacing == 0)
                {
                    throw new ArithmeticException("width and space of indictor is 0.");
                }
                count = (int)((this.SizeWidth - returnArrowWidth - nextArrowWidth) / (indicatorWidth + indicatorSpacing));
            }
            return count;
        }

        private void OnSelectChangeEvent(object sender, SelectChangeEventArgs e)
        {
            selectChangeEventHandlers?.Invoke(sender, e);
        }


        /// <summary>
        /// SelectChange Event Arguments.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public class SelectChangeEventArgs : EventArgs
        {
            /// <summary>
            /// Previous select indicator index.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            public int PreviousIndex;

            /// <summary>
            /// Previous select indicator index.
            /// </summary>
            /// <since_tizen> 5.5 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            public int CurrentIndex;
        }
    }
}
