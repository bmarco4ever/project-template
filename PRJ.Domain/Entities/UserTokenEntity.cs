using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ.Domain.Entities
{
    public class UserTokenEntity
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
