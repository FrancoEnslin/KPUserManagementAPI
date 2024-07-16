//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using KPUserManagementAPI.BusinessLogic; // Adjust as per your project structure
//using KPUserManagementAPI.Models; // Adjust as per your project structure
//using KPUserManagementAPI.Dtos;

//namespace KPUserManagementAPI.Pages
//{
//    public class IndexModel : PageModel
//    {
//        private readonly UsersBusinessLogic _usersBusinessLogic;

//        public IndexModel(UsersBusinessLogic usersBusinessLogic)
//        {
//            _usersBusinessLogic = usersBusinessLogic;
//        }

//        public List<ReturnUser> Users { get; set; }

//        public async Task OnGetAsync()
//        {
//            try
//            {
//                // Call your business logic method to get users
//                var result = await _usersBusinessLogic.GetUsers();

//                if (result is OkObjectResult okResult && okResult.Value is List<ReturnUser> users)
//                {
//                    Users = users;
//                }
//                else
//                {
//                    // Handle error or no data scenario
//                    Users = new List<ReturnUser>();
//                    Console.WriteLine("Failed to retrieve users: " + result);
//                }
//            }
//            catch (Exception ex)
//            {
//                // Handle exception, log, or set Users to empty list
//                Users = new List<ReturnUser>();
//                Console.WriteLine("Exception while fetching users: " + ex.Message);
//            }
//        }

//        public bool IsAdminUser(ReturnUser user)
//        {
//            // Implement your logic to check if user has 'Admin' permission
//            // Example:
//            return user.Groups.Exists(group => group.Permissions.Contains("Admin"));
//        }
//    }
//}
