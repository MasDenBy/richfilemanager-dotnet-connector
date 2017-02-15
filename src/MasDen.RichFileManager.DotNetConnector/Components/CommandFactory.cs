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
	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Options;

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
		public static CommandBase CreateCommand(IQueryCollection query, IOptions<FileManagerConfiguration> configuration)
		{
			var mode = query[ModeQueryKey];

			switch (mode)
			{
				case "initiate":
					return new InitiateCommand(query, configuration);

				case "getfolder":
					return null;

				default:
					throw new InvalidOperationException();
			}
		}

		#endregion
	}
}