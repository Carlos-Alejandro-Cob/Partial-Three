//Implementa el método de ordenamiento burbuja para ordenar un arreglo de números ingresados por el 
//usuario de menor a mayor. 
//El programa debe solicitar al usuario la cantidad de números a ingresar y luego solicitar los números
//uno por uno. Al finalizar, el programa debe mostrar los números ordenados de menor a mayor.
//Nota: El método de ordenamiento burbuja consiste en comparar cada elemento de la lista con el siguiente
//y cambiarlos de posición si están en el orden incorrecto. Este proceso se repite hasta que no se necesiten 
//más intercambios, lo que significa que la lista está ordenada.
//En programacion orientada a objetos, el metodo de ordenamiento burbuja se implementa en una clase y se
//llama a un metodo de esa clase para ordenar el arreglo.

using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingresa la cantidad de números a ingresar: ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Por favor, ingresa un número entero positivo.");
            return;
        }

        int[] numeros = new int[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            Console.Write("Ingresa el número " + (i + 1) + ": ");
            if (!int.TryParse(Console.ReadLine(), out numeros[i]))
            {
                Console.WriteLine("Por favor, ingresa un número entero válido.");
                return;
            }
        }

        OrdenarBurbuja(numeros);

        Console.WriteLine("Números ordenados de menor a mayor:");
        for (int i = 0; i < cantidad; i++)
        {
            Console.WriteLine(numeros[i]);
        }
    }

    static void OrdenarBurbuja(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    // Intercambiar array[j] y array[j + 1]
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}
