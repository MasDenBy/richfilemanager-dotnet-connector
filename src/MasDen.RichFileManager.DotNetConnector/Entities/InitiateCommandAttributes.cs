//-----------------------------------------------------------------------
// <copyright file="InitiateCommandAttributes.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Entities
{
	#region Usings

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the result attributes for the <see cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.InitiateCommand"/>.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Entities.AttributesBase" />
	public class InitiateCommandAttributes : AttributesBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InitiateCommandAttributes"/> class.
		/// </summary>
		public InitiateCommandAttributes()
		{
			this.Config = new InitiateCommandAttributesConfig();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		[JsonProperty("config")]
		public InitiateCommandAttributesConfig Config { get; set; }

		#endregion
	}
}