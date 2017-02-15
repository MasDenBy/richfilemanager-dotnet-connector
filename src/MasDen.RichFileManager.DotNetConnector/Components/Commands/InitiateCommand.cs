//-----------------------------------------------------------------------
// <copyright file="InitiateCommand.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Commands
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;
	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;

	using Microsoft.AspNetCore.Http;

	using Microsoft.Extensions.Options;

	#endregion

	/// <summary>
	/// Represents the "initiate" command.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.CommandBase" />
	public class InitiateCommand : CommandBase
	{
		#region Private Fields

		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IOptions<FileManagerConfiguration> configuration;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InitiateCommand" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="configuration">The configuration.</param>
		public InitiateCommand(IQueryCollection query, IOptions<FileManagerConfiguration> configuration)
			: base(query)
		{
			this.configuration = configuration;
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes this instance.
		/// </summary>
		/// <returns>The result of command.</returns>
		public override CommandResult Execute()
		{
			var attributes = new InitiateCommandAttributes();
			attributes.Config.Options = this.configuration.Value.Options;
			attributes.Config.Security = this.configuration.Value.Security;
			attributes.Config.Upload = this.configuration.Value.Upload;

			return new CommandResult("/", CommandType.Initiate, attributes);
		}

		#endregion
	}
}