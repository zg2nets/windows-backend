using System.Collections.Generic;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace Tizen.FH.NUI.Examples
{
    public class CommonResource
    {
        public static string GetFHResourcePath()
        {
            return @"../../../demo/csharp-demo/res/images/FH3/";
        }
        public static string GetDaliResourcePath()
        {
            Tizen.Log.Fatal("NUI", $"GetFHResourcePath()! res={Tizen.Applications.Application.Current.DirectoryInfo.Resource}");
            //turn Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/Dali/";
            return @"../../../demo/csharp-demo/res/images/Dali/";
        }
    }
    public class NaviListItemData
    {
        private string str;

        public NaviListItemData(string s)
        {
            str = s;
        }

        public string TextString
        {
            get
            {
                return str;
            }
        }
    }

    public class NaviListItemView : View
    {
        private TextLabel mText;

        public NaviListItemView()
        {
            mText = new TextLabel();
            mText.WidthResizePolicy = ResizePolicyType.FillToParent;
            mText.HeightResizePolicy = ResizePolicyType.FillToParent;
            mText.PointSize = 40;
            mText.HorizontalAlignment = HorizontalAlignment.Begin;
            mText.VerticalAlignment = VerticalAlignment.Center;
            Add(mText);
        }

        public string MainText
        {
            get
            {
                return mText.Text;
            }
            set
            {
                mText.Text = value;
            }
        }
    }

    public class NaviListBridge : Tizen.NUI.Components.DA.FlexibleView.Adapter
    {
        private List<NaviListItemData> mDatas;

        public NaviListBridge(List<NaviListItemData> datas)
        {
            mDatas = datas;
        }

        public void InsertData(int position)
        {
            mDatas.Insert(position, new NaviListItemData((1000 + position).ToString()));
            NotifyItemInserted(position);
        }

        public void RemoveData(int position)
        {
            mDatas.RemoveAt(position);
            NotifyItemRemoved(position);
        }

        public override Tizen.NUI.Components.DA.FlexibleView.ViewHolder OnCreateViewHolder(int viewType)
        {
            Tizen.NUI.Components.DA.FlexibleView.ViewHolder viewHolder = new Tizen.NUI.Components.DA.FlexibleView.ViewHolder(new NaviListItemView());

            return viewHolder;
        }

        public override void OnBindViewHolder(Tizen.NUI.Components.DA.FlexibleView.ViewHolder holder, int position)
        {
            NaviListItemData listItemData = mDatas[position];
            NaviListItemView listItemView = holder.ItemView as NaviListItemView;

            listItemView.Name = "Item" + position;
            listItemView.SizeWidth = 1000;
            listItemView.SizeHeight = 163;

            if (listItemView != null)
            {
                listItemView.MainText = listItemData.TextString;
            }
            listItemView.Margin = new Extents(0, 0, 1, 0);
            listItemView.BackgroundColor = new Color(1f, 1f, 1f, 1f);
        }

        public override void OnDestroyViewHolder(Tizen.NUI.Components.DA.FlexibleView.ViewHolder holder)
        {
            if (holder.ItemView != null)
            {
                holder.ItemView.Dispose();
            }
        }

        public override int GetItemCount()
        {
            return mDatas.Count;
        }

        public override void OnFocusChange(Tizen.NUI.Components.DA.FlexibleView flexibleView, int previousFocus, int currentFocus)
        {
            Tizen.NUI.Components.DA.FlexibleView.ViewHolder previousFocusView = flexibleView.FindViewHolderForAdapterPosition(previousFocus);
            if (previousFocusView != null)
            {
            
            }
            Tizen.NUI.Components.DA.FlexibleView.ViewHolder currentFocusView = flexibleView.FindViewHolderForAdapterPosition(currentFocus);
            if (currentFocusView != null)
            {
                
            }
        }

        public override void OnViewAttachedToWindow(Tizen.NUI.Components.DA.FlexibleView.ViewHolder holder)
        {

        }

        public override void OnViewDetachedFromWindow(Tizen.NUI.Components.DA.FlexibleView.ViewHolder holder)
        {

        }

    }
}

