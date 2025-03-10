//Solicita al usuario llenar un arreglo de 5 nombres y luego pedirle un nombre a buscar. El programa 
//debe indicar si el nombre está en el arreglo y en qué posición se encuentra. Si el nombre no está en el arreglo,
//el programa debe indicarlo.

using System;

class Program
{
    static void Main()
    {
        string[] nombres = new string[5];
        for (int i = 0; i < 5; i++)
        {
            Console.Write("Ingresa el nombre " + (i + 1) + ": ");
            nombres[i] = Console.ReadLine();
        }

        Console.Write("Ingresa el nombre a buscar: ");
        string nombre = Console.ReadLine();

        Buscar(nombres, nombre);
    }

    static void Buscar(string[] nombres, string nombre)
    {
        for (int i = 0; i < nombres.Length; i++)
        {
            if (nombres[i] == nombre)
            {
                Console.WriteLine("El nombre " + nombre + " se encuentra en la posición " + i);
                return;
            }
        }
        Console.WriteLine("El nombre " + nombre + " no se encuentra en el arreglo.");
    }
}