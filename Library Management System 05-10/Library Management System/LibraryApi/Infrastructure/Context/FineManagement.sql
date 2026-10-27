IF OBJECT_ID(N'dbo.Fines', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Fines
    (
        Id int IDENTITY(1, 1) NOT NULL,
        BookLoanId int NOT NULL,
        FineType nvarchar(30) NOT NULL,
        Amount decimal(18, 2) NOT NULL,
        CreatedAtUtc datetime2 NOT NULL
            CONSTRAINT DF_Fines_CreatedAtUtc DEFAULT SYSUTCDATETIME(),
        PaymentDate date NULL,
        PaymentMethod nvarchar(10) NULL,
        CONSTRAINT PK_Fines PRIMARY KEY (Id),
        CONSTRAINT FK_Fines_BookLoans_BookLoanId
            FOREIGN KEY (BookLoanId) REFERENCES dbo.BookLoans (Id),
        CONSTRAINT UQ_Fines_BookLoanId_FineType UNIQUE (BookLoanId, FineType),
        CONSTRAINT CK_Fines_FineType
            CHECK (FineType IN (N'BookDamage', N'LateSubmission')),
        CONSTRAINT CK_Fines_Amount
            CHECK
            (
                (FineType = N'BookDamage' AND Amount = 0)
                OR (FineType = N'LateSubmission' AND Amount > 0)
            ),
        CONSTRAINT CK_Fines_PaymentDetails
            CHECK
            (
                (PaymentDate IS NULL AND PaymentMethod IS NULL)
                OR (PaymentDate IS NOT NULL AND PaymentMethod IN (N'UPI', N'Cash'))
            )
    );
END;
GO

-- Additive upgrade for databases where dbo.Fines already exists.
IF COL_LENGTH(N'dbo.Fines', N'PaymentDate') IS NULL
    ALTER TABLE dbo.Fines ADD PaymentDate date NULL;
GO

IF COL_LENGTH(N'dbo.Fines', N'PaymentMethod') IS NULL
    ALTER TABLE dbo.Fines ADD PaymentMethod nvarchar(10) NULL;
GO

IF NOT EXISTS
(
    SELECT 1 FROM sys.check_constraints
    WHERE name = N'CK_Fines_PaymentDetails'
      AND parent_object_id = OBJECT_ID(N'dbo.Fines')
)
    ALTER TABLE dbo.Fines WITH CHECK ADD CONSTRAINT CK_Fines_PaymentDetails
        CHECK
        (
            (PaymentDate IS NULL AND PaymentMethod IS NULL)
            OR (PaymentDate IS NOT NULL AND PaymentMethod IN (N'UPI', N'Cash'))
        );
GO
