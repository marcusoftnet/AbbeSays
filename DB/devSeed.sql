-- Seed with development data
DELETE FROM Tags
DELETE FROM Comments
DELETE FROM Quotes
DELETE FROM QuoteTag
DELETE FROM Kids
DELETE FROM Users
GO

INSERT INTO Users (Name, Email, UserName) VALUES ('Marcus', 'marcusoft.net@gmail.com', 'marcusoftnet')
GO

Declare @parentId as Int
SELECT @parentId = Id FROM Users WHERE Name = 'Marcus'

INSERT INTO Kids (Name, BirthDate, ParentId) VALUES ('Albert', '2008-01-24', @parentId)
INSERT INTO Kids (Name, BirthDate, ParentId) VALUES ('Arvid', '2010-03-19', @parentId)
INSERT INTO Kids (Name, BirthDate, ParentId) VALUES ('Gustav', '2010-03-19', @parentId)
GO

Declare @albertId as Int
SELECT @albertId = Id FROM Kids WHERE Name = 'Albert'
INSERT INTO Quotes (KidId, Quote, SaidAt) VALUES (@albertId, 'Då blir jag sur', '2011-11-01')
INSERT INTO Quotes (KidId, Quote, SaidAt) VALUES (@albertId, 'Jag är kär i Alexandra', '2011-11-22')
INSERT INTO Quotes (KidId, Quote, SaidAt) VALUES (@albertId, 'Vad söt han är', '2011-11-12')
GO


