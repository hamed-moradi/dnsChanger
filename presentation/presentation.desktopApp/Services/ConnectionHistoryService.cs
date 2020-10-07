using Presentation.DesktopApp.Infrastructure;
using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Services {
    public class ConnectionHistoryContainer {
        #region ctor
        private readonly SqliteDbContext _myDbContext;
        public ConnectionHistoryContainer() {
            _myDbContext = new SqliteDbContext();
        }
        #endregion

        public async Task<ConnectionHistory> Add(ConnectionHistory model) {
            var result = _myDbContext.ConnectionHistories.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
