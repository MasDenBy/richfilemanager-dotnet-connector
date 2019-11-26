//-----------------------------------------------------------------------
// <copyright file="InitiateQuery.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;
	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the "initiate" query.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.ActionBase" />
	public class InitiateQuery : ActionBase
	{
		#region Private Fields

		/// <summary>
		/// The configuration manager
		/// </summary>
		private readonly IConfigurationManager configurationManager;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InitiateQuery" /> class.
		/// </summary>
		/// <param name="configurationManager">The configuration manager.</param>
		public InitiateQuery(IConfigurationManager configurationManager)
		{
			this.configurationManager = configurationManager;
		}

		#endregion

		#region CommandBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var configuration = this.configurationManager.GetConfiguration();

			var attributes = new InitiateCommandAttributes();
			attributes.Config.Security = configuration.Security;
			attributes.Config.Upload = configuration.Upload;

			var result = new CommandResult("/", ItemType.Initiate, attributes);

			await response.WriteAsync(this.SerializeToJson(result));
		}

		#endregion
	}
}