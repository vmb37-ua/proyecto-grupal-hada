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
* Apuesta
* Juego
* Categoría
* Login
* Estadio
* Equipo
  
**Parte privada**
Un usuario registrado podrá acceder como usuario sin permisos o como administrador.
El usuario sin permisos podrá apostar en juegos y consultar su perfil, cartera e historial.
El administrador tendrá la opción de dar por finalizada una apuesta, declarando el equipo ganador, y de eliminar o añadir nuevos juegos.
Entidades de negocio:
* Saldo
* Usuario
* Administrador
* Transacciones
* Historial
* Perfil
* Cuenta

**Posibles mejoras**
De nuevo, es posible ampliar el proyecto añadiendo un mapa integrado de Google Maps (o una alternativa abierta) en la información de los estadios.
Además, planteamos la opción de crear un perfil con imagen o añadir un Captcha para iniciar la sesión.



