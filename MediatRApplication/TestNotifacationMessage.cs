using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRApplication
{

    public class TestNotificationMessage1 : INotification
    {
        public string Message { get; set; }
    }
    public class TestNotificationMessage2 : INotification
    {
        public string Message { get; set; }
    }
    public class TestNotificationMessage3 : INotification
    {
        public string Message { get; set; }
    }

}
