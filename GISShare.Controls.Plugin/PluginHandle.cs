using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GISShare.Controls.Plugin
{
    /// <summary>
    /// ���ݷ�����Ʋ���������󲢽���װ����Ŀ¼�ֵ�
    /// </summary>
    public class PluginHandle
    {
        /// <summary>
        /// ��¼������غͷ�����¼�
        /// </summary>
        public event PluginReflectionEventHandler PluginReflection;

        /// <summary>
        /// ��һ�������ļ����ж�ȡ���
        /// </summary>
        /// <param name="strPluginFolderArry">�ļ�������</param>
        /// <param name="strPluginFolderArry">�ų����ļ����б�������׺����</param>
        /// <param name="bFilterFilePathArrayTypeRemove">true ���ˣ�false ����</param>
        /// <param name="strFilterObjectNameArray">�߳����������б�</param>
        /// <param name="bFilterObjectNameArrayTypeRemove">true ���ˣ�false ����</param>
        /// <returns>���Ŀ¼�ֵ�</returns>
        public PluginCategoryDictionary GetPluginsFromDLLFolders(string[] strPluginFolderArray, string[] strFilterFilePathArray, bool bFilterFilePathArrayTypeRemove, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginCategoryDictionary pluginCategoryDictionary = new PluginCategoryDictionary();
            //
            foreach (string pluginFolder in strPluginFolderArray)
            {
                //������ļ����Ƿ���ڣ�������������½�һ����������쳣
                if (!Directory.Exists(pluginFolder))
                {
                    Directory.CreateDirectory(pluginFolder);
                    continue;
                }
                //��ò���ļ�����ÿһ��dll�ļ�
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
        /// ��һ������DLL�ļ��ж�ȡ���
        /// </summary>
        /// <param name="strPluginFolderArry">DLL�ļ�����</param>
        /// <param name="strFilterObjectNameArray">�߳����������б�</param>
        /// <returns>���Ŀ¼�ֵ�</returns>
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
        /// ��һ��DLL�ļ��ж�ȡ���
        /// </summary>
        /// <param name="strPluginFolderArry">DLL�ļ�</param>
        /// <param name="strFilterObjectNameArray">�߳����������б�</param>
        /// <returns>���Ŀ¼�ֵ�</returns>
        public PluginCategoryDictionary GetPluginsFromDll(string strFilePath, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            PluginCategoryDictionary pluginCategoryDictionary = new PluginCategoryDictionary();
            //
            #region ��ȡ�������
            Assembly assembly = Assembly.LoadFrom(strFilePath);
            if (assembly != null)
            {
                Type[] typeArray = null;
                try
                {
                    //��ȡ�����ж��������
                    typeArray = assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("�������ͼ����쳣��" + ex.ToString() + "��");
                    this.OnPluginReflection(
                        new PluginReflectionEventArgs(
                            PluginReflectionStyle.eConflictPluginObject, null, "�������ͼ����쳣��" + ex.ToString() + "��"));
                }
                catch (Exception ex)
                {
                    //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("�������ͼ����쳣��" + ex.ToString() + "��");
                    this.OnPluginReflection(
                        new PluginReflectionEventArgs(
                            PluginReflectionStyle.eException, null, "�������ͼ����쳣��" + ex.ToString() + "��"));
                }
                finally
                {
                    foreach (Type type in typeArray)
                    {
                        if (type.IsClass && !type.IsAbstract)
                        {
                            //���һ����������ʵ�ֵĽӿ�
                            Type[] interfaceArray = type.GetInterfaces();
                            //�����ӿ�����
                            foreach (Type one in interfaceArray)
                            {
                                //�������ĳ�����ͣ�����ӵ�������϶�����
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
        /// ��������������Ŀ¼�ֵ�
        /// </summary>
        /// <param name="pluginCategoryDictionary">���Ŀ¼�ֵ�</param>
        /// <param name="type">��������</param>
        /// <param name="strFilterObjectNameArray">���Ŀ¼�ֵ�</param>
        private void GetPluginObject(PluginCategoryDictionary pluginCategoryDictionary, Type type, string[] strFilterObjectNameArray, bool bFilterObjectNameArrayTypeRemove)
        {
            IPlugin pPlugin = null;
            try
            {
                pPlugin = Activator.CreateInstance(type) as IPlugin;
            }
            catch (Exception ex)
            {
                //LiuZhenHong.Controls.WFNew.Forms.TBMessageBox.Show("��" + type.FullName + "���������ɶ���ʱ�����쳣��" + ex.Message + "��");
                this.OnPluginReflection(
                    new PluginReflectionEventArgs(
                        PluginReflectionStyle.eCreateException, null, "��" + type.FullName + "���������ɶ���ʱ�����쳣��" + ex.Message + "��"));
            }
            finally
            {
                if (pPlugin != null)
                {
                    if (bFilterObjectNameArrayTypeRemove)
                    {
                        if (this.Contains(strFilterObjectNameArray, pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eExceptPluginObject, pPlugin, "�ų��Ĳ������"));
                        }
                        else if (pluginCategoryDictionary.ContainsPlugin(pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eConflictPluginObject, pPlugin, "��ͻ�Ĳ������"));
                        }
                        else
                        {
                            pluginCategoryDictionary.AddPlugin(pPlugin);
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eEfficientlyPluginObject, pPlugin, "������Ч�ز������"));
                        }
                    }
                    else
                    {
                        if (this.Contains(strFilterObjectNameArray, pPlugin.Name))
                        {
                            pluginCategoryDictionary.AddPlugin(pPlugin);
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eEfficientlyPluginObject, pPlugin, "������Ч�ز������"));
                        }
                        else if (pluginCategoryDictionary.ContainsPlugin(pPlugin.Name))
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eConflictPluginObject, pPlugin, "��ͻ�Ĳ������"));
                        }
                        else
                        {
                            this.OnPluginReflection(new PluginReflectionEventArgs(PluginReflectionStyle.eExceptPluginObject, pPlugin, "�ų��Ĳ������"));
                        }
                    }
                }
            }
        }
        private bool Contains(string[] strArray, string str)//�߳�ָ������
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

        //�ܱ���
        protected virtual void OnPluginReflection(PluginReflectionEventArgs e)
        {
            if (this.PluginReflection != null) this.PluginReflection(this, e);
        }
    }
}
