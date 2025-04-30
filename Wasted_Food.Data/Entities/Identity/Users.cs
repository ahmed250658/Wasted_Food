using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using Wasted_Food.Data.Enum;

namespace Wasted_Food.Data.Entities.Identity
{
    public class Users : IdentityUser<int>
    {
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
        public TypeOfOrganization? TypeOfOrgn { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
    }
}
// SignUpApp.Core/UseCases/SignUp.cs
//namespace SignUpApp.Core.UseCases
//{
//    public class SignUp
//    {
//        private readonly IUserRepository _userRepository;

//        public SignUp(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<SignUpResult> Execute(SignUpRequest request)
//        {
//            if (request.Password != request.ConfirmPassword)
//            {
//                return new SignUpResult { Success = false, ErrorMessage = "Passwords do not match" };
//            }

//            if (await _userRepository.EmailExistsAsync(request.Email))
//            {
//                return new SignUpResult { Success = false, ErrorMessage = "Email already exists" };
//            }

//            var user = new User
//            {
//                OrganizationName = request.OrganizationName,
//                Email = request.Email,
//                Password = request.Password, // Note: In real app, hash this password
//                UserType = request.UserType,
//                AcceptsCommunication = request.AcceptsCommunication
//            };

//            await _userRepository.AddAsync(user);

//            return new SignUpResult { Success = true };
//        }
//    }

//    public class SignUpRequest
//    {
//        public string OrganizationName { get; set; }
//        public string Email { get; set; }
//        public string Password { get; set; }
//        public string ConfirmPassword { get; set; }
//        public UserType UserType { get; set; }
//        public bool AcceptsCommunication { get; set; }
//    }

//    public class SignUpResult
//    {
//        public bool Success { get; set; }
//        public string ErrorMessage { get; set; }
//    }
//}