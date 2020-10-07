using Presentation.DesktopApp.Infrastructure;
using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Services {
    public class Network2DNSContainer {
        #region ctor
        private readonly SqliteDbContext _myDbContext;
        public Network2DNSContainer() {
            _myDbContext = new SqliteDbContext();
        }
        #endregion

        public async Task<Network2DNS> Add(Network2DNS model) {
            var result = _myDbContext.Network2DNSes.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }
    }
}
