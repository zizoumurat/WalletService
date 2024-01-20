using TransactionApi.Infrasturcture.Hubs;
using TransactionApi.WebApi.Configurations;
using TransactionApi.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
    builder.Configuration, typeof(IServiceInstaller).Assembly);

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transaction API V1");
    });
}

app.MapHub<NotificationHub>("/hub");

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
