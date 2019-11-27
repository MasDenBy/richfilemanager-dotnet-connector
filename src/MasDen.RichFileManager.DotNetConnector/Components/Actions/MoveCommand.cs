//-----------------------------------------------------------------------
// <copyright file="MoveCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which renames an existed file or folder.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class MoveCommand : ActionBase
	{
		#region Private Fields

		private readonly string oldPath;
		private readonly string newPath;

		#endregion

		#region Constructors

		public MoveCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.oldPath = httpContext.Request.GetOld();
			this.newPath = httpContext.Request.GetNew();
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var rename = this.GetService<IFileManager>().Move(this.oldPath, this.newPath);

			await this.Response(rename);
		}

		#endregion
	}
}