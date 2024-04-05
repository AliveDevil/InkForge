namespace InkForge.Data
{
    public class Note
    {
        public DateTimeOffset Created { get; set; }

        public string Name { get; set; } = default!;

        public DateTimeOffset Updated { get; set; }

        public DateTimeOffset? Deleted { get; set; }

        public Blob Content { get; set; } = default!;
    }

	public class NoteEntity : Entity<NoteEntity, Note, int>;
}
