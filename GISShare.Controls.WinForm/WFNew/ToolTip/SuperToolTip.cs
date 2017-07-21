using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace GISShare.Controls.WinForm.WFNew
{
    public class SuperToolTip : ISuperToolTip
    {
        System.Windows.Forms.Timer m_Timer;

        IToolTipPopup m_pToolTipPopup;
        Dictionary<IBaseItem2, ITipInfo> m_ToolTipInfoDictionary_BaseItem;
        //Dictionary<Forms.INCBaseItem, ITipInfo> m_ToolTipInfoDictionary_NCBaseItem;
        Dictionary<System.Windows.Forms.Control, ITipInfo> m_ToolTipInfoDictionary_Control;

        public SuperToolTip()
        {
            this.m_Timer = new System.Windows.Forms.Timer();
            this.m_Timer.Interval = 5000;
            this.m_Timer.Tick += new EventHandler(Timer_Tick);
            //
            this.m_pToolTipPopup = new ToolTipPopup();
            this.m_pToolTipPopup.PopupOpened += new EventHandler(ToolTipPopup_PopupOpened);
            this.m_pToolTipPopup.PopupClosed += new EventHandler(ToolTipPopup_PopupClosed);
            //
            this.m_ToolTipInfoDictionary_BaseItem = new Dictionary<IBaseItem2, ITipInfo>();
            //this.m_ToolTipInfoDictionary_NCBaseItem = new Dictionary<Forms.INCBaseItem, ITipInfo>();
            this.m_ToolTipInfoDictionary_Control = new Dictionary<System.Windows.Forms.Control, ITipInfo>();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            this.m_pToolTipPopup.Close();
        }
        void ToolTipPopup_PopupOpened(object sender, EventArgs e)
        {
            if(this.AutoClose) this.m_Timer.Start();
        }
        void ToolTipPopup_PopupClosed(object sender, EventArgs e)
        {
            this.m_Timer.Stop();
        }

        ~SuperToolTip()
        {
            this.ClearToolTip();
            this.m_ToolTipInfoDictionary_BaseItem = null;
            //this.m_ToolTipInfoDictionary_NCBaseItem = null;
            this.m_ToolTipInfoDictionary_Control = null;
            this.m_pToolTipPopup = null;
            this.m_Timer = null;
        }

        #region ISuperToolTip
        private bool m_AutoClose = true;
        public bool AutoClose
        {
            get { return m_AutoClose; }
            set { m_AutoClose = value; }
        }

        public int Interval
        {
            get
            {
                return this.m_Timer.Interval;
            }
            set
            {
                this.m_Timer.Interval = value;
            }
        }

        private int m_OffsetX = 0;
        public int OffsetX
        {
            get { return m_OffsetX; }
            set { m_OffsetX = value; }
        }

        private int m_OffsetY = 8;
        public int OffsetY
        {
            get { return m_OffsetY; }
            set { m_OffsetY = value; }
        }

        public bool SetToolTip(IBaseItem2 pBaseItem)
        {
            if (pBaseItem == null ||
                this.m_ToolTipInfoDictionary_BaseItem.ContainsKey(pBaseItem)) return false;
            //
            IBaseItemEvent pBaseItemEvent = pBaseItem as IBaseItemEvent;
            if (pBaseItemEvent == null) return false;
            //
            this.m_ToolTipInfoDictionary_BaseItem.Add(pBaseItem, new TipInfo(pBaseItem.Text));
            pBaseItemEvent.MouseEnter += new EventHandler(BaseItem_MouseEnter);
            pBaseItemEvent.MouseLeave += new EventHandler(BaseItem_MouseLeave);
            return true;
        }
        public bool SetToolTip(IBaseItem2 pBaseItem, ITipInfo pTipInfo)
        {
            if (pBaseItem == null || 
                pTipInfo == null ||
                this.m_ToolTipInfoDictionary_BaseItem.ContainsKey(pBaseItem)) return false;
            //
            IBaseItemEvent pBaseItemEvent = pBaseItem as IBaseItemEvent;
            if (pBaseItemEvent == null) return false;
            //
            this.m_ToolTipInfoDictionary_BaseItem.Add(pBaseItem, pTipInfo);
            pBaseItemEvent.MouseEnter += new EventHandler(BaseItem_MouseEnter);
            pBaseItemEvent.MouseLeave += new EventHandler(BaseItem_MouseLeave);
            return true;
        }
        void BaseItem_MouseEnter(object sender, EventArgs e)
        {
            IBaseItem2 pBaseItem = sender as IBaseItem2;
            if (pBaseItem != null && this.m_ToolTipInfoDictionary_BaseItem.ContainsKey(pBaseItem))
            {
                if (!this.m_pToolTipPopup.SetTipInfo(this.m_ToolTipInfoDictionary_BaseItem[pBaseItem])) return;
                //
                Point point = pBaseItem.PointToScreen(new Point(pBaseItem.DisplayRectangle.Left, pBaseItem.DisplayRectangle.Bottom));
                this.m_pToolTipPopup.Show(new Point(System.Windows.Forms.Form.MousePosition.X + this.OffsetX, point.Y + this.OffsetY));
            }
        }
        void BaseItem_MouseLeave(object sender, EventArgs e)
        {
            this.m_pToolTipPopup.Close();
        }
        public bool RemoveToolTip(IBaseItem2 pBaseItem)
        {
            if (this.m_ToolTipInfoDictionary_BaseItem.ContainsKey(pBaseItem))
            {
                this.m_ToolTipInfoDictionary_BaseItem.Remove(pBaseItem);
                //
                IBaseItemEvent pBaseItemEvent = pBaseItem as IBaseItemEvent;
                if (pBaseItemEvent != null)
                {
                    pBaseItemEvent.MouseEnter -= new EventHandler(BaseItem_MouseEnter);
                    pBaseItemEvent.MouseLeave -= new EventHandler(BaseItem_MouseLeave);
                }
                return true;
            }
            return false;
        }

        //public bool SetToolTip(Forms.INCBaseItem pNCBaseItem)
        //{
        //    if (pNCBaseItem == null ||
        //        this.m_ToolTipInfoDictionary_NCBaseItem.ContainsKey(pNCBaseItem)) return false;
        //    //
        //    this.m_ToolTipInfoDictionary_NCBaseItem.Add(pNCBaseItem, new TipInfo(pNCBaseItem.Text));
        //    pNCBaseItem.MouseEnter += new EventHandler(NCBaseItem_MouseEnter);
        //    pNCBaseItem.MouseLeave += new EventHandler(NCBaseItem_MouseLeave);
        //    return true;
        //}
        //public bool SetToolTip(Forms.INCBaseItem pNCBaseItem, ITipInfo pTipInfo)
        //{
        //    if (pNCBaseItem == null ||
        //        pTipInfo == null ||
        //        this.m_ToolTipInfoDictionary_NCBaseItem.ContainsKey(pNCBaseItem)) return false;
        //    //
        //    this.m_ToolTipInfoDictionary_NCBaseItem.Add(pNCBaseItem, pTipInfo);
        //    pNCBaseItem.MouseEnter += new EventHandler(NCBaseItem_MouseEnter);
        //    pNCBaseItem.MouseLeave += new EventHandler(NCBaseItem_MouseLeave);
        //    return true;
        //}
        //void NCBaseItem_MouseEnter(object sender, EventArgs e)
        //{
        //    Forms.INCBaseItem pNCBaseItem = sender as Forms.INCBaseItem;
        //    if (pNCBaseItem != null && this.m_ToolTipInfoDictionary_NCBaseItem.ContainsKey(pNCBaseItem))
        //    {
        //        if (!this.m_pToolTipPopup.SetTipInfo(this.m_ToolTipInfoDictionary_NCBaseItem[pNCBaseItem])) return;
        //        //
        //        Point point = pNCBaseItem.PointToScreen(new Point(pNCBaseItem.DisplayRectangle.Left, pNCBaseItem.DisplayRectangle.Bottom));
        //        this.m_pToolTipPopup.Show(new Point(System.Windows.Forms.Form.MousePosition.X + this.OffsetX, point.Y + pNCBaseItem.NCOffsetY + this.OffsetY));
        //    }
        //}
        //void NCBaseItem_MouseLeave(object sender, EventArgs e)
        //{
        //    this.m_pToolTipPopup.Close();
        //}
        //public bool RemoveToolTip(Forms.INCBaseItem pNCBaseItem)
        //{
        //    if (this.m_ToolTipInfoDictionary_NCBaseItem.ContainsKey(pNCBaseItem))
        //    {
        //        this.m_ToolTipInfoDictionary_NCBaseItem.Remove(pNCBaseItem);
        //        pNCBaseItem.MouseEnter -= new EventHandler(NCBaseItem_MouseEnter);
        //        pNCBaseItem.MouseLeave -= new EventHandler(NCBaseItem_MouseLeave);
        //        return true;
        //    }
        //    return false;
        //}

        public bool SetToolTipEx(System.Windows.Forms.Control control)
        {
            if (control == null ||
                this.m_ToolTipInfoDictionary_Control.ContainsKey(control)) return false;
            //
            this.m_ToolTipInfoDictionary_Control.Add(control, new TipInfo(control.Text));
            control.MouseEnter += new EventHandler(Control_MouseEnter);
            control.MouseLeave += new EventHandler(Control_MouseLeave);
            return true;
        }
        public bool SetToolTipEx(System.Windows.Forms.Control control, ITipInfo pTipInfo)
        {
            if (control == null ||
                pTipInfo == null ||
                this.m_ToolTipInfoDictionary_Control.ContainsKey(control)) return false;
            //
            this.m_ToolTipInfoDictionary_Control.Add(control, pTipInfo);
            control.MouseEnter += new EventHandler(Control_MouseEnter);
            control.MouseLeave += new EventHandler(Control_MouseLeave);
            return true;
        }
        void Control_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Control control = sender as System.Windows.Forms.Control;
            if (control != null && this.m_ToolTipInfoDictionary_Control.ContainsKey(control))
            {
                if (!this.m_pToolTipPopup.SetTipInfo(this.m_ToolTipInfoDictionary_Control[control])) return;
                //
                Point point = control.PointToScreen(new Point(control.DisplayRectangle.Left, control.DisplayRectangle.Bottom));
                this.m_pToolTipPopup.Show(new Point(System.Windows.Forms.Form.MousePosition.X + this.OffsetX, point.Y + this.OffsetY));
            }
        }
        void Control_MouseLeave(object sender, EventArgs e)
        {
            this.m_pToolTipPopup.Close();
        }
        public bool RemoveToolTipEx(System.Windows.Forms.Control control)
        {
            if (this.m_ToolTipInfoDictionary_Control.ContainsKey(control))
            {
                this.m_ToolTipInfoDictionary_Control.Remove(control);
                control.MouseEnter -= new EventHandler(Control_MouseEnter);
                control.MouseLeave -= new EventHandler(Control_MouseLeave);
                return true;
            }
            return false;
        }

        public void ClearToolTip()
        {
            foreach (KeyValuePair<IBaseItem2, ITipInfo> one in this.m_ToolTipInfoDictionary_BaseItem)
            {
                IBaseItemEvent pBaseItemEvent = one.Key as IBaseItemEvent;
                if (pBaseItemEvent != null)
                {
                    pBaseItemEvent.MouseEnter -= new EventHandler(BaseItem_MouseEnter);
                    pBaseItemEvent.MouseLeave -= new EventHandler(BaseItem_MouseLeave);
                }
            }
            this.m_ToolTipInfoDictionary_BaseItem.Clear();
            //
            //foreach (KeyValuePair<Forms.INCBaseItem, ITipInfo> one in this.m_ToolTipInfoDictionary_NCBaseItem)
            //{
            //    one.Key.MouseEnter -= new EventHandler(NCBaseItem_MouseEnter);
            //    one.Key.MouseLeave -= new EventHandler(NCBaseItem_MouseLeave);
            //}
            //this.m_ToolTipInfoDictionary_NCBaseItem.Clear();
            //
            foreach (KeyValuePair<System.Windows.Forms.Control, ITipInfo> one in this.m_ToolTipInfoDictionary_Control)
            {
                one.Key.MouseEnter -= new EventHandler(Control_MouseEnter);
                one.Key.MouseLeave -= new EventHandler(Control_MouseLeave);
            }
            this.m_ToolTipInfoDictionary_Control.Clear();
        }
        #endregion
    }
}
