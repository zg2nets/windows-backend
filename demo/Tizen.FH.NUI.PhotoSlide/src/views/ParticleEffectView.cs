using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIPhotoSlide
{
    class ParticleEffectView : ImageView
    {
        private List<String> particleList;
        public ParticleEffectView() : base()
        {
            particleList = new List<String>();

            String FolderName = CommonResource.GetResourcePath() + "/particle/";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".png") == 0)
                {
                    String FileNameOnly = File.Name.Substring(0, File.Name.Length - 4);
                    String FullFileName = File.FullName;

                    particleList.Add(FullFileName);
                }
            }
            particleList.Sort();

            PropertyMap property = new PropertyMap();
            PropertyArray array = new PropertyArray();
            for (int i = 0; i < particleList.Count; i++)
            {
                array.PushBack(new PropertyValue(particleList[i]));
            }

            property.Add(ImageVisualProperty.FrameDelay, new PropertyValue(30));
            property.Add(ImageVisualProperty.URL, new PropertyValue(array));
            this.Image = property;

        }
    }
}
