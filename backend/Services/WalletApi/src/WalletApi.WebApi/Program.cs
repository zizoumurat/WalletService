using WalletApi.WebApi.Configurations;
using WalletApi.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .InstallServices(
    builder.Configuration, typeof(IServiceInstaller).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Wallet API V1");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.Run();
