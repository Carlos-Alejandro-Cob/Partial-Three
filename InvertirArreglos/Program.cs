//realiza un programa que contenga un metodo que invuerta un arreglo unidimensional con valores enteros en programacion orientada a objetos
using System;

class program 
{
    static void Main()
    {
        int [] numeros = new int[5];
        numeros[0] = 10;
        numeros[1] = 20;
        numeros[2] = 30;
        numeros[3] = 40;
        numeros[4] = 50;
        Invertir(numeros);  
    }

    static void Invertir(int[] numeros)
    {
        for (int i = numeros.Length - 1; i >= 0; i--)
        {
            Console.WriteLine("Elemento en el indice " + i + ": " + numeros[i]);
        }
    }
}