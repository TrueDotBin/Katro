# KTLang

**KTLang** is a programming language designed for Katro.

KTLang is an interpreted language, meaning it does not compile to a native executable (e.g., `.exe` or `.elf`)

## File Extension

KTLang uses the `.ktl` extension.

## Planned Syntax

Here is an example using KTLang:

```ktlang
> This is a comment, it is ignored by the interpreter.

var a = 5;
println(a);

if 2 + 2 == 4
{
    println("Math is working correctly!");
}
else
{
    println("Math is NOT working correctly :(");
}
```

## Roadmap

- [x] Lexer/Tokenizer
- [x] Parser/AST Generator
- [ ] Interpreter
- [ ] Variables
- [ ] Control Flow (if, else)