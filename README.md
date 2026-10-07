# Katro

A kernel made in [Cosmos gen3][cosmos-gen3]

## Features

- FAT32 filesystem (You can disable it by booting into "Katro (No file system)" in the boot menu)
- Lua script execution (powered by [Cosmos.Executable.Lua][cosmos-lua]) with some of the Katro functions, [Supported functions](#katro-lua-functions)

## Roadmap

- [x] Implement file system
- [x] Add Lua script execution
- [ ] Add networking
- [ ] Add more Katro functions to Lua
- [ ] Add a desktop and some apps

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

## Building

Requires the Cosmos dev kit and .NET.  
**Install Cosmos [here][cosmos-install].**

Create a disk image using `qemu-img`

```bash
# 64MB disk image
qemu-img create katro.img 64M
```

Build and run the kernel:

```bash
cosmos build
cosmos run --disk katro.img
```

Or, without a filesystem:

```bash
cosmos run
# pick "Katro (No file system)" in the boot menu
```

Licensed under BSD-3-Clause, see the [LICENSE](LICENSE) file for more details.

[cosmos-lua]: https://github.com/CosmosOS/Cosmos.Executable.Lua
[cosmos-gen3]: https://gocosmos.org
[cosmos-install]: https://cosmosos.github.io/articles/user/install.html