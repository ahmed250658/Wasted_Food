using FluentValidation;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Feautres.FoodRequsts.Command.Models;
using Wasted_Food.Core.Shared;

namespace Wasted_Food.Core.Feautres.FoodRequsts.Command.Vaildator
{
    public class CreateFoodRequestVaildator : AbstractValidator<CreateFoodRequest>
    {
        #region fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        #endregion
        #region Constructor
        public CreateFoodRequestVaildator(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public CreateFoodRequestVaildator()
        {
            ApplyValidationRoles();
            ApplyCustomValidationRoles();
        }

        #endregion
        #region Handle Function

        public void ApplyValidationRoles()
        {
            //RuleFor(x => x.RequestedBy).
            // NotEmpty().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]).
            // NotNull().WithMessage(_stringLocalizer[SharedREsourceKeys.NotEmpty]);


        }

        public void ApplyCustomValidationRoles()
        {

        }
        #endregion
    }
}
