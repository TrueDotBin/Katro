# Katro Lua

Katro allows users to run Lua 5.5 scripts with some Katro functions built in.  
Katro Lua can also be called **KTLua**.

## What is under the hood?

Under the hood, Katro uses the [Cosmos.Executable.Lua][cosmos-lua] library to run Lua scripts.

## Quick start

```lua
-- No need to require("katro"), Katro registers the functions globally.

-- Logs a message to Katro
katro_log("info", "Hello, Lua!")
```

## Unsupported functions

As stated by [Cosmos.Executable.Lua][cosmos-lua]'s README:
- `io.popen` is not supported due to Cosmos not having a processing system.
- `os.tmpname` will fail because Cosmos has no `/tmp` yet.  
Once Katro expands, it may be replaced with `katro_tmpname()` (Actual function name can change)
- `os.getenv` returns nil due to the kernel not having environment variables.

## KTLua Functions

Here is a list of all available KTLua functions:

| Function | Description | Example Usage |
|----------|-------------|---------------|
| `katro_log(level, message)` | Logs a message to Katro, [invalid log levels will throw an error](#valid-log-levels) | `katro_log("info", "Informational message")` |         

## Valid Log Levels

The `katro_log` function is strict, and **will** throw an error if the first parameter doesn't match the following:

| Level | Aliases | Meaning |
|-------|---------|---------|
| `information` | `info` | Logs an informational message
| `success` | `ok` | Logs a successful message
| `warn` | `warning` | Logs a warning
| `error` | - | Logs an error

[cosmos-lua]: https://github.com/CosmosOS/Cosmos.Executable.Lua