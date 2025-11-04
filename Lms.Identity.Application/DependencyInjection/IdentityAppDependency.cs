using Lms.Shared.Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Identity.Application.DependencyInjection
{
    public static class IdentityAppDependency
    {
        public static IServiceCollection AddIdentityApplicationServices(this IServiceCollection services)
        {
            return services.AddMediatetorService();
                //.AddCustomMediator(typeof(IdentityAppDependency).Assembly);
        }
    }
}
