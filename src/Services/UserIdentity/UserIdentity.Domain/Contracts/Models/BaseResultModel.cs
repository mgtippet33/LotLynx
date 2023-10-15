using FluentValidation.Results;

namespace UserIdentity.Domain.Contracts.Models;

public class BaseResultModel<TData>
{
    public BaseResultModel(TData data, ValidationResult validationResult)
    {
        Data = data;
        ValidationResult = validationResult;
    }
    
    public TData Data { get; private set; }
    
    public ValidationResult ValidationResult { get; private set; }

    public bool IsValid => ValidationResult.IsValid;
}