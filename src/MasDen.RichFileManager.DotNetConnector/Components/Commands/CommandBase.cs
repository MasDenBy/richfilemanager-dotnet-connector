//-----------------------------------------------------------------------
// <copyright file="CommandBase.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Components.Commands
{
	#region Usings

	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Http;

	using Newtonsoft.Json;

	#endregion

	/// <summary>
	/// Represents the base class to the command.
	/// </summary>
	public abstract class CommandBase
	{
		#region Public Methods

		/// <summary>
		/// Executes the specified response.
		/// </summary>
		/// <param name="response">The HTTP response.</param>
		public abstract Task Execute(HttpResponse response);

		#endregion

		#region Protected Methods

		/// <summary>
		/// Serializes the result to json.
		/// </summary>
		/// <typeparam name="T">The type of result</typeparam>
		/// <param name="value">The value.</param>
		/// <returns>The json.</returns>
		protected string SerializeToJson<T>(T value) 
			where T : class
		{
			JsonSerializerSettings jsonSerializerOptions = new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			};

			return JsonConvert.SerializeObject(value, jsonSerializerOptions);
		}

		#endregion
	}
}