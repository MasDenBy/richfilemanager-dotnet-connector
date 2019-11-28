//-----------------------------------------------------------------------
// <copyright file="IntegrationTestBase.cs" author="Ihar Maiseyeu">
//     Copyright Ihar Maiseyeu. All rights reserved.
//     Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

namespace MasDen.RichFileManager.DotNetConnector.RegressionTest.Infrastructure
{
	#region Usings

	using System.Net.Http;

	using Microsoft.AspNetCore.Hosting;
	using Microsoft.AspNetCore.TestHost;

	using NUnit.Framework;

	#endregion

	/// <summary>
	/// Represents the base class for tests.
	/// </summary>
	[TestFixture]
	public abstract class IntegrationTestBase
	{
		#region Private Fields

		/// <summary>
		/// The web host builder
		/// </summary>
		private IWebHostBuilder webHostBuilder;

		/// <summary>
		/// The host
		/// </summary>
		private TestServer host;

		#endregion

		#region Protected Properties

		/// <summary>
		/// Gets the client.
		/// </summary>
		/// <value>
		/// The client.
		/// </value>
		protected HttpClient Client { get; private set; }

		#endregion

		#region Test Context

		/// <summary>
		/// Setups this instance.
		/// </summary>
		[SetUp]
		public void Setup()
		{
			this.webHostBuilder = new WebHostBuilder()
				.UseStartup<TestStartup>();

			this.host = new TestServer(this.webHostBuilder);
			this.Client = host.CreateClient();
		}

		/// <summary>
		/// Tears down.
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			this.Client.Dispose();
			this.host.Dispose();
		}

		#endregion
	}
}