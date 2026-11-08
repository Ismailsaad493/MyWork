# Database Integration Test Report

**Project:** `LibraryApi`  
**Date:** 2026-11-08  
**Scope:** MySQL connectivity, EF Core configuration, CRUD paths, relationships, repository/service behavior, invalid inputs, and schema consistency.

## Result

**Overall: Failed for the requested MySQL integration; database CRUD testing was blocked.** The API is configured for SQL Server, not MySQL. Its configured SQL Server endpoint was also unavailable from this environment. No CRUD requests or SQL statements that modify data were run, and no database records were changed.

## Passed checks

- `dotnet build LibraryApi.csproj --no-restore` succeeded with 0 errors. It emitted one `NU1900` warning because the NuGet vulnerability service index could not be reached.
- The API host started successfully in Development and listened on `http://127.0.0.1:5199`. Startup alone does not establish a database connection.
- Static EF model review confirmed primary keys for `Books`, `Members`, and `Loans`; foreign keys from `Loans` to `Books` and `Members`; restrictive deletes for those relationships; a unique member email index; and a filtered unique index allowing at most one active loan per book.
- Static review found DTO and service validation for required book/member fields, positive loan IDs and periods, fine fields, and payment fields. These validation rules were not confirmed through HTTP because loopback requests were denied by the execution environment.

## Failed and blocked checks

| Check | Result | Evidence |
|---|---|---|
| MySQL configuration/provider | **Failed** | `LibraryApi.csproj` references `Microsoft.EntityFrameworkCore.SqlServer`; infrastructure calls `UseSqlServer`; no MySQL provider or MySQL connection string is configured. |
| MySQL connectivity | **Failed / no target configured** | No MySQL endpoint is present in project configuration. A read-only check found no service listening on local TCP port 3306. |
| Configured database connectivity | **Failed** | The read-only `sqlcmd` probe to `localhost/LibraryDB` failed before running its `SELECT`, reporting SQL Server unavailable/login timeout. |
| Runtime null/range request validation | **Blocked** | The API started, but the environment denied local HTTP socket requests before they reached the app. No request reached a controller or database. |

## Not run

- CRUD for `Books`, `Members`, `Loans`, or `Fines`.
- Live primary-key, foreign-key, index, and relationship checks against database metadata.
- Runtime tests for missing IDs, duplicate records, null values, or SQL constraint handling.
- SQL/data consistency queries.

These checks require a reachable database and, for any write-path tests, a separate test database. None was configured or available, so write requests were deliberately not sent to `LibraryDB`.

## Bugs and risks found

1. **The project does not currently integrate with MySQL.** The configured provider, connection string, SQL scripts, and parts of the repository layer are SQL Server-specific. A MySQL database cannot be validated through this configuration.
2. **The EF model and fine repository target different schemas.** EF-backed book, member, and loan operations use `Books`, `Members`, and `Loans`. `FineRepository` uses SQL Server-specific `TOP (1)` queries against `dbo.BookLoans` and `dbo.Borrowers`, with legacy columns such as `BorrowerId`, `BorrowDate`, and `ReturnDate`. The README also describes `BookLoans` and `Borrowers` as the existing database schema. If only one of these schema generations exists, operations for the other generation will encounter missing-table or missing-column SQL errors. The live database was unavailable, so those SQL errors were not observed directly.
3. **Fines are not represented in the checked-in EF migration history.** The current context exposes and configures `Fine`, but the initial migration and model snapshot contain only `Books`, `Members`, and `Loans`. Fine creation instead depends on manually applying `Infrastructure/Context/FineManagement.sql`. The EF model also has no configured relationship for `Fine.BookLoanId`; the SQL script separately defines a database foreign key to `BookLoans`.
4. **Constraint error handling is tied to SQL Server.** `ApiExceptionMiddleware` recognizes `Microsoft.Data.SqlClient.SqlException` numbers for duplicate and foreign-key violations. It would not map MySQL provider exceptions to the intended conflict response without provider-specific handling.

## Test safety

All database probes were read-only. No insert, update, delete, migration, schema script, or database setup command was run. No existing database records were modified.

## Follow-up: SQL Server `LibraryDB_Test` connection check

**Date:** 2026-11-08  
**Connection result:** **Connected successfully.** A read-only SQL Server query returned database `LibraryDB_Test` on server `LAPTOP-EAOM9HIG`. The initial sandboxed attempt to `localhost` failed; a permitted external read-only attempt succeeded. No production database was accessed.

The app setting `ConnectionStrings:DefaultConnection` now targets `LibraryDB_Test` on `localhost`, using the existing Windows authentication mode. The API was also started with an explicit test-database-only environment override. No credential values were printed.

### Read-only schema results

- The database contains `dbo.Books`, `dbo.BookLoans`, and `dbo.Fines`, among other library tables.
- The API also expects `dbo.Members`, `dbo.Loans`, and `dbo.Borrowers`; these tables are absent.
- `dbo.Books` has primary key `Id` and a foreign key from `AuthorId` to `Authors.Id`. It lacks the `Author` and `RowVersion` columns selected by the API's current `Book` EF model.
- `dbo.BookLoans` has primary key `Id` and foreign keys to `Books.Id`, `Students.Id`, and `Users.Id`. It uses columns such as `StudentId` and `LoanDate`; the fine repository instead queries `BorrowerId` and `BorrowDate` and joins `Borrowers`.
- `dbo.Fines` has primary key `Id` and foreign keys to `BookLoans.Id` and `Students.Id`. Its columns differ from the current EF `Fine` model, which expects `CreatedAtUtc` and `PaymentMethod` among others.
- The database contains many historical EF migrations from another schema generation. No migration was run during this check.

### EF Core and application queries

The API connected to SQL Server and issued EF Core read queries, confirming transport/authentication and DbContext setup. The read-only GET endpoints did not complete successfully because the database schema does not match the current application model:

| GET endpoint | Result | SQL Server error |
|---|---:|---|
| `/api/books` | HTTP 500 | Error 207: `Invalid column name 'Author'. Invalid column name 'RowVersion'.` |
| `/api/members` | HTTP 500 | The expected `Members` table is absent. |
| `/api/loans` | HTTP 500 | The expected `Loans` table is absent. |
| `/api/fines` | HTTP 500 | Error 208: `Invalid object name 'dbo.Borrowers'.` |

**Application status:** SQL Server connectivity is successful, but the application cannot successfully serve these database-backed endpoints until its EF model/repository SQL and `LibraryDB_Test` schema are aligned. No writes, schema changes, or further CRUD tests were performed.

The SQL checks used `sqlcmd` in read-only mode against the SQL Server instance; no SSMS GUI session was used. All checks remained scoped to `LibraryDB_Test`.
