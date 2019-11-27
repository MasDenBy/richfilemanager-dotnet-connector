using System.IO;
using System.Threading.Tasks;

using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Constants;
using MasDen.RichFileManager.DotNetConnector.Entities;
using MasDen.RichFileManager.DotNetConnector.Entities.Enumerations;
using MasDen.RichFileManager.DotNetConnector.Interfaces;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components.Actions
{
	[TestFixture]
	public class CopyCommandTest
	{
		#region Constants

		private const string Source = "source";
		private const string Target = "target";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private CopyCommand command;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = Substitute.For<Stream>();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.Copy, "GET");
			httpContext.Response.Returns(response);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Source, Source);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Target, Target);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.command = new CopyCommand(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldCopyTest()
		{
			// Arrange
			var itemData = new ItemData(Source, ItemType.File, null);

			this.fileManager.Copy(Source, Target).Returns(itemData);

			// Act
			await this.command.Execute();

			// Assert
			this.fileManager.Received().Copy(Source, Target);
			await this.httpContext.Response.Body.Received().WriteAsync(Arg.Any<byte[]>(), Arg.Any<int>(), Arg.Any<int>());
		}

		#endregion
	}
}