using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class ToolTipPopupPanelItem : BaseItemStackExItem, IToolTipPopupPanelItem
    {
        private ImageLabelItem m_ImageLabelItem;
        private LabelItem m_LabelItem;

        public ToolTipPopupPanelItem()
        {
            this.LineDistance = 3;
            this.Padding = new System.Windows.Forms.Padding(6, 6, 5, 3);
            this.eOrientation = System.Windows.Forms.Orientation.Vertical;
            this.IsRestrictItems = true;
            this.LockHeight = true;
            this.LockWith = true;
            //
            //
            //
            this.m_ImageLabelItem = new ImageLabelItem();
            this.m_ImageLabelItem.ITSpace = 3;
            this.m_ImageLabelItem.LockHeight = true;
            this.m_ImageLabelItem.LockWith = false;
            this.m_ImageLabelItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_ImageLabelItem.Size = new Size(19, 19);
            this.m_ImageLabelItem.eImageSizeStyle = ImageSizeStyle.eSystem;
            this.m_ImageLabelItem.ImageAlign = ContentAlignment.MiddleLeft;
            this.m_ImageLabelItem.TextAlign = ContentAlignment.MiddleLeft;
            this.BaseItems.Add(this.m_ImageLabelItem);
            //
            this.m_LabelItem = new LabelItem();
            this.m_LabelItem.LockHeight = true;
            this.m_LabelItem.LockWith = false;
            this.m_LabelItem.TextAlign = ContentAlignment.MiddleLeft;
            this.m_LabelItem.Size = new Size(16, 16);
            this.BaseItems.Add(this.m_LabelItem);
            //
            //
            //
            ((ILockCollectionHelper)this.BaseItems).SetLocked(true);
        }

        public override bool ShowBackgroud
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowBackgroud = value;
            }
        }

        public override bool ShowOutLine
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowOutLine = value;
            }
        }

        #region IPopupPanel
        private Control m_Entity;
        /// <summary>
        /// 依附实体
        /// </summary>
        [Browsable(false), Description("Popup依附实体"), Category("属性")]
        public Control Entity
        {
            get { return m_Entity; }
            set { m_Entity = value; }
        }

        public void TrySetPopupPanelSize(Size size)
        {
            this.Size = size;
            if (this.m_Entity != null) this.m_Entity.Size = size;
        }
        #endregion

        #region IToolTipPopupPanelItem
        private ITipInfo m_pTipInfo;
        public ITipInfo TipInfo
        {
            get { return m_pTipInfo; }
        }

        public bool SetTipInfo(ITipInfo pTipInfo)
        {
            if (this.m_pTipInfo != pTipInfo)
            {
                this.m_pTipInfo = pTipInfo;
                //
                this.m_ImageLabelItem.Image = this.m_pTipInfo.TitleImage;
                this.m_ImageLabelItem.Text = this.m_pTipInfo.TitleText;
                this.m_ImageLabelItem.Visible = this.m_ImageLabelItem.Image != null || (this.m_ImageLabelItem.Text != null && this.m_ImageLabelItem.Text.Length > 0);
                this.m_LabelItem.Text = this.m_pTipInfo.TipInfoText;
                this.m_LabelItem.Visible = this.m_LabelItem.Text != null && this.m_LabelItem.Text.Length > 0;
                //
                if (this.m_ImageLabelItem.Visible && this.m_LabelItem.Visible)
                {
                    this.Padding = new System.Windows.Forms.Padding(6, 6, 5, 3);
                }
                else
                {
                    this.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
                }
            }
            //
            return this.m_ImageLabelItem.Visible || this.m_LabelItem.Visible;
        }
        #endregion

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnDraw(e);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderToolTipPopupPanel(new ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }
    }
}
