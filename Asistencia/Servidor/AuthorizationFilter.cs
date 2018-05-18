using Hangfire.Dashboard;
using Microsoft.Owin;
using System.Collections.Generic;
using System;

namespace Asistencia.Servidor
{
    public class AuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            var context = new OwinContext(owinEnvironment);
            return true;
        }

        bool IDashboardAuthorizationFilter.Authorize(DashboardContext context)
        {
            return true;
        }
    }
}