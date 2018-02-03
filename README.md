# Future Processing Notes

## Api Queries

### Candidates

```http
POST http://webtask.future-processing.com:8069/candidates HTTP/1.1
Accept: application/xml
```

```http
POST http://webtask.future-processing.com:8069/candidates HTTP/1.1
```

```http
GET http://webtask.future-processing.com:8069/candidates HTTP/1.1
Accept: application/xml
```

```http
GET http://webtask.future-processing.com:8069/candidates HTTP/1.1
```

### Disallowed people

```http
POST http://webtask.future-processing.com:8069/blocked HTTP/1.1
Accept: application/xml
```

```http
POST http://webtask.future-processing.com:8069/blocked HTTP/1.1
```

```http
GET http://webtask.future-processing.com:8069/blocked HTTP/1.1
Accept: application/xml
```

```http
GET http://webtask.future-processing.com:8069/blocked HTTP/1.1
```

---

## Database used in application

### NuGet Package Manager - Entity Framework Tools commands

```
Add-Migration CreateElectionDb -Verbose -Project FpElectionCalculator.Domain -Context ElectionDbContext
Update-Database -Verbose -Project FpElectionCalculator.Domain -Context ElectionDbContext
Script-Migration -Verbose -Project FpElectionCalculator.Domain -Context ElectionDbContext
Remove-Migration -Verbose -Project FpElectionCalculator.Domain -Context ElectionDbContext
```

### Database AzureSQL Connection

Connection string: `@"Server=(localdb)\MSSQLLocalDB;Database=fp-electioncalculator-db;Trusted_Connection=True;MultipleActiveResultSets=true"`
```
    Host:     fp-electioncalculator-db.windows.net
    Login:    ksf
    Password: FPElectionCalculator1
```

### SQL Scripts

#### Creating Database for FP Calculator

```sql
IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Parties] (
    [PartyId] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    CONSTRAINT [PK_Parties] PRIMARY KEY ([PartyId])
);

GO

CREATE TABLE [Candidates] (
    [CandidateId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NULL,
    [PartyId] int NOT NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY ([CandidateId]),
    CONSTRAINT [FK_Candidates_Parties_PartyId] FOREIGN KEY ([PartyId]) REFERENCES [Parties] ([PartyId]) ON DELETE CASCADE
);

GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [CandidateId] int NOT NULL,
    [Comment] nvarchar(max) NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [Pesel] nvarchar(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Candidates_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [Candidates] ([CandidateId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Candidates_PartyId] ON [Candidates] ([PartyId]);

GO

CREATE INDEX [IX_Users_CandidateId] ON [Users] ([CandidateId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180201013832_CreateElectionDb', N'2.0.1-rtm-125');

GO
```


#### Drop tables

```sql
DROP TABLE [dbo].[Votes]
DROP TABLE [dbo].[Parties]
DROP TABLE [dbo].[Candidates]
DROP TABLE [dbo].[Users]
DROP TABLE [dbo].[__EFMigrationsHistory]
```