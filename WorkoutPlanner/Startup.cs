using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Model;
using Owin;

[assembly: OwinStartup(typeof(WorkoutPlanner.Startup))]
namespace WorkoutPlanner
{

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
