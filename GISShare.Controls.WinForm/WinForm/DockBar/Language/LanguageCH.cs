using System;
using System.Collections.Generic;
using System.Text;

namespace GISShare.Controls.WinForm.DockBar
{
    public class LanguageCH : Language
    {
        public override string CustomizeFormTitle { get { return "自定义"; } }
        public override string CustomizeForm_ButtonCancelText { get { return "关  闭"; } }
        public override string CustomizeForm_TabPageBarText { get { return "工具栏"; } }
        public override string CustomizeForm_TabPageBar_LabelBarText { get { return "工具条："; } }
        public override string CustomizeForm_TabPageBar_ButtonewText { get { return "新建"; } }
        public override string CustomizeForm_TabPageBar_ButtonRenameText { get { return "重命名"; } }
        public override string CustomizeForm_TabPageBar_ButtonDeleteText { get { return "删除"; } }
        public override string CustomizeForm_TabPageBar_ButtonResetText { get { return "重置"; } }
        public override string CustomizeForm_TabPageBar_CheckBoxLargeImageText { get { return "使用大图标"; } }
        public override string CustomizeForm_TabPageBar_CheckBoxToolTipText { get { return "显示提示标签"; } }
        public override string CustomizeForm_TabPageItemText { get { return "命令"; } }
        public override string CustomizeForm_TabPageItem_LabelCategoryText { get { return "类别："; } }
        public override string CustomizeForm_TabPageItem_LabelItemText { get { return "命令："; } }
        public override string CustomizeForm_TabPageItem_LabelTipText { get { return "若要添加命令，请在对应的命令上单击鼠标右键。"; } }
        //
        public override string CreateOrModifyFormTitle_Create { get { return "新建工具条"; } }
        public override string CreateOrModifyFormTitle_Modify { get { return "重命名工具条"; } }
        public override string CreateOrModifyForm_LabelameText { get { return "工具条名称："; } }
        public override string CreateOrModifyForm_TextBoxameText { get { return "新建工具条"; } }
        public override string CreateOrModifyForm_ButtonOkText { get { return "确 定"; } }
        public override string CreateOrModifyForm_ButtonCancelText { get { return "取 消"; } }
        //
        public override string MainMenuText { get { return "主菜单"; } }
        public override string StatusBarText { get { return "状态栏"; } }
        public override string CustomizeText { get { return "自定义..."; } }
        public override string AddOrRemoveText { get { return "添加或移除按钮"; } }
        public override string ResetToolbarText { get { return "重置工具栏"; } }
        public override string StandardText { get { return "标准"; } }
        public override string ResetText { get { return "重置"; } }
        public override string AddToText { get { return "添加到"; } }
        public override string ItemText { get { return "项"; } }
        public override string InText { get { return "第 "; } }
        public override string PositionText { get { return " 位"; } }
        public override string DoubleQuotationMarks_Left { get { return "“"; } }
        public override string DoubleQuotationMarks_Right { get { return "”"; } }
        //
        public override string DefaultText { get { return "默认"; } }
    }
}
