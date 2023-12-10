using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }

        public enum DiscountType
        {
            Percentage,
            Value
        }
    }
}
