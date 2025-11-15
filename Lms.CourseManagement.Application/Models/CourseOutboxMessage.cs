using Lms.SharedKernel.Domain;
using System.Text.Json;

namespace Lms.CourseManagement.Application.Models
{
    public class CourseOutboxMessage 
    {
        public Guid Id { get; set; }
        public string? Type { get; set; } = string.Empty;
        public string Content { get;  set; } = string.Empty; //
        public DateTime? ProcessedOn { get; set; } // When ot was successfully published
        public DateTime? OccuredOn { get;  set; } // when event happened
        public string? Error { get; set; }

        public int RetryCount { get; set; } = 0;
        public DateTime? NextRetryOn { get; set; }

        private CourseOutboxMessage() { } // EF Core

        public CourseOutboxMessage(object integrationEvent)
        {
            Id = Guid.NewGuid();
            Type = integrationEvent.GetType().AssemblyQualifiedName;
            Content = JsonSerializer.Serialize(integrationEvent); // serialized
            OccuredOn = DateTime.UtcNow;
        }

        public void MarkProcessedOn() => ProcessedOn = DateTime.Now;
        public void MarkFailed(string error) => Error = error; 
    }
}
