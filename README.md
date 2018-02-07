# Future Processing Election Calculator (Notes)

### Build

TravisCI [![Build Status](https://travis-ci.org/kajstof/FpElectionCalculator.svg?branch=master)](https://travis-ci.org/kajstof/FpElectionCalculator)

CircleCI [![CircleCI](https://circleci.com/gh/kajstof/FpElectionCalculator.svg?style=svg)](https://circleci.com/gh/kajstof/FpElectionCalculator)

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

### Database connections

Azure SQL Access Data:

    Host:     fp-electioncalculator-db.windows.net
    Login:    ksf
    Password: FPElectionCalculator1

Connection string Azure MSSQL:

`@"Server=tcp:fp-electioncalculator-db.database.windows.net,1433;Initial Catalog=fp-electioncalculator-db;Persist Security Info=False;User ID=ksf;Password=FPElectionCalculator1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"`

Connection string LocalDB:

`@"Server=(localdb)\MSSQLLocalDB;Database=fp-electioncalculator-db;Trusted_Connection=True;MultipleActiveResultSets=true"`

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

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Comment] nvarchar(max) NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [PeselHashed] nvarchar(max) NULL,
    [Voted] bit NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
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

CREATE TABLE [Votes] (
    [VoteId] int NOT NULL IDENTITY,
    [CandidateId] int NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Votes] PRIMARY KEY ([VoteId]),
    CONSTRAINT [FK_Votes_Candidates_CandidateId] FOREIGN KEY ([CandidateId]) REFERENCES [Candidates] ([CandidateId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Votes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_Candidates_PartyId] ON [Candidates] ([PartyId]);

GO

CREATE INDEX [IX_Votes_CandidateId] ON [Votes] ([CandidateId]);

GO

CREATE INDEX [IX_Votes_UserId] ON [Votes] ([UserId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20180207103503_CreateElectionDb', N'2.0.1-rtm-125');

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