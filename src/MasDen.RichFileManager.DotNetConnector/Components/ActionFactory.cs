//-----------------------------------------------------------------------
// <copyright file="ActionFactory.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components
{
	#region Usings

	using System;

	using MasDen.RichFileManager.DotNetConnector.Components.Actions;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.DependencyInjection;

	#endregion

	/// <summary>
	/// Represents the factory component which creates all actions.
	/// </summary>
	public static class ActionFactory
	{
		#region Constants

		/// <summary>
		/// The mode query key
		/// </summary>
		private const string ModeQueryKey = "mode";

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the action.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns>The <see cref="ActionBase"/> object.</returns>
		public static ActionBase CreateAction(HttpContext context)
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

			var fileManager = context.RequestServices.GetService<IFileManager>();

			switch (mode)
			{
				case "initiate":
					return new InitiateQuery(context.RequestServices.GetService<IConfigurationManager>());

				case "readfolder":
					return new ReadFolderQuery(context.Request.Query, fileManager);

				case "getimage":
					return new GetImageCommand(context.Request.Query, fileManager);

				case "addfolder":
					return new AddFolderCommand(context.Request.Query, fileManager);

				case "delete":
					return new DeleteCommand(context.Request.Query, fileManager);

				case "getfile":
					return new GetFileCommand(context.Request.Query, fileManager);

				case "upload":
					return new UploadCommand(context.Request, fileManager);

				case "rename":
					return new RenameCommand(context.Request.Query, fileManager);

				case "move":
					return new MoveCommand(context.Request.Query, fileManager);

				case "copy":
					return new CopyCommand(context.Request.Query, fileManager);

				default:
					throw new InvalidOperationException();
			}
		}

		#endregion
	}
}