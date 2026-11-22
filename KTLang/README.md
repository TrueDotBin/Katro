# KTLang

**KTLang** is a programming language designed for Katro.

KTLang is an interpreted language, meaning it does not compile to a native executable (e.g., `.exe` or `.elf`)

## File Extension

KTLang uses the `.ktl` extension.

## Planned Syntax

Here is an example using KTLang:

```ktlang
> This is a comment, it is ignored by the interpreter.

fn greet(name) {
    println("Hello,", name)
}

var a = 5;
var b = 10;
var c = a + b;

println("a =", a);
println("b =", b);
println("a + b =", c);

if 2 + 2 == 4
{
    println("Math is working correctly!")
}
else
{
    println("Math is NOT working correctly :(")
}
```

## Roadmap

- [ ] Lexer/Tokenizer
- [ ] Parser/AST Generator
- [ ] Interpreter
- [ ] Variables
- [ ] Control Flow (if, else)