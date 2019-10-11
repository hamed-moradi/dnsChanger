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
    public class Product_Service: IProduct_Service {
        #region Constructor
        private readonly IParameterHandler _parameterHandler;
        private readonly IGenericRepository<IBase_Model> _repository;
        private readonly IStoreProcedure<Product_Model, Product_GetByLocation_Schema> _getByLocation;
        private readonly IStoreProcedure<Product_Model, Product_GetPaging_Schema> _getPaging;
        public Product_Service(IParameterHandler parameterHandler,
            IGenericRepository<IBase_Model> repository,
            IStoreProcedure<Product_Model, Product_GetByLocation_Schema> getByLocation,
            IStoreProcedure<Product_Model, Product_GetPaging_Schema> getPaging) {
            _repository = repository;
            _parameterHandler = parameterHandler;
            _getByLocation = getByLocation;
            _getPaging = getPaging;
        }
        #endregion
        public async Task<Product_Model> GetAsync(Product_Get_Schema model) {
            var product = new Product_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                product = await result.ReadFirstOrDefaultAsync<Product_Model>();
            }
            if(!result.IsConsumed) {
                var images = await result.ReadAsync<Image_Model>();
                if(images.Any()) {
                    product.Images = images.ToList();
                }
            }
            _parameterHandler.SetOutputValues(model, parameters);
            _parameterHandler.SetReturnValue(model, parameters);
            return product;
        }
        public async Task<List<Product_Model>> GetByLocationAsync(Product_GetByLocation_Schema model) {
            var result = await _getByLocation.ExecuteAsync(model);
            return result.ToList();
        }
        public async Task<List<Product_Model>> GetPagingAsync(Product_GetPaging_Schema model) {
            var result = await _getPaging.ExecuteAsync(model);
            model.TotalCount = result.Any() ? result.Single().TotalCount : 0;
            return result.ToList();
        }
        public async Task<Product_Model> CreateAsync(Product_New_Schema model) {
            var product = new Product_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                product = await result.ReadFirstOrDefaultAsync<Product_Model>();
            }
            if(!result.IsConsumed) {
                var images = await result.ReadAsync<Image_Model>();
                if(images.Any()) {
                    product.Images = images.ToList();
                }
            }
            _parameterHandler.SetOutputValues(model, parameters);
            _parameterHandler.SetReturnValue(model, parameters);
            return product;
        }
        public async Task<Product_Model> EditAsync(Product_Edit_Schema model) {
            var product = new Product_Model();
            var parameters = _parameterHandler.MakeParameters(model);
            var result = await _repository.QueryMultipleAsync(model.GetSchemaName(), param: parameters, commandType: CommandType.StoredProcedure);
            if(!result.IsConsumed) {
                product = await result.ReadFirstOrDefaultAsync<Product_Model>();
            }
            if(!result.IsConsumed) {
                var images = await result.ReadAsync<Image_Model>();
                if(images.Any()) {
                    product.Images = images.ToList();
                }
            }
            _parameterHandler.SetOutputValues(model, parameters);
            _parameterHandler.SetReturnValue(model, parameters);
            return product;
        }
    }
}
