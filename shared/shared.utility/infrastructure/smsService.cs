using System;
using System.Collections.Generic;
using System.Text;

namespace shared.utility.infrastructure {
    public interface ISMSService {
        (bool result, string errorMessage, string trackId) Send(string phoneNo, string message);
    }
    public class Candoo: ISMSService {

        public (bool result, string errorMessage, string trackId) Send(string phoneNo, string message) {
            return (false, null, null);
        }
    }
}
