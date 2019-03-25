using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    public class DescriptionMenuPopupPanelItem : BaseItemStackExItem, IDescriptionPopupPanelItem
    {
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

        public override System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                return System.Windows.Forms.Orientation.Vertical;
            }
            set
            {
                base.eOrientation = System.Windows.Forms.Orientation.Vertical;
            }
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true);
            this.Relayout(e.Graphics, LayoutStyle.eLayoutAuto, false);
            //
            GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderDescriptionMenuPopupPanel(
                new GISShare.Controls.WinForm.ObjectRenderEventArgs(e.Graphics, this, this.DisplayRectangle));
        }

    }
}
