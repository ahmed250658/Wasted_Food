using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.Donation.Command.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.Donation.Command.Vaildator
{
    public class CreateDonationVaildator : AbstractValidator<CreateDonationCommand>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public CreateDonationVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public CreateDonationVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {

            RuleFor(x => x.Name).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.Quantity).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.Location).
              NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
              NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.ExpiryData).
             NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
             NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

            RuleFor(x => x.Image).
            NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
            NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);

        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
