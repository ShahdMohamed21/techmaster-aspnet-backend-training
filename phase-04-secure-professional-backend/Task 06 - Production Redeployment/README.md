# Task 06 – Production Redeployment

## Training Center API

### Live Deployment
- **Hosting:** MonsterASP
- **Live Swagger:** [http://techmaster.runasp.net/swagger/index.html](http://techmaster.runasp.net/swagger/index.html)
- **Database:** Remote SQL Server

### Technologies
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Authentication & Role-Based Authorization

### Security Features
- Secure password hashing.
- JWT-based authentication.
- Protected endpoints using Bearer tokens.
- Role-based access control.
- Production secrets stored outside source control.

### Production Verification
- Swagger accessibility.
- Remote database connectivity.
- User registration and login.
- Protected endpoint access.
- Unauthorized access rejection (`401`).
- Wrong-role rejection (`403`).

### Evidence
- Live Swagger screenshot.
- Remote database tables and data.
- Successful login returning a token.
- Protected endpoint using a Bearer token.
- Wrong-role request returning `403 Forbidden`.

### Deployment Notes
The API is deployed to MonsterASP and configured to use a remote SQL Server database. Production connection strings and JWT signing keys must remain private and must never be committed to GitHub.

