//-----------------------------------------------------------------------
// <copyright file="RenameCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which renames an existed file or folder.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class RenameCommand : ActionBase
	{
		#region Private Fields

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		/// <summary>
		/// The old path
		/// </summary>
		private readonly string oldPath;

		/// <summary>
		/// The new path
		/// </summary>
		private readonly string newPath;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RenameCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public RenameCommand(IQueryCollection query, IFileManager fileManager)
		{
			this.fileManager = fileManager;
			this.oldPath = query["old"];
			this.newPath = query["new"];
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var rename = this.fileManager.Rename(this.oldPath, this.newPath);

			await response.WriteAsync(this.SerializeToJson(new CommandResult(rename)));
		}

		#endregion
	}
}