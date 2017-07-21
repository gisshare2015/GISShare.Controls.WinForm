using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISShare.Controls.Plugin.WinForm.WFNew.Ribbon
{
    [System.Serializable]
    public class HostRibbonFrameworkSerializableObject
    {
        SubItemSerializableObject m_MenuItems = new SubItemSerializableObject();
        public SubItemSerializableObject MenuItems
        {
            get { return m_MenuItems; }
            set { m_MenuItems = value; }
        }
        SubItemSerializableObject m_OperationItems = new SubItemSerializableObject();
        public SubItemSerializableObject OperationItems
        {
            get { return m_OperationItems; }
            set { m_OperationItems = value; }
        }
        SubItemSerializableObject m_RecordItems = new SubItemSerializableObject();
        public SubItemSerializableObject RecordItems
        {
            get { return m_RecordItems; }
            set { m_RecordItems = value; }
        }
        //
        SubItemSerializableObject m_ToolbarItems = new SubItemSerializableObject();
        public SubItemSerializableObject ToolbarItems
        {
            get { return m_ToolbarItems; }
            set { m_ToolbarItems = value; }
        }
        SubItemSerializableObject m_PageContents = new SubItemSerializableObject();
        public SubItemSerializableObject PageContents
        {
            get { return m_PageContents; }
            set { m_PageContents = value; }
        }
        SubItemSerializableObject m_RibbonPages = new SubItemSerializableObject();
        public SubItemSerializableObject RibbonPages
        {
            get { return m_RibbonPages; }
            set { m_RibbonPages = value; }
        }
        //
        SubItemSerializableObject m_StatusbarItems = new SubItemSerializableObject();
        public SubItemSerializableObject StatusbarItems
        {
            get { return m_StatusbarItems; }
            set { m_StatusbarItems = value; }
        }
        //
        SubItemSerializableObject m_ContextPopups = new SubItemSerializableObject();
        public SubItemSerializableObject ContextPopups
        {
            get { return m_ContextPopups; }
            set { m_ContextPopups = value; }
        }
    }
}