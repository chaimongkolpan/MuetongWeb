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
	}
}

