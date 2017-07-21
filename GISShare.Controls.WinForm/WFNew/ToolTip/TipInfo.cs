using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public class TipInfo : ITipInfo
    {
        public TipInfo(string strTipInfoText)
            : this(null, null, strTipInfoText) { }

        public TipInfo(string strTitleText, string strTipInfoText)
            : this(null, strTipInfoText, strTipInfoText) { }

        public TipInfo(Image imgTitleImage, string strTitleText, string strTipInfoText)
        {
            this.m_TitleText = strTitleText;
            this.m_TitleImage = imgTitleImage;
            this.m_TipInfoText = strTipInfoText;
        }

        private Image m_TitleImage = null;
        public Image TitleImage
        {
            get { return m_TitleImage; }
        }

        private string m_TitleText = null;
        public string TitleText
        {
            get { return m_TitleText; }
        }

        private string m_TipInfoText;
        public string TipInfoText
        {
            get { return m_TipInfoText; }
        }
    }
}
