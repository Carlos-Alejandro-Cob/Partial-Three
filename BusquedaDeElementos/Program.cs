// Crea un programa que busque un elemento en un arreglo bidimensional y muestre su posición en caso de encontrarlo. En caso contrario, debe mostrar un mensaje indicando que el elemento no se encuentra en el arreglo.
//Intenta buscar un elemento que se encuentre en el arreglo y otro que no se encuentre para verificar que el programa funciona correctamente.
//El arreglo bidimensional debe ser de tamaño 3x3 y debe contener números enteros.
//El programa debe tener un método llamado Buscar que reciba el arreglo bidimensional y el elemento a buscar, y muestre la posición del elemento si se encuentra o un mensaje indicando que no se encuentra.
//exepcion si el usuario ingresa un numero que ya habia ingresado antes que imprima ambos indices
//Ejemplo de entrada:
//Ingrese los elementos del arreglo 3x3:
//Ingrese el elemento [0,0]: 1
using System;
using System.Collections.Generic;

class ArregloBidimensional
{
    private int[,] numeros;
    private Dictionary<int, List<(int, int)>> indices;

    // Constructor que inicializa el arreglo y el diccionario de índices
    public ArregloBidimensional()
    {
        numeros = new int[3, 3];
        indices = new Dictionary<int, List<(int, int)>>();
    }

    // Método para ingresar los elementos del arreglo
    public void IngresarElementos()
    {
        Console.WriteLine("Ingrese los elementos del arreglo 3x3:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write($"Ingrese el elemento [{i},{j}]: ");
                int numero = int.Parse(Console.ReadLine());
                numeros[i, j] = numero;

                // Almacenar el índice del número en el diccionario
                if (indices.ContainsKey(numero))
                {
                    indices[numero].Add((i, j));
                }
                else
                {
                    indices[numero] = new List<(int, int)> { (i, j) };
                }
            }
        }
    }

    // Método para buscar un elemento en el arreglo
    public void BuscarElemento(int elemento)
    {
        if (indices.ContainsKey(elemento))
        {
            List<(int, int)> posiciones = indices[elemento];
            Console.Write($"El elemento {elemento} se encuentra en los índices: ");
            foreach (var posicion in posiciones)
            {
                Console.Write($"[{posicion.Item1},{posicion.Item2}] ");
            }
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"El elemento {elemento} no se encuentra en el arreglo.");
        }
    }
}

class Program
{
    static void Main()
    {
        ArregloBidimensional arreglo = new ArregloBidimensional();

        // Ingresar elementos en el arreglo
        arreglo.IngresarElementos();

        // Solicitar al usuario que ingrese el elemento a buscar
        Console.Write("Ingrese el elemento a buscar: ");
        int elemento = int.Parse(Console.ReadLine());

        // Buscar el elemento en el arreglo
        arreglo.BuscarElemento(elemento);
    }
}
