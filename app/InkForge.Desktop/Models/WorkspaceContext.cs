namespace InkForge.Desktop.Models
{
	public interface IWorkspaceContext
	{
		Workspace? Workspace { get; set; }
	}

	public class WorkspaceContext : IWorkspaceContext
	{
		public Workspace? Workspace { get; set; }
	}
}
