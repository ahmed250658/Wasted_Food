namespace Wasted_Food.Data.AppMetaData
{
    public static class Router
    {
        public const string root = "Api";
        public const string version = "V1";
        public const string Rule = root + "/" + version + "/";


        public static class AppUserRouting
        {
            public const string perfix = Rule + "User";
            public const string Create = perfix + "/Create";
            public const string Paginated = perfix + "/Paginated";
            public const string GetById = perfix + "/{id}";
            public const string Edit = perfix + "/Edit";
            public const string Delete = perfix + "/Delete/{id}";
            public const string ChangePassword = perfix + "/Change-Password";
            public const string publicOrganization = perfix + "/public-Organization";
            public const string CharityOrganization = perfix + "/Charity-Organization";
        }
        public static class Authentication
        {
            public const string perfix = Rule + "Authentication";
            public const string SignIn = perfix + "/SignIn";
            public const string RefreshToken = perfix + "/Refresh-Token";
            public const string VaildateToken = perfix + "/Vaildate-Token";
            public const string sendResetPassword = perfix + "/sendReset-Password";
            public const string ConfirmResetPassword = perfix + "/ConfirmReset-Password";
            public const string ResetPassword = perfix + "/Reset-Password";
            public const string ConfirmEmail = "/Api/Authentication/Confirm-Email";

        }
        public static class Authorization
        {
            public const string perfix = Rule + "Authorization";
            public const string Create = perfix + "/Role/Create";
        }
        public static class Email
        {
            public const string perfix = Rule + "Authorization";
            public const string SendEmail = perfix + "/SendEmail";
        }
        public static class Donation
        {
            public const string perfix = Rule + "Donation";
            public const string Create = perfix + "/Create";
            public const string GetListDonation = perfix + "/List";
            public const string Delete = perfix + "/Delete";
        }
        public static class FoodRequest
        {
            public const string perfix = Rule + "Requst";
            public const string Create = perfix + "/Create";
            public const string GetListRequest = perfix + "/publicList";
            public const string GetListCart = perfix + "/CartList";
            public const string GetListAccept = perfix + "/AcceptList";
            public const string AcceptedRequestCommand = perfix + "/Accept-Response";
            public const string RejectedRequestCommand = perfix + "/Reject-Response";
            public const string CancleRequestCommand = perfix + "/Cancel-Response";
            public const string GetById = perfix + "/{id}";
            public const string Delete = perfix + "/Delete";
        }

    }

}
