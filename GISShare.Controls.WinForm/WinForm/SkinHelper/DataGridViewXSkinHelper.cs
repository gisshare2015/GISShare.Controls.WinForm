using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public class DataGridViewXSkinHelper : ControlSkinHelper, IDataGridViewX
    {
        private DataGridView m_HostDataGridView;

        public DataGridViewXSkinHelper(DataGridView hostDataGridView)
            : base(hostDataGridView)
        {
            this.m_HostDataGridView = hostDataGridView;
            //
            this.m_HostDataGridView.CellPainting += new DataGridViewCellPaintingEventHandler(HostDataGridView_CellPainting);
            this.m_HostDataGridView.CellStateChanged += new DataGridViewCellStateChangedEventHandler(HostDataGridView_CellStateChanged);
            this.m_HostDataGridView.CellMouseEnter += new DataGridViewCellEventHandler(HostDataGridView_CellMouseEnter);
            this.m_HostDataGridView.CellMouseLeave += new DataGridViewCellEventHandler(HostDataGridView_CellMouseLeave);
        }
        void HostDataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
        }
        private bool ContainsColumnIndex(int iColumnIndex)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return false;
            //
            foreach (DataGridViewCell one in this.m_HostDataGridView.SelectedCells)
            {
                if (one.ColumnIndex == iColumnIndex) return true;
            }
            //
            return false;
        }
        private bool ContainsRowIndex(int iRowIndex)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return false;
            //
            foreach (DataGridViewCell one in this.m_HostDataGridView.SelectedCells)
            {
                if (one.RowIndex == iRowIndex) return true;
            }
            //
            return false;
        }
        void HostDataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }
        void HostDataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.AutoMouseMoveSeleced) this.InvalidateCell(e.ColumnIndex, e.RowIndex);
        }
        void HostDataGridView_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.InvalidateColumn(i);
            }
        }

        #region 注册
        protected override void UnregisterEventHandlers(Control hostControl)
        {
            base.UnregisterEventHandlers(hostControl);
            //
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return;
            //
            this.m_HostDataGridView.CellPainting -= new DataGridViewCellPaintingEventHandler(HostDataGridView_CellPainting);
            this.m_HostDataGridView.CellStateChanged -= new DataGridViewCellStateChangedEventHandler(HostDataGridView_CellStateChanged);
            this.m_HostDataGridView.CellMouseEnter -= new DataGridViewCellEventHandler(HostDataGridView_CellMouseEnter);
            this.m_HostDataGridView.CellMouseLeave -= new DataGridViewCellEventHandler(HostDataGridView_CellMouseLeave);
        }
        #endregion

        #region IOwner
        public virtual WFNew.IOwner pOwner { get { return null; } }
        #endregion

        #region IItemOwner
        bool WinForm.IItemOwner.ShowGripRegion
        {
            get { return false; }
        }

        int WinForm.IItemOwner.ColorRegionWidth
        {
            get { return -1; }
        }

        int WinForm.IItemOwner.ImageGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        int WinForm.IItemOwner.LeftGripRegionWidth
        {
            get
            {
                return -1;
            }
        }

        WinForm.ItemDrawStyle WinForm.IItemOwner.eItemDrawStyle
        {
            get { return GISShare.Controls.WinForm.ItemDrawStyle.eSimply; }
        }
        #endregion

        #region IDataGridViewX
        bool m_AutoMouseMoveSeleced = false;
        public bool AutoMouseMoveSeleced
        {
            get { return m_AutoMouseMoveSeleced; }
            set { m_AutoMouseMoveSeleced = value; }
        }

        [Browsable(false), Description("其外框矩形"), Category("布局")]
        public Rectangle FrameRectangle
        {
            get
            {
                return new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }
        }

        //

        public BorderStyle BorderStyle
        {
            get
            {
                if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return BorderStyle.Fixed3D;
                return this.m_HostDataGridView.BorderStyle;
            }
        }

        public DataGridViewCell CurrentCell
        {
            get
            {
                if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return null;
                return this.m_HostDataGridView.CurrentCell;
            }
        }

        public DataGridViewRow CurrentRow
        {
            get
            {
                if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return null;
                return this.m_HostDataGridView.CurrentRow;
            }
        }

        public DataGridViewColumnCollection Columns
        {
            get
            {
                if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return null;
                return this.m_HostDataGridView.Columns;
            }
        }

        public DataGridViewRowCollection Rows
        {
            get
            {
                if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return null;
                return this.m_HostDataGridView.Rows;
            }
        }

        public void InvalidateCell(DataGridViewCell dataGridViewCell)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return;
            this.m_HostDataGridView.InvalidateCell(dataGridViewCell);
        }

        public void InvalidateCell(int columnIndex, int rowIndex)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return;
            this.m_HostDataGridView.InvalidateCell(columnIndex, rowIndex);
        }

        public void InvalidateColumn(int columnIndex)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return;
            this.m_HostDataGridView.InvalidateColumn(columnIndex);
        }

        public void InvalidateRow(int rowIndex)
        {
            if (this.m_HostDataGridView == null || this.m_HostDataGridView.IsDisposed) return;
            this.m_HostDataGridView.InvalidateRow(rowIndex);
        }
        #endregion
    }
}
