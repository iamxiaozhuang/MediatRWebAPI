using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRApplication
{
    public class CreateCategory : IRequest<TestResponseMessage1>
    {
        public string Message { get; set; }
    }
    public class TestResponseMessage1
    {
        public string Message { get; set; }
    }

    public class ReadCategory : IRequest<TestResponseMessage2>
    {
        public string Message { get; set; }
    }
    public class TestResponseMessage2
    {
        public string Message { get; set; }
    }

    public class UpdateCategory : IRequest<TestResponseMessage3>
    {
        public string Message { get; set; }
    }
    public class TestResponseMessage3
    {
        public string Message { get; set; }
    }

    public class DeleteCategory : IRequest
    {
        public string Message { get; set; }
    }
    public class TestRequestWithNoResponseMessage : IRequest
    {
        public string Message { get; set; }
    }

}
