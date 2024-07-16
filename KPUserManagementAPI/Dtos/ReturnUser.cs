namespace KPUserManagementAPI.Dtos
{
    public class ReturnUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<string> Groups { get; set; }
        public ICollection<string> Permissions { get; set; }
    }
}
