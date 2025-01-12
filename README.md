# LLImageAnnotation

**LLImageAnnotation** es una herramienta intuitiva para la anotación de imágenes, desarrollada en **C#** utilizando **Windows Forms** y dirigida a la plataforma **.NET Framework 4.8.1**. Esta aplicación está diseñada para crear **Bounding Boxes** sobre imágenes, con el objetivo de generar datasets personalizados para entrenar modelos de detección de objetos como **YOLO**.

## Características principales

- **Interfaz de usuario gráfica intuitiva:** permite una interacción rápida y cómoda para crear y ajustar Bounding Boxes.
- **Carga de proyectos desde carpetas seleccionadas por el usuario:** la aplicación puede abrir y manejar proyectos de anotación almacenados en directorios locales.
- **Compatibilidad con YOLO:** las anotaciones generadas se exportan en formatos compatibles con YOLO, facilitando el entrenamiento de modelos de detección de objetos.
- **Soporte para múltiples imágenes:** permite trabajar con varias imágenes dentro de un mismo proyecto.

## Requisitos del sistema

- **Sistema operativo:** Windows 10 o superior
- **Plataforma:** .NET Framework 4.8.1
- **Entorno de desarrollo:** Visual Studio 2019 o superior

## Instalación y uso

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/tuusuario/LLImageAnnotation.git
   ```
2. **Abrir el proyecto en Visual Studio:**
   - Abre el archivo de solución `.sln` en Visual Studio.
3. **Compilar y ejecutar:**
   - Compila el proyecto y ejecuta la aplicación desde Visual Studio.

## Contribuciones

Las contribuciones son bienvenidas. Si deseas contribuir a este proyecto, por favor sigue estos pasos:

1. Realiza un fork del repositorio.
2. Crea una rama para tu nueva función o corrección de errores.
3. Envía un Pull Request explicando los cambios realizados.

## Licencia

Este proyecto está licenciado bajo la **Licencia MIT**. Consulta el archivo [LICENSE](./LICENSE) para obtener más información.

---

# .gitignore (Plantilla)

```gitignore
# Archivos binarios y de compilación
bin/
obj/

# Configuraciones de usuario de Visual Studio
*.user
*.suo
*.vspx
*.vsp

# Archivos de depuración
*.log
*.pdb

# Archivos temporales
*.tmp
*.temp
*.cache

# Archivos generados por el sistema
Thumbs.db
Desktop.ini
```

---

# LICENSE (Plantilla de Licencia MIT)

```plaintext
MIT License

Copyright (c) [Año] [Tu Nombre o Nombre de la Organización]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

