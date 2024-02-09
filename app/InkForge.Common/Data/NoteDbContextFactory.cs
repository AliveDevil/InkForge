using InkForge.Data;

using Microsoft.EntityFrameworkCore;

namespace InkForge.Common.Data;

public class NoteDbContextFactory : IDbContextFactory<NoteDbContext>
{

	public NoteDbContext CreateDbContext()
	{
		return new NoteDbContext(null);
	}

}
