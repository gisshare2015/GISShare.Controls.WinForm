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
    class DoubleInputRegion : InputRegion, IDoubleInputRegion
    {
        private const int CONST_FLOATLENGTH = 100;

        public DoubleInputRegion(IInputObject pInputObject)
            : base(pInputObject)
        {
            this.AutoGetFocus = true;
            this.AutoSelectAll = false;
            ////
            //string[] strList = double.MaxValue.ToString().Split('.');
            //if (strList.Length >= 2) READONLY_FLOATLENGTH = strList[1].Length;
        }

        #region IDoubleInputRegion
        [Browsable(true), Description("获取当前值"), Category("属性")]
        public double Value
        {
            get
            {
                double dValue;
                if (double.TryParse(this.Text, out dValue))
                {
                    return dValue;
                }
                else
                {
                    return this.Minimum > 0 ? this.Minimum : 0;
                }
            }
            set
            {
                string strValue = value.ToString();
                if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strValue, this.Minimum.ToString()) >= 0 &&
                    GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strValue, this.Maximum.ToString()) <= 0)
                {
                    this.Text = strValue;
                }
            }
        }

        double m_Minimum = double.MinValue;
        [Browsable(true), Description("最小值"), Category("属性")]
        public double Minimum
        {
            get { return m_Minimum; }
            set
            {
                if (value > Maximum) value = Maximum - 1;
                m_Minimum = value;
            }
        }

        double m_Maximum = double.MaxValue;
        [Browsable(true), Description("最大值"), Category("属性")]
        public double Maximum
        {
            get { return m_Maximum; }
            set
            {
                if (value < Minimum) value = Minimum + 1;
                m_Maximum = value;
            }
        }

        int m_FloatLength = 6;
        [Browsable(true), DefaultValue(6), Description("浮点长度（最大值100）"), Category("属性")]
        public int FloatLength
        {
            get { return m_FloatLength; }
            set
            {
                m_FloatLength = value > CONST_FLOATLENGTH ? CONST_FLOATLENGTH : value;
            }
        }
        #endregion

        protected override bool FilterKeyChar(char cKeyChar)
        {
            if (this.FloatLength <= 0) return this.FilterKeyChar_Integer(cKeyChar);
            else return this.FilterKeyChar_Double(cKeyChar);
        }
        private bool FilterKeyChar_Double(char cKeyChar)
        {
            int iTextLength = this.Text.Length;
            if (iTextLength <= 0)
            {
                //if (cKeyChar > '0' &&
                //    cKeyChar <= '9' &&
                //    GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(cKeyChar.ToString(), this.Minimum.ToString()) >= 0 &&
                //    GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(cKeyChar.ToString(), this.Maximum.ToString()) <= 0)
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
                bOk = !this.Text.Contains(".") || this.SelectedText.Contains(".");
            }
            else if (cKeyChar == (char)0x2E)//Delete键
            {
                bOk = iSelectionStart >= 0 && iSelectionStart <= iTextLength;
            }
            else if (cKeyChar == (char)0x8)//退格键
            {
                bOk = true; //bOk = this.Text[0] == '-' ? iSelectionStart >= 2 : iSelectionStart >= 1;
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
                bOk = this.Text[0] == '-' ? iSelectionStart >= 0 : iSelectionStart >= 1;
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
                if (cKeyChar == '.')
                {
                    strNewValue = this.Text.Remove(iSelectionStart, this.SelectionLength);
                    strNewValue = strNewValue.Insert(iSelectionStart, cKeyChar.ToString());
                }
                else if (cKeyChar == (char)0x2E && this.SelectionLength <= 0)
                {
                    strNewValue = this.Text.Remove(iSelectionStart, 1);
                }
                else if (cKeyChar == (char)0x8 && this.SelectionLength <= 0 && iSelectionStart > 0)
                {
                    strNewValue = this.Text.Remove(iSelectionStart - 1, 1);
                }
                else
                {
                    strNewValue = this.Text.Remove(iSelectionStart, this.SelectionLength);
                    strNewValue = strNewValue.Insert(iSelectionStart, cKeyChar.ToString());
                }
                //
                if (strNewValue.Length <= 0)
                {
                    bOk = true;
                }
                else
                {
                    if (strNewValue.Contains("."))
                    {
                        string[] strList = strNewValue.Split('.');
                        if (strList.Length >= 2 && strList[1].Length > this.FloatLength) return false;
                    }
                    //bOk = GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strNewValue, this.Minimum.ToString(".")) >= 0 && GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strNewValue, this.Maximum.ToString(".")) <= 0;
                }
            }
            //
            return bOk;
        }
        private bool FilterKeyChar_Integer(char cKeyChar)
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
                string strMinimum = this.Minimum.ToString("###");
                string strMaximum = this.Maximum.ToString("###");
                if (strText.Length <= 0)
                {
                    if (this.Minimum > 0) { strText = strMinimum; }
                    else { strText = "0"; }
                    this.SelectionStart = strText.Length;
                }
                else if (strText.Length == 1 && strText[0] == '-')
                {
                    if (this.Minimum > 0) { strText = "-" + strMinimum; }
                    else { strText = "-0"; }
                    this.SelectionStart = strText.Length;
                }
                else if (strText.Contains("."))
                {
                    //int iLastIndex = strText.Length - 1;
                    int iPointIndex = strText.IndexOf(".");
                    if (strText[strText.Length - 1] == '.')
                    {
                        strText += "0";
                        this.SelectionStart = strText.Length - 1;
                    }
                    //
                    if (strText[0] == '-')
                    {
                        if (strText[strText.Length - 1] == '0' && iPointIndex >= 1 && iPointIndex < strText.Length - 1 - 1)
                        {
                            strText = strText.Remove(strText.Length - 1);
                            this.SelectionStart = strText.Length;
                        }
                        //
                        if (iPointIndex == 1)
                        {
                            strText = strText.Insert(1, "0");
                            this.SelectionStart = 2;
                        }
                        else if (iPointIndex < 0 || iPointIndex >= 3)
                        {
                            if (strText.Length > 3 && strText[0] == '-' && strText[1] == '0')
                            {
                                strText = strText.Remove(1, 1);
                                this.SelectionStart = strText.Length;
                            }
                        }
                    }
                    else
                    {
                        if (strText[strText.Length - 1] == '0' && iPointIndex >= 0 && iPointIndex < strText.Length - 1 - 1)
                        {
                            strText = strText.Remove(strText.Length - 1);
                            this.SelectionStart = strText.Length;
                        }
                        if (iPointIndex == 0)
                        {
                            strText = strText.Insert(0, "0");
                            this.SelectionStart = 2;
                        }
                        //
                        iPointIndex = strText.IndexOf(".");
                        if (strText.Length >= 2 && strText[1] == '0' && iPointIndex >= 2)
                        {
                            strText = strText.Remove(0, 1);
                            this.SelectionStart = strText.IndexOf(".");
                        }
                    }
                }
                else
                {
                    if (strText.Length > 1 && strText[0] == '0')
                    {
                        strText = strText.Remove(0, 1);
                        this.SelectionStart = strText.Length;
                    }
                    else if (strText.Length > 2 && strText[0] == '-' && strText[1] == '0')
                    {
                        strText = strText.Remove(1, 1);
                        this.SelectionStart = strText.Length;
                    }
                }
                //
                if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strText, strMinimum) < 0)
                {
                    strText = strMinimum;
                }
                else if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(strText, strMaximum) > 0)
                {
                    strText = strMaximum;
                }
                ////
                //if (this.FloatLength >= 0)
                //{
                //    int iLen = strText.Length - 1 - strText.IndexOf('.');
                //    if (strText.Length == iLen)
                //    {
                //        strText += ".";
                //        for (int i = 0; i < iLen; i++)
                //        {
                //            strText += "0";
                //        }
                //    }
                //    else if (strText.Length > iLen)
                //    {
                //        int iNum = iLen - this.FloatLength;
                //        if (iNum > 0)
                //        {
                //            strText = strText.Remove(strText.Length - 1 - iNum, iNum);
                //        }
                //        else
                //        {
                //            for (int i = 0; i < iNum; i++) 
                //            {
                //                strText += "0";
                //            }
                //        }
                //    }
                //}
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
        //        else if (this.Text.Contains("."))
        //        {
        //            //int iLastIndex = this.Text.Length - 1;
        //            int iPointIndex = this.Text.IndexOf(".");
        //            if (this.Text[this.Text.Length - 1] == '.')
        //            {
        //                this.Text += "0";
        //                this.SelectionStart = this.Text.Length - 1;
        //            }
        //            //
        //            if (this.Text[0] == '-')
        //            {
        //                if (this.Text[this.Text.Length - 1] == '0' && iPointIndex >= 1 && iPointIndex < this.Text.Length - 1 - 1)
        //                {
        //                    this.Text = this.Text.Remove(this.Text.Length - 1);
        //                    this.SelectionStart = this.Text.Length;
        //                }
        //                //
        //                if (iPointIndex == 1)
        //                {
        //                    this.Text = this.Text.Insert(1, "0");
        //                    this.SelectionStart = 2;
        //                }
        //                else if (iPointIndex < 0 || iPointIndex >= 3)
        //                {
        //                    if (this.Text.Length > 3 && this.Text[0] == '-' && this.Text[1] == '0')
        //                    {
        //                        this.Text = this.Text.Remove(1, 1);
        //                        this.SelectionStart = this.Text.Length;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (this.Text[this.Text.Length - 1] == '0' && iPointIndex >= 0 && iPointIndex < this.Text.Length - 1 - 1)
        //                {
        //                    this.Text = this.Text.Remove(this.Text.Length - 1);
        //                    this.SelectionStart = this.Text.Length;
        //                }
        //                if (iPointIndex == 0)
        //                {
        //                    this.Text = this.Text.Insert(0, "0");
        //                    this.SelectionStart = 2;
        //                }
        //                //
        //                iPointIndex = this.Text.IndexOf(".");
        //                if (this.Text.Length >= 2 && this.Text[1] == '0' && iPointIndex >= 2)
        //                {
        //                    this.Text = this.Text.Remove(0, 1);
        //                    this.SelectionStart = this.Text.IndexOf(".");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (this.Text.Length > 1 && this.Text[0] == '0')
        //            {
        //                this.Text = this.Text.Remove(0, 1);
        //                this.SelectionStart = this.Text.Length;
        //            }
        //            else if (this.Text.Length > 2 && this.Text[0] == '-' && this.Text[1] == '0')
        //            {
        //                this.Text = this.Text.Remove(1, 1);
        //                this.SelectionStart = this.Text.Length;
        //            }
        //        }
        //        //
        //        if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(this.Text, this.Minimum.ToString()) < 0)
        //        {
        //            this.Text = this.Minimum.ToString();
        //        }
        //        else if (GISShare.Controls.WinForm.Util.UtilCompare.CompareNumEx(this.Text, this.Maximum.ToString()) > 0)
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
