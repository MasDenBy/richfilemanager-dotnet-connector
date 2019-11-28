using System;
using System.IO;
using System.Threading.Tasks;

using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Interfaces;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test
{
	[TestFixture]
	public class FileManagerMiddlewareTest
	{
		#region Private Fields

		private FileManagerMiddleware middleware;

		private RequestDelegate requestDelegate;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.requestDelegate = Substitute.For<RequestDelegate>();
			this.middleware = new FileManagerMiddleware(this.requestDelegate);
		}

		#endregion

		#region Tests

		[Test]
		public async Task InvokeIfPathIncorrectShouldInvokeNextTest()
		{
			// Arrange
			var httpContext = CreateHttpContext(null);

			// Act
			await this.middleware.Invoke(httpContext);

			// Assert
			await this.requestDelegate.Received().Invoke(httpContext);
		}

		[Test]
		public async Task InvokePathCorrectShouldInvokeActionTest()
		{
			// Arrange
			var action = Substitute.For<TestAction>();

			var actionFactory = Substitute.For<IActionFactory>();
			actionFactory.CreateAction(Arg.Any<HttpContext>()).Returns(action);

			var httpContext = CreateHttpContext("/filemanager.dotnet");
			httpContext.RequestServices = Substitute.For<IServiceProvider>();
			httpContext.RequestServices.GetService(typeof(IActionFactory)).Returns(actionFactory);

			// Act
			await this.middleware.Invoke(httpContext);

			// Assert
			await action.Received().Execute();
		}

		[Test]
		public async Task InvokeIfExceptionThrowsShouldResponse500Test()
		{
			// Arrange
			const string exceptionMessage = "Error message";

			var action = Substitute.For<TestAction>();
			action.Execute().Returns(x => { throw new InvalidOperationException(exceptionMessage); });

			var actionFactory = Substitute.For<IActionFactory>();
			actionFactory.CreateAction(Arg.Any<HttpContext>()).Returns(action);

			var httpContext = CreateHttpContext("/filemanager.dotnet");
			httpContext.RequestServices = Substitute.For<IServiceProvider>();
			httpContext.RequestServices.GetService(typeof(IActionFactory)).Returns(actionFactory);
			httpContext.Response.Returns(Substitute.For<HttpResponse>());
			httpContext.Response.Body = new MemoryStream();

			// Act
			await this.middleware.Invoke(httpContext);
			var response = ReadStream(httpContext.Response.Body);

			// Assert
			Assert.AreEqual("{\"errors\":[{\"id\":\"server\",\"code\":\"500\",\"title\":\"Error message\"}]}", response);
			
		}

		#endregion

		#region Private Methods

		private static HttpContext CreateHttpContext(PathString path)
		{
			var httpRequest = Substitute.For<HttpRequest>();
			httpRequest.Path = path;

			var httpContext = Substitute.For<HttpContext>();
			httpContext.Request.Returns(httpRequest);

			return httpContext;
		}

		private static string ReadStream(Stream stream)
		{
			if (stream.Position > 0)
				stream.Seek(0, SeekOrigin.Begin);

			using (TextReader tr = new StreamReader(stream))
			{
				return tr.ReadToEnd();
			}
		}

		#endregion

		#region Nested Types

		public abstract class TestAction : ActionBase
		{
			public TestAction() : base(null) { }
		}

		#endregion
	}
}