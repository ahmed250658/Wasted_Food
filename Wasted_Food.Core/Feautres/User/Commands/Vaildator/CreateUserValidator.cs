using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Features.User.Commands.Validator
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public CreateUserValidator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public CreateUserValidator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {
            RuleFor(x => x.OrganizationName).
                NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
                NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
                MaximumLength(100).WithMessage(_stringLocalizer[SharedREsourceKeys.MaxLengthis100]);

            RuleFor(x => x.Email).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.Password).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);
            RuleFor(x => x.ConfrimPassword).
              Equal(x => x.Password).WithMessage(_stringLocalizer[SharedREsourceKeys.PasswordNotEquelConfrimPassword]);

        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
