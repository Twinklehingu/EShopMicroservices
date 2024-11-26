var builder = WebApplication.CreateBuilder(args);

//added services to the container



var app = builder.Build();

//configure the HTTP request pipeline


app.MapGet("/", () => "Hello World!");

app.Run();
