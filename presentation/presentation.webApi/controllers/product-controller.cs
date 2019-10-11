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
    public class ProductController: BaseController {
        #region Constructor
        private readonly IProduct_Service _productService;
        public ProductController(IProduct_Service productService) {
            _productService = productService;
        }
        #endregion

        [ArgumentBinding, HttpGet, Route("get")]
        public async Task<IActionResult> Get([FromQuery]Product_Get_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Product_Get_Schema>(collection);
                var result = await _productService.GetAsync(model);
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
                        return Ok(data: _mapper.Map<Product_ViewModel>(result));
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

        [ArgumentBinding, HttpGet, Route("getByLocation")]
        public async Task<IActionResult> GetByLocation([FromQuery]Product_GetByLocation_BindingModel collection) {
            if(collection.Latitude <= 0 || collection.Longitude <= 0) {
                return BadRequest(_stringLocalizer[SharedResource.InvalidPoint]);
            }
            try {
                var model = _mapper.Map<Product_GetByLocation_Schema>(collection);
                var result = await _productService.GetByLocationAsync(model);
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
                        return Ok(data: _mapper.Map<List<Product_ViewModel>>(result));
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
        public async Task<IActionResult> GetPaging([FromQuery]Product_GetPaging_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Product_GetPaging_Schema>(collection);
                var result = await _productService.GetPagingAsync(model);
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
                        var viewModel = _mapper.Map<List<Product_ViewModel>>(result);
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
        public async Task<IActionResult> Create([FromQuery]Product_New_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Product_New_Schema>(collection);
                var result = await _productService.CreateAsync(model);
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
                        return Ok(data: _mapper.Map<Product_ViewModel>(result));
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
        public async Task<IActionResult> Edit([FromQuery]Product_Edit_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Product_Edit_Schema>(collection);
                var result = await _productService.EditAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 415:
                        return BadRequest(_stringLocalizer[SharedResource.ProductNotFound]);
                    case 420:
                        return BadRequest(_stringLocalizer[SharedResource.ProductIsNotActive]);
                    case 200:
                        return Ok(data: _mapper.Map<Product_ViewModel>(result));
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
