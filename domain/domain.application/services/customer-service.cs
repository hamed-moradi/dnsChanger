using domain.application._app;
using domain.repository.models;
using domain.repository.schemas;
using shared.utility;
using shared.utility._app;
using shared.utility.infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.application.services {
    public class Customer_Service: ICustomer_Service {
        #region Constructor
        private readonly IParameterHandler _parameterHandler;
        private readonly IGenericRepository<IBase_Model> _repository;
        private readonly IStoreProcedure<Customer_GetById_Model, Void_Schema> _customerGet;
        public Customer_Service(
            IParameterHandler parameterHandler,
            IGenericRepository<IBase_Model> repository, 
            IStoreProcedure<Customer_GetById_Model, Void_Schema> customerGet) {
            _repository = repository;
            _parameterHandler = parameterHandler;
            _customerGet = customerGet;
        }
        #endregion
        public async Task<User_Model> SignUpAsync(User_SignUp_Schema model) {
            var user = new User_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                user = await result.ReadFirstAsync<User_Model>();
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
        public async Task<Customer_GetById_Model> GetByIdAsync(Void_Schema model) {
            return await _customerGet.ExecuteFirstOrDefaultAsync(model);
        }
    }
}
