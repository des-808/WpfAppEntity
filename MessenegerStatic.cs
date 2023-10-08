using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppEntity
{
    public static class MessengerStatic
    {
        public static event Action<object> Bus;
        public static void Send(object data)=> Bus?.Invoke(data);
    }
}
