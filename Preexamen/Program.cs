//Hacer un programa de una tienda de productos donde se pueda ingresar el codigo, el nombre del producto, el precio unitario y la cantidad en stock.
//Los datos se deben guardar en un archivo JSON.
//Luego se debe poder cargar los datos del archivo JSON y mostrarlos en pantalla.
//Antes de pedir al usuario que ingrese los datos, se debe mostrar un un mensaje de si desea buscar un producto por codigo o nombre.
//Si el usuario ingresa 1, se debe pedir el codigo del producto y mostrar los datos del producto.
//Si el usuario ingresa 2, se debe pedir el nombre del producto y mostrar los datos del producto.
//Despues muestra el menu para ingresar un nuevo producto o salir.
//imprimir los datos del producto.


using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public decimal PrecioUnitario { get; set; }
    public int CantidadStock { get; set; }
}

class Program
{
    private static readonly string ArchivoJson = "productos.json";

    static void Main(string[] args)
    {
        List<Producto> productos = CargarDatos();

        while (true)
        {
            Console.WriteLine("¿Desea buscar un producto por código (1), por nombre (2) o no buscar (3)?");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Ingrese el código del producto: ");
                string codigo = Console.ReadLine();
                Producto producto = BuscarPorCodigo(productos, codigo);
                if (producto != null)
                {
                    MostrarProducto(producto);
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            else if (opcion == "2")
            {
                Console.Write("Ingrese el nombre del producto: ");
                string nombre = Console.ReadLine();
                Producto producto = BuscarPorNombre(productos, nombre);
                if (producto != null)
                {
                    MostrarProducto(producto);
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
            }
            else if (opcion == "3")
            {
                Console.WriteLine("No se realizará ninguna búsqueda.");
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }

            Console.WriteLine("\n¿Desea ingresar un nuevo producto (1) o salir (2)?");
            opcion = Console.ReadLine();

            if (opcion == "1")
            {
                Console.Write("Ingrese el código del producto: ");
                string codigo = Console.ReadLine();
                Console.Write("Ingrese el nombre del producto: ");
                string nombre = Console.ReadLine();
                Console.Write("Ingrese el precio unitario del producto: ");
                decimal precioUnitario = decimal.Parse(Console.ReadLine());
                Console.Write("Ingrese la cantidad en stock del producto: ");
                int cantidadStock = int.Parse(Console.ReadLine());

                Producto nuevoProducto = new Producto
                {
                    Codigo = codigo,
                    Nombre = nombre,
                    PrecioUnitario = precioUnitario,
                    CantidadStock = cantidadStock
                };

                productos.Add(nuevoProducto);
                GuardarDatos(productos);
                Console.WriteLine("Producto agregado con éxito.");
            }
            else if (opcion == "2")
            {
                Console.WriteLine("Saliendo del programa.");
                break;
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }

            MostrarProductos(productos);
        }
    }

    static List<Producto> CargarDatos()
    {
        if (File.Exists(ArchivoJson))
        {
            string json = File.ReadAllText(ArchivoJson);
            return JsonSerializer.Deserialize<List<Producto>>(json);
        }
        return new List<Producto>();
    }

    static void GuardarDatos(List<Producto> productos)
    {
        string json = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(ArchivoJson, json);
    }

    static void MostrarProductos(List<Producto> productos)
    {
        foreach (var producto in productos)
        {
            MostrarProducto(producto);
            Console.WriteLine("---------------------------");
        }
    }

    static void MostrarProducto(Producto producto)
    {
        Console.WriteLine($"Código: {producto.Codigo}");
        Console.WriteLine($"Nombre: {producto.Nombre}");
        Console.WriteLine($"Precio Unitario: {producto.PrecioUnitario}");
        Console.WriteLine($"Cantidad en Stock: {producto.CantidadStock}");
    }

    static Producto BuscarPorCodigo(List<Producto> productos, string codigo)
    {
        return productos.FirstOrDefault(p => p.Codigo == codigo);
    }

    static Producto BuscarPorNombre(List<Producto> productos, string nombre)
    {
        return productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }
}
