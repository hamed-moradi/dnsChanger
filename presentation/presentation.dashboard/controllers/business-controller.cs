using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using domain.office;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using shared.model.dashboard_models;

namespace presentation.dashboard.controllers {
    public class BusinessController: BaseController {
        #region Constructor
        private readonly IBusiness_Container _businessContainer;
        public BusinessController(IBusiness_Container businessContainer) {
            _businessContainer = businessContainer;
        }
        #endregion

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> Get([FromQuery]int id) {
            try {
                var result = await _businessContainer.GetByIdAsync(id);
                return View(_mapper.Map<Business_DashboardModel>(result));
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }
    }
}
