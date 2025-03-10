///*Escribe un programa que permita al usuario ingresar 10 números enteros en un arreglo y luego: 
//1. Muestre la suma total de los valores. 
//2. Calcule el promedio de los números ingresados. 


using System;

class Program
{
    static void Main()
    {
        int[] numeros = new int[10];
        for (int i = 0; i < 10; i++)
        {
            Console.Write("Ingresa el número " + (i + 1) + ": ");
            numeros[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("La suma total de los valores es: " + Sumar(numeros));
        Console.WriteLine("El promedio de los números ingresados es: " + Promedio(numeros));
    }

    static int Sumar(int[] numeros)
    {
        int suma = 0;
        for (int i = 0; i < numeros.Length; i++)
        {
            suma += numeros[i];
        }
        return suma;
    }

    static double Promedio(int[] numeros)
    {
        return (double)Sumar(numeros) / numeros.Length;
    }
}