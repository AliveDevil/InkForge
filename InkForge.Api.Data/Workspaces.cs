using InkForge.Data;

using Microsoft.AspNetCore.Identity;

namespace InkForge.Api.Data
{
	public class Workspace
	{
		public DateTimeOffset Created { get; set; }

		public DateTimeOffset? Deleted { get; set; }

		public string Name { get; set; } = default!;

		public IdentityUser Owner { get; set; } = default!;

		public DateTimeOffset Updated { get; set; }
	}

	public class WorkspaceEntity : Entity<Workspace, int>;
}
