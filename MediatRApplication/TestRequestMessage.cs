using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRApplication
{
    public class TestRequestWithNoResponseMessage : IRequest
    {
        public string Message { get; set; }
    }

}
