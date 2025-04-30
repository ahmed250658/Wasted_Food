using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Authentication.Commands.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Authentication.Commands.Vaildator
{
    public class SignInVaildator : AbstractValidator<SignInCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public SignInVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public SignInVaildator()
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


        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }

}
