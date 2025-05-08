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