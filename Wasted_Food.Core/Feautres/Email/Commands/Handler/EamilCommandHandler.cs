using MediatR;
using Microsoft.Extensions.Localization;
using Wasted_Food.Core.Bases;
using Wasted_Food.Core.Feautres.Email.Commands.Models;
using Wasted_Food.Core.Shared;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Core.Feautres.Email.Commands.Handler
{
    public class EamilCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;
        private readonly IEmailService _emailService;
        #endregion
        #region Constructor
        public EamilCommandHandler(IStringLocalizer<SharedResource> stringLocalizer, IEmailService emailService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
        }
        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.Email, request.Message, null);
            if (result == "Success")
                return Success<string>("");
            return BadRequest<string>(_stringLocalizer[SharedREsourceKeys.FailedToSendEmail]);
        }
        #endregion

    }
}
