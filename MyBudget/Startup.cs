﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyBudget.Startup))]
namespace MyBudget
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
