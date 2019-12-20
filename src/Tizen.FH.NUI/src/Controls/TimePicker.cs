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
using StyleManager = Tizen.NUI.Components.DA.StyleManager;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// TimePicker is one kind of Fhub component, a timePicker allows the user to change time information: hour/minute/second/AMPM.
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.    
    public class TimePicker : Control
    {
        private ImageView colonImage = null;
        private ImageView colonImage2 = null;        
        private Spin hourSpin = null;
        private Spin minuteSpin = null;
        private Spin secondSpin = null;
        private Spin amPmSpin = null;
        private View weekView = null;
        private TextLabel title = null;
        private ImageView[] weekSelectImage = null;
        private TextLabel[] weekText = null;
        private TextLabel weekTitleText = null;
        private bool[] selected = null;

        /// <summary>
        /// Creates a new instance of a TimePicker.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public TimePicker() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance of a TimePicker with style.
        /// </summary>
        /// <param name="style">Create TimePicker by special style defined in UX.</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public TimePicker(string style) : base(style)
        {
            Initialize();
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new TimePickerStyle Style => ViewStyle as TimePickerStyle;

        /// <summary>
        /// Current time in TimePicker.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public DateTime CurrentTime
        {
            get;
            set;
        }

        /// <summary>
        /// Dispose TimePicker and all children on it.
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
                Utility.Dispose(colonImage);
                Utility.Dispose(colonImage2);
                Utility.Dispose(hourSpin);
                Utility.Dispose(minuteSpin);
                Utility.Dispose(secondSpin);
                Utility.Dispose(amPmSpin);
                Utility.Dispose(title);
                Utility.Dispose(weekTitleText);
                if (weekSelectImage != null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        Utility.Dispose(weekSelectImage[i]);
                    }
                }
                if (weekText!= null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        weekText[i].TouchEvent -= OnRepeatTextTouchEvent;
                        Utility.Dispose(weekText[i]);
                    }
                }
                Utility.Dispose(weekView);
            }
            base.Dispose(type);
        }   

        /// <summary>
        /// Get TimePicker attribues.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override ViewStyle GetViewStyle()
        {
            return new TimePickerStyle();
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
		/// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
		protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            TimePickerStyle tempAttributes = StyleManager.Instance.GetViewStyle(style) as TimePickerStyle;
            if (tempAttributes != null)
            {
                Style.CopyFrom(tempAttributes);
                RelayoutRequest();
            }
        }

        /// <summary>
        /// Update TimePicker by attributes.
        /// </summary>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void OnUpdate()
        {           
            int w = 0;
            int h = 0;           
            if (Style.Shadow != null)
            {
                w = (int)(Size.Width + Style.ShadowExtents.Start + Style.ShadowExtents.End);
                h = (int)(Size.Height + Style.ShadowExtents.Top + Style.ShadowExtents.Bottom);
                Style.Shadow.Size = new Size(w, h);
            }

            int x = 0;           
            if (colonImage2 != null)
            {
                x = (int)(minuteSpin.Position.X + minuteSpin.Size.Width + (minuteSpin.Position.X - hourSpin.Position.X - hourSpin.Size.Width - colonImage.Size.Width)/2);
                colonImage2.Position = new Position(x, colonImage2.Position.Y);
            }
            x = (int)(hourSpin.Position.X + hourSpin.Size.Width + (minuteSpin.Position.X - hourSpin.Position.X - hourSpin.Size.Width - colonImage.Size.Width)/2);
            colonImage.Position = new Position(x, colonImage.Position.Y);

            if (weekView != null)
            {
                if ((weekText != null) && (weekSelectImage != null))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        weekText[i].Position = new Position(i * weekText[i].Size.Width, weekText[i].Position.Y);
                        weekSelectImage[i].Position = new Position(i * weekText[i].Size.Width + (weekText[i].Size.Width -weekSelectImage[i].Size.Width)/2, weekSelectImage[i].Position.Y);
                    }
                    weekText[0].Text = "Sun";
                    weekText[0].TextColor = Color.Red;
                    weekText[1].Text = "Mon";
                    weekText[2].Text = "Tue";
                    weekText[3].Text = "Wen";
                    weekText[4].Text = "Thu";
                    weekText[5].Text = "Fri";
                    weekText[6].Text = "Sat";
                }                
            }
            if (amPmSpin != null)
            {
                hourSpin.MaxValue = 11;
                hourSpin.MinValue = 0;
                amPmSpin.MaxValue = 1;
                amPmSpin.MinValue = 0;
            }
            else
            {
                hourSpin.MaxValue = 23;
                hourSpin.MinValue = 0;
            }
            if (minuteSpin !=null)
            {
                minuteSpin.MaxValue = 59;
                minuteSpin.MinValue = 0;
            }
            if (secondSpin !=null)
            {
                secondSpin.MaxValue = 59;
                secondSpin.MinValue = 0;
            }
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public override void ApplyStyle(ViewStyle viewStyle)
        {
            base.ApplyStyle(viewStyle);

            TimePickerStyle timePickerStyle = viewStyle as TimePickerStyle;
            if (colonImage == null)
            {
                colonImage = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                Add(colonImage);
                colonImage.ApplyStyle(timePickerStyle.ColonImage);
            }
            if (colonImage2 == null)
            {
                colonImage2 = new ImageView()
                {
                    WidthResizePolicy = ResizePolicyType.FillToParent,
                    HeightResizePolicy = ResizePolicyType.FillToParent
                };
                Add(colonImage2);
                colonImage2.ApplyStyle(timePickerStyle.ColonImage);
            }
            if (timePickerStyle.Title != null && title == null)
            {
                title = new TextLabel();
                Add(title);
                title.ApplyStyle(timePickerStyle.Title);
            }
            if (timePickerStyle.WeekView != null && weekView == null)
            {
                weekView = new View();
                Add(weekView);
                weekView.ApplyStyle(timePickerStyle.WeekView);
            }
            if (timePickerStyle.WeekSelectImage != null && weekSelectImage == null)
            {
                weekSelectImage = new ImageView[7];
                selected = new bool[7];
                for (int i = 0; i < 7; i++)
                {
                    weekSelectImage[i] = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent
                    };
                    weekSelectImage[i].Hide();
                    weekView.Add(weekSelectImage[i]);
                    selected[i] = false;
                    weekSelectImage[i].ApplyStyle(timePickerStyle.WeekSelectImage);
                }
            }
            if (timePickerStyle.WeekText != null && weekText == null)
            {
                weekText = new TextLabel[7];
                for (int i = 0; i < 7; i++)
                {
                    weekText[i] = new TextLabel();
                    weekText[i].TouchEvent += OnRepeatTextTouchEvent;
                    weekView.Add(weekText[i]);
                    weekText[i].ApplyStyle(timePickerStyle.WeekText);
                }
            }
            if (timePickerStyle.WeekTitleText != null && weekTitleText == null)
            {
                weekTitleText = new TextLabel();
                weekView.Add(weekTitleText);
                weekTitleText.ApplyStyle(timePickerStyle.WeekTitleText);
            }
        }

        private void Initialize()
        {
            LeaveRequired = true;
            CurrentTime = DateTime.Now;

            hourSpin = new Spin("DASpin");
            hourSpin.NameText = "Hours";
            if (Style.AmPmSpin != null)
            {
                hourSpin.CurrentValue = CurrentTime.Hour % 12;
            }
            else
            {
                hourSpin.CurrentValue = CurrentTime.Hour % 24;
            }
            Add(hourSpin);
            hourSpin.ParentOrigin = Style.HourSpin.ParentOrigin;
            hourSpin.PivotPoint = Style.HourSpin.PivotPoint;
            hourSpin.PositionUsesPivotPoint = (bool)Style.HourSpin.PositionUsesPivotPoint;
            hourSpin.Size = Style.HourSpin.Size;
            hourSpin.Position = Style.HourSpin.Position;

            minuteSpin = new Spin("DASpin");
            minuteSpin.NameText = "Minutes";
            minuteSpin.CurrentValue = CurrentTime.Minute;
            Add(minuteSpin);
            minuteSpin.ParentOrigin = Style.MinuteSpin.ParentOrigin;
            minuteSpin.PivotPoint = Style.MinuteSpin.PivotPoint;
            minuteSpin.PositionUsesPivotPoint = (bool)Style.MinuteSpin.PositionUsesPivotPoint;
            minuteSpin.Size = Style.MinuteSpin.Size;
            minuteSpin.Position = Style.MinuteSpin.Position;

            if (Style.SecondSpin != null)
            {
                secondSpin = new Spin("DASpin");
                secondSpin.NameText = "Seconds";
                secondSpin.CurrentValue = CurrentTime.Second;
                Add(secondSpin);
                secondSpin.ParentOrigin = Style.SecondSpin.ParentOrigin;
                secondSpin.PivotPoint = Style.SecondSpin.PivotPoint;
                secondSpin.PositionUsesPivotPoint = (bool)Style.SecondSpin.PositionUsesPivotPoint;
                secondSpin.Size = Style.SecondSpin.Size;
                secondSpin.Position = Style.SecondSpin.Position;
            }
            if (Style.AmPmSpin != null)
            {
                amPmSpin = new Spin("DAStrSpin");
                amPmSpin.CurrentValue = CurrentTime.Hour / 12;
                Add(amPmSpin);
                amPmSpin.ParentOrigin = Style.AmPmSpin.ParentOrigin;
                amPmSpin.PivotPoint = Style.AmPmSpin.PivotPoint;
                amPmSpin.PositionUsesPivotPoint = (bool)Style.AmPmSpin.PositionUsesPivotPoint;
                amPmSpin.Size = Style.AmPmSpin.Size;
                amPmSpin.Position = Style.AmPmSpin.Position;
            }
        }

        private bool OnRepeatTextTouchEvent(object source, View.TouchEventArgs e)
        {
            TextLabel textLabel = source as TextLabel;
            PointStateType state = e.Touch.GetState(0);
            
            if (state == PointStateType.Down)
            {
                int i = 0;                
                for (i = 0; i < 7; i++)
                {
                    if (weekText[i] == textLabel)
                    {
                        break;
                    }
                }                
                if (selected[i] == false)
                {
                    selected[i] = true;
                    weekSelectImage[i].Show();
                }
                else
                {
                    selected[i] = false;
                    weekSelectImage[i].Hide();
                }
            }
            
            return false;
        }
    }    
}

