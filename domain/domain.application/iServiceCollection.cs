using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.application {
    public interface IException_Service {
        Task InsertAsync(Exception model, string url, string ip);
    }
}
