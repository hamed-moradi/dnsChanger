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
using shared.utility.infrastructure;
using System.IO;
using shared.utility._app;
using System.Drawing;
using System.Drawing.Imaging;

namespace presentation.webApi.controllers {
    public class UserController: BaseController {
        #region Constructor
        private readonly IUser_Service _userService;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;
        public UserController(IUser_Service userService, IRandomGenerator randomGenerator, IEmailService emailService, ISMSService smsService) {
            _userService = userService;
            _randomGenerator = randomGenerator;
            _emailService = emailService;
            _smsService = smsService;
        }
        #endregion

        [ArgumentBinding, HttpPost, Route("signin")]
        public async Task<IActionResult> SignIn([FromBody]User_SignIn_BindingModel collection) {
            var model = _mapper.Map<User_SignIn_Schema>(collection);
            try {
                await _userService.SignInAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIdNotFound]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.RequestForVerificationCodeFirst]);
                    case 412:
                        return BadRequest(_stringLocalizer[SharedResource.VerificationCodeHasBeenExpired]);
                    case 413:
                        return BadRequest(_stringLocalizer[SharedResource.WrongVerificationCode]);
                    case 415:
                        return BadRequest(_stringLocalizer[SharedResource.WrongPassword]);
                    case 205:
                        return Ok(HttpStatusCode.PartialContent, _stringLocalizer[SharedResource.GoToStepTwo]);
                    case 200:
                        return Ok(data: model.Token);
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                Log.Information($"DeviceId: '{model.DeviceId}' tried to signing in, result: '{model.StatusCode}'.");
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPost, Route("signup")]
        public async Task<IActionResult> SignUp([FromBody]User_SignUp_BindingModel collection) {
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

        [ArgumentBinding, HttpPost, Route("sendverificationcode")]
        public async Task<IActionResult> SendVerificationCode([FromBody]User_Verify_BindingModel collection) {
            if(string.IsNullOrWhiteSpace(collection.DeviceId)) {
                return BadRequest(_stringLocalizer[SharedResource.DeviceIdNotFound]);
            }
            try {
                var model = _mapper.Map<User_SetVerificationCode_Schema>(collection);
                model.VerificationCode = _randomGenerator.Create("****");
                await _userService.SetVerificationCodeAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIdNotFound]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 416:
                        return BadRequest(_stringLocalizer["u must register a cell phone first"]);
                    case 417:
                        return BadRequest(_stringLocalizer["CellPhone does not match"]);
                    case 418:
                        return BadRequest(_stringLocalizer["u must register an email first"]);
                    case 419:
                        return BadRequest(_stringLocalizer["Email does not match"]);
                    case 200: {
                        if(!string.IsNullOrWhiteSpace(model.CellPhone)) {
                            // Send SMS
                            _smsService.Send(model.CellPhone, $"{_stringLocalizer["This is your verification code"]} {model.VerificationCode}");
                        }
                        if(!string.IsNullOrWhiteSpace(model.Email)) {
                            // Send Email
                            _emailService.Send(model.CellPhone, _stringLocalizer["VerificationCode"],
                                $"{_stringLocalizer["This is your verification code"]} {model.VerificationCode}");
                        }
                        return Ok();
                    }
                }
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPost, Route("verify")]
        public async Task<IActionResult> Verify([FromBody]User_Verify_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<User_Verify_Schema>(collection);
                await _userService.VerifyAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 416:
                        return BadRequest(_stringLocalizer["u must register a cell phone first"]);
                    case 417:
                        return BadRequest(_stringLocalizer["CellPhone does not match"]);
                    case 418:
                        return BadRequest(_stringLocalizer["u must register an email first"]);
                    case 419:
                        return BadRequest(_stringLocalizer["Email does not match"]);
                    case 205:
                        return Ok(_stringLocalizer["u'r cell phone is already active"]);
                    case 210:
                        return Ok(_stringLocalizer["u'r email is already active"]);
                    case 200:
                        return Ok();
                }
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpGet, Route("get")]
        public async Task<IActionResult> Get([FromQuery]FullHeader_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<Void_Schema>(collection);
                //model.EntityName = "[user]";
                var result = await _userService.GetAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 200:
                        return Ok(data: _mapper.Map<User_SignUp_ViewModel>(result));
                }
            }
            catch(Exception ex) {
                Log.Error(ex, MethodBase.GetCurrentMethod().Name);
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPost, Route("enabletwofactorauthentication")]
        public async Task<IActionResult> EnableTwoFactorAuthentication([FromBody]User_TwoFactorAuthentication_BindingModel collection) {
            HeaderValidator(collection);
            if(collection.Password.Length < 6) {
                return BadRequest(_stringLocalizer["your password doesn't meet the legal length"]);
            }
            try {
                var model = _mapper.Map<User_EnableTwoFactorAuthentication_Schema>(collection);
                await _userService.EnableTwoFactorAuthentication(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 411:
                        return BadRequest(_stringLocalizer[SharedResource.RequestForVerificationCodeFirst]);
                    case 412:
                        return BadRequest(_stringLocalizer[SharedResource.VerificationCodeHasBeenExpired]);
                    case 413:
                        return BadRequest(_stringLocalizer[SharedResource.WrongVerificationCode]);
                    case 200:
                        return Ok();
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPost, Route("disabletwofactorauthentication")]
        public async Task<IActionResult> DisableTwoFactorAuthentication([FromBody]User_TwoFactorAuthentication_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<User_DisableTwoFactorAuthentication_Schema>(collection);
                await _userService.DisableTwoFactorAuthentication(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.TokenNotFound]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIdNotFound]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 415:
                        return BadRequest(_stringLocalizer[SharedResource.WrongPassword]);
                    case 200:
                        return Ok();
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPut, Route("edit")]
        public async Task<IActionResult> Edit([FromBody]User_Update_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var fileName = string.Empty;
                var model = _mapper.Map<Void_Schema>(collection);
                var user = await _userService.GetAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                }
                if(collection.Avatar != null) {
                    using(var memoryStream = new MemoryStream()) {
                        await collection.Avatar.CopyToAsync(memoryStream);
                        if(memoryStream.ToArray().Length == 0) {
                            return BadRequest(_stringLocalizer["FileHasNoContent"]);
                        }
                        if(memoryStream.ToArray().Length > int.Parse(AppSettings.AvatarSize) * 1024) {
                            return BadRequest(_stringLocalizer["FileSizeIsTooMuch"]);
                        }
                        var image = new Bitmap(memoryStream);
                        if(!ImageFormats.Contains(image.RawFormat)) {
                            return BadRequest(_stringLocalizer["WrongFileType"]);
                        }
                        var avatarResolution = AppSettings.AvatarResolution.Split('x');
                        if(image.Width * image.Height > int.Parse(avatarResolution[0]) * int.Parse(avatarResolution[1])) {
                            return BadRequest(_stringLocalizer["FileResolutionIsTooMuch"]);
                        }
                        if(!Directory.Exists(AppSettings.FilePath))
                            Directory.CreateDirectory(AppSettings.FilePath);
                        if(string.IsNullOrWhiteSpace(user.Avatar)) {
                            fileName = $"{user.Username}_001";
                        }
                        else {
                            var avatarArray = user.Username.Split('_');
                            var avatarNo = int.Parse(avatarArray[1]);
                            fileName = (++avatarNo).ToString();
                            for(var index = 0; index < (3 - avatarNo.ToString().Length); index++) {
                                fileName = "0" + fileName;
                            }
                            fileName = $"{user.Username}_{fileName}";
                        }
                        image.Save($@"{AppSettings.FilePath}\{fileName}.jpeg", ImageFormat.Jpeg);
                    }
                }
                var editModel = new User_Update_Schema {
                    Token = collection.Token,
                    DeviceId = collection.DeviceId,
                    Avatar = $"{fileName}.jpeg",
                    Nickname = collection.Nickname,
                    BirthDate = collection.BirthDate.ToDateTime(null)
                };
                await _userService.UpdateAsync(editModel);
                switch(model.StatusCode) {
                    case 200:
                        return Ok();
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }

        [ArgumentBinding, HttpPut, Route("disableme")]
        public async Task<IActionResult> DisableMe([FromBody]User_DisableMe_BindingModel collection) {
            HeaderValidator(collection);
            try {
                var model = _mapper.Map<User_DisableMe_Schema>(collection);
                await _userService.DisableMeAsync(model);
                switch(model.StatusCode) {
                    case 400:
                        return BadRequest(_stringLocalizer[SharedResource.AuthenticationFailed]);
                    case 405:
                        return BadRequest(_stringLocalizer[SharedResource.DeviceIsNotActive]);
                    case 410:
                        return BadRequest(_stringLocalizer[SharedResource.UserIsNotActive]);
                    case 200:
                        return Ok();
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            return InternalServerError();
        }
    }
}
