using FluentValidation;
using MS.Libs.Core.Domain.DbContexts.Repositorys;
using MS.Libs.Core.Domain.Plugins.Validators;
using MS.Libs.Infra.Plugins.Validators;
using MS.Services.Auth.Core.Domain.Contansts;
using MS.Services.Auth.Core.Domain.DbContexts.Entities;
using MS.Services.Auth.Core.Domain.Models.Users;
using MS.Services.Auth.Plugins.FluentValidation.Extensions;

namespace MS.Services.Auth.Plugins.FluentValidation.User;

public class UpdateUserValidator : BaseValidator<UpdateUserModel>, IValidatorModel<UpdateUserModel>
{
    public UpdateUserValidator(ISearchRepository<UserGroup> searchUserGroupRepository)
    {
        RuleFor(c => c.Username).NotNullOrEmpty().WithError(UserErrors.Validators.USERNAME_REQUIRED);
        RuleFor(c => c.Email).NotNullOrEmpty().WithError(UserErrors.Validators.EMAIL_REQUIRED);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithError(UserErrors.Validators.EMAIL_INVALID);
        });

        RuleFor(c => c.UserGroupId).NotNullOrEmpty().WithError(UserErrors.Validators.USER_GROUP_REQUIRED);

        RuleFor(x => x.UserGroupId).MustAsync(async (userGroup, cancelation) =>
        {
            if (!string.IsNullOrEmpty(userGroup))
            {
                return !((await searchUserGroupRepository.FirstOrDefaultAsync(u => u.Id.ToString() == userGroup)) == null);
            }
            return true;
        }).WithError(UserErrors.Validators.USER_GROUP_NOT_FOUND);
    }
}
