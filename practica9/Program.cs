//Crea un programa que registre las ventas de un día en un archivo CSV (ventas.csv), donde cada línea 
//tenga el nombre del producto, cantidad vendida y precio unitario. Luego, el programa debe leer el 
//archivo y calcular el total de ventas del día. El programa debe mostrar el total de ventas en pantalla.
//El programa debe permitir al usuario imprimir el contenido del archivo ventas.csv.
//El programa debe manejar excepciones en caso de que el archivo no exista o haya un error al leerlo.
//Guardar en archivos de texto .CSV
//Permitir al usuario ingresar los datos de las ventas y guardarlos en el archivo ventas.csv
//Permitir al usuario ingresar un producto a buscar y mostrar el total de ventas de ese producto

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace practica9
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una instancia de GestorVentas para manejar la lógica de ventas
            GestorVentas gestor = new GestorVentas("ventas.csv");
            // Carga las ventas existentes desde el archivo CSV
            gestor.CargarVentas();

            string opcion;
            do
            {
                // Crea una nueva venta con datos ingresados por el usuario
                Venta venta = gestor.CrearVenta();
                // Agrega la nueva venta a la lista
                gestor.AgregarVenta(venta);
                // Guarda la lista actualizada de ventas en el archivo CSV
                gestor.GuardarVentas();

                // Pregunta al usuario si desea ingresar otra venta
                Console.WriteLine("Desea ingresar otra venta? (s/n)");
                opcion = Console.ReadLine();
            } while (opcion == "s");

            // Muestra el total de ventas del día
            gestor.MostrarTotalVentas();

            // Pregunta al usuario si desea imprimir el contenido del archivo ventas.csv
            Console.WriteLine("Desea imprimir el contenido del archivo ventas.csv? (s/n)");
            string opcion2 = Console.ReadLine();
            if (opcion2 == "s")
            {
                // Muestra el contenido del archivo ventas.csv
                gestor.MostrarVentas();
            }

            // Pide al usuario ingresar un producto a buscar
            Console.WriteLine("Ingrese el nombre de un producto a buscar:");
            string productoABuscar = Console.ReadLine();
            // Muestra el total de ventas del producto ingresado
            gestor.MostrarTotalVentasProducto(productoABuscar);
        }
    }

    class Venta
    {
        // Propiedades que representan los datos de una venta
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    class GestorVentas
    {
        private string archivo;
        private List<Venta> ventas;

        public GestorVentas(string archivo)
        {
            this.archivo = archivo;
            this.ventas = new List<Venta>();
        }

        // Cargar las ventas existentes desde el archivo CSV
        public void CargarVentas()
        {
            ventas.Clear(); // Limpia la lista antes de cargar nuevos datos
            if (File.Exists(archivo))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(archivo);
                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split(',');
                        if (partes.Length == 3 &&
                            int.TryParse(partes[1], out int cantidad) &&
                            decimal.TryParse(partes[2], NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal precio))
                        {
                            Venta venta = new Venta
                            {
                                Producto = partes[0],
                                Cantidad = cantidad,
                                PrecioUnitario = precio
                            };
                            ventas.Add(venta);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                }
            }
        }

        // Guardar la lista de ventas en el archivo CSV
        public void GuardarVentas()
        {
            try
            {
                List<string> lineas = new List<string>();
                foreach (var venta in ventas)
                {
                    string linea = $"{venta.Producto},{venta.Cantidad},{venta.PrecioUnitario.ToString(CultureInfo.InvariantCulture)}";
                    lineas.Add(linea);
                }
                File.WriteAllLines(archivo, lineas);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        // Crear una nueva venta con datos ingresados por el usuario
        public Venta CrearVenta()
        {
            Venta venta = new Venta();
            Console.WriteLine("Ingrese el nombre del producto:");
            venta.Producto = Console.ReadLine();

            Console.WriteLine("Ingrese la cantidad vendida:");
            int cantidad;
            while (!int.TryParse(Console.ReadLine(), out cantidad) || cantidad <= 0)
            {
                Console.WriteLine("Cantidad inválida. Ingrese un número entero positivo:");
            }
            venta.Cantidad = cantidad;

            Console.WriteLine("Ingrese el precio unitario:");
            decimal precio;
            while (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.InvariantCulture, out precio) || precio <= 0)
            {
                Console.WriteLine("Precio inválido. Ingrese un número decimal positivo:");
            }
            venta.PrecioUnitario = precio;

            return venta;
        }

        // Agregar una nueva venta a la lista
        public void AgregarVenta(Venta venta)
        {
            ventas.Add(venta);
        }

        // Mostrar el total de ventas del día
        public void MostrarTotalVentas()
        {
            decimal total = ventas.Sum(v => v.Cantidad * v.PrecioUnitario);
            Console.WriteLine($"El total de ventas del día es: {total.ToString("C", CultureInfo.CurrentCulture)}");
        }

        // Mostrar el contenido del archivo ventas.csv
        public void MostrarVentas()
        {
            Console.WriteLine("Contenido del archivo ventas.csv:");
            foreach (var venta in ventas)
            {
                Console.WriteLine($"{venta.Producto},{venta.Cantidad},{venta.PrecioUnitario.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        // Mostrar el total de ventas de un producto específico
        public void MostrarTotalVentasProducto(string productoABuscar)
        {
            if (string.IsNullOrWhiteSpace(productoABuscar))
            {
                Console.WriteLine("El nombre del producto no puede estar vacío.");
                return;
            }

            decimal totalProducto = ventas
                .Where(v => v.Producto.Equals(productoABuscar, StringComparison.OrdinalIgnoreCase))
                .Sum(v => v.Cantidad * v.PrecioUnitario);

            if (totalProducto > 0)
            {
                Console.WriteLine($"El total de ventas para el producto '{productoABuscar}' es: {totalProducto.ToString("C", CultureInfo.CurrentCulture)}");
            }
            else
            {
                Console.WriteLine($"No se encontraron ventas para el producto '{productoABuscar}'.");
            }
        }
    }
}
