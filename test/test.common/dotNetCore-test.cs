using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using shared.utility._app;

namespace test.common {
    [TestClass]
    public class DotNetCoreTest {
        [TestMethod]
        [TestCategory("DotNetCore")]
        [TestCategory("AppSettings")]
        public void ReadAppSettings() {
            ////var sqlCon = AppSettings.SqlConnection;
            //var mongoCon = AppSettings.MongoConnection;
            ////Assert.IsTrue(sqlCon.Contains("Server"));
            //Assert.IsTrue(mongoCon.Contains("mongodb"));
        }
    }
}
