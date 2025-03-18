//1. Solicita al usuario escribir un texto y guárdalo en un archivo (texto.txt). 
//2. Luego, abre el archivo y cuenta cuántas palabras contiene el texto.
//3. Finalmente, muestra en pantalla el número de palabras.
//4. Que el programa te de la opción de imprimir el texto guardado en el archivo texto.txt
//5. Exepcion de que el texto se encuentre en el archivo marque un mensaje de error y que escriba un texto diferente
//6. que el programa acepte caracteres especiales como puntos, comas, etc.
//7. que el programa no cuente espacios en blanco como palabras.
//Guardar en archivos de texto .Json 

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace practica7
{
    class Program
    {
        static void Main(string[] args)
        {
            GestorTexto gestor = new GestorTexto("texto.json");
            gestor.CargarTexto();

            string opcion;
            do
            {
                Texto texto = gestor.CrearTexto();
                gestor.AgregarTexto(texto);
                gestor.GuardarTexto();

                Console.WriteLine("Desea ingresar otro texto? (s/n)");
                opcion = Console.ReadLine();
            } while (opcion.ToLower() == "s");

            gestor.ContarPalabras();

            Console.WriteLine("Desea imprimir el texto guardado? (s/n)");
            string opcion2 = Console.ReadLine();
            if (opcion2.ToLower() == "s")
            {
                gestor.MostrarTexto();
            }
        }
    }

    class Texto
    {
        public string Contenido { get; set; }
    }

    class GestorTexto
    {
        private string archivo;
        private List<Texto> textos;

        public GestorTexto(string archivo)
        {
            this.archivo = archivo;
            this.textos = new List<Texto>();
        }

        public void CargarTexto()
        {
            if (File.Exists(archivo))
            {
                try
                {
                    string contenido = File.ReadAllText(archivo);
                    textos = JsonSerializer.Deserialize<List<Texto>>(contenido);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar el archivo: {ex.Message}");
                }
            }
        }

        public void GuardarTexto()
        {
            try
            {
                string contenido = JsonSerializer.Serialize(textos);
                File.WriteAllText(archivo, contenido);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar el archivo: {ex.Message}");
            }
        }

        public Texto CrearTexto()
        {
            Texto texto = new Texto();
            Console.WriteLine("Escribe un texto:");
            string nuevoTexto = Console.ReadLine();

            if (textos.Any(t => t.Contenido == nuevoTexto))
            {
                Console.WriteLine("Error: El texto ya existe. Por favor, escribe un texto diferente.");
                return CrearTexto();
            }

            texto.Contenido = nuevoTexto;
            return texto;
        }

        public void AgregarTexto(Texto texto)
        {
            textos.Add(texto);
        }

public void ContarPalabras()
{
    if (textos.Any())
    {
        int totalPalabras = 0;
        foreach (var texto in textos)
        {
            // Dividir el texto en palabras usando espacios y caracteres especiales como delimitadores
            string[] palabras = texto.Contenido.Split(new char[] { ' ', '.', ',', '!', '?', ':', ';', '-', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            // Filtrar palabras vacías o nulas
            totalPalabras += palabras.Count(p => !string.IsNullOrWhiteSpace(p));
        }
        Console.WriteLine($"El número total de palabras es: {totalPalabras}");
    }
    else
    {
        Console.WriteLine("No hay textos guardados.");
    }
}


        public void MostrarTexto()
        {
            if (textos.Any())
            {
                foreach (var texto in textos)
                {
                    Console.WriteLine(texto.Contenido);
                }
            }
            else
            {
                Console.WriteLine("No hay textos guardados.");
            }
        }
    }
}