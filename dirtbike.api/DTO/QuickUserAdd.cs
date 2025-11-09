namespace dirtbike.api.DTOs
{
    public class QuickUserAdd
    {
        public required string Username { get; set; }
        public required string Fullname { get; set; }
        public required string Email { get; set; }
        public required string ActiveProfileUrl { get; set; }
        public required string Role { get; set; }

        public int GenerateUserId()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999); // 6-digit number
        }

        public Models.User ToUser(int userId)
        {
            return new Models.User
            {
                Username = this.Username,
                Fullname = this.Fullname,
                Email = this.Email,
                Activeprofileurl = this.ActiveProfileUrl,
                Userid = userId,
                Role = this.Role
                // Other fields can be defaulted or omitted
            };
        }

        public Models.Userprofile ToUserProfile(int userId)
        {
            return new Models.Userprofile
            {
                Userid = userId,
                Fullname = this.Fullname,
                Email = this.Email,
                Activepictureurl = this.ActiveProfileUrl
                // Other fields can be defaulted or omitted
            };
        }
    }
}


