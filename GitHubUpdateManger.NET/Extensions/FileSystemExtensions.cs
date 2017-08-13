using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUpdateManger.Extensions
{
	public static class FileSystemExtensions
	{
		public static DirectoryInfo EnsureDirectory(string filePath)
		{
			var directoryPath = Path.GetDirectoryName(filePath);

			return Directory.CreateDirectory(directoryPath);
		}
	}
}
