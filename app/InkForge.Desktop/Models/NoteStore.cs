using System.Collections.ObjectModel;
using System.Reactive.Linq;

using DynamicData;

using InkForge.Data;

using Microsoft.EntityFrameworkCore;

namespace InkForge.Desktop.Models;

public class NoteStore
{
	private readonly IDbContextFactory<NoteDbContext> _dbContextFactory;
	private readonly SourceCache<Note, int> _notesCache = new(m => m.Id);

	public ReadOnlyObservableCollection<Note> Notes { get; }

	public NoteStore(IDbContextFactory<NoteDbContext> dbContextFactory)
	{
		_dbContextFactory = dbContextFactory;
	}

	public void AddNote(Note note)
	{
		using var dbContext = _dbContextFactory.CreateDbContext();
		var entity = ToEntity(note);
		var entry = dbContext.Notes.Add(entity);
		
		dbContext.SaveChanges();
	}

	public Note CreateNote() => new(this);

	public Note? GetById(int id)
	{
		if (((Note?)_notesCache.Lookup(id)) is not Note note)
		{
			using var dbContext = _dbContextFactory.CreateDbContext();
			if (dbContext.Notes.Find(id) is not { } dbNote)
			{
				return null;
			}

			note = ToNote(dbNote);
		}

		return note;
	}

	public IObservable<Note?> Watch(int id)
	{
		return _notesCache.WatchValue(id);
	}

	private NoteEntity ToEntity(Note note)
	{
		return new()
		{
			Id = note.Id,
			Value = new()
			{
				Name = note.Name,
				Created = note.CreatedTime,
				Updated = note.UpdatedTime,
			},
			Parent = note.Parent switch
			{
				{ Id: { } parentId } => new()
				{
					Id = parentId
				},
				_ => null,
			}
		};
	}

	private Note ToNote(NoteEntity entity)
	{
		var note = CreateNote();
		note.Id = entity.Id;
		note.Name = entity.Value.Name;
		note.CreatedTime = entity.Value.Created;
		note.UpdatedTime = entity.Value.Updated;
		note.ParentId = entity.Parent?.Id;
		return note;
	}
}
