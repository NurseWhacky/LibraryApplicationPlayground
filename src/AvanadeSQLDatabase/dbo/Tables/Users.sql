CREATE TABLE [dbo].[Users] (
    [Id]       BIGINT        NOT NULL,
    [Username] NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [Role]     NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CHECK ([Role]='User' OR [Role]='Admin'),
    UNIQUE NONCLUSTERED ([Username] ASC)
);

