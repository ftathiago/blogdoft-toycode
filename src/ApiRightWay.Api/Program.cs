using ApiRightWay.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers().Services
    .ConfigSwashbuckle();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseDeveloperExceptionPage()
        .ConfigureSwagger();
}

app
    .UseHttpsRedirection()
    .UseAuthorization();
app.MapControllers();
app.Run();
