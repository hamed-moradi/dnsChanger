using domain.office._app;
using domain.office.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.containers {
    public class ConnectionHistoryContainer {
        #region ctor
        private readonly MyDbContext _myDbContext;
        public ConnectionHistoryContainer() {
            _myDbContext = new MyDbContext();
        }
        #endregion

        public async Task<ConnectionHistory> Add(ConnectionHistory model) {
            var result = _myDbContext.ConnectionHistories.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
