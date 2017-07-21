using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.Plugin
{
    public class InsertSubItem : IPlugin, IPluginInfo, ISubItem, IInsertSubItem
    {
        protected string _Name = "GISShare.Controls.Plugin.InsertSubItem";
        //
        protected int _ItemCount = 0;
        //
        protected string _DependItem = "";
        protected int _InsertIndex = 0;
        protected int _InsertCategoryIndex = -1;
        protected int _LoadingIndex = 0;

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
                return (int)CategoryIndex_0_Style.eInsertSubItem;
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

        #region IInsertSubItem
        /// <summary>
        /// 依附项的键值
        /// </summary>
        public string DependItem
        {
            get { return _DependItem; }
            set { _DependItem = value; }
        }

        /// <summary>
        /// 插入索引
        /// </summary>
        public int InsertIndex
        {
            get { return _InsertIndex; }
        }

        /// <summary>
        /// 插入对象的目录索引
        /// </summary>
        public int InsertCategoryIndex
        {
            get { return _InsertCategoryIndex; }
        }

        /// <summary>
        /// 加载索引（用于控制排序）
        /// </summary>
        public int LoadingIndex
        {
            get { return _LoadingIndex; }
        }
        #endregion
    }
}
