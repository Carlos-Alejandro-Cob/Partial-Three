//1. Solicita al usuario escribir un texto y guárdalo en un archivo (texto.txt). 
//2. Luego, abre el archivo y cuenta cuántas palabras contiene el texto.
//3. Finalmente, muestra en pantalla el número de palabras.
//4. Que el programa te de la opción de imprimir el texto guardado en el archivo texto.txt
//5. Exepcion de que el texto se encuentre en el archivo marque un mensaje de error y que escriba un texto diferente
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
            // Crea una instancia de GestorTexto para manejar la lógica de texto
            GestorTexto gestor = new GestorTexto("texto.json");
            // Carga el texto existente desde el archivo JSON
            gestor.CargarTexto();

            string opcion;
            do
            {
                // Crea un nuevo texto con datos ingresados por el usuario
                Texto texto = gestor.CrearTexto();
                // Agrega el nuevo texto a la lista
                gestor.AgregarTexto(texto);
                // Guarda la lista actualizada de texto en el archivo JSON
                gestor.GuardarTexto();

                // Pregunta al usuario si desea ingresar otro texto
                Console.WriteLine("Desea ingresar otro texto? (s/n)");
                opcion = Console.ReadLine();
            } while (opcion == "s");

            // Muestra el número de palabras del texto
            gestor.ContarPalabras();

            // Pregunta al usuario si desea imprimir el texto guardado
            Console.WriteLine("Desea imprimir el texto guardado? (s/n)");
            string opcion2 = Console.ReadLine();
            if (opcion2 == "s")
            {
                // Muestra el texto guardado
                gestor.MostrarTexto();
            }
        }
    }

    class Texto
    {
        // Propiedad que representa el texto ingresado por el usuario
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

        // Cargar el texto existente desde el archivo JSON
        public void CargarTexto()
        {
            if (File.Exists(archivo))
            {
                string contenido = File.ReadAllText(archivo);
                textos = JsonSerializer.Deserialize<List<Texto>>(contenido);
            }
        }

        // Guardar la lista de textos en el archivo JSON
        public void GuardarTexto()
        {
            string contenido = JsonSerializer.Serialize(textos);
            File.WriteAllText(archivo, contenido);
        }

        // Crear un nuevo texto con datos ingresados por el usuario
        public Texto CrearTexto()
        {
            Texto texto = new Texto();
            Console.WriteLine("Escribe un texto:");
            string nuevoTexto = Console.ReadLine();

            // Verificar si el texto ya existe en la lista
            if (textos.Any(t => t.Contenido == nuevoTexto))
            {
                Console.WriteLine("Error: El texto ya existe. Por favor, escribe un texto diferente.");
                return CrearTexto(); // Llamada recursiva para solicitar un nuevo texto
            }

            texto.Contenido = nuevoTexto;
            return texto;
        }

        // Agregar un nuevo texto a la lista
        public void AgregarTexto(Texto texto)
        {
            textos.Add(texto);
        }

        // Contar y mostrar el número de palabras en el texto
        public void ContarPalabras()
        {
            if (textos.Any())
            {
                int totalPalabras = textos.Sum(t => t.Contenido.Split(' ').Length);
                Console.WriteLine($"El número total de palabras es: {totalPalabras}");
            }
            else
            {
                Console.WriteLine("No hay textos guardados.");
            }
        }

        // Mostrar el texto guardado
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
