/*
   den 28 december 201120:37:18
   User: 
   Server: .\SQLEXPRESS
   Database: AbbeSays
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Kids ADD
	PictureURL varchar(300) NULL
GO
ALTER TABLE dbo.Kids SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
