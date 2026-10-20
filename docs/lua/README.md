# Katro Lua

Katro allows users to run Lua 5.5 scripts with some Katro functions built in.  
Katro Lua can also be called **KTLua**.

## What is under the hood?

Under the hood, Katro uses the [Cosmos.Executable.Lua][cosmos-lua] library to run Lua scripts.

## Quick start

```lua
-- Katro registers the functions globally.

-- Logs a message to Katro
katro_log("info", "Hello, Lua!")
```

If you want a list of all functions that Katro registers, see [this file](functions.md)

## Unsupported functions

As stated by [Cosmos.Executable.Lua][cosmos-lua]'s README:
- `io.popen` is not supported due to Cosmos not having a processing system.
- `os.tmpname` will fail because Cosmos has no `/tmp` yet.
- `os.getenv` returns nil due to the kernel not having environment variables.

[cosmos-lua]: https://github.com/CosmosOS/Cosmos.Executable.Lua