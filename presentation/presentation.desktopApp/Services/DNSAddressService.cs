using Presentation.DesktopApp.Infrastructure;
using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Services {
    public class DNSAddressContainer {
        #region ctor
        private readonly SqliteDbContext _myDbContext;
        public DNSAddressContainer() {
            _myDbContext = new SqliteDbContext();
        }
        #endregion

        public async Task<DNSAddress> Add(DNSAddress model) {
            var result = _myDbContext.DNSAddresses.Add(model);
            await _myDbContext.SaveChangesAsync();
            return result;
        }
        public async Task<int> Update(DNSAddress model) {
            var result = _myDbContext.DNSAddresses.Find(model);
            result = model;
            return await _myDbContext.SaveChangesAsync();
        }
        public async Task<int> Remove(DNSAddress model) {
            _myDbContext.DNSAddresses.Remove(model);
            return await _myDbContext.SaveChangesAsync();
        }
    }
}
