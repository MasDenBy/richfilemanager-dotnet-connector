using System.IO;
using System.Threading.Tasks;

using MasDen.RichFileManager.DotNetConnector.Components.Actions;
using MasDen.RichFileManager.DotNetConnector.Constants;
using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;
using MasDen.RichFileManager.DotNetConnector.Interfaces;

using Microsoft.AspNetCore.Http;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components.Actions
{
	[TestFixture]
	public class InitiateQueryTest
	{
		#region Private Fields

		private HttpContext httpContext;

		private InitiateQuery query;

		private IConfigurationManager configurationManager;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.configurationManager = Substitute.For<IConfigurationManager>();

			var response = Substitute.For<HttpResponse>();
			response.Body = new MemoryStream();

			this.httpContext = TestHelpers.CreateHttpContextMock(ModeNames.Initiate, "GET");
			httpContext.Response.Returns(response);
			httpContext.RequestServices.GetService(typeof(IConfigurationManager)).Returns(this.configurationManager);

			this.query = new InitiateQuery(this.httpContext);
		}

		#endregion

		#region Tests

		[Test]
		public async Task ExecuteShouldDeleteTest()
		{
			// Arrange
			var config = new FileManagerConfiguration
			{
				RootPath = "/root"
			};

			this.configurationManager.GetConfiguration().Returns(config);

			// Act
			await this.query.Execute();

			// Assert
			this.configurationManager.Received().GetConfiguration();
			Assert.IsTrue(this.httpContext.Response.Body.Length > 0);
		}

		#endregion
	}
}