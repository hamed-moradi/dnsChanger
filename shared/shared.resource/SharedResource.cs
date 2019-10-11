using System;
using System.Collections.Generic;
using System.Text;

namespace shared.resource {
    public class SharedResource {
        #region General
        public static string Ok { get { return nameof(Ok); } }
        public static string BadRequest { get { return nameof(BadRequest); } }
        public static string InternalServerError { get { return nameof(InternalServerError); } }
        public static string AuthenticationFailed { get { return nameof(AuthenticationFailed); } }
        public static string TokenNotFound { get { return nameof(TokenNotFound); } }
        public static string DeviceIdNotFound { get { return nameof(DeviceIdNotFound); } }
        public static string DeviceIsNotActive { get { return nameof(DeviceIsNotActive); } }
        public static string UserIsNotActive { get { return nameof(UserIsNotActive); } }
        public static string PhoneIsNotVerified { get { return nameof(PhoneIsNotVerified); } }
        public static string ConnectionError { get { return nameof(ConnectionError); } }
        public static string UnexpectedError { get { return nameof(UnexpectedError); } }
        public static string NothingFound { get { return nameof(NothingFound); } }
        public static string DefectiveEntry { get { return nameof(DefectiveEntry); } }
        public static string RetrieveLimit { get { return nameof(RetrieveLimit); } }
        public static string DangerousRequest { get { return nameof(DangerousRequest); } }
        public static string UnsupportedLanguage { get { return nameof(UnsupportedLanguage); } }
        public static string UnsupportedTimeZone { get { return nameof(UnsupportedTimeZone); } }
        public static string WrongPassword { get { return nameof(WrongPassword); } }
        public static string RequestForVerificationCodeFirst { get { return nameof(RequestForVerificationCodeFirst); } }
        public static string VerificationCodeHasBeenExpired { get { return nameof(VerificationCodeHasBeenExpired); } }
        public static string WrongVerificationCode { get { return nameof(WrongVerificationCode); } }
        public static string AccessDenied { get { return nameof(AccessDenied); } }
        #endregion

        #region User
        #region SignUp
        public static string DefectiveCellPhone { get { return nameof(DefectiveCellPhone); } }
        public static string DefectiveEmail { get { return nameof(DefectiveEmail); } }
        #endregion

        #region SignIn
        public static string InvalidSigninAttempt { get { return nameof(InvalidSigninAttempt); } }
        public static string GoToStepTwo { get { return nameof(GoToStepTwo); } }
        #endregion
        #endregion

        #region Business
        public static string InvalidPoint { get { return nameof(InvalidPoint); } }
        public static string BusinessNotFound { get { return nameof(BusinessNotFound); } }
        public static string BusinessIsNotActive { get { return nameof(BusinessIsNotActive); } }
        #endregion

        #region Product
        public static string ProductNotFound { get { return nameof(ProductNotFound); } }
        public static string ProductIsNotActive { get { return nameof(ProductIsNotActive); } }
        #endregion

        #region Comment
        public static string CommentNotFound { get { return nameof(CommentNotFound); } }
        public static string CommentIsNotActive { get { return nameof(CommentIsNotActive); } }
        #endregion
    }
}
