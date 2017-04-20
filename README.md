# richfilemanager-dotnet-connector

richfilemanager-dotnet-connector is a connector which helps use [RichFilemanager](https://github.com/servocoder/RichFilemanager) in ASP.NET Core aplications.

## Installation and Configuration
1. Create folder (for example "Files") inside wwwroot
2. Install nuget package
```
Install-Package MasDen.RichFileManager.DotNetConnector
```
3. Create configuration ([more about configuration](https://github.com/servocoder/RichFilemanager/wiki/Configuration-options)). RootPath is required.
```
"Filemanager": {
  "RootPath": "Files"
}
```
4. In Startup.cs

```
public Startup(IHostingEnvironment hostingEnvironment)
{
  var builder = new ConfigurationBuilder()
    .SetBasePath(hostingEnvironment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

  this.Configuration = builder.Build();
}

public IConfigurationRoot Configuration { get; }

public void ConfigureServices(IServiceCollection services)
{
  ...
  services.AddRichFileManager(conf => { this.Configuration.GetSection("Filemanager"); });
  ...
}
```
5. Install Richfilemanager ([link](https://github.com/servocoder/RichFilemanager/wiki/Deploy-and-setup))
6. Edit API section in "RichfileManager/config/filemanager.config.json"
```
"api": {
  "lang": "dotnet",
  ...
}
```
