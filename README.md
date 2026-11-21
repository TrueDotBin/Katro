# Katro

> [!WARNING]
> Please do ***not*** try this on actual hardware! It may cause **IRREPARABLE DAMAGE** to your data. Use a virtual machine instead!

A [monolithic kernel][monolithic-kernel] made in [Cosmos gen3][cosmos-gen3]

## Features

- A command-line shell
- Support for FAT and ext2 file systems
- A service host
- A user system

## Roadmap

- [x] Add a command-line shell
- [x] Implement file system
- [x] Add a user system
- [ ] Add a way to run programs (Lua scripts, custom shell scripts, etc.)
- [ ] Add basic networking
- [ ] Add a desktop interface
  
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

---

*Licensed under BSD-3-Clause, see the [LICENSE](LICENSE) file for more details.*

[cosmos-gen3]: https://gocosmos.org
[cosmos-install]: https://cosmosos.github.io/articles/user/install.html
[monolithic-kernel]: https://en.wikipedia.org/wiki/Monolithic_kernel