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
		public static string GetUrlTag(long id, string ext, string path)
		{
			if (FileConstants.ImageExt.Contains(ext))
				return string.Format("<a href=\"./file/{0}/{1}\" target=\"_blank\"><img src=\"./file/{0}/{1}\" style=\"width: 50%;\"/></a>", id, GetFilename(path));
			else
				return string.Format("<a href=\"./file/{0}/{1}\" target=\"_blank\">ดูไฟล์</a>", id, GetFilename(path));
		}
	}
}

