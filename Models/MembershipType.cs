using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public Byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public Byte DurationInMonths { get; set; }
        public Byte DiscountRate { get; set; }
    }
}