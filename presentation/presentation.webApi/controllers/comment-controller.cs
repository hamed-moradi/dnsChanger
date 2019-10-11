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
using shared.resource;

namespace presentation.webApi.controllers {
    public class CommentController: BaseController {
        #region Constructor
        private readonly IComment_Service _commentService;
        public CommentController(IComment_Service commentService) {
            _commentService = commentService;
        }
        #endregion

        [ArgumentBinding, HttpGet, Route("getPaging")]
        public async Task<IActionResult> GetPaging([FromQuery]Comment_GetPaging_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Comment_GetPaging_Schema>(collection);
                var result = await _commentService.GetPagingAsync(model);
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
                        var viewModel = _mapper.Map<List<Comment_ViewModel>>(result);
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
        public async Task<IActionResult> Create([FromQuery]Comment_New_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Comment_New_Schema>(collection);
                var result = await _commentService.CreateAsync(model);
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
                        return Ok(data: _mapper.Map<Comment_ViewModel>(result));
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
        public async Task<IActionResult> Edit([FromQuery]Comment_Edit_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Comment_Edit_Schema>(collection);
                var result = await _commentService.EditAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 415:
                        return BadRequest(_stringLocalizer[SharedResource.CommentNotFound]);
                    case 420:
                        return BadRequest(_stringLocalizer[SharedResource.CommentIsNotActive]);
                    case 425:
                        return BadRequest(_stringLocalizer[SharedResource.AccessDenied]);
                    case 200:
                        return Ok(data: _mapper.Map<Comment_ViewModel>(result));
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
