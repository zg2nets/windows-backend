﻿using Tizen.NUI.Binding;
using Tizen.NUI.Xaml;

namespace Tizen.NUI.Controls
{
    public class ToastAttributes : ViewAttributes
    {
        private ImageAttributes backgroundImageAttrs = null;
        private TextAttributes textAttrs= null;
        private int? upSpace;
        public ToastAttributes() : base() { }

        public ToastAttributes(ToastAttributes attrs) : base(attrs)
        {
            if(attrs.backgroundImageAttrs != null)
            {
                backgroundImageAttrs = attrs.backgroundImageAttrs.Clone() as ImageAttributes;
            }
            if(attrs.textAttrs != null)
            {
                textAttrs = attrs.textAttrs.Clone() as TextAttributes;
            }
        }

        public static readonly BindableProperty BackgroundImageAttributesProperty = BindableProperty.Create("BackgroundImageAttributes", typeof(ImageAttributes), typeof(ToastAttributes), default(ImageAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (ToastAttributes)bindable;
            if(newValue != null)
            {
                attrs.backgroundImageAttrs = (ImageAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (ToastAttributes)bindable;
            return attrs.backgroundImageAttrs;
        });

        public static readonly BindableProperty TextAttributesProperty = BindableProperty.Create("TextAttributes", typeof(TextAttributes), typeof(ToastAttributes), default(TextAttributes), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (ToastAttributes)bindable;
            if (newValue != null)
            {
                attrs.textAttrs = (TextAttributes)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (ToastAttributes)bindable;
            return attrs.textAttrs;
        });

        public static readonly BindableProperty UpSpaceProperty = BindableProperty.Create("UpSpace", typeof(int), typeof(ToastAttributes), default(int), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var attrs = (ToastAttributes)bindable;
            if (newValue != null)
            {
                attrs.upSpace = (int?)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            var attrs = (ToastAttributes)bindable;
            return attrs.upSpace;
        });

        public ImageAttributes BackgroundImageAttributes
        {
            get
            {
                return (ImageAttributes)GetValue(BackgroundImageAttributesProperty);
            }
            set
            {
                SetValue(BackgroundImageAttributesProperty, value);
            }
        }

        public TextAttributes TextAttributes
        {
            get
            {
                return (TextAttributes)GetValue(TextAttributesProperty);
            }
            set
            {
                SetValue(TextAttributesProperty, value);
            }
        }

        public int? UpSpace
        {
            get
            {
                return (int?)GetValue(UpSpaceProperty);
            }
            set
            {
                SetValue(UpSpaceProperty, value);
            }
        }

        public override Attributes Clone()
        {
            return new ToastAttributes(this);
        }

    }
}
