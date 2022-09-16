Para entender la idea detras de este proyecto, primero hay que entender el domino a fondo y comprender todas sus características.
El domino es un juego que se desarrolla por turnos entre uno o más jugadores; en cada turno un jugador agrega al desarrollo del juego una o mas fichas que cumplan con algún criterio definido por las reglas de la variante que se esté jugando en el momento. Según sea esta variante, las fichas se pueden comparar entre sí y por lo tanto puede existir un criterio de orden entre ellas. A su vez, las fichas pueden ser repartidas al comienzo del juego, o irse repartiendo a medida que se desarrolla el juego, este acaba cuando se cumplen las condiciones para que  exista un ganador o cuando se ha llegado a un punto en el que el juego se estanca.Según la variante de domino es la forma en que se determinan los ganadores del juego en caso de haber uno.

Si observamos estas características desde un punto de vista mas general y abstracto, podemos decir que el domino es un juego que inicia, se desarrolla, tiene una forma de determinar su fin, el orden de los jugadores, los ganadores, y de actualizar su estado; y a su vez es capaz de decidir de que manera va a hacer todas estas características.
Para abstraernos de esto, se creó la interface "IGame" y la interface "IGameModesSelector", la cuales reunen todas las características de un juego, o sea:
-- Inicia
-- Actualiza su estado
-- Decide su ganador
-- Decide el orden de sus jugadores
-- Determina cuando termina el juego
-- Decide de que forma se va a realizar cada uno de estos procesos
