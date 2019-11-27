using System;
using Microsoft.AspNetCore.Http;

namespace MasDen.RichFileManager.DotNetConnector.Extensions
{
	public static class HttpRequestExtensions
	{
		#region Public Methods

		public static string GetPath(this HttpRequest httpRequest)
		{
			return GetFormValue(httpRequest, "path");
		}

		public static string GetContent(this HttpRequest httpRequest)
		{
			return GetFormValue(httpRequest, "content");
		}

		#endregion

		#region Private Methods

		private static string GetFormValue(HttpRequest httpRequest, string key)
		{
			if(httpRequest.Form.ContainsKey(key))
			{
				return httpRequest.Form[key];
			}

			throw new InvalidOperationException($"Form key {key} does not exist in the request");
		}

		#endregion
	}
}