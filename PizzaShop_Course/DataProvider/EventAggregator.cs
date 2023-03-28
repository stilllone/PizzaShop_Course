using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public class EventAggregator
    {
        private static readonly Lazy<EventAggregator> instance = new Lazy<EventAggregator>(() => new EventAggregator());

        private EventAggregator()
        {
        }

        public static EventAggregator Instance => instance.Value;

        public NotificationEvent NotificationEvent { get; } = new NotificationEvent();
    }
}
