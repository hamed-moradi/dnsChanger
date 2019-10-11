using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using domain.application;
using domain.repository.schemas;
using Microsoft.AspNetCore.Mvc;
using presentation.webApi.filterAttributes;
using shared.model.bindingModels;
using shared.model.viewModels;
using Serilog;
using shared.resource;

namespace presentation.webApi.controllers {
    public class CustomerController: BaseController {
        #region Constructor
        private readonly IUser_Service _userService;
        private readonly ICustomer_Service _customerService;
        public CustomerController(IUser_Service userService,ICustomer_Service customerService) {
            _userService = userService;
            _customerService = customerService;
        }
        #endregion

        [ArgumentBinding, HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody]Customer_SignUp_BindingModel collection) {
            if(string.IsNullOrWhiteSpace(collection.CellPhone) && string.IsNullOrWhiteSpace(collection.Email)) {
                return BadRequest(_stringLocalizer[SharedResource.DefectiveEntry]);
            }
            try {
                var model = _mapper.Map<User_SignUp_Schema>(collection);
                var user = await _userService.SignUpAsync(model);
                switch(model.StatusCode) {
                    case 420:
                        return BadRequest(_stringLocalizer[SharedResource.DefectiveEntry]);
                    case 421:
                        return BadRequest(_stringLocalizer[SharedResource.DefectiveCellPhone]);
                    case 422:
                        return BadRequest(_stringLocalizer[SharedResource.DefectiveEmail]);
                    case 200:
                        return Ok(data: new { model.Token, user });
                }
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpGet, Route("get")]
        public async Task<IActionResult> Get([FromQuery]Customer_GetById_BindingModel collection) {
            try {
                var model = _mapper.Map<Void_Schema>(collection);
                var result = await _customerService.GetByIdAsync(model);
                switch(model.StatusCode) {
                    case 200:
                        return Ok(data: _mapper.Map<Customer_GetById_ViewModel>(result));
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    default:
                        return InternalServerError();
                }
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }
    }
}
