using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopProject.Domain.Entities
{
    public class StripeInfo
    {
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }

        public string WHSecret { get; set; }
    }
}
