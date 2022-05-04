# XMEN__
API REST - Define si una persona es un mutante o un humano según su secuencia de ADN

# Intrucciones ejecución API

1. Instalar visual studio 2019 o superior.
2. Abrir el archivo XMEN.sln.
3. Marcar como proyecto de ejecución incial el proyecto XMEN.WebApi.
4. Seleccionar la ejecución del proyecto como IIS Express o apuntando a la misma WebApi.
5. Desde el proyecto XMEN.WebApi archivo appsetting.json ajustar el parametro DefaultConnetion para apuntar a el data source (SqlServer) correspondiente así como el usuario y la contraseña para el login.
6. Luego se abrira la ventana con la documentación en Swagger del Api.
7. Registrarse o loguearse para utilizar el API con los metodos del api (/api/Auth/Register - /api/Auth/Login).
8. Con el token generado por el servicio Login colocarlo en el apartado de autorización usando el tipo Bearer Token.
9. Puedes utilizar la API para comprobar si eres un mutante !!

# Documentos adicionales en repositorio directorio Documents

1. Reporte de covertura de las pruebas unitarias (ReportCoverage).
2. Colección de postman con los metodos del API (XMEN.postman_collection).

# Url API Azure

 - webxmenapi.azurewebsites.net/api/{controller}/{Service} - ejemplo => webxmenapi.azurewebsites.net/api/Mutant/isMutant
