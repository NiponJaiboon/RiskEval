GO
PRINT N'Creating [dbo].[UserSession]...';


GO
CREATE TABLE [dbo].[UserSession] (
    [UserSessionID]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [SystemID]             INT           NULL,
    [ApplicationSessionID] NVARCHAR (50) NULL,
    [FromIPAddress]        VARCHAR (50)  NULL,
    [IsTimeOut]            BIT           NULL,
    [SigninTS]             DATETIME      NULL,
    [SignoutTS]            DATETIME      NULL,
    [UserID]               INT           NULL,
	[LoginMessage]		   NTEXT		 NULL,
	[LogoutMessage]		   NTEXT		 NULL,
	[LoginFailed]		   BIT			 NULL,
	[UserName]			   NVARCHAR(40)	 NULL		
);


GO
PRINT N'Creating [dbo].[UserSessionLog]...';


GO
CREATE TABLE [dbo].[UserSessionLog] (
    [ID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [UserSessionID] BIGINT          NULL,
    [FunctionID]    INT             NULL,
    [PageID]        INT             NULL,
    [MenuID]        INT             NULL,
    [Timestamp]     DATETIME        NULL,
    [Action]        NVARCHAR (50)   NULL,
    [Message]       NVARCHAR (2000) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);


GO
PRINT N'Creating [dbo].[UserSessionLog].[IX_UserSessionLog]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserSessionLog]
    ON [dbo].[UserSessionLog]([UserSessionID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


CREATE NONCLUSTERED INDEX [OrgUnit_ParentEffectivePeriod] ON [dbo].[OrgUnit]
(
	[OrgParentID] ASC,
	[EffectiveFrom] ASC,
	[EffectiveTo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO

GO
PRINT N'Creating [dbo].[Project]...';


GO
CREATE TABLE [dbo].[Project] (
	[ID]				BIGINT IDENTITY (1, 1) NOT NULL,
	[ProjectNo]			NVARCHAR(40)		NOT NULL,   
	[Name]				NVARCHAR(500)		NOT NULL,    
    [BudgetType]		TINYINT					NULL,
    [BudgetAmount]		MONEY		   NULL,
    [BudgetYear]		NVARCHAR(4)	   NULL,
	[EffectiveFrom]     DATETIME       NULL,
    [EffectiveTo]       DATETIME       NULL,
	[CreatedBy]         INT            NULL,
    [CreatedTS]         DATETIME       NULL,
    [ApprovedBy]        INT            NULL,
    [ApprovedTS]        DATETIME       NULL,
	[UpdatedBy]         INT            NULL,
    [UpdatedTS]         DATETIME       NULL,
	[OrgUnitID]			INT			   NOT NULL,
	[StrategicID]		INT			   NOT NULL,
	[IsNewProject]		BIT			   NULL,
	[IsInvestment]		BIT			   NULL,
	[IsImportant]		BIT			   NULL,
	[IsRiskAnalysis]	BIT			   NULL,
	[OriginOfProject]	NVARCHAR(4000) NULL,
	[UrgencyOfProject]	NVARCHAR(4000) NULL,
	[ProjectCategory]	TINYINT			   NULL,

);

GO
ALTER TABLE [dbo].[Project]
    ADD CONSTRAINT [Project_PK] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating Project_PK...';


ALTER TABLE [dbo].[Users]
ADD Address NVARCHAR(4000);