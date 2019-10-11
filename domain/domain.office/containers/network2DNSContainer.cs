using domain.office._app;
using domain.office.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.containers {
    public class Network2DNSContainer {
        #region ctor
        private readonly MyDbContext _myDbContext;
        public Network2DNSContainer() {
            _myDbContext = new MyDbContext();
        }
        #endregion

        public async Task<Network2DNS> Add(Network2DNS model) {
            var result = _myDbContext.Network2DNSes.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
