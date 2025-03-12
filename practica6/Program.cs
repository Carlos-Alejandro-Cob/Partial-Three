//1.Permita al usuario ingresar el nombre y 3 calificaciones de varios estudiantes.
//2. Guarde esta información en un archivo de texto (notas.txt).
//3. Luego, el programa debe leer el archivo y calcular el promedio de cada estudiante
//4. Finalmente, el programa debe mostrar en pantalla el nombre de cada estudiante y su promedio.
//5. Que el programa te de la opción de imprimir los nombre guardados en el archivo notas.txt
//6. Exepcion de que el nombre se encuentre en el archivo marque un mensaje de error y que escriba un nombre diferente
//Guardar en archivos de texto .JSON

using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace practica6
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crea una instancia de GestorEstudiantes para manejar la lógica de estudiantes
            GestorEstudiantes gestor = new GestorEstudiantes("notas.json");
            // Carga los estudiantes existentes desde el archivo JSON
            gestor.CargarEstudiantes();

            string opcion;
            do
            {
                // Crea un nuevo estudiante con datos ingresados por el usuario
                Estudiante estudiante = gestor.CrearEstudiante();
                // Agrega el nuevo estudiante a la lista
                gestor.AgregarEstudiante(estudiante);
                // Guarda la lista actualizada de estudiantes en el archivo JSON
                gestor.GuardarEstudiantes();

                // Pregunta al usuario si desea ingresar otro estudiante
                Console.WriteLine("Desea ingresar otro estudiante? (s/n)");
                opcion = Console.ReadLine();
            } while (opcion == "s");

            // Muestra el promedio de calificaciones de cada estudiante
            gestor.MostrarPromedios();

            // Pregunta al usuario si desea imprimir los nombres de los estudiantes guardados
            Console.WriteLine("Desea imprimir los nombres guardados? (s/n)");
            string opcion2 = Console.ReadLine();
            if (opcion2 == "s")
            {
                // Muestra los nombres de los estudiantes
                gestor.MostrarNombres();
            }
        }
    }

    class Estudiante
    {
        // Propiedades que representan los datos de un estudiante
        public string Nombre { get; set; }
        public int Calificacion1 { get; set; }
        public int Calificacion2 { get; set; }
        public int Calificacion3 { get; set; }

        // Método para calcular el promedio de las calificaciones del estudiante
        public double CalcularPromedio()
        {
            return (Calificacion1 + Calificacion2 + Calificacion3) / 3.0;
        }
    }

    class GestorEstudiantes
    {
        private string archivo; // Ruta del archivo JSON donde se guardan los datos
        private List<Estudiante> estudiantes; // Lista para almacenar los estudiantes en memoria

        // Constructor que inicializa el gestor con la ruta del archivo
        public GestorEstudiantes(string archivo)
        {
            this.archivo = archivo;
            estudiantes = new List<Estudiante>();
        }

        // Método para cargar los estudiantes desde el archivo JSON
        public void CargarEstudiantes()
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);
                estudiantes = JsonSerializer.Deserialize<List<Estudiante>>(json);
            }
        }

        // Método para crear un nuevo estudiante con datos ingresados por el usuario
        public Estudiante CrearEstudiante()
        {
            string nombre;
            do
            {
                Console.WriteLine("Ingrese el nombre del estudiante: ");
                nombre = Console.ReadLine();
                // Verifica si el nombre ya existe en la lista de estudiantes
            } while (estudiantes.Exists(e => e.Nombre == nombre));

            Console.WriteLine("Ingrese la calificacion 1: ");
            int calificacion1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la calificacion 2: ");
            int calificacion2 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese la calificacion 3: ");
            int calificacion3 = int.Parse(Console.ReadLine());

            // Crea y devuelve un nuevo objeto Estudiante
            return new Estudiante
            {
                Nombre = nombre,
                Calificacion1 = calificacion1,
                Calificacion2 = calificacion2,
                Calificacion3 = calificacion3
            };
        }

        // Método para agregar un estudiante a la lista
        public void AgregarEstudiante(Estudiante estudiante)
        {
            estudiantes.Add(estudiante);
        }

        // Método para guardar la lista de estudiantes en el archivo JSON
        public void GuardarEstudiantes()
        {
            File.WriteAllText(archivo, JsonSerializer.Serialize(estudiantes, new JsonSerializerOptions { WriteIndented = true }));
        }

        // Método para mostrar el promedio de calificaciones de cada estudiante
        public void MostrarPromedios()
        {
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine($"Nombre: {estudiante.Nombre} Promedio: {estudiante.CalcularPromedio()}");
            }
        }

        // Método para mostrar los nombres de los estudiantes
        public void MostrarNombres()
        {
            foreach (var estudiante in estudiantes)
            {
                Console.WriteLine(estudiante.Nombre);
            }
        }
    }
}
