local M = {}

function M.echo(args)
	local text = table.concat(args, " ", 2)
	katro_writeln(text)
end

function M.pwd()
	local cwd = katro_get_cwd()
	katro_writeln(cwd)
end

function M.cd(args)
	local path = args[2]

	if not path then
		katro_writeln("cd: no arguments", "red")
		return
	end

	local success = katro_chdir(path)

	if not success then
		katro_writeln("cd: no such directory: " .. path, "red")
	end
end

function M.ls(args)
	local path = args[2]

	if not path then
		path = katro_get_cwd()
	end

	if not katro_dir_exists(path) then
		katro_writeln("ls: no such directory: " .. path, "red")
		return
	end

	local dirs = katro_get_dirs(path)
	local files = katro_get_files(path)

	for i = 1, #dirs do
		katro_write("[DIR] ", "darkgray")

		local dir = dirs[i]
		local name = katro_get_file_name(dir)

		katro_writeln(name)
	end

	for i = 1, #files do
		katro_write("      ")

		local file = files[i]
		local name = katro_get_file_name(file)

		katro_writeln(name)
	end
end

return M