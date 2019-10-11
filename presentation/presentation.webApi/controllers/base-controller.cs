using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using domain.application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using presentation.webApi.filterAttributes;
using shared.model.bindingModels;
using shared.model.viewModels;
using shared.resource;
using shared.utility;
using shared.utility._app;

namespace presentation.webApi.controllers {
    [SecurityFilter, Route("[controller]")]
    public class BaseController: Controller {
        #region Constructor
        protected readonly IMapper _mapper;
        protected readonly IException_Service _exceptionService;
        protected readonly IStringLocalizer<BaseController> _stringLocalizer;
        protected string IP { get { return HttpContext.Connection.RemoteIpAddress.ToString(); } }
        protected string URL { get { return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}"; } }
        protected IList<ImageFormat> ImageFormats { get { return new List<ImageFormat> { ImageFormat.Gif, ImageFormat.Jpeg, ImageFormat.Tiff, ImageFormat.Png }; } }
        protected CultureInfo[] Languages { get { return CultureInfo.GetCultures(CultureTypes.AllCultures); } }
        protected IReadOnlyCollection<TimeZoneInfo> TimeZones { get { return TimeZoneInfo.GetSystemTimeZones(); } }
        //protected string[] ImageExtensions { get { return new string[] { ".jpg", ".jpeg", ".png", ".tif", ".bmp", ".gif" }; } }
        //protected readonly string[] Languages = new string[] { "en", "en-US", "fa" };
        //protected readonly IReadOnlyCollection<TimeZoneInfo> TimeZones = TimeZoneInfo.GetSystemTimeZones();

        public BaseController() {
            _mapper = ServiceLocator.Current.GetInstance<IMapper>();
            _exceptionService = ServiceLocator.Current.GetInstance<IException_Service>();
            _stringLocalizer = ServiceLocator.Current.GetInstance<IStringLocalizer<BaseController>>();
        }
        #endregion

        public void HeaderValidator<T>(T model) where T : FullHeader_BindingModel {
            if(!Languages.Contains(CultureInfo.GetCultureInfoByIetfLanguageTag(model.Language))) {
                model.Language = new CultureInfo("en-US").IetfLanguageTag;
                //BadRequest(_stringLocalizer[SharedResource.UnsupportedLanguage]);
            }
            if(!TimeZones.Contains(TimeZoneInfo.FromSerializedString(model.TimeZone))) {
                model.TimeZone = TimeZoneInfo.Utc.ToSerializedString();
                //BadRequest(_stringLocalizer[SharedResource.UnsupportedTimeZone]);
            }
            if(!string.IsNullOrWhiteSpace(model.Token)) {
                BadRequest(_stringLocalizer[SharedResource.TokenNotFound]);
            }
            if(!string.IsNullOrWhiteSpace(model.DeviceId)) {
                BadRequest(_stringLocalizer[SharedResource.DeviceIdNotFound]);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Ok(HttpStatusCode status = HttpStatusCode.OK, string message = null, object data = null, int? totalPages = null) {
            message = message ?? _stringLocalizer[SharedResource.Ok];
            return Json(new Base_ViewModel { Status = status, Message = message, Data = data, TotalPages = totalPages });
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult BadRequest(string message = null) {
            message = message ?? _stringLocalizer[SharedResource.BadRequest];
            return Json(new Base_ViewModel { Status = HttpStatusCode.BadRequest, Message = message });
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult InternalServerError(string message = null) {
            message = message ?? _stringLocalizer[SharedResource.InternalServerError];
            return Json(new Base_ViewModel { Status = HttpStatusCode.InternalServerError, Message = message });
        }
    }
}