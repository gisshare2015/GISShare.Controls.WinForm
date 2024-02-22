# GISShare.Controls.WinForm & GISShare.Controls.Plugin
GISShare.Controls.WinForm：是基于.NET开发的WinForm自定义控件库，提供丰富多样的控件（如：浮动工具条、浮动面板、功能区控件、数据表、树结构数据表、列表控件、树控件、折叠面板、分割面板、自定义窗体等），有效地补充VS原生组件库的不足，也是很好地GUI学习资料。控件主要分为WFNew和WinForm两大类，其中WFNew里的控件是基于基础Control类实现（所有控件都是通过BaseItemHost来承载实现），而WinForm则是对原有VS控件的重绘。  
GISShare.Controls.Plugin：是一个UI插件引擎库，设计结构模仿“ArcGIS Desktop”产品，便于大家学习研究。

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
示例Demo窗口  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/05fa9d39-df39-4607-b648-f7ab8c4348f7)  
浮动工具条：DockPanelManager  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/00cf164b-433c-40d2-b79f-39a98047b0ac)  
浮动面板：DockBarManager  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/27909308-7c4d-4fea-93dd-5a326649f55c)  
功能区控件：RibbonControl  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/bfd8a959-637f-427d-b657-131b2e1a651f)  
数据表：GridViewItemListBox  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/87f50b1e-c174-418a-a4be-c5c8325cb36e)  
树结构数据表：GridNodeViewItemTree  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/15f89d36-e535-4bbd-9012-32adb42be169)  
列表控件：ViewItemListBox  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/cc47e96d-7d5d-4b52-9cd9-484beb40d4fa)  
树控件：NodeViewTree  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/c12d7067-f9d2-4994-b154-e6416c92f97a)  
折叠面板：ExpandableCaptionPanel  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/02cb8ecd-2741-4f83-91a8-02b1a569a8ef)  
分割面板：CollapsableSplitPanel  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/a7f199ac-63c6-40b0-b99b-1683f3894ec9)  

**插件引擎相关贴图**  
示例Demo窗口  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/26a3adb7-d1a4-4d5b-8c13-86d523e80367)  
功能区控件+浮动面板 插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/788111e7-b330-4634-b3c3-b059713697eb)  
浮动工具条+浮动面板 插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/7bde6c14-7f7f-461a-8aea-c59d8c7e53ed)  
自定义窗体+浮动工具条+浮动面板 插件展示  
![image](https://github.com/gisshare2015/GISShare.Controls.WinForm/assets/20768620/9f6f6f56-d868-4805-aeb9-7bd30e80af80)  
