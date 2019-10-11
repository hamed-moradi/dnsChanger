using domain.office;
using domain.repository.entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using shared.utility._app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

namespace presentation.dashboard.helpers {
    public static class AccountHelper {
        #region Constructor
        private static readonly IHttpContextAccessor _httpContextAccessor;
        private static readonly IModuleReference_Container _moduleReferenceContainer;

        static AccountHelper() {
            _httpContextAccessor = ServiceLocator.Current.GetInstance<IHttpContextAccessor>();
            _moduleReferenceContainer = ServiceLocator.Current.GetInstance<IModuleReference_Container>();
        }
        #endregion

        #region Private
        private static bool CheckAccess(string path) {
            return ModuleReferences().Any(a => a.Path.ToLower().Equals(path.ToLower()));
        }
        public static int AdminId {
            get {
                if(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) {
                    if(_httpContextAccessor.HttpContext.User is AccountPrincipal accountPrincipal) {
                        return accountPrincipal.Id;
                    }
                }
                return 0;
            }
        }

        [ResponseCache(Duration = 10)]
        public static List<ModuleReference_Entity> ModuleReferences() => _moduleReferenceContainer.GetByAdminIdAsync(AdminId).Result;
        #endregion

        public static bool HasAccess(string path) {
            return CheckAccess(path);
        }
        public static bool HasAccess(string action, string controller) {
            return CheckAccess($"/{controller}/{action}");
        }

        public static bool CheckPermission(IList<ModuleReference_Entity> modules, string controller, string action, string method, int? moduleId = null, string path = "") {
            try {
                if(AdminId > 0) {
                    foreach(var item in modules) {
                        if(item.HttpMethod.ToLower().Contains(method))
                            if(moduleId != null) {
                                if(item.Id == moduleId)
                                    return true;
                            }
                            else if(!string.IsNullOrWhiteSpace(path)) {
                                if(item.Path.ToLower().Equals(path.ToLower()))
                                    return true;
                            }
                            else {
                                if(item.Path.ToLower() == $@"/{controller}/{action}")
                                    return true;
                            }
                    }
                    return false;
                }
                return false;
            }
            catch(Exception ex) {
                Log.Error(ex, "");
                return false;
            }
        }
    }

    //public static class UserCacheExtensions {
    //    private const string CookieName = "AdminCache";
    //    public static void AdminCache(this HttpResponseBase response, AdminCacheModel info) {
    //        var json = JsonConvert.SerializeObject(info);
    //        json = HttpUtility.UrlEncode(json);
    //        var cookie = new HttpCookie(CookieName, json) { Expires = DateTime.UtcNow.AddDays(60), };
    //        response.SetCookie(cookie);
    //    }
    //    public static AdminCacheModel AdminCache(this HttpRequestBase request) {
    //        var json = "{}";
    //        var cookie = request.Cookies.Get(CookieName);
    //        if(cookie != null)
    //            json = cookie.Value ?? json;
    //        json = HttpUtility.UrlDecode(json);
    //        var userCache = JsonConvert.DeserializeObject<AdminCacheModel>(json);
    //        return userCache;
    //    }
    //}
    //public class CustomPrincipalSerializeModel {
    //    public int Id { get; set; }
    //    public string FullName { get; set; }
    //    public string Avatar { get; set; }
    //    public string LastLogin { get; set; }
    //    public string IP { get; set; }
    //    public string Path { get; set; }
    //}
}
