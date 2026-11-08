# API Execution Report

**Target database:** `LibraryDB_Test` only  
**Date:** 2026-11-08

## Result

The requested Legacy API and New Books Module workflows could not be run because those modules are not present in the checked-out project. Source inventory found one API project (`LibraryApi`), one `LibraryDbContext`, and four controllers: Books, Members, Loans, and Fines. There is no `AppDbContext`, `NewBooksDbContext`, Authors/Categories/Students/BookLoans controller, or `/api/new-books-module` route implementation.

**Count:** 2 checks passed (database connection and OpenAPI document); 19 checks failed (required schema check, four existing database-backed GET endpoints, and 14 requested route probes).

The configured SQL Server connection to `LibraryDB_Test` succeeds. The existing API can open the connection and issue EF Core queries, but its current endpoints fail because the database schema does not match the API model. No test records were created, no migrations were run, and no schema or database records were modified.

## Execution results

| Module / check | Test | Expected | Actual | Status |
|---|---|---|---|---|
| Database | Read-only connection and `SELECT DB_NAME()` | `LibraryDB_Test` reachable | Connected successfully to `LibraryDB_Test` | PASS |
| Database | Read-only tables/keys query | Required schema available | Query succeeded; `Members`, `Loans`, and `Borrowers` are absent; `Books` and `Fines` have incompatible columns | FAIL |
| Legacy | Authors, Categories, Students, BookLoans route probes | Routes available | No corresponding controllers/routes in project; GET probes returned 404 | FAIL |
| Legacy | Books GET | Valid JSON | HTTP 500; SQL Server error 207: invalid columns `Author` and `RowVersion` | FAIL |
| Legacy | Members GET | Valid JSON | HTTP 500; expected `Members` table is absent | FAIL |
| Legacy | Loans GET | Valid JSON | HTTP 500; expected `Loans` table is absent | FAIL |
| Legacy | Fines GET | Valid JSON | HTTP 500; SQL Server error 208: `Invalid object name 'dbo.Borrowers'.` | FAIL |
| New Books | Requested `/api/new-books-module/*` route probes | Routes available | No corresponding controllers/routes in project; all 10 GET probes returned 404 | FAIL |
| API docs | `/openapi/v1.json` | OpenAPI document loads | HTTP 200, `application/json` | PASS |

The route probes were GET-only. The requested POST workflows were not attempted: their first foundation routes are absent, so no real IDs could be obtained. No dependent requests with placeholder IDs were sent.

## Database shape findings

- `dbo.Books` exists with primary key `Id`, but lacks `Author` and `RowVersion`, which the current EF entity selects.
- `dbo.BookLoans` exists with primary key `Id` and foreign keys to `Books`, `Students`, and `Users`. It uses `StudentId` and `LoanDate`; the current fine repository expects `BorrowerId`, `BorrowDate`, and `dbo.Borrowers`.
- `dbo.Fines` exists with primary key `Id` and foreign keys to `BookLoans` and `Students`, but its columns do not match the current EF `Fine` model.
- Current API expectations for `Members`, `Loans`, and `Borrowers` are not met by this database.

## Fixes and remaining issues

- Updated `appsettings.json` so `ConnectionStrings:DefaultConnection` targets `LibraryDB_Test` on `localhost`. The test run also used an explicit connection override to ensure it could not target production.
- No controller/service/repository fix was applied. The requested modules and DbContexts do not exist in this project, and adding replacements would violate the instruction to test the existing modules without creating duplicates. The current API also targets a schema that differs from `LibraryDB_Test`.
- To complete the requested workflows, provide the project checkout containing the Legacy and New Books Module APIs, or authorize implementing those modules and aligning the current API to the existing test schema. Any required schema changes remain out of scope and were not performed.

## Safety

All SQL inspection was read-only and scoped to `LibraryDB_Test`. API requests in this run were GET-only. No insert, update, delete, migration, or schema modification was performed. The SQL connection was verified with `sqlcmd`; no SSMS GUI session was used.
