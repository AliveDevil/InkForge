using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InkForge.Api.Data;

public class ApiDbcontext(
	DbContextOptions<ApiDbcontext> options
) : IdentityDbContext<IdentityUser>(options)
{
	public DbSet<WorkspaceEntity> Workspaces { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<WorkspaceEntity>(options =>
		{
			options.HasKey(m => m.Id);

			options.OwnsOne(m => m.Value);
		});
	}
}
