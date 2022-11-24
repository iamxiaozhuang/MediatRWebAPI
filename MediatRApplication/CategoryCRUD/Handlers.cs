using MediatR;

namespace MediatRApplication.CategoryCRUD
{
    public class TestRequestMessage1Handler : IRequestHandler<CreateCategory, CreateCategoryResult>
    {

        public async Task<CreateCategoryResult> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            CreateCategoryResult  testResponseMessage = new CreateCategoryResult();
            testResponseMessage.Message = $"ACK:{request.Message},{DateTime.Now.ToString("HH:mm:ss")}";
            return testResponseMessage;
        }
    }

    public class TestRequestMessage3Handler : IRequestHandler<UpdateCategory, UpdateCategoryResult>
    {

        public async Task<UpdateCategoryResult> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            UpdateCategoryResult testResponseMessage = new UpdateCategoryResult();
            testResponseMessage.Message = $"ACK:{request.Message},{DateTime.Now.ToString("HH:mm:ss")}";
            return testResponseMessage;
        }
    }

    public class TestRequestWithNoResponseMessage1Handler : IRequestHandler<DeleteCategory>
    {

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            return Unit.Value;
        }
    }
}