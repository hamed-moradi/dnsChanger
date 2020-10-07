using Presentation.DesktopApp.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DesktopApp {
    public partial class SqliteDbContext: DbContext {
        #region ctor
        public SqliteDbContext() : base("Name=ConnectionString") { }//AppSettings.ConnectionString
        #endregion

        #region instance
        private static SqliteDbContext _instance;
        private static readonly object _syncLock = new object();
        public static SqliteDbContext Instance {
            get {
                if(_instance == null) {
                    lock(_syncLock) {
                        if(_instance == null) {
                            _instance = new SqliteDbContext();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public virtual DbSet<AppConfiguration> AppConfigurations { get; set; }
        public virtual DbSet<ConnectionHistory> ConnectionHistories { get; set; }
        public virtual DbSet<DNSAddress> DNSAddresses { get; set; }
        public virtual DbSet<Network2DNS> Network2DNSes { get; set; }
        public virtual DbSet<NetworkAdapter> NetworkAdapters { get; set; }
    }
}