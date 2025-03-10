//Pide al usuario 5 números enteros y almacénalos en un arreglo. Luego, muestra el arreglo original y su 
//versión invertida. Pide al usuario 5 números enteros y almacénalos en un arreglo. Luego, muestra el arreglo original y su 
//versión invertida.    
//Nota: Para invertir el arreglo, puedes crear un nuevo arreglo y copiar los elementos en orden inverso, o puedes
//modificar el arreglo original intercambiando los elementos de posición.
//En programación orientada a objetos, el método de invertir el arreglo se implementa en una clase y se llama a un
//método de esa clase para invertir el arreglo.

using System;

class Program
{
    static void Main()
    {
        int[] numeros = new int[5];
        for (int i = 0; i < 5; i++)
        {
            Console.Write("Ingresa el número " + (i + 1) + ": ");
            numeros[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Arreglo original:");
        Mostrar(numeros);

        Invertir(numeros);

        Console.WriteLine("Arreglo invertido:");
        Mostrar(numeros);
    }

    static void Mostrar(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }

    static void Invertir(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n / 2; i++)
        {
            int temp = array[i];
            array[i] = array[n - i - 1];
            array[n - i - 1] = temp;
        }
    }
}