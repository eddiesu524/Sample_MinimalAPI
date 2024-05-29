using Sample_MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//TODO 使用擴充方法來組織端點
app.SampleEndPoints();

app.Run();
