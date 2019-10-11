using domain.office._app;
using domain.office.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office.containers {
    public class AppConfigurationContainer {
        #region ctor
        private readonly MyDbContext _myDbContext;
        public AppConfigurationContainer() {
            _myDbContext = new MyDbContext();
        }
        #endregion

        public async Task<AppConfiguration> Get() {
            var result = await _myDbContext.AppConfigurations.FindAsync(1);
            return result;
        }
    }
}
