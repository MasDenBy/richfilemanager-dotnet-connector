using MasDen.RichFileManager.DotNetConnector.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NSubstitute;

namespace MasDen.RichFileManager.DotNetConnector.Test
{
	public static class TestHelpers
	{
		#region Public Methods

		public static HttpContext CreateHttpContextMock(string mode, string method)
		{
			var httpRequest = CreateHttpRequestMock(method);
			AddQueryKey(httpRequest.Query, RequestKeys.Mode, mode);
			AddFormKey(httpRequest.Form, RequestKeys.Mode, mode);

			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Returns(httpRequest);

			return httpContext;
		}

		public static HttpRequest CreateHttpRequestMock(string method)
		{
			var httpRequest = Substitute.For<HttpRequest>();
			httpRequest.Method.Returns(method);
			httpRequest.Query = Substitute.For<IQueryCollection>();
			httpRequest.Form = Substitute.For<IFormCollection>();

			return httpRequest;
		}

		public static void AddQueryKey(IQueryCollection query, string key, string value)
		{
			query.ContainsKey(key).Returns(true);
			query[key].Returns(new StringValues(value));
		}

		public static void AddFormKey(IFormCollection form, string key, string value)
		{
			form.ContainsKey(key).Returns(true);
			form[key].Returns(new StringValues(value));
		}

		#endregion
	}
}