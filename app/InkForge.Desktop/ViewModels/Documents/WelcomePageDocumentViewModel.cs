using System.Reactive;

using Avalonia.Platform.Storage;

using Dock.Model.ReactiveUI.Controls;

using InkForge.Desktop.Managers;
using InkForge.Desktop.Services;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels.Documents;

public class WelcomePageDocumentViewModel : Document
{
	private readonly WorkspaceManager _workspaceController;

	public ReactiveCommand<Unit, Unit> CreateNew { get; }

	public ReactiveCommand<Unit, Unit> OpenNew { get; }

	public WelcomePageDocumentViewModel(WorkspaceManager workspaceController)
	{
		Title = "Welcome";
		
		_workspaceController = workspaceController;
		CreateNew = ReactiveCommand.CreateFromTask(OnCreateNew);
		OpenNew = ReactiveCommand.CreateFromTask(OnOpenNew);
	}

	private async Task OnCreateNew()
	{
		var storageProvider = this.GetStorageProvider()!;

		var documents = await storageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Documents);
		var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
		{
			DefaultExtension = ".ifdb",
			FileTypeChoices =
			[
				new FilePickerFileType("InkForge Database File")
				{
					Patterns = [ "*.ifdb" ],
				},
			],
			SuggestedStartLocation = documents,
			Title = "Select InkForge Database Name",
		});

		if (file?.TryGetLocalPath() is not { } filePath)
		{
			return;
		}

		await _workspaceController.OpenWorkspace(filePath, true);
	}

	private async Task OnOpenNew()
	{
		var storageProvider = this.GetStorageProvider()!;

		var documents = await storageProvider.TryGetWellKnownFolderAsync(WellKnownFolder.Documents);
		var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
		{
			AllowMultiple = false,
			SuggestedStartLocation = documents,
			FileTypeFilter =
			[
				new FilePickerFileType("InkForge Database File")
				{
					Patterns = [ "*.ifdb" ]
				}
			],
			Title = "Open InkForge Database file"
		});

		if (files.Count != 1)
		{
			return;
		}

		if (files[0].TryGetLocalPath() is not { } filePath)
		{
			return;
		}

		await _workspaceController.OpenWorkspace(filePath, false);
	}
}
