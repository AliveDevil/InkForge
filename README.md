# InkForge

## Modifying EF Model

Apply Migrations for Notes-Database (`InkForge.Data`) or Identity-Database (including Workspaces, `InkForge.Api.Data`)

**InkForge.Data**:<br>
```
dotnet ef migrations add <Name> \
    --startup-project design/InkForge.Migrations/ \
    --project shared/migrations/InkForge.Sqlite/ \
    -c InkForge.Data.NoteDbContext -- \
    --DbProvider Sqlite --connectionstrings-defaultconnection "Data Source=:memory:"
```

**InkForge.Api.Data**:<br>
```
dotnet ef migrations add <Name> \
    --startup-project design/InkForge.Migrations/ \
    --project migrations/InkForge.Api.Sqlite \
    -c InkForge.Api.Data.ApiDbContext -- \
    --DbProvider Sqlite --connectionstrings-defaultconnection "Data Source=:memory:"
```
