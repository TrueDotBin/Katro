local M = {}

function M.echo(args)
	local text = table.concat(args, " ", 2)
	katro_writeln(text)
end

function M.pwd()
	local cwd = katro_get_cwd()
	katro_writeln(cwd)
end

return M