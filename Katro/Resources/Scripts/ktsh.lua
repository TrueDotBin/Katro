local commands = require("shell.commands")
local tokenizer = require("tokenizer")

local function print_prompt()
	katro_write("Katro ", "cyan")
	katro_write("[ ")

	local cwd = katro_get_cwd()
	katro_write(cwd, "yellow")

	katro_write(" ] :> ")
end

while true do
	print_prompt()
	local input = katro_read_string()

	local tokenized = tokenizer.tokenize(input)
	local name = tokenized[1]
	
	if #tokenized > 0 then
		if commands[name] then
			local command = commands[name]
			command(tokenized)
		else
			katro_writeln("katro: command \"" .. name .. "\" not found", "red")
		end
	end
end