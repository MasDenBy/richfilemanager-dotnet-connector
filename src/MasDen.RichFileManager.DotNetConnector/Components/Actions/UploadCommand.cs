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

	using MasDen.RichFileManager.DotNetConnector.Entities;
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

		/// <summary>
		/// The request
		/// </summary>
		private readonly HttpRequest request;

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UploadCommand" /> class.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="fileManager">The file manager.</param>
		public UploadCommand(HttpRequest request, IFileManager fileManager)
		{
			this.request = request;
			this.fileManager = fileManager;
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		/// <returns></returns>
		public override async Task Execute(HttpResponse response)
		{
			string path = this.request.Form["path"];

			var result = this.fileManager.Upload(this.request.Form.Files, path);

			await response.WriteAsync(this.SerializeToJson(new CommandResultCollection(result)));
		}

		#endregion
	}
}