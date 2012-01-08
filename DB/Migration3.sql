sp_RENAME 'Parents' , 'Families'
GO

sp_RENAME 'Kids.[ParentId]' , 'FamiliyId', 'COLUMN'
GO

BEGIN TRANSACTION
GO
ALTER TABLE dbo.Quotes ADD
	Likes int NOT NULL CONSTRAINT DF_Quotes_NumberOfLikes DEFAULT 0
GO
ALTER TABLE dbo.Quotes SET (LOCK_ESCALATION = TABLE)
GO
COMMIT



