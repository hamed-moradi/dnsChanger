using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace presentation.dashboard.extensions {
    public static class UrlExtensions {
        public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl) {
            if(!urlHelper.IsLocalUrl(localUrl)) {
                return urlHelper.Page("/Get");
            }

            return localUrl;
        }
    }
}
