# AutoDisposer

This is an evil library that automatically disposes of objects marked with a specific attribute. Of course, **you should dispose objects on your own responsibility**, but if you are lazy, you may feel this library useful.

## Features
- Automatically disposes of properties and fields which implements IDisposable and marked with the `OwnedAttribute`.
- Supports both reference types and Nullable value types.
- Sets reference types or Nullable types to null after disposing them to help with garbage collection.

## Usage
1. Mark properties or fields that you want to be automatically disposed with the `OwnedAttribute`.

```csharp
using OaktreeLab.Utils.Common;

public class MyClass {
    [Owned]
    public MemoryStream Stream { get; set; }

    [Owned]
    public SqlConnection? Connection { get; set; }
}
```
2. Call `AutoDisposer.Dispose(this)` in your class when you want to dispose of the owned objects.

```csharp
using OaktreeLab.Utils.Common;

public class MyClass {
    [Owned]
    public MemoryStream Stream { get; set; }

    [Owned]
    public SqlConnection? Connection { get; set; }

    public void DisposeOwnedObjects() {
        AutoDisposer.Dispose(this);
    }
}
```

## Note
- This library uses reflection, so it may have performance implications. Use it judiciously.
- This library only disposes of properties and fields marked with the `OwnedAttribute`. Make sure to mark all relevant members.
- Use this library at your own risk. It is recommended to understand the implications of automatic disposal before using it in production code.
- Anyway, please dispose of objects on your own responsibility!