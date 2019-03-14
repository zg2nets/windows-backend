using System;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Controls
{
    public class Header : Control
    {
        private HeaderAttributes HeaderAttribute;
        private TextLabel HeaderTitle;
        private View HeaderLine;

        public Header() : base()
        {
            Initialize();
        }
        public Header(string style) : base(style)
        {
            Initialize();
        }

        public Header(HeaderAttributes attributes) : base()
        {
            this.attributes = HeaderAttribute = attributes.Clone() as HeaderAttributes;
        }

        public string HeaderText
        {
            get
            {
                return HeaderAttribute?.TextAttributes?.Text?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    if (HeaderAttribute.TextAttributes.Text == null)
                    {
                        HeaderAttribute.TextAttributes.Text = new StringSelector();
                    }
                    HeaderAttribute.TextAttributes.Text.All = value;

                    RelayoutRequest();
                }
            }
        }
        
        public Color HeaderTextColor
        {
            get
            {
                return HeaderAttribute?.TextAttributes?.TextColor?.All;
            }
            set
            {
                if (value != null)
                {
                    CreateTextAttributes();
                    if (HeaderAttribute.TextAttributes.TextColor == null)
                    {
                        HeaderAttribute.TextAttributes.TextColor = new ColorSelector();
                    }
                    HeaderAttribute.TextAttributes.TextColor.All = value;
                    RelayoutRequest();
                }
            }
        }

        public Color LinebackgroundColor
        {
            get
            {
                return HeaderAttribute?.LineAttributes?.BackgroundColor?.All;
            }
            set
            {
                CreateLineAttributes();
                if (HeaderAttribute.LineAttributes.BackgroundColor == null)
                {
                    HeaderAttribute.LineAttributes.BackgroundColor = new ColorSelector();
                }
                HeaderAttribute.LineAttributes.BackgroundColor.All = value;
                RelayoutRequest();
            }
        }

        protected override Attributes GetAttributes()
        {
            return new HeaderAttributes();
        }

        protected override void Dispose(DisposeTypes type)
        {
            if (disposed)
            {
                return;
            }
            if (type == DisposeTypes.Explicit)
            {
                if (HeaderAttribute.TextAttributes != null)
                {
                    if (HeaderTitle== null)
                    {
                        HeaderTitle= new TextLabel();
                        this.Add(HeaderTitle);
                    }
                    ApplyAttributes(HeaderTitle, HeaderAttribute.TextAttributes);
                }
                if (HeaderAttribute.LineAttributes != null)
                {
                    if (HeaderLine== null)
                    {
                        HeaderLine = new View();
                        this.Add(HeaderLine);
                    }
                    ApplyAttributes(HeaderLine, HeaderAttribute.LineAttributes);
                }
            }
            base.Dispose(type);
        }

        protected override void OnUpdate(Attributes attributtes)
        {
            HeaderAttribute = attributes as HeaderAttributes;
            if (HeaderAttribute == null)
            {
                return;
            }
            ApplyAttributes(this, HeaderAttribute);
            if (HeaderAttribute.TextAttributes != null)
            {
                if (HeaderTitle== null)
                {
                    HeaderTitle= new TextLabel
                    {
                        FontFamily = "SamsungOne 500",
                        Position2D = new Position2D(55, 0),
                        SizeHeight=128,
                        SizeWidth= 968,
                        PointSize=50,
                        HorizontalAlignment =   HorizontalAlignment.Center,
                        VerticalAlignment =  VerticalAlignment.Center,
                    };
                    this.Add(HeaderTitle);
                }
                ApplyAttributes(HeaderTitle, HeaderAttribute.TextAttributes);
            }
            if (HeaderAttribute.LineAttributes != null)
            {
                if (HeaderLine == null)
                {
                    HeaderLine = new View
                    {
                        Position2D = new Position2D(0, 127),
                       Size2D = new Size2D(1080, 1),
                       
                };
                    this.Add(HeaderLine);
                }
                ApplyAttributes(HeaderLine, HeaderAttribute.LineAttributes);
            }
        }

        private void CreateTextAttributes()
        {
            if (HeaderAttribute.TextAttributes == null)
            {
                HeaderAttribute.TextAttributes = new TextAttributes();
            }
        }

        private void CreateLineAttributes()
        {

            if (HeaderAttribute.LineAttributes == null)
            {
                HeaderAttribute.LineAttributes = new ViewAttributes();
            }
        }

        private void Initialize()
        {
            HeaderAttribute = attributes as HeaderAttributes;
            if (HeaderAttribute == null)
            {
                throw new Exception("Header attribute parse error.");
            }
        }
     
    }
}
