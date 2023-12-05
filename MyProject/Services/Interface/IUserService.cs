using MyProject.Models;
using System;
using System.Collections.Generic;

namespace MyProject.Services.Interface
{
    public interface IUsersService
    {
        /// <summary>
        /// Get User by ID
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        UserViewModel GetUser(Guid userid);

        /// <summary>
        /// Get All Users List
        /// </summary>
        /// <returns></returns>
        UserListModel GetAllUser();

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Response DeleteUser(Guid userid);

        /// <summary>
        /// Create & update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Response ManageUser(UserViewModel user);
    }
}
