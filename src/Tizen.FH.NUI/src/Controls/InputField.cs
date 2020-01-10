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
using System.ComponentModel;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;
using StyleManager = Tizen.NUI.Components.DA.StyleManager;

namespace Tizen.FH.NUI.Components
{
    /// <summary>
    /// InputField is a editable input compoment with delete button or delete and add button.
    /// After pressing Return key, search button will show
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.    
    public class InputField : Tizen.NUI.Components.DA.Control
    {
        private TextField textField = null;
        // the cancel button
        private ImageView cancelBtn = null;
        // the delete button
        private ImageView deleteBtn = null;
        // the add button background image
        private ImageView addBtnBg = null;
        // the add button overlay image
        private ImageView addBtnOverlay = null;
        // the add button foreground image
        private ImageView addBtnFg = null;
        // the search button
        private ImageView searchBtn = null;
        
        private InputStyle inputStyle = InputStyle.None;

        private ControlStates textFieldState = ControlStates.Normal;
        private TextState textState = TextState.Guide;
        private bool isDoneKeyPressed = false;

        private EventHandler<ButtonClickArgs> cancelBtnClickHandler;
        private EventHandler<ButtonClickArgs> deleteBtnClickHandler;
        private EventHandler<ButtonClickArgs> addBtnClickHandler;
        private EventHandler<ButtonClickArgs> searchBtnClickHandler;
        private EventHandler<KeyEventArgs> keyEventHandler;

        /// <summary>
        /// Initializes a new instance of the InputField class.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public InputField() : base()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the InputField class.
        /// </summary>
        /// <param name="style">Create Header by special style defined in UX.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public InputField(string style) : base(style)
        {
            Initialize();
        }

        /// <summary>
        /// Click Event attached to Cancel Button.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public event EventHandler<ButtonClickArgs> CancelButtonClickEvent
        {
            add
            {
                cancelBtnClickHandler += value;
            }
            remove
            {
                cancelBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Delete Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public event EventHandler<ButtonClickArgs> DeleteButtonClickEvent
        {
            add
            {
                deleteBtnClickHandler += value;
            }
            remove
            {
                deleteBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Add Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public event EventHandler<ButtonClickArgs> AddButtonClickEvent
        {
            add
            {
                addBtnClickHandler += value;
            }
            remove
            {
                addBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// Click Event attached to Search Button
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public event EventHandler<ButtonClickArgs> SearchButtonClickEvent
        {
            add
            {
                searchBtnClickHandler += value;
            }
            remove
            {
                searchBtnClickHandler -= value;
            }
        }

        /// <summary>
        /// The handler Event of Key
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public new event EventHandler<KeyEventArgs> KeyEvent
        {
            add
            {
                keyEventHandler += value;
            }
            remove
            {
                keyEventHandler -= value;
            }
        }

        /// <summary>
        /// The  state of Button Click
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public enum ButtonClickState
        {
            /// <summary> Press down </summary>
            /// <since_tizen> 6 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            PressDown,
            /// <summary> Bounce up </summary>
            /// <since_tizen> 6 </since_tizen>
            /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.            
            BounceUp
        }

        private enum TextState
        {
            Guide,
            Input,
        }

        private enum InputStyle
        {
            None,
            Default,
            StyleB,
            SearchBar
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        public new InputFieldStyle Style => ViewStyle as InputFieldStyle;

        /// <summary>
        /// Set the status of the Input Field editable or not.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        public bool StateEnabled
        {
            get
            {
                return Sensitive;
            }
            set
            {
                if (Sensitive == value)
                {
                    return;
                }
                UpdateComponentsByStateEnabledChanged(value);
                Sensitive = value;
            }
        }

        /// <summary>
        /// Gets or sets the property for the text content.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Text
        {
            get
            {
                return textField?.Text;
            }
            set
            {
                if (null != textField) textField.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the property for the hint text.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string HintText
        {
            get
            {
                return textField?.PlaceholderText;
            }
            set
            {
                if (null != textField) textField.PlaceholderText = value;
            }
        }

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void ApplyStyle(ViewStyle viewStyle)
        {
            base.ApplyStyle(viewStyle);

            InputFieldStyle inputFieldStyle = viewStyle as InputFieldStyle;
            if (null != inputFieldStyle.InputBox)
            {
                if (null == textField)
                {
                    textField = new TextField()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                        PositionUsesPivotPoint = true,
                    };
                    this.Add(textField);
                    textField.FocusGained += OnTextFieldFocusGained;
                    textField.FocusLost += OnTextFieldFocusLost;
                    textField.TextChanged += OnTextFieldTextChanged;
                    textField.KeyEvent += OnTextFieldKeyEvent;
                }
                textField.ApplyStyle(inputFieldStyle.InputBox);
            }

            if (null != inputFieldStyle?.CancelButton)
            {
                if (null == cancelBtn)
                {
                    cancelBtn = new ImageView()
                    {
                        //WidthResizePolicy = ResizePolicyType.FillToParent,
                        //HeightResizePolicy = ResizePolicyType.FillToParent,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(cancelBtn);
                    cancelBtn.TouchEvent += OnCancelBtnTouchEvent;
                }
                cancelBtn.ApplyStyle(inputFieldStyle.CancelButton);
            }
            if (null != inputFieldStyle.DeleteButton)
            {
                if (null == deleteBtn)
                {
                    deleteBtn = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(deleteBtn);
                    deleteBtn.TouchEvent += OnDeleteBtnTouchEvent;
                }
                deleteBtn.ApplyStyle(inputFieldStyle.DeleteButton);
            }
            if (null != inputFieldStyle.SearchButton)
            {
                if (null == searchBtn)
                {
                    searchBtn = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(searchBtn);
                    searchBtn.TouchEvent += OnSearchBtnTouchEvent;
                }
                searchBtn.ApplyStyle(inputFieldStyle.SearchButton);
            }
            if (null != inputFieldStyle.AddButtonBackground)
            {
                if (null == addBtnBg)
                {
                    addBtnBg = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(addBtnBg);
                }
                addBtnBg.ApplyStyle(inputFieldStyle.AddButtonBackground);
            }
            if (null != inputFieldStyle.AddButtonOverlay)
            {
                if (null == addBtnOverlay)
                {
                    addBtnOverlay = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                    };
                    addBtnBg.Add(addBtnOverlay);
                }
                addBtnOverlay.ApplyStyle(inputFieldStyle.AddButtonOverlay);
            }
            if (null != inputFieldStyle.AddButtonForeground)
            {
                if (null == addBtnFg)
                {
                    addBtnFg = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                    };
                    addBtnOverlay.Add(addBtnFg);
                    addBtnFg.TouchEvent += OnAddBtnTouchEvent;
                }
                addBtnFg.ApplyStyle(inputFieldStyle.AddButtonForeground);
            }

            if (cancelBtn.ResourceUrl != "")
            {
                if (searchBtn.ResourceUrl == "")
                {
                    inputStyle = InputStyle.Default;
                }
                else
                {
                    inputStyle = InputStyle.SearchBar;
                }
            }
            else
            {
                if (deleteBtn.ResourceUrl != "" && addBtnBg.ResourceUrl != "" && addBtnOverlay.ResourceUrl != "" && addBtnFg.ResourceUrl != "")
                {
                    inputStyle = InputStyle.StyleB;
                }
            }
        }

        /// <summary>
        /// Dispose Input Field and all children on it.
        /// </summary>
        /// <param name="type">Dispose type.</param>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                if (cancelBtn != null)
                {
                    cancelBtn.TouchEvent -= OnCancelBtnTouchEvent;
                    Utility.Dispose(cancelBtn);
                }
                if (deleteBtn != null)
                {
                    deleteBtn.TouchEvent -= OnDeleteBtnTouchEvent;
                    Utility.Dispose(deleteBtn);
                }
                if (searchBtn != null)
                {
                    searchBtn.TouchEvent -= OnSearchBtnTouchEvent;
                    Utility.Dispose(searchBtn);
                }
                if (addBtnFg != null)
                {
                    addBtnFg.TouchEvent -= OnAddBtnTouchEvent;
                    Utility.Dispose(addBtnFg);
                }
                Utility.Dispose(addBtnOverlay);
                Utility.Dispose(addBtnBg);
            }
            base.Dispose(type);
        }

        /// <summary>
        /// Get Input Field attribues.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override ViewStyle GetViewStyle()
        {
            return new InputFieldStyle();
        }

        /// <summary>
        /// Update Input Field by attributes.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void OnUpdate()
        {
            //RelayoutTextField(false);
            base.OnUpdate();     
            RelayoutComponents();
            UpdateComponentsByStateEnabledChanged(Sensitive);
            OnLayoutDirectionChanged();
        }

        /// <summary>
        /// Theme change callback when theme is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected override void OnThemeChangedEvent(object sender, StyleManager.ThemeChangeEventArgs e)
        {
            InputFieldStyle tempAttributes = StyleManager.Instance.GetViewStyle(base.style) as InputFieldStyle;
            if (tempAttributes != null)
            {
                Style.CopyFrom(tempAttributes);
                RelayoutRequest();
            }
        }

        /// <summary>
        ///  Text field focus gain callback when focus is getted, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected void OnTextFieldFocusGained(object source, EventArgs e)
        {
            // when press on TextField, it will gain focus
            textFieldState = ControlStates.Selected;
            RelayoutComponents(false, true, true, false);
        }

        /// <summary>
        /// Text field lost gain  callback when focus is lost, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected void OnTextFieldFocusLost(object source, EventArgs e)
        {
            textFieldState = ControlStates.Normal;
            RelayoutComponents(false, true, true, false);
        }

        /// <summary>
        /// Text field change callback when text  is changed, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected void OnTextFieldTextChanged(object sender, TextField.TextChangedEventArgs e)
        {
            if (sender is TextField)
            {
                TextField textField = sender as TextField;
                int textLen = textField.Text.Length;
                if (textLen == 0)
                {
                    textState = TextState.Guide;
                }
                else
                {
                    textState = TextState.Input;
                }
                isDoneKeyPressed = false;
                RelayoutComponents(false, true, true, false);
            }
        }

        /// <summary>
        /// Text field key callback when "Return"  click down, this callback will be trigger.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.        
        protected bool OnTextFieldKeyEvent(object source, KeyEventArgs e)
        {
            keyEventHandler?.Invoke(this, e);

            if (e.Key.State == Key.StateType.Down)
            {
                if (e.Key.KeyPressedName == "Return")
                {
                    // when press "Return" key("Done" key in IME), the searchBtn should show.
                    isDoneKeyPressed = true;
                    RelayoutComponents(false, false, true, false);
                    return true;
                }
            }
            return false;
        }

        private void Initialize()
        {
            if (null != Style.CancelButton)
            {
                if (null == cancelBtn)
                {
                    cancelBtn = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(cancelBtn);
                    cancelBtn.TouchEvent += OnCancelBtnTouchEvent;
                }
            }
            if (null != Style.DeleteButton)
            {
                if (null == deleteBtn)
                {
                    deleteBtn = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(deleteBtn);
                    deleteBtn.TouchEvent += OnDeleteBtnTouchEvent;
                }
            }
            if (null != Style.SearchButton)
            {
                if (null == searchBtn)
                {
                    searchBtn = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(searchBtn);
                    searchBtn.TouchEvent += OnSearchBtnTouchEvent;
                }
            }
            if (null != Style.AddButtonBackground)
            {
                if (null == addBtnBg)
                {
                    addBtnBg = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.Fixed,
                        HeightResizePolicy = ResizePolicyType.Fixed,
                        ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight,
                        PivotPoint = Tizen.NUI.PivotPoint.CenterRight,
                        PositionUsesPivotPoint = true
                    };
                    this.Add(addBtnBg);
                }
            }
            if (null != Style.AddButtonOverlay)
            {
                if (null == addBtnOverlay)
                {
                    addBtnOverlay = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                    };
                    addBtnBg.Add(addBtnOverlay);
                }
            }
            if (null != Style.AddButtonForeground)
            {
                if (null == addBtnFg)
                {
                    addBtnFg = new ImageView()
                    {
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                    };
                    addBtnOverlay.Add(addBtnFg);
                    addBtnFg.TouchEvent += OnAddBtnTouchEvent;
                }
            }

            if (cancelBtn.ResourceUrl != "")
            {
                if (searchBtn.ResourceUrl == "")
                {
                    inputStyle = InputStyle.Default;
                }
                else
                {
                    inputStyle = InputStyle.SearchBar;
                }
            }
            else
            {
                if (deleteBtn.ResourceUrl != "" && addBtnBg.ResourceUrl != "" && addBtnOverlay.ResourceUrl != "" && addBtnFg.ResourceUrl != "")
                {
                    inputStyle = InputStyle.StyleB;
                }
            }

            if (null == textField)
            {
                textField = new TextField()
                {
                    WidthResizePolicy = ResizePolicyType.Fixed,
                    HeightResizePolicy = ResizePolicyType.Fixed,
                    ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft,
                    PivotPoint = Tizen.NUI.PivotPoint.CenterLeft,
                    PositionUsesPivotPoint = true,
                };
                this.Add(textField);
                textField.FocusGained += OnTextFieldFocusGained;
                textField.FocusLost += OnTextFieldFocusLost;
                textField.TextChanged += OnTextFieldTextChanged;
                textField.KeyEvent += OnTextFieldKeyEvent;
            }
        }

        private void OnLayoutDirectionChanged()
        {
            if (Style == null) return;

            if (LayoutDirection == ViewLayoutDirectionType.LTR)
            {
                if (cancelBtn)
                {
                    if (Style.CancelButton != null)
                    {
                        Style.CancelButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.CancelButton.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.CancelButton.PositionUsesPivotPoint = true;
                    }
                    cancelBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    cancelBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    cancelBtn.PositionUsesPivotPoint = true;
                }
                if(addBtnBg)
                {
                    if (Style.AddButtonBackground != null)
                    {
                        Style.AddButtonBackground.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.AddButtonBackground.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.AddButtonBackground.PositionUsesPivotPoint = true;
                    }
                    addBtnBg.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    addBtnBg.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    addBtnBg.PositionUsesPivotPoint = true;
                }
                if(searchBtn)
                {
                    if (Style.SearchButton != null)
                    {
                        Style.SearchButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.SearchButton.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.SearchButton.PositionUsesPivotPoint = true;
                    }
                    searchBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    searchBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    searchBtn.PositionUsesPivotPoint = true;
                }
                if(deleteBtn)
                {
                    if (Style.DeleteButton != null)
                    {
                        Style.DeleteButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.DeleteButton.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.DeleteButton.PositionUsesPivotPoint = true;
                    }
                    deleteBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    deleteBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    deleteBtn.PositionUsesPivotPoint = true;
                }
                if(textField)
                {
                    textField.HorizontalAlignment = HorizontalAlignment.Begin;
                    textField.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    textField.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    textField.PositionUsesPivotPoint = true;
                }
            }
            else
            {
                if (cancelBtn)
                {
                    if (Style.AddButtonBackground != null)
                    {
                        Style.AddButtonBackground.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.AddButtonBackground.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.AddButtonBackground.PositionUsesPivotPoint = true;
                    }
                    cancelBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    cancelBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    cancelBtn.PositionUsesPivotPoint = true;
                }
                if (addBtnBg)
                {
                    if (Style.AddButtonBackground != null)
                    {
                        Style.AddButtonBackground.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.AddButtonBackground.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.AddButtonBackground.PositionUsesPivotPoint = true;
                    }
                    addBtnBg.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    addBtnBg.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    addBtnBg.PositionUsesPivotPoint = true;
                }
                if (searchBtn)
                {
                    if (Style.SearchButton != null)
                    {
                        Style.SearchButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                        Style.SearchButton.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                        Style.SearchButton.PositionUsesPivotPoint = true;
                    }
                    searchBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    searchBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    searchBtn.PositionUsesPivotPoint = true;
                }
                if (deleteBtn)
                {
                    if (Style.DeleteButton != null)
                    {
                        Style.DeleteButton.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                        Style.DeleteButton.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                        Style.DeleteButton.PositionUsesPivotPoint = true;
                    }
                    deleteBtn.ParentOrigin = Tizen.NUI.ParentOrigin.CenterLeft;
                    deleteBtn.PivotPoint = Tizen.NUI.PivotPoint.CenterLeft;
                    deleteBtn.PositionUsesPivotPoint = true;
                }
                if(textField)
                {
                    textField.HorizontalAlignment = HorizontalAlignment.End;
                    textField.ParentOrigin = Tizen.NUI.ParentOrigin.CenterRight;
                    textField.PivotPoint = Tizen.NUI.PivotPoint.CenterRight;
                    textField.PositionUsesPivotPoint = true;
                }
            }
        }

        private void RelayoutComponents(bool shouldUpdate = true, bool enableRelayoutDefault = true,
            bool enableRelayoutSearchBar = true, bool enableRelayoutStyleB = true)
        {
            switch(inputStyle)
            {
                case InputStyle.Default:
                    if (enableRelayoutDefault)
                    {
                        RelayoutComponentsForDefault(shouldUpdate);
                    }
                    break;
                case InputStyle.SearchBar:
                    if (enableRelayoutSearchBar)
                    {
                        RelayoutComponentsForSearchBar(shouldUpdate);
                    }
                    break;
                case InputStyle.StyleB:
                    if (enableRelayoutStyleB)
                    {
                        RelayoutComponentsForStyleB(shouldUpdate);
                    }
                    break;
                default:
                    break;
            }
        }

        private void RelayoutComponentsForDefault(bool shouldUpdate)
        {
            if (null == cancelBtn || null == textField)
            {
                return;
            }
            // 2 type layouts:
            // #1 TextField                 normal state, text's length == 0;
            // #2 TextField + CancelBtn     except #1.
            int space = Style.Space ?? 0;
            if (textFieldState == ControlStates.Normal && textState == TextState.Guide)
            {
                //SetTextFieldSize2D((int)Size.Width - space * 2, (int)Size.Height);
                textField.Size2D = new Size2D((int)Size.Width - space * 2, (int)Size.Height);
                cancelBtn.Hide();
            }
            else
            {
                //SetTextFieldSize2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - SpaceBetweenTextFieldAndRightButton()), (int)Size.Height);
                textField.Size2D = new Size2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - SpaceBetweenTextFieldAndRightButton()), (int)Size.Height);
                cancelBtn.Show();
            }
            if (shouldUpdate)
            {
                if(this.LayoutDirection == ViewLayoutDirectionType.RTL)
                {
                    //SetTextFieldPosX(-space);
                    textField.PositionX = -space;
                    cancelBtn.PositionX = space;
                }
                else
                {
                    //SetTextFieldPosX(space);
                    textField.PositionX = space;
                    cancelBtn.PositionX = -space;
                }
            }
        }

        private void RelayoutComponentsForSearchBar(bool shouldUpdate)
        {
            if (null == searchBtn || null == cancelBtn || null == textField)
            {
                return;
            }
            // 3 type layouts:
            // #1 SearchBtn + TextField                 normal state, text's length == 0;
            // #2 SearchBtn + TextField + CancelBtn     input state, text's length > 0, press "Done" key on IME;
            // #3 TextField + CancelBtn                 excepte #1 & #2.
            int space = Style.Space ?? 0;
            int textfieldX = 0;
            if (textFieldState == ControlStates.Normal && textState == TextState.Guide)
            {// #1
                int spaceBetweenTextFieldAndLeftButton = SpaceBetweenTextFieldAndLeftButton();
                //SetTextFieldSize2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton), (int)Size.Height);
                textField.Size2D = new Size2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton), (int)Size.Height);

                textfieldX = (int)(space + searchBtn.Size.Width + spaceBetweenTextFieldAndLeftButton);
                searchBtn.Show();
                cancelBtn.Hide();
            }
            else if (textFieldState == ControlStates.Selected && textState == TextState.Input && isDoneKeyPressed)
            {// #2
                int spaceBetweenTextFieldAndLeftButton = SpaceBetweenTextFieldAndLeftButton();
                int spaceBetweenTextFieldAndRightButton = SpaceBetweenTextFieldAndRightButton();
                //SetTextFieldSize2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton - cancelBtn.Size.Width - spaceBetweenTextFieldAndRightButton), (int)Size.Height);
                textField.Size2D = new Size2D((int)(Size.Width - space * 2 - searchBtn.Size.Width - spaceBetweenTextFieldAndLeftButton - cancelBtn.Size.Width - spaceBetweenTextFieldAndRightButton), (int)Size.Height);

                textfieldX = (int)(space + searchBtn.Size.Width + spaceBetweenTextFieldAndLeftButton);
                searchBtn.Show();
                cancelBtn.Show();
            }
            else
            {// #3
                int spaceBetweenTextFieldAndRighttButton = SpaceBetweenTextFieldAndRightButton();
                //SetTextFieldSize2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - spaceBetweenTextFieldAndRighttButton), (int)Size.Height);
                textField.Size2D = new Size2D((int)(Size.Width - space * 2 - cancelBtn.Size.Width - spaceBetweenTextFieldAndRighttButton), (int)Size.Height);

                textfieldX = space;
                searchBtn.Hide();
                cancelBtn.Show();
            }

            if (this.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                if (shouldUpdate)
                {
                    searchBtn.PositionX = -space;
                    cancelBtn.PositionX = space;
                }
                //SetTextFieldPosX(-textfieldX);
                textField.PositionX = -textfieldX;
            }
            else
            {
                if (shouldUpdate)
                {
                    searchBtn.PositionX = space;
                    cancelBtn.PositionX = -space;
                }
                //SetTextFieldPosX(textfieldX);
                textField.PositionX = textfieldX;
            }
        }

        private void RelayoutComponentsForStyleB(bool shouldUpdate)
        {
            if (null == addBtnBg || null == deleteBtn || null == textField)
            {
                return;
            }
            if (!shouldUpdate)
            {
                return;
            }
            int space = Style.Space ?? 0;
            int spaceBetweenTextFieldAndRightButton = SpaceBetweenTextFieldAndRightButton();
            //SetTextFieldSize2D((int)(Size.Width - space - spaceBetweenTextFieldAndRightButton - deleteBtn.Size.Width - addBtnBg.Size.Width), (int)Size.Height);
            textField.Size2D = new Size2D((int)(Size.Width - space - spaceBetweenTextFieldAndRightButton - deleteBtn.Size.Width - addBtnBg.Size.Width), (int)Size.Height);

            if (this.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                //SetTextFieldPosX(-space);

                textField.PositionX = -space;
                addBtnBg.PositionX = 0;
                deleteBtn.PositionX = addBtnBg.Size.Width;
            }
            else
            {
                //SetTextFieldPosX(space);
                textField.PositionX = space;
                addBtnBg.PositionX = 0;
                deleteBtn.PositionX = -addBtnBg.Size.Width;
            }
        }

        private int SpaceBetweenTextFieldAndRightButton()
        {
            int space = 0;
            if (Style != null && Style.SpaceBetweenTextFieldAndRightButton != null)
            {
                space = Style.SpaceBetweenTextFieldAndRightButton.Value;
            }
            return space;
        }

        private int SpaceBetweenTextFieldAndLeftButton()
        {
            int space = 0;
            if (Style != null && Style.SpaceBetweenTextFieldAndLeftButton != null)
            {
                space = Style.SpaceBetweenTextFieldAndLeftButton.Value;
            }
            return space;
        }
        
        private void UpdateComponentsByStateEnabledChanged(bool isEnabled)
        {
            if (isEnabled)
            {
                UpdateTextFieldTextColor(ControlStates.Selected);
                UpdateDeleteBtnState(ControlStates.Normal);
                UpdateAddBtnState(ControlStates.Normal);
            }
            else
            {
                UpdateTextFieldTextColor(ControlStates.Disabled);
                UpdateDeleteBtnState(ControlStates.Disabled);
                UpdateAddBtnState(ControlStates.Disabled);
            }
        }
        
        private void UpdateTextFieldTextColor(ControlStates state)
        {
            if (null != Style && null != Style.InputBox
                && null != Style.InputBox.TextColor && null != textField)
            {
                switch (state)
                {
                    case ControlStates.Disabled:
                    case ControlStates.DisabledSelected:
                        //SetTextFieldTextColor(Style.InputBoxAttributes.TextColor.Disabled);
                        textField.TextColor = Style.InputBox.TextColor.Disabled;
                        break;
                    case ControlStates.Normal:
                    case ControlStates.Selected:
                        //SetTextFieldTextColor(Style.InputBoxAttributes.TextColor.Normal);
                        textField.TextColor = Style.InputBox.TextColor.Normal;
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateDeleteBtnState(ControlStates state)
        {
            if (deleteBtn != null && Style != null && Style.DeleteButton != null
                && Style.DeleteButton.ResourceUrl != null)
            {
                switch (state)
                {
                    case ControlStates.Disabled:
                    case ControlStates.DisabledSelected:
                        deleteBtn.ResourceUrl = Style.DeleteButton.ResourceUrl.Disabled;
                        break;
                    case ControlStates.Selected:
                        deleteBtn.ResourceUrl = Style.DeleteButton.ResourceUrl.Pressed;
                        break;
                    case ControlStates.Normal:
                        deleteBtn.ResourceUrl = Style.DeleteButton.ResourceUrl.Normal;
                        break;
                    default:
                        break;
                }
            }
        }

        private void UpdateAddBtnState(ControlStates state)
        {
            if (Style == null || addBtnBg == null || addBtnOverlay == null || addBtnFg == null)
            {
                return;
            }
            switch (state)
            {
                case ControlStates.Disabled:
                case ControlStates.DisabledSelected:
                    {
                        if (Style.AddButtonBackground != null
                            && Style.AddButtonBackground.ResourceUrl != null)
                        {
                            addBtnBg.ResourceUrl = Style.AddButtonBackground.ResourceUrl.Disabled;
                        }
                        if (Style.AddButtonOverlay != null
                            && Style.AddButtonOverlay.ResourceUrl != null)
                        {
                            addBtnOverlay.ResourceUrl = Style.AddButtonOverlay.ResourceUrl.Disabled;
                        }
                        if (Style.AddButtonForeground != null
                            && Style.AddButtonForeground.ResourceUrl != null)
                        {
                            addBtnFg.ResourceUrl = Style.AddButtonForeground.ResourceUrl.Disabled;
                        }
                    }
                    break;
                case ControlStates.Selected:
                    {
                        if (Style.AddButtonBackground != null
                            && Style.AddButtonBackground.ResourceUrl != null)
                        {
                            addBtnBg.ResourceUrl = Style.AddButtonBackground.ResourceUrl.Pressed;
                        }
                        if (Style.AddButtonOverlay != null
                            && Style.AddButtonOverlay.ResourceUrl != null)
                        {
                            addBtnOverlay.ResourceUrl = Style.AddButtonOverlay.ResourceUrl.Pressed;
                        }
                        if (Style.AddButtonForeground != null
                            && Style.AddButtonForeground.ResourceUrl != null)
                        {
                            addBtnFg.ResourceUrl = Style.AddButtonForeground.ResourceUrl.Pressed;
                        }
                    }
                    break;
                case ControlStates.Normal:
                    {
                        if (Style.AddButtonBackground != null
                            && Style.AddButtonBackground.ResourceUrl != null)
                        {
                            addBtnBg.ResourceUrl = Style.AddButtonBackground.ResourceUrl.Normal;
                        }
                        if (Style.AddButtonOverlay != null
                            && Style.AddButtonOverlay.ResourceUrl != null)
                        {
                            addBtnOverlay.ResourceUrl = Style.AddButtonOverlay.ResourceUrl.Normal;
                        }
                        if (Style.AddButtonForeground != null
                            && Style.AddButtonForeground.ResourceUrl != null)
                        {
                            addBtnFg.ResourceUrl = Style.AddButtonForeground.ResourceUrl.Normal;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private bool OnDeleteBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (deleteBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    deleteBtnClickHandler(this, args);
                }
                UpdateDeleteBtnState(ControlStates.Selected);
            }
            else if (state == PointStateType.Finished)
            {
                if (deleteBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    deleteBtnClickHandler(this, args);
                }
                UpdateDeleteBtnState(ControlStates.Normal);
            }
            return true;
        }

        private bool OnSearchBtnTouchEvent(object source, TouchEventArgs e)
        {
            if (textState == TextState.Guide)
            {
                return true;
            }
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (searchBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    searchBtnClickHandler(this, args);
                }
            }
            else if (state == PointStateType.Finished)
            {
                if (searchBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    searchBtnClickHandler(this, args);
                }
            }
            return true;
        }

        private bool OnAddBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (addBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    addBtnClickHandler(this, args);
                }
                UpdateAddBtnState(ControlStates.Selected);
            }
            else if (state == PointStateType.Finished)
            {
                if (addBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    addBtnClickHandler(this, args);
                }
                UpdateAddBtnState(ControlStates.Normal);
            }
            return true;
        }

        private bool OnCancelBtnTouchEvent(object source, TouchEventArgs e)
        {
            PointStateType state = e.Touch.GetState(0);
            if (state == PointStateType.Down)
            {
                if (cancelBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.PressDown;
                    cancelBtnClickHandler(this, args);
                }
            }
            else if (state == PointStateType.Finished)
            {
                if (cancelBtnClickHandler != null)
                {
                    ButtonClickArgs args = new ButtonClickArgs();
                    args.State = ButtonClickState.BounceUp;
                    cancelBtnClickHandler(this, args);
                }
            }
            return true;
        }

        public class ButtonClickArgs : EventArgs
        {
            public ButtonClickState State;
        }
    }
}
