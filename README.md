# **Important: The ```Ignite.Console``` class requires the ```Ignite.Convert``` class from https://github.com/RX-J/Ignite.Convert/blob/main/Convert.cs**

To print a value to the console, call the ```Print``` function:

```cs
Ignite.Console.Print ("Hello World!"); // Writes "Hello World!" to the console
```

### The Print, Printl, and Scan Functions Support These Tags:

| Tag                   |  Explanation                                  | Reverse Tag            |
|-----------------------|-----------------------------------------------|------------------------|
| ```<bold>```          | Writes text in **bold** letters               | ```</bold>```          |
| ```<faint>```         | Writes fainted letters                        | ```</faint>```         |
| ```<italic>```        | Writes text in *italic* letters               | ```</italic>```        |
| ```<underline>```     | Writes <u>underlined</u> text                 | ```</underline>```     |
| ```<blinking>```      | Writes blinking text                          | ```</blinking>```      |
| ```<reverse>```       | Reverses the foreground and background colors | ```</reverse>```       |
| ```<invisible>```     | Writes the text invisible to the console      | ```</invisible>```     |
| ```<strikethrough>``` | Writes text in ~~strikethrough~~ letters      | ```</strikethrough>``` |

### You Can Also Change the Foreground and Background Colors:

| Tag                   |  Explanation                   | Reverse Tag         |
|-----------------------|--------------------------------|---------------------|
| ```<foreground-red>```| Creates a red foreground color | ```</foreground>``` |
| ```<background-red>```| Creates a red background color | ```</background>``` |

**Important: The reverse tags always reset the foreground or background color, ```no``` ```</foreground-red>``` ```or``` ```</background-red>``` are necessary.**

**The following <u>foreground</u> colors are supported by default:**

```<foreground-gray>```\
```<foreground-red>```\
```<foreground-green>```\
```<foreground-yellow>```\
```<foreground-blue>```\
```<foreground-magenta>```\
```<foreground-cyan>```\
```<foreground-white>```\
```<foreground-black>```

**The following <u>background</u> colors are supported by default:**

```<background-gray>```\
```<background-red>```\
```<background-green>```\
```<background-yellow>```\
```<background-blue>```\
```<background-magenta>```\
```<background-cyan>```\
```<background-white>```\
```<background-black>```

### Changing the Foreground and Background Colors Using HEX Color Codes:

```<foreground-#FFFFFF>```\
```<background-#FFFFFF>```

**Short notations are allowed:**

```<foreground-#FFF>```\
```<background-#FFF>```

# Using Extended Formatting with Positions

```cs
Ignite.Console.Print (Ignite.Console.Position.Centered, "Hello World!"); // This text will be centered in the console
```

**Supported are the following positions:**\
```Left```\
```Centered```\
```Right```\
Left is the default allingment

# Example Usage of the Print Function

```cs
Ignite.Console.Print ("This text is <bold>bold</bold> - this text <italic>italic</italic> - and this text <foreground-red>red</foreground>");
Ignite.Console.Print (Ignite.Console.Position.Centered, "This text is <bold>bold</bold> and centered");
```

# Printl and Scan Functions

```Printl``` works like the ```Console.WriteLine``` function but also supports formatting.
```Scan``` works almost like the ```Console.ReadLine``` function but supports formatting and built-in converting.

**Usage of the Printl function:**

```cs
Ignite.Console.Printl ("This text is <bold>bold</bold> - this text <italic>italic</italic> - and this text <foreground-red>red</foreground>");
// A new line will be generated between this text
Ignite.Console.Printl (Ignite.Console.Position.Centered, "This text is <bold>bold</bold> and centered");
```

```cs
var name = Ignite.Console.Scan<string> ("Please enter your name: ");
var age = Ignite.Console.Scan<int> ("Please enter your age: ");
```

# Simple New Lines

**All functions support simple new lines formatting, you can call it by using:**

```cs
Ignite.Console.Print (
    "First line",
    "Second Line",
    "Third line",
    "...");
```

**This works for the ```Print```, ```Printl```, and ```Scan``` functions, tags and position alignments are also possible:**

```cs
Ignite.Console.Print (
    Ignite.Console.Position.Centered,
    "<bold>First line</bold>",
    "<italic>Second Line</italic>",
    "<underline>Third line</underline>",
    "...");
```

**A new line will be automatically placed between each new (,).**