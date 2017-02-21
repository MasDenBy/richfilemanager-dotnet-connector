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
	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;
	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Hosting;

	using Microsoft.Extensions.Options;

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
		/// The configuration
		/// </summary>
		private readonly IOptions<FileManagerConfiguration> configuration;

		/// <summary>
		/// The hosting environment
		/// </summary>
		private readonly IHostingEnvironment hostingEnvironment;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileManager"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="hostingEnvironment">The hosting environment.</param>
		public FileManager(IOptions<FileManagerConfiguration> configuration, IHostingEnvironment hostingEnvironment)
		{
			this.configuration = configuration;
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
				FolderAttributes attributes = new FolderAttributes()
				{
					Created = dir.CreationTime,
					Modified = dir.LastWriteTime,
					Name = dir.Name,
					Path = this.PrepareRelativePath(Path.Combine(path, dir.Name)),
					Readable = true,
					TimeStamp = FileManager.GetTimeStamp(dir.CreationTime),
					Writable = true
				};
				result.Add(new FolderItemData(FileManager.GetItemIdentifier(path, dir.Name), attributes));
			}

			foreach (var file in directoryInfo.GetFiles())
			{
				string filePath = this.PrepareRelativePath(Path.Combine(path, file.Name));

				Entities.FileAttributes attributes = new Entities.FileAttributes()
				{
					Created = file.CreationTime,
					Modified = file.LastWriteTime,
					Name = file.Name,
					Extension = file.Extension.Replace(".", string.Empty),
					Path = this.PrepareRelativePath(Path.Combine(path, file.Name)),
					Readable = true,
					TimeStamp = FileManager.GetTimeStamp(file.CreationTime),
					Writable = true,
					Size = file.Length
				};

				if(this.IsImage(file))
				{
					using (Image img = Image.FromFile(file.FullName))
					{
						attributes.Height = img.Height;
						attributes.Width = img.Width;
					}
				}

				result.Add(new FileItemData(FileManager.GetItemIdentifier(path, file.Name), attributes));
			}

			return result;
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
		/// Prepares the relative path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The path without incorrect symbols</returns>
		private string PrepareRelativePath(string path)
		{
			string rootFolder = !this.configuration.Value.RootPath.StartsWith("\\") 
				? $"\\{this.configuration.Value.RootPath}" 
				: this.configuration.Value.RootPath;

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
			return Path.Combine(this.hostingEnvironment.ContentRootPath, this.configuration.Value.RootPath);
		}

		#endregion
	}
}