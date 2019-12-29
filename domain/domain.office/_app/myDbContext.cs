using domain.office.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.office._app {
    public partial class MyDbContext: DbContext {
        #region ctor
        public MyDbContext() : base("Name=ConnectionString") { }//AppSettings.ConnectionString
        #endregion

        #region instance
        private static MyDbContext _instance;
        private static readonly object _syncLock = new object();
        public static MyDbContext Instance {
            get {
                if(_instance == null) {
                    lock(_syncLock) {
                        if(_instance == null) {
                            _instance = new MyDbContext();
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