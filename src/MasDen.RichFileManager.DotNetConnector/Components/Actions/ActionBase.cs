//-----------------------------------------------------------------------
// <copyright file="ActionBase.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Entities;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Net.Http.Headers;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	#endregion

	/// <summary>
	/// Represents the base class to the action.
	/// </summary>
	public abstract class ActionBase
	{
		#region Constants

		/// <summary>
		/// The default buffer size
		/// </summary>
		private const int DefaultBufferSize = 0x1000;

		#endregion

		#region Private Fields

		private readonly HttpContext httpContext;

		#endregion

		#region Constructors

		protected ActionBase(HttpContext httpContext)
		{
			this.httpContext = httpContext;
		}

		#endregion

		#region Public Methods

		public abstract Task Execute();

		#endregion

		#region Protected Methods

		protected T GetService<T>()
		{
			return this.httpContext.RequestServices.GetService<T>();
		}

		protected async Task Response<T>(T value)
			where T : class
		{
			await this.httpContext.Response.WriteAsync(SerializeToJson(value));
		}

		protected async Task Response(ItemData value)
		{
			await this.httpContext.Response.WriteAsync(SerializeToJson(new ActionResult(value)));
		}

		protected async Task Response(ICollection<ItemData> values)
		{
			await this.httpContext.Response.WriteAsync(SerializeToJson(new ActionResultCollection(values)));
		}

		protected async Task ResponseFile(FileData fileData)
		{
			this.httpContext.Response.ContentType = fileData.ContentType;
			this.SetContentDispositionHeader(fileData);

			if (!Path.IsPathRooted(fileData.FilePath))
			{
				throw new InvalidOperationException($"The path {fileData.FilePath} is not rooted.");
			}

			using (var fileStream = GetFileStream(fileData.FilePath))
			{
				await fileStream.CopyToAsync(this.httpContext.Response.Body, DefaultBufferSize);
			}
		}

		#endregion

		#region Private Fields

		private static Stream GetFileStream(string path)
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

		private static string SerializeToJson<T>(T value)
			where T : class
		{
			JsonSerializerSettings jsonSerializerOptions = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore,
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				}
			};

			return JsonConvert.SerializeObject(value, jsonSerializerOptions);
		}

		private void SetContentDispositionHeader(FileData result)
		{
			if (!string.IsNullOrEmpty(result.FileName))
			{
				var contentDisposition = new ContentDispositionHeaderValue("attachment");
				contentDisposition.SetHttpFileName(result.FileName);
				this.httpContext.Response.Headers[HeaderNames.ContentDisposition] = contentDisposition.ToString();
			}
		}

		#endregion
	}
}