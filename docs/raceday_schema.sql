CREATE DATABASE RaceDayDB;
GO
USE RaceDayDB;
GO

CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Organiser', 'Participant')),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Events (
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    OrganiserID INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    EventDate DATETIME NOT NULL,
    Location NVARCHAR(100) NOT NULL,
    RegistrationDeadline DATETIME NOT NULL,
    FOREIGN KEY (OrganiserID) REFERENCES Users(UserID)
);

CREATE TABLE Categories (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    CategoryName NVARCHAR(50) NOT NULL,
    DistanceKm DECIMAL(5,2) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (EventID) REFERENCES Events(EventID) ON DELETE CASCADE
);

CREATE TABLE Enrolments (
    EnrolmentID INT IDENTITY(1,1) PRIMARY KEY,
    ParticipantID INT NOT NULL,
    CategoryID INT NOT NULL,
    EnrolmentDate DATETIME DEFAULT GETDATE(),
    PaymentStatus NVARCHAR(20) DEFAULT 'Pending' CHECK (PaymentStatus IN ('Pending', 'Paid', 'Cancelled')),
    FOREIGN KEY (ParticipantID) REFERENCES Users(UserID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Results (
    ResultID INT IDENTITY(1,1) PRIMARY KEY,
    EnrolmentID INT NOT NULL UNIQUE,
    FinishTime TIME NOT NULL,
    Position INT NOT NULL,
    RecordedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (EnrolmentID) REFERENCES Enrolments(EnrolmentID)
);

CREATE TABLE RouteDetails (
    RouteID INT IDENTITY(1,1) PRIMARY KEY,
    EventID INT NOT NULL,
    ElevationGainMeters INT NOT NULL,
    MapImageUrl NVARCHAR(255),
    RouteDescription NVARCHAR(300),
    FOREIGN KEY (EventID) REFERENCES Events(EventID) ON DELETE CASCADE
);
GO

-- Seed Data (Min requirements: 2 Organisers, 2 Participants, 3 Events, Categories, Enrolments)
INSERT INTO Users (Username, Email, PasswordHash, Role, FirstName, LastName) VALUES
('org_john', 'john@raceday.co.za', 'hash1', 'Organiser', 'John', 'Doe'),
('org_sarah', 'sarah@raceday.co.za', 'hash2', 'Organiser', 'Sarah', 'Smith'),
('part_mike', 'mike@gmail.com', 'hash3', 'Participant', 'Mike', 'Johnson'),
('part_emma', 'emma@gmail.com', 'hash4', 'Participant', 'Emma', 'Brown');

INSERT INTO Events (OrganiserID, Title, Description, EventDate, Location, RegistrationDeadline) VALUES
(1, 'Cape Town Marathon', 'Scenic road marathon.', '2026-10-15 06:00:00', 'Green Point', '2026-10-01 23:59:59'),
(1, 'Two Oceans Half', 'Coastal half marathon.', '2026-11-20 07:00:00', 'Newlands', '2026-11-05 23:59:59'),
(2, 'Soweto Spring Walk', 'Community walk.', '2026-12-01 08:00:00', 'Soweto', '2026-11-15 23:59:59');

INSERT INTO Categories (EventID, CategoryName, DistanceKm, Price) VALUES
(1, 'Full Marathon', 42.20, 550.00),
(1, '10km Run', 10.00, 200.00),
(2, 'Half Marathon', 21.10, 400.00),
(3, '5km Fun Walk', 5.00, 100.00);

INSERT INTO Enrolments (ParticipantID, CategoryID, PaymentStatus) VALUES
(3, 1, 'Paid'),
(3, 2, 'Paid'),
(4, 3, 'Paid'),
(4, 4, 'Pending');

INSERT INTO Results (EnrolmentID, FinishTime, Position) VALUES
(1, '03:15:22', 12),
(3, '01:45:10', 5);

INSERT INTO RouteDetails (EventID, ElevationGainMeters, MapImageUrl, RouteDescription) VALUES
(1, 350, '/images/ct.png', 'Coastal flat route.'),
(2, 420, '/images/to.png', 'Hilly route.'),
(3, 90, '/images/sw.png', 'Flat circuit.');
GO