using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    /// <summary>
    /// 根据反射机制产生插件对象并将其装入插件目录字典
    /// </summary>
    public class PluginHandle
    {
        /// <summary>
        /// 记录插件加载和反射的事件
        /// </summary>
        public event PluginReflectionEventHandler PluginReflection;

        /// <summary>
        /// 从一个或多个文件夹中读取插件
        /// </summary>
        /// <param name="strPluginFolderArry">文件夹数组</param>
        /// <param name="strPluginFolderArry">排除的文件名列表（包含后缀名）</param>
        /// <param name="bFilterFilePathArrayTypeRemove">true 过滤；false 包含</param>
        /// <param name="strFilterObjectNameArray">踢出对象名称列表</param>
        /// <param name="bFilterObjectNameArrayTypeRemove">true 过滤；false 包含</param>
        /// <returns>插件目录字典</returns>
        public PluginCategoryDictionary GetPluginsFromDLLFolders(string[] strPluginFolderArray, string[] strFilterFilePathArray, bool bFilterFilePathArrayTypeRemove, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginCategoryDictionary pluginCategoryDictionary = new PluginCategoryDictionary();
            //
            foreach (string pluginFolder in strPluginFolderArray)
            {
                //检测插件文件夹是否存在，如果不存在则新建一个避免出现异常
                if (!Directory.Exists(pluginFolder))
                {
                    Directory.CreateDirectory(pluginFolder);
                    continue;
                }
                //获得插件文件夹中每一个dll文件
                string[] _files = Directory.GetFiles(pluginFolder, "*.dll");
                foreach (string one in _files)
                {
                    if (bFilterFilePathArrayTypeRemove)
                    {
                        if (this.Contains(strFilterFilePathArray, System.IO.Path.GetFileName(one))) continue;
                        pluginCategoryDictionary.Add(this.GetPluginsFromDll(one, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove));
                    }
                    else
                    {
                        if (!this.Contains(strFilterFilePathArray, System.IO.Path.GetFileName(one))) continue;
                        pluginCategoryDictionary.Add(this.GetPluginsFromDll(one, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove));
                    }
                }
            }

            return pluginCategoryDictionary;
        }

        /// <summary>
        /// 从一个或多个DLL文件中读取插件
        /// </summary>
        /// <param name="strPluginFolderArry">DLL文件数组</param>
        /// <param name="strFilterObjectNameArray">踢出对象名称列表</param>
        /// <returns>插件目录字典</returns>
        public PluginCategoryDictionary GetPluginsFromDLLFiles(string[] strFilePathArray, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginCategoryDictionary pluginCategoryDictionary = new PluginCategoryDictionary();
            //
            foreach (string one in strFilePathArray)
            {
                pluginCategoryDictionary.Add(this.GetPluginsFromDll(one, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove));
            }
            //
            return pluginCategoryDictionary;
        }

        /// <summary>
        /// 从一个DLL文件中读取插件
        /// </summary>
        /// <param name="strPluginFolderArry">DLL文件</param>
        /// <param name="strFilterObjectNameArray">踢出对象名称列表</param>
        /// <returns>插件目录字典</returns>
        public PluginCategoryDictionary GetPluginsFromDll(string strFilePath, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginCategoryDictionary pluginCategoryDictionary = new PluginCategoryDictionary();
            //
            #region 获取插件类型
            Assembly assembly = Assembly.LoadFrom(strFilePath);
            if (assembly != null)
            {
                Type[] typeArray = null;
                try
                {
                    //获取程序集中定义的类型
                    typeArray = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("反射类型加载异常（" + ex.ToString() + "）");
                    this.OnPluginReflection(
                        new PluginReflectionEventArgs(
                            PluginReflectionStyle.eConflictPluginObject, null, "反射类型加载异常（" + ex.ToString() + "）"));
                }
                catch (Exception ex)
                {
                    //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("反射类型加载异常（" + ex.ToString() + "）");
                    this.OnPluginReflection(
                        new PluginReflectionEventArgs(
                            PluginReflectionStyle.eException, null, "反射类型加载异常（" + ex.ToString() + "）"));
                }
                finally
                {
                    foreach (Type type in typeArray)
                    {
                        if (type.IsClass && !type.IsAbstract)
                        {
                            //获得一个类型所有实现的接口
                            Type[] interfaceArray = type.GetInterfaces();
                            //遍历接口类型
                            foreach (Type one in interfaceArray)
                            {
                                //如果满足某种类型，则添加到插件集合对象中
                                switch (one.FullName)
                                {
                                    case "GISShare.Controls.Plugin.IPlugin":
                                        //MessageBox.Show(theInterface.FullName);
                                        this.GetPluginObject(pluginCategoryDictionary, type, strFilterObjectNameArray, bFilterObjectNameArrayTypeRemove);
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //
            return pluginCategoryDictionary;
        }

        /// <summary>
        /// 将反射对象放入插件目录字典
        /// </summary>
        /// <param name="pluginCategoryDictionary">插件目录字典</param>
        /// <param name="type">反射类型</param>
        /// <param name="strFilterObjectNameArray">插件目录字典</param>
        private void GetPluginObject(PluginCategoryDictionary pluginCategoryDictionary, Type type, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            IPlugin pPlugin = null;
            try
            {
                pPlugin = Activator.CreateInstance(type) as IPlugin;
            }
            catch (Exception ex)
            {
                //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("“" + type.FullName + "”反射生成对象时发生异常（" + ex.Message + "）");
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eCreateException, null, "“" + type.FullName + "”反射生成对象时发生异常（" + ex.Message + "）"));
            }
            finally
            {
                if (pPlugin != null)
                {
                    if (bFilterObjectNameArrayTypeRemove)
                    {
                        if (this.Contains(strFilterObjectNameArray, pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eExceptPluginObject, pPlugin, "排除的插件对象"));
                        }
                        else if (pluginCategoryDictionary.ContainsPlugin(pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eConflictPluginObject, pPlugin, "冲突的插件对象"));
                        }
                        else
                        {
                            pluginCategoryDictionary.AddPlugin(pPlugin);
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eEfficientlyPluginObject, pPlugin, "创建有效地插件对象"));
                        }
                    }
                    else
                    {
                        if (this.Contains(strFilterObjectNameArray, pPlugin.Name))
                        {
                            pluginCategoryDictionary.AddPlugin(pPlugin);
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eEfficientlyPluginObject, pPlugin, "创建有效地插件对象"));
                        }
                        else if (pluginCategoryDictionary.ContainsPlugin(pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eConflictPluginObject, pPlugin, "冲突的插件对象"));
                        }
                        else
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eExceptPluginObject, pPlugin, "排除的插件对象"));
                        }
                    }
                }
            }
        }
        private bool Contains(string[] strArray, string str)//踢除指定对象
        {
            if (strArray != null)
            {
                foreach (string one in strArray)
                {
                    if (one == str) return true;
                }
            }
            //
            return false;
        }

        //受保护
        protected virtual void OnPluginReflection(PluginReflectionEventArgs e)
        {
            if (this.PluginReflection != null) this.PluginReflection(this, e);
        }
    }
}
