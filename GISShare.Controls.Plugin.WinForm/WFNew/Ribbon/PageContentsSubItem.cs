using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.Ribbon
{
    public class PageContentsSubItem : IPlugin, IPluginInfo, ISubItem
    {
        protected string _Name = "GISShare.Controls.Plugin.WinForm.WFNew.Ribbon.PageContentsSubItem";
        protected int _ItemCount = 0;

        #region IPlugin
        /// <summary>
        /// 唯一编码
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
        }

        /// <summary>
        /// 接口所在的分类（用于标识自身反射对象的分类）
        /// </summary>
        public int CategoryIndex
        {
            get
            {
                return (int)CategoryIndex_3_Style.eRibbonControlEx_PageContents;
            }
        }
        #endregion

        #region IPluginInfo
        public virtual string GetDescribe()
        {
            return this.Name;
        }
        #endregion

        #region ISubItem
        /// <summary>
        /// 携带的 Item 数量
        /// </summary>
        public int ItemCount
        {
            get
            {
                return _ItemCount;
            }
        }

        /// <summary>
        /// 访问快捷菜单中每个Item的方法
        /// </summary>
        /// <param name="iIndex"></param>
        /// <param name="pItemDef"></param>
        public virtual void GetItemInfo(int iIndex, IItemDef pItemDef) { }
        #endregion
    }
}
