# Notes

Notes are plain-text content, written in MarkDown.

Metadata is supported in FrontMatter.

## Versions
Note content is put verbatim in the data store.

The client is responsible for building a patch that can be
applied to the latest version of a note.

The serving instance is able to produce a patch from any
revision to the latest revision, in order for a client to
sync up to the current state.
