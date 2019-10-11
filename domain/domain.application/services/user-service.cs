using domain.application._app;
using domain.repository.models;
using domain.repository.schemas;
using shared.utility;
using shared.utility._app;
using shared.utility.infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace domain.application.services {
    public class UserService: IUser_Service {
        #region Constructor
        private readonly IParameterHandler _parameterHandler;
        private readonly IGenericRepository<IBase_Model> _repository;
        private readonly IStoreProcedure<IBase_Model, User_SetVerificationCode_Schema> _setVerificationCode;
        private readonly IStoreProcedure<IBase_Model, User_Verify_Schema> _verify;
        private readonly IStoreProcedure<IBase_Model, User_EnableTwoFactorAuthentication_Schema> _enableTwoFactorAuthentication;
        private readonly IStoreProcedure<IBase_Model, User_DisableTwoFactorAuthentication_Schema> _disableTwoFactorAuthentication;
        private readonly IStoreProcedure<User_Model, User_SignIn_Schema> _signIn;
        private readonly IStoreProcedure<IBase_Model, User_Update_Schema> _updateProfile;
        private readonly IStoreProcedure<IBase_Model, User_DisableMe_Schema> _disableMe;
        public UserService(IParameterHandler parameterHandler,
            IGenericRepository<IBase_Model> repository,
            IStoreProcedure<IBase_Model, User_SetVerificationCode_Schema> setVerificationCode,
            IStoreProcedure<IBase_Model, User_Verify_Schema> verify,
            IStoreProcedure<IBase_Model, User_EnableTwoFactorAuthentication_Schema> enableTwoFactorAuthentication,
            IStoreProcedure<IBase_Model, User_DisableTwoFactorAuthentication_Schema> disableTwoFactorAuthentication,
            IStoreProcedure<User_Model, User_SignIn_Schema> signIn,
            IStoreProcedure<IBase_Model, User_Update_Schema> updateProfile,
            IStoreProcedure<IBase_Model, User_DisableMe_Schema> disableMe) {
            _repository = repository;
            _parameterHandler = parameterHandler;
            _setVerificationCode = setVerificationCode;
            _verify = verify;
            _enableTwoFactorAuthentication = enableTwoFactorAuthentication;
            _disableTwoFactorAuthentication = disableTwoFactorAuthentication;
            _signIn = signIn;
            _updateProfile = updateProfile;
            _disableMe = disableMe;
        }
        #endregion

        public async Task SignInAsync(User_SignIn_Schema model) {
            await _signIn.ExecuteFirstOrDefaultAsync(model);
        }
        public async Task<User_Model> SignUpAsync(User_SignUp_Schema model) {
            var user = new User_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                user = await result.ReadFirstOrDefaultAsync<User_Model>();
            }
            if(!result.IsConsumed) {
                user.Customer = await result.ReadFirstOrDefaultAsync<Customer_Model>();
            }
            if(!result.IsConsumed) {
                var properties = await result.ReadAsync<UserProperty_Model>();
                if(properties.Any()) {
                    user.Properties = properties.ToList();
                }
            }
            _parameterHandler.SetOutputValues(model, parameters);
            _parameterHandler.SetReturnValue(model, parameters);
            return user;
        }
        public async Task SetVerificationCodeAsync(User_SetVerificationCode_Schema model) {
            await _setVerificationCode.ExecuteReturnLessAsync(model);
        }
        public async Task VerifyAsync(User_Verify_Schema model) {
            await _verify.ExecuteReturnLessAsync(model);
        }
        public async Task<User_Model> GetAsync(Void_Schema model) {
            var user = new User_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                user = await result.ReadFirstOrDefaultAsync<User_Model>();
            }
            if(!result.IsConsumed) {
                user.Customer = await result.ReadFirstOrDefaultAsync<Customer_Model>();
            }
            if(!result.IsConsumed) {
                var properties = await result.ReadAsync<UserProperty_Model>();
                if(properties.Any()) {
                    user.Properties = properties.ToList();
                }
            }
            _parameterHandler.SetOutputValues(model, parameters);
            _parameterHandler.SetReturnValue(model, parameters);
            return user;
        }
        public async Task EnableTwoFactorAuthentication(User_EnableTwoFactorAuthentication_Schema model) {
            await _enableTwoFactorAuthentication.ExecuteReturnLessAsync(model);
        }
        public async Task DisableTwoFactorAuthentication(User_DisableTwoFactorAuthentication_Schema model) {
            await _disableTwoFactorAuthentication.ExecuteReturnLessAsync(model);
        }
        public async Task UpdateAsync(User_Update_Schema model) {
            await _updateProfile.ExecuteReturnLessAsync(model);
        }
        public async Task DisableMeAsync(User_DisableMe_Schema model) {
            await _disableMe.ExecuteReturnLessAsync(model);
        }
    }
}