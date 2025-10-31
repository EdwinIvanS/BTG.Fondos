# 🧩 Prueba Técnica – Ingeniero de Desarrollo Back End

## Parte 1 – Fondos (80%)

### Necesidad de negocio
BTG Pactual desea crear una plataforma que permita a los clientes gestionar sus fondos de inversión sin necesidad de contactar a un asesor.

### Funcionalidades del sistema
1. Suscribirse a un nuevo fondo (apertura).  
2. Cancelar la suscripción a un fondo actual.  
3. Ver historial de transacciones (aperturas y cancelaciones).  
4. Enviar notificación por email o SMS según preferencia del usuario al suscribirse a un fondo.

### Reglas de negocio
- Monto inicial del cliente: COP $500.000.  
- Cada transacción debe tener un identificador único.  
- Cada fondo tiene un monto mínimo de vinculación.  
- Al cancelar una suscripción, el valor de vinculación se retorna al cliente.  
- Si no hay saldo suficiente, mostrar:  
  `"No tiene saldo disponible para vincularse al fondo <Nombre del fondo>"`

## 📦 Tecnologías utilizadas
- **Backend:** .NET Core 8  
- **Base de datos:** SQL Server / NoSQL (según modelo de datos implementado)  
- **Pruebas unitarias:** xUnit  
- **Control de versiones:** Git  
- **Infraestructura y despliegue:** AWS CloudFormation, EC2, Security Groups, IAM

## Modelo de datos
Se diseñó un modelo de datos que soporta:  
- Transacciones de apertura y cancelación.  
- Relación cliente-fondo con historial de movimientos.  
- Validación de montos mínimos y saldo disponible.

## API REST Endpoints

> Base URL: `http://ec2-98-91-193-89.compute-1.amazonaws.com`

### Autenticación
| Método | Endpoint           | Descripción           |
|--------|------------------|----------------------|
| POST   | `/api/auth/register` | Registro de usuario |
| POST   | `/api/auth/login`    | Login y obtención de token |

### Fondos
| Método | Endpoint                     | Descripción                                      |
|--------|------------------------------|--------------------------------------------------|
| GET    | `/api/fondos/historial/{id}` | Obtener historial de transacciones de un cliente |
| POST   | `/api/fondos/suscribir`      | Suscribirse a un fondo                           |
| POST   | `/api/fondos/cancelar`       | Cancelar suscripción a un fondo                  |


## ⚙️ Requisitos previos
- Autenticación mediante JWT.  
- Autorización por roles (cliente, administrador).  
- Encriptación de datos sensibles en la base de datos.  
- Validaciones y manejo de excepciones implementados.

## 🚀 Cómo ejecutar el proyecto localmente
## Ejecución local
```bash
# Configurar variable de entorno DOTNET_ROOT
export DOTNET_ROOT=/opt/dotnet

# Ejecutar la API en puerto 80
sudo /opt/dotnet/dotnet ./BTG.Fondos.API.dll --urls "http://0.0.0.0:80"

## 🧪 Pruebas
### MICROSERVICE - FONDOS
  - [Post] (http://ec2-98-91-193-89.compute-1.amazonaws.com/api/auth/register)
  - [Post] (http://ec2-98-91-193-89.compute-1.amazonaws.com/api/auth/login)
  - [Get]  (http://ec2-98-91-193-89.compute-1.amazonaws.com/api/fondos/historial/1)
  - [Post] (http://ec2-98-91-193-89.compute-1.amazonaws.com/api/fondos/suscribir)
  - [Post] (http://ec2-98-91-193-89.compute-1.amazonaws.com/api/fondos/cancelar)

