using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew.Forms
{
    public partial class TBMessageBoxForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm
    {
        //const int CONST_MINWIDTH = 160;
        //const int CONST_MINHEIGHT = 160;
        const int CONST_IMAGESIZE = 32;
        const int CONST_IMAGELEFTSPACE = 12;
        const int CONST_IMAGETOTEXTSPACE = 12;
        const int CONST_TEXTTOPTSPACE = 12;
        const int CONST_TEXTBOTTOMSPACE = 12;
        const int CONST_TEXTLEFTSPACE = 12;
        const int CONST_TEXTRIGHTSPACE = 12;
        const int CONST_BOTTOMHEIGHT = 52;
        const int CONST_BOTTONWIDTH = 90;
        const int CONST_BOTTONHEIGTH = 26;
        const int CONST_BOTTONSPACE = 20;
        //
        readonly int CONST_MAXTEXTHEIGHT;
        readonly int CONST_MAXTEXTWIDTH;

        string m_strTextInfo = "";
        Image m_Image = null;
        MessageBoxIcon m_eMessageBoxIcon = MessageBoxIcon.None;
        MessageBoxButtons m_eMessageBoxButtons = MessageBoxButtons.OK;
        MessageBoxDefaultButton m_eMessageBoxDefaultButton = MessageBoxDefaultButton.Button1;

        public TBMessageBoxForm(string text)
            : this(text, "", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
        { }

        public TBMessageBoxForm(string text, string caption)
            : this(text, caption, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
        { }

        public TBMessageBoxForm(string text, string caption, MessageBoxButtons buttons)
            : this(text, caption, buttons, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
        { }

        public TBMessageBoxForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            : this(text, caption, buttons, icon, MessageBoxDefaultButton.Button1)
        { }

        public TBMessageBoxForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            #region 获取基础参数
            this.m_strTextInfo = text;
            this.Text = caption;
            this.m_eMessageBoxButtons = buttons;
            this.m_eMessageBoxIcon = icon;
            this.m_eMessageBoxDefaultButton = defaultButton;
            #endregion
            //
            #region 设置图片
            switch (this.m_eMessageBoxIcon)
            {
                case MessageBoxIcon.Asterisk:
                //case MessageBoxIcon.Information:
                    this.m_Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Forms.MessageBox.Image.Asterisk_Information.png"));
                    break;
                case MessageBoxIcon.Error:
                //case MessageBoxIcon.Hand:
                //case MessageBoxIcon.Stop:
                    this.m_Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Forms.MessageBox.Image.Error_Hand_Stop.png"));
                    break;
                case MessageBoxIcon.Exclamation:
                    //case MessageBoxIcon.Warning:
                    this.m_Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Forms.MessageBox.Image.Exclamation_Warning.png"));
                    break;
                case MessageBoxIcon.Question:
                    this.m_Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Forms.MessageBox.Image.Question.png"));
                    break;
                case MessageBoxIcon.None:
                default:
                    this.m_Image = new Bitmap(this.GetType().Assembly.GetManifestResourceStream("GISShare.Controls.WinForm.WFNew.Forms.MessageBox.Image.Asterisk_Information.png"));
                    break;
            }
            #endregion
            //
            #region 设置只读参数
            CONST_MAXTEXTHEIGHT = System.Windows.Forms.SystemInformation.WorkingArea.Height - CONST_BOTTOMHEIGHT - CONST_TEXTTOPTSPACE - CONST_TEXTBOTTOMSPACE;
            if (this.ShowImage)
            {
                CONST_MAXTEXTWIDTH = System.Windows.Forms.SystemInformation.WorkingArea.Width - CONST_IMAGELEFTSPACE - CONST_IMAGESIZE - CONST_IMAGETOTEXTSPACE - CONST_TEXTRIGHTSPACE;
            }
            else
            {
                CONST_MAXTEXTWIDTH = System.Windows.Forms.SystemInformation.WorkingArea.Width - CONST_IMAGELEFTSPACE - CONST_TEXTRIGHTSPACE;
            }
            #endregion
            //
            InitializeComponent();
            //
            #region 设置基础尺寸
            switch (this.m_eMessageBoxButtons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                case MessageBoxButtons.YesNoCancel:
                    this.Width = 350;
                    this.Height = 160;
                    break;
                case MessageBoxButtons.OKCancel:
                case MessageBoxButtons.RetryCancel:
                case MessageBoxButtons.YesNo:
                    this.Width = 260;
                    this.Height = 160;
                    break;
                case MessageBoxButtons.OK:
                default:
                    this.Width = 160;
                    this.Height = 160;
                    break;
            }
            #endregion
            //
            this.SetAndGetFormInfo();
            //
            this.LayoutButton();
        }
        Rectangle m_DrawTextRectangle;
        private void SetAndGetFormInfo()
        {
            this.m_DrawTextRectangle = this.TextRectangle;
            //
            if (this.ShowTextInfo)
            {
                try
                {
                    Graphics graphics = Graphics.FromHwnd(this.Handle);
                    //
                    string[] strList = this.m_strTextInfo.Split('\n');
                    int iRowHeigth = (int)graphics.MeasureString("A", this.Font).Height + 1;
                    List<int> widthList = new List<int>();
                    foreach (string one in strList)
                    {
                        if (one == null || one.Length <= 0) widthList.Add(0);
                        else widthList.Add((int)graphics.MeasureString(one, this.Font).Width + 1);
                    }
                    int iTextWidth = 0;
                    int iRowCount = 0;
                    foreach (int one in widthList)
                    {
                        if (one > CONST_MAXTEXTWIDTH)
                        {
                            iRowCount += one / CONST_MAXTEXTWIDTH;
                            if (one % CONST_MAXTEXTWIDTH > 0) iRowCount += 1;
                            //
                            iTextWidth = CONST_MAXTEXTWIDTH;
                        }
                        else
                        {
                            iRowCount += 1;
                            //
                            if (iTextWidth < one) iTextWidth = one;
                        }
                    }
                    //
                    int iTextHeight = iRowCount * iRowHeigth;
                    Rectangle textRectangle = this.m_DrawTextRectangle;
                    this.m_DrawTextRectangle = new Rectangle
                        (
                        textRectangle.Left,
                        iTextHeight > textRectangle.Height ? textRectangle.Top : (textRectangle.Top + textRectangle.Bottom - iTextHeight) / 2,
                        iTextWidth,
                        iTextHeight > CONST_MAXTEXTHEIGHT ? CONST_MAXTEXTHEIGHT : iTextHeight
                        );
                    int iW = this.m_DrawTextRectangle.Width - textRectangle.Width;
                    int iH = this.m_DrawTextRectangle.Height - textRectangle.Height;
                    if (iW > 0) this.Width += iW;
                    if (iH > 0) this.Height += iH;
                    //
                    graphics.Dispose();
                }
                catch { }
            }
        }
        private void LayoutButton()
        {
            #region 设置按钮
            this.baseItemHost1.Size = new Size(CONST_BOTTONWIDTH, CONST_BOTTONHEIGTH);
            this.baseItemHost2.Size = new Size(CONST_BOTTONWIDTH, CONST_BOTTONHEIGTH);
            this.baseItemHost3.Size = new Size(CONST_BOTTONWIDTH, CONST_BOTTONHEIGTH);
            //
            Rectangle rectangle = this.BottomRectangle;
            int iY = (rectangle.Top + rectangle.Bottom - CONST_BOTTONHEIGTH) / 2 + 1;
            switch (this.m_eMessageBoxButtons)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - 3 * CONST_BOTTONWIDTH - 2 * CONST_BOTTONSPACE) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "Abort";
                    this.baseItemHost1.BaseItemObject.Text = "中   止";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.Location = new Point(this.baseItemHost1.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost2.BaseItemObject.Name = "Retry";
                    this.baseItemHost2.BaseItemObject.Text = "重   试";
                    this.baseItemHost2.Visible = true;
                    this.baseItemHost3.Location = new Point(this.baseItemHost2.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost3.BaseItemObject.Name = "Ignore";
                    this.baseItemHost3.BaseItemObject.Text = "忽   略";
                    this.baseItemHost3.Visible = true;
                    break;
                case MessageBoxButtons.OK:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - CONST_BOTTONWIDTH) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "OK";
                    this.baseItemHost1.BaseItemObject.Text = "确   定";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.BaseItemObject.Name = "Cancel";
                    this.baseItemHost2.BaseItemObject.Text = "取   消";
                    this.baseItemHost2.Visible = false;
                    this.baseItemHost3.BaseItemObject.Name = "None";
                    this.baseItemHost3.BaseItemObject.Text = "None";
                    this.baseItemHost3.Visible = false;
                    break;
                case MessageBoxButtons.OKCancel:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - 2 * CONST_BOTTONWIDTH - CONST_BOTTONSPACE) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "OK";
                    this.baseItemHost1.BaseItemObject.Text = "确   定";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.Location = new Point(this.baseItemHost1.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost2.BaseItemObject.Name = "Cancel";
                    this.baseItemHost2.BaseItemObject.Text = "取   消";
                    this.baseItemHost2.Visible = true;
                    this.baseItemHost3.BaseItemObject.Name = "None";
                    this.baseItemHost3.BaseItemObject.Text = "None";
                    this.baseItemHost3.Visible = false;
                    break;
                case MessageBoxButtons.RetryCancel:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - 2 * CONST_BOTTONWIDTH - CONST_BOTTONSPACE) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "Retry";
                    this.baseItemHost1.BaseItemObject.Text = "重   试";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.Location = new Point(this.baseItemHost1.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost2.BaseItemObject.Name = "Cancel";
                    this.baseItemHost2.BaseItemObject.Text = "取   消";
                    this.baseItemHost2.Visible = true;
                    this.baseItemHost3.BaseItemObject.Name = "None";
                    this.baseItemHost3.BaseItemObject.Text = "None";
                    this.baseItemHost3.Visible = false;
                    break;
                case MessageBoxButtons.YesNo:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - 2 * CONST_BOTTONWIDTH - CONST_BOTTONSPACE) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "Yes";
                    this.baseItemHost1.BaseItemObject.Text = "是";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.Location = new Point(this.baseItemHost1.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost2.BaseItemObject.Name = "No";
                    this.baseItemHost2.BaseItemObject.Text = "否";
                    this.baseItemHost2.Visible = true;
                    this.baseItemHost3.BaseItemObject.Name = "Cancel";
                    this.baseItemHost3.BaseItemObject.Text = "取   消";
                    this.baseItemHost3.Visible = false;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - 3 * CONST_BOTTONWIDTH - 2 * CONST_BOTTONSPACE) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "Yes";
                    this.baseItemHost1.BaseItemObject.Text = "是";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.Location = new Point(this.baseItemHost1.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost2.BaseItemObject.Name = "No";
                    this.baseItemHost2.BaseItemObject.Text = "否";
                    this.baseItemHost2.Visible = true;
                    this.baseItemHost3.Location = new Point(this.baseItemHost2.Location.X + CONST_BOTTONWIDTH + CONST_BOTTONSPACE, iY);
                    this.baseItemHost3.BaseItemObject.Name = "Cancel";
                    this.baseItemHost3.BaseItemObject.Text = "取   消";
                    this.baseItemHost3.Visible = true;
                    break;
                default:
                    this.baseItemHost1.Location = new Point((rectangle.Left + rectangle.Right - CONST_BOTTONWIDTH) / 2, iY);
                    this.baseItemHost1.BaseItemObject.Name = "OK";
                    this.baseItemHost1.BaseItemObject.Text = "确   定";
                    this.baseItemHost1.Visible = true;
                    this.baseItemHost2.BaseItemObject.Name = "Cancel";
                    this.baseItemHost2.BaseItemObject.Text = "取   消";
                    this.baseItemHost2.Visible = false;
                    this.baseItemHost3.BaseItemObject.Name = "None";
                    this.baseItemHost3.BaseItemObject.Text = "None";
                    this.baseItemHost3.Visible = false;
                    break;
            }
            //
            this.baseItemHost1.BaseItemObject.MouseClick += new MouseEventHandler(Button_MouseClick);
            this.baseItemHost2.BaseItemObject.MouseClick += new MouseEventHandler(Button_MouseClick);
            this.baseItemHost3.BaseItemObject.MouseClick += new MouseEventHandler(Button_MouseClick);
            #endregion
        }
        void Button_MouseClick(object sender, MouseEventArgs e)
        {
            WFNew.IBaseItem ribbonBaseButton = sender as WFNew.IBaseItem;
            if (ribbonBaseButton == null || !ribbonBaseButton.Visible) return;
            //
            switch (ribbonBaseButton.Name)
            {
                case "Abort":
                    this.DialogResult = DialogResult.Abort;
                    break;
                case "Cancel":
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case "Ignore":
                    this.DialogResult = DialogResult.Ignore;
                    break;
                case "No":
                    this.DialogResult = DialogResult.No;
                    break;
                case "None":
                    this.DialogResult = DialogResult.None;
                    break;
                case "OK":
                    this.DialogResult = DialogResult.OK;
                    break;
                case "Retry":
                    this.DialogResult = DialogResult.Retry;
                    break;
                case "Yes":
                    this.DialogResult = DialogResult.Yes;
                    break;
                default:
                    this.DialogResult = DialogResult.None;
                    break;
            }
        }

        public bool ShowTextInfo
        {
            get 
            {
                return this.m_strTextInfo != null && this.m_strTextInfo.Length > 0;
            }
        }

        public bool ShowImage
        {
            get
            {
                return this.m_Image != null && this.m_eMessageBoxIcon != MessageBoxIcon.None; 
            }
        }

        public Rectangle TopRectangle
        {
            get 
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom - CONST_BOTTOMHEIGHT);
            }
        }

        public Rectangle BottomRectangle
        {
            get
            {
                Rectangle rectangle = this.DisplayRectangle;
                return Rectangle.FromLTRB(rectangle.Left, rectangle.Bottom - CONST_BOTTOMHEIGHT, rectangle.Right, rectangle.Bottom);
            }
        }

        public Rectangle ImageRectangle
        {
            get 
            {
                Rectangle rectangle = this.TopRectangle;
                return new Rectangle
                    (
                    rectangle.Left + CONST_IMAGELEFTSPACE,
                    (rectangle.Top + rectangle.Bottom - CONST_IMAGESIZE) / 2, 
                    CONST_IMAGESIZE, 
                    CONST_IMAGESIZE
                    );
            }
        }

        public Rectangle TextRectangle
        {
            get
            {
                Rectangle rectangle = this.TopRectangle;
                if (this.ShowImage)
                {
                    return Rectangle.FromLTRB
                        (
                        rectangle.Left + CONST_IMAGELEFTSPACE + CONST_IMAGESIZE + CONST_IMAGETOTEXTSPACE,
                        rectangle.Top + CONST_TEXTTOPTSPACE,
                        rectangle.Right - CONST_TEXTRIGHTSPACE,
                        rectangle.Bottom - CONST_TEXTBOTTOMSPACE
                        );
                }
                else
                {
                    return Rectangle.FromLTRB
                        (
                        rectangle.Left + CONST_TEXTLEFTSPACE,
                        rectangle.Top + CONST_TEXTTOPTSPACE,
                        rectangle.Right - CONST_TEXTRIGHTSPACE,
                        rectangle.Bottom - CONST_TEXTBOTTOMSPACE
                        );
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //
            if (this.ShowTextInfo)
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonText(
                    new TextRenderEventArgs(e.Graphics, this, this.Enabled, this.m_strTextInfo, this.ForeColor, this.Font, this.m_DrawTextRectangle));
            }
            //
            if (this.ShowImage) 
            {
                GISShare.Controls.WinForm.WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderRibbonImage(
                    new ImageRenderEventArgs(e.Graphics, this, this.Enabled, this.m_Image, this.ImageRectangle));
            }
        }
    }
}