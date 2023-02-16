namespace SP23.P02.Web.Features.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
