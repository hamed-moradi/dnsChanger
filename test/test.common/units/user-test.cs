using domain.application;
using domain.repository.schemas;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shared.utility._app;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace test.common.units {
    [TestClass]
    public class UserTest {
        #region Constructor
        private readonly IUser_Service _userService;
        public UserTest() {
            _userService = ServiceLocator.Current.GetInstance<IUser_Service>();
        }
        #endregion

        [TestMethod, TestCategory("User"), TestCategory("SignUp")]
        public async Task SignIn() {
            try {
                var model = new User_SignUp_Schema { DeviceId = "", Name = "behzad", Family = "saemi", CellPhone = "911" };
                await _userService.SignUpAsync(model);
                Assert.IsTrue(model.StatusCode > 0);
                Console.WriteLine($"StatusCode: {model.StatusCode}");
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [TestMethod, TestCategory("User"), TestCategory("SentVerificationCode")]
        public async Task SentVerificationCode() {
            try {
                var model = new User_SetVerificationCode_Schema { DeviceId = "", VerificationCode = 911 };
                await _userService.SetVerificationCodeAsync(model);
                Assert.IsTrue(model.StatusCode > 0);
                Console.WriteLine($"StatusCode: {model.StatusCode}");
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [TestMethod, TestCategory("User"), TestCategory("GetById")]
        public async Task GetById() {
            try {
                var model = new Void_Schema { EntityName = "[user]" };
                var result = await _userService.GetAsync(model);
                Assert.IsTrue(model.StatusCode > 0);
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Properties.Any());
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [TestMethod, TestCategory("User"), TestCategory("SetActivities")]
        public void SetActivities() {
        }
    }
}