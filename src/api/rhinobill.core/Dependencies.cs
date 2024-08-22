using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using rhinobill.core.Pipelines.Abstractions;
using rhinobill.core.Pipelines.Behaviours;

namespace rhinobill.core
{
    public static class Dependencies
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ICommand<>).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddValidatorsFromAssembly(typeof(ICommand<>).Assembly);
            return services;
        }
    }
}
