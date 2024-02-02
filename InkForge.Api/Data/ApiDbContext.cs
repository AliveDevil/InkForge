using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace InkForge.Api.Data;

public class ApiDbcontext(
	DbContextOptions options,
	IOptions<OperationalStoreOptions> operationalStoreOptions
) : ApiAuthorizationDbContext<IdentityUser>(options, operationalStoreOptions)
{
}
