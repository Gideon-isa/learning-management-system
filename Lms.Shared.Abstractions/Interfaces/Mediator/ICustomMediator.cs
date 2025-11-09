using Lms.Shared.Abstractions.Interfaces.EventPublisher;
using Lms.Shared.Abstractions.Interfaces.Sender;

namespace Lms.Shared.Application.CustomMediator.Interfaces.Mediator
{
    public interface ICustomMediator : ISender, IPublisher { }
}
