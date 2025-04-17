# 🧩 SanusVita.Framework.DependencyAnnotation

This package simplifies the implementation of dependency injection. It only requires adding one annotation to classes, avoiding the need to load Program or Startup at the dependency configuration level.
-----



##Configure in Program.cs

```csharp
using DependencyAnnotation;
builder.Services.AddDependencyAnnotation();
```

## Usage

1. You must add the annotation to the class with the scope type you require. The annotations are: ScopeService, TransientService, and SingletonService.

```csharp
global using SanusVita.Framework.DependencyAnnotation.DependencyAnnotation;

[ScopeService]
public class MyClass : IMyClass
{
    
}

[TransientService]
public class MyClass
{
    
}

[SingletonService]
public class MyClass
{
    
}
```

2. Now you can use your dependencies and call them in the traditional way.
```csharp
global using SanusVita.Framework.DependencyAnnotation.DependencyAnnotation;

public class YourController : ControllerBase
{
    private readonly MyClass myclass;
    private readonly IMyClass imyclass;
    
    public YourController(MyClass myclass, IMyClass imyclass)
    {
        this.myclass = myclass;
        this.imyclass = imyclass;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MyCommand command)
    {
        var response = await myclass.Create(command);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MyCommand command)
    {
        var response = await imyclass.Create(command);
        return Ok(response);
    }
}
```

If you have any suggestions, please feel free to contact me.

gfmendoza.27@outlook.com

or create an issue on GitHu
