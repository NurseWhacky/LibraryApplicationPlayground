CREATE DATABASE AvanadeLibrary;
GO;

USE AvanadeLibrary;

-- Create Books table
CREATE TABLE Books (
    Id BIGINT PRIMARY KEY NOT NULL,
    Title NVARCHAR(255) NOT NULL ,
    AuthorName NVARCHAR(50) NOT NULL,
    AuthorSurname NVARCHAR(50) NOT NULL,
    Publisher NVARCHAR(50) NOT NULL,
    Quantity INT NOT NULL
);

-- Create Users table
CREATE TABLE Users (
    Id BIGINT PRIMARY KEY NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(50) CHECK (Role IN ('Admin', 'User')) NOT NULL
);

-- Create Reservations table
CREATE TABLE Reservations (
    Id BIGINT PRIMARY KEY,
    UserId BIGINT NOT NULL,
    BookId BIGINT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (BookId) REFERENCES Books(Id)
);