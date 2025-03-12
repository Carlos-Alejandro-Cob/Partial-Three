//1. Solicita al usuario ingresar 5 nombres y guárdalos en un archivo de texto (nombres.txt).
//2. Luego, abre el archivo y muestra en pantalla los nombres almacenados.
//3. Finalmente, ordena los nombres alfabéticamente y muestra la lista ordenada en pantalla.
//Que el programa solicite los nombres, los almacene en un archivo, los lea del archivo, los ordene y los muestre
//en pantalla. y que el programa te de la opción de imprimir los nombre guardados en el archivo nombres.txt
//Exepcion de que el nombre se encuentre en el archivo marque un mensaje de error y que escriba un nombre diferente
//Exepcion de que no se permita ingresar numeros y solo nombres
//Nota: Puedes utilizar el método de ordenamiento burbuja para ordenar los nombres.
//En programación orientada a objetos, el método de ordenamiento burbuja se implementa en una clase y se
//llama a un método de esa clase para ordenar el arreglo.

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
    private const string FileName = "nombres.txt";

    static void Main()
    {
        Console.Write("Ingresa la cantidad de nombres a ingresar: ");
        if (!int.TryParse(Console.ReadLine(), out int cantidad) || cantidad <= 0)
        {
            Console.WriteLine("Por favor, ingresa un número entero positivo.");
            return;
        }

        string[] nombresExistentes = LeerNombres();
        HashSet<string> nombresUnicos = new HashSet<string>();
        if (nombresExistentes != null)
        {
            nombresUnicos.UnionWith(nombresExistentes);
        }

        string[] nombres = new string[cantidad];
        for (int i = 0; i < cantidad; i++)
        {
            Console.Write("Ingresa el nombre " + (i + 1) + ": ");
            string nombre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío.");
                i--; // Repetir la iteración para el mismo índice
            }
            else if (!EsNombreValido(nombre))
            {
                Console.WriteLine("El nombre no puede contener números ni caracteres especiales.");
                i--; // Repetir la iteración para el mismo índice
            }
            else if (nombresUnicos.Contains(nombre))
            {
                Console.WriteLine("Este nombre ya ha sido ingresado anteriormente. Por favor, ingresa un nombre diferente.");
                i--; // Repetir la iteración para el mismo índice
            }
            else
            {
                nombres[i] = nombre;
                nombresUnicos.Add(nombre);
            }
        }

        GuardarNombres(nombres);

        string[] nombresLeidos = LeerNombres();
        if (nombresLeidos != null)
        {
            Console.WriteLine("Nombres almacenados:");
            foreach (string nombre in nombresLeidos)
            {
                Console.WriteLine(nombre);
            }

            OrdenarBurbuja(nombresLeidos);

            Console.WriteLine("Nombres ordenados alfabéticamente:");
            foreach (string nombre in nombresLeidos)
            {
                Console.WriteLine(nombre);
            }
        }

        // Validar nombres leídos del archivo
        try
        {
            string[] nombresValidar = File.ReadAllLines(FileName);
            foreach (string nombre in nombresValidar)
            {
                if (EsNombreValido(nombre))
                {
                    Console.WriteLine($"El nombre '{nombre}' es válido.");
                }
                else
                {
                    Console.WriteLine($"El nombre '{nombre}' contiene números o caracteres especiales.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer el archivo: " + ex.Message);
        }
    }

    static void GuardarNombres(string[] nombres)
    {
        try
        {
            File.AppendAllLines(FileName, nombres);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al guardar los nombres: " + ex.Message);
        }
    }

    static string[] LeerNombres()
    {
        if (!File.Exists(FileName))
        {
            Console.WriteLine("El archivo " + FileName + " no existe.");
            return null;
        }

        try
        {
            return File.ReadAllLines(FileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al leer los nombres: " + ex.Message);
            return null;
        }
    }

    static bool EsNombreValido(string nombre)
    {
        // Patrón de expresión regular para validar nombres
        string patron = "^[a-zA-Z]+$";
        return Regex.IsMatch(nombre, patron);
    }

    static void OrdenarBurbuja(string[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (string.Compare(array[j], array[j + 1]) > 0)
                {
                    // Intercambiar array[j] y array[j + 1]
                    string temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }
}

