/*public class UserProfileFull : DbContext
{
	public UserProfileFull(DbContextOptions<CeContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<UserProfile> UserProfiles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>()
			.HasOne(s => s.UserProfile)
			.WithMany(c => c.Users)
			.HasForeignKey(s => s.UserId);
	}
}
*/