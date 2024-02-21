namespace InkForge.Desktop.Models
{
	public interface IWorkspaceAccessor
	{
		Workspace? Workspace { get; set; }
	}

	public class WorkspaceAccessor : IWorkspaceAccessor
	{
		public Workspace? Workspace { get; set; }
	}
}
