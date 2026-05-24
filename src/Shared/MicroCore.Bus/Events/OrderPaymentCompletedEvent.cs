using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCore.Bus.Events
{
    public class OrderPaymentCompletedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; } = null!;

        public List<Guid> CourseIds { get; set; } = new();
    }
}
