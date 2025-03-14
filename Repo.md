# Cómo Subir Cambios al Repositorio  

Sigue estos pasos para subir tus cambios al repositorio de GitHub. Abre tu terminal y navega al directorio donde se encuentra tu repositorio local:  

```bash
cd "C:\Users\MSI\Desktop\Repo\Partial-Three"
```
Verifica el estado actual de tu repositorio y los cambios pendientes con:

```bash
git status
Agrega todos los archivos modificados o nuevos al área de staging:
```
```bash
git add .
Guarda los cambios en tu repositorio local con un mensaje descriptivo:
```
```bash
git commit -m "Descripción de los cambios realizados"
```
Sube los commits al repositorio remoto en GitHub:

```bash
git push origin main
```
Si hay cambios en el repositorio remoto que necesitas integrar en tu copia local, usa:

```bash
git pull origin main
```
Para asegurarte de que la URL remota está configurada correctamente:

```bash
git remote -
```
Notas: Asegúrate de tener configurada tu clave SSH para interactuar con el repositorio remoto sin problemas de autenticación. Escribe mensajes de commit claros y descriptivos para facilitar el seguimiento de cambios.

