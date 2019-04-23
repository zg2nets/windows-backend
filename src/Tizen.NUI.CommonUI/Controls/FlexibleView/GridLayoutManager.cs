﻿/*
 * Copyright(c) 2018 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */
using System.ComponentModel;

namespace Tizen.NUI.CommonUI
{
    /// <summary>
    /// Layout collection of views in a grid.
    /// </summary>
    /// <since_tizen> 5.5 </since_tizen>
    /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class GridLayoutManager : LinearLayoutManager
    {
        private static readonly int DEFAULT_SPAN_COUNT = -1;

        private int mSpanCount = DEFAULT_SPAN_COUNT;

        /// <summary>
        /// Creates a GridLayoutManager with orientation. 
        /// </summary>
        /// <param name="spanCount">The number of columns or rows in the grid</param>
        /// <param name="orientation">Layout orientation.Should be HORIZONTAL or VERTICAL</param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public GridLayoutManager(int spanCount, int orientation) : base(orientation)
        {
            mSpanCount = spanCount;
        }

        /// <summary>
        /// Retrieves a position that neighbor to current position by direction. 
        /// </summary>
        /// <param name="position">The anchor adapter position</param>
        /// <param name="direction">The direction.</param>
        /// <param name="state">Transient state of FlexibleView </param>
        /// <since_tizen> 5.5 </since_tizen>
        /// This will be public opened in tizen_5.5 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override int GetNextPosition(int position, string direction, FlexibleView.ViewState state)
        {
            if (mOrientation == HORIZONTAL)
            {
                switch (direction)
                {
                    case "Left":
                        if (position >= mSpanCount)
                        {
                            return position - mSpanCount;
                        }
                        break;
                    case "Right":
                        if (position < state.ItemCount - mSpanCount)
                        {
                            return position + mSpanCount;
                        }
                        break;
                    case "Up":
                        if (position % mSpanCount > 0)
                        {
                            return position - 1;
                        }
                        break;
                    case "Down":
                        if (position < state.ItemCount - 1 && (position % mSpanCount < mSpanCount - 1))
                        {
                            return position + 1;
                        }
                        break;
                }
            }
            else
            {
                switch (direction)
                {
                    case "Left":
                        if (position % mSpanCount > 0)
                        {
                            return position - 1;
                        }
                        break;
                    case "Right":
                        if (position < state.ItemCount - 1 && (position % mSpanCount < mSpanCount - 1))
                        {
                            return position + 1;
                        }
                        break;
                    case "Up":
                        if (position >= mSpanCount)
                        {
                            return position - mSpanCount;
                        }
                        break;
                    case "Down":
                        if (position < state.ItemCount - mSpanCount)
                        {
                            return position + mSpanCount;
                        }
                        break;
                }
            }

            return NO_POSITION;
        }

        internal override void LayoutChunk(FlexibleView.Recycler recycler, FlexibleView.ViewState state,
            LayoutState layoutState, LayoutChunkResult result)
        {
            bool layingOutInPrimaryDirection =
                layoutState.ItemDirection == LayoutState.ITEM_DIRECTION_TAIL;

            int count = mSpanCount;
            for (int i = 0; i < count; i++)
            {
                FlexibleView.ViewHolder holder = layoutState.Next(recycler);
                if (holder == null)
                {
                    result.Finished = true;
                    return;
                }

                if (layingOutInPrimaryDirection)
                    AddView(holder);
                else
                    AddView(holder, 0);

                result.Consumed = mOrientationHelper.GetViewHolderMeasurement(holder);

                float left, top, width, height;
                if (mOrientation == VERTICAL)
                {
                    width = (GetWidth() - GetPaddingLeft() - GetPaddingRight()) / count;
                    height = result.Consumed;
                    if (layoutState.LayoutDirection == LayoutState.LAYOUT_END)
                    {
                        left = GetPaddingLeft() + width * i;
                        top = layoutState.Offset;
                    }
                    else
                    {
                        left = GetPaddingLeft() + width * (count - 1 - i);
                        top = layoutState.Offset - height;
                    }
                    LayoutChild(holder, left, top, width, height);
                }
                else
                {
                    width = result.Consumed;
                    height = (GetHeight() - GetPaddingTop() - GetPaddingBottom()) / count;
                    if (layoutState.LayoutDirection == LayoutState.LAYOUT_END)
                    {
                        top = GetPaddingTop() + height * i;
                        left = layoutState.Offset;
                    }
                    else
                    {
                        top = GetPaddingTop() + height * (count - 1 - i);
                        left = layoutState.Offset - width;
                    }
                    LayoutChild(holder, left, top, width, height);
                }
            }
        }


    }
}
