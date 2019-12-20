using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.NUI;

namespace NUIPhotoSlide
{
    class ImageItemData
    {
        private Position postion;

        public Position Position
        {
            get
            {
                return postion;
            }
        }

        public ImageItemData(Position pos)
        {
            this.postion = pos;
        }
    }
}
