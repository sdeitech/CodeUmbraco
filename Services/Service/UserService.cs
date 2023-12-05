using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyProject.Models;
using MyProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common;
using Umbraco.Extensions;

namespace MyProject.Services.Service
{
    public class UserService : IUsersService
    {
        #region Private Fields
        private readonly IContentService _contentService;
        private readonly UmbracoHelper _umbracoHelper;
        private readonly ILogger<UserService> _logger;
        private readonly IPublishedContentQuery _publishedContentQuery;
        #endregion

        #region Ctor
        public UserService(
            IContentService contentService,
             IHttpContextAccessor contextAccessor, IPublishedContentQuery publishedContentQuery,
             ILogger<UserService> logger
             )
        {
            _logger = logger;
            _contentService = contentService;
            _publishedContentQuery = publishedContentQuery;
            _umbracoHelper = contextAccessor.HttpContext.RequestServices.GetRequiredService<UmbracoHelper>();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserViewModel GetUser(Guid userid)
        {
            UserViewModel user = new();
            try
            {
                if(userid != Guid.Empty)
                {
                    //Fetch User using IPublishedContentQuery service to get published content. 
                    var node = _publishedContentQuery.Content(userid);
                    if(node != null)
                    {
                        user.UserId = node.Key;
                        user.UserName = node.GetProperty("userName").GetValue().ToString();
                        user.Email = node.GetProperty("email").GetValue().ToString();
                        user.Phone = node.GetProperty("phone").GetValue().ToString();
                        user.ResponseMessage = "Succesfull.";
                        user.ResponseCode = 200;
                    }
                    else
                    {
                        user = new();
                        user.ResponseMessage = "No Record Found.";
                        user.ResponseCode = 404;
                    }
                }
                else
                {
                    user = new();
                    user.ResponseMessage = "Invalid request.";
                    user.ResponseCode = 400;
                }
            }
            catch (Exception ex)
            {
                user = new();
                user.ResponseMessage = ex.Message;
                user.ResponseCode = 500;
            }
            return user;
        }

        /// <summary>
        /// Get all users content list
        /// </summary>
        /// <returns></returns>
        public UserListModel GetAllUser()
        {
            UserListModel userlist = new();
            try
            {
                //Fetch All User content using UmbracoHelper service to get content by document type. 
                var node = _umbracoHelper.ContentAtXPath("//user");

                //filter data by documenttype and sort by user name
                node = node.Where(x => x.IsDocumentType("user")).OrderBy(x => x.Name).ToList();

                if (node != null && node.Any())
                {
                    foreach(var item in node)
                    {
                        UserViewModel _obj = new();
                        _obj.UserId = item.Key;
                        _obj.UserName = item.GetProperty("userName").GetValue().ToString();
                        _obj.Email = item.GetProperty("email").GetValue().ToString();
                        _obj.Phone = item.GetProperty("phone").GetValue().ToString();

                        userlist.DataList.Add(_obj);
                    }
                    userlist.ResponseMessage = "Succesfull.";
                    userlist.ResponseCode = 200;
                }
                else
                {
                    userlist.ResponseMessage = "No Record Found.";
                    userlist.ResponseCode = 404;
                }
                
            }
            catch (Exception ex)
            {
                userlist = new();
                userlist.ResponseMessage = ex.Message;
                userlist.ResponseCode = 500;
            }
            return userlist;
        }

        /// <summary>
        /// Delete user content by userid
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public Response DeleteUser(Guid userid)
        {
            Response response = new();
            try
            {
                if (userid != Guid.Empty)
                {
                    //Fetch User using IContentService service by using ID. 
                    var node = _contentService.GetById(userid);
                    if (node != null)
                    {
                        // Delete user content using Delete Functionality of IContentService. 
                        _contentService.Delete(node);
                        var result = _contentService.SaveAndPublish(node);

                        if (result.Success)
                        {
                            response.ResponseMessage = "Deleted succesfully.";
                            response.ResponseCode = 200;
                        }
                    }
                    else
                    {
                        response = new();
                        response.ResponseMessage = "No Record Found.";
                        response.ResponseCode = 404;
                    }
                }
                else
                {
                    response = new();
                    response.ResponseMessage = "Invalid request.";
                    response.ResponseCode = 400;
                }
            }
            catch (Exception ex)
            {
                response = new();
                response.ResponseMessage = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
        }

        /// <summary>
        /// Create user content if no userid and update user content if id present in data model
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Response ManageUser(UserViewModel user)
        {
            Response response = new();
            try
            {
                if (user != null)
                {
                    IContent _content = null;

                    // Fetch parent folder details where user need to be created
                    var Root = _umbracoHelper.ContentAtXPath("//users");
                    var parentId = Root.FirstOrDefault(x => x.IsDocumentType("users"));

                    if (user.UserId != Guid.Empty)
                    {
                        // Fetch content details using user id
                        _content = _contentService.GetById(user.UserId);
                        response.ResponseMessage = "User updated successfully.";
                    }
                    else
                    {
                        //create new User details document content using Username , parent folder id , user document content name.
                        _content = _contentService.Create(user.UserName, Guid.Parse(parentId.Key.ToString()), "users");
                        response.ResponseMessage = "User created successfully.";
                    }

                    // set or update user data with new data value using IContentService set value functionality
                    _content.SetValue("userGuid", _content.Key);
                    _content.SetValue("userName", user.UserName);
                    _content.SetValue("email", user.Email);
                    _content.SetValue("phone", user.Phone);
                    var result = _contentService.SaveAndPublish(_content);
                    if (result.Success)
                    {
                        response.ResponseCode = 200;
                    }
                }
                else
                {
                    response = new();
                    response.ResponseMessage = "Invalid request.";
                    response.ResponseCode = 400;
                }
            }
            catch (Exception ex)
            {
                response = new();
                response.ResponseMessage = ex.Message;
                response.ResponseCode = 500;
            }
            return response;
        }

        #endregion
    }
}
