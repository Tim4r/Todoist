USE TodoistDB

CREATE TABLE Categories
(
Id int IDENTITY(1,1) CONSTRAINT PK_Categories_Id PRIMARY KEY CLUSTERED(Id),
NameCategory nvarchar(30) NOT NULL
)

CREATE TABLE Goals
(
Id int IDENTITY(1,1) CONSTRAINT PK_Goals_Id PRIMARY KEY CLUSTERED(Id),
Title nvarchar(30) NOT NULL,
[Description] nvarchar(100),
Created datetime2(7) NOT NULL,
[Status] nvarchar(20) NOT NULL,
CategoryId int CONSTRAINT FK_Goals_Categories FOREIGN KEY(CategoryId) REFERENCES Categories(Id)
)

INSERT INTO Categories (NameCategory)
VALUES 
('ImportantAndUrgent'),
('ImportantAndNonUrgent'),
('UnimportantAndUrgent'),
('UnimportantAndNonUrgent')

INSERT INTO Goals (Title, [Description], Created, [Status], CategoryId)
VALUES
('Apply for a Master"s programme', 'Until the end of the week!', SYSDATETIME(), 'InProcess', 1),
('Studying the principle of Dependency Injection', 'Preferably in C#', SYSDATETIME(), 'VerificationStage', 2),
('Sort out the mail', 'The name of the email is rulka@gmail.com', SYSDATETIME(), 'InProcess', 3),
('Paint the fence', 'Ask the girl if blue or yellow paint is better', SYSDATETIME(), 'Postponed', 4),
('Hang a shelf in the hallway', 'The shelf is in the pantry, the dowels are missing, need to be purchased', SYSDATETIME(), 'Postponed', 4)
GO