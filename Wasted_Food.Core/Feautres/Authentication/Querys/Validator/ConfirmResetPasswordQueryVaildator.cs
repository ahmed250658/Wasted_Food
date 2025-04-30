using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Authentication.Querys.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Vaildator
{
    public class ConfirmResetPasswordQueryVaildator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public ConfirmResetPasswordQueryVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public ConfirmResetPasswordQueryVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {


            RuleFor(x => x.Code).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);
            RuleFor(x => x.Email).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

        }
        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
