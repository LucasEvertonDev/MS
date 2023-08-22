﻿using FluentValidation;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Services.Products.Core.Domain.Models.Users;
using MS.Services.Products.Infra.Plugins.FluentValidation.CustomValidators;

namespace MS.Services.Products.Infra.Plugins.FluentValidation.User;

public class CreateUserValidator : BaseValidator<CreateUserModel>, IValidatorModel<CreateUserModel>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Username).SetValidator(new UserNameValidator());
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email é obrigatorio");
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email inválido").WithErrorCode("Teste de erro");
        });
    }
}