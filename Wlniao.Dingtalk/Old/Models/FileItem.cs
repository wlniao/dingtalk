using System;
namespace Wlniao.Dingtalk.Models
{
    /// <summary>
    /// 文件实体信息
    /// </summary>
    public class FileItem
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string MimeType
        {
            get
            {
                return GetMimeType(FilePath);
            }
        }
        /// <summary>
        /// 文件数据
        /// </summary>
        public byte[] Bytes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FileItem()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        public FileItem(String path, String name = "")
        {
            FilePath = path;
            FileName = string.IsNullOrEmpty(name) ? System.IO.Path.GetFileName(path) : name;
            if (!string.IsNullOrEmpty(GetMimeType(path)) && file.Exists(path))
            {
                Bytes = file.ReadByte(path);
            }
        }



        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMimeType(string name)
        {
            var ext = System.IO.Path.GetExtension(name).Trim('.');
            switch (ext)
            {
                case "txt": return "text/plain";
                case "zip": return "application/zip";
                case "pdf": return "application/pdf";
                case "doc": return "application/msword";
                case "xls": return "application/vnd.ms-excel";
                case "ppt": return "application/vnd.ms-powerpoint";
                case "wps": return "application/vnd.ms-works";
                case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "xlsx": return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case "pptx": return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case "rar": return "application/octet-stream";
                default: return "";
            }
        }
    }
}