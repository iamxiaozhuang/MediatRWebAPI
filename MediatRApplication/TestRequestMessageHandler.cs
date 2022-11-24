using MediatR;

namespace MediatRApplication
{
    public class TestRequestMessage1Handler : IRequestHandler<CreateCategory, TestResponseMessage1>
    {

        public async Task<TestResponseMessage1> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            TestResponseMessage1 testResponseMessage = new TestResponseMessage1();
            testResponseMessage.Message = $"ACK:{request.Message},{DateTime.Now.ToString("HH:mm:ss")}";
            return testResponseMessage;
        }
    }

    public class TestRequestMessage3Handler : IRequestHandler<UpdateCategory, TestResponseMessage3>
    {

        public async Task<TestResponseMessage3> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            TestResponseMessage3 testResponseMessage = new TestResponseMessage3();
            testResponseMessage.Message = $"ACK:{request.Message},{DateTime.Now.ToString("HH:mm:ss")}";
            return testResponseMessage;
        }
    }

    public class TestRequestWithNoResponseMessage1Handler : IRequestHandler<DeleteCategory>
    {

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            TestResponseMessage3 testResponseMessage = new TestResponseMessage3();
            testResponseMessage.Message = $"ACK:{request.Message},{DateTime.Now.ToString("HH:mm:ss")}";
            return Unit.Value;
        }
    }
}