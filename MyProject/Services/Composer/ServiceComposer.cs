using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Mapping;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Services.Interface;
using MyProject.Services.Service;

namespace MyProject.Services.Composer
{
    public class ServiceComposer : IComposer
    {   
        /// <summary>
        /// Composer is to build and run Custom Service in Project startup. 
        /// </summary>
        /// <param name="builder"></param>
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IUsersService, UserService>();
        }
    }
}
