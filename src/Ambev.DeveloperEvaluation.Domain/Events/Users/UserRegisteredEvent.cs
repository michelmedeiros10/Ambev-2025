using Ambev.DeveloperEvaluation.Common.Service;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events.Users
{
    public class UserRegisteredEvent : IEvent, IIntegrationEvent
    {
        public Guid Oid { get; set; }
        public Guid Sid { get; set; }
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty;

        public User User { get; }

        public UserRegisteredEvent(User user)
        {
            User = user;
            Type = GetType().FullName;
        }
    }
}
