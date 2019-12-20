using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class ImageItem : Item
    {
        public ImageItem()
        {
            this.Type = "Image";
            myView = new ImageView();

            this.Add(myView);
        }

        public override string ResourceUrl
        {
            get
            {
                return (myView as ImageView).ResourceUrl;
            }
            set
            {
                (myView as ImageView).ResourceUrl = value;
            }
        }

        public int DesiredWidth
        {
            get
            {
                return (myView as ImageView).DesiredWidth;
            }
            set
            {
                (myView as ImageView).DesiredWidth = value;
                this.Size2D = new Size2D((myView as ImageView).DesiredWidth, (myView as ImageView).DesiredHeight);
            }
        }
        public int DesiredHeight
        {
            get
            {
                return (myView as ImageView).DesiredWidth;
            }
            set
            {
                (myView as ImageView).DesiredHeight = value;
                this.Size2D = new Size2D((myView as ImageView).DesiredWidth, (myView as ImageView).DesiredHeight);
            }
        }

    }
}
