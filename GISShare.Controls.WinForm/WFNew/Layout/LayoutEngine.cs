using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew
{
    public class LayoutEngine
    {
        #region ���壨Canvas������

        #endregion

        #region ջ��BaseItemStack������
        //
        #region ջ���֣�˳�ƣ��������򶥶˿�ʼ����
        /// <summary>
        /// ˮƽ����ջ���֣���������
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pUICollectionItem">���϶���</param>
        /// <param name="topViewItemIndex">�׸����ƶ��������</param>
        /// <param name="bIsStretchItems">�Ƿ�ͳһ��������</param>
        /// <param name="bIsRestrictItems">�Ƿ�ͳһ��������</param>
        /// <param name="iColumnDistance">���</param>
        /// <param name="iRestrictItemsWidth">����ָ�����</param>
        /// <param name="iRestrictItemsHeight">����ָ���߶�</param>
        /// <param name="iLeftSpace">��߼��</param>
        /// <param name="iTopSpace">�������</param>
        /// <param name="iRightSpace">�ұ߼��</param>
        /// <param name="iBottomSpace">�ײ����</param>
        /// <param name="iMinSize">ˮƽ�������С�ߴ�</param>
        /// <param name="iMaxSize">ˮƽ��������ߴ�</param>
        /// <param name="eLayoutStyle">��������</param>
        /// <param name="refOverflowItemsCount">������������</param>
        /// <param name="outDrawItemsCount">�����������������</param>
        /// <returns>���ض����ʵ��ߴ�</returns>
        public static Size LayoutStackH_LT(Graphics g, IUICollectionItem pUICollectionItem, int topViewItemIndex,
            bool bIsStretchItems, bool bIsRestrictItems, int iColumnDistance, int iRestrictItemsWidth, int iRestrictItemsHeight,
            int iLeftSpace, int iTopSpace, int iRightSpace, int iBottomSpace,
            int iMinSize, int iMaxSize, LayoutStyle eLayoutStyle, ref int refOverflowItemsCount, ref int outDrawItemsCount)//
        {
            Rectangle rectangle = pUICollectionItem.ItemsRectangle;
            //
            int iCount = pUICollectionItem.BaseItems.Count;
            if (topViewItemIndex < 0) topViewItemIndex = 0;
            if (topViewItemIndex >= iCount) topViewItemIndex = iCount - 1;
            //
            outDrawItemsCount = 0;
            refOverflowItemsCount = topViewItemIndex - pUICollectionItem.BaseItems.GetItemCount(false, 0, topViewItemIndex);
            //
            int iNum = 0;
            int iWidth = rectangle.Left;
            int iHeight = 0;
            BaseItem item;
            //
            if (bIsStretchItems)
            {
                #region IsStretchItemsT
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2);
                        }
                    }
                }
                iTempW = 0;
                iTempH = 0;
                iWidth = rectangle.Left;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    if (refOverflowItemsCount > 0) { }
                    //
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2);
                        }
                    }
                    iWidth += item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                    iTempH = item.Margin.Top + iTempH + item.Margin.Bottom;
                    if (iTempH > iHeight) iHeight = iTempH;
                    //
                    if (iWidth - rectangle.Left - iColumnDistance > rectangle.Width) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iWidth -= rectangle.Left + iColumnDistance;
                }
                #endregion
            }
            else
            {
                #region IsStretchItemsF
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                }
                iTempW = 0;
                iTempH = 0;
                iWidth = rectangle.Left;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iWidth += item.Margin.Left + item.DisplayRectangle.Width + item.Margin.Right + iColumnDistance;
                    iTempH = item.Margin.Top + iTempH + item.Margin.Bottom;
                    if (iTempH > iHeight) iHeight = iTempH;
                    //
                    if (iWidth - rectangle.Left - iColumnDistance > rectangle.Width) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iWidth -= rectangle.Left + iColumnDistance;
                }
                #endregion
            }
            //
            iWidth += iLeftSpace + iRightSpace;
            iHeight += iTopSpace + iBottomSpace;
            //
            if (iMinSize > 0 && iMinSize <= iMaxSize && iWidth < iMinSize) iWidth = iMinSize;
            if (iMaxSize > 0 && iMinSize <= iMaxSize && iWidth > iMaxSize) iWidth = iMaxSize;
            //
            return new Size(iWidth, iHeight);
        }

        /// <summary>
        /// ��ֱ����ջ���֣���������
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pUICollectionItem">���϶���</param>
        /// <param name="topViewItemIndex">�׸����ƶ��������</param>
        /// <param name="bIsStretchItems">�Ƿ�ͳһ��������</param>
        /// <param name="bIsRestrictItems">�Ƿ�ͳһ��������</param>
        /// <param name="iColumnDistance">���</param>
        /// <param name="iRestrictItemsWidth">����ָ�����</param>
        /// <param name="iRestrictItemsHeight">����ָ���߶�</param>
        /// <param name="iLeftSpace">��߼��</param>
        /// <param name="iTopSpace">�������</param>
        /// <param name="iRightSpace">�ұ߼��</param>
        /// <param name="iBottomSpace">�ײ����</param>
        /// <param name="iMinSize">��ֱ�������С�ߴ�</param>
        /// <param name="iMaxSize">��ֱ��������ߴ�</param>
        /// <param name="eLayoutStyle">��������</param>
        /// <param name="refOverflowItemsCount">������������</param>
        /// <param name="outDrawItemsCount">�����������������</param>
        /// <returns>���ض����ʵ��ߴ�</returns>
        public static Size LayoutStackV_LT(Graphics g, IUICollectionItem pUICollectionItem, int topViewItemIndex,
            bool bIsStretchItems, bool bIsRestrictItems, int iLineDistance, int iRestrictItemsWidth, int iRestrictItemsHeight,
            int iLeftSpace, int iTopSpace, int iRightSpace, int iBottomSpace,
            int iMinSize, int iMaxSize, LayoutStyle eLayoutStyle, ref int refOverflowItemsCount, ref int outDrawItemsCount)
        {
            Rectangle rectangle = pUICollectionItem.ItemsRectangle;
            //
            int iCount = pUICollectionItem.BaseItems.Count;
            if (topViewItemIndex < 0) topViewItemIndex = 0;
            if (topViewItemIndex >= iCount) topViewItemIndex = iCount - 1;
            //
            outDrawItemsCount = 0;
            refOverflowItemsCount = topViewItemIndex - pUICollectionItem.BaseItems.GetItemCount(false, 0, topViewItemIndex);
            //
            int iNum = 0;
            int iWidth = 0;
            int iHeight = rectangle.Top;
            BaseItem item;
            //
            if (bIsStretchItems)
            {
                #region IsStretchItemsT
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top);
                        }
                    }
                }
                iTempW = 0;
                iTempH = 0;
                iHeight = rectangle.Top;
                //System.Diagnostics.Debug.WriteLine("--------------------" + iTempW + "-" + iWidth);
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                            //System.Diagnostics.Debug.WriteLine("0." + item.Name + ":" + iTempW + "-" + iWidth);
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top);
                        }
                    }
                    iHeight += item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                    //System.Diagnostics.Debug.WriteLine("1." + item.Name + ":" + iTempW + "-" + iWidth);
                    iTempW = item.Margin.Left + iTempW + item.Margin.Right;
                    //System.Diagnostics.Debug.WriteLine("2." + item.Name + ":" + iTempW + "-" + iWidth);
                    if (iTempW > iWidth) iWidth = iTempW;
                    //System.Diagnostics.Debug.WriteLine("3." + item.Name + ":" + iTempW + "-" + iWidth);
                    //
                    if (iHeight - rectangle.Top - iLineDistance > rectangle.Height) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iHeight -= rectangle.Top + iLineDistance;
                }
                #endregion
            }
            else
            {
                #region IsStretchItemsF
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                }
                iTempW = 0;
                iTempH = 0;
                iHeight = rectangle.Top;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iHeight += item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                    iTempW = item.Margin.Left + iTempW + item.Margin.Right;
                    if (iTempW > iHeight) iWidth = iTempW;
                    //
                    if (iHeight - rectangle.Top - iLineDistance > rectangle.Height) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iHeight -= rectangle.Top + iLineDistance;
                }
                #endregion
            }
            //
            iWidth += iLeftSpace + iRightSpace;
            iHeight += iTopSpace + iBottomSpace;
            //
            if (iMinSize > 0 && iMinSize <= iMaxSize && iHeight < iMinSize) iHeight = iMinSize;
            if (iMaxSize > 0 && iMinSize <= iMaxSize && iHeight > iMaxSize) iHeight = iMaxSize;
            //
            return new Size(iWidth, iHeight);
        }
        #endregion

        #region ջ���֣����򣩣����Ҳ��ײ���ʼ����
        /// <summary>
        /// ˮƽ����ջ���֣���������
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pUICollectionItem">���϶���</param>
        /// <param name="topViewItemIndex">�׸����ƶ��������</param>
        /// <param name="bIsStretchItems">�Ƿ�ͳһ��������</param>
        /// <param name="bIsRestrictItems">�Ƿ�ͳһ��������</param>
        /// <param name="iColumnDistance">���</param>
        /// <param name="iRestrictItemsWidth">����ָ�����</param>
        /// <param name="iRestrictItemsHeight">����ָ���߶�</param>
        /// <param name="iLeftSpace">��߼��</param>
        /// <param name="iTopSpace">�������</param>
        /// <param name="iRightSpace">�ұ߼��</param>
        /// <param name="iBottomSpace">�ײ����</param>
        /// <param name="iMinSize">ˮƽ�������С�ߴ�</param>
        /// <param name="iMaxSize">ˮƽ��������ߴ�</param>
        /// <param name="eLayoutStyle">��������</param>
        /// <param name="refOverflowItemsCount">������������</param>
        /// <param name="outDrawItemsCount">�����������������</param>
        /// <returns>���ض����ʵ��ߴ�</returns>
        public static Size LayoutStackH_RB(Graphics g, IUICollectionItem pUICollectionItem, int topViewItemIndex,
            bool bIsStretchItems, bool bIsRestrictItems, int iColumnDistance, int iRestrictItemsWidth, int iRestrictItemsHeight,
            int iLeftSpace, int iTopSpace, int iRightSpace, int iBottomSpace,
            int iMinSize, int iMaxSize, LayoutStyle eLayoutStyle, ref int refOverflowItemsCount, ref int outDrawItemsCount)
        {
            Rectangle rectangle = pUICollectionItem.ItemsRectangle;
            //
            int iCount = pUICollectionItem.BaseItems.Count;
            if (topViewItemIndex < 0) topViewItemIndex = 0;
            if (topViewItemIndex >= iCount) topViewItemIndex = iCount - 1;
            //
            outDrawItemsCount = 0;
            refOverflowItemsCount = topViewItemIndex - pUICollectionItem.BaseItems.GetItemCount(false, 0, topViewItemIndex);
            //
            int iNum = 0;
            int iWidth = rectangle.Right + iColumnDistance;
            int iHeight = 0;
            BaseItem item;
            //
            if (bIsStretchItems)
            {
                #region IsStretchItemsT
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2);
                        }
                    }
                    iWidth += item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                }
                iTempW = 0;
                iTempH = 0;
                iWidth = rectangle.Right + iColumnDistance;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2);
                        }
                    }
                    iTempH = item.Margin.Top + iTempH + item.Margin.Bottom;
                    if (iTempH > iHeight) iHeight = iTempH;
                    //
                    if (rectangle.Right - iWidth > rectangle.Width) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iWidth = rectangle.Right - iWidth;
                }
                #endregion
            }
            else
            {
                #region IsStretchItemsF
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iWidth += item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                }
                iTempW = 0;
                iTempH = 0;
                iWidth = rectangle.Right + iColumnDistance;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, rectangle.Height - item.Margin.Top - item.Margin.Bottom);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iWidth -= item.Margin.Left + iTempW + item.Margin.Right + iColumnDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eVAlignmentStyle)
                            {
                                case VAlignmentStyle.eBottom:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Bottom - item.Margin.Bottom - iTempH, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, (rectangle.Top + rectangle.Bottom - iTempH) / 2, iTempW, iTempH);
                                    break;
                                case VAlignmentStyle.eTop:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(iWidth + item.Margin.Left, rectangle.Top + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iTempH = item.Margin.Top + iTempH + item.Margin.Bottom;
                    if (iTempH > iHeight) iHeight = iTempH;
                    //
                    if (rectangle.Right - iWidth > rectangle.Width) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iWidth = rectangle.Right - iWidth;
                }
                #endregion
            }
            //
            iWidth += iLeftSpace + iRightSpace;
            iHeight += iTopSpace + iBottomSpace;
            //
            if (iMinSize > 0 && iMinSize <= iMaxSize && iWidth < iMinSize) iWidth = iMinSize;
            if (iMaxSize > 0 && iMinSize <= iMaxSize && iWidth > iMaxSize) iWidth = iMaxSize;
            //
            return new Size(iWidth, iHeight);
        }

        /// <summary>
        /// ��ֱ����ջ���֣���������
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pUICollectionItem">���϶���</param>
        /// <param name="topViewItemIndex">�׸����ƶ��������</param>
        /// <param name="bIsStretchItems">�Ƿ�ͳһ��������</param>
        /// <param name="bIsRestrictItems">�Ƿ�ͳһ��������</param>
        /// <param name="iColumnDistance">���</param>
        /// <param name="iRestrictItemsWidth">����ָ�����</param>
        /// <param name="iRestrictItemsHeight">����ָ���߶�</param>
        /// <param name="iLeftSpace">��߼��</param>
        /// <param name="iTopSpace">�������</param>
        /// <param name="iRightSpace">�ұ߼��</param>
        /// <param name="iBottomSpace">�ײ����</param>
        /// <param name="iMinSize">��ֱ�������С�ߴ�</param>
        /// <param name="iMaxSize">��ֱ��������ߴ�</param>
        /// <param name="eLayoutStyle">��������</param>
        /// <param name="refOverflowItemsCount">������������</param>
        /// <param name="outDrawItemsCount">�����������������</param>
        /// <returns>���ض����ʵ��ߴ�</returns>
        public static Size LayoutStackV_RB(Graphics g, IUICollectionItem pUICollectionItem, int topViewItemIndex,
            bool bIsStretchItems, bool bIsRestrictItems, int iLineDistance, int iRestrictItemsWidth, int iRestrictItemsHeight,
            int iLeftSpace, int iTopSpace, int iRightSpace, int iBottomSpace,
            int iMinSize, int iMaxSize, LayoutStyle eLayoutStyle, ref int refOverflowItemsCount, ref int outDrawItemsCount)
        {
            Rectangle rectangle = pUICollectionItem.ItemsRectangle;
            //
            int iCount = pUICollectionItem.BaseItems.Count;
            if (topViewItemIndex < 0) topViewItemIndex = 0;
            if (topViewItemIndex >= iCount) topViewItemIndex = iCount - 1;
            //
            outDrawItemsCount = 0;
            refOverflowItemsCount = topViewItemIndex - pUICollectionItem.BaseItems.GetItemCount(false, 0, topViewItemIndex);
            //
            int iNum = 0;
            int iWidth = 0;
            int iHeight = rectangle.Bottom + iLineDistance;
            BaseItem item;
            //
            if (bIsStretchItems)
            {
                #region IsStretchItemsT
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top);
                        }
                    }
                    iHeight += item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                }
                iTempW = 0;
                iTempH = 0;
                iHeight = rectangle.Bottom + iLineDistance;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            ((ISetBaseItemHelper)item).SetLocation((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top);
                        }
                    }
                    iTempW = item.Margin.Left + iTempW + item.Margin.Right;
                    if (iTempW > iWidth) iWidth = iTempW;
                    //
                    if (iHeight - iLineDistance > rectangle.Height) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iHeight = rectangle.Bottom - iHeight;
                }
                #endregion
            }
            else
            {
                #region IsStretchItemsF
                int iTempW = 0;
                int iTempH = 0;
                for (int i = topViewItemIndex - 1; i >= 0; i--)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iHeight += item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                }
                iTempW = 0;
                iTempH = 0;
                iHeight = rectangle.Bottom + iLineDistance;
                for (int i = topViewItemIndex; i < iCount; i++)
                {
                    item = pUICollectionItem.BaseItems[i];
                    if (item == null || !item.Visible) continue;
                    iNum++;
                    //
                    if (!item.LockHeight && !item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockWith)
                    {
                        if (bIsRestrictItems)
                        {
                            iTempW = iRestrictItemsWidth > 0 ? iRestrictItemsWidth : item.MeasureSize(g).Width;
                        }
                        else
                        {
                            iTempW = item.DisplayRectangle.Width;
                        }
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, rectangle.Width - item.Margin.Left - item.Margin.Right, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else if (!item.LockHeight)
                    {
                        iTempW = item.DisplayRectangle.Width;
                        if (bIsRestrictItems)
                        {
                            iTempH = iRestrictItemsHeight > 0 ? iRestrictItemsHeight : item.MeasureSize(g).Height;
                        }
                        else
                        {
                            iTempH = item.DisplayRectangle.Height;
                        }
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        iTempW = item.DisplayRectangle.Width;
                        iTempH = item.DisplayRectangle.Height;
                        //
                        iHeight -= item.Margin.Top + iTempH + item.Margin.Bottom + iLineDistance;
                        if ((eLayoutStyle == LayoutStyle.eLayout) || (eLayoutStyle == LayoutStyle.eLayoutAuto && pUICollectionItem.BaseItems.OwnerEquals(item.pOwner)))
                        {
                            switch (item.eHAlignmentStyle)
                            {
                                case HAlignmentStyle.eRight:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Right - item.Margin.Right - iTempW, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eCenter:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eStretch:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle((rectangle.Left + rectangle.Right - iTempW) / 2, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                                case HAlignmentStyle.eLeft:
                                default:
                                    ((ISetBaseItemHelper)item).SetDisplayRectangle(rectangle.Left + item.Margin.Left, iHeight + item.Margin.Top, iTempW, iTempH);
                                    break;
                            }
                        }
                    }
                    iTempW = item.Margin.Left + iTempW + item.Margin.Right;
                    if (iTempW > iHeight) iWidth = iTempW;
                    //
                    if (iHeight - iLineDistance > rectangle.Height) { refOverflowItemsCount++; }
                    else { outDrawItemsCount++; }
                }
                //
                if (iNum > 0)
                {
                    iHeight = rectangle.Right + iHeight;
                }
                #endregion
            }
            //
            iWidth += iLeftSpace + iRightSpace;
            iHeight += iTopSpace + iBottomSpace;
            //
            if (iMinSize > 0 && iMinSize <= iMaxSize && iHeight < iMinSize) iHeight = iMinSize;
            if (iMaxSize > 0 && iMinSize <= iMaxSize && iHeight > iMaxSize) iHeight = iMaxSize;
            //
            return new Size(iWidth, iHeight);
        }
        #endregion
        //
        #endregion

        /// <summary>
        /// ��ֱ����ջ����ֻ�ʺ��б��Ͳ��֣��磺ListBox��Tree��
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pViewListEnumerator">����ö�ٳ�Ա</param>
        /// <param name="rectangle">���ֿ�</param>
        /// <param name="topViewItemIndex">��������������</param>
        /// <param name="iLeftOffset">���ƫ��</param>
        /// <param name="iRightOffset">�Ҳ�ƫ��</param>
        /// <param name="bottomViewItemIndex">���Ƶײ�������</param>
        /// <param name="refHScrollBarMaximum">�����</param>
        /// <returns>���ؼ�������</returns>
        public static int LayoutStackV_ListBox(Graphics g, View.IViewListEnumerator pViewListEnumerator, Rectangle rectangle,
            int topViewItemIndex, int iLeftOffset, int iRightOffset, ref int bottomViewItemIndex, ref int refHScrollBarMaximum)
        {
            if (iRightOffset == 0) iRightOffset = 0;
            //
            refHScrollBarMaximum = 0;
            //
            int iW_W;
            View.IViewItem viewItem = null;
            Size viewItemSize;
            int iItemHeight = 0;
            bottomViewItemIndex = topViewItemIndex;
            int iCount = pViewListEnumerator.GetViewItemCount();
            View.ISetViewItemHelper pSetViewItemHelper;
            for (; bottomViewItemIndex < iCount; bottomViewItemIndex++)
            {
                viewItem = pViewListEnumerator.GetViewItem(bottomViewItemIndex);
                if (viewItem != null)
                {
                    viewItemSize = viewItem.MeasureSize(g);
                    iW_W = viewItemSize.Width - rectangle.Width;
                    if (iW_W > refHScrollBarMaximum) refHScrollBarMaximum = iW_W;
                    //
                    pSetViewItemHelper = viewItem as View.ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle
                            (
                            Rectangle.FromLTRB
                               (
                               rectangle.Left - iLeftOffset,
                               rectangle.Top + iItemHeight,
                               rectangle.Right + iRightOffset,
                               rectangle.Top + iItemHeight + viewItemSize.Height
                               )
                            );
                    }
                    //
                    iItemHeight += viewItem.DisplayRectangle.Height;
                    if (iItemHeight >= rectangle.Height) break;
                }
            }
            for (int i = bottomViewItemIndex; i < iCount; i++)
            {
                viewItem = pViewListEnumerator.GetViewItem(i);
                if (viewItem != null)
                {
                    viewItemSize = viewItem.MeasureSize(g);
                    iW_W = viewItemSize.Width - rectangle.Width;
                    if (iW_W > refHScrollBarMaximum) refHScrollBarMaximum = iW_W;
                }
            }
            //
            return iCount;
        }

        /// <summary>
        /// ˮƽ����ջ����ֻ�ʺ�����ͼ���֣��磺RowView��
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pViewListEnumerator">����ö�ٳ�Ա</param>
        /// <param name="viewItemsRectangle">��ͼ����</param>
        /// <param name="rectangle">���ƾ���</param>
        /// <param name="refTopViewItemIndex">��������������</param>
        /// <param name="refBottomViewItemIndex">�ײ�����������</param>
        /// <returns>���ؼ�������</returns>
        public static int LayoutStackH_Row(Graphics g, View.IViewListEnumerator pViewListEnumerator, 
            Rectangle viewItemsRectangle, Rectangle rectangle,
            ref int refTopViewItemIndex, ref int refBottomViewItemIndex)
        {
            int iW = 0;
            int iOffsetX = 0;
            //
            View.IViewItem viewItem = null;
            int iCount = pViewListEnumerator.GetViewItemCount();
            int iLeftX = 0;
            int iRightX = 0;
            int iLeftIndex = 0;
            int iRightIndex = iCount;
            for (int i = 0; i < iCount; i++)
            {
                viewItem = pViewListEnumerator.GetViewItem(i);
                //
                if (viewItem is View.IVisibleViewItem && !((View.IVisibleViewItem)viewItem).Visible) continue;
                //
                iW = viewItem.MeasureSize(g).Width;
                iLeftX = rectangle.Left + iOffsetX;
                iRightX = iLeftX + iW;
                if (iRightX <= viewItemsRectangle.Left)
                {
                    iLeftIndex++;
                }
                else if (iLeftX >= viewItemsRectangle.Right)
                {
                    iRightIndex--;
                }
                else
                {
                    View.ISetViewItemHelper pSetViewItemHelper = viewItem as View.ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(iLeftX, rectangle.Top, iW, rectangle.Height));
                    }
                }
                //
                iOffsetX += iW;
            }
            //
            refTopViewItemIndex = iLeftIndex;
            refBottomViewItemIndex = iRightIndex - 1;
            //
            return iCount;
        }

        /// <summary>
        /// ��ֱ����ջ����ֻ�ʺ�����ͼ���֣��磺VRowView��
        /// </summary>
        /// <param name="g">���ƶ���</param>
        /// <param name="pViewListEnumerator">����ö�ٳ�Ա</param>
        /// <param name="viewItemsRectangle">��ͼ����</param>
        /// <param name="rectangle">���ƾ���</param>
        /// <param name="refTopViewItemIndex">��������������</param>
        /// <param name="refBottomViewItemIndex">�ײ�����������</param>
        /// <returns>���ؼ�������</returns>
        public static int LayoutStackV_Row(Graphics g, View.IViewListEnumerator pViewListEnumerator,
            Rectangle viewItemsRectangle, Rectangle rectangle,
            ref int refTopViewItemIndex, ref int refBottomViewItemIndex)
        {
            int iH = 0;
            int iOffsetY = 0;
            //
            View.IViewItem viewItem = null;
            int iCount = pViewListEnumerator.GetViewItemCount();
            int iTopY = 0;
            int iBottomY = 0;
            int iLeftIndex = 0;
            int iRightIndex = iCount;
            for (int i = 0; i < iCount; i++)
            {
                viewItem = pViewListEnumerator.GetViewItem(i);
                //
                if (viewItem is View.IVisibleViewItem && !((View.IVisibleViewItem)viewItem).Visible) continue;
                //
                iH = viewItem.MeasureSize(g).Height;
                iTopY = rectangle.Top + iOffsetY;
                iBottomY = iTopY + iH;
                if (iBottomY <= viewItemsRectangle.Top)
                {
                    iLeftIndex++;
                }
                else if (iTopY >= viewItemsRectangle.Bottom)
                {
                    iRightIndex--;
                }
                else
                {
                    View.ISetViewItemHelper pSetViewItemHelper = viewItem as View.ISetViewItemHelper;
                    if (pSetViewItemHelper != null)
                    {
                        pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(rectangle.Left, iTopY, rectangle.Width, iH));
                    }
                }
                //
                iOffsetY += iH;
            }
            //
            refTopViewItemIndex = iLeftIndex;
            refBottomViewItemIndex = iRightIndex - 1;
            //
            return iCount;
        }
        
        //public static int LayoutStackH_Row_X(Graphics g, View.IViewListEnumerator pViewListEnumerator,
        //    Rectangle viewItemsRectangle, Rectangle rectangle,
        //    ref int refTopViewItemIndex, ref int refBottomViewItemIndex)
        //{
        //    int iW = 0;
        //    int iOffsetX = 0;
        //    //
        //    View.IViewItem viewItem = null;
        //    int iCount = pViewListEnumerator.GetViewItemCount();
        //    int iLeftX = 0;
        //    int iRightX = 0;
        //    int iLeftIndex = 0;
        //    int iRightIndex = iCount;
        //    for (int i = 0; i < iCount; i++)
        //    {
        //        viewItem = pViewListEnumerator.GetViewItem(i);
        //        //
        //        if (viewItem is View.IVisibleViewItem && !((View.IVisibleViewItem)viewItem).Visible) continue;
        //        //
        //        iW = viewItem.MeasureSize(g).Width;
        //        iLeftX = rectangle.Left + iOffsetX;
        //        iRightX = iLeftX + iW;
        //        if (iRightX <= viewItemsRectangle.Left)
        //        {
        //            iLeftIndex++;
        //        }
        //        else if (iLeftX >= viewItemsRectangle.Right)
        //        {
        //            iRightIndex--;
        //        }
        //        else
        //        {
        //            View.ISetViewItemHelper pSetViewItemHelper = viewItem as View.ISetViewItemHelper;
        //            if (pSetViewItemHelper != null)
        //            {
        //                pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(iLeftX, rectangle.Top, iW, rectangle.Height));
        //            }
        //        }
        //        //
        //        iOffsetX += iW;
        //    }
        //    //
        //    refTopViewItemIndex = iLeftIndex;
        //    refBottomViewItemIndex = iRightIndex - 1;
        //    //
        //    return iCount;
        //}

        //public static int LayoutStackV_Row_X(Graphics g, View.IViewListEnumerator pViewListEnumerator,
        //    Rectangle viewItemsRectangle, Rectangle rectangle,
        //    ref int refTopViewItemIndex, ref int refBottomViewItemIndex)
        //{
        //    int iH = 0;
        //    int iOffsetY = 0;
        //    //
        //    View.IViewItem viewItem = null;
        //    int iCount = pViewListEnumerator.GetViewItemCount();
        //    int iTopY = 0;
        //    int iBottomY = 0;
        //    int iLeftIndex = 0;
        //    int iRightIndex = iCount;
        //    for (int i = 0; i < iCount; i++)
        //    {
        //        viewItem = pViewListEnumerator.GetViewItem(i);
        //        //
        //        if (viewItem is View.IVisibleViewItem && !((View.IVisibleViewItem)viewItem).Visible) continue;
        //        //
        //        iH = viewItem.MeasureSize(g).Height;
        //        iTopY = rectangle.Top + iOffsetY;
        //        iBottomY = iTopY + iH;
        //        if (iBottomY <= viewItemsRectangle.Top)
        //        {
        //            iLeftIndex++;
        //        }
        //        else if (iTopY >= viewItemsRectangle.Bottom)
        //        {
        //            iRightIndex--;
        //        }
        //        else
        //        {
        //            View.ISetViewItemHelper pSetViewItemHelper = viewItem as View.ISetViewItemHelper;
        //            if (pSetViewItemHelper != null)
        //            {
        //                pSetViewItemHelper.SetViewItemDisplayRectangle(new Rectangle(rectangle.Left, iTopY, rectangle.Width, iH));
        //            }
        //        }
        //        //
        //        iOffsetY += iH;
        //    }
        //    //
        //    refTopViewItemIndex = iLeftIndex;
        //    refBottomViewItemIndex = iRightIndex - 1;
        //    //
        //    return iCount;
        //}
    }
}
