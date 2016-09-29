using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using iSabaya;
using Budget.General;
using Budget;

namespace InitializeDatabase
{
    [System.Runtime.InteropServices.GuidAttribute("4AFE1D32-E43D-4594-91B9-AC7B80149DE8")]
    class Program
    {
        private static SessionContext context;
        private static Language currentLanguage;
        static void Main(string[] args)
        {
            SetContext();

            TruncateTable(
                "MLSValue",
                "MultilingualString",
                "Organization",
                "OrgUnit",
                "OrgName",
                "Configuration",
                "Users",
                "Person",
                "PersonName",
                "Role",
                "UserRole",
                "UserSession",
                "UserSessionLog",
                "Project",
                "UserOrgUnit",
                "Announce", 
                "GoodGovernance",
                "Strategic"
            );
            InitLanguages();
            //InitGovernmentUnits();
            new InitialOrganization(context);
            InitConfigurations();
            InitRoles();
            InitAdminUser();

        }

        private static void InitAdminUser()
        {
            Organization org = context.PersistenceSession.Get<Organization>(1L);
            OrgUnit orgUnit = org.OrgUnits.Where(x => x.Code == "01007").SingleOrDefault();
            SelfAuthenticatedUser admin = new SelfAuthenticatedUser
            (
                SystemEnum.RiskAssessmentAdminSystem, //SessionContext.MySystem.SystemID,
                org, //org,
                orgUnit, //orgUnit,
                "0000000000000",//idCard,
                "admin",//loginName,
                "แอดมิน",//firstNameTH,
                "admin",//firstNameEN, 
                "แอดมิน",//lastNameTH, 
                "admin",//lastNameEN,
                "",//middleNameTH, 
                "",//middleNameEN,
                "",//email,
                "",//mobilePhone, 
                "",//telephone, 
                "",//toNumber, 
                "",//directTelephone
                "",//Address
                true
            );

            admin.EffectivePeriod = TimeInterval.EffectiveNow;
            admin.UserRoles = new List<UserRole>
            {
                new UserRole{
                    User = admin,
                    Role = context.PersistenceSession.QueryOver<Role>().Where(x => x.SystemID == SystemEnum.RiskAssessmentAdminSystem).SingleOrDefault(),
                    EffectivePeriod = TimeInterval.EffectiveNow,
                }
            };

            admin.Persist(context);
        }

        private static void InitRoles()
        {
            Role user = new Role
            {
                Code = "User",
                Description = "ส่วนราชการ, รัฐวิสาหกิจ, หน่วยงานอื่นของรัฐ, จังหวัดหรือกลุ่มจังหวัด",
                SystemID = SystemEnum.RiskAssessmentProjectOwnerSystem
            };
            user.Save(context);

            Role budgetingOfficer = new Role
            {
                Code = "BudgetingOfficer",
                Description = "เจ้าหน้าที่จัดทำงบประมาณ(สำนักงบประมาณ)",
                SystemID = SystemEnum.RiskAssessmentAnalysisSystem
            };
            budgetingOfficer.Save(context);

            Role evaluator = new Role
            {
                Code = "Evaluator",
                Description = "เจ้าหน้าที่ประเมินผล(สำนักงบประมาณ)",
                SystemID = SystemEnum.RiskAssessmentAnalysisSystem
            };
            evaluator.Save(context);

            Role admin = new Role
            {
                Code = "Admin",
                Description = "ผู้ดูแลระบบ(สำนักงบประมาณ)",
                SystemID = SystemEnum.RiskAssessmentAdminSystem
            };
            admin.Save(context);
        }

        private static void InitConfigurations()
        {
            BudgetConfiguration adminConfig = new BudgetConfiguration();
            adminConfig.EffectivePeriod = TimeInterval.EffectiveNow;
            adminConfig.DefaultLanguage = currentLanguage;
            adminConfig.SystemID = SystemEnum.RiskAssessmentAdminSystem;
            adminConfig.Security = new SecurityConfig
            {
                MaxUsernameLength = 100,
                MinUsernameLength = 5,
                WebSessionTimeoutValueInMinutes = 15
            };

            adminConfig.Persist(context);

            BudgetConfiguration analysisConfig = new BudgetConfiguration();
            analysisConfig.EffectivePeriod = TimeInterval.EffectiveNow;
            analysisConfig.DefaultLanguage = currentLanguage;
            analysisConfig.SystemID = SystemEnum.RiskAssessmentAnalysisSystem;
            analysisConfig.Security = new SecurityConfig
            {
                MaxUsernameLength = 100,
                MinUsernameLength = 5,
                WebSessionTimeoutValueInMinutes = 15
            };

            analysisConfig.Persist(context);

            BudgetConfiguration projectOwnerConfig = new BudgetConfiguration();
            projectOwnerConfig.EffectivePeriod = TimeInterval.EffectiveNow;
            projectOwnerConfig.DefaultLanguage = currentLanguage;
            projectOwnerConfig.SystemID = SystemEnum.RiskAssessmentProjectOwnerSystem;
            projectOwnerConfig.Security = new SecurityConfig
            {
                MaxUsernameLength = 100,
                MinUsernameLength = 5,
                WebSessionTimeoutValueInMinutes = 15
            };

            projectOwnerConfig.Persist(context);
        }

        private static void InitLanguages()
        {
            PersistLanguage("th-TH");
            PersistLanguage("en-US");
        }

        private static void PersistLanguage(string langCode)
        {

            Language language = new Language(langCode);

            if (langCode == "th-TH")
                currentLanguage = language;

            language.Save(context);

        }



        private static void InitGovernmentUnits()
        {
            PersistGovernmentUnit
            (
                "01000", new OrgName { Name = new MultilingualString("th-TH", "สำนักนายกรัฐมนตรี", "en-US", "Office of the Prime Minister") },
                new List<OrgUnit>
                {
                    new OrgUnit{ Code = "01001", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "สำนักงานปลัดสำนักนายกรัฐมนตรี", "en-US", "Office of the Secretariat") }},
                    new OrgUnit{ Code = "01002", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                    new OrgUnit{ Code = "01003", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                    new OrgUnit{ Code = "01004", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                    new OrgUnit{ Code = "01005", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                    new OrgUnit{ Code = "01006", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                    new OrgUnit{ Code = "01007", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
                }
            );
            //PersistGovernmentUnit
            //(
            //    "02000", new OrgName { Name = new MultilingualString("th-TH", "กระทรวงกลาโหม", "en-US", "Ministry of Defence") },
            //    new List<OrgUnit>
            //    {
            //        new OrgUnit{ Code = "02001", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "สำนักงานปลัดกระทรวงกลาโหม", "en-US", "Office of the Secretariat") }},
            //        new OrgUnit{ Code = "02002", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //        new OrgUnit{ Code = "02003", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //        new OrgUnit{ Code = "02004", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //        new OrgUnit{ Code = "02005", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //        new OrgUnit{ Code = "02006", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //        new OrgUnit{ Code = "02007", CurrentName = new OrgName{ Name = new MultilingualString("th-TH", "", "en-US", "") }},
            //    }
            //);
        }

        private static void PersistGovernmentUnit(string code, OrgName name, IList<OrgUnit> orgunits)
        {
            var govUnit = new Organization
            {
                Code = code,
                CurrentName = name,
                OrgUnits = orgunits
            };
            govUnit.Persist(context);
        }

        public static void SetContext()
        {
            try
            {
                var hConfiguration = new NHibernate.Cfg.Configuration();
                hConfiguration.AddAssembly("BudgetORM");
                hConfiguration.AddAssembly("iSabayaORM");
                hConfiguration.AddAssembly("iSabaya.ExtensibleORM");


                ISessionFactory sessionFactory = hConfiguration.BuildSessionFactory();

                context = new SessionContext(new iSystem(SystemEnum.RiskAssessmentAdminSystem), sessionFactory);
                if (context == null)
                    throw new Exception("Can't create context.");
            }
            catch (Exception exc)
            {

            }
        }

        public static void TruncateTable(params string[] tableNames)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string tn in tableNames)
            {
                sb.Append("truncate table ");
                sb.Append(tn);
                sb.Append(";");
            }
            context.PersistenceSession.CreateSQLQuery(sb.ToString()).ExecuteUpdate();
        }
    }
}
