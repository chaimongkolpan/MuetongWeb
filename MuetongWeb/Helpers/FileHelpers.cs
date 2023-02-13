using Microsoft.AspNetCore.Mvc.Infrastructure;
using MuetongWeb.Constants;
using System;
namespace MuetongWeb.Helpers
{
	public static class FileHelpers
	{
		public static string GetExtention(string filename)
		{
			if (string.IsNullOrWhiteSpace(filename))
				return string.Empty;
			var arr = filename.Split('.');
			var len = arr.Length;
			if (len > 0)
				return arr[len - 1];
			return string.Empty;
		}
		public static string GetFilename(string fullpath)
		{
			if (string.IsNullOrWhiteSpace(fullpath))
				return string.Empty;
			var arr = fullpath.Split('\\');
			var len = arr.Length;
			if (len > 0)
				return arr[len - 1];
			return string.Empty;
        }
        public static string GetType(string ext)
        {
            if (FileConstants.ImageExt.Contains(ext))
                return "image";
            else if (ext == "pdf")
                return "pdf";
            else if (ext == "doc" || ext == "xls" || ext == "ppt")
                return "office";
            else if (ext == "txt")
                return "text";
            else if (ext == "htm" || ext == "html")
                return "html";
            else if (ext == "docx" || ext == "xlsx" || ext == "pptx")
                return "image";
            else
                return "image";
        }
        public static string GetUrlTag(long id, string ext, string path)
		{
			if (FileConstants.ImageExt.Contains(ext))
				return "./File/" + id.ToString() + "/" + GetFilename(path);
				//return string.Format("<a href=\"./file/{0}/{1}\" target=\"_blank\"><img src=\"./file/{0}/{1}\" style=\"width: 50%;\"/></a>", id, GetFilename(path));
			else if (ext == "pdf")
                return "./File/" + id.ToString() + "/" + GetFilename(path);
            else if(ext == "doc" || ext == "xls" || ext == "ppt")
                return "./File/" + id.ToString() + "/" + GetFilename(path);
            else if (ext == "txt")
                return "./File/" + id.ToString() + "/" + GetFilename(path);
            else if (ext == "htm" || ext == "html")
                return "./File/" + id.ToString() + "/" + GetFilename(path);
            else
                return string.Format("\" style=\"display:none;\"><a style=\"font-size:30px;\" href=\"./File/{0}/{1}\" target=\"_blank\">ดูไฟล์</a><img style=\"display:none;", id, GetFilename(path));
            //return string.Format("<a href=\"./File/{0}/{1}\" target=\"_blank\">ดูไฟล์</a>", id, GetFilename(path));
		}
	}
}

