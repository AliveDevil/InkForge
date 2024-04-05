using Microsoft.EntityFrameworkCore;

namespace InkForge.Data;

public class NoteDbContext(
	DbContextOptions options
) : DbContext(options)
{
	public DbSet<Blob> Blobs { get; set; } = default!;

	public DbSet<MetadataEntity> Metadata { get; set; } = default!;

	public DbSet<NoteEntity> Notes { get; set; } = default!;

	public NoteDbContext(DbContextOptions<NoteDbContext> options) : this((DbContextOptions)options)
	{ }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<MetadataEntity>(options =>
		{
			options.HasKey(m => m.Id);
		});

		modelBuilder.Entity<NoteEntity>(options =>
		{
			options.HasKey(m => m.Id);

			options.OwnsOne(m => m.Value);

			options.HasOne(m => m.Parent);
		});
	}
}
