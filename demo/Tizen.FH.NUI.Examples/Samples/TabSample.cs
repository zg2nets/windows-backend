using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components.DA;

namespace Tizen.FH.NUI.Examples
{
    public class Tab : IExample
    {
        private SampleLayout root;
        private Tizen.NUI.Components.DA.Tab tab = null;
        private Tizen.NUI.Components.DA.Button[] button = new Tizen.NUI.Components.DA.Button[2];
        private int num = 2;

        private static string[] mode = new string[]
        {
            "LargeTab",
            "Small Tab",
        };

        public void Activate()
        {
            Window.Instance.BackgroundColor = Color.White;
            root = new SampleLayout();
            root.HeaderText = "Tab";

            tab = new Tizen.NUI.Components.DA.Tab("Tab");
            tab.UseTextNaturalSize = false;
            tab.Size2D = new Size2D(1080, 108);
            tab.Position2D = new Position2D(0, 300);
            tab.BackgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            root.Add(tab);

            for (int i = 0; i < 3; i++)
            {
                Tizen.NUI.Components.DA.Tab.TabItemData item = new Tizen.NUI.Components.DA.Tab.TabItemData();
                item.Text = "Tab " + i;
                if (i == 1)
                {
                    item.Text = "Long long Tab " + i;
                }
                tab.AddItem(item);
            }

            for (int i = 0; i < num; i++)
            {
                button[i] = new Tizen.NUI.Components.DA.Button("ServiceButton");
                button[i].Size2D = new Size2D(240, 80);
                button[i].Position2D = new Position2D(280 + i * 260, 700);
                button[i].Text = mode[i];
                button[i].ClickEvent += ButtonClickEvent;
                root.Add(button[i]);
            }
        }

        public void Deactivate()
        {
            if (root != null)
            {
                if (tab != null)
                {
                    root.Remove(tab);
                    tab.Dispose();
                    tab = null;
                }

                for (int i = 0; i < num; i++)
                {
                    if (button[i] != null)
                    {
                        root.Remove(button[i]);
                        button[i].Dispose();
                        button[i] = null;
                    }
                }

                root.Dispose();
            }
        }

        private void ButtonClickEvent(object sender, Tizen.NUI.Components.DA.Button.ClickEventArgs e)
        {
            Tizen.NUI.Components.DA.Button btn = sender as Tizen.NUI.Components.DA.Button;
            if (button[0] == btn)
            {
                tab.UseTextNaturalSize = false;
            }
            else if (button[1] == btn)
            {
                tab.UseTextNaturalSize = true;
            }

            //tab.DeleteItem(0);

            //Tab.TabItemData item = new Tab.TabItemData();
            //item.Text = "insert Tab ";
            //tab.InsertItem(item, 1);
        }
    }
}
