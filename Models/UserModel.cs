using J2N.Collections.Generic;
using System;

namespace MyProject.Models
{
    /// <summary>
    /// User Model Details
    /// </summary>
    public class UserViewModel : Response
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class UserListModel : Response
    {
        public List<UserViewModel> DataList { get; set; }
        
    }


}
