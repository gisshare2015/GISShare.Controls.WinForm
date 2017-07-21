using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    [Designer(typeof(GISShare.Controls.WinForm.WFNew.Design.ExpandableNodePanelDesigner)), ToolboxItem(false)]
    public class ExpandableNodePanel : ExpandableCaptionPanel
    {
        public ExpandableNodePanel()
        {
            base.Dock = DockStyle.None;
            base.eExpandButtonStyle = ExpandButtonStyle.eTopToBottom;
        }

        public override bool ShowCaption
        {
            get { return true; }
            set { base.ShowCaption = value; }
        }

        public override ExpandButtonStyle eExpandButtonStyle
        {
            get { return ExpandButtonStyle.eTopToBottom; }
            set { base.eExpandButtonStyle = value; }
        }

        public override TabAlignment eCaptionAlignment
        {
            get { return TabAlignment.Top; }
            set { base.eCaptionAlignment = value; }
        }

    }
}
