//-----------------------------------------------------------------------
// <copyright file="CommandBase.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Commands
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Entities;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the base class to the command.
	/// </summary>
	public abstract class CommandBase
	{
		#region Private Fields

		/// <summary>
		/// The query
		/// </summary>
		private readonly IQueryCollection query;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandBase"/> class.
		/// </summary>
		/// <param name="query">The query.</param>
		protected CommandBase(IQueryCollection query)
		{
			this.query = query;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Executes this instance.
		/// </summary>
		/// <returns>The result of operation.</returns>
		public abstract CommandResult Execute();

		#endregion
	}
}