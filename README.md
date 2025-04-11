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
Todas quedan así de momento, excepto las de Alejandro Villagordo (49596205V), que no se han podido implementar por la incompatibilidad
de su rama.

El esquema EER de la base de datos propuesta por el momento se encuentra en el directorio raíz del proyecto (el mismo que README.md),
con el título "EsquemaDB.pdf".