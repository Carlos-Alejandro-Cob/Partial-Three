# Partial-Three


# En este repositorio se subiran todas las actividades relacionadas con el parcial 3 de POO, matrices y la gestion de archivos de texto .txt .json y .csv 

# Estructura de los archivos de textto .JSON

Los archivos JSON (JavaScript Object Notation) son un formato de texto ligero utilizado para intercambiar datos. La estructura de un archivo JSON es bastante sencilla y se basa en dos estructuras principales:

1. Objetos: Colecciones de pares clave-valor. Un objeto JSON está rodeado por llaves {} y contiene pares de clave y valor separados por dos puntos :. Los pares están separados por comas ,.
{
|"nombre": |"Juan",|
 | "edad":| 30,|
  |"ciudad": |"Ciudad de México"|
}

2. Arreglos: Listas ordenadas de valores. Un arreglo JSON está rodeado por corchetes [] y contiene valores separados por comas ,.
[
 | "manzana",|
  |"banana",|
  |"cereza"|
]

# Ejemplo Combinado
Un archivo JSON puede combinar objetos y arreglos para representar datos más complejos:

- {
- "nombre": "Juan",
- "edad": 30,
- "direccion": {
- "calle": "Av. Reforma",
- "numero": 123,
- "ciudad": "Ciudad de México"
- },
- "telefonos": [
- {
-  "tipo": "casa",
- "numero": "555-1234"
- },
- {
- "tipo": "trabajo",
- "numero": "555-5678"
- }
- ]
- }
