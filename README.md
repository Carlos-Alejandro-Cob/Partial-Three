# Partial-Three


# En este repositorio se subiran todas las actividades relacionadas con el parcial 3 de POO, matrices y la gestion de archivos de texto .txt .json y .csv 

# Estructura de los archivos de textto .JSON

Los archivos JSON (JavaScript Object Notation) son un formato de texto ligero utilizado para intercambiar datos. La estructura de un archivo JSON es bastante sencilla y se basa en dos estructuras principales:

1. Objetos: Colecciones de pares clave-valor. Un objeto JSON está rodeado por llaves {} y contiene pares de clave y valor separados por dos puntos :. Los pares están separados por comas ,.
```json
{
 "nombre": "Juan", 
 "edad": 30, 
 "ciudad": "Ciudad de México" 
}
```
2. Arreglos: Listas ordenadas de valores. Un arreglo JSON está rodeado por corchetes [] y contiene valores separados por comas ,.
```json
[
  "manzana",
"banana",
"cereza"
]
```
# Ejemplo Combinado
Un archivo JSON puede combinar objetos y arreglos para representar datos más complejos:

```json
{
  "nombre": "Juan",
  "edad": 30,
  "direccion": {
    "calle": "Av. Reforma",
    "numero": 123,
    "ciudad": "Ciudad de México"
  },
  "telefonos": [
    {
      "tipo": "casa",
      "numero": "555-1234"
    },
    {
      "tipo": "trabajo",
      "numero": "555-5678"
    }
  ]
}
```

#  Estructura de como Invocar un archivo de texto .json

En C#, puedes trabajar con archivos JSON utilizando la clase JsonSerializer del espacio de nombres System.Text.Json. A continuación, te muestro un ejemplo básico de cómo leer y escribir datos en un archivo JSON utilizando C#.

# Leer y Escribir un Archivo JSON en C#

Aquí tienes un ejemplo de cómo puedes leer y escribir datos en un archivo JSON utilizando C#:

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        // Datos a escribir en el archivo JSON
        var datos = new Persona
        {
            Nombre = "Juan",
            Edad = 30,
            Ciudad = "Ciudad de México",
            Intereses = new List<string> { "música", "deportes", "lectura" },
            Trabajo = new Trabajo
            {
                Puesto = "Ingeniero",
                Empresa = "TechCorp"
            }
        };

        // Escribir datos en un archivo JSON
        string jsonString = JsonSerializer.Serialize(datos, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        File.WriteAllText("datos.json", jsonString);

        // Leer datos desde un archivo JSON
        string jsonLeido = File.ReadAllText("datos.json");
        Persona datosLeidos = JsonSerializer.Deserialize<Persona>(jsonLeido);

        // Mostrar los datos leídos
        Console.WriteLine(datosLeidos.Nombre);
        Console.WriteLine(datosLeidos.Edad);
        Console.WriteLine(datosLeidos.Ciudad);
        Console.WriteLine(string.Join(", ", datosLeidos.Intereses));
        Console.WriteLine($"{datosLeidos.Trabajo.Puesto} en {datosLeidos.Trabajo.Empresa}");
    }
}

public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Ciudad { get; set; }
    public List<string> Intereses { get; set; }
    public Trabajo Trabajo { get; set; }
}

public class Trabajo
{
    public string Puesto { get; set; }
    public string Empresa { get; set; }
}

```
# Explicacion del codigo 

Este ejemplo muestra cómo serializar y deserializar objetos en **C#** utilizando `System.Text.Json` para trabajar con archivos **JSON**.

## 1. Definición de Clases  

- `Persona` y `Trabajo` son clases que representan la estructura de los datos JSON.

## 2. Escribir Datos en un Archivo JSON  

- Se utiliza `JsonSerializer.Serialize()` para convertir un objeto en una cadena JSON.  
- `WriteIndented = true` permite formatear el JSON con indentación para mejorar la legibilidad.  
- `File.WriteAllText()` escribe la cadena JSON en un archivo.  

## 3. Leer Datos desde un Archivo JSON  

- `File.ReadAllText()` lee el contenido del archivo JSON.  
- `JsonSerializer.Deserialize<Persona>()` convierte la cadena JSON en un objeto de tipo `Persona`.  

## Código de Ejemplo  

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        // Datos a escribir en el archivo JSON
        var datos = new Persona
        {
            Nombre = "Juan",
            Edad = 30,
            Ciudad = "Ciudad de México",
            Intereses = new List<string> { "música", "deportes", "lectura" },
            Trabajo = new Trabajo
            {
                Puesto = "Ingeniero",
                Empresa = "TechCorp"
            }
        };

        // Escribir datos en un archivo JSON
        string jsonString = JsonSerializer.Serialize(datos, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText("datos.json", jsonString);

        // Leer datos desde un archivo JSON
        string jsonLeido = File.ReadAllText("datos.json");
        Persona datosLeidos = JsonSerializer.Deserialize<Persona>(jsonLeido);

        // Mostrar los datos leídos
        Console.WriteLine(datosLeidos.Nombre);
        Console.WriteLine(datosLeidos.Edad);
        Console.WriteLine(datosLeidos.Ciudad);
        Console.WriteLine(string.Join(", ", datosLeidos.Intereses));
        Console.WriteLine($"{datosLeidos.Trabajo.Puesto} en {datosLeidos.Trabajo.Empresa}");
    }
}

public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Ciudad { get; set; }
    public List<string> Intereses { get; set; }
    public Trabajo Trabajo { get; set; }
}

public class Trabajo
{
    public string Puesto { get; set; }
    public string Empresa { get; set; }
}

---

# Archivos de Texto (.txt)

Los archivos de texto (.txt) son uno de los formatos más simples y ampliamente utilizados para almacenar información. A diferencia de los archivos JSON, que tienen una estructura específica, los archivos de texto pueden contener cualquier tipo de texto sin formato.  

# Características de los Archivos de Texto

1. **Texto Plano**: Los archivos de texto contienen texto sin formato, lo que significa que no tienen estilos de fuente, colores, ni otros formatos especiales.
2. **Codificación**: Generalmente, los archivos de texto se guardan en codificaciones como ASCII o UTF-8, que son compatibles con la mayoría de los sistemas y editores de texto.
3. **Estructura Simple**: No tienen una estructura predefinida como JSON. Pueden contener párrafos, listas, tablas, etc., pero todo se representa como texto simple.
4. **Compatibilidad**: Son compatibles con casi cualquier sistema operativo y editor de texto, lo que los hace muy portátiles.
5. **Uso Común**: Se utilizan para tomar notas, escribir documentos simples, guardar configuraciones y almacenar datos que no requieren formato especial.

# Ejemplo de Contenido de un Archivo de Texto
```texto
Este es un ejemplo de archivo de texto.
Puede contener varias líneas de texto.
Cada línea se separa con un salto de línea.

Los archivos de texto son muy simples y fáciles de usar.
```


# Ventajas

- **Simplicidad**: Fáciles de crear y editar.
- **Portabilidad**: Pueden ser abiertos en cualquier editor de texto.
- **Tamaño Pequeño**: Ocupan poco espacio en comparación con formatos más complejos.

# Desventajas

- **Falta de Formato**: No pueden contener formato de texto como negritas, cursivas, etc.
- **Limitaciones de Estructura**: No son adecuados para datos estructurados complejos.


Los archivos de texto son ideales para situaciones donde se necesita almacenar información de manera simple y sin complicaciones.


---
# Archivos CSV (.csv)

Los archivos **CSV (Comma-Separated Values)** son un formato de texto simple utilizado para almacenar datos tabulares, como hojas de cálculo o bases de datos. Cada línea en un archivo CSV corresponde a una fila de la tabla, y las columnas están separadas por comas.  

## Estructura de un Archivo CSV

1. **Encabezado (Opcional)**: La primera línea de un archivo CSV a menudo contiene los nombres de las columnas, separados por comas.

```csv

Nombre,Edad,Ciudad

```


2. **Datos**: Las líneas siguientes contienen los datos, con cada valor separado por una coma.

```csv

Juan,30,Ciudad de México
Ana,25,Guadalajara
Luis,35,Monterrey

```


## Ejemplo Completo

```csv

Nombre,Edad,Ciudad
Juan,30,Ciudad de México
Ana,25,Guadalajara
Luis,35,Monterrey

```


## Características Clave

- **Separador**: Aunque las comas son el separador más común, otros caracteres como punto y coma (`;`) o tabulaciones pueden ser usados.
- **Compatibilidad**: Los archivos CSV son compatibles con muchas aplicaciones, incluyendo hojas de cálculo como Microsoft Excel y Google Sheets, y lenguajes de programación como Python y R.
- **Simplicidad**: Son fáciles de leer y escribir, tanto para humanos como para máquinas.
- **Limitaciones**: No soportan tipos de datos complejos ni relaciones entre tablas.

## Ventajas

- **Intercambio de Datos**: Ideales para el intercambio de datos entre diferentes sistemas.
- **Tamaño Pequeño**: Ocupan poco espacio debido a su simplicidad.

## Desventajas

- **Falta de Tipos de Datos**: No hay una definición explícita de tipos de datos, lo que puede llevar a errores de interpretación.
- **Sin Metadatos**: No almacenan información sobre los datos, como descripciones de columnas o relaciones.

Los archivos CSV son ampliamente utilizados debido a su simplicidad y facilidad de uso, especialmente cuando se necesita compartir datos tabulares entre diferentes aplicaciones o sistemas.
