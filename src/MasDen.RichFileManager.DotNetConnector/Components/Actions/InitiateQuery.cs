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
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class InitiateQuery : ActionBase
	{
		#region Constructors

		public InitiateQuery(HttpContext httpContext)
			: base(httpContext)
		{
		}

		#endregion

		#region ActionBase Members

		public override async Task Execute()
		{
			var configurationManager = this.GetService<IConfigurationManager>();
			var configuration = configurationManager.GetConfiguration();

			var attributes = new InitiateCommandAttributes();
			attributes.Config.Security = configuration.Security;
			attributes.Config.Upload = configuration.Upload;

			var result = new ActionResult("/", ItemType.Initiate, attributes);

			await this.Response(result);
		}

		#endregion
	}
}