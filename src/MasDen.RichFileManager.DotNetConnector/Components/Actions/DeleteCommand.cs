//-----------------------------------------------------------------------
// <copyright file="DeleteCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the delete command.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class DeleteCommand : ActionBase
	{
		#region Private Fields

		private readonly string path;

		#endregion

		#region Constructors

		public DeleteCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.path = httpContext.Request.GetPath();
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var fileManager = this.GetService<IFileManager>();
			var result = fileManager.Delete(this.path);

			await this.Response(result);
		}

		#endregion
	}
}