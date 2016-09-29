
ALTER TABLE [dbo].[Announce]
ADD 
	EffectiveFrom DATETIME,
	EffectiveTo DATETIME,
	CreatedBy INT,
	CreatedTS DATETIME,
	UpdatedBy INT,
	UpdatedTS DATETIME
;

ALTER TABLE [dbo].[Strategic]
ADD 
	UpdatedBy INT,
	UpdatedTS DATETIME
;

ALTER TABLE [dbo].[GoodGovernance]
ADD 
	UpdatedBy INT,
	UpdatedTS DATETIME
;

ALTER TABLE [dbo].[Project]
ADD 
	CommentedBy INT,
	CommentedTS DATETIME,
	CommentedRemark NVARCHAR(4000),
	BudgetResult TINYINT,
	BudgetApprovalAmount MONEY
;