using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class DataGridViewX : DataGridView, IDataGridViewX
    {
        #region IBaseItem
        WFNew.RenderStyle m_eRenderStyle = WFNew.RenderStyle.eSystem;
        [Browsable(true), DefaultValue(typeof(WFNew.RenderStyle), "eSystem"), Description("��Ⱦ����"), Category("���")]
        public virtual WFNew.RenderStyle eRenderStyle
        {
            get { return m_eRenderStyle; }
            set { m_eRenderStyle = value; }
        }

        private WFNew.BaseItemState m_eBaseItemState = WFNew.BaseItemState.eNormal;
        protected virtual bool SetBaseItemState(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            return true;
        }
        protected virtual bool SetBaseItemStateEx(WFNew.BaseItemState baseItemState)
        {
            if (this.m_eBaseItemState == baseItemState) return false;
            this.m_eBaseItemState = baseItemState;
            this.Refresh();
            return true;
        }
        [Browsable(false), Description("����������״̬��������¡������á�������"), Category("״̬")]
        public virtual WFNew.BaseItemState eBaseItemState
        {
            get
            {
                return m_eBaseItemState;
            }
        }
        #endregion

        #region IOwner
        [Browsable(false), Description("��ȡ��ӵ����"), Category("����")]
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        [Browsable(false),  Description("�Ƿ���ʾ������"), Category("���")]
        bool WinForm.IItemOwner.ShowGripRegion
        {
            get { return false; }
        }

        [Browsable(false), Description("ֵΪ��ɫ��ʱ��������ɫ������Ŀ��"), Category("����")]
        int WinForm.IItemOwner.ColorRegionWidth
        {
            get { return -1; }
        }

        [Browsable(false), Description("ͼƬ����������Ŀ��"), Category("����")]
        int WinForm.IItemOwner.ImageGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        [Browsable(false), Description("����˻���������Ŀ��"), Category("����")]
        int WinForm.IItemOwner.LeftGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        [Browsable(false), Description("��¼���Ļ���״̬"), Category("���")]
        WinForm.ItemDrawStyle WinForm.IItemOwner.eItemDrawStyle
        {
            get { return GISShare.Controls.WinForm.ItemDrawStyle.eSimply; }
        }
        #endregion

        #region IDataGridViewX
        bool m_AutoMouseMoveSeleced = false;
        [Browsable(true), DefaultValue(false), Description("����ƶ��켴ѡ��������"), Category("״̬")]
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        [Browsable(false), Description("��������"), Category("����")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }
        #endregion

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            Point point = this.PointToClient(Form.MousePosition);
            //
            WFNew.BaseItemState eBaseItemState = (this.AutoMouseMoveSeleced && e.CellBounds.Contains(point)) ? GISShare.Controls.WinForm.WFNew.BaseItemState.eHot : GISShare.Controls.WinForm.WFNew.BaseItemState.eNormal;
            //
            Rectangle rectangle = Rectangle.FromLTRB(e.CellBounds.Left + 1, e.CellBounds.Top + 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
            Rectangle rectangle2 = Rectangle.FromLTRB(rectangle.Left + 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 1);
            Rectangle rectangle3 = Rectangle.FromLTRB(rectangle2.Left + 1, rectangle2.Top + 1, rectangle2.Right - 1, rectangle2.Bottom - 1);
            if (e.ColumnIndex == -1 && e.RowIndex == -1)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderDataGridViewCellItem
                    (
                    new ItemRenderEventArgs
                        (
                        e.Graphics,
                        e,
                        this,
                        CheckState.Unchecked,
                        eBaseItemState,
                        rectangle3,
                        rectangle2,
                        rectangle,
                        e.CellBounds
                        )
                   );
            }
            else if (e.RowIndex == -1)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderDataGridViewCellItem
                   (
                   new ItemRenderEventArgs
                       (
                       e.Graphics,
                       e,
                       this,
                       this.ContainsColumnIndex(e.ColumnIndex) ? CheckState.Checked : CheckState.Unchecked,
                       eBaseItemState,
                       rectangle3,
                       rectangle2,
                       rectangle,
                       e.CellBounds
                       )
                  );
            }
            else if (e.ColumnIndex == -1)
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderDataGridViewCellItem
                  (
                  new ItemRenderEventArgs
                      (
                      e.Graphics,
                      e,
                      this,
                      (this.CurrentRow != null && e.RowIndex == this.CurrentRow.Index) ? CheckState.Checked : CheckState.Unchecked,
                      eBaseItemState,
                      rectangle3,
                      rectangle2,
                      rectangle,
                      e.CellBounds
                      )
                 );
            }
            else
            {
                GISShare.Controls.WinForm.WinFormRenderer.WinFormRendererStrategy.OnRenderDataGridViewCellItem
                     (
                     new ItemRenderEventArgs
                         (
                         e.Graphics,
                         e,
                         this,
                         (e.State == (DataGridViewElementStates.Displayed | DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) ? CheckState.Checked : CheckState.Unchecked,
                         eBaseItemState,
                         rectangle3,
                         rectangle2,
                         rectangle,
                         e.CellBounds
                         )
                    );
            }
            //
            base.OnCellPainting(e);
        }
        private bool ContainsColumnIndex(int iColumnIndex)
        {
            foreach (DataGridViewCell one in this.SelectedCells)
            {
                if (one.ColumnIndex == iColumnIndex) return true;
            }
            //
            return false;
        }
        private bool ContainsRowIndex(int iRowIndex)
        {
            foreach (DataGridViewCell one in this.SelectedCells)
            {
                if (one.RowIndex == iRowIndex) return true;
            }
            //
            return false;
        }

        protected override void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.InvalidateCell(e.ColumnIndex, e.RowIndex);
            base.OnCellMouseEnter(e);
        }

        protected override void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.InvalidateCell(e.ColumnIndex, e.RowIndex);
            base.OnCellLeave(e);
        }

        protected override void OnCellStateChanged(DataGridViewCellStateChangedEventArgs e)
        {
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.InvalidateColumn(i);
            }
            base.OnCellStateChanged(e);
        }
    }
}
