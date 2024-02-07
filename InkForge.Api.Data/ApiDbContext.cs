using InkForge.Api.Data.Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InkForge.Api.Data;

public class ApiDbcontext(
	DbContextOptions<ApiDbcontext> options
) : IdentityDbContext<IdentityUser>(options)
{
	public DbSet<WorkspaceEntity> Workspaces { get; set; } = default!;

	public DbSet<WorkspaceVersionEntity> WorkspaceVersions { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<WorkspaceEntity>(options =>
		{
			options.OwnsOne(m => m.Value);

			options.HasKey(m => m.Id);
		});

		builder.Entity<WorkspaceVersionEntity>(options =>
		{
			options.OwnsOne(m => m.Value);

			options.HasKey(m => m.Version);
			options.HasIndex(nameof(WorkspaceVersionEntity.Id), nameof(WorkspaceVersionEntity.Version)).IsUnique();
		});
	}
}
