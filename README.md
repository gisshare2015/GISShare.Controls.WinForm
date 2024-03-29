# GISShare.Controls.WinForm & GISShare.Controls.Plugin
**GISShare.Controls.WinForm：** 是基于.NET开发的WinForm自定义控件库，提供丰富多样的控件（如：浮动工具条、浮动面板、功能区控件、数据表、树结构数据表、列表控件、树控件、折叠面板、分割面板、自定义窗体等），有效地补充VS原生组件库的不足，也是很好地GUI学习资料。控件主要分为WFNew和WinForm两大类，其中WFNew里的控件是基于基础Control类实现（所有控件都是通过BaseItemHost来承载实现），而WinForm则是对原有VS控件的重绘。  
**GISShare.Controls.Plugin：** 是一个UI插件引擎库，设计结构模仿“ArcGIS Desktop”产品，便于大家交流应用。

**控件相关**  
GISShare.Win32.dll：依赖DLL，Windows相关API；  
GISShare.Controls.WinForm.dll：自定义控件库（引入VS工具箱即可拖拽使用）；  
GISShare.Controls.WinForm.Demo.exe：示例DEMO；  
**插件引擎相关**  
GISShare.Controls.Plugin.dll：插件接口及引擎  
GISShare.Controls.Plugin.WinForm.Demo.Hook.dll：示例DEMO：钩子  
GISShare.Controls.Plugin.WinForm.Demo.PluginDLL.dll：示例DEMO插件库  
GISShare.Controls.Plugin.WinForm.Demo.exe：示例DEMO  

**控件相关贴图**  
1.示例Demo窗口  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/05fa9d39-df39-4607-b648-f7ab8c4348f7)  
2.浮动工具条：DockPanelManager  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/00cf164b-433c-40d2-b79f-39a98047b0ac)  
3.浮动面板：DockBarManager（在WinForm目录）  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/27909308-7c4d-4fea-93dd-5a326649f55c)  
4.功能区控件：RibbonControl  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/bfd8a959-637f-427d-b657-131b2e1a651f)  
5.数据表：GridViewItemListBox  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/87f50b1e-c174-418a-a4be-c5c8325cb36e)  
6.树结构数据表：GridNodeViewItemTree  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/15f89d36-e535-4bbd-9012-32adb42be169)  
7.列表控件：ViewItemListBox  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/cc47e96d-7d5d-4b52-9cd9-484beb40d4fa)  
8.树控件：NodeViewTree  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/c12d7067-f9d2-4994-b154-e6416c92f97a)  
9.折叠面板：ExpandableCaptionPanel  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/02cb8ecd-2741-4f83-91a8-02b1a569a8ef)  
10.折叠面板容器：ExpandablePanelContainer  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/2663134a-a39e-45b8-abaf-164b673e55b9)  
11.分割面板：CollapsableSplitPanel  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/a7f199ac-63c6-40b0-b99b-1683f3894ec9)  
12.按钮类组件  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/016927cb-4e5a-4da1-bd29-4799c593f13c)  
13.标签&输入框类组件  
![QQ截图20240222165619副本](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/e7316c75-500a-4418-99a6-fb480f0e40a5)  
14.单选项&复选框&滑条&星&分割线&容器类等组件  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/97812bd6-bca5-4859-b51a-f39b9df81e15)  
15.功能区相关组件  
![QQ截图20240222170522副本](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/9eb9685a-a240-418f-93f7-ef9e31257f8e)  

**插件引擎相关贴图**  
1.示例Demo窗口  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/26a3adb7-d1a4-4d5b-8c13-86d523e80367)  
2.功能区控件+浮动面板插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/788111e7-b330-4634-b3c3-b059713697eb)  
3.浮动工具条+浮动面板插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/7bde6c14-7f7f-461a-8aea-c59d8c7e53ed)  
4.自定义窗体+浮动工具条+浮动面板插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/9f6f6f56-d868-4805-aeb9-7bd30e80af80)  

**VS应用**  
1.在工具箱右击“添加选项卡”名为“GISShare.Controls.WinForm 组件”，效果图如下：  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/c7adbdce-e16d-4478-81f3-77f6fe765315)  
2.常规继承Control的控件直接拖入即可，特别介绍一下“WFNew”命名空间下的绘制类组件（优势：共用一个句柄效率高，多控件不卡顿）的应用：直接拖入“BaseItemHost”控件，点击右上角的箭头，在弹框内点击“关系树设计器”，在弹出的“集合控制设计器”内选择对应节点右击，在弹出的右击菜单内选择对应的操作即可（如：添加子项、清除子项、刷新等，不同的节点内容不通），如下图：  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/cca4ccc5-964d-4069-b936-7088ccceabaa)  
3.“BaseItemHost”的根节点就是设计器中的“BaseItemObject”属性，如下图：  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/8e9bc491-245c-4146-8921-0245971a6a4b)

**结语**  
我是一位GIS方向的Coder，该项目是我在大学初识ArcEngine二次开发时设计研发。2015年之前一直有更新迭代，2015年以后偶有维护，想来已有十几年了。该控件的UI效果可能已经完全跟不上现在的系统了（当然你也可以定制自己的颜色表来修改它），可我觉得它的设计实现对于了解操作系统（如：API、消息机制、事件等），学习GUI知识，插件机制等还是有很大的帮助。所以，把它放在这里，与大家一起交流。
