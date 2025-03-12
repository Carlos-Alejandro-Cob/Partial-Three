//1. Crea una matriz de 3x3 donde el usuario ingrese los valores. 
//2. Luego, multiplica cada fila por un número ingresado por el usuario. 
//3. Muestra la nueva matriz con los valores actualizados.
//4. Guarda la matriz en un archivo JSON.
//5. Carga la matriz desde el archivo JSON y muestra los valores en pantalla.
//6. Que el programa te de la opción de imprimir la matriz guardada en el archivo matriz.json
//Guardar en archivos de texto .JSON
//exepcion de que el archivo no exista o haya un error al leerlo
//exepcion de que lo introducido no sea un número
//exepcion de que el número sea menor a 0

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace practica10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una instancia de GestorMatriz para manejar la lógica de la matriz
            GestorMatriz gestor = new GestorMatriz("matriz.json");

            // Solicita al usuario ingresar los valores de la matriz
            gestor.IngresarMatriz();

            // Pide al usuario ingresar un número para multiplicar cada fila
            int numeroMultiplicador;
            do
            {
                Console.WriteLine("Ingrese un número para multiplicar cada fila:");
                string numeroMultiplicadorStr = Console.ReadLine();
                if (!int.TryParse(numeroMultiplicadorStr, out numeroMultiplicador))
                {
                    Console.WriteLine("Error: Debe ingresar un número entero.");
                    continue;
                }
                if (numeroMultiplicador < 0)
                {
                    Console.WriteLine("Error: El número debe ser mayor o igual a 0.");
                    continue;
                }

                // Multiplica cada fila de la matriz por el número ingresado
                gestor.MultiplicarMatriz(numeroMultiplicador);
                break;
            } while (true);

            // Muestra la nueva matriz con los valores actualizados
            gestor.MostrarMatriz();

            // Guarda la matriz actualizada en el archivo JSON
            gestor.GuardarMatriz();

            // Pregunta al usuario si desea imprimir la matriz guardada
            Console.WriteLine("Desea imprimir la matriz guardada en el archivo matriz.json? (s/n)");
            string opcion = Console.ReadLine();
            if (opcion == "s")
            {
                // Carga y muestra la matriz guardada en el archivo JSON
                gestor.CargarMatriz();
                gestor.MostrarMatriz();
            }
        }
    }

    class GestorMatriz
    {
        private int[,] matriz;
        private string archivo;

        public GestorMatriz(string archivo)
        {
            this.archivo = archivo;
            this.matriz = new int[3, 3]; // Inicializa la matriz con un tamaño de 3x3
        }

        // Cargar la matriz desde el archivo JSON
        public void CargarMatriz()
        {
            if (File.Exists(archivo))
            {
                try
                {
                    string json = File.ReadAllText(archivo);
                    var listaDeListas = JsonSerializer.Deserialize<List<List<int>>>(json);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            matriz[i, j] = listaDeListas[i][j];
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al leer el archivo: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("El archivo no existe.");
            }
        }

        // Guardar la matriz en el archivo JSON
        public void GuardarMatriz()
        {
            try
            {
                var listaDeListas = new List<List<int>>();
                for (int i = 0; i < 3; i++)
                {
                    var fila = new List<int>();
                    for (int j = 0; j < 3; j++)
                    {
                        fila.Add(matriz[i, j]);
                    }
                    listaDeListas.Add(fila);
                }
                string json = JsonSerializer.Serialize(listaDeListas);
                File.WriteAllText(archivo, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        // Solicitar al usuario ingresar los valores de la matriz
        public void IngresarMatriz()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int valor;
                    do
                    {
                        Console.WriteLine($"Ingrese el valor para la posición [{i}, {j}]:");
                        string valorStr = Console.ReadLine();
                        if (!int.TryParse(valorStr, out valor))
                        {
                            Console.WriteLine("Error: Debe ingresar un número entero.");
                            continue;
                        }
                        matriz[i, j] = valor;
                        break;
                    } while (true);
                }
            }
        }

        // Multiplicar cada fila de la matriz por un número
        public void MultiplicarMatriz(int numeroMultiplicador)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matriz[i, j] *= numeroMultiplicador;
                }
            }
        }

        // Mostrar la matriz en pantalla
        public void MostrarMatriz()
        {
            Console.WriteLine("Matriz:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{matriz[i, j]} ");
                }
                Console.WriteLine();
            }
        }
    }
}
