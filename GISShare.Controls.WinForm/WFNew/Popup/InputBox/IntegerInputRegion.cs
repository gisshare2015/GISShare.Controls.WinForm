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
    class IntegerInputRegion : InputRegion, IIntegerInputRegion
    {
        public IntegerInputRegion(IInputObject pInputObject)
            : base(pInputObject)
        {
            this.AutoGetFocus = true;
            this.AutoSelectAll = false;
        }

        #region IIntegerInputRegion
        [Browsable(true), Description("获取当前值"), Category("属性")]
        public int Value
        {
            get
            {
                int iValue;
                if (int.TryParse(this.Text, out iValue))
                {
                    return iValue;
                }
                else
                {
                    return this.Minimum > 0 ? this.Minimum : 0;
                }
            }
            set
            {
                string strValue = value.ToString();
                if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strValue, this.Minimum.ToString()) >= 0 &&
                    GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strValue, this.Maximum.ToString()) <= 0)
                {
                    this.Text = strValue;
                }
            }
        }

        int m_Minimum = int.MinValue;
        [Browsable(true), Description("最小值"), Category("属性")]
        public int Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (value > Maximum) value = Maximum - 1;
                m_Minimum = value;
            }
        }

        int m_Maximum = int.MaxValue;
        [Browsable(true), Description("最大值"), Category("属性")]
        public int Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (value < Minimum) value = Minimum + 1;
                m_Maximum = value;
            }
        }
        #endregion

        protected override bool FilterKeyChar(char cKeyChar)
        {
            int iTextLength = this.Text.Length;
            if (iTextLength <= 0)
            {
                //if (cKeyChar > '0' &&
                //    cKeyChar <= '9' &&
                //    GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(cKeyChar.ToString(), this.Minimum.ToString()) >= 0 &&
                //    GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(cKeyChar.ToString(), this.Maximum.ToString()) <= 0)
                if (cKeyChar > '0' && cKeyChar <= '9')
                {
                    this.Text = cKeyChar.ToString();
                }
                else
                {
                    if (this.Minimum > 0) { this.Text = this.Minimum.ToString(); }
                    else { this.Text = "0"; }
                }
                return false;
            }
            //
            bool bOk = false;
            int iSelectionStart = this.SelectionStart;
            if (cKeyChar == '.') 
            {
                bOk = false;
            }
            else if (cKeyChar == (char)0x2E)//Delete键
            {
                bOk = iSelectionStart >= 0 && iSelectionStart <= iTextLength;
            }
            else if (cKeyChar == (char)0x8)//退格键
            {
                bOk = true;// this.Text[0] == '-' ? iSelectionStart >= 2 : iSelectionStart >= 1;
            }
            else if (cKeyChar == '-')//负号“-”
            {
                //bOk = iSelectionStart == 0 && this.Text[0] != '-';
                if (this.Text[0] != '-')
                {
                    bOk = iSelectionStart == 0;
                }
                else 
                {
                    bOk = iSelectionStart == 0 && this.SelectionLength > 0;
                }
            }
            else if (cKeyChar == '0')//数字“0”
            {
                if (iTextLength == this.SelectionLength)
                {
                    bOk = true;
                }
                else if (this.Text[0] == '-')
                {
                    if (iSelectionStart == 1 && iTextLength - 1 == this.SelectionLength) bOk = true;
                    else if (iSelectionStart >= 1 && iTextLength == 1) bOk = true;
                    else if (iSelectionStart >= 2 && iTextLength >= 2) bOk = true;
                    else bOk = false;                    
                }
                else
                {
                    if (iTextLength == 0 && iSelectionStart >= 0) bOk = true;
                    else if (iTextLength >= 1 && iSelectionStart >= 1) bOk = true;
                    else bOk = false;
                }
            }
            else if (cKeyChar > '0' && cKeyChar <= '9')//数字“1 - 9”
            {
                bOk = this.Text[0] == '-' ? iSelectionStart > 0 : iSelectionStart >= 0;
            }
            else if (((int)cKeyChar >= 0x9 && (int)cKeyChar <= 0x10) || ((int)cKeyChar >= 0x25 && (int)cKeyChar <= 0x28))
            {
                bOk = true;
            }
            //
            if (bOk)
            {
                string strNewValue = "";
                if (cKeyChar == (char)0x2E && this.SelectionLength <= 0)//Delete键
                {
                    strNewValue = this.Text.Remove(iSelectionStart, 1);
                }
                else if (cKeyChar == (char)0x8 && this.SelectionLength <= 0 && iSelectionStart > 0)//退格键
                {
                    strNewValue = this.Text.Remove(iSelectionStart - 1, 1);
                }
                else
                {
                    strNewValue = this.Text.Remove(iSelectionStart, this.SelectionLength);
                    strNewValue = strNewValue.Insert(iSelectionStart, cKeyChar.ToString());
                }
                if (strNewValue.Length <= 0)
                {
                    bOk = true;
                }
                //else
                //{
                //    bOk = GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strNewValue, this.Minimum.ToString()) >= 0 && GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strNewValue, this.Maximum.ToString()) <= 0;
                //}
            }
            //
            return bOk;
        }

        protected override string FilterText(string strText)
        {
            try
            {
                if (strText.Length <= 0)
                {
                    if (this.Minimum > 0) { strText = this.Minimum.ToString(); }
                    else { strText = "0"; }
                    this.SelectionStart = strText.Length;
                }
                else if (strText.Length == 1 && strText[0] == '-')
                {
                    if (this.Minimum > 0) { strText = "-" + this.Minimum.ToString(); }
                    else { strText = "-0"; }
                    this.SelectionStart = strText.Length;
                }
                else if (strText.Length > 1 && strText[0] == '0')
                {
                    strText = strText.Remove(0, 1);
                    this.SelectionStart = strText.Length;
                }
                else if (strText.Length > 2 && strText[0] == '-' && strText[1] == '0')
                {
                    strText = strText.Remove(1, 1);
                    this.SelectionStart = strText.Length;
                }
                //
                if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strText, this.Minimum.ToString()) < 0)
                {
                    strText = this.Minimum.ToString();
                }
                else if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(strText, this.Maximum.ToString()) > 0)
                {
                    strText = this.Maximum.ToString();
                }
            }
            catch { }
            //
            return strText;
        }

        //protected override void OnTextChanged(EventArgs e)
        //{
        //    try
        //    {
        //        if (this.Text.Length <= 0)
        //        {
        //            if (this.Minimum > 0) { this.Text = this.Minimum.ToString(); }
        //            else { this.Text = "0"; }
        //            this.SelectionStart = this.Text.Length;
        //        }
        //        else if (this.Text.Length == 1 && this.Text[0] == '-')
        //        {
        //            if (this.Minimum > 0) { this.Text = "-" + this.Minimum.ToString(); }
        //            else { this.Text = "-0"; }
        //            this.SelectionStart = this.Text.Length;
        //        }
        //        else if (this.Text.Length > 1 && this.Text[0] == '0')
        //        {
        //            this.Text = this.Text.Remove(0, 1);
        //            this.SelectionStart = this.Text.Length;
        //        }
        //        else if (this.Text.Length > 2 && this.Text[0] == '-' && this.Text[1] == '0')
        //        {
        //            this.Text = this.Text.Remove(1, 1);
        //            this.SelectionStart = this.Text.Length;
        //        }
        //        else if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(this.Text, this.Minimum.ToString()) < 0)
        //        {
        //            this.Text = this.Minimum.ToString();
        //        }
        //        else if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNum(this.Text, this.Maximum.ToString()) > 0)
        //        {
        //            this.Text = this.Maximum.ToString();
        //        }
        //    }
        //    catch { }
        //    finally
        //    {
        //        base.OnTextChanged(e);
        //    }
        //}
    }
}
