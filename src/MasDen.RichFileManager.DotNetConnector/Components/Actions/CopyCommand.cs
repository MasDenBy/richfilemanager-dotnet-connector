//-----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which copies file or folder to specified directory.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class CopyCommand : ActionBase
	{
		#region Private Fields

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		/// <summary>
		/// The source
		/// </summary>
		private readonly string source;

		/// <summary>
		/// The target
		/// </summary>
		private readonly string target;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CopyCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public CopyCommand(IQueryCollection query, IFileManager fileManager)
		{
			this.fileManager = fileManager;
			this.source = query["source"];
			this.target = query["target"];
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var item = this.fileManager.Copy(this.source, this.target);

			await response.WriteAsync(this.SerializeToJson(new CommandResult(item)));
		}

		#endregion
	}
}