using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json;

[assembly: OwinStartup(typeof(BookingBreakerApi.Startup))]

namespace BookingBreakerApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
