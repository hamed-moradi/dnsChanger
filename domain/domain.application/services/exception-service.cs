using domain.application._app;
using domain.repository.schemas;
using domain.application;
using Newtonsoft.Json;
using shared.utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace domain.application.services {
    public class ExceptionService: IException_Service {
        #region Constructor
        private readonly IStoreProcedure<IBase_Model, Exception_Insert_Schema> _matchPredict;
        public ExceptionService(IStoreProcedure<IBase_Model, Exception_Insert_Schema> matchPredict) {
            _matchPredict = matchPredict;
        }
        #endregion

        public async Task InsertAsync(Exception model, string url, string ip) {
            var schema = new Exception_Insert_Schema {
                URL = url,
                Data = JsonConvert.SerializeObject(model.Data.Keys),
                IP = ip,
                Message = model.Message,
                Source = model.Source,
                StackTrace = model.StackTrace,
                TargetSite = model.TargetSite.ToString()
            };
            await _matchPredict.ExecuteReturnLessAsync(schema);
        }
    }
}
