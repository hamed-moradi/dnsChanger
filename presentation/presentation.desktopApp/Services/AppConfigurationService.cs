using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp.Services {
    public class AppConfigurationContainer {
        #region ctor
        private readonly SqliteDbContext _myDbContext;
        public AppConfigurationContainer() {
            _myDbContext = new SqliteDbContext();
        }
        #endregion

        public async Task<AppConfiguration> Get(int id) {
            var result = await _myDbContext.AppConfigurations.FindAsync(id);
            return result;
        }

        public async Task<int> Update(AppConfiguration model) {
            var appConf = await _myDbContext.AppConfigurations.FindAsync(model.Id);
            appConf = model;
            return await _myDbContext.SaveChangesAsync();
        }
    }
}
