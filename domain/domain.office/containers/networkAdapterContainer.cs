using domain.office._app;
using domain.office.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.containers {
    public class NetworkAdapterContainer {
        #region ctor
        private readonly MyDbContext _myDbContext;
        public NetworkAdapterContainer() {
            _myDbContext = new MyDbContext();
        }
        #endregion

        public async Task<NetworkAdapter> Add(NetworkAdapter model) {
            var result = _myDbContext.NetworkConnections.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Remove(NetworkAdapter model) {
            _myDbContext.NetworkConnections.Remove(model);
            return await _myDbContext.SaveChangesAsync();
        }
    }
}
