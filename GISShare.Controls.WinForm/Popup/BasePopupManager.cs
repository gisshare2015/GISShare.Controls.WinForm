using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.Popup
{
    class BasePopupManager : IDisposable
    {
        public static BasePopupManager PopupManager = new BasePopupManager();

        //
        //
        //

        private List<BasePopup> BasePopupCollection;
        private GISShare.Win32.GlobalMouseHook m_GlobalMouseHook;

        public BasePopupManager()
        {
            this.BasePopupCollection = new List<BasePopup>();
            //
            this.m_GlobalMouseHook = new GISShare.Win32.GlobalMouseHook();
            this.m_GlobalMouseHook.MouseDown += new System.Windows.Forms.MouseEventHandler(GlobalMouseHook_MouseDown);
        }
        void GlobalMouseHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) return;
            //
            for (int i = this.BasePopupCollection.Count - 1; i >= 0; i--)
            {
                if (this.BasePopupCollection[i].CustomFiltration)
                {
                    if (this.BasePopupCollection[i].Filtration(e))
                    {
                        Dismiss(i + 1, DismissReason.eItemClicked);
                        return;
                    }
                }
                else
                {
                    if (this.BasePopupCollection[i].BoundsContainsXY(e.X, e.Y))
                    {
                        Dismiss(i + 1, DismissReason.eItemClicked);
                        return;
                    }
                }
            }
            //
            Dismiss(DismissReason.eAppClicked);
        }

        ~BasePopupManager() { this.Dispose(); }

        public int Count
        {
            get { return this.BasePopupCollection.Count; }
        }

        public BasePopup LastBasePopup
        {
            get
            {
                if (BasePopupCollection.Count > 0)
                {
                    return BasePopupCollection[BasePopupCollection.Count - 1];
                }
                //
                return null;
            }
        }

        public void Register(BasePopup p)
        {
            if (!BasePopupCollection.Contains(p))
            {
                BasePopupCollection.Add(p);
                //
                if (this.Count == 1) this.m_GlobalMouseHook.CreatHook();
            }
        }

        public void Unregister(BasePopup p)
        {
            if (BasePopupCollection.Contains(p))
            {
                BasePopupCollection.Remove(p);
                //
                if (this.Count <= 0) this.m_GlobalMouseHook.DestroyHook();
            }
        }

        public void DismissChildren(BasePopup parent, DismissReason eDismissReason)
        {
            if (parent == null) return;
            //
            int index = BasePopupCollection.IndexOf(parent);
            //
            if (index >= 0)
            {
                Dismiss(index + 1, eDismissReason);
            }
        }

        public void Dismiss(DismissReason eDismissReason)
        {
            Dismiss(0, eDismissReason);
        }

        public void Dismiss(BasePopup startPopup, DismissReason eDismissReason)
        {
            if (startPopup == null) return;
            //
            int index = BasePopupCollection.IndexOf(startPopup);
            //
            if (index >= 0)
            {
                Dismiss(index, eDismissReason);
            }
        }

        private void Dismiss(int startPopupIndex, DismissReason eDismissReason)
        {
            for (int i = BasePopupCollection.Count - 1; i >= startPopupIndex; i--)
            {
                BasePopupCollection[i].Close(eDismissReason);
            }
        }

        #region IDisposable
        public void Dispose()
        {
            this.m_GlobalMouseHook.Dispose();
        }
        #endregion
    }

    public enum DismissReason
    {
        eItemClicked,
        eAppClicked,
        eNewBasePopup,
        eAppFocusChanged,
        eEscapePressed,
        eCustomize
    }
}
