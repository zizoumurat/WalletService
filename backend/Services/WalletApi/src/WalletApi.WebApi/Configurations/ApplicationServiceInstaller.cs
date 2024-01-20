using WalletApi.Application;
using MediatR;
using FluentValidation;
using WalletApi.Application.Behavior;

namespace WalletApi.WebApi.Configurations;

public class ApplicationServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(typeof(AssemblyReference).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), (typeof(ValidationBehavior<,>)));

        services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

        services.AddAutoMapper(typeof(AssemblyReference).Assembly);
    }
}
