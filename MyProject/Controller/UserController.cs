using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.Models;
using MyProject.Services.Interface;
using System;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace MyProject.Controller
{
    public class UserController : SurfaceController
    {
        #region Private Fields

        private readonly IUsersService _services;
        private readonly ILogger<UserController> _logger;

        #endregion

        #region Ctor
        public UserController(
        //these are required by the base controller
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider,
        IUsersService user, ILogger<UserController> logger
       )
      : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {

            _services = user;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get User details by ID
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUser(Guid userid)
        {
            UserViewModel res = new();
            try
            {
                res = _services.GetUser(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User Controller - GetUserAPI :" + ex.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// Get All User List API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAllUserList()
        {
            UserListModel res = new();
            try
            {
                res = _services.GetAllUser();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User Controller - GetAllUserList : "+ ex.Message);
            }

            return new JsonResult(res);

        }

        /// <summary>
        /// Delete User API
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteUser(Guid userid)
        {
            Response res = new();
            try
            {
                res = _services.DeleteUser(userid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User Controller - DeleteUser : " + ex.Message);
            }
            return new JsonResult(res);
        }

        /// <summary>
        /// Create/Update User API
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ManageUser(UserViewModel model)
        {
            Response res = new();
            try
            {
                res = _services.ManageUser(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User Controller - ManageUser : " + ex.Message);
            }
            return new JsonResult(res);
        }
        #endregion
    }
}
