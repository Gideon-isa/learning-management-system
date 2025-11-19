using Lms.SharedKernel.Domain.Enums;
using System.Text.Json.Serialization;

namespace Lms.Identity.Application.Features.Identity.Users.Requests
{
    public class InstructorApprovalRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public InstructorStatus IsApproved { get; set; }
    }

}
