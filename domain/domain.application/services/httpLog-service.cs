using domain.repository._app;
using domain.repository.collections;
using shared.utility;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace domain.application.services {
    public class HttpLogService: IHttpLog_Service {
        #region Constructor
        private readonly MongoDBContext _mongoDBContext;
        private readonly IException_Service _exceptionService;

        public HttpLogService(MongoDBContext mongoDBContext, IException_Service exceptionService) {
            _mongoDBContext = mongoDBContext;
            _exceptionService = exceptionService;
        }
        #endregion

        public async Task<int> InsertAsync(HttpLog model, int timeoutMS) {
            var startTime = DateTime.UtcNow;
            try {
                using(var timeoutCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(timeoutMS))) {
                    await _mongoDBContext.HttpLogs.InsertOneAsync(model, cancellationToken: timeoutCancellationTokenSource.Token);
                }
                return 1;
            }
            catch(OperationCanceledException ex) {
                await _exceptionService.InsertAsync(ex, MethodBase.GetCurrentMethod().Name, GeneralVariables.LocalIP);
                var elapsed = DateTime.UtcNow - startTime;
                return 0;
            }
        }
    }
}
