using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KPUserManagementAPI.Models; // Ensure you have the correct namespace
using KPUserManagementAPI.Dtos;

namespace KPUserManagementAPI.Pages
{
    public class AddUserModel : PageModel
    {
        [BindProperty]
        public AddUser user  { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Add your logic to save the user here

            return RedirectToPage("/Index");
        }
    }
}
