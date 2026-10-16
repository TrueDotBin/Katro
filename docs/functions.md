# Katro System Library

Global Lua functions provided by Katro for working with the filesystem, console input/output and general information. All paths are unix-like (e.g. `/home/me/notes.txt`).

## Contents

**Filesystem**

- [`katro_chdir`](#katro_chdir)
- [`katro_get_cwd`](#katro_get_cwd)
- [`katro_get_dirs`](#katro_get_dirs)
- [`katro_get_files`](#katro_get_files)
- [`katro_file_exists`](#katro_file_exists)
- [`katro_dir_exists`](#katro_dir_exists)
- [`katro_get_file_name`](#katro_get_file_name)
- [`katro_get_file_name_without_ext`](#katro_get_file_name_without_ext)

**Output**

- [`katro_log`](#katro_log)
- [`katro_set_color`](#katro_set_color)
- [`katro_reset_color`](#katro_reset_color)
- [`katro_write`](#katro_write)
- [`katro_writeln`](#katro_writeln)

**Input**

- [`katro_read_string`](#katro_read_string)

**Other**

- [`katro_get_version`](#katro_get_version)

---

## katro_chdir

`katro_chdir(dir)`

Changes the current working directory. Returns `true` if the directory exists and the change succeeded, or `false` if the directory does not exist (the working directory is left unchanged).

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `dir` | `string` | Yes | Path of the directory to switch to. Can be absolute or relative to the current working directory. |

### Returns

`boolean` - `true` on success, `false` if the directory does not exist.

### Example

```lua
if katro_chdir("projects/my_app") then
    katro_log("success", "Now in " .. katro_get_cwd())
else
    katro_log("error", "Directory does not exist!")
end
```

---

## katro_get_cwd

`katro_get_cwd()`

Gets the current working directory.

### Parameters

This function takes no parameters.

### Returns

`string` - the full path of the current working directory.

### Example

```lua
local cwd = katro_get_cwd()
katro_writeln("Working directory: " .. cwd)
```

---

## katro_get_dirs

`katro_get_dirs([path])`

Gets a list of all directories inside a directory. If no path is given, the current working directory is used.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | No | Directory to list. Defaults to the current working directory. |

### Returns

`table` - an array of directory paths (strings).

### Notes

- Only direct subdirectories are returned; the search is not recursive.

### Example

```lua
for _, dir in ipairs(katro_get_dirs()) do
    katro_writeln(katro_get_file_name(dir))
end

-- list the directories of a specific path
local dirs = katro_get_dirs("/home")
```

---

## katro_get_files

`katro_get_files([path])`

Gets a list of all files inside a directory. If no path is given, the current working directory is used.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | No | Directory to list. Defaults to the current working directory. |

### Returns

`table` - an array of file paths (strings).

### Notes

- Only files directly inside the directory are returned; the search is not recursive.

### Example

```lua
for _, file in ipairs(katro_get_files()) do
    katro_writeln(katro_get_file_name(file))
end

-- list the files of a specific path
local files = katro_get_files("/home/me/documents")
```

---

## katro_file_exists

`katro_file_exists(path)`

Determines whether a file exists.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | Yes | Path of the file to check. |

### Returns

`boolean` - `true` if the file exists, otherwise `false`.

### Notes

- Returns `false` for directories. Use `katro_dir_exists` to check for a directory.

### Example

```lua
if katro_file_exists("notes.txt") then
    katro_writeln("notes.txt found!")
else
    katro_writeln("notes.txt not found.", "red")
end
```

---

## katro_dir_exists

`katro_dir_exists(path)`

Determines whether a directory exists.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | Yes | Path of the directory to check. |

### Returns

`boolean` - `true` if the directory exists, otherwise `false`.

### Notes

- Returns `false` for files. Use `katro_file_exists` to check for a file.

### Example

```lua
if not katro_dir_exists("output") then
    katro_log("warn", "The output directory is missing")
end
```

---

## katro_get_file_name

`katro_get_file_name(path)`

Gets the file name of a path, including the extension.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | Yes | The path to get the file name from. |

### Returns

`string` - the file name with its extension.

### Example

```lua
local name = katro_get_file_name("/home/me/notes.txt")
katro_writeln(name) -- notes.txt
```

---

## katro_get_file_name_without_ext

`katro_get_file_name_without_ext(path)`

Gets the file name of a path without the extension.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `path` | `string` | Yes | The path to get the file name from. |

### Returns

`string` - the file name without its extension.

### Example

```lua
local name = katro_get_file_name_without_ext("/home/me/notes.txt")
katro_writeln(name) -- notes
```

---

## katro_log

`katro_log(level, message)`

Logs a message to Katro using its built-in logger. The level controls how the message is displayed.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `level` | `string` | Yes | The log level. One of `info` (or `information`), `warn` (or `warning`), `success` (or `ok`), or `error`. Case-insensitive, and surrounding whitespace is ignored. |
| `message` | `any` | Yes | The message to log. Non-string values are converted to a string. |

### Returns

Nothing.

### Notes

- Raises a Lua error (`invalid log level: '<level>'`) if the level is not one of the values listed above.

### Example

```lua
katro_log("info", "Starting script...")
katro_log("warn", "Config file not found, using defaults")
katro_log("success", "Done!")
katro_log("error", "Something went wrong")
```

---

## katro_set_color

`katro_set_color(foreground, [background])`

Sets the console foreground and background colors. The colors stay in effect until you change them again or call `katro_reset_color`.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `foreground` | `string` | Yes | Name of the foreground color (case-insensitive). |
| `background` | `string` | No | Name of the background color (case-insensitive). Defaults to `"black"`. |

### Returns

Nothing.

### Notes

- Valid color names: `Black`, `DarkBlue`, `DarkGreen`, `DarkCyan`, `DarkRed`, `DarkMagenta`, `DarkYellow`, `Gray`, `DarkGray`, `Blue`, `Green`, `Cyan`, `Red`, `Magenta`, `Yellow`, `White`.
- Raises a Lua error (`invalid foreground color` / `invalid background color`) if a color name is not valid.
- Unlike `katro_write` and `katro_writeln`, this does **not** reset the colors afterwards. Call `katro_reset_color` when you're done.

### Example

```lua
katro_set_color("yellow", "darkblue")
katro_writeln("Yellow text on a dark blue background")
katro_reset_color()
```

---

## katro_reset_color

`katro_reset_color()`

Resets the console foreground and background colors to their defaults.

### Parameters

This function takes no parameters.

### Returns

Nothing.

### Example

```lua
katro_set_color("red")
katro_writeln("This is red")
katro_reset_color()
katro_writeln("This uses the default colors")
```

---

## katro_write

`katro_write([text], [foreground], [background])`

Writes text to the console **without** a trailing new line. The colors are applied only to this text and are reset afterwards.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `text` | `string` | No | The text to write. Defaults to an empty string. |
| `foreground` | `string` | No | Name of the foreground color (case-insensitive). Defaults to `"white"`. |
| `background` | `string` | No | Name of the background color (case-insensitive). Defaults to `"black"`. |

### Returns

Nothing.

### Notes

- Valid color names: `Black`, `DarkBlue`, `DarkGreen`, `DarkCyan`, `DarkRed`, `DarkMagenta`, `DarkYellow`, `Gray`, `DarkGray`, `Blue`, `Green`, `Cyan`, `Red`, `Magenta`, `Yellow`, `White`.
- Raises a Lua error (`invalid foreground color` / `invalid background color`) if a color name is not valid.

### Example

```lua
katro_write("Name: ", "cyan")
katro_write("Katro", "green")
katro_write("\n")
```

---

## katro_writeln

`katro_writeln([text], [foreground], [background])`

Writes text to the console **with** a trailing new line. The colors are applied only to this line and are reset afterwards.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `text` | `string` | No | The text to write. Defaults to an empty string (prints a blank line). |
| `foreground` | `string` | No | Name of the foreground color (case-insensitive). Defaults to `"white"`. |
| `background` | `string` | No | Name of the background color (case-insensitive). Defaults to `"black"`. |

### Returns

Nothing.

### Notes

- Valid color names: `Black`, `DarkBlue`, `DarkGreen`, `DarkCyan`, `DarkRed`, `DarkMagenta`, `DarkYellow`, `Gray`, `DarkGray`, `Blue`, `Green`, `Cyan`, `Red`, `Magenta`, `Yellow`, `White`.
- Raises a Lua error (`invalid foreground color` / `invalid background color`) if a color name is not valid.

### Example

```lua
katro_writeln("Hello, world!")
katro_writeln("Warning!", "black", "yellow")
katro_writeln() -- blank line
```

---

## katro_read_string

`katro_read_string([prompt])`

Reads a line of input from the user. Use this instead of `io.read()`.

### Parameters

| Parameter | Type | Required | Description |
| --- | --- | --- | --- |
| `prompt` | `string` | No | Text printed before waiting for input. Defaults to an empty string. |

### Returns

`string` - the line the user typed. Returns an empty string if no input is available.

### Example

```lua
local name = katro_read_string("What's your name? ")
katro_writeln("Hello, " .. name .. "!", "green")
```

---

## katro_get_version

`katro_get_version()`

Gets the version of Katro.

### Parameters

This function takes no parameters.

### Returns

`string` - the Katro version.

### Example

```lua
katro_writeln("Running Katro " .. katro_get_version())
```