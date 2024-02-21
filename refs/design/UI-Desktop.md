# UI Desktop

## Local-Mode
Uses same data backend as Sync (Sqlite)

## Online-Mode
Performs OpenAPI calls to Sync server.

*Consideration*: Allow for syncing to local backend.

## Technical

Figure out a way to get navigation/commands relative to the window they are in.<br>
I.e. make Windows scoped, then get a shell-object in each Window, which then
consumes a Menu-service, which navigates the shell-object tree to find eligible
menu-objects to present.

## Research
