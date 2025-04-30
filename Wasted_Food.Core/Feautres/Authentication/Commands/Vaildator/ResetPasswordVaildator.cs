using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Authentication.Commands.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Vaildator
{
    internal class ResetPasswordVaildator : AbstractValidator<ResetPasswordCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public ResetPasswordVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public ResetPasswordVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {


            RuleFor(x => x.Email).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.Password).
             NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
             NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);
            RuleFor(x => x.ConfirmPassword).
            Equal(x => x.Password).WithMessage(_stringLocalizer[SharedREsourceKeys.PasswordNotEquelConfrimPassword]);

        }
        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }


}
