using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Authentication.Querys.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Authentication.Querys.Validator
{
    public class ConfirmEmailVaildator : AbstractValidator<ConfirmEmailQuery>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public ConfirmEmailVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public ConfirmEmailVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {


            RuleFor(x => x.userId).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.code).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);


        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
