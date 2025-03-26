using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace ProductosApp
{
    class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int CantidadStock { get; set; }

        public Producto(string codigo, string nombre, decimal precioUnitario, int cantidadStock)
        {
            Codigo = codigo;
            Nombre = nombre;
            PrecioUnitario = precioUnitario;
            CantidadStock = cantidadStock;
        }

        public void Mostrar()
        {
            Console.WriteLine($"Código: {Codigo}");
            Console.WriteLine($"Nombre: {Nombre}");
            Console.WriteLine($"Precio Unitario: {PrecioUnitario}");
            Console.WriteLine($"Cantidad en Stock: {CantidadStock}");
        }
    }

    class GestorProductos
    {
        private readonly string ArchivoJson;
        private List<Producto> productos;

        public GestorProductos(string archivoJson)
        {
            ArchivoJson = archivoJson;
            productos = CargarDatos();
        }

        private List<Producto> CargarDatos()
        {
            if (File.Exists(ArchivoJson))
            {
                string json = File.ReadAllText(ArchivoJson);
                return JsonSerializer.Deserialize<List<Producto>>(json);
            }
            return new List<Producto>();
        }

        private void GuardarDatos()
        {
            string json = JsonSerializer.Serialize(productos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ArchivoJson, json);
        }

        public void BuscarYMostrarProductoPorCodigo()
        {
            Console.Write("Ingrese el código del producto: ");
            string codigo = Console.ReadLine();
            Producto producto = productos.FirstOrDefault(p => p.Codigo == codigo);

            if (producto != null)
            {
                producto.Mostrar();
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void BuscarYMostrarProductoPorNombre()
        {
            Console.Write("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();
            Producto producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Mostrar();
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void ComprarProducto()
        {
            Console.Write("Ingrese el código del producto que desea comprar: ");
            string codigo = Console.ReadLine();
            Producto producto = productos.FirstOrDefault(p => p.Codigo == codigo);
            if (producto != null)
            {
                Console.Write("Ingrese la cantidad a comprar: ");
                if (int.TryParse(Console.ReadLine(), out int cantidadCompra) && cantidadCompra <= producto.CantidadStock)
                {
                    producto.CantidadStock -= cantidadCompra;
                    GuardarDatos();
                    decimal total = producto.PrecioUnitario * cantidadCompra;
                    Console.WriteLine($"Total a pagar: {total}");

                    Console.Write("¿Desea realizar el pago? (S/N): ");
                    string respuesta = Console.ReadLine();
                    if (respuesta.Equals("S", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Ingrese el dinero: ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal dinero) && dinero >= total)
                        {
                            decimal cambio = dinero - total;
                            Console.WriteLine($"Cambio: {cambio}");
                            Console.WriteLine("Compra realizada con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("Dinero insuficiente.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Compra cancelada.");
                    }
                }
                else
                {
                    Console.WriteLine("No hay suficiente stock disponible o cantidad inválida.");
                }
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void AgregarNuevoProducto()
        {
            Console.Write("Ingrese el código del producto: ");
            string codigo = Console.ReadLine();

            if (productos.Any(p => p.Codigo == codigo))
            {
                Console.WriteLine("Ya existe un producto con ese código.");
                return;
            }

            Console.Write("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el precio unitario del producto: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal precioUnitario) && precioUnitario > 0)
            {
                Console.Write("Ingrese la cantidad en stock del producto: ");
                if (int.TryParse(Console.ReadLine(), out int cantidadStock) && cantidadStock >= 0)
                {
                    Producto nuevoProducto = new Producto(codigo, nombre, precioUnitario, cantidadStock);
                    productos.Add(nuevoProducto);
                    GuardarDatos();
                    Console.WriteLine("Producto agregado con éxito.");
                }
                else
                {
                    Console.WriteLine("Cantidad en stock inválida.");
                }
            }
            else
            {
                Console.WriteLine("Precio unitario inválido.");
            }
        }

        public void ActualizarProducto()
        {
            Console.Write("Ingrese el código del producto a actualizar: ");
            string codigo = Console.ReadLine();
            Producto producto = productos.FirstOrDefault(p => p.Codigo == codigo);
            if (producto != null)
            {
                Console.Write("Ingrese el nuevo nombre del producto: ");
                string nombre = Console.ReadLine();
                Console.Write("Ingrese el nuevo precio unitario del producto: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal precioUnitario) && precioUnitario > 0)
                {
                    Console.Write("Ingrese la nueva cantidad en stock del producto: ");
                    if (int.TryParse(Console.ReadLine(), out int cantidadStock) && cantidadStock >= 0)
                    {
                        producto.Nombre = nombre;
                        producto.PrecioUnitario = precioUnitario;
                        producto.CantidadStock = cantidadStock;
                        GuardarDatos();
                        Console.WriteLine("Producto actualizado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Cantidad en stock inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("Precio unitario inválido.");
                }
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }

        public void EliminarProducto()
        {
            Console.Write("Ingrese el código del producto a eliminar: ");
            string codigo = Console.ReadLine();
            Producto producto = productos.FirstOrDefault(p => p.Codigo == codigo);
            if (producto != null)
            {
                productos.Remove(producto);
                GuardarDatos();
                Console.WriteLine("Producto eliminado con éxito.");
            }
            else
            {
                Console.WriteLine("Producto no encontrado.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GestorProductos gestor = new GestorProductos("productos.json");

            while (true)
            {
                try
                {
                    Console.WriteLine("¿Qué desea hacer?");
                    Console.WriteLine("1. Buscar un producto por código");
                    Console.WriteLine("2. Buscar un producto por nombre");
                    Console.WriteLine("3. Comprar un producto");
                    Console.WriteLine("4. Agregar un nuevo producto");
                    Console.WriteLine("5. Actualizar un producto existente");
                    Console.WriteLine("6. Eliminar un producto");
                    Console.WriteLine("7. Salir");

                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            gestor.BuscarYMostrarProductoPorCodigo();
                            break;

                        case "2":
                            gestor.BuscarYMostrarProductoPorNombre();
                            break;

                        case "3":
                            gestor.ComprarProducto();
                            break;

                        case "4":
                            gestor.AgregarNuevoProducto();
                            break;

                        case "5":
                            gestor.ActualizarProducto();
                            break;

                        case "6":
                            gestor.EliminarProducto();
                            break;

                        case "7":
                            Console.WriteLine("Saliendo del programa.");
                            return;

                        default:
                            Console.WriteLine("Opción no válida.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: Formato de entrada no válido. Asegúrese de ingresar números válidos.");
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error de E/S: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }
            }
        }
    }
}
