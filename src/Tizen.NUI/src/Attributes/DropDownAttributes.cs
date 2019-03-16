﻿using Tizen.NUI.Binding;

namespace Tizen.NUI.Controls
{
    public class DropDownAttributes : ViewAttributes
    {
        public static readonly BindableProperty ButtonAttributesProperty = BindableProperty.Create("ButtonAttributes", typeof(ButtonAttributes), typeof(DropDownAttributes), default(ButtonAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownAttributes)bindable;
            if (newValue != null)
            {
                attrs.buttonAttributes = (ButtonAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (DropDownAttributes)bindable;
            return attrs.buttonAttributes;
        });

        public static readonly BindableProperty HeaderTextAttributesProperty = BindableProperty.Create("HeaderTextAttributes", typeof(TextAttributes), typeof(DropDownAttributes), default(TextAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownAttributes)bindable;
            if (newValue != null)
            {
                attrs.headerTextAttributes = (TextAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (DropDownAttributes)bindable;
            return attrs.headerTextAttributes;
        });

        public static readonly BindableProperty SpaceBetweenButtonTextAndIconProperty = BindableProperty.Create("SpaceBetweenButtonTextAndIcon", typeof(int), typeof(DropDownAttributes), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownAttributes)bindable;
            if (newValue != null)
            {
                attrs.spaceBetweenButtonTextAndIcon = (int)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (DropDownAttributes)bindable;
            return attrs.spaceBetweenButtonTextAndIcon;
        });

        public static readonly BindableProperty SpaceProperty = BindableProperty.Create("Space", typeof(Vector4), typeof(DropDownAttributes), default(Vector4), propertyChanged: (BindableProperty.BindingPropertyChangedDelegate)((bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownAttributes)bindable;
            if (newValue != null)
            {
                attrs.space = (Vector4)newValue;
            }
        }),
        defaultValueCreator: (BindableProperty.CreateDefaultValueDelegate)((bindable) =>
        {
            var attrs = (DropDownAttributes)bindable;
            return (object)attrs.space;
        }));

        private ButtonAttributes buttonAttributes = null;
        private TextAttributes headerTextAttributes = null;
        private int spaceBetweenButtonTextAndIcon = 0;
        private Vector4 space = new Vector4(0, 0, 0, 0);

        public DropDownAttributes() : base() { }
        public DropDownAttributes(DropDownAttributes attributes) : base(attributes)
        {
            if (attributes.buttonAttributes != null)
            {
                buttonAttributes = attributes.buttonAttributes.Clone() as ButtonAttributes;
            }

            if (attributes.headerTextAttributes != null)
            {
                headerTextAttributes = attributes.headerTextAttributes.Clone() as TextAttributes;
            }

            spaceBetweenButtonTextAndIcon = attributes.spaceBetweenButtonTextAndIcon;
        }

        public ButtonAttributes ButtonAttributes
        {
            get
            {
                return (ButtonAttributes)GetValue(ButtonAttributesProperty);
            }
            set
            {
                SetValue(ButtonAttributesProperty, value);
            }
        }

        public TextAttributes HeaderTextAttributes
        {
            get
            {
                return (TextAttributes)GetValue(HeaderTextAttributesProperty);
            }
            set
            {
                SetValue(HeaderTextAttributesProperty, value);
            }
        }

        public int SpaceBetweenButtonTextAndIcon
        {
            get
            {
                return (int)GetValue(SpaceBetweenButtonTextAndIconProperty);
            }
            set
            {
                SetValue(SpaceBetweenButtonTextAndIconProperty, value);
            }
        }

        public Vector4 Space
        {
            get
            {
                return (Vector4)GetValue(SpaceProperty);
            }
            set
            {
                SetValue(SpaceProperty, value);
            }
        }

        public override Attributes Clone()
        {
            return new DropDownAttributes(this);
        }
    }

    public class DropDownItemAttributes : ButtonAttributes
    {
        public static readonly BindableProperty SubTextAttributesProperty = BindableProperty.Create("SubTextAttributes", typeof(TextAttributes), typeof(DropDownItemAttributes), default(TextAttributes), propertyChanged: (BindableProperty.BindingPropertyChangedDelegate)((bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            if (newValue != null)
            {
                attrs.subTextAttributes = (TextAttributes)newValue;
            }
        }),
        defaultValueCreator: (BindableProperty.CreateDefaultValueDelegate)((bindable) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            return (object)attrs.subTextAttributes;
        }));

        public static readonly BindableProperty DividerLineAttributesProperty = BindableProperty.Create("DividerLineAttributes", typeof(ViewAttributes), typeof(DropDownItemAttributes), default(ViewAttributes), propertyChanged: (BindableProperty.BindingPropertyChangedDelegate)((bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            if (newValue != null)
            {
                attrs.dividerAttributes = (ViewAttributes)newValue;
            }
        }),
        defaultValueCreator: (BindableProperty.CreateDefaultValueDelegate)((bindable) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            return (object)attrs.dividerAttributes;
        }));

        public static readonly BindableProperty SpaceProperty = BindableProperty.Create("Space", typeof(Vector4), typeof(DropDownItemAttributes), default(Vector4), propertyChanged: (BindableProperty.BindingPropertyChangedDelegate)((bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            if (newValue != null)
            {
                attrs.space = (Vector4)newValue;
            }
        }),
        defaultValueCreator: (BindableProperty.CreateDefaultValueDelegate)((bindable) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            return (object)attrs.space;
        }));

        public static readonly BindableProperty EnableIconCenterProperty = BindableProperty.Create("EnableIconCenter", typeof(bool), typeof(DropDownItemAttributes), default(bool), propertyChanged: (BindableProperty.BindingPropertyChangedDelegate)((bindable, oldValue, newValue) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            if (newValue != null)
            {
                attrs.enableIconCenter = (bool)newValue;
            }
        }),
        defaultValueCreator: (BindableProperty.CreateDefaultValueDelegate)((bindable) =>
        {
            var attrs = (DropDownItemAttributes)bindable;
            return (object)attrs.enableIconCenter;
        }));

        private TextAttributes subTextAttributes;
        private ViewAttributes dividerAttributes;
        private Vector4 space = new Vector4(0, 0, 0, 0);
        private bool enableIconCenter = false;

        public DropDownItemAttributes() : base() { }
        public DropDownItemAttributes(DropDownItemAttributes attributes) : base(attributes)
        {
            if (attributes.subTextAttributes != null)
            {
                subTextAttributes = attributes.subTextAttributes.Clone() as TextAttributes;
            }

            if (attributes.dividerAttributes != null)
            {
                dividerAttributes = attributes.dividerAttributes.Clone() as ViewAttributes;
            }

            if (attributes.space != null)
            {
                space = new Vector4(attributes.space.X, attributes.space.Y, attributes.space.Z, attributes.space.W);
            }

            enableIconCenter = attributes.enableIconCenter;
        }

        public TextAttributes SubTextAttributes
        {
            get
            {
                return (TextAttributes)GetValue(SubTextAttributesProperty);
            }
            set
            {
                SetValue(SubTextAttributesProperty, value);
            }
        }

        public ViewAttributes DividerLineAttributes
        {
            get
            {
                return (ViewAttributes)GetValue(DividerLineAttributesProperty);
            }
            set
            {
                SetValue(DividerLineAttributesProperty, value);
            }
        }

        public Vector4 Space
        {
            get
            {
                return (Vector4)GetValue(SpaceProperty);
            }
            set
            {
                SetValue(SpaceProperty, value);
            }
        }

        public bool EnableIconCenter
        {
            get
            {
                return (bool)GetValue(EnableIconCenterProperty);
            }
            set
            {
                SetValue(EnableIconCenterProperty, value);
            }
        }

        public override Attributes Clone()
        {
            return new DropDownItemAttributes(this);
        }
    }
}
