//-----------------------------------------------------------------------
// <copyright file="UploadCommand.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Extensions;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the command which upload files to the server.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class UploadCommand : ActionBase
	{
		#region Private Fields

		private readonly string path;

		private readonly IFormFileCollection files;

		#endregion

		#region Constructors

		public UploadCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.path = httpContext.Request.GetPath();
			this.files = httpContext.Request.Form.Files;
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var fileManager = this.GetService<IFileManager>();
			var result = fileManager.Upload(this.files, this.path);

			await this.Response(result);
		}

		#endregion
	}
}