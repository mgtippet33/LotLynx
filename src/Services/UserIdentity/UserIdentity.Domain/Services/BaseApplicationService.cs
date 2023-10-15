using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using UserIdentity.Domain.Infrastructure.Exceptions;

namespace UserIdentity.Domain.Services;

public class BaseApplicationService
{
    private readonly IServiceProvider _serviceProvider;

    public BaseApplicationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected async Task<ValidationResult> ValidateModelAsync<TModel>(TModel model)
    {
        IValidator<TModel>? validator = _serviceProvider.GetService<IValidator<TModel>>();
        
        return model != null && validator != null
            ? await validator.ValidateAsync(model)
            : throw new NotFoundValidatorException();
    }
}