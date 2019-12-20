using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Examples
{
    public class DropDown : IExample
    {
        private SampleLayout root;
        private Tizen.NUI.Components.DA.DropDown dropDown = null;
        private Tizen.NUI.Components.DA.DropDown dropDown2 = null;
        private ScrollBar scrollBar = null;
        private TextLabel text = null;

        private static string[] iconImage = new string[]
        {
            CommonResource.GetFHResourcePath() + "10. Drop Down/img_dropdown_fahrenheit.png",
            CommonResource.GetFHResourcePath() + "10. Drop Down/img_dropdown_celsius.png",
        };

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            root = new SampleLayout();
            root.HeaderText = "DropDown";

            text = new TextLabel();
            text.Text = "DropDown Clicked item string is ";
            text.PointSize = 14;
            text.Size2D = new Size2D(880, 50);
            text.Position2D = new Position2D(100, 10);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.MultiLine = true;
            root.Add(text);

            dropDown = new Tizen.NUI.Components.DA.DropDown("HeaderDropDown");
            dropDown.Size2D = new Size2D(1080, 108);
            dropDown.Position2D = new Position2D(0, 100);
            dropDown.Style.ListBackgroundImage.Size = new Size(360, 500);
            dropDown.Style.HeaderText.Text = "Header area";
            dropDown.Style.Button.Text.Text = "Normal list 1";
            dropDown.ItemClickEvent += DropDownItemClickEvent;
            root.Add(dropDown);

            for (int i = 0; i < 8; i++)
            {
                Tizen.NUI.Components.DA.DropDown.DropDownDataItem item = new Tizen.NUI.Components.DA.DropDown.DropDownDataItem("TextListItem");
                item.Size = new Size(360, 96);
                item.Text = "Normal list " + i;
                dropDown.AddItem(item);
            }

            dropDown.SelectedItemIndex = 1;
            ////////Attach scrollbar///////////
            scrollBar = new ScrollBar();
            scrollBar.Direction = ScrollBar.DirectionType.Vertical;
            scrollBar.Position2D = new Position2D(394, 2);
            scrollBar.Size2D = new Size2D(4, 446);
            scrollBar.TrackColor = Color.Green;
            scrollBar.ThumbSize = new Size(4, 30);
            scrollBar.ThumbColor = Color.Yellow;
            scrollBar.TrackImageURL = CommonResource.GetFHResourcePath() + "component/c_progressbar/c_progressbar_white_buffering.png";
            dropDown.AttachScrollBar(scrollBar);

            //////////////////ListSpinner DropDown////////////////////////
            dropDown2 = new Tizen.NUI.Components.DA.DropDown("ListDropDown");
            dropDown2.Size2D = new Size2D(1080, 108);
            dropDown2.Position2D = new Position2D(0, 300);
            dropDown2.Style.ListBackgroundImage.Size = new Size(360, 192);
            dropDown2.Style.HeaderText.Text = "List area";
            dropDown2.Style.Button.Text.Text = "Menu";
            root.Add(dropDown2);

            for (int i = 0; i < 2; i++)
            {
                Tizen.NUI.Components.DA.DropDown.DropDownDataItem item = new Tizen.NUI.Components.DA.DropDown.DropDownDataItem("IconListItem");
                item.Size = new Size(360, 96);
                item.IconResourceUrl = iconImage[i];
                dropDown2.AddItem(item);
            }

            dropDown2.SelectedItemIndex = 0;

            dropDown.RaiseToTop();
        }

        private void DropDownItemClickEvent(object sender, Tizen.NUI.Components.DA.DropDown.ItemClickEventArgs e)
        {
            text.Text = "DropDown Clicked item string is " + e.Text;
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (text != null)
                {
                    root.Remove(text);
                    text.Dispose();
                    text = null;
                }

                if (dropDown != null)
                {
                    if (scrollBar != null)
                    {
                        dropDown.DetachScrollBar();
                        scrollBar.Dispose();
                        scrollBar = null;
                    }

                    root.Remove(dropDown);
                    dropDown.Dispose();
                    dropDown = null;
                }

                if (dropDown2 != null)
                {
                    root.Remove(dropDown2);
                    dropDown2.Dispose();
                    dropDown2 = null;
                }

                root.Dispose();
            }
        }

        private void ButtonClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            Tizen.NUI.Components.DA.Button btn = sender as Tizen.NUI.Components.DA.Button;

        }
    }
}
