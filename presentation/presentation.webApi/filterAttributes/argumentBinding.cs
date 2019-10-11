using Microsoft.AspNetCore.Mvc.Filters;
using shared.model.bindingModels;
using System.Linq;

namespace presentation.webApi.filterAttributes {
    public class ArgumentBinding: ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext context) {
            foreach(var param in context.ActionArguments) {
                if(param.Value is IBase_BindingModel) {
                    var properties = param.Value.GetType().GetProperties();
                    foreach(var item in properties) {
                        if(!string.IsNullOrWhiteSpace(item.Name)) {
                            switch(item.Name.ToLower()) {
                                case "token": // GuidWithoutDash
                                    var token = context.HttpContext.Request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("token"));
                                    if(token.Value.Any())
                                        item.SetValue(param.Value, token.Value[0]);
                                    break;
                                case "deviceid":
                                    var deviceId = context.HttpContext.Request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("deviceid"));
                                    if(deviceId.Value.Any())
                                        item.SetValue(param.Value, deviceId.Value[0]);
                                    break;
                                case "language": // IetfLanguageTag
                                    var language = context.HttpContext.Request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("language"));
                                    if(language.Value.Any())
                                        item.SetValue(param.Value, language.Value[0]);
                                    break;
                                case "timezone": // TimeSpan
                                    var timeZone = context.HttpContext.Request.Headers.FirstOrDefault(f => f.Key.ToLower().Equals("timezone"));
                                    if(timeZone.Value.Any())
                                        item.SetValue(param.Value, timeZone.Value[0]);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
