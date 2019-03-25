using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.DockPanel
{
    class HideAreaTabButtonItem : WFNew.BaseButtonExItem //RenderableButton
    {
        #region 私有变量
        private int m_BasePanelID = 0;                                                                               //记录所对应的BasePanel的索引（便于展现隐藏面板）
        private System.Windows.Forms.TabAlignment m_TabAlignment = System.Windows.Forms.TabAlignment.Bottom; //记录隐藏区按钮的绘制状态
        #endregion

        public HideAreaTabButtonItem(int iBasePanelID, string text, Image image, TabAlignment eAlignment)
            : base()
        {
            base.Text = text;
            base.Image = image;
            this.m_BasePanelID = iBasePanelID;
            this.m_TabAlignment = eAlignment;
        }

        #region WFNew.TabButtonItem
        public override bool Checked
        {
            get
            {
                return false;
            }
            set
            {
                base.Checked = value;
            }
        }

        public override bool ShowNomalState
        {
            get
            {
                return true;
            }
            set
            {
                base.ShowNomalState = value;
            }
        }

        public override Padding Padding
        {
            get
            {
                return new Padding(2);
            }
            set
            {
                base.Padding = value;
            }
        }

        public override Orientation eOrientation
        {
            get
            {
                switch (this.TabAlignment)
                {
                    case TabAlignment.Top:
                    case TabAlignment.Bottom:
                        return Orientation.Horizontal;
                    case TabAlignment.Left:
                    case TabAlignment.Right:
                        return Orientation.Vertical;
                    default:
                        return base.eOrientation;
                }
            }
            set
            {
                base.eOrientation = value;
            }
        }

        public override GISShare.Controls.WinForm.WFNew.ImageSizeStyle eImageSizeStyle
        {
            get
            {
                return GISShare.Controls.WinForm.WFNew.ImageSizeStyle.eSystem;
            }
            set
            {
                base.eImageSizeStyle = value;
            }
        }

        public override ContentAlignment ImageAlign
        {
            get
            {
                switch (this.eOrientation)
                {
                    case Orientation.Horizontal:
                        return ContentAlignment.MiddleLeft;
                    default:
                        return ContentAlignment.TopCenter;
                }
            }
            set
            {
                base.ImageAlign = value;
            }
        }

        public override ContentAlignment TextAlign
        {
            get
            {
                return ContentAlignment.MiddleCenter;
            }
            set
            {
                base.TextAlign = value;
            }
        }
        #endregion

        [Browsable(false)]
        public int BasePanelID//记录所对应的BasePanel的索引
        {
            get { return m_BasePanelID; }
        }

        [Browsable(false)]
        public System.Windows.Forms.TabAlignment TabAlignment//记录隐藏区按钮的绘制状态
        {
            get { return m_TabAlignment; }
            set { m_TabAlignment = value; }
        }
    }
}
