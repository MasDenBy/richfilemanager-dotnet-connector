//-----------------------------------------------------------------------
// <copyright file="CommandFactory.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components
{
	#region Usings

	using System;

	using MasDen.RichFileManager.DotNetConnector.Components.Commands;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Represents the factory component which creates all commands.
	/// </summary>
	public static class CommandFactory
	{
		#region Constants

		/// <summary>
		/// The mode query key
		/// </summary>
		private const string ModeQueryKey = "mode";

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the command.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns>The <see cref="CommandBase"/> object.</returns>
		public static CommandBase CreateCommand(HttpContext context)
		{
			string mode = string.Empty;

			if(context.Request.Method == "POST")
			{
				mode = context.Request?.Form?[ModeQueryKey];
			}
			else
			{
				mode = context.Request.Query[ModeQueryKey];
			}

			switch (mode)
			{
				case "initiate":
					return new InitiateCommand(context.RequestServices.GetService<IConfigurationManager>());

				case "getfolder":
					return new GetFolderCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "getimage":
					return new GetImageCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "addfolder":
					return new AddFolderCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "delete":
					return new DeleteCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "getfile":
					return new GetFileCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "upload":
					return new UploadCommand(context.Request, context.RequestServices.GetService<IFileManager>());

				case "rename":
					return new RenameCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "move":
					return new MoveCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				case "copy":
					return new CopyCommand(context.Request.Query, context.RequestServices.GetService<IFileManager>());

				default:
					throw new InvalidOperationException();
			}
		}

		#endregion
	}
}