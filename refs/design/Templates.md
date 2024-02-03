# Templates

Templates are copied to a note, making them completely editable.

*Consideration*: Inherit a Template, this will make only `{{Variable}}`-formatted content blocks editable. <- Predefined Form

## Metadata
````
---
Template: Yes
---
# {{Title}}

Content
````

## Built-In
**Book** - Form

Editable Text Content with default-content
````
---
Template: Form
---
# {{Book Name}}

{{Content multiline}}

:::query format=list
note.parents include this
:::
````
