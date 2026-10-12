local commands = require("shell.commands")
local tokenizer = require("tokenizer")

local function print_prompt()
	katro_set_color("cyan")
	katro_write("Katro ")

	katro_reset_color()
	katro_write("[ ")

	local cwd = katro_get_cwd()

	katro_set_color("yellow")
	katro_write(cwd)

	katro_reset_color()
	katro_write(" ] ")

	katro_write(":> ")
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
			katro_writeln("katro: command \"" .. name .. "\" not found")
		end
	end
end