# Proyecto JuJu

## Implementación de Requerimientos

Este proyecto implementa todos los requerimientos solicitados.

## Arquitectura Limpia

El código se organiza para usar arquitecturas limpias. La capa del core no depende de la infraestructura ni del API.

## Inversión de Dependencias

Se utiliza la inversión de dependencias para que desde la capa del core se puedan usar elementos de la infraestructura, aunque la capa del core no tiene referencia a infraestructura. En cambio, la infraestructura es quien depende del core.

## Inyección de Dependencias

Este proyecto utiliza la inyección de dependencias.

## Manejo de Excepciones

Se crea un filtro para el manejo de excepciones en lugar de usar try-catch.

## Pruebas Unitarias

Se han creado pruebas unitarias para validar la funcionalidad del código.

## DTOs

Se crean Data Transfer Objects (DTOs) para no acceder a las entidades directamente desde el API.

## Validaciones del Modelo

Se realizan validaciones del modelo para asegurar la integridad de los datos.

## Eliminación de Código Innecesario

Se ha eliminado código innecesario, como la Clase BaseModel.

## Configuración

Por favor, revisa el archivo `appsettings.json` para asegurarte de que la cadena de conexión sea la correcta.

