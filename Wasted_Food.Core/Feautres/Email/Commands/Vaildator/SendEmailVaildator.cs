using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Email.Commands.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Email.Commands.Vaildator
{
    public class SendEmailVaildator : AbstractValidator<SendEmailCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public SendEmailVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public SendEmailVaildator()
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

            RuleFor(x => x.Message).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);


        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
