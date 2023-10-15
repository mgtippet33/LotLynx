using FluentValidation.Results;

namespace UserIdentity.Domain.Contracts.Models;

public class SignInUserResultModel : BaseResultModel<SignInUserModel>
{
    public SignInUserResultModel(SignInUserModel model, ValidationResult validationResult) : base(model,
        validationResult)
    {
    }
}