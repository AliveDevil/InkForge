using InkForge.Data.Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace InkForge.Data;

public class NoteDbContext(
	DbContextOptions<NoteDbContext> options
) : DbContext(options)
{
	public DbSet<Blob> Blobs { get; set; } = default!;

	public DbSet<MetadataEntity> Metadata { get; set; } = default!;

	public DbSet<MetadataVersionEntity> MetadataHistory { get; set; } = default!;

	public DbSet<NoteEntity> Notes { get; set; } = default!;

	public DbSet<NoteVersionEntity> NoteVersions { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<MetadataEntity>(options =>
		{
			options.HasKey(m => m.Id);
		});

		modelBuilder.Entity<MetadataVersionEntity>(options =>
		{
			options.Property(m => m.Id).IsRequired();
			options.HasKey(m => m.Version);
			options.HasIndex(nameof(MetadataVersionEntity.Id), nameof(MetadataVersionEntity.Version)).IsUnique();
		});

		modelBuilder.Entity<NoteEntity>(options =>
		{
			options.OwnsOne(m => m.Value);

			options.HasKey(m => m.Id);
		});

		modelBuilder.Entity<NoteVersionEntity>(options =>
		{
			options.OwnsOne(m => m.Value);
			options.Property(m => m.Id).IsRequired();
			options.HasKey(m => m.Version);
			options.HasIndex(nameof(NoteVersionEntity.Id), nameof(NoteVersionEntity.Version)).IsUnique();
		});
	}
}
