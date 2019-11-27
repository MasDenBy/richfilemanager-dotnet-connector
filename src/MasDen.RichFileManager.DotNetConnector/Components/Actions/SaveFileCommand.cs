using System.Threading.Tasks;
using MasDen.RichFileManager.DotNetConnector.Entities;
using MasDen.RichFileManager.DotNetConnector.Extensions;
using MasDen.RichFileManager.DotNetConnector.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MasDen.RichFileManager.DotNetConnector.Components.Actions
{
	public class SaveFileCommand : ActionBase
	{
		#region Private Fields

		private readonly HttpRequest request;

		private readonly IFileManager fileManager;

		#endregion

		#region Constructors

		public SaveFileCommand(HttpRequest request, IFileManager fileManager)
		{
			this.request = request;
			this.fileManager = fileManager;
		}

		#endregion

		#region Public Methods

		public override async Task Execute(HttpResponse response)
		{
			var path = this.request.GetPath();
			var content = this.request.GetContent();

			var modifiedFile = this.fileManager.SaveFile(path, content);

			await response.WriteAsync(this.SerializeToJson(new ActionResult(modifiedFile)));
		}

		#endregion
	}
}