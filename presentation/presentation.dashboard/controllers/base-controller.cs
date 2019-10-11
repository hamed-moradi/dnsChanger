using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using shared.utility._app;

namespace presentation.dashboard.controllers {
    public class BaseController<T>: Controller { }
    public class BaseController: Controller {
        #region Constructor
        protected readonly IMapper _mapper;
        protected readonly IStringLocalizer<BaseController> _stringLocalizer;
        protected string IP { get { return HttpContext.Connection.RemoteIpAddress.ToString(); } }
        protected string URL { get { return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}"; } }
        protected IList<ImageFormat> ImageFormats { get { return new List<ImageFormat> { ImageFormat.Gif, ImageFormat.Jpeg, ImageFormat.Tiff, ImageFormat.Png }; } }
        //protected string[] ImageExtensions { get { return new string[] { ".jpg", ".jpeg", ".png", ".tif", ".bmp", ".gif" }; } }
        public BaseController() {
            _mapper = ServiceLocator.Current.GetInstance<IMapper>();
            _stringLocalizer = ServiceLocator.Current.GetInstance<IStringLocalizer<BaseController>>();
        }
        #endregion
    }
}