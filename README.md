# Proyecto grupal HADA

- Rubén Duro Muñoz 48803149R
- Víctor Mingorance Boix 74536760Q
- Andrés Maciá Valero 74443594T
- Marcos De La Fuente 48789872H
- Alexander Veldemar Volkov 55393741N
- Enmanuel Moreno Montes 55178727W
- Alejandro Villagordo Andreu 49596205V 
----
**Descripción:**
Página web para hacer apuestas deportivas. El nombre está por determinar (Betify, UApuestas, BetZoneX son algunas de las propuestas de momento).
Se podrá acceder como usuario o administrador, para apostar o modificar/comprobar resultados, respectivamente.
El usuario podrá consultar su cartera y sus apuestas (presentes y pasadas).
También se podrá consultar información de los estadios donde se celebren los deportes (nombre, equipo, ciudad, etc.), además de su ubicación en un Google Maps, como posible mejora.

**Parte pública**
El usuario sin registrar podrá acceder a la lista de juegos disponibles para apostar, con su respectiva información, así como a la información de los estadios.
Como es obvio, también podrá registrarse e iniciar sesión.
Entidades de negocio:
* Patrocinadores - Alexander
* Apuesta - Marcos
* Categoría - Marcos
* Estadio - Rubén
* Equipo - Rubén

**Parte privada**
Un usuario registrado podrá acceder como usuario sin permisos o como administrador.
El usuario sin permisos podrá apostar en juegos y consultar su perfil, cartera e historial.
El administrador tendrá la opción de dar por finalizada una apuesta, declarando el equipo ganador, y de eliminar o añadir nuevos juegos.
Entidades de negocio:
* Usuario - Víctor
* Notificaciones - Alejandro
* Transacciones - Alejandro
* Apuesta_usuario - Enmanuel
* Rol - Alexander
* Municipio - Víctor
* Provincia - Andrés
* País - Enmanuel
* Favoritos - Andrés

**Posibles mejoras**
De nuevo, es posible ampliar el proyecto añadiendo un mapa integrado de Google Maps (o una alternativa abierta) en la información de los estadios.
Además, planteamos la opción de crear un perfil con imagen o añadir un Captcha para iniciar la sesión.

## Entrega nº 2 11/04/2025
Hecha la primera versión de los ficheros de entidades de negocio (EN y CAD), cada integrante los suyos.

El esquema EER de la base de datos propuesta por el momento se encuentra en el directorio raíz del proyecto (el mismo que README.md),
con el título "EsquemaDB.pdf".

## Entrega nº 3 02/05/2025
Hechas todas las interfaces básicas de la página junto a la mayoría de interfaces secundarias. Falta la página de informes.
Modificada y arreglada la base de datos, además, se ha modificado el esquema en el pdf.

Se puede navegar en este punto por toda la web utilizando los botones. La vista de la página maestra es una mezcla de su versión pública, privada y de administrador
para probar las funciones. Haciendo click en la foto de perfil se va a la página de perfil, mientras que haciendo click en el logo se va a la página de inicio de sesión.

El proyecto proWeb se está guardando en dos carpetas diferentes "proWeb" y "ProWeb". No sabemos por qué, pero parece cosa del SO, porque hay
miembros del grupo que, partiendo del mismo commit, guardan automáticamente los ficheros en una carpeta y otros en otra. El proyecto aun así compila y funciona correctamente.

## Entrega final
### Propuesta final de proyecto
Tal y como teníamos en la propuesta inicial del proyecto, se trata de una web de apuestas deportivas.\
En su parte pública el usuario invitado podrá consultar las apuestas disponibles, así como las listas de los equipos y estadios almacenados en la base de datos.\
En su parte privada con un perfil cualquiera se podrá acceder a apostar, ver las notificaciones recibidas, consultar la cartera virtual, o añadir equipos a la lista de favoritos.\
Por último desde la parte privada pero con acceso de administrador se podrá acceder a todas las páginas que gestionan los usuarios, equipos, estadios, notificaciones, etc; así como a la página de los informes generales de monetización.\
Como **mejoras** hemos implementado:
* Foto de perfil
* Uso de cookies para facilitar el inicio de sesión
* Login con reCaptcha
* Contraseña encriptada mediante hashing
* Informes sobre el rendimiento económico (obligatorio)
* Google maps para localizar los estadios

### Dificultades y soluciones
Como principal dificultad encontramos la compatibilidad entre ramas a la hora de hacer merge. Esto supone un problema recurrente, pero lo solucionamos poco a poco resolviendo los conflictos que causaban estos ficheros.
Otro problema fue el de almacenamiento de imágenes en el proyecto. Para arreglarlo, decidimos almacenar las imágenes en una carpeta "Source/Images" y guardar en la BD solamente los nombres de los ficheros para poder refererirnos a ellas, almacenando, por ejemplo con otra nomenclatura las imágenes de perfil que las de equipos para evitar riesgos de sobreescritura.
Por otra parte, encontramos que el proyecto web "proWeb" se estaba almacenando internamente como "proWeb" y "ProWeb" en dos carpetas separadas. Equipos de algunos miembros almacenaban los ficheros en uno, mientras que el resto en el otro. Al no encontrar relaciones aparentes entre los posibles motivos, decidimos creer que es una cuestión de la configuración del equipo o del sistema operativo de cada uno; y aunque no esté resuelto visual studio reconoce ambas carpetas y funciona correctamente así.
Finalmente, es destacable la falta de experiencia de trabajo en grupo que tenemos, así como de desarrollo en equipo. Principalmente erramos en alcanzar las metas internas a tiempo y la comunicación entre nosotros, pero se ha notado una mejoría exponencial con el paso de los días.

### Instrucciones de instalación
El proyecto no tiene requisitos más allá de tener instalados correctamente los paquetes nuGet necesarios (deberían estar incluidos con la instalación), además de
los requisitos para compilar y ejecutar NET 4.8

### Tareas hechas por cada miembro
| Miembro del grupo | Tareas realizadas                                                                                                                                        |
|-------------------|----------------------------------------------------------------------------------------------------------------------------------------------------------|
| Víctor            | ENU y CAD Usuario, EN y CAD Municipio, Login aspx, Editar usuario aspx, Perfil aspx, Master, Coordinador, Cookies, Foto perfil, Diseño y programación DB |
| Marcos            | EN y CAD Categoría, EN y CAD Apuesta, Apuestas usuario aspx, Admin patrocinadores aspx, Admin usuario aspx, Juegos aspx, ayuda con diseño BD             |
| Alexander         | Esquema DB, EN y CAD Rol, EN y CAD Patrocinador, Admin Rol aspx, Admin categoria aspx, Info. estadios aspx                                               |
| Andrés            | EN y CAD favoritos EN y CAD provincia, register aspx, Admin notificaciones aspx y Admin menu aspx                                                        |
| Alejandro         | EN y CAD notificaciones, EN y CAD transacción, cartera aspx, notificación aspx, favoritos aspx, funcionalidad informe                                    |
| Enmanuel          | EN y CAD País, EN y CAD apuesta usuario, ListaEquipos aspx, Apuesta_Usuario aspx, Ubicaciones aspx, interfaz informe                                     |
| Rubén             | Admin Equipos aspx, Admin Apuestas aspx, Admin Estadios aspx, CAD y EN Equipo, CAD y EN Estadio                                                          |

(Todas las páginas del code-behind han sido hechas por la misma persona que hizo la interfaz aspx)

La presentación de Canva es accesible desde este enlace: https://www.canva.com/design/DAGnKsUfF34/u1xNSgyGKseqJrosUcWiiw/edit?utm_content=DAGnKsUfF34&utm_campaign=designshare&utm_medium=link2&utm_source=sharebutton \
O, en su defecto, existe una descarga en pdf guardada en el directorio raíz llamada "PresentacionPDF.pdf".
