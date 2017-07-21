using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class InputRegion : BasePopup, IInputRegion, IInputRegionEvent
    {
        private System.Windows.Forms.TextBox m_TextBox = null;
        private IInputObject m_pInputObject = null;
        private ToolStripControlHost m_ToolStripControlHost = null;

        public InputRegion(IInputObject pInputObject)
        {
            this.m_pInputObject = pInputObject;
            //
            this.m_TextBox = new System.Windows.Forms.TextBox();
            this.m_TextBox.ReadOnly = !this.m_pInputObject.CanEdit;
            this.m_TextBox.Font = this.m_pInputObject.InputFont;
            this.m_TextBox.ForeColor = this.m_pInputObject.InputForeColor;
            this.m_TextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.m_TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_TextBox.KeyDown += new KeyEventHandler(TextBox_KeyDown);
            this.m_TextBox.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            this.m_TextBox.KeyUp += new KeyEventHandler(TextBox_KeyUp);
            this.m_TextBox.TextChanged += new EventHandler(TextBox_TextChanged);
            //
            Control textBoxContainer = new Control();
            textBoxContainer.Dock = DockStyle.Fill;
            textBoxContainer.Controls.Add(this.m_TextBox);
            //
            this.m_ToolStripControlHost = new ToolStripControlHost(textBoxContainer);
            this.m_ToolStripControlHost.Dock = DockStyle.Fill;
            this.m_ToolStripControlHost.Margin = new Padding(0);
            this.m_ToolStripControlHost.Padding = new Padding(0);
            //
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.DropShadowEnabled = false;
            this.Items.Add(this.m_ToolStripControlHost);
            //
            ((ISetOwnerHelper)this).SetOwner(this.m_pInputObject as IBaseItemOwner);
        }
        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.Handled = !this.FilterKeyChar(e.KeyChar);
            }
            catch { }
            finally
            {
                if (!e.Handled)
                {
                    IEventHelper pEventHelper = this.m_pInputObject as IEventHelper;
                    if (pEventHelper != null)
                    {
                        pEventHelper.RelationEvent("KeyPress", e);
                    }
                }
            }
        }
        void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            IEventHelper pEventHelper = this.m_pInputObject as IEventHelper;
            if (pEventHelper != null)
            {
                pEventHelper.RelationEvent("KeyDown", e);
            }
        }
        void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DismissPopup();
            }
            //
            IEventHelper pEventHelper = this.m_pInputObject as IEventHelper;
            if (pEventHelper != null)
            {
                pEventHelper.RelationEvent("KeyUp", e);
            }
        }
        void TextBox_TextChanged(object sender, EventArgs e)
        {
            //this.OnTextChanged(e);
            //this.m_pInputObject.InputText = this.m_TextBox.Text;
            if (this.InputingFilterText)
            {
                this.m_pInputObject.InputText = this.FilterText(this.m_TextBox.Text);
            }
        }

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            if (!this.InputingFilterText)
            {
                CancelEventArgs cancelEventArgs = new CancelEventArgs();
                this.OnInputEnd(cancelEventArgs);
                if (!cancelEventArgs.Cancel)
                {
                    this.m_pInputObject.InputText = this.FilterText(this.m_TextBox.Text);
                }
            }
            //
            base.OnClosed(e);
        }

        bool m_AutoGetFocus = true;
        public new bool AutoGetFocus
        {
            get { return m_AutoGetFocus; }
            set { m_AutoGetFocus = value; }
        }

        bool m_AutoSelectAll = true;
        public bool AutoSelectAll
        {
            get { return m_AutoSelectAll; }
            set { m_AutoSelectAll = value; }
        }

        public int SelectionStart
        {
            get { return this.m_TextBox.SelectionStart; }
            set { this.m_TextBox.SelectionStart = value; }
        }

        public int SelectionLength
        {
            get { return this.m_TextBox.SelectionLength; }
        }

        public string SelectedText
        {
            get { return this.m_TextBox.SelectedText; }
        }

        public override string Text
        {
            get
            {
                return this.m_TextBox.Text;
            }
            set
            {
                this.m_TextBox.Text = value;
            }
        }

        /// <summary>
        /// 返回一个值，该值指示输入的字符是否成功过滤
        /// </summary>
        /// <param name="cKeyChar"></param>
        /// <returns></returns>
        protected virtual bool FilterKeyChar(char cKeyChar)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        protected virtual string FilterText(string strText) 
        {
            return strText;
        }
        
        #region Clone
        public override object Clone()
        {
            return null;
        }
        #endregion

        #region Radius
        public override bool UseRadius
        {
            get
            {
                return false;
            }
        }

        public override int LeftTopRadius { get { return 0; } }

        public override int RightTopRadius { get { return 0; } }

        public override int LeftBottomRadius { get { return 0; } }

        public override int RightBottomRadius { get { return 0; } }
        #endregion

        #region IInputRegion
        bool m_InputingFilterText = false;
        public bool InputingFilterText
        {
            get { return m_InputingFilterText; }
            set { m_InputingFilterText = value; }
        }

        public char PasswordChar
        {
            get
            {
                return this.m_TextBox.PasswordChar;
            }
            set
            {
                this.m_TextBox.PasswordChar = value;
            }
        }

        public new void Show()
        {
            this.m_TextBox.ReadOnly = !this.m_pInputObject.CanEdit;
            this.m_TextBox.Text = this.m_pInputObject.InputText;
            this.m_TextBox.Font = this.m_pInputObject.InputFont;
            this.m_TextBox.ForeColor = this.m_pInputObject.InputForeColor;
            //
            Rectangle rectangle = this.m_pInputObject.InputRegionRectangle;
            this.m_ToolStripControlHost.Size = rectangle.Size;
            base.Show(rectangle.Location);
            //
            if (this.AutoSelectAll) this.m_TextBox.SelectAll();
            if (this.AutoGetFocus)
            {
                this.m_TextBox.Focus(); 
                this.m_TextBox.SelectionStart = this.Text.Length;
            }
        }

        public string TryGetInputingText()
        {
            return this.FilterText(this.m_TextBox.Text); 
        }

        public void ShowInputRegion()
        {
            this.Show();
        }

        public Size GetInputRegionSize()
        {
            //this.m_TextBox.Text = this.m_pInputObject.InputText;
            this.m_TextBox.Font = this.m_pInputObject.InputFont;
            this.m_TextBox.ForeColor = this.m_pInputObject.InputForeColor;
            return this.m_TextBox.Size;
        }
        #endregion

        #region IInputRegionEvent
        [Browsable(true), Description("编辑结束后触发（InputingFilterText == false 时有效）"), Category("属性已更改")]
        public event CancelEventHandler InputEnd;
        #endregion

        //
        protected virtual void OnInputEnd(CancelEventArgs e)
        {
            if (this.InputEnd != null) { this.InputEnd(this, e); }
        }

    }
}
