using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.User.Commands.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.User.Commands.Vaildator
{
    public class EditUserVaildator : AbstractValidator<EditUserCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public EditUserVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public EditUserVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {
            RuleFor(x => x.FullName).
                NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
                NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
                MaximumLength(100).WithMessage(_stringLocalizer[SharedREsourceKeys.MaxLengthis100]);

            RuleFor(x => x.Email).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.CurrentPassword).
            NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
            NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.ChangePassword).
             NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
             NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);
            RuleFor(x => x.ConfirmPassword).
              Equal(x => x.ChangePassword).WithMessage(_stringLocalizer[SharedREsourceKeys.PasswordNotEquelConfrimPassword]);
        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
