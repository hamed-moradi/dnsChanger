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
    public class BusinessController: BaseController {
        #region Constructor
        private readonly IBusiness_Service _businessService;
        public BusinessController(IBusiness_Service businessService) {
            _businessService = businessService;
        }
        #endregion

        [ArgumentBinding, HttpGet, Route("getByLocation")]
        public async Task<IActionResult> GetByLocation([FromQuery]Business_GetByLocation_BindingModel collection) {
            if(collection.Latitude<=0 || collection.Longitude <= 0) {
                return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
            }
            try {
                var model = _mapper.Map<Business_GetByLocation_Schema>(collection);
                var result = await _businessService.GetByLocationAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
                    case 200:
                        return Ok(data: _mapper.Map<List<Business_ViewModel>>(result));
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                ///Log.Information(MethodBase.GetCurrentMethod().Name);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpGet, Route("getPaging")]
        public async Task<IActionResult> GetPaging([FromQuery]Business_GetPaging_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Business_GetPaging_Schema>(collection);
                var result = await _businessService.GetPagingAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
                    case 200:
                        var viewModel = _mapper.Map<List<Business_ViewModel>>(result);
                        return Ok(data: viewModel, totalPages: model.TotalPages);
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                ///Log.Information(MethodBase.GetCurrentMethod().Name);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpGet, Route("create")]
        public async Task<IActionResult> Create([FromQuery]Business_New_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Business_New_Schema>(collection);
                var result = await _businessService.CreateAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
                    case 200:
                        return Ok(data: _mapper.Map<Business_ViewModel>(result));
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                ///Log.Information(MethodBase.GetCurrentMethod().Name);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpGet, Route("edit")]
        public async Task<IActionResult> Edit([FromQuery]Business_Edit_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Business_Edit_Schema>(collection);
                var result = await _businessService.EditAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
                    case 415:
                        return BadRequest(_stringLocalizer[SharedResource.BusinessNotFound]);
                    case 420:
                        return BadRequest(_stringLocalizer[SharedResource.BusinessIsNotActive]);
                    case 200:
                        return Ok(data: _mapper.Map<Business_ViewModel>(result));
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                ///Log.Information(MethodBase.GetCurrentMethod().Name);
            }
            return InternalServerError();
        }
    }
}
