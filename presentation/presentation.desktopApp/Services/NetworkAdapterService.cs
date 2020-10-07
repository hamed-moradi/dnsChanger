using Presentation.DesktopApp.Infrastructure;
using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Services {
    public class NetworkAdapterContainer {
        #region ctor
        private readonly SqliteDbContext _myDbContext;
        public NetworkAdapterContainer() {
            _myDbContext = new SqliteDbContext();
        }
        #endregion

        public NetworkAdapter Add(NetworkAdapter model) {
            var netAdaptor = _myDbContext.NetworkAdapters.FirstOrDefault(f => f.AdapterId == model.AdapterId);
            if(netAdaptor is null) {
                var result = _myDbContext.NetworkAdapters.Add(model);
                _myDbContext.SaveChanges();
                return result;
            }
            return model;
        }

        public int Remove(NetworkAdapter model) {
            _myDbContext.NetworkAdapters.Remove(model);
            return _myDbContext.SaveChanges();
        }
    }
}
