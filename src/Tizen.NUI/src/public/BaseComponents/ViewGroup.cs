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
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.Binding;

namespace Tizen.NUI.BaseComponents
{

    /// <summary>
    /// ViewGroup is a group of view which can be set common property<br />
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class ViewGroup : BaseHandle
    {
        public List<View> itemGroup;
        private View root;

        public ViewGroup(View view) : base()
        {
            itemGroup = new List<View>();
            if ((root = view) == null)
            {
                throw new Exception("Root view is null.");
            }      
        }

        public int Count
        {
            get
            {
                return itemGroup.Count;
            }
        }

        public bool Contains(View view)
        {
            return itemGroup.Contains(view);
        }

        public int GetIndex(View view)
        {
            return itemGroup.IndexOf(view);
        }

        public void AddItem(View view)
        {
            if (itemGroup.Contains(view))
            {
                return;
            }
            itemGroup.Add(view);
            root.Add(view);
        }

        public void RemoveItem(View view)
        {
            if (!itemGroup.Contains(view))
            {
                return;
            }
            itemGroup.Remove(view);
            root.Remove(view);
        }

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemSizeProperty = BindableProperty.Create(nameof(ItemSize), typeof(Size), typeof(ViewGroup), new Size(0, 0), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ViewGroup viewGroup = (ViewGroup)bindable;
            Size itemSize = (Size)newValue;
            if (viewGroup.itemGroup != null)
            {        
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.Size = itemSize;
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Size ret = new Size(0, 0);
            ViewGroup viewGroup = (ViewGroup)bindable;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.Size = ret;
                }
            }
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemOpacityProperty = BindableProperty.Create(nameof(ItemOpacity), typeof(float), typeof(ViewGroup), default(float), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ViewGroup viewGroup = (ViewGroup)bindable;
            float itemOpacity = (float)newValue;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.Opacity = itemOpacity;
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            float ret = 1;
            ViewGroup viewGroup = (ViewGroup)bindable;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.Opacity = ret;
                }
            }
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundColorProperty = BindableProperty.Create(nameof(ItemBackgroundColor), typeof(Color), typeof(ViewGroup), Color.Transparent, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ViewGroup viewGroup = (ViewGroup)bindable;
            Color itemColor = (Color)newValue;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.BackgroundColor = itemColor;
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            Color ret = new Color(0, 0, 0, 0);
            ViewGroup viewGroup = (ViewGroup)bindable;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.BackgroundColor = ret;
                }
            }
            return ret;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundImageProperty = BindableProperty.Create(nameof(ItemBackgroundImage), typeof(string), typeof(ViewGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ViewGroup viewGroup = (ViewGroup)bindable;
            string itemImage = (string)newValue;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.BackgroundImage = itemImage;
                }
            }
        },
        defaultValueCreator: (bindable) =>
        {
            string ret = "";
            ViewGroup viewGroup = (ViewGroup)bindable;
            if (viewGroup.itemGroup != null)
            {
                for (int i = 0; i < viewGroup.itemGroup.Count; i++)
                {
                    View view = viewGroup.itemGroup[i];
                    view.BackgroundImage = ret;
                }
            }
            return ret;
        });

        /// <summary>
        /// Common size for all of the Items
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public Size ItemSize
        {
            get
            {
                return (Size)GetValue(ItemSizeProperty);
            }
            set
            {
                SetValue(ItemSizeProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Retrieves and sets the view's opacity.<br />
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public float ItemOpacity
        {
            get
            {
                return (float)GetValue(ItemOpacityProperty);
            }
            set
            {
                SetValue(ItemOpacityProperty, value);
                NotifyPropertyChanged();
            }
        }

        public Color ItemBackgroundColor
        {
            get
            {
                return (Color)GetValue(ItemBackgroundColorProperty);
            }
            set
            {
                SetValue(ItemBackgroundColorProperty, value);
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The mutually exclusive with "backgroundColor" and "background" type Map.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string ItemBackgroundImage
        {
            get
            {
                return (string)GetValue(ItemBackgroundImageProperty);
            }
            set
            {
                SetValue(ItemBackgroundImageProperty, value);
                NotifyPropertyChanged();
            }
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }

            if (type == DisposeTypes.Explicit)
            {
                if (itemGroup != null)
                {
                    foreach (View view in itemGroup)
                    {
                        root.Remove(view);
                        view.Dispose();
                    }
                    itemGroup.Clear();
                    itemGroup = null;
                }
            }

            base.Dispose(type);
        }
    }
}
