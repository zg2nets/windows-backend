﻿using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Controls
{
    /**
    * Helper class for LayoutManagers to abstract measurements depending on the View's orientation.
    * <p>
    * It is developed to easily support vertical and horizontal orientations in a LayoutManager but
    * can also be used to abstract calls around view bounds and child measurements with margins and
    * decorations.
    *
    * @see #createHorizontalHelper(RecyclerView.LayoutManager)
    * @see #createVerticalHelper(RecyclerView.LayoutManager)
    */
    public abstract class OrientationHelper
    {
        public static readonly int HORIZONTAL = 0;
        public static readonly int VERTICAL = 1;

        private static readonly int INVALID_SIZE = -1;

        protected FlexibleView.LayoutManager mLayoutManager;

        private float mLastTotalSpace = INVALID_SIZE;

        public OrientationHelper(FlexibleView.LayoutManager layoutManager)
        {
            mLayoutManager = layoutManager;
        }

        /**
         * Call this method after onLayout method is complete if state is NOT pre-layout.
         * This method records information like layout bounds that might be useful in the next layout
         * calculations.
         */
        public void OnLayoutComplete()
        {
            mLastTotalSpace = GetTotalSpace();
        }

        /**
         * Returns the layout space change between the previous layout pass and current layout pass.
         * <p>
         * Make sure you call {@link #onLayoutComplete()} at the end of your LayoutManager's
         * {@link RecyclerView.LayoutManager#onLayoutChildren(RecyclerView.Recycler,
         * RecyclerView.State)} method.
         *
         * @return The difference between the current total space and previous layout's total space.
         * @see #onLayoutComplete()
         */
        public float GetTotalSpaceChange()
        {
            return INVALID_SIZE == mLastTotalSpace ? 0 : GetTotalSpace() - mLastTotalSpace;
        }

        /**
         * Returns the start of the view including its decoration and margin.
         * <p>
         * For example, for the horizontal helper, if a View's left is at pixel 20, has 2px left
         * decoration and 3px left margin, returned value will be 15px.
         *
         * @param view The view element to check
         * @return The first pixel of the element
         * @see #getDecoratedEnd(android.view.View)
         */
        public abstract float GetViewHolderStart(FlexibleView.ViewHolder holder);

        /**
         * Returns the end of the view including its decoration and margin.
         * <p>
         * For example, for the horizontal helper, if a View's right is at pixel 200, has 2px right
         * decoration and 3px right margin, returned value will be 205.
         *
         * @param view The view element to check
         * @return The last pixel of the element
         * @see #getDecoratedStart(android.view.View)
         */
        public abstract float GetViewHolderEnd(FlexibleView.ViewHolder holder);

        /**
         * Returns the space occupied by this View in the current orientation including decorations and
         * margins.
         *
         * @param view The view element to check
         * @return Total space occupied by this view
         * @see #getDecoratedMeasurementInOther(View)
         */
        public abstract float GetViewHolderMeasurement(FlexibleView.ViewHolder holder);

        /**
         * Returns the space occupied by this View in the perpendicular orientation including
         * decorations and margins.
         *
         * @param view The view element to check
         * @return Total space occupied by this view in the perpendicular orientation to current one
         * @see #getDecoratedMeasurement(View)
         */
        public abstract float GetViewHolderMeasurementInOther(FlexibleView.ViewHolder holder);

        /**
         * Returns the start position of the layout after the start padding is added.
         *
         * @return The very first pixel we can draw.
         */
        public abstract float GetStartAfterPadding();

        /**
         * Returns the end position of the layout after the end padding is removed.
         *
         * @return The end boundary for this layout.
         */
        public abstract float GetEndAfterPadding();

        /**
         * Returns the end position of the layout without taking padding into account.
         *
         * @return The end boundary for this layout without considering padding.
         */
        public abstract float GetEnd();

        /**
         * Offsets all children's positions by the given amount.
         *
         * @param amount Value to add to each child's layout parameters
         */
        public abstract void OffsetChildren(float amount);

        /**
         * Returns the total space to layout. This number is the difference between
         * {@link #getEndAfterPadding()} and {@link #getStartAfterPadding()}.
         *
         * @return Total space to layout children
         */
        public abstract float GetTotalSpace();

        /**
         * Offsets the child in this orientation.
         *
         * @param view   View to offset
         * @param offset offset amount
         */
        public abstract void OffsetChild(FlexibleView.ViewHolder holder, int offset);

        /**
         * Returns the padding at the end of the layout. For horizontal helper, this is the right
         * padding and for vertical helper, this is the bottom padding. This method does not check
         * whether the layout is RTL or not.
         *
         * @return The padding at the end of the layout.
         */
        public abstract float GetEndPadding();

        /**
         * Creates an OrientationHelper for the given LayoutManager and orientation.
         *
         * @param layoutManager LayoutManager to attach to
         * @param orientation   Desired orientation. Should be {@link #HORIZONTAL} or {@link #VERTICAL}
         * @return A new OrientationHelper
         */
        public static OrientationHelper createOrientationHelper(
                FlexibleView.LayoutManager layoutManager, int orientation)
        {
            if (orientation == HORIZONTAL)
            {
                return CreateHorizontalHelper(layoutManager);
            }
            else if (orientation == VERTICAL)
            {
                return CreateVerticalHelper(layoutManager);
            }
            
            throw new ArgumentException("invalid orientation");
        }


        /**
         * Creates a horizontal OrientationHelper for the given LayoutManager.
         *
         * @param layoutManager The LayoutManager to attach to.
         * @return A new OrientationHelper
         */
        public static OrientationHelper CreateHorizontalHelper(FlexibleView.LayoutManager layoutManager)
        {
            return new HorizontalHelper(layoutManager);

        }
        /**
        * Creates a vertical OrientationHelper for the given LayoutManager.
        *
        * @param layoutManager The LayoutManager to attach to.
        * @return A new OrientationHelper
        */
        public static OrientationHelper CreateVerticalHelper(FlexibleView.LayoutManager layoutManager)
        {
            return new VerticalHelper(layoutManager);
        }
    }

    public class HorizontalHelper : OrientationHelper
    {
        public HorizontalHelper(FlexibleView.LayoutManager layoutManager): base(layoutManager)
        {

        }

        public override float GetEndAfterPadding()
        {
            return mLayoutManager.GetWidth() - mLayoutManager.GetPaddingRight();
        }

        public override float GetEnd()
        {
            return mLayoutManager.GetWidth();
        }

        public override void OffsetChildren(float amount)
        {
            mLayoutManager.OffsetChildrenHorizontal(amount);
        }


        public override float GetStartAfterPadding()
        {
            return mLayoutManager.GetPaddingLeft();
        }

        public override float GetViewHolderMeasurement(FlexibleView.ViewHolder holder)
        {
            return holder.SizeWidth;
        }

        public override float GetViewHolderMeasurementInOther(FlexibleView.ViewHolder holder)
        {
            return holder.SizeHeight;
        }

        public override float GetViewHolderEnd(FlexibleView.ViewHolder holder)
        {
            return holder.PositionX + holder.SizeWidth;
        }

        public override float GetViewHolderStart(FlexibleView.ViewHolder holder)
        {
            return holder.PositionX;
        }

        public override float GetTotalSpace()
        {
            return mLayoutManager.GetWidth() - mLayoutManager.GetPaddingLeft()
                    - mLayoutManager.GetPaddingRight();
        }

        public override void OffsetChild(FlexibleView.ViewHolder holder, int offset)
        {
            //view.offsetLeftAndRight(offset);
        }

        public override float GetEndPadding()
        {
            return mLayoutManager.GetPaddingRight();
        }

    }

    public class VerticalHelper : OrientationHelper
    {
        public VerticalHelper(FlexibleView.LayoutManager layoutManager) : base(layoutManager)
        {

        }

        public override float GetEndAfterPadding()
        {
            return mLayoutManager.GetHeight() - mLayoutManager.GetPaddingBottom();
        }

        public override float GetEnd()
        {
            return mLayoutManager.GetHeight();
        }

        public override void OffsetChildren(float amount)
        {
            mLayoutManager.OffsetChildrenVertical(amount);
        }

        public override float GetStartAfterPadding()
        {
            return mLayoutManager.GetPaddingTop();
        }

        public override float GetViewHolderMeasurement(FlexibleView.ViewHolder holder)
        {
            return holder.SizeHeight;
        }

        public override float GetViewHolderMeasurementInOther(FlexibleView.ViewHolder holder)
        {
            return holder.SizeWidth;
        }

        public override float GetViewHolderEnd(FlexibleView.ViewHolder holder)
        {
            return holder.PositionY + holder.SizeHeight;
        }

        public override float GetViewHolderStart(FlexibleView.ViewHolder holder)
        {
            return holder.PositionY;
        }

        public override float GetTotalSpace()
        {
            return mLayoutManager.GetHeight() - mLayoutManager.GetPaddingTop()
                    - mLayoutManager.GetPaddingBottom();
        }

        public override void OffsetChild(FlexibleView.ViewHolder view, int offset)
        {
            //view.offsetTopAndBottom(offset);
        }

        public override float GetEndPadding()
        {
            return mLayoutManager.GetPaddingBottom();
        }

    }

}
