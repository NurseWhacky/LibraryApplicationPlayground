CREATE TABLE [dbo].[Books] (
    [Id]            BIGINT         NOT NULL,
    [Title]         NVARCHAR (255) NOT NULL,
    [AuthorName]    NVARCHAR (50)  NOT NULL,
    [AuthorSurname] NVARCHAR (50)  NOT NULL,
    [Publisher]     NVARCHAR (50)  NOT NULL,
    [Quantity]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

