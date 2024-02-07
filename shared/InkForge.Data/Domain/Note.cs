using InkForge.Data.Infrastructure;

namespace InkForge.Data.Domain;

public class Note
{
	public DateTimeOffset Created { get; set; }

	public NoteEntity? Parent { get; set; }

	public string Name { get; set; } = default!;

	public DateTimeOffset Updated { get; set; }

	public DateTimeOffset? Deleted { get; set; }

	public Blob Content { get; set; } = default!;
}
