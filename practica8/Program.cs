//Pide al usuario ingresar una lista de 10 números en un arreglo y luego solicitarle un número a eliminar. 
//El programa debe quitar ese número del arreglo y mostrar el nuevo arreglo sin el valor eliminado.
//Exception handling: si el número no se encuentra en el arreglo, mostrar un mensaje de error y solicitar otro número.
//Exepcion de que lo introducido no sea un número
//Guardar en archivos de texto .JSON

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace practica8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una instancia de GestorNumeros para manejar la lógica de números
            GestorNumeros gestor = new GestorNumeros("numeros.json");
            // Carga los números existentes desde el archivo JSON
            gestor.CargarNumeros();

            // Solicita al usuario ingresar 10 números
            gestor.IngresarNumeros();

            // Pide al usuario ingresar un número a eliminar
            int numeroAEliminar;
            do
            {
                Console.WriteLine("Ingrese un número a eliminar:");
                string numeroAEliminarStr = Console.ReadLine();
                if (!int.TryParse(numeroAEliminarStr, out numeroAEliminar))
                {
                    Console.WriteLine("Error: Debe ingresar un número entero.");
                    continue;
                }

                // Elimina el número ingresado de la lista
                if (!gestor.EliminarNumero(numeroAEliminar))
                {
                    Console.WriteLine("Error: El número no se encuentra en la lista. Intente nuevamente.");
                }
                else
                {
                    break;
                }
            } while (true);

            // Guarda la lista actualizada de números en el archivo JSON
            gestor.GuardarNumeros();

            // Muestra el nuevo arreglo sin el valor eliminado
            gestor.MostrarNumeros();
        }
    }

    class Numero
    {
        // Propiedad que representa un número entero
        public int Valor { get; set; }
    }

    class GestorNumeros
    {
        private string archivo;
        private List<Numero> numeros;

        public GestorNumeros(string archivo)
        {
            this.archivo = archivo;
            this.numeros = new List<Numero>();
        }

        // Cargar los números existentes desde el archivo JSON
        public void CargarNumeros()
        {
            if (File.Exists(archivo))
            {
                string contenido = File.ReadAllText(archivo);
                numeros = JsonSerializer.Deserialize<List<Numero>>(contenido);
            }
        }

        // Guardar la lista de números en el archivo JSON
        public void GuardarNumeros()
        {
            string contenido = JsonSerializer.Serialize(numeros);
            File.WriteAllText(archivo, contenido);
        }

        // Solicitar al usuario ingresar 10 números
        public void IngresarNumeros()
        {
            numeros.Clear(); // Limpia la lista antes de ingresar nuevos números
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Ingrese el número {i + 1}:");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int numero))
                {
                    numeros.Add(new Numero { Valor = numero });
                }
                else
                {
                    Console.WriteLine("Error: Debe ingresar un número entero.");
                    i--; // Repite la iteración si el input no es un número
                }
            }
        }

        // Eliminar un número de la lista
        public bool EliminarNumero(int numeroAEliminar)
        {
            Numero numero = numeros.FirstOrDefault(n => n.Valor == numeroAEliminar);
            if (numero != null)
            {
                numeros.Remove(numero);
                return true;
            }
            return false;
        }

        // Mostrar los números en la lista
        public void MostrarNumeros()
        {
            Console.WriteLine("Números en la lista:");
            foreach (var numero in numeros)
            {
                Console.WriteLine(numero.Valor);
            }
        }
    }
}
