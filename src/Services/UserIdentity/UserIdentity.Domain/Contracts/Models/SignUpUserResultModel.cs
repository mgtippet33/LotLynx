using FluentValidation.Results;

namespace UserIdentity.Domain.Contracts.Models;

public class SignUpUserResultModel : BaseResultModel<SignUpUserModel>
{
    public SignUpUserResultModel(SignUpUserModel model, ValidationResult validationResult) : base(model,
        validationResult)
    {
    }
}