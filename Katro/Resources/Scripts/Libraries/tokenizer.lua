local M = {}

function M.tokenize(input)
    local args = {}
    local current = {}
    local in_quote = false
    local escaped = false

    for i = 1, #input do
        local c = input:sub(i, i)

        if escaped then
            current[#current + 1] = c
            escaped = false
        elseif c == "\\" then
            escaped = true
        elseif c == '"' then
            in_quote = not in_quote
        elseif (c == " " or c == "\t") and not in_quote then
            if #current > 0 then
                args[#args + 1] = table.concat(current)
                current = {}
            end
        else
            current[#current + 1] = c
        end
    end

    if #current > 0 then
        args[#args + 1] = table.concat(current)
    end

    return args
end

return M