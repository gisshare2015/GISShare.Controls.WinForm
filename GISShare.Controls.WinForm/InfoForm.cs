using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm
{
    public sealed class InfoForm : GISShare.Controls.WinForm.WFNew.Forms.TBForm//Form
    {
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ������������ɵĴ���

        /// <summary>
        /// �����֧������ķ��� - ��Ҫ
        /// ʹ�ô���༭���޸Ĵ˷��������ݡ�
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoForm));
            this.SuspendLayout();
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(544, 270);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InfoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "����";
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.InfoForm_MouseClick);
            this.ResumeLayout(false);

        }

        #endregion

        //
        //
        //
        //
        //

        public InfoForm()
        {
            InitializeComponent();
        }

        protected override void OnDraw(PaintEventArgs e)
        {
            //base.OnDraw(e);
            Graphics gGraphics = e.Graphics;
            Brush bBrush = new SolidBrush(Color.Black);
            //
            string strInfo = "GISShare.Controls  �Ƕ�VS�Դ��ؼ��Ĳ������չ��Ϊ������\n\r" +
                             "�򵥵�����Ӧ�ó��򿪷��ṩ������\n\r";
            Font fFont = new Font("����", 13);
            Point pPoint = new Point(10, 10);
            gGraphics.DrawString(strInfo, fFont, bBrush, pPoint.X, pPoint.Y);
            //
            strInfo = "��Ȩ���У���ӭʹ�ã�\n\r" + "http://www.gisshare.com/";
            fFont = new Font("����", 10);
            pPoint = new Point(10, 80);
            gGraphics.DrawString(strInfo, fFont, bBrush, pPoint.X, pPoint.Y);
            ////
            //strInfo = "";
            //fFont = new Font("����", 8);
            //pPoint = new Point(243, 245);
            //gGraphics.DrawString(strInfo, fFont, bBrush, pPoint.X, pPoint.Y);
        }

        private void InfoForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X > 10 && e.X < 172 && e.Y > 97 && e.Y < 107)
            {
                System.Diagnostics.Process.Start("http://www.gisshare.com/");
            }
            //else if (e.X > 246 && e.X < 517 && e.Y > 259 && e.Y < 265)
            //{
            //    System.Diagnostics.Process.Start("http://user.qzone.qq.com/441856317/infocenter");
            //}
        }
    }
}