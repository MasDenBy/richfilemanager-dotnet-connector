//-----------------------------------------------------------------------
// <copyright file="GetImageCommand.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System;
	using System.IO;
	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Net.Http.Headers;

	#endregion

	/// <summary>
	/// Represents the command to get image.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class GetImageCommand : ActionBase
	{
		#region Constants

		/// <summary>
		/// The default buffer size
		/// </summary>
		private const int DefaultBufferSize = 0x1000;

		#endregion

		#region Private Fields

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		/// <summary>
		/// The path
		/// </summary>
		private readonly string path;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GetImageCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public GetImageCommand(IQueryCollection query, IFileManager fileManager)
		{
			this.fileManager = fileManager;
			this.path = query["path"];
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var imageData = this.fileManager.GetFileData(this.path);

			response.ContentType = imageData.ContentType;
			this.SetContentDispositionHeader(imageData, response);

			if (!Path.IsPathRooted(imageData.FilePath))
			{
				throw new NotSupportedException($"The path{imageData.FilePath} is not rooted.");
			}

			var fileStream = this.GetFileStream(imageData.FilePath);

			using (fileStream)
			{
				await fileStream.CopyToAsync(response.Body, DefaultBufferSize);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the content disposition header.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <param name="response">The response.</param>
		private void SetContentDispositionHeader(FileData result, HttpResponse response)
		{
			if (!string.IsNullOrEmpty(result.FileName))
			{
				var contentDisposition = new ContentDispositionHeaderValue("attachment");
				contentDisposition.SetHttpFileName(result.FileName);
				response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
			}
		}

		/// <summary>
		/// Gets the file stream.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The <see cref="Stream"/> associaed with file.</returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		private Stream GetFileStream(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			return new FileStream(
					path,
					FileMode.Open,
					FileAccess.Read,
					FileShare.ReadWrite,
					DefaultBufferSize,
					FileOptions.Asynchronous | FileOptions.SequentialScan);
		}

		#endregion
	}
}