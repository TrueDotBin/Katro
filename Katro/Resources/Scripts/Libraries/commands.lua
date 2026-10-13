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

return M