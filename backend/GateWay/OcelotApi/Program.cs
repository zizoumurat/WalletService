using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder
            .WithOrigins("http://localhost:4002")
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
                        .AddJwtBearer("GatewayAuthenticationScheme", options =>
                        {
                            options.Authority = builder.Configuration["IdentityServerURL"];
                            options.Audience = "Resource_GateWay";
                            options.RequireHttpsMetadata = false;
                        });


var app = builder.Build();

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}
app.UseSwaggerForOcelotUI().UseOcelot().Wait();
app.UseDeveloperExceptionPage();
app.UseCors("AllowSpecificOrigins");
app.MapControllers();
app.Run();