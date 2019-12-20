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
using System.Globalization;
using StyleManager = Tizen.NUI.Components.DA.StyleManager;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// Picker is one kind of Fhub component, a picker allows the user to change date information: year/month/day.
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
    public class Picker : Control
    {
        private ImageView focusImage = null;
        private ImageView endSelectedImage = null;
        private View dateView = null;
        private TextLabel sunText = null;
        private TextLabel monText = null;
        private TextLabel tueText = null;
        private TextLabel wenText = null;
        private TextLabel thuText = null;
        private TextLabel friText = null;
        private TextLabel satText = null;
        private TextLabel[,] dateTable = null;
        private ImageView leftArrowImage = null;
        private ImageView rightArrowImage = null;
        private TextLabel monthText = null;
        private DropDown dropDown = null;
        private TextLabel preTouch = null;
        private DateTime showDate;
        private DateTime curDate;
        private DataArgs data;

        /// <summary>
        /// Creates a new instance of a Picker.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Picker() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a Picker with style.
        /// </summary>
        /// <param name="style">Create Picker by special style defined in UX.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public Picker(string style) : base(style)
        {
            Initialize();
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new PickerStyle Style => ViewStyle as PickerStyle;

        /// <summary>
        /// Current date in Picker.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public DateTime CurDate
        {
            get
            {
                return curDate;
            }
            set
            {
                curDate = value;
                showDate = curDate;
            }
        }

        /// <summary>
        /// Dispose Picker and all children on it.
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
                if (leftArrowImage != null)
                {
                    leftArrowImage.TouchEvent -= OnPreMonthTouchEvent;
                    Utility.Dispose(leftArrowImage);
                }                
                if (rightArrowImage != null)
                {
                    rightArrowImage.TouchEvent -= OnNextMonthTouchEvent;
                    Utility.Dispose(rightArrowImage);
                }                
                if (dropDown != null)
                {
                    Utility.Dispose(dropDown);
                }                
                if (monthText != null)
                {
                    Utility.Dispose(monthText);
                }                
                if (dropDown != null)
                {
                    dropDown.ItemClickEvent -= OnDropDownItemClickEvent;
                    Utility.Dispose(dropDown);
                }
                if (dateView != null)
                {
                    if (focusImage != null)
                    {
                        Utility.Dispose(focusImage);
                    }                    
                    if (endSelectedImage != null)
                    {
                        Utility.Dispose(endSelectedImage);
                    }                    
                    if (sunText != null)
                    {
                        Utility.Dispose(sunText);
                    }                    
                    if (monText != null)
                    {
                        Utility.Dispose(monText);
                    }                    
                    if (tueText != null)
                    {
                        Utility.Dispose(tueText);
                    }
                    if (wenText != null)
                    {
                        Utility.Dispose(wenText);
                    }
                    if (thuText != null)
                    {
                        Utility.Dispose(thuText);
                    }
                    if (friText != null)
                    {
                        Utility.Dispose(friText);
                    }
                    if (satText != null)
                    {
                        Utility.Dispose(satText);
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        for(int j = 0; j < 7; j++)
                        {
                            if (dateTable[i, j] != null)
                            {
                                dateTable[i, j].TouchEvent -= OnDateTouchEvent;
                                Utility.Dispose(dateTable[i, j]);
                            }
                        }
                    }
                    Utility.Dispose(dateView);
                }
            }

            base.Dispose(type);
        }

        /// <summary>
        /// Get Picker attribues.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override ViewStyle GetViewStyle()
        {
            return new PickerStyle();
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
		/// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
		protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            PickerStyle tempAttributes = StyleManager.Instance.GetViewStyle(style) as PickerStyle;
            if (tempAttributes != null)
            {
                Style.CopyFrom(tempAttributes);
                RelayoutRequest();
            }
        }

        /// <summary>
        /// Update Picker by attributes.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void OnUpdate()
        {
            if (Style.YearDropDownItemStyle != null)
            {
                int value = showDate.Year;
                
                for (int i = (int)Style.YearRange.X; i <= (int)Style.YearRange.Y; i++)
                {
                    DropDown.DropDownDataItem item = new DropDown.DropDownDataItem(Style.YearDropDownItemStyle);
                    item.Text = i.ToString();
                    //dropDown.AddItem(item);
                }
                
                //dropDown.FocusedItemIndex = value - (int)Style.YearRange.X;
                //dropDown.SelectedItemIndex = dropDown.FocusedItemIndex;
                //dropDown.Style.Button.Text.Text = showDate.Year.ToString();
            }
                        
            int tableX = 0;
            int tableY = (int)sunText.Size.Height;
            int tableW = (int)dateTable[0, 0].Size.Width;
            int tableH = (int)dateTable[0, 0].Size.Height;
            
            for (int i = 0; i < 6; i++)
            { 
                tableX = 0;                
                for (int j = 0; j < 7; j++)
                {
                    dateTable[i, j].Position = new Position(tableX, tableY );                    
                    if (j % 2 == 0)
                    {
                        tableW = (int)dateTable[0, 0].Size.Width; 
                    }
                    else
                    {
                        tableW = (int)dateTable[0, 1].Size.Width; 
                    }                    
                    tableX += tableW;
                }                
                tableY += tableH;
            }
            UpdateDate();
        }

        private void Initialize()
        {
            LeaveRequired = true;

            if (Style.LeftArrow != null)
            {
                leftArrowImage = new ImageView()
                {
                  WidthResizePolicy = ResizePolicyType.FillToParent,
                  HeightResizePolicy = ResizePolicyType.FillToParent
                };
                leftArrowImage.TouchEvent += OnPreMonthTouchEvent;
                Add(leftArrowImage);
                leftArrowImage.ApplyStyle(Style.LeftArrow);
            }
            if (Style.RightArrow != null)
            {
                rightArrowImage = new ImageView()
                {
                  WidthResizePolicy = ResizePolicyType.FillToParent,
                  HeightResizePolicy = ResizePolicyType.FillToParent
                };
                rightArrowImage.TouchEvent += OnNextMonthTouchEvent;
                Add(rightArrowImage);
                rightArrowImage.ApplyStyle(Style.RightArrow);
            }
            if (Style.MonthText != null)
            {
                monthText = new TextLabel();
                Add(monthText);
                monthText.ApplyStyle(Style.MonthText);
            }
            if (Style.DateView != null)
            {
                dateView = new View();
                Add(dateView);
                dateView.ApplyStyle(Style.DateView);
                if (Style.FocusImage != null)
                {
                    focusImage = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };                    
                    focusImage.Hide();
                    dateView.Add(focusImage);
                    focusImage.ApplyStyle(Style.FocusImage);
                }
                if (Style.EndSelectedImage != null)
                {
                    endSelectedImage = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };                    
                    endSelectedImage.Hide();
                    dateView.Add(endSelectedImage);
                    endSelectedImage.ApplyStyle(Style.EndSelectedImage);
                }                
                if (Style.SundayText != null)
                {
                    sunText = new TextLabel();
                    dateView.Add(sunText);
                    sunText.ApplyStyle(Style.SundayText);
                }                
                if (Style.MondayText != null)
                {
                    monText = new TextLabel();
                    dateView.Add(monText);
                    monText.ApplyStyle(Style.MondayText);
                }                
                if (Style.TuesdayText != null)
                {
                    tueText = new TextLabel();
                    dateView.Add(tueText);
                    tueText.ApplyStyle(Style.TuesdayText);
                }
                if (Style.WensdayText != null)
                {
                    wenText = new TextLabel();
                    dateView.Add(wenText);
                    wenText.ApplyStyle(Style.WensdayText);
                }
                if (Style.ThursdayText != null)
                {
                    thuText = new TextLabel();
                    dateView.Add(thuText);
                    thuText.ApplyStyle(Style.ThursdayText);
                }
                if (Style.FridayText != null)
                {
                    friText = new TextLabel();
                    dateView.Add(friText);
                    friText.ApplyStyle(Style.FridayText);
                }
                if (Style.SaturdayText != null)
                {
                    satText = new TextLabel();
                    dateView.Add(satText);
                    satText.ApplyStyle(Style.SaturdayText);
                }
                dateTable = new TextLabel[6,7];
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        dateTable[i, j] = new TextLabel();
                        dateTable[i, j].Focusable = true;
                        dateTable[i, j].TouchEvent += OnDateTouchEvent;
                        dateView.Add(dateTable[i, j]);
                        if (j % 2 == 0)
                        {
                            dateTable[i, j].ApplyStyle(Style.DateText);
                        }
                        else
                        {
                            dateTable[i, j].ApplyStyle(Style.DateText2);
                        }
                    }
                }                
                data = new DataArgs();
                showDate = DateTime.Now;
                curDate = showDate;                
            }

            if (dropDown == null)
            {
                //dropDown = new DropDown(Style.YearDropDownStyle);
            }
            //dropDown.ItemClickEvent += OnDropDownItemClickEvent;
            //Add(dropDown);
        }

        private void OnDropDownItemClickEvent(object sender, DropDown.ItemClickEventArgs e)
        {
            int year = 0;
            
            if (int.TryParse(e.Text, out year))
            {
                if (year == showDate.Year)
                {
                    return;
                }
                
                int month = showDate.Month;
                
                if (month == curDate.Month && year == curDate.Year)
                {
                    showDate = new DateTime(curDate.Year, curDate.Month, curDate.Day);
                }
                else
                {
                    showDate = new DateTime(year, month, 1);
                }
                
                //dropDown.FocusedItemIndex = dropDown.SelectedItemIndex;
                //dropDown.Style.Button.Text.Text = showDate.Year.ToString();

                UpdateDate();
            }
        }
        
        private bool OnNextMonthTouchEvent(object source, View.TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            
            if (state == PointStateType.Down)
            {
                if (showDate.Month == 12)
                {
                    if (showDate.Year == (int)Style.YearRange.Y)
                    {
                        return false;
                    }
                    else
                    {
                        //dropDown.FocusedItemIndex += 1;
                        //dropDown.SelectedItemIndex = dropDown.FocusedItemIndex;
                        //dropDown.Style.Button.Text.Text = (showDate.Year + 1).ToString();
                    }
                }
                
                int month = (showDate.Month == 12) ? 1 : showDate.Month + 1;
                int year = (showDate.Month == 12) ? showDate.Year + 1 : showDate.Year;
                
                if (month == curDate.Month && year == curDate.Year)
                {
                    showDate = new DateTime(curDate.Year, curDate.Month, curDate.Day);
                }
                else
                {
                    showDate = new DateTime(year, month, 1);
                }
                
                UpdateDate();
            } 
            
            return false;
        }
        
        private bool OnPreMonthTouchEvent(object source, View.TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            
            if (state == PointStateType.Down)
            {
                if (showDate.Month == 1)
                {
                    if (showDate.Year == (int)Style.YearRange.X)
                    {
                        return false;
                    }
                    else
                    {
                        //dropDown.FocusedItemIndex -= 1;
                        //dropDown.SelectedItemIndex = dropDown.FocusedItemIndex;
                        //dropDown.Style.Button.Text.Text = (showDate.Year - 1).ToString();
                    }
                }
                
                int month = (showDate.Month == 1) ? 12 : showDate.Month - 1;
                int year = (showDate.Month == 1) ? showDate.Year - 1 : showDate.Year;
                
                if (month == curDate.Month && year == curDate.Year)
                {
                    showDate = new DateTime(curDate.Year, curDate.Month, curDate.Day);
                }
                else
                {
                    showDate = new DateTime(year, month, 1);
                }
                
                UpdateDate();
            }
            
            return false;
        }

        private bool OnDateTouchEvent(object source, View.TouchEventArgs e)
        {
            TextLabel textLabel = source as TextLabel;           
            int line = (int)((textLabel.Position.Y - dateTable[0, 0].Position.Y) / dateTable[0, 0].Size.Height);
            int i = 0;
            
            for (i = 0; i < 7; i++)
            {
                if (dateTable[line, i].Position.X == textLabel.Position.X)
                {
                     break;
                }
            }

            int index = line * 7 + i;
            
            if (index < data.prenum || index >= (42 - data.nextnum))
            {
                return false;
            }

            if (preTouch != null)
            {
                int X = (int)preTouch.Position.X;
                
                if (X == 0)
                {
                    preTouch.TextColor = Color.Red;
                }
                else
                {
                    preTouch.TextColor = Color.Black;
                }
            }
            
            int focusX = (int)(textLabel.Position.X + (textLabel.Size.Width - focusImage.Size.Width) / 2);
            int focusY = (int)(textLabel.Position.Y + (textLabel.Size.Height - focusImage.Size.Height) / 2);
            
            focusImage.Position = new Position(focusX, focusY);
            focusImage.Show();            
            textLabel.TextColor = Color.White;
            preTouch = textLabel;
            return false;
        }
        
        private void UpdateDate()
        {
            DateTime dateTime = new DateTime(showDate.Year, showDate.Month, 1);
            int weekStart = Convert.ToInt32(dateTime.DayOfWeek);
            int days = DateTime.DaysInMonth(showDate.Year, showDate.Month); 
            int lines = ((days + weekStart) % 7 == 0) ? (days + weekStart) / 7 : ((days + weekStart) / 7 + 1);
            
            dateView.Size = new Size(dateView.Size.Width, dateTable[0, 0].Size.Height*(lines + 1));
            data.curnum = days;
            data.prenum = weekStart;
            data.nextnum = 42 - weekStart - days;

            int[] value = new int[42];
            int idx = 0;
            for (int i = 0; i < data.prenum; i++)
            {
                value[idx++] = 0xFF;
            }
            
            int t = 1;
            for(int i = 0; i < data.curnum; i++)
            {
                value[idx++] = t++;
            }
            
            for (int i = 0; i < data.nextnum; i++)
            {
                value[idx++] = 0xFF;
            }

            for (int i = 0; i < 42; i++)
            {
                int x = i / 7;
                int y = i % 7;
                if (value[i] != 0xFF)
                {
                    dateTable[x, y].Text = value[i].ToString();
                }
                else
                {
                    dateTable[x, y].Text = " ";
                }
            }

            for (int i = data.prenum; i < data.prenum+data.curnum; i++)
            {
                int x = i / 7;
                int y = i % 7;                
                if(y == 0)
                {
                    dateTable[x, y].TextColor = Color.Red;
                }
                else
                {
                    dateTable[x, y].TextColor = Color.Black;
                }
            }

            int focusidx = data.prenum + showDate.Day - 1;
            dateTable[focusidx / 7, focusidx % 7].TextColor = Color.White;

            int focusX = (int)(dateTable[focusidx / 7, focusidx % 7].Position.X + (dateTable[focusidx / 7, focusidx % 7].Size.Width - focusImage.Size.Width) / 2);
            int focusY = (int)(dateTable[focusidx / 7, focusidx % 7].Position.Y + (dateTable[focusidx / 7, focusidx % 7].Size.Height - focusImage.Size.Height) / 2);
            focusImage.Position = new Position(focusX, focusY);
            focusImage.Show();

            if (showDate.Month == curDate.Month && showDate.Year == curDate.Year)
            {
                endSelectedImage.Position = new Position(focusX, focusY);
                endSelectedImage.Show();
                if (showDate.Day == curDate.Day)
                {
                    focusImage.Hide();
                }
            }
            else
            {
                endSelectedImage.Hide();
            }

            preTouch = dateTable[focusidx / 7, focusidx % 7];
            monthText.Text =  showDate.ToString("MMMM", new CultureInfo("en-us"));
        }

        private struct DataArgs
        {
            internal int prenum;
            internal int curnum;
            internal int nextnum;
        }
    }
}
