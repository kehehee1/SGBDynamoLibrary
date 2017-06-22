﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SGBDynamoLibrary
{
    public class FileOperator
    {
        public static List<string> FileToList(string str_path)
        {
            StreamReader str_reader = new StreamReader(str_path);
            string str_line = str_reader.ReadLine();
            List<string> str_list = new List<string>();
            while (str_line != null)
            {
                str_list.Add(str_line);
                str_line = str_reader.ReadLine();
            }
            return str_list;
        }

        public static void WriteListToFile(List<string> str_list, string str_path)
        {
            StreamWriter str_writer = new StreamWriter(str_path);
            for (int i = 0; i < str_list.Count; ++i)
            {
                str_writer.WriteLine(str_list[i]);
            }
            str_writer.Close();
        }

        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="sPath">    年月  例  2011-04</param>
        /// <param name="sFileName">月日  例  04-22</param>
        /// <param name="content">时间+  内容</param>
        /// <returns></returns>
        public static bool WriteLog(string sPath, string sFileName, string content)
        {
            try
            {


                StreamWriter sr;
                if (!Directory.Exists(sPath))
                {
                    Directory.CreateDirectory(sPath);
                }
                string v_filename = sPath + "\\" + sFileName;


                if (!File.Exists(v_filename)) //如果文件存在,则创建File.AppendText对象
                {
                    sr = File.CreateText(v_filename);
                    sr.Close();
                }
                using (FileStream fs = new FileStream(v_filename, System.IO.FileMode.Append, System.IO.FileAccess.Write, System.IO.FileShare.Write))
                {
                    using (sr = new StreamWriter(fs))
                    {

                        sr.WriteLine(DateTime.Now.ToString("hh:mm:ss") + "     " + content);
                        sr.Close();
                    }
                    fs.Close();
                }
                return true;

            }
            catch { return false; }
        }


        /// <summary>
        /// 读取文本文件内容,每行存入arrayList 并返回arrayList对象
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns>arrayList</returns>
        public static ArrayList ReadFileRow(string sFileName)
        {
            string sLine = "";
            ArrayList alTxt = null;
            try
            {
                using (StreamReader sr = new StreamReader(sFileName))
                {
                    alTxt = new ArrayList();

                    while (!sr.EndOfStream)
                    {
                        sLine = sr.ReadLine();
                        if (sLine != "")
                        {
                            alTxt.Add(sLine.Trim());
                        }

                    }
                    sr.Close();
                }
            }
            catch
            {

            }
            return alTxt;
        }


        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!System.IO.File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName + "文件不存在！");

            if (!overwrite && System.IO.File.Exists(destFileName))
                return false;

            try
            {
                System.IO.File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!System.IO.File.Exists(backupFileName))
                    throw new FileNotFoundException(backupFileName + "文件不存在！");

                if (backupTargetFileName != null)
                {
                    if (!System.IO.File.Exists(targetFileName))
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    else
                        System.IO.File.Copy(targetFileName, backupTargetFileName, true);
                }
                System.IO.File.Delete(targetFileName);
                System.IO.File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName)
        {
            return RestoreFile(backupFileName, targetFileName, null);
        }
    }
}
