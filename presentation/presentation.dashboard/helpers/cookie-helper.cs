using domain.office;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace presentation.dashboard.helpers {
    public class AccountPrincipal: ClaimsPrincipal {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string LastSignedin { get; set; }
        public string IP { get; set; }
        public string Path { get; set; }
    }

    public class AccountCookieAuthenticationEvents: CookieAuthenticationEvents {
        private readonly IAdmin_Container _adminRepository;

        public AccountCookieAuthenticationEvents(IAdmin_Container adminRepository) {
            _adminRepository = adminRepository;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context) {
            var accountPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var lastChanged = (from c in accountPrincipal.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();

            if(string.IsNullOrEmpty(lastChanged) || !_adminRepository.ValidateLastChangedAsync(lastChanged)) {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }

    //public class CookieHelper {
    //    #region Constructor
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    public CookieHelper(IHttpContextAccessor httpContextAccessor) {
    //        _httpContextAccessor = httpContextAccessor;
    //    }
    //    #endregion
    //    public class SearchAttribute: Attribute, IMetadataAware {
    //        public SearchAttribute(GeneralEnums.SearchFieldType type = GeneralEnums.SearchFieldType.String) {
    //            Type = type;
    //        }

    //        public GeneralEnums.SearchFieldType Type { get; }

    //        public void OnMetadataCreated(ModelMetadata metadata) {
    //            metadata.AdditionalValues["Type"] = Type;
    //        }
    //    }
    //    public static void Set(string path = null) {
    //        var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
    //        if(authCookie == null) return;
    //        var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
    //        if(string.IsNullOrWhiteSpace(authTicket.UserData)) return;
    //        var serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
    //        var newUser = new AccountPrincipal(authTicket.Name) {
    //            Id = serializeModel.Id,
    //            FullName = serializeModel.FullName,
    //            Avatar = serializeModel.Avatar,
    //            LastLogin = serializeModel.LastLogin,
    //            IP = serializeModel.IP,
    //            Path = path
    //        };
    //        HttpContext.Current.User = newUser;
    //    }
    //}
}
