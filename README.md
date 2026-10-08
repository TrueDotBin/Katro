# Katro

> [!WARNING]
> Please do ***not*** try this on actual hardware! It may cause **IRREPARABLE DAMAGE** to your data. Use a virtual machine instead!

A kernel made in [Cosmos gen3][cosmos-gen3]

## Features

- FAT32 filesystem (You can disable it by booting into "Katro (No file system)" in the boot menu)
- Lua script execution (powered by [Cosmos.Executable.Lua][cosmos-lua]) with some of the Katro functions, [Read more](docs/lua/README.md)

## Roadmap

- [x] Implement file system
- [x] Add Lua script execution
- [ ] Implement command-line shell
- [ ] Add networking
- [ ] Add more Katro functions to Lua
- [ ] Add a desktop and some apps

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


[cosmos-gen3]: https://gocosmos.org
[cosmos-install]: https://cosmosos.github.io/articles/user/install.html