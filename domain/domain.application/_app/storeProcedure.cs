using shared.utility;
using shared.utility._app;
using shared.utility.infrastructure;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace domain.application._app {
    public interface IStoreProcedure<Result, Schema>
        where Result : IBase_Model
        where Schema : IBase_Schema {
        //Sync
        void ExecuteReturnLess(string procedure);
        void ExecuteReturnLess(Schema model);
        IEnumerable<Result> Execute(string procedure);
        IEnumerable<Result> Execute(Schema model);
        Result ExecuteFirstOrDefault(string procedure);
        Result ExecuteFirstOrDefault(Schema model);

        //Async
        Task ExecuteReturnLessAsync(string procedure);
        Task ExecuteReturnLessAsync(Schema model);
        Task<IEnumerable<Result>> ExecuteAsync(string procedure);
        Task<IEnumerable<Result>> ExecuteAsync(Schema model);
        Task<Result> ExecuteFirstOrDefaultAsync(string procedure);
        Task<Result> ExecuteFirstOrDefaultAsync(Schema model);
    }
    public class StoreProcedure<Result, Schema>: IStoreProcedure<Result, Schema>
        where Result : IBase_Model
        where Schema : IBase_Schema {
        #region Constructor
        private readonly IGenericRepository<Result> _repository;
        private readonly IParameterHandler<Schema> _parameterHandler;
        public StoreProcedure(IGenericRepository<Result> repository, IParameterHandler<Schema> parameterHandler) {
            _repository = repository;
            _parameterHandler = parameterHandler;
        }
        #endregion

        //Sync
        public void ExecuteReturnLess(string procedure) {
            _repository.Execute(procedure, commandType: CommandType.StoredProcedure);
        }
        public void ExecuteReturnLess(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            _repository.Execute(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
            _parameterHandler.SetReturnValue(parameters);
        }
        public IEnumerable<Result> Execute(string procedure) {
            var result = _repository.Query(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }
        public IEnumerable<Result> Execute(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            var result = _repository.Query(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
            _parameterHandler.SetReturnValue(parameters);
            return result;
        }
        public Result ExecuteFirstOrDefault(string procedure) {
            var result = _repository.QueryFirstOrDefault(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }
        public Result ExecuteFirstOrDefault(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            var result = _repository.QueryFirstOrDefault(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
            _parameterHandler.SetReturnValue(parameters);
            return result;
        }

        //Async
        public async Task ExecuteReturnLessAsync(string procedure) {
            await _repository.ExecuteAsync(procedure, commandType: CommandType.StoredProcedure);
        }
        public async Task ExecuteReturnLessAsync(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            await _repository.ExecuteAsync(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
        }
        public async Task<IEnumerable<Result>> ExecuteAsync(string procedure) {
            var result = await _repository.QueryAsync(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<IEnumerable<Result>> ExecuteAsync(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            var result = await _repository.QueryAsync(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
            _parameterHandler.SetReturnValue(parameters);
            return result;
        }
        public async Task<Result> ExecuteFirstOrDefaultAsync(string procedure) {
            var result = await _repository.QueryFirstOrDefaultAsync(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }
        public async Task<Result> ExecuteFirstOrDefaultAsync(Schema model) {
            var parameters = _parameterHandler.MakeParameters();
            var result = await _repository.QueryFirstOrDefaultAsync(model.GetSchemaName(), parameters, commandType: CommandType.StoredProcedure);
            _parameterHandler.SetOutputValues(parameters);
            _parameterHandler.SetReturnValue(parameters);
            return result;
        }
    }
}