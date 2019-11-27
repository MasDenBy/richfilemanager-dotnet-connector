using System;

using MasDen.RichFileManager.DotNetConnector.Components;
using MasDen.RichFileManager.DotNetConnector.Entities.Configuration;

using Microsoft.Extensions.Options;

using NSubstitute;

using NUnit.Framework;

namespace MasDen.RichFileManager.DotNetConnector.Test.Components
{
	[TestFixture]
	public class ConfigurationManagerTest
	{
		#region Private Fields

		private ConfigurationManager configurationManager;

		private IOptions<FileManagerConfiguration> options;

		#endregion

		#region Test Context

		[SetUp]
		public void Setup()
		{
			this.options = Substitute.For<IOptions<FileManagerConfiguration>>();
			this.configurationManager = new ConfigurationManager(this.options);
		}

		#endregion

		#region Tests

		[Test]
		public void GetConfigurationIfOptionsAreNullShouldThrowExceptionTest()
		{
			// Arrange
			this.options = null;

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => this.configurationManager.GetConfiguration());
		}

		[Test]
		public void GetConfigurationIfOptionsValueIsNullShouldThrowExceptionTest()
		{
			// Arrange
			this.options.Value.Returns((FileManagerConfiguration)null);

			// Act & Assert
			Assert.Throws<InvalidOperationException>(() => this.configurationManager.GetConfiguration());
		}

		[Test]
		public void GetConfigurationIfOptionsIsNotNullShouldReturnConfigurationTest()
		{
			// Arrange
			var configuration = new FileManagerConfiguration
			{
				RootPath = "path",
				Security = new SecuritySection
				{
					ReadOnly = true
				},
				Upload = new UploadSection
				{
					ChunkSize = true
				}
			};

			this.options.Value.Returns(configuration);

			// Act
			var actualOptions = this.configurationManager.GetConfiguration();

			// Assert
			Assert.AreEqual(configuration, actualOptions);
		}

		#endregion
	}
}