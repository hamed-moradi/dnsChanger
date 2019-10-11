using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using domain.office;
using domain.repository.entities;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using shared.model.dashboard_models;
using shared.model.viewModels;

namespace presentation.dashboard.controllers {
    public class UserController: BaseController {
        #region Constructor
        private readonly IUser_Container _userContainer;
        public UserController(IUser_Container userContainer) {
            _userContainer = userContainer;
        }
        #endregion

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> FindSingleAsync([FromRoute]int id) {
            try {
                var result = await _userContainer.FindSingleAsync(id);
                return View(_mapper.Map<User_DashboardModel>(result));
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FindAllAsync([FromRoute]User_ViewModel collection) {
            try {
                var model = _mapper.Map<User_Entity>(collection);
                var response = await _userContainer.GetPagingAsync(model);
                return View(response);
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]User_ViewModel collection) {
            try {
                var model = _mapper.Map<User_Entity>(collection);
                var response = await _userContainer.AddAsync(model);
                return View(response);
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]User_ViewModel collection) {
            try {
                var model = _mapper.Map<User_Entity>(collection);
                var response = await _userContainer.UpdateAsync(model);
                return View(response);
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAsync([FromBody]User_ViewModel collection) {
            try {
                var model = _mapper.Map<User_Entity>(collection);
                var response = await _userContainer.RemoveAsync(model);
                return View(response);
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
            }
            return View();
        }
    }
}
