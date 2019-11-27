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
	using MasDen.RichFileManager.DotNetConnector.Constants;
	using MasDen.RichFileManager.DotNetConnector.Extensions;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the factory component which creates all actions.
	/// </summary>
	public static class ActionFactory
	{
		#region Public Methods

		/// <summary>
		/// Creates the action.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <returns>The <see cref="ActionBase"/> object.</returns>
		public static ActionBase CreateAction(HttpContext context)
		{
			var mode = context.Request.GetMode();

			switch (mode)
			{
				case ModeNames.Initiate:
					return new InitiateQuery(context);

				case ModeNames.ReadFolder:
					return new ReadFolderQuery(context);

				case ModeNames.GetImage:
					return new GetImageQuery(context);

				case ModeNames.AddFolder:
					return new AddFolderCommand(context);

				case ModeNames.Delete:
					return new DeleteCommand(context);

				case ModeNames.ReadFile:
					return new ReadFileQuery(context);

				case ModeNames.Upload:
					return new UploadCommand(context);

				case ModeNames.Rename:
					return new RenameCommand(context);

				case ModeNames.Move:
					return new MoveCommand(context);

				case ModeNames.Copy:
					return new CopyCommand(context);

				case ModeNames.SaveFile:
					return new SaveFileCommand(context);

				default:
					throw new InvalidOperationException();
			}
		}

		#endregion
	}
}