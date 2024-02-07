using InkForge.Data.Domain;

namespace InkForge.Data.Infrastructure
{
	public class NoteEntity : Entity<Note, int>;

	public class NoteVersionEntity : VersionedEntity<Note, int>;
}
