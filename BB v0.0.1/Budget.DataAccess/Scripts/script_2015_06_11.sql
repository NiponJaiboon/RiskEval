
GO
CREATE TABLE [dbo].[Language] (
    [LanguageCode]    NCHAR (5)      NOT NULL,
    [SeqNo]           INT            NULL,
    [ShortTitle]      NVARCHAR (50)  NULL,
    [SmallImageBytes] VARCHAR (MAX)  NULL,
    [Title]           NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([LanguageCode] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);
GO
PRINT N'Creating [dbo].[Language]...';

GO
CREATE TABLE [dbo].[OrgUnit] (
    [OrgID]             INT            IDENTITY (1, 1) NOT NULL,
    [Code]              NVARCHAR (20)  NULL,
    [CurrentNameID]     INT            NULL,
    [EffectiveFrom]     DATETIME       NULL,
    [EffectiveTo]       DATETIME       NULL,
    [HolidayCalendarID] INT            NULL,
    [LevelNo]           INT            NULL,
    [OfficialIDNo]      NVARCHAR (20)  NULL,
    [ClearingZone]      INT            NULL,
    [BahtNetZone]       INT            NULL,
    [BranchStatus]      INT            NULL,
    [OrderedDate]       DATETIME       NULL,
    [OrgParentID]       INT            NULL,
    [Properties]        INT            NULL,
    [Reference]         NVARCHAR (100) NULL,
    [Remark]            NVARCHAR (300) NULL,
    [CreatedBy]         INT            NULL,
    [CreatedTS]         DATETIME       NULL,
    [ApprovedBy]        INT            NULL,
    [ApprovedTS]        DATETIME       NULL,
    [UpdatedTS]         DATETIME       NULL,
    [UpdatedBy]         INT            NULL,
    [URL]               NVARCHAR (100) NULL,
    [WorkCalendarID]    INT            NULL,
	[IsActive]			BIT			   NULL
);
GO
PRINT N'Creating [dbo].[OrgUnit]...';

GO
ALTER TABLE [dbo].[OrgUnit]
    ADD CONSTRAINT [OrgUnit_PK] PRIMARY KEY CLUSTERED ([OrgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating OrgUnit_PK...';

GO
CREATE NONCLUSTERED INDEX [IX_OrgUnit_OfficialIDNo]
    ON [dbo].[OrgUnit]([OfficialIDNo] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];
	
GO
PRINT N'Creating [dbo].[OrgUnit].[IX_OrgUnit_OfficialIDNo]...';

GO
CREATE TABLE [dbo].[OrgName] (
    [OrgNameID]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]               NVARCHAR (50)  NULL,
    [EffectiveFrom]      DATETIME       NULL,
    [EffectiveTo]        DATETIME       NULL,
    [OrderedDate]        DATETIME       NULL,
    [OwnerID]            INT            NULL,
    [OwnerDiscriminator] TINYINT        NULL,
    [NameMLSID]          INT            NULL,
    [Reference]          NVARCHAR (120) NULL,
    [Remark]             NVARCHAR (250) NULL,
    [ShortNameMLSID]     INT            NULL,
    [UpdatedTS]          DATETIME       NULL,
    [UpdatedBy]          INT            NULL,
    [PackageName]        NVARCHAR (50)  NULL
);

GO
PRINT N'Creating [dbo].[OrgName]...';

GO
ALTER TABLE [dbo].[OrgName]
    ADD CONSTRAINT [PK_OrgName] PRIMARY KEY CLUSTERED ([OrgNameID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating PK_OrgName...';

GO
CREATE TABLE [dbo].[Organization] (
    [OrgID]                   INT            IDENTITY (1, 1) NOT NULL,
    [AddressID]               INT            NULL,
    [CategoryNodeID]          INT            NULL,
    [Code]                    NVARCHAR (50)  NULL,
    [ContactNameMLSID]        INT            NULL,
    [CurrentNameID]           INT            NULL,
    [EffectiveFrom]           DATETIME       NULL,
    [EffectiveTo]             DATETIME       NULL,
    [Email]                   NVARCHAR (50)  NULL,
    [HolidayCalendarID]       INT            NULL,
    [ImageFileName]           NVARCHAR (100) NULL,
    [MobilePhone]             NVARCHAR (50)  NULL,
    [NationalityID]           INT            NULL,
    [OfficialIDNo]            NVARCHAR (20)  NULL,
    [OrderedDate]             DATETIME       NULL,
    [PersonOrgRelationRootID] INT            NULL,
    [PersonRoleRootID]        INT            NULL,
    [Phone]                   NVARCHAR (50)  NULL,
    [Properties]              INT            NULL,
    [Reference]               NVARCHAR (50)  NULL,
    [Remark]                  NVARCHAR (250) NULL,
    [CreatedBy]               INT            NULL,
    [CreatedTS]               DATETIME       NULL,
    [ApprovedBy]              INT            NULL,
    [ApprovedTS]              DATETIME       NULL,
    [UpdatedBy]               INT            NULL,
    [UpdatedTS]               DATETIME       NULL,
    [URL]                     NVARCHAR (50)  NULL,
    [WorkCalendarID]          INT            NULL,
    [PackageName]             NVARCHAR (50)  NULL,
	[IsActive]				  BIT			 NULL	
);

GO
PRINT N'Creating [dbo].[Organization]...';

GO
ALTER TABLE [dbo].[Organization]
    ADD CONSTRAINT [Organization_PK] PRIMARY KEY CLUSTERED ([OrgID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
GO
PRINT N'Creating Organization_PK...';

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Organization_1]
    ON [dbo].[Organization]([Code] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

GO
PRINT N'Creating [dbo].[Organization].[IX_Organization_1]...';

GO
CREATE NONCLUSTERED INDEX [IX_Organization_OfficialIDNo]
    ON [dbo].[Organization]([OfficialIDNo] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

GO
PRINT N'Creating [dbo].[Organization].[IX_Organization_OfficialIDNo]...';

GO
CREATE TABLE [dbo].[MultilingualString] (
    [MLSID]       INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (300) NULL,
    [Code]        NVARCHAR (50)  NULL,
    [PackageName] NVARCHAR (50)  NULL,
    [Category]    TINYINT        NULL
);

GO
PRINT N'Creating [dbo].[MultilingualString]...';

GO
ALTER TABLE [dbo].[MultilingualString]
    ADD CONSTRAINT [MultilingualString_PK] PRIMARY KEY CLUSTERED ([MLSID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

	GO
PRINT N'Creating MultilingualString_PK...';

GO
CREATE TABLE [dbo].[MLSValue] (
    [MLSValueID]   INT             IDENTITY (1, 1) NOT NULL,
    [MLSID]        INT             NOT NULL,
    [LanguageCode] NVARCHAR (8)    NOT NULL,
    [Value]        NVARCHAR (1000) NULL,
    [UpdatedBy]    INT             NULL,
    [UpdatedTS]    DATETIME        NULL,
    [PackageName]  NVARCHAR (50)   NULL
);

GO
PRINT N'Creating [dbo].[MLSValue]...';


GO
ALTER TABLE [dbo].[MLSValue]
    ADD CONSTRAINT [PK_MLSValue] PRIMARY KEY CLUSTERED ([MLSID] ASC, [LanguageCode] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating PK_MLSValue...';


GO
CREATE TABLE [dbo].[Configuration] (
    [ConfigurationID]                                  INT            IDENTITY (1, 1) NOT NULL,
    --[BankAccountCategoryRootNodeID]                    INT            NULL,
    --[BankOrgCategoryNodeID]                            INT            NULL,
    --[BillerCategoryRootNodeID]                         INT            NULL,
    --[BloodGroupParentNodeID]                           INT            NULL,
    --[CapitalGainTaxScheduleID]                         INT            NULL,
    --[ChequeNoToStringFormat]                           NVARCHAR (20)  NULL,
    --[CountryParentNodeID]                              INT            NULL,
    --[ContactCategoryNodeID]                            INT            NULL,
    --[DefaultCountryID]                                 INT            NULL,
    --[DefaultCurrencyCode]                              NCHAR (3)      NULL,
    [DefaultLanguageCode]                              NVARCHAR (8)   NULL,
    --[DefaultNationalityNodeID]                         INT            NULL,
    [EffectiveFrom]                                    DATETIME       NULL,
    [EffectiveTo]                                      DATETIME       NULL,
    --[GenderParentNodeID]                               INT            NULL,
    --[GeographicAddressCategoryRootNodeID]              INT            NULL,
    --[IncomeTaxScheduleID]                              INT            NULL,
    --[IDCategoriesRootNodeID]                           INT            NULL,
    --[NationalityParentNodeID]                          INT            NULL,
    --[NonworkCalendarID]                                INT            NULL,
    --[MAPSWebServiceAddress]                            NVARCHAR (100) NULL,
    --[SMSWebServiceAddress]                             NVARCHAR (100) NULL,
    --[PrimaryLDAPServerAddress]                         NVARCHAR (100) NULL,
    --[SecondaryLDAPServerAddress]                       NVARCHAR (100) NULL,
    --[RegisteredAddressCategoryRootNodeID]              INT            NULL,
    --[OtherAddressCategoryRootNode]                     INT            NULL,
    --[ResidentAddressCategoryRootNodeID]                INT            NULL,
    --[MaxConsecutiveFailedLogonAttempts]                INT            NULL,
    --[MaxDaysOfInactivity]                              INT            NULL,
    --[MinPasswordLength]                                INT            NULL,
    --[MinNumberOfSpecialCharsInPassword]                INT            NULL,
    --[MinNumberOfCapitalLettersInPassword]              INT            NULL,
    --[MinNumberOfSmallLettersInPassword]                INT            NULL,
    --[MinNumberOfDigitsInPassword]                      INT            NULL,
    --[PasswordAgeInDays]                                INT            NULL,
    --[PasswordHistoryDepth]                             INT            NULL,
    --[OrganizationAttributeKeyParentNodeID]             INT            NULL,
    --[OrganizationBusinessCategoryRootNodeID]           INT            NULL,
    --[OrganizationCategoryBankNodeID]                   INT            NULL,
    --[OrganizationCategoryRootNodeID]                   INT            NULL,
    --[OrganizationCategorySellingAgentNodeID]           INT            NULL,
    --[OrganizationIdentityCategoryParentNodeID]         INT            NULL,
    --[OrganizationNationalityParentNodeID]              INT            NULL,
    --[OrganizationPersonRelationshipCategoryRootNodeID] INT            NULL,
    --[PersonAddressCategoryParentNodeID]                INT            NULL,
    --[PersonAttributeKeyParentNodeID]                   INT            NULL,
    --[PersonCategoryParentNodeID]                       INT            NULL,
    --[PersonIdentityCategoryParentNodeID]               INT            NULL,
    --[PersonNamePrefixParentNodeID]                     INT            NULL,
    --[PersonNameSuffixParentNodeID]                     INT            NULL,
    --[PersonOccupationParentNodeID]                     INT            NULL,
    --[ReligionParentNodeID]                             INT            NULL,
    --[RelationshipCategoryParentNodeID]                 INT            NULL,
    --[ReserveCategoryRootNodeID]                        INT            NULL,
    --[SalesTaxRate]                                     FLOAT          NULL,
    --[SalesTaxScheduleID]                               INT            NULL,
    --[ScheduleCategoryParentNodeID]                     INT            NULL,
    --[SequenceNumberGeneratingRuleID]                   INT            NULL,
    --[ServiceTaxRate]                                   FLOAT          NULL,
    --[SystemOwnerOrgID]                                 INT            NULL,
    --[TaxScheduleCategoryRootNodeID]                    INT            NULL,
    [UpdatedTS]                                        DATETIME       NULL,
    [UpdatedBy]                                        INT            NULL,
	[CreatedTS]										   DATETIME		  NULL,	
	[CreatedBy]										   INT			  NULL,	
	[ApprovedTS]									   DATETIME		  NULL,	
	[ApprovedBy]									   INT			  NULL,
    --[WithholdDividendTaxRate]                          FLOAT          NULL,
    --[WithholdSalesTaxRate]                             FLOAT          NULL,
    --[WithholdServiceTaxRate]                           FLOAT          NULL,
    --[WorkCalendarID]                                   INT            NULL,
	[MaxUsernameLength]								   INT				NULL,
    [MinUsernameLength]								   INT				NULL,
    [WebSessionTimeoutValueInMinutes]				   INT				NULL,
	[SystemID]										   INT				NULL,
	[IsNotFinalized]								   BIT				NULL
);

GO
PRINT N'Creating [dbo].[Configuration]...';


GO
ALTER TABLE [dbo].[Configuration]
    ADD CONSTRAINT [PK_Configuration] PRIMARY KEY CLUSTERED ([ConfigurationID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating PK_Configuration...';


GO
CREATE TABLE [dbo].[Users] (
    [UserID]                        INT           IDENTITY (1, 1) NOT NULL,
    [Discriminator]                 TINYINT       NULL,
    [CreatedBy]                     INT           NULL,
    [CreatedTS]                     DATETIME      NULL,
    [ApprovedBy]                    INT           NULL,
    [ApprovedTS]                    DATETIME      NULL,
    [LoginName]                     NVARCHAR (50) NULL,
    [LastLoginTimestamp]            DATETIME      NULL,
    [EMailAddress]                  NVARCHAR (50) NULL,
    [MobilePhoneNumber]             NVARCHAR (20) NULL,
	[PhoneCenter]					NVARCHAR (20) NULL,
	[PhoneCenterTo]					NVARCHAR (20) NULL,
	[PhoneDirect]					NVARCHAR (20) NULL,
    [CurrentPasswordID]             INT           NULL,
    [EffectiveFrom]                 DATETIME      NULL,
    [EffectiveTo]                   DATETIME      NULL,
    [IsAutomaticSchedule]           BIT           NULL,
    [IsNotFinalized]                BIT           NULL,
    [IsBuiltin]                     BIT           NULL,
    [IsDisable]                     BIT           NULL,
    [IsReinstated]                  BIT           NULL,
    [MustChangePasswordAtNextLogon] BIT           NULL,
    [PasswordNeverExpires]          BIT           NULL,
    [PasswordAgeInDays]             INT           NULL,
    [SystemID]                      INT           NULL,
    [PersonID]                      INT           NULL,
    [OrgID]                         INT           NULL,
	[OrgUnitID]                     INT           NULL,
    [PersonNameID]                  INT           NULL,
	[ConsecutiveFailedLoginCount]	INT			  NULL,
	[LastFailedLoginTimestamp]		DATETIME	  NULL,
	[MustChangePasswordAfterFirstLogon] INT		  NULL
);


GO
PRINT N'Creating PK_User...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating UIX_Users...';


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [UIX_Users] UNIQUE NONCLUSTERED ([LoginName] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF) ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Users].[UIX_User_LoginName]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UIX_User_LoginName]
    ON [dbo].[Users]([LoginName] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];

	GO
PRINT N'Creating [dbo].[Person]...';


GO
CREATE TABLE [dbo].[Person] (
    [PersonID]         INT            IDENTITY (1, 1) NOT NULL,
    [BirthDate]        DATETIME       NULL,
    [BloodGroupNodeID] INT            NULL,
    [CategoryNodeID]   INT            NULL,
    [CitizenCountryID] INT            NULL,
    [CurrentNameID]    INT            NULL,
    [DeceasedDate]     DATETIME       NULL,
    [Email]            NVARCHAR (50)  NULL,
    [Faxes]            NVARCHAR (80)  NULL,
    [GenderNodeID]     INT            NULL,
    [MobilePhone]      NVARCHAR (100) NULL,
    [Nationality]      NVARCHAR (50)  NULL,
    [NationalityID]    INT            NULL,
    [OfficialID]       INT            NULL,
    [OfficialIDNo]     NVARCHAR (20)  NULL,
    [OccupationID]     INT            NULL,
    [OrderedDate]      DATETIME       NOT NULL,
    [Phone]            NVARCHAR (100) NULL,
    [Properties]       INT            NULL,
    [Reference]        NVARCHAR (50)  NULL,
    [ReligionNodeID]   INT            NULL,
    [Remark]           NVARCHAR (250) NULL,
    [CreatedBy]        INT            NULL,
    [CreatedTS]        DATETIME       NULL,
    [ApprovedBy]       INT            NULL,
    [ApprovedTS]       DATETIME       NULL,
    [UpdatedTS]        DATETIME       NULL,
    [UpdatedBy]        INT            NULL,
    [URL]              NVARCHAR (50)  NULL,
    [PackageName]      NVARCHAR (50)  NULL
);


GO
PRINT N'Creating Person_PK...';


GO
ALTER TABLE [dbo].[Person]
    ADD CONSTRAINT [Person_PK] PRIMARY KEY CLUSTERED ([PersonID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[PersonName]...';


GO
CREATE TABLE [dbo].[PersonName] (
    [PersonID]      INT            NOT NULL,
    [PersonNameID]  INT            IDENTITY (1, 1) NOT NULL,
    [EffectiveFrom] DATETIME       NOT NULL,
    [EffectiveTo]   DATETIME       NULL,
    [FirstNameID]   INT            NULL,
    [LastNameID]    INT            NULL,
    [MiddleNameID]  INT            NULL,
    [NamePrefixID]  INT            NULL,
    [NameSuffixID]  INT            NULL,
    [OrderedDate]   DATETIME       NOT NULL,
    [Reference]     NVARCHAR (120) NULL,
    [Remark]        NVARCHAR (120) NULL,
    [UpdatedTS]     DATETIME       NULL,
    [UpdatedBy]     INT            NULL,
    [PackageName]   NVARCHAR (50)  NULL
);


GO
PRINT N'Creating PersonName_PK...';


GO
ALTER TABLE [dbo].[PersonName]
    ADD CONSTRAINT [PersonName_PK] PRIMARY KEY CLUSTERED ([PersonNameID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[PersonName].[IX_PersonName_Person]...';


GO
CREATE NONCLUSTERED INDEX [IX_PersonName_Person]
    ON [dbo].[PersonName]([PersonID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF, ONLINE = OFF, MAXDOP = 0)
    ON [PRIMARY];


GO
PRINT N'Creating [dbo].[Role]...';


GO
CREATE TABLE [dbo].[Role] (
    [RoleId]          INT            IDENTITY (1, 1) NOT NULL,
    [SystemID]        INT            NULL,
    [Code]            NVARCHAR (50)  NULL,
    [Description]     NVARCHAR (100) NULL,
    [IsAdministrator] BIT            NULL,
    [IsBuiltin]       BIT            NULL,
    [PrivilegeLevel]  INT            NOT NULL,
    [SeqNo]           INT            NULL
);


GO
PRINT N'Creating Role_PK...';


GO
ALTER TABLE [dbo].[Role]
    ADD CONSTRAINT [Role_PK] PRIMARY KEY CLUSTERED ([RoleId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

GO
PRINT N'Creating [dbo].[UserRole]...';


GO
CREATE TABLE [dbo].[UserRole] (
    [ID]            INT      IDENTITY (1, 1) NOT NULL,
    [UserID]        INT      NOT NULL,
    [RoleId]        INT      NOT NULL,
    [EffectiveFrom] DATETIME NOT NULL,
    [EffectiveTo]   DATETIME NULL
);


GO
PRINT N'Creating RolePeriod_PK...';


GO
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [RolePeriod_PK] PRIMARY KEY CLUSTERED ([UserID] ASC, [RoleId] ASC, [EffectiveFrom] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);


GO
PRINT N'Creating [dbo].[UserOrgUnit]...';


GO
CREATE TABLE [dbo].[UserOrgUnit] (
    [ID]             INT      IDENTITY (1, 1) NOT NULL,
    [CreatedBy]      INT      NULL,
    [CreatedTS]      DATETIME NULL,
    [ApprovedBy]     INT      NULL,
    [ApprovedTS]     DATETIME NULL,
    [IsNotFinalized] BIT      NOT NULL,
    [EffectiveFrom]  DATETIME NOT NULL,
    [EffectiveTo]    DATETIME NULL,
    [UserID]         INT      NULL,
    [OrgUnitID]        INT      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF)
);

GO
