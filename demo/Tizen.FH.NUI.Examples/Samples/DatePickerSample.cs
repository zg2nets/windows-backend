using Tizen.NUI;
using System;
using Tizen.FH.NUI.Components;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Examples
{
    public class DatePicker : IExample
    {
        private SampleLayout root;
        private Tizen.NUI.Components.DA.Popup popup = null;
        private Picker datePicker = null;

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            root = new SampleLayout();
            root.HeaderText = "Date Picker";

            CreateDatePicker();
        }

        private void CreateDatePicker()
        {
            popup = new Tizen.NUI.Components.DA.Popup("Popup");
            popup.Size = new Size2D(1032, 982);
            popup.Position = new Position2D(24, 0);
            popup.Style.Title.Text = "Set Date";
            popup.AddButton("Yes");
            popup.AddButton("Exit");
            popup.PopupButtonClickEvent += PopupButtonClickedEvent;
            popup.ButtonHeight = 132;
            popup.ButtonBackground = CommonResource.GetFHResourcePath() + "3. Button/rectangle_btn_normal.png";
            popup.ButtonBackgroundBorder = new Rectangle(5, 5, 5, 5);
            popup.ButtonOverLayBackgroundColorSelector = new Selector<Color>
            {
                Normal = new Color(1.0f, 1.0f, 1.0f, 1.0f),
                Pressed = new Color(0.0f, 0.0f, 0.0f, 0.1f),
                Selected = new Color(1.0f, 1.0f, 1.0f, 1.0f),
            };

            datePicker = new Picker("DAPicker");
            datePicker.Size2D = new Size2D(1032, 724);
            datePicker.Position2D = new Position2D(0, 0);
            datePicker.CurDate = new DateTime(2012, 12, 12);
            popup.ContentView.Add(datePicker);
            root.Add(popup);
        }

        private void PopupButtonClickedEvent(object sender, Tizen.NUI.Components.DA.Popup.ButtonClickEventArgs e)
        {

        }

        private void DestoryDatePicker()
        {
            if (popup != null)
            {
                if (datePicker != null)
                {
                    popup.ContentView.Remove(datePicker);
                    datePicker.Dispose();
                    datePicker = null;
                }

                root.Remove(popup);
                popup.Dispose();
                popup = null;
            }
        }

        public void Deactivate()
        {
            if (root != null)
            {
                DestoryDatePicker();
                root.Dispose();
            }
        }
    }
}
