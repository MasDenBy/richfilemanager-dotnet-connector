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
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the command which created new directory in file system.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class AddFolderCommand : ActionBase
	{
		#region Private Fields

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		/// <summary>
		/// The path
		/// </summary>
		private readonly string path;

		/// <summary>
		/// The name
		/// </summary>
		private readonly string name;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AddFolderCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public AddFolderCommand(IQueryCollection query, IFileManager fileManager)
		{
			this.fileManager = fileManager;
			this.path = query["path"];
			this.name = query["name"];
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var folderItemData = this.fileManager.CreateDirectory(this.path, this.name);

			await response.WriteAsync(this.SerializeToJson(new CommandResult(folderItemData)));
		}

		#endregion
	}
}