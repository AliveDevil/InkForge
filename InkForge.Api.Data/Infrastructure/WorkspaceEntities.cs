using InkForge.Api.Data.Domain;
using InkForge.Data;

namespace InkForge.Api.Data.Infrastructure
{
	public class WorkspaceEntity : Entity<Workspace, int>;

	public class WorkspaceVersionEntity : VersionedEntity<Workspace, int>;
}
