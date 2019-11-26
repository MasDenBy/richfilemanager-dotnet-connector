//-----------------------------------------------------------------------
// <copyright file="ReadFileQuery.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	#region Usings

	using System.IO;
	using System.Threading.Tasks;

	using MasDen.RichFileManager.DotNetConnector.Interfaces;

	using Microsoft.AspNetCore.Http;

	#endregion

	/// <summary>
	/// Represents the command which provides data for a single file.
	/// </summary>
	/// <seealso cref="MasDen.RichFileManager.DotNetConnector.Components.Actions.ActionBase" />
	public class ReadFileQuery : ActionBase
	{
		#region Private Fields

		/// <summary>
		/// The file manager
		/// </summary>
		private readonly IFileManager fileManager;

		/// <summary>
		/// The path
		/// </summary>
		private readonly string path;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadFileQuery" /> class.
		/// </summary>
		/// <param name="query">The query.</param>
		/// <param name="fileManager">The file manager.</param>
		public ReadFileQuery(IQueryCollection query, IFileManager fileManager)
		{
			this.fileManager = fileManager;
			this.path = query["path"];
		}

		#endregion

		#region ActionBase Members

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public override async Task Execute(HttpResponse response)
		{
			var fileContent = this.fileManager.ReadFile(this.path);

			using (var ms = new MemoryStream(fileContent))
			{
				await ms.CopyToAsync(response.Body);
			}
		}

		#endregion
	}
}