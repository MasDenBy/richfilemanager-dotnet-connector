//-----------------------------------------------------------------------
// <copyright file="GetFolderCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which return information about folder.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class GetFolderCommand : ActionBase
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

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="GetFolderCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public GetFolderCommand(IQueryCollection query, IFileManager fileManager)
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
			var result = new CommandResultCollection(this.fileManager.GetFolder(this.path));

			await response.WriteAsync(this.SerializeToJson(result));
		}

		#endregion
	}
}