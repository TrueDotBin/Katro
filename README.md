# Katro

> [!WARNING]
> Please do ***not*** try this on actual hardware! It may cause **IRREPARABLE DAMAGE** to your data. Use a virtual machine instead!

A kernel made in [Cosmos gen3][cosmos-gen3]

## Features

- FAT32 filesystem
- Lua script execution, [read more](docs/lua/README.md)
- A shell written in Lua called "Katro Shell" (ktsh)

## Roadmap

- [x] Implement file system
- [x] Add Lua script execution
- [x] Implement command-line shell
- [ ] Add networking
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

If you want to run Katro's tests as well:
```bash
cosmos run --disk katro.img
# Select "Katro (Run Tests)" from the boot meny
```

Licensed under BSD-3-Clause, see the [LICENSE](LICENSE) file for more details.


[cosmos-gen3]: https://gocosmos.org
[cosmos-install]: https://cosmosos.github.io/articles/user/install.html