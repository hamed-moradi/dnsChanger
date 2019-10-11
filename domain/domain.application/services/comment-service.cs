using domain.application._app;
using domain.repository.models;
using domain.repository.schemas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.application.services {
    public class Comment_Service: IComment_Service {
        #region Constructor
        private readonly IStoreProcedure<Comment_Model, Comment_GetPaging_Schema> _getPaging;
        private readonly IStoreProcedure<Comment_Model, Comment_New_Schema> _new;
        private readonly IStoreProcedure<Comment_Model, Comment_Edit_Schema> _edit;
        public Comment_Service(
            IStoreProcedure<Comment_Model, Comment_GetPaging_Schema> getPaging,
            IStoreProcedure<Comment_Model, Comment_New_Schema> @new,
            IStoreProcedure<Comment_Model, Comment_Edit_Schema> edit) {
            _getPaging = getPaging;
            _new = @new;
            _edit = edit;
        }
        #endregion

        public async Task<List<Comment_Model>> GetPagingAsync(Comment_GetPaging_Schema model) {
            var result = await _getPaging.ExecuteAsync(model);
            model.TotalCount = result.Any() ? result.Single().TotalCount : 0;
            return result.ToList();
        }
        public async Task<Comment_Model> CreateAsync(Comment_New_Schema model) {
            var result = await _new.ExecuteFirstOrDefaultAsync(model);
            return result;
        }
        public async Task<Comment_Model> EditAsync(Comment_Edit_Schema model) {
            var result = await _edit.ExecuteFirstOrDefaultAsync(model);
            return result;
        }
    }
}
