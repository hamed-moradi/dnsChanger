using domain.application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shared.utility._app;
using domain.repository.collections;
using System;

namespace test.common.units {
    [TestClass]
    public class HttpLogTest {
        #region Constructor
        private readonly IHttpLog_Service _httpLogService;
        public HttpLogTest() {
            _httpLogService = ServiceLocator.Current.GetInstance<IHttpLog_Service>();
        }
        #endregion
        [TestMethod]
        [TestCategory("HttpLog")]
        [TestCategory("Insert")]
        public void Insert() {
            var result = _httpLogService.InsertAsync(new HttpLog { Duration = 1, Method = "1", ReqestedAt = DateTime.Now, ResponsedAt = DateTime.Now }, 10000).Result;
            Assert.IsTrue(result > 0);
        }
    }
}