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
	public class ReadFolderQueryTest
	{
		#region Constants

		private const string Path = "/Resources/";

		#endregion

		#region Private Fields

		private HttpContext httpContext;

		private ReadFolderQuery query;

		private IFileManager fileManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.fileManager = Substitute.For<IFileManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.ReadFolder, "GET");
			httpContext.Response.Returns(response);
			TestHelpers.AddQueryKey(this.httpContext.Request.Query, RequestKeys.Path, Path);

			this.httpContext.RequestServices.GetService(typeof(IFileManager)).Returns(this.fileManager);

			this.query = new ReadFolderQuery(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldReadFolderStructureTest()
		{
			// Arrange
			var folderData = new[] { new ItemData("", ItemType.File, null) };

			this.fileManager.GetFolder(Path).Returns(folderData);

			// Act
			await this.query.Execute();

			// Assert
			this.fileManager.Received().GetFolder(Path);
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}