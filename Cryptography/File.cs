using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SysFile = System.IO.File;
using SysFileInfo = System.IO.FileInfo;

namespace Cryptography
{
	public class File
	{
		private readonly byte[] _fileRawData;


		/// <summary>
		/// Current file name
		/// </summary>
		public string Name;
		/// <summary>
		/// Path to the directory containing this file
		/// </summary>
		public string Path { get; }
		/// <summary>
		/// Path to the directory containing this file with file file name and extension
		/// </summary>
		public string FullPath { get; }
		/// <summary>
		/// File extension
		/// </summary>
		public string Extension { get; }
		/// <summary>
		/// File size in bytes
		/// </summary>
		public long Length { get; }

		public File(string path)
		{
			if (SysFile.Exists(path))
			{
				_fileRawData = SysFile.ReadAllBytes(path);

				var fileInfo = new SysFileInfo(path);

				Name = fileInfo.Name;
				Path = fileInfo.DirectoryName;
				FullPath = fileInfo.FullName;
				Extension = fileInfo.Extension;
				Length = fileInfo.Length;
			}
			else
				throw new FileNotFoundException();
		}


		internal byte[] GetBytes()
		{
			return _fileRawData;
		}

		public override string ToString()
		{
			var properties =  new List<string>
			{
				$"File name: \t{Name}",
				$"File path: \t{Path}",
				$"File full path: {FullPath}",
				$"File Extension: {Extension}",
				$"File Lenght: \t{Length}",
			};
			return String.Join("\n", properties);
		}
	}


	class Analyzer
	{
		private Dictionary<string, string> _signatures;
		private Dictionary<string, int> _signaturesCount;
		private Dictionary<string, long> _signaturesLength;

		public Folder Folder { get; }


		public Analyzer(Folder folder)
		{
			this.Folder = folder;
		}


		public void ScanFolder()
		{

		}


		private void GetFilesExtension()
		{
			var t_info = new FolderInfo(this.Folder);
			var t_files = t_info.GetAllFiles();
			var t_extensions = new List<string>();

			_signatures = new Dictionary<string, string>();
			_signaturesCount = new Dictionary<string, int>();
			_signaturesLength = new Dictionary<string, long>();

			foreach (FileInfo file in t_files)
				t_extensions.Add(file.Extension);

			t_extensions = t_extensions.Distinct().ToList();

			foreach (string line in t_extensions)
			{
				if (!_signatures.ContainsKey(line.Replace(".", "").ToUpper()))
				{
					_signatures.Add(line.Replace(".", "").ToUpper(), line);
					_signaturesCount.Add(line.Replace(".", "").ToUpper(), t_info.GetSpecificFilesCount(line));
					_signaturesLength.Add(line.Replace(".", "").ToUpper(), t_info.GetSpecificFilesLength(line));
				}
			}
		}
	}


	class Folder
	{
		private FolderInfo _folderInfo;

		/// <summary>
		/// Имя директории
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Полный путь до директории
		/// </summary>
		public string Path { get; }
		/// <summary>
		/// Количество подпапок в директории
		/// </summary>
		public int FoldersCount { get; }
		/// <summary>
		/// Количество файлов в директории
		/// </summary>
		public int FilesCount { get; }
		/// <summary>
		/// Общее количество элементов в директории
		/// </summary>
		public int Count { get; }
		/// <summary>
		/// Размер текущей директории в байтах
		/// </summary>
		public long Length { get; }

		public Folder(string path)
		{
			Path = path;
			_folderInfo = new FolderInfo(this);
			Count = _folderInfo.GetCount();
			FoldersCount = _folderInfo.GetFoldersCount();
			FilesCount = _folderInfo.GetFilesCount();
			Name = _folderInfo.GetFolderName();
			Length = _folderInfo.GetDirectorySize();
		}
	}


	class FolderInfo
	{
		private Folder _folder;
		private DirectoryInfo _directoryInfo;

		public FolderInfo(Folder folder)
		{
			_folder = folder;
			_directoryInfo = new DirectoryInfo(_folder.Path);
		}


		/// <summary>
		/// Получение размера директории
		/// </summary>
		/// <returns></returns>
		public long GetDirectorySize()
		{
			long t_fileSize = 0;
			foreach (FileInfo file in _directoryInfo.GetFiles("*", SearchOption.AllDirectories))
			{
				t_fileSize += file.Length;
			}

			return t_fileSize;
		}
		/// <summary>
		/// Получение имени текущей директории
		/// </summary>
		/// <returns></returns>
		public string GetFolderName() => _directoryInfo.Name;
		/// <summary>
		/// Получение количества подпапок содержащихся в папке
		/// </summary>
		/// <returns>Возвращает количество подпапок в текущей папке</returns>
		public int GetFoldersCount() => _directoryInfo.GetDirectories().Length;
		/// <summary>
		/// Получение количества файлов содержащихся в папке
		/// </summary>
		/// <returns>Возвращает количество файлов в текущей папке</returns>
		public int GetFilesCount() => _directoryInfo.GetFiles().Length;
		/// <summary>
		/// Общее количество элементов включая папки
		/// </summary>
		/// <returns>Возвращает общее количество элементов в текущей папке</returns>
		public int GetCount() => GetFoldersCount() + GetFilesCount();




		/////////////////////////////////////////////////////////////////
		// DEBUG 
		/////////////////////////////////////////////////////////////////
		public FileInfo[] GetAllFiles()
		{
			if (GetFilesCount() > 0)
			{
				return _directoryInfo.GetFiles("*", SearchOption.AllDirectories);
			}
			return null;
		}

		public int GetSpecificFilesCount(string pattern)
		{
			return _directoryInfo.GetFiles("*" + pattern, SearchOption.AllDirectories).Length;
		}

		public long GetSpecificFilesLength(string pattern)
		{
			long t_size = 0;
			var t_files = _directoryInfo.GetFiles("*" + pattern, SearchOption.AllDirectories);

			foreach (var file in t_files)
			{
				t_size += file.Length;
			}

			return t_size;
		}

	}
}
