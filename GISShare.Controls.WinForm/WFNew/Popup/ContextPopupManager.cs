using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace GISShare.Controls.WinForm.WFNew
{
    public class ContextPopupManager : Component, IEnumerable, ICollectionItem2, ICollectionItem3
    {
        List<BasePopup> m_BasePopupList = null;

        public ContextPopupManager()
        {
            this.m_BasePopupList = new List<BasePopup>();
        }

        public int Count
        {
            get { return this.m_BasePopupList.Count; }
        }

        public BasePopup this[int iIndex]
        {
            get { return this.m_BasePopupList[iIndex]; }
        }

        public BasePopup this[string strName]
        {
            get
            {
                foreach (BasePopup one in this.m_BasePopupList)
                {
                    if (one.Name == strName) return one;
                }
                return null;
            }
        }

        public void Add(BasePopup basePopup)
        {
            this.m_BasePopupList.Add(basePopup);
        }

        public void Remove(BasePopup basePopup)
        {
            this.m_BasePopupList.Remove(basePopup);
        }

        public void Clear()
        { 
            this.m_BasePopupList.Clear();
        }

        #region ICollectionItem2
        IBaseItem ICollectionItem2.GetBaseItem(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.m_BasePopupList)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem2 pCollectionItem2 = one as ICollectionItem2;
                    if (pCollectionItem2 != null)
                    {
                        pBaseItem = pCollectionItem2.GetBaseItem(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        #region ICollectionItem3
        public IBaseItem GetBaseItem2(string strName)
        {
            IBaseItem pBaseItem = null;
            foreach (IBaseItem one in this.m_BasePopupList)
            {
                if (one.Name == strName) pBaseItem = one;
                else
                {
                    ICollectionItem3 pCollectionItem3 = one as ICollectionItem3;
                    if (pCollectionItem3 != null)
                    {
                        pBaseItem = pCollectionItem3.GetBaseItem2(strName);
                    }
                }
                //
                if (pBaseItem != null) break;
            }
            //
            return pBaseItem;
        }
        #endregion

        public IEnumerator GetEnumerator()
        {
            return this.m_BasePopupList.GetEnumerator();
        }
    }
}
