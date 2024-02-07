using Microsoft.AspNetCore.Identity;

namespace InkForge.Api.Data.Domain;

public class Workspace
{
	public string Name { get; set; } = default!;

	public DateTimeOffset Created { get; set; }

	public IdentityUser Owner { get; set; } = default!;

	public DateTimeOffset Updated { get; set; }

	public DateTimeOffset? Deleted { get; set; }
}
