# Library API

An ASP.NET Core 10 API for managing books, members, and loan history. Requests and responses use DTOs; controllers delegate to services; repositories own EF Core queries and persistence.

## Requirements

- .NET 10 SDK
- A reachable SQL Server instance

The default connection targets `localhost` and the `LibraryDB` database using Windows authentication. Override it with `ConnectionStrings__DefaultConnection` or edit `appsettings.json` for your SQL Server instance.

## Add the fines table to the existing database

The existing `LibraryDB` uses `BookLoans` and `Borrowers`. Apply the additive schema script from this directory; it creates only `dbo.Fines` and relates each fine to its existing book-loan record:

```powershell
sqlcmd -C -S localhost -d LibraryDB -E -i Infrastructure/Context/FineManagement.sql
```

For a database where `dbo.Fines` already exists, apply the additive payment columns before running the updated API:

```powershell
sqlcmd -C -S localhost -d LibraryDB -E -i Infrastructure/Context/FinePayment.sql
```

## Run

```powershell
dotnet run --launch-profile https
```

In Development, the OpenAPI document is available at `/openapi/v1.json`. `LibraryApi.http` contains sample requests.

## Endpoints

| Method | Route | Purpose |
| --- | --- | --- |
| GET | `/api/books?author=...` | List books, optionally filtered by author |
| GET | `/api/books/{id}` | Get a book |
| POST, PUT, DELETE | `/api/books`, `/api/books/{id}` | Create, update, and delete books |
| GET | `/api/members`, `/api/members/{id}` | List members or get one member |
| POST, PUT, DELETE | `/api/members`, `/api/members/{id}` | Create, update, and delete members |
| GET | `/api/loans`, `/api/loans/{id}` | List loan history or get one loan |
| POST | `/api/loans` | Check out a book |
| POST | `/api/loans/{id}/return` | Return an active loan |
| GET | `/api/fines`, `/api/fines/{id}` | List fines or get one fine |
| POST | `/api/fines` | Add a Book Damage or Late Submission fine |
| POST | `/api/fines/{id}/payment` | Record a fine payment using UPI or Cash |

Invalid requests return 400 Problem Details; missing records return 404; duplicate or conflicting operations return 409. Books and members with loan history cannot be deleted, preserving the loan record.

Fine requests include `fineType` (`"Book Damage"` or `"Late Submission"`) and `bookId`. Book Damage fines are stored as ₹0 and omit `amount`; Late Submission fines require a positive `amount` and apply to the latest returned, overdue loan for that book. Fine records retain their loan association and include the book and borrower in responses.

Payment requests require `paymentDate` in `YYYY-MM-DD` format and `paymentMethod` set to `UPI` or `Cash`. Payment dates use SQL Server's `date` type and are returned as calendar dates, so client timezone conversion cannot shift the displayed day. Payment fields are nullable for existing unpaid fines; the endpoint records one payment per positive-value fine. Fine responses include `paymentDate` and `paymentMethod` for display in client applications.
