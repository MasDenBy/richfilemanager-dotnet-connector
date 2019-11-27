//-----------------------------------------------------------------------
// <copyright file="SaveFileCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which save file.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class SaveFileCommand : ActionBase
	{
		#region Private Fields

		private readonly string path;
		private readonly string content;

		#endregion

		#region Constructors

		public SaveFileCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.path = httpContext.Request.GetPath();
			this.content = httpContext.Request.GetContent();
		}

		#endregion

		#region Public Methods

		public override async Task Execute()
		{
			var modifiedFile = this.GetService<IFileManager>().SaveFile(path, content);

			await this.Response(modifiedFile);
		}

		#endregion
	}
}