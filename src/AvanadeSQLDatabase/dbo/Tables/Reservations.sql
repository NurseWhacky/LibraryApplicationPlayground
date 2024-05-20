CREATE TABLE [dbo].[Reservations] (
    [Id]        BIGINT   NOT NULL,
    [UserId]    BIGINT   NOT NULL,
    [BookId]    BIGINT   NOT NULL,
    [StartDate] DATETIME NOT NULL,
    [EndDate]   DATETIME NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

