using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    class BaseItemDBCollectionDesignerForm : WFNew.Design.CollectionDesignerForm
    {
        public BaseItemDBCollectionDesignerForm(WFNew.IObjectDesignHelper pObjectDesignHelper)
            : base(pObjectDesignHelper) { }

        /// <summary>
        /// 默认的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected override Type[] CreateNewItemTypes()
        {
            return new Type[] { typeof(MenuItem) };
        }

        /// <summary>
        /// 各类型 对应的加载子项的类型数组
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        {
            Dictionary<string, Type[]> typeCreateNewItemTypesDictionary = new Dictionary<string, Type[]>();
            //
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.MenuBar",
                new Type[] { 
                        typeof(MenuItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem), 
                        typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.ToolBar",
                new Type[] { 
                        typeof(ButtonItem), 
                        typeof(DropDownButtonItem),
                        typeof(SplitButtonItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem),
                        typeof(LabelItem), 
                        typeof(CheckBoxItem),
                        typeof(RadioButtonItem),
                        typeof(ProgressBarItem),
                        typeof(NumericUpDownItem),
                        typeof(MaskedTextBoxItem),
                        typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.StatusBar",
                new Type[] { 
                        typeof(LabelItem), 
                        typeof(ButtonItem),
                        typeof(DropDownButtonItem),
                        typeof(SplitButtonItem), 
                        typeof(ProgressBarItem), 
                        typeof(SeparatorItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.ContextMenu",
                new Type[] { 
                        typeof(MenuItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem), 
                        typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.MenuItem",
                new Type[] { 
                        typeof(MenuItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem), 
                        typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.DropDownButtonItem",
                new Type[] { 
                        typeof(MenuItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem), 
                        typeof(SeparatorItem) }
                );
            typeCreateNewItemTypesDictionary.Add
                (
                "GISShare.Controls.WinForm.DockBar.SplitButtonItem",
                new Type[] { 
                        typeof(MenuItem), 
                        typeof(ComboBoxItem), 
                        typeof(TextBoxItem), 
                        typeof(SeparatorItem) }
                );
            //
            return typeCreateNewItemTypesDictionary;
        }

        /// <summary>
        /// 创建节点类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override GISShare.Controls.WinForm.WFNew.View.NodeViewItem CreateNode(object value)
        {
            if (value is WFNew.ICollectionObjectDesignHelper)
            {
                GISShare.Controls.WinForm.WFNew.View.NodeViewItem node = new GISShare.Controls.WinForm.WFNew.View.NodeViewItem();
                node.ShowNomalState = true;
                return node;
            }
            //
            return base.CreateNode(value);
        }
    }
}
