# Katro

An operating system made in [Cosmos gen3][cosmos-gen3]

## Features

- FAT32 filesystem (You can disable it by booting into "Katro (No file system)" in the boot menu)
- Lua script execution (powered by [Cosmos.Executable.Lua][cosmos-lua]) with some of the Katro functions, [Supported functions](#katro-lua-functions)

## Katro Lua Functions

Despite Katro being able to run Lua scripts, Katro also adds some functions which allow for further interaction with Katro.
They are usually prefixed with `katro_`, e.g., `katro_log`

Here are all of them:

- `katro_log("level", "message")` - Logs a message to Katro

The first parameter ("level") can only be one of those:
| Level | Aliases | Description |
|---|---|---|
| `info` | `information` | Logs an informational message |
| `ok` | `success` | Logs a successful message |
| `warn` | `warning` | Logs a warning |
| `error` | None | Logs an error |

[cosmos-lua]: https://github.com/CosmosOS/Cosmos.Executable.Lua
[cosmos-gen3]: https://gocosmos.org