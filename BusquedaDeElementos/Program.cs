using System;

class Program
{
    static void Main()
    {
        int[,] numeros = new int[3, 3];
        numeros[0, 0] = 10;
        numeros[0, 1] = 20;
        numeros[0, 2] = 30;
        numeros[1, 0] = 40;
        numeros[1, 1] = 50;
        numeros[1, 2] = 60;
        numeros[2, 0] = 70;
        numeros[2, 1] = 80;
        numeros[2, 2] = 90;
        int elemento = 50;

        Buscar(numeros, elemento);
    }

    static void Buscar(int[,] numeros, int elemento)
    {
        for (int i = 0; i < numeros.GetLength(0); i++)
        {
            for (int j = 0; j < numeros.GetLength(1); j++)
            {
                if (numeros[i, j] == elemento)
                {
                    Console.WriteLine("El elemento " + elemento + " se encuentra en el índice [" + i + "," + j + "]");
                    return;
                }
            }
        }
        Console.WriteLine("El elemento " + elemento + " no se encuentra en el arreglo.");
    }
}
