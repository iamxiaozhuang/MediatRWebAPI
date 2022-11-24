using MediatR;

namespace MediatRApplication
{
    public class TestNotificationMessage1Handler : INotificationHandler<TestNotificationMessage1>
    {

        public async Task Handle(TestNotificationMessage1 notification, CancellationToken cancellationToken)
        {
            string message = notification.Message;
        }
    }
}