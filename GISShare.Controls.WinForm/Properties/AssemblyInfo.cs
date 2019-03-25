using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过下列属性集
// 控制。更改这些属性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("GISShare.Controls 拓展VS控件集合")]
[assembly: AssemblyDescription("GISShare.Controls 拓展VS控件集合")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("GISShare（即时分享 http://www.gisshare.com ）")]
[assembly: AssemblyProduct("GISShare.Controls 拓展VS控件集合")]
[assembly: AssemblyCopyright("版权所有 (C) GISShare 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 属性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("0368c2d4-582c-46e1-916d-eb75b2af2893")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      内部版本号
//      修订号
//
// 可以指定所有这些值，也可以使用“修订号”和“内部版本号”的默认值，
// 方法是按如下所示使用“*”:
[assembly: AssemblyVersion("3.0.0.0")]
[assembly: AssemblyFileVersion("3.0.0.0")]

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//A.B.C.D：版本号
//A：大规模的结构修改，以及新增一个控件模块或功能模块。
//B：新增一个控件系列。
//C：新增零散的控件或组件成员，用于丰富当前的控件。
//D：小范围的改动，如：修改异常、函数维护与更新、新增接口或辅助类等等。
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//
//2.0.0.1 修改DockPanelManager里的图片释放异常；
//2.0.0.2 修改DockBarManager里的浮动窗体关闭按钮重绘；
//2.0.0.3 公开键盘和鼠标钩子；
//
//2.0.1.3 添加IDoubleInputBoxItem、IDoubleInputRegion、DoubleInputBox、DoubleInputBoxItem，并修改集合设计器；//
//2.0.1.4 树折叠异常（死循环）；
//2.0.1.5 修改钩子嵌入模式；
//2.0.1.6 修改DescriptionMenuPopup的GetIdealSize()函数；
//2.0.1.7 修改ContextPopup、DescriptionMenuPopup、RibbonApplicationPopup的TrySetPopupPanelSize(Size size)函数；
//2.0.1.8 修改布局函数[Size Relayout(Graphics g, LayoutStyle eLayoutStyle, bool bSetSize)]，并在布局前先进行布局规划[this.Relayout(e.Graphics, LayoutStyle.eLayoutPlan, true)]；
//2.0.1.9 修改ITabButtonItem接口。1.添加属性[bool HaveTabButtonContainer { get; }]；2.修改RibbonPage和RibbonPageItem的Visible及VisibleEx属性；3.RibbonPage的VisibleEx在设计模式下可以编辑；
//
//2.0.2.9 添加ISuperToolTip、ITipInfo、IToolTipPopup、IToolTipPopupPanelItem、SuperToolTip、TipInfo、ToolTipPopup、ToolTipPopupPanel实现提示标签（添加颜色表和渲染方式）；//
//2.0.2.10 给DockPanel添加提示标签，新增接口IGetPartItemHelper用于获取组件以外的零件用于TabButtonContainer和TabButtonContainerItem；
//
//2.0.3.10 添加ILabelExItem、LabelExItem、LabelNEx实现多行标签控件，并修改集合设计器；//
//2.0.3.11 修改设计器（BaseItemCollectionDesignerForm、BaseItemCollectionDesignerFormEx）；
//
//2.0.4.11 添加IComboDateItem、ComboDateItem、ComboDate实现日期控件，并修改集合设计器；//
//
//2.0.5.11 添加IToolBarItem、ToolBarItem、ToolBarN工具条控件（添加颜色表和渲染方式）；//
//2.0.5.12 修改部分控件的构造函数使之便于布局（RibbonPageItem、RibbonPage、GroupButtonItem、GroupButton）；//
//2.0.5.13 修改布局类小问题（减号改成加号）；//
//
//2.0.6.13 添加DocumentArea、DocumentAreaDesigner控件，并修改DockPanelManager的DocumentDockArea属性改为DocumentArea；//
//2.0.6.14 修改ComboBoxItem、ComboBoxN控件，将属性ViewItems改为Items；//
//2.0.6.15 修改RibbonControl、RibbonControlEx控件中的RibbonPageContentContainerItem使之自动约束子项（IsRestrictItems = true）；//
//2.0.6.16 修改IInputRegion、TextBoxItem、TextBoxN控件，添加PasswordChar属性，新增接口ITextBoxItem2；//
//2.0.6.17 修改文本输入项（Item）的初始化时的宽度；//
//2.0.6.18 修改ComboBoxItem、ComboBoxN、ComboTreeItem、ComboTree的Text属性；//
//2.0.6.19 修改ToolTipPopupPanel底部间距；//
//2.0.6.20 添加ViewItemEx类，并修改对应的设计器；//
//2.0.6.21 修改BasePanel文本改变刷新DockPanel；//
//
//2.0.7.21 添加一个AE的InfoForm窗体；//
//2.0.7.22 修改API参数GetWindowText的string lpString 改为 System.Text.StringBuilder lpString；//
//2.0.7.23 修改HandleViewForm中的树控件；//
//2.0.7.24 修改NCQuickAccessToolbarItem；//
//2.0.7.25 修改HandleViewForm句柄读取；//
//
//2.0.8.25 添加一个IStarItem、StarItem、Star星型控件（添加颜色表和渲染方式）；//
//2.0.8.26 给IToolBarItem添加ToolBarStyle eToolBarStyle属性，修改DropDownButtonItem的eArrowDock属性【if (this.IsBaseBarItem) return ArrowDock.eRight; 改为 if (this.m_eArrowDock != ArrowDock.eNone && this.IsBaseBarItem) return ArrowDock.eRight;】；//
//2.0.8.27 修改部分属性；//
//
//2.0.9.27 添加一个Plugin的InfoForm窗体；//
//2.0.9.28 修改ISuperToolTip、SuperToolTip；//
//2.0.9.29 修改接口名称IDescriptionButton -> IDescriptionButtonItem；//
//
//
//
//2.1.0.0 添加插件引擎；//
//
//
//2.2.0.0 修改消息传递方式以适应新的View机制；
//
//
//
//2.3.0.0 较大规模的修改View（如：GridViewItemListBox、GridViewItemListBoxItem等等）；//
//2.3.1.0 解决64位机器长整型的问题；为DockPanelManager添加DockPanelFloatForms_Read属性和DockBarManager添加DockBarFloatForms_Read属性；//
//2.3.2.0 修改NodeViewTree的名称为NodeViewItemTree，修改NodeViewTreeItem的名称和NodeViewItemTreeItem，修改NodeViewItemTree和NodeViewItemTreeItem的CanEdit属性；
//2.3.3.0 添加控件GridNodeViewItemTree和GridNodeViewItemTreeItem，新增相关项INodeCellViewItem、IRowNodeCellViewItem、NodeCellViewItem、RowNodeCellViewItem，以及相关渲染；
//
//
//
//3.0.0.0 删除WFNew中的控件实体，采用Item显示；//

