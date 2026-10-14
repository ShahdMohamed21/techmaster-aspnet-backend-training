# Task 08 – Bad Auth Refactor Pack

## Overview

This task focuses on refactoring insecure authentication code into a safer, maintainable, and production-oriented authentication system using ASP.NET Core Web API.

The original implementation contained security vulnerabilities, including plain-text password comparison, fake token generation, unrestricted role assignment, and returning sensitive user information.

The refactored implementation separates authentication business logic into `AuthService`, uses password hashing, generates JWT access tokens, validates requests, and returns safe response DTOs.

## Security Improvements

The following improvements address the original insecure implementation:

1. **Password Hashing:** Passwords are hashed using ASP.NET Core `PasswordHasher<ApplicationUser>` instead of being stored as plain text.

2. **Secure Password Verification:** Login verifies the submitted password against the stored password hash rather than comparing plain-text values.

3. **Real JWT Authentication:** Fake tokens are replaced with signed JWT access tokens.

4. **JWT Claims:** Tokens include user identification, email, role, and a unique token identifier (`jti`).

5. **Safe Response DTOs:** Authentication endpoints return `AuthResponse` instead of exposing the complete user entity and sensitive database fields.

6. **Input Validation:** Data annotations and service-level checks validate required fields, email format, password strength, and password confirmation.

7. **Duplicate Email Prevention:** Registration checks whether the email is already registered before creating a new account.

8. **Role Restrictions:** Invalid roles are rejected, and public registration is not allowed to create Admin accounts.

9. **Separation of Concerns:** Authentication business logic is moved from the controller into `AuthService`, accessed through `IAuthService`.

10. **Asynchronous Database Operations:** Entity Framework Core asynchronous methods such as `FirstOrDefaultAsync`, `AnyAsync`, and `SaveChangesAsync` are used.

11. **Inactive Account Protection:** Inactive users cannot log in or refresh their authentication tokens.

12. **Appropriate HTTP Status Codes:** The controller uses status codes such as `400 Bad Request`, `401 Unauthorized`, and `409 Conflict` for relevant failure scenarios.

13. **Logging:** Authentication events and failed login attempts are logged without intentionally recording passwords or authentication tokens.

14. **Audit Trail Integration:** Successful user registration and login events are recorded through the audit logging service.

15. **Refresh Token Security:** Refresh tokens are generated using cryptographically secure random bytes, stored as hashes, and rotated during token refresh.

16. **Password Change Validation:** Password changes verify the current password and enforce password complexity requirements.

17. **Dependency Injection:** Services are injected through constructors, improving maintainability and testability.

18. **Role-Based Authorization:** Protected endpoints use `[Authorize]`, while public authentication endpoints explicitly use `[AllowAnonymous]`.

## Before vs. After

| Original Implementation | Refactored Implementation |
|---|---|
| Plain-text password comparison | Password hashing and verification |
| Fake token string | Signed JWT access token |
| Full user entity returned | Safe authentication response DTO |
| Missing input validation | DTO and service-level validation |
| User-controlled role assignment | Role validation and Admin registration restriction |
| Database logic inside controller | Dedicated authentication service |
| Synchronous database operations | Asynchronous EF Core operations |
| Incorrect or inconsistent error responses | Appropriate HTTP status codes |
| No inactive-account check | Inactive accounts are rejected |
| No structured authentication logging | Authentication logging and audit integration |

## API Endpoints

| Method | Endpoint | Description | Access |
|---|---|---|---|
| POST | `/api/auth/register` | Register a new user | Anonymous |
| POST | `/api/auth/login` | Authenticate and obtain tokens | Anonymous |
| GET | `/api/auth/me` | Retrieve the current user's profile | Authenticated |
| POST | `/api/auth/change-password` | Change the current user's password | Authenticated |
| POST | `/api/auth/refresh-token` | Refresh authentication tokens | Anonymous |
| POST | `/api/auth/logout` | Revoke the supplied refresh token | Authenticated |

## Validation Rules

Registration validates:

- Full name is required and must satisfy the configured length constraints.
- Email is required and must have a valid email format.
- Password must contain at least eight characters, including uppercase and lowercase letters, a number, and a special character.
- Confirm password must match the password.
- Role must be valid.
- Admin registration through the public endpoint is prohibited.
- Duplicate email addresses are rejected.

Login validates required credentials, verifies the password hash, and rejects inactive accounts.

## Logging and Audit Trail

The authentication service integrates with the audit logging system to record:

- `UserRegistered` — successful user registration.
- `UserLoggedIn` — successful login.

Application logging also records relevant failed authentication attempts.

Passwords, password hashes, access tokens, and refresh tokens should never be included in log messages or audit metadata.

## Testing Checklist

The following scenarios should be tested before submission:

- [ ] Register with valid information.
- [ ] Reject an invalid email address.
- [ ] Reject a weak password.
- [ ] Reject mismatched password confirmation.
- [ ] Reject a duplicate email address.
- [ ] Reject public registration with the Admin role.
- [ ] Log in successfully with valid credentials.
- [ ] Reject an incorrect password.
- [ ] Reject login for an inactive account.
- [ ] Verify that responses do not expose password hashes.
- [ ] Verify that the generated JWT contains the expected claims.
- [ ] Verify that protected endpoints reject unauthenticated requests.
- [ ] Verify that successful registration and login create audit records.
- [ ] Verify refresh token rotation and logout revocation.
- [ ] Build the project successfully.

## Evidence and Deliverables

The submission should include:

1. **Original Bad Code:** Preserve the original insecure AuthController as a reference file outside the active application compilation path.
2. **Refactored Code:** Include the updated AuthController, AuthService, IAuthService, authentication DTOs, and related configuration.
3. **README:** Document the security issues and the corresponding fixes.
4. **Before/After Evidence:** Provide screenshots or test notes demonstrating the original vulnerabilities and the corrected behavior.
5. **Git History:** Include at least five meaningful commits documenting the refactoring process.

## Suggested Git Commit History

The following is a suggested sequence. Each commit should correspond to changes actually made in the repository.

1. `docs: preserve original insecure authentication example`
2. `security: implement password hashing and verification`
3. `security: replace fake tokens with JWT authentication`
4. `refactor: separate auth logic and improve validation`
5. `security: add auth logging audit trail and documentation`

## Technologies Used

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- ASP.NET Core PasswordHasher
- Data Annotations
- Dependency Injection
- Git and GitHub

## Conclusion

The authentication refactoring replaces the original insecure implementation with a service-based design that improves credential handling, token generation, validation, authorization, error handling, and auditability.

The result is more maintainable and secure, while providing a foundation for further automated testing and production hardening.
