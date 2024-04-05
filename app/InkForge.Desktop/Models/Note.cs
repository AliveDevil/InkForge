using System.Reactive.Linq;
using System.Reactive.Subjects;

using ReactiveUI;

namespace InkForge.Desktop.Models;

public class Note : ReactiveObject
{
	private readonly ObservableAsPropertyHelper<Note?> _parent;
	private readonly BehaviorSubject<int?> _parentId = new(default);
	private DateTimeOffset _createdTime;
	private int _id;
	private string _name = default!;
	private DateTimeOffset _updatedTime;

	public DateTimeOffset CreatedTime
	{
		get => _createdTime;
		set => this.RaiseAndSetIfChanged(ref _createdTime, value);
	}

	public int Id
	{
		get => _id;
		set => this.RaiseAndSetIfChanged(ref _id, value);
	}

	public string Name
	{
		get => _name;
		set => this.RaiseAndSetIfChanged(ref _name, value);
	}

	public DateTimeOffset UpdatedTime
	{
		get => _updatedTime;
		set => this.RaiseAndSetIfChanged(ref _updatedTime, value);
	}

	public Note? Parent
	{
		get => _parent.Value;
		set => ParentId = value?.Id;
	}

	public int? ParentId
	{
		get => _parentId.Value;
		set => _parentId.OnNext(value);
	}

	public Note(NoteStore noteStore)
	{
		_parent = _parentId.Select(id => id switch
		{
			{ } => noteStore.Watch((int)id),
			_ => Observable.Empty<Note?>(),
		}).Switch(null).ToProperty(this, nameof(Parent), deferSubscription: true);
	}
}
