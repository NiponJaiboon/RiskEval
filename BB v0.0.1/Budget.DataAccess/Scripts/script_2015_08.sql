
ALTER TABLE [dbo].[Project]
ADD 
	ProjectType TINYINT NULL,
	Status TINYINT NULL,
	StatusCategory TINYINT NULL,
	RiskResult TINYINT NULL,
	BookNo NVARCHAR(50) NULL,
	BookDate DATETIME NULL
;