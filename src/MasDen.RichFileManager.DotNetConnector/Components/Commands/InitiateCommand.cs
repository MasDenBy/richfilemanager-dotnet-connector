//-----------------------------------------------------------------------
// <copyright file="InitiateCommand.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Commands
{
	#region Usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using MasDen.RichFileManager.DotNetConnector.Entities;
	using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;
	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the "initiate" command.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Commands.CommandBase" />
	public class InitiateCommand : CommandBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InitiateCommand"/> class.
		/// </summary>
		/// <param name="query">The query.</param>
		public InitiateCommand(IQueryCollection query)
			: base(query)
		{
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

			return new CommandResult("/", CommandType.Initiate, attributes);
		}

		#endregion
	}
}