namespace KPUserManagementAPI.Dtos
{
    public class ReturnGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Users { get; set; }
    }
}
