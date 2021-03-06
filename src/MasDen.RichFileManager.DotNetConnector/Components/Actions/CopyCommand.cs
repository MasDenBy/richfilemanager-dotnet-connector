﻿//-----------------------------------------------------------------------
// <copyright file="CopyCommand.cs" author="Ihar Maiseyeu">
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
	/// Represents the command which copies file or folder to specified directory.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class CopyCommand : ActionBase
	{
		#region Private Fields

		private readonly string source;

		private readonly string target;

		#endregion

		#region Constructors

		public CopyCommand(HttpContext httpContext)
			: base(httpContext)
		{
			this.source = httpContext.Request.GetSource();
			this.target = httpContext.Request.GetTarget();
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var item = this.GetService<IFileManager>().Copy(this.source, this.target);

			await this.Response(item);
		}

		#endregion
	}
}