using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using shared.utility._app;
using System.IO;
using shared.utility.infrastructure;

namespace test.common {
    [TestClass]
    public class CommonTest {
        #region Constructor
        private readonly IRandomGenerator _randomGenerator;
        public CommonTest() {
            _randomGenerator = ServiceLocator.Current.GetInstance<IRandomGenerator>();
        }
        #endregion

        [TestMethod, TestCategory("Common"), TestCategory("Directory")]
        public void IODirectory() {
            //var d1 = Application.StartupPath;
            var d2 = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var d3 = AppDomain.CurrentDomain.BaseDirectory;
            var d4 = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            //var d5 = Path.GetDirectory(Application.ExecutablePath);
            var t = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var d = Directory.GetCurrentDirectory();
        }

        [TestMethod, TestCategory("Common"), TestCategory("SubStr")]
        public void SubStr() {
            var mongodbName = AppSettings.MongoConnection.Split('?')[0].Split('/')[3];
            Assert.IsTrue(mongodbName.Equals("PredictionEngine"));
        }

        [TestMethod, TestCategory("Common"), TestCategory("RandomGenerator")]
        public void RandomGenerator() {
            var number = _randomGenerator.Create("***");
            Assert.IsTrue(number >= 100 && number <= 999);
        }
    }
}
