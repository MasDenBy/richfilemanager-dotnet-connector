//-----------------------------------------------------------------------
// <copyright file="FileManager.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Linq;

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.StaticFiles;

	#endregion

	/// <summary>
	/// Represents the file manager component.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Interfaces.IFileManager" />
	public class FileManager : IFileManager
	{
		#region Private Fields

		/// <summary>
		/// The image extensions
		/// </summary>
		private readonly string[] imgExtensions = new string[] { ".jpg", ".png", ".jpeg", ".gif", ".bmp" };

		/// <summary>
		/// The configuration manager
		/// </summary>
		private readonly IConfigurationManager configurationManager;

		/// <summary>
		/// The hosting environment
		/// </summary>
		private readonly IHostingEnvironment hostingEnvironment;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileManager"/> class.
		/// </summary>
		/// <param name="configurationManager">The configuration.</param>
		/// <param name="hostingEnvironment">The hosting environment.</param>
		public FileManager(IConfigurationManager configurationManager, IHostingEnvironment hostingEnvironment)
		{
			this.configurationManager = configurationManager;
			this.hostingEnvironment = hostingEnvironment;
		}

		#endregion

		#region IFileManager Members

		/// <summary>
		/// Gets the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The collection of <see cref="ItemData" /> from path.
		/// </returns>
		/// <exception cref="System.InvalidOperationException"></exception>
		public ICollection<ItemData> GetFolder(string path)
		{
			var directoryPath = this.GetServerPath(path);

			if (!Directory.Exists(directoryPath))
			{
				throw new InvalidOperationException($"Directory '{path}' not found");
			}

			ICollection<ItemData> result = new List<ItemData>();

			var directoryInfo = new DirectoryInfo(directoryPath);

			foreach (var dir in directoryInfo.GetDirectories())
			{
				result.Add(this.CreateFolderItemData(dir, path));
			}

			foreach (var file in directoryInfo.GetFiles())
			{
				string filePath = this.PrepareRelativePath(Path.Combine(path, file.Name));

				var attributes = CreateFileAttribute(file, path);

				result.Add(new FileItemData(FileManager.GetItemIdentifier(path, file.Name), attributes));
			}

			return result;
		}

		/// <summary>
		/// Gets the file data.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The <see cref="FileData" /> object.
		/// </returns>
		public FileData GetFileData(string path)
		{
			var serverPath = this.GetServerPath(path);

			if(!File.Exists(serverPath))
			{
				throw new InvalidOperationException($"The file {path} does not exists.");
			}

			FileInfo fileInfo = new FileInfo(serverPath);

			return new FileData()
			{
				ContentType = FileManager.GetContentType(fileInfo.Name),
				FileName = fileInfo.Name,
				FilePath = serverPath
			};
		}

		/// <summary>
		/// Creates the directory.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="folderName">Name of the folder.</param>
		/// <returns>
		/// The <see cref="ItemData" /> with all details of created folder.
		/// </returns>
		public ItemData CreateDirectory(string path, string folderName)
		{
			string rootPath = this.GetServerPath(path);
			string directoryPath = Path.Combine(rootPath, folderName);

			if(Directory.Exists(directoryPath))
			{
				throw new InvalidOperationException($"The directory {folderName} has already exists.");
			}

			Directory.CreateDirectory(directoryPath);
			DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

			return this.CreateFolderItemData(dirInfo, path);
		}

		/// <summary>
		/// Deletes the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The <see cref="ItemData" /> object with all information about deleted resource.
		/// </returns>
		public ItemData Delete(string path)
		{
			string fullPath = this.GetServerPath(path);

			System.IO.FileAttributes attributes = File.GetAttributes(fullPath);

			if((attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
				Directory.Delete(fullPath);

				return this.CreateFolderItemData(dirInfo, path.Replace(dirInfo.Name, string.Empty));
			}
			else
			{
				FileInfo fileInfo = new FileInfo(fullPath);

				var result = this.CreateFileItemData(fileInfo, path.Replace(fileInfo.Name, string.Empty));

				File.Delete(fullPath);

				return result;
			}
		}

		/// <summary>
		/// Uploads the specified files.
		/// </summary>
		/// <param name="files">The files.</param>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The collection of <see cref="ItemData" />.
		/// </returns>
		public ICollection<ItemData> Upload(IFormFileCollection files, string path)
		{
			string uploadPath = this.GetServerPath(path);

			if(!Directory.Exists(uploadPath))
			{
				throw new InvalidOperationException($"The file {path} does not exists.");
			}

			ICollection<ItemData> result = new List<ItemData>();

			foreach (var file in files)
			{
				string filePath = Path.Combine(uploadPath, file.FileName);

				if (File.Exists(filePath))
				{
					throw new InvalidOperationException($"File {file.FileName} exists in folder {path}");
				}

				using (FileStream fs = File.Create(filePath))
				{
					file.CopyTo(fs);
					fs.Flush();
				}

				result.Add(this.GetFile(path + file.FileName));
			}

			return result;
		}

		/// <summary>
		/// Renames the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="newName">The new name.</param>
		/// <returns>Information about new file or folder.</returns>
		public ItemData Rename(string path, string newName)
		{
			string fullPath = this.GetServerPath(path);

			System.IO.FileAttributes attributes = File.GetAttributes(fullPath);

			if ((attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				DirectoryInfo oldDirectory = new DirectoryInfo(fullPath);
				DirectoryInfo newDirecory = new DirectoryInfo(Path.Combine(oldDirectory.Parent.FullName, newName));

				Directory.Move(fullPath, newDirecory.FullName);

				return this.CreateFolderItemData(newDirecory, path.Replace(oldDirectory.Name, string.Empty));
			}
			else
			{
				FileInfo oldFile = new FileInfo(fullPath);
				FileInfo newFile = new FileInfo(Path.Combine(oldFile.Directory.FullName, newName));

				File.Move(fullPath, newFile.FullName);

				return this.CreateFileItemData(newFile, path.Replace(oldFile.Name, string.Empty));
			}
		}

		/// <summary>
		/// Moves the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="destination">The destination.</param>
		/// <returns>
		/// The <see cref="ItemData" /> object.
		/// </returns>
		public ItemData Move(string path, string destination)
		{
			string fullPath = this.GetServerPath(path);
			string destinationPath = this.GetServerPath(destination);

			if (!Directory.Exists(destinationPath))
			{
				Directory.CreateDirectory(destinationPath);
			}

			System.IO.FileAttributes attributes = File.GetAttributes(fullPath);

			if ((attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				DirectoryInfo oldDirectory = new DirectoryInfo(fullPath);
				DirectoryInfo newDirecory = new DirectoryInfo(Path.Combine(destinationPath, oldDirectory.Name));

				Directory.Move(fullPath, newDirecory.FullName);

				return this.CreateFolderItemData(newDirecory, destination);
			}
			else
			{
				FileInfo oldFile = new FileInfo(fullPath);
				FileInfo newFile = new FileInfo(Path.Combine(destinationPath, oldFile.Name));

				File.Move(fullPath, newFile.FullName);

				return this.CreateFileItemData(newFile, destination);
			}
		}

		/// <summary>
		/// Copies the specified source.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		/// <returns>The <see cref="ItemData" /> object.</returns>
		public ItemData Copy(string source, string target)
		{
			string fullPath = this.GetServerPath(source);
			string targetPath = this.GetServerPath(target);

			System.IO.FileAttributes attributes = File.GetAttributes(fullPath);

			if ((attributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
			{
				DirectoryInfo oldDirectory = new DirectoryInfo(fullPath);
				DirectoryInfo newDirecory = new DirectoryInfo(Path.Combine(targetPath, oldDirectory.Name));

				FileManager.CopyDirectory(oldDirectory, newDirecory);

				return this.CreateFolderItemData(newDirecory, target);
			}
			else
			{
				FileInfo oldFile = new FileInfo(fullPath);
				FileInfo newFile = new FileInfo(Path.Combine(targetPath, oldFile.Name));

				if (!Directory.Exists(targetPath))
				{
					Directory.CreateDirectory(targetPath);
				}

				File.Copy(fullPath, newFile.FullName);

				return this.CreateFileItemData(newFile, target);
			}
		}

		public byte[] ReadFile(string path)
		{
			var fullPath = GetFileServerPath(path);

			return File.ReadAllBytes(fullPath);
		}

		public ItemData SaveFile(string path, string content)
		{
			var fullPath = GetFileServerPath(path);

			File.WriteAllText(fullPath, content);

			return this.GetFile(path);
		}

		#endregion

		#region Private Fields

		/// <summary>
		/// Prepares the path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The path prepared in right format.</returns>
		private static string PreparePhysicalPath(string path)
		{
			path = FileManager.RemoveForwardSlashFromStart(path);

			return path.Replace("/", @"\");
		}

		/// <summary>
		/// Gets the time stamp.
		/// </summary>
		/// <param name="date">The date.</param>
		/// <returns>The time stamp</returns>
		private static int GetTimeStamp(DateTime date)
		{
			return (Int32)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
		}

		/// <summary>
		/// Removes the forward slash from start.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>The values without forward slash in the start.</returns>
		private static string RemoveForwardSlashFromStart(string value)
		{
			if (value.StartsWith("/"))
			{
				return value.Substring(1, value.Length - 1);
			}

			return value;
		}

		/// <summary>
		/// Gets the item identifier.
		/// </summary>
		/// <param name="parentPath">The parent path.</param>
		/// <param name="itemPath">The item path.</param>
		/// <returns>
		/// Returns the item identifier.
		/// </returns>
		private static string GetItemIdentifier(string parentPath, string itemPath)
		{
			return (parentPath.EndsWith("/") ? parentPath : $"{parentPath}/") + itemPath;
		}

		/// <summary>
		/// Gets the type of the content.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>The content type pf specified file.</returns>
		private static string GetContentType(string fileName)
		{
			string contentType;

			new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

			return contentType ?? "application/octet-stream";
		}

		/// <summary>
		/// Copies the directory.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="target">The target.</param>
		private static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
		{
			Directory.CreateDirectory(target.FullName);

			foreach (var file in source.GetFiles())
			{
				File.Copy(file.FullName, Path.Combine(target.FullName, file.Name));
			}

			foreach (var folder in source.GetDirectories())
			{
				FileManager.CopyDirectory(folder, new DirectoryInfo(Path.Combine(target.FullName, folder.Name)));
			}
		}

		/// <summary>
		/// Creates the folder item data.
		/// </summary>
		/// <param name="dir">The dir.</param>
		/// <param name="path">The path.</param>
		/// <returns>The <see cref="FolderItemData"/> object with all information about folder.</returns>
		private FolderItemData CreateFolderItemData(DirectoryInfo dir, string path)
		{
			FolderAttributes attributes = new FolderAttributes()
			{
				Created = dir.CreationTime,
				Modified = dir.LastWriteTime,
				Name = dir.Name,
				Path = this.PrepareRelativePath(Path.Combine(path, dir.Name)),
				Readable = true,
				Writable = true
			};

			return new FolderItemData(FileManager.GetItemIdentifier(path, dir.Name), attributes);
		}

		/// <summary>
		/// Creates the file item data.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <param name="path">The path.</param>
		/// <returns>The <see cref="FileItemData"/> object.</returns>
		private FileItemData CreateFileItemData(FileInfo file, string path)
		{
			string filePath = this.PrepareRelativePath(Path.Combine(path, file.Name));

			Entities.FileAttributes attributes = CreateFileAttribute(file, path);

			return new FileItemData(FileManager.GetItemIdentifier(path, file.Name), attributes);
		}

		/// <summary>
		/// Prepares the relative path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The path without incorrect symbols</returns>
		private string PrepareRelativePath(string path)
		{
			var configuration = this.configurationManager.GetConfiguration();

			string rootFolder = !configuration.RootPath.StartsWith("\\") 
				? $"\\{configuration.RootPath}" 
				: configuration.RootPath;

			return Path.Combine(rootFolder, FileManager.RemoveForwardSlashFromStart(path)).Replace("\\", "/");
		}

		/// <summary>
		/// Determines whether the specified file information is image.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <returns><c>True</c> if specified file is image.</returns>
		private bool IsImage(FileInfo fileInfo)
		{
			return this.imgExtensions.Any(e => e.Equals(fileInfo.Extension.ToLowerInvariant()));
		}

		/// <summary>
		/// Gets the server path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The physical path</returns>
		private string GetServerPath(string path)
		{
			if (path.Equals("/", StringComparison.OrdinalIgnoreCase))
			{
				return this.GetRootPath();
			}

			return Path.Combine(this.GetRootPath(), FileManager.PreparePhysicalPath(path));
		}

		/// <summary>
		/// Gets the root path.
		/// </summary>
		/// <returns>The full path to root directory.</returns>
		private string GetRootPath()
		{
			return Path.Combine(this.hostingEnvironment.WebRootPath, this.configurationManager.GetConfiguration().RootPath);
		}

		private Entities.FileAttributes CreateFileAttribute(FileInfo file, string path)
		{
			var attributes = new Entities.FileAttributes()
			{
				Created = file.CreationTime,
				Modified = file.LastWriteTime,
				Name = file.Name,
				Path = this.PrepareRelativePath(Path.Combine(path, file.Name)),
				Readable = true,
				Writable = true,
				Size = file.Length
			};

			if (this.IsImage(file))
			{
				using (Image img = Image.FromFile(file.FullName))
				{
					attributes.Height = img.Height;
					attributes.Width = img.Width;
				}
			}

			return attributes;
		}

		private ItemData GetFile(string path)
		{
			var fullPath = this.GetFileServerPath(path);
			var fileInfo = new FileInfo(fullPath);

			return this.CreateFileItemData(fileInfo, path.Replace(fileInfo.Name, string.Empty));
		}

		private string GetFileServerPath(string path)
		{
			var fullPath = this.GetServerPath(path);

			if (!File.Exists(fullPath))
			{
				throw new InvalidOperationException($"The file {path} does not exists.");
			}

			return fullPath;
		}

		#endregion
	}
}