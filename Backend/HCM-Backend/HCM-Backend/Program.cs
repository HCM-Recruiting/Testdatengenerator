//----------------------------------------
// .Net Core WebApi project create script 
//           v8.1.2 from 2024-01-21
//   (C)Robert Grueneis/HTL Grieskirchen 
//----------------------------------------
using HCMBackend.Logging;
using GrueneisR.RestClientGenerator;
using Microsoft.OpenApi.Models;
using HCMBackend.Services;

string corsKey = "_myAllowSpecificOrigins";
string swaggerVersion = "v1";
string swaggerTitle = "HCM-Backend";
string restClientFolder = Environment.CurrentDirectory;
string restClientFilename = "_requests.http";

var builder = WebApplication.CreateBuilder(args);

#region -------------------------------------------- ConfigureServices
builder.Services.AddControllers();
builder.Services
  .AddEndpointsApiExplorer()
  .AddAuthorization()
  .AddSwaggerGen(x => x.SwaggerDoc(
    swaggerVersion,
    new OpenApiInfo { Title = swaggerTitle, Version = swaggerVersion }
  ))
  .AddCors(options => options.AddPolicy(
    corsKey,
    x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
  ))
  .AddRestClientGenerator(options => options
	  .SetFolder(restClientFolder)
	  .SetFilename(restClientFilename)
	  .SetAction($"swagger/{swaggerVersion}/swagger.json")
	  //.EnableLogging()
  );
builder.Services.AddLogging(x => x.AddCustomFormatter());
builder.Services.AddScoped<ApplicationService>();
builder.Services.AddScoped<CreateService>();
builder.Services.AddScoped<AuthService>();
#endregion

var app = builder.Build();

#region -------------------------------------------- Middleware pipeline
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	Console.WriteLine("++++ Swagger enabled: http://localhost:5000");
	app.UseSwagger();
	Console.WriteLine($@"++++ RestClient generating (after first request) to {restClientFolder}\{restClientFilename}");
	app.UseRestClientGenerator(); //Note: must be used after UseSwagger
	app.UseSwaggerUI(x => x.SwaggerEndpoint( $"/swagger/{swaggerVersion}/swagger.json", swaggerTitle));
}

app.UseCors(corsKey);
app.UseHttpsRedirection();
app.UseAuthorization();

//app.UseExceptionHandler(config => config.Run(async context =>
//{
//  context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//  context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json; //"application/json"
//  var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
//  if (error != null)
//  {
//    await context.Response.WriteAsync(
//      $"Exception: {error.Error?.Message} {error.Error?.InnerException?.Message}");
//  }
//}));
#endregion

app.Map("/", () => Results.Redirect("/swagger"));


app.MapControllers();
Console.WriteLine("Ready for clients...");
app.Run();


