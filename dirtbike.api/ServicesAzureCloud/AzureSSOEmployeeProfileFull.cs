/*public class EmployeeProfileFull : DbContext
{
	public EmployeeProfileFull(DbContextOptions<CeContext> options) : base(options) { }

	public DbSet<Employee> Employee { get; set; }
	public DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Employee>()
			.HasOne(s => s.EmployeeProfile)
			.WithMany(c => c.Employee)
			.HasForeignKey(s => s.EmployeeId);
	}
}
*/