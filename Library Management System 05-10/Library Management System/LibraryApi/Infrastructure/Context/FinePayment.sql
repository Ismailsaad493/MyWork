IF OBJECT_ID(N'dbo.Fines', N'U') IS NULL
    THROW 50001, 'dbo.Fines must exist before applying the fine payment update.', 1;
GO

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
