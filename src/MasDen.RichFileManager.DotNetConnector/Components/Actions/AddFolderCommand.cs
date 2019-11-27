//-----------------------------------------------------------------------
// <copyright file="AddFolderCommand.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Extensions;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the command which created new directory in file system.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class AddFolderCommand : ActionBase
	{
		#region Private Fields

		private readonly string path;

		private readonly string name;

		#endregion

		#region Constructors

		public AddFolderCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.path = httpContext.Request.GetPath();
			this.name = httpContext.Request.GetName();
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var fileManager = this.GetService<IFileManager>();
			var folderItemData = fileManager.CreateDirectory(this.path, this.name);

			await this.Response(folderItemData);
		}

		#endregion
	}
}