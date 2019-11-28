//-----------------------------------------------------------------------
// <copyright file="IActionFactory.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Interfaces
{
	#region Usings

	using MasDen.RichFileManager.DotNetConnector.Components.Actions;

	using Microsoft.AspNetCore.Http;

	#endregion

	public interface IActionFactory
	{
		ActionBase CreateAction(HttpContext context);
	}
}