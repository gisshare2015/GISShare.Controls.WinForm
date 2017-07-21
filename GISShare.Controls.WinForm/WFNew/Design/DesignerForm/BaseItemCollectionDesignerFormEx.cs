using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.WFNew.Design
{
    public class BaseItemCollectionDesignerFormEx : CollectionDesignerForm
    {
        public BaseItemCollectionDesignerFormEx(IObjectDesignHelper pObjectDesignHelper)
            : base(pObjectDesignHelper) { }

        protected override bool FiltrationSelected(object value)
        {
            if (value == null) return false;
            //
            if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return false;
            if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return false;
            if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return false;
            //
            return base.FiltrationSelected(value);
        }

        protected override string GetTypeDescription(object value)
        {
            if (value == null) return "null";
            //
            if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleLeftItem") return "�˵���";
            if (value.GetType().Name == "RibbonApplicationPopupPanelMiddleRightItem") return "��¼��";
            if (value.GetType().Name == "RibbonApplicationPopupPanelBottomItem") return "������";
            //
            return base.GetTypeDescription(value);
        }

        /// <summary>
        /// Ĭ�ϵļ����������������
        /// </summary>
        /// <returns></returns>
        protected override Type[] CreateNewItemTypes()
        {
            return BaseItemCollectionDesignerForm.GetCreateNewItemTypes();
        }

        /// <summary>
        /// ������ ��Ӧ�ļ����������������
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, Type[]> CreateNewItemTypesDictionary()
        {
            return BaseItemCollectionDesignerForm.GetCreateNewItemTypesDictionary();
        }
    }
}
