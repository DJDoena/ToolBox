# DoenaSoft.ToolBox

[![NuGet](https://img.shields.io/nuget/v/DoenaSoft.ToolBox.svg)](https://www.nuget.org/packages/DoenaSoft.ToolBox/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A collection of useful utility classes and extension methods for .NET, focusing on XML serialization, LINQ extensions, and string manipulation.

## Features

### Generic XML Serialization
- **XmlSerializer\<T\>**: Type-safe generic XML serializer with simplified API
- **XsltSerializer**: XML serialization with XSLT transformation support
- Support for custom serialization providers through `IXsltSerializerDataProvider`

### LINQ Extensions
Powerful extension methods for `IEnumerable<T>`:
- **ForEach**: Execute an action on each item in a collection
- **Split**: Split collections based on predicates
- **TryCast**: Safe type casting with filtering
- **CountAtLeast/CountAtMost**: Efficient count checking without full enumeration

### Brace Splitters
Advanced string parsing utilities:
- **BraceSplitter**: Parse and split strings based on matching braces
- Support for parentheses `()`, brackets `[]`, curly braces `{}`, and chevrons `<>`
- Nested brace handling
- Extract text segments and brace segments separately

### String & Object Extensions
- String manipulation utilities
- Object extension methods

## Installation

Install via NuGet Package Manager:

```powershell
Install-Package DoenaSoft.ToolBox
```

Or via .NET CLI:

```bash
dotnet add package DoenaSoft.ToolBox
```

## Quick Start

### XML Serialization

```csharp
using DoenaSoft.ToolBox.Generics;

// Serialize
var myObject = new MyClass { Name = "Example" };
XmlSerializer<MyClass>.Serialize("output.xml", myObject);

// Deserialize
var loaded = XmlSerializer<MyClass>.Deserialize("output.xml");
```

### LINQ Extensions

```csharp
using DoenaSoft.ToolBox.Extensions;

// ForEach
myList.ForEach(item => Console.WriteLine(item));

// Efficient count checking
if (myCollection.CountAtLeast(10))
{
    // More efficient than Count() >= 10 for large collections
}

// Safe casting
var strings = mixedList.TryCast<string>();
```

### Brace Splitting

```csharp
using DoenaSoft.ToolBox.BraceSplitters;

var splitter = new BraceSplitter(
    searchForParenthesis: true,
    searchForBrackets: true,
    searchForCurlyBraces: true,
    searchForChevrons: true
);

var segments = splitter.Split("Hello (world) and [test]");
// Returns segments with text and brace information
```

## Target Frameworks

- .NET Standard 2.0
- .NET Framework 4.7.2
- .NET 10.0

## License

This project is licensed under the MIT License.

## Author

**DJ Doena** - [Doena Soft.](https://github.com/DJDoena)

## Links

- [GitHub Repository](https://github.com/DJDoena/ToolBox)
- [NuGet Package](https://www.nuget.org/packages/DoenaSoft.ToolBox/)
- [Report Issues](https://github.com/DJDoena/ToolBox/issues)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
