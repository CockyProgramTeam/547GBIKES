namespace dirtbike.api.DTOs
{
    public class QuickUserAdd
    {
        public required string Username { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string Activepictureurl { get; set; }
        public required string Role { get; set; }
        public required string Plainpassword { get; set;}

        public Models.User ToUser()
        {
            return new Models.User
            {
                Username = this.Username,
                Fullname = this.Fullname,
                Email = this.Email,
                Activepictureurl = this.Activepictureurl, // ✅ required non-null
                Role = this.Role,
                Plainpassword = this.Plainpassword
                // Id → auto-increment
                // Userid → trigger assigns
            };
        }
    }
}

