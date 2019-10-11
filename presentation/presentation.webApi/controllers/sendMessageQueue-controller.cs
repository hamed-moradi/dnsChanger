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
using Serilog;
using shared.utility._app;
using shared.utility.infrastructure;

namespace presentation.webApi.controllers {
    public class SendMessageQueueController: BaseController {
        #region Constructor
        private readonly ISendMessageQueue_Service _sendMessageQueueService;
        private readonly IEmailService _emailService;
        private readonly ISMSService _smsService;
        public SendMessageQueueController(ISendMessageQueue_Service sendMessageQueueService, IEmailService emailService, ISMSService smsService) {
            _sendMessageQueueService = sendMessageQueueService;
            _emailService = emailService;
            _smsService = smsService;
        }
        #endregion

        [ArgumentBinding, HttpGet, Route("dequeue")]
        public async Task<IActionResult> Dequeue([FromQuery]SendMessageQueue_GetPaging_BindingModel collection) {
            if(collection.Token != AppSettings.SystemToken || collection.DeviceId != AppSettings.SystemDeviceId) {
                return BadRequest();
            }
            int totalSMS = 0, sentSMS = 0, totalEmail = 0, sentEmail = 0;
            var model = _mapper.Map<SendMessageQueue_GetPaging_Schema>(collection);
            try {
                var result = await _sendMessageQueueService.GetPagingAsync(model);
                switch(model.StatusCode) {
                    case 200:
                        totalSMS = result.Count(c => c.TypeId == 1);
                        totalEmail = result.Count(c => c.TypeId == 2);
                        foreach(var item in result) {
                            switch(item.TypeId) {
                                case 1: // CellPhone
                                    try {
                                        _smsService.Send(item.CellPhone, item.Body);
                                        sentSMS++;
                                    }
                                    catch(Exception ex) {
                                        Log.Error(ex, "Send SMS Problem!");
                                    }
                                    break;
                                case 2: // Email
                                    try {
                                        _emailService.Send(item.CellPhone, item.Subject, item.Body);
                                        sentEmail++;
                                    }
                                    catch(Exception ex) {
                                        Log.Error(ex, "Send Email Problem!");
                                    }
                                    break;
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) {
                await _exceptionService.InsertAsync(ex, URL, IP);
            }
            finally {
                Log.Information($"Method: '{MethodBase.GetCurrentMethod().Name}' called at {DateTime.Now}. \n\r" +
                    $"'{sentSMS}' SMSs sent from '{totalSMS}'. \n\r" +
                    $"'{sentEmail}' Emails sent from '{totalEmail}'.");
            }
            return InternalServerError();
        }
    }
}
