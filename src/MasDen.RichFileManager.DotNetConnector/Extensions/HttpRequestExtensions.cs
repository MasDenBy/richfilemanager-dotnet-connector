//-----------------------------------------------------------------------
// <copyright file="HttpRequestExtensions.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.Extensions
{
	#region Usings

	using System;

	using MasDen.RichFileManager.DotNetConnector.Constants;

	using Microsoft.AspNetCore.Http;

	#endregion

	public static class HttpRequestExtensions
	{
		#region Constants

		private const string PostMethodName = "POST";

		#endregion

		#region Public Methods

		public static string GetPath(this HttpRequest httpRequest)
		{
			return GetRequestValue(httpRequest, RequestKeys.Path);
		}

		public static string GetContent(this HttpRequest httpRequest)
		{
			return GetFormValue(httpRequest, RequestKeys.Content);
		}

		public static string GetMode(this HttpRequest httpRequest)
		{
			return GetRequestValue(httpRequest, RequestKeys.Mode);
		}

		public static string GetName(this HttpRequest httpRequest)
		{
			return GetQueryValue(httpRequest, RequestKeys.Name);
		}

		public static string GetOld(this HttpRequest httpRequest)
		{
			return GetQueryValue(httpRequest, RequestKeys.Old);
		}

		public static string GetNew(this HttpRequest httpRequest)
		{
			return GetQueryValue(httpRequest, RequestKeys.New);
		}

		public static string GetSource(this HttpRequest httpRequest)
		{
			return GetQueryValue(httpRequest, RequestKeys.Source);
		}

		public static string GetTarget(this HttpRequest httpRequest)
		{
			return GetQueryValue(httpRequest, RequestKeys.Target);
		}

		#endregion

		#region Private Methods

		private static string GetRequestValue(HttpRequest httpRequest, string key)
		{
			return httpRequest.Method == PostMethodName
				? GetFormValue(httpRequest, key)
				: GetQueryValue(httpRequest, key);
		}

		private static string GetFormValue(HttpRequest httpRequest, string key)
		{
			if(httpRequest.Form.ContainsKey(key))
			{
				return httpRequest.Form[key];
			}

			throw new InvalidOperationException($"Form key {key} does not exist in the request");
		}

		private static string GetQueryValue(HttpRequest httpRequest, string key)
		{
			if (httpRequest.Query.ContainsKey(key))
			{
				return httpRequest.Query[key];
			}

			throw new InvalidOperationException($"Query key {key} does not exist in the request");
		}

		#endregion
	}
}