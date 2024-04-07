# Operation Results

Libreria liviana y libre de dependencias para implementar el patrón de resultados en proyectos de .NET 6 y 8, fue construida para resolver requerimientos muy especificos.

## ¿Cómo empezar?

### Crear un resultado exitoso

Se pueden crear resultados de exito utilizando el método `Success`

```csharp
var success = OperationResult.Success();
var successWithMessage = OperationResult.Success("Más información sobre el proceso.");

// Agregar un valor al resultado
var user = new User { Name = "John", LastName = "Doe" };

var successWithValue = OperationResult.Success<User>(user);
var successWithValueAndMessage = OperationResult.Success<User>(user, "Usuario recuperado con éxito");
```

### Crear un resultado fallido

Los resultados de fallo se crean utilizando el método `Error`

```csharp
var errorWithMessage = OperationResult.Error("Más información sobre el error.");
var errorWithException = OperationResult.Error(new Exception("Detalles sobre la excepción"));
var errorWithCustomError = OperationResult.Error(new NotValidError());
var errorWithListCustomError = OperationResult.Error(new List<IError> { new NotValidError(), ... });
```

Puede crear errores personalizados heredando la clase `Error`

```csharp
class NotValidError : Error
{
    public NotValidError() : base("Esta operación no es válida",  "Detalles más informativos sobre la operación") { }
}
```

### Intentar ejecutar una operación y devolver un resultado

Para manejar operaciones y devolver un resultado dependiendo de como finalice la misma, puede utilizar el método `Try`

```csharp
var tryAction = OperationResult.Try(() =>
{
    Console.WriteLine("Hola Mundo!");
});

//En caso de error la operación se ejecutara las veces que sean
//establecidas en la propiedad MaxRetries del objeto RetryOptions.
var retryOptions = new RetryOptions(3, TimeSpan.FromSeconds(3));
var tryActionAndReturnValue = await OperationResult.TryAsync(() =>
{
    return Task.FromResult(new FakePersonEntity("John", "Doe"));
}, retryOptions);
```

### Comprobar el estado de un resultado

Existen 2 propiedades dentro de un resultado que pueden ser consultadas para verificar el estado de un resultado: `IsSuccess` y `IsFailure`, tambien puede obtener en valor con la propiedad `Value` en caso de corresponder.

```csharp
if (errorWithMessage.IsSuccess)
{
    // El resultado fue éxitoso
}

if (errorWithMessage.IsFailure)
{
    // El resultado no es éxitoso
}

var user = successWithValue.Value;
```