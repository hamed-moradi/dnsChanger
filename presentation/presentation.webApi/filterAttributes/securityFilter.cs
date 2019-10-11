using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using shared.model.bindingModels;
using shared.model.viewModels;
using Serilog;
using shared.resource;
using shared.resource._app;
using shared.utility;
using shared.utility._app;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace presentation.webApi.filterAttributes {
    public class SecurityFilter: ActionFilterAttribute {
        #region Constructor
        protected readonly IStringLocalizer<SecurityFilter> _stringLocalizer;
        private readonly string[] UnsafeKeywords = { "javascript", "vbscript", "shutdown", "exec", "having", "union", "select", "insert", "update", "delete", "drop", "truncate", "script" };

        public SecurityFilter() {
            _stringLocalizer = ServiceLocator.Current.GetInstance<IStringLocalizer<SecurityFilter>>();
        }
        #endregion

        #region Private
        private void KeywordChecker(ActionExecutingContext filterContext, string text) {
            if(UnsafeKeywords.Contains(text.ToLower())) {
                Log.Information(JsonConvert.SerializeObject(new {
                    Account = filterContext.HttpContext.User.Identity.Name,
                    IP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Method = filterContext.Controller.ToString(),
                    Keyword = text,
                    Message = InternalMessage.RestrictedKeywordDetection
                }));
                throw new Exception(_stringLocalizer[SharedResource.DangerousRequest], new Exception { Source = GeneralVariables.ExceptionSource });
            }
        }
        #endregion

        public override void OnActionExecuting(ActionExecutingContext context) {
            foreach(var param in context.ActionArguments) {
                if(param.Value is string && param.Value != null) {
                    //filterContext.ActionParameters[param.Key] = new KeyValuePair<string, object>(param.Key, param.Value.ToString().CharacterNormalizer());
                    KeywordChecker(context, param.Value.ToString());
                }
                if(param.Value is IBase_BindingModel) {
                    var properties = param.Value.GetType().GetProperties();
                    foreach(var item in properties) {
                        if(item.PropertyType == typeof(string) && item != null) {
                            //filterContext.ActionParameters[param.Key] = new KeyValuePair<string, object>(param.Key, param.Value.ToString().CharacterNormalizer());
                            KeywordChecker(context, item.ToString());
                        }
                    }
                }
            }
        }
    }
}
