using System;
using System.Collections.Generic;
using System.Text;

namespace shared.utility.infrastructure {
    public interface IEmailService {
        bool Send(string email, string subject, string body);
    }
    public class EmailService: IEmailService {
        public bool Send(string email, string subject, string body) {
            return true;
        }
    }
}
