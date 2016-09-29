using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using BizPortal;
using DevExpress.Web.ASPxGridView;
using iSabaya;
using NHibernate;

namespace WebHelper.ServiceLayer
{
    public class BillPaymentService : Service
    {
        /// <summary>
        /// Get NameWithoutAffixes of user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetName(UserAction user)
        {
            return user != null ? user.ByUser.Person.NameWithoutAffixes : "N/A";
        }

        /// <summary>
        /// Get date time form user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetDateTime(UserAction user, string format = "")
        {
            string dateTime;
            if (format != "")
                dateTime = user != null ? user.Timestamp.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture) : "N/A";
            else
                dateTime = user != null ? user.Timestamp.ToString(format, CultureInfo.InvariantCulture) : "N/A";
            return dateTime;
        }

        /// <summary>
        /// Delete target Bracket in gridview of visibleIndwx
        /// </summary>
        /// <param name="brackets"></param>
        /// <param name="gridView"></param>
        /// <param name="visibleIndex"></param>
        /// <param name="targetIndex"></param>
        /// <param name="lowerBound"></param>
        /// <param name="mockBracketId"></param>
        public static void DeleteBarcket(ref IList<FeeBracket> brackets, ASPxGridView gridView, string visibleIndex,
            ref int targetIndex, ref string lowerBound, ref int mockBracketId)
        {
            IList<FeeBracket> deletes = brackets;
            int size = deletes.Count - 1; // array start 0

            FeeBracket feeBracket = (FeeBracket)gridView.GetRow(Int32.Parse(visibleIndex));
            int index = feeBracket.TempID;
            targetIndex = index;
            lowerBound = feeBracket.LowerBound.ToString();

            for (int i = size; i >= index; i--)
            {
                deletes.RemoveAt(i);
                --mockBracketId;
            }
            brackets = deletes;
            switch (index)
            {
                case 0:
                    mockBracketId = 0;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Create bracket
        /// </summary>
        /// <param name="brackets"></param>
        /// <param name="mockBracketId"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="fixOwner"></param>
        /// <param name="rateOwner"></param>
        /// <param name="minOwner"></param>
        /// <param name="maxOwner"></param>
        /// <param name="fixRecive"></param>
        /// <param name="rateRecive"></param>
        /// <param name="minRecive"></param>
        /// <param name="maxRecive"></param>
        ///
        public static void CreateBracket(ref IList<FeeBracket> brackets, ref int mockBracketId, string lowerBound, string upperBound,
            string fixOwner, string rateOwner, string minOwner, string maxOwner,
            string fixRecive, string rateRecive, string minRecive, string maxRecive)
        {
            if (brackets == null)
            {
                IList<FeeBracket> moneys = new List<FeeBracket>
                {
                    new FeeBracket
                    {
                        TempID = mockBracketId++,
                        LowerBound = new Money(decimal.Parse(lowerBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        UpperBound = new Money(decimal.Parse(upperBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        Rate = new DoubleFeeRate
                        {
                            SenderFeeRate = new FeeRate
                            {
                                FixedAmount = new Money(decimal.Parse(fixOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                PercentageRate = double.Parse(rateOwner),
                                MinAmount = new Money(decimal.Parse(minOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                MaxAmount = new Money(decimal.Parse(maxOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                            },
                            ReceiverFeeRate = new FeeRate
                            {
                                FixedAmount = new Money(decimal.Parse(fixRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                PercentageRate = double.Parse(rateRecive),
                                MinAmount = new Money(decimal.Parse(minRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                MaxAmount = new Money(decimal.Parse(maxRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                            }
                        }
                    }
                };
                brackets = moneys;
            }
            else
            {
                IList<FeeBracket> moneys = brackets;

                moneys.Add(new FeeBracket
                {
                    TempID = mockBracketId++,
                    LowerBound = new Money(decimal.Parse(lowerBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                    UpperBound = new Money(decimal.Parse(upperBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),

                    //SeqNo = moneys.Count,
                    Rate = new DoubleFeeRate
                    {
                        SenderFeeRate = new FeeRate
                        {
                            FixedAmount = new Money(decimal.Parse(fixOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            PercentageRate = double.Parse(rateOwner),
                            MinAmount = new Money(decimal.Parse(minOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            MaxAmount = new Money(decimal.Parse(maxOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                        },
                        ReceiverFeeRate = new FeeRate
                        {
                            FixedAmount = new Money(decimal.Parse(fixRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            PercentageRate = double.Parse(rateRecive),
                            MinAmount = new Money(decimal.Parse(minRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            MaxAmount = new Money(decimal.Parse(maxRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                        }
                    }
                });
                brackets = moneys;
            }
        }

        public static Biller updateFeeSchdule(BizPortalSessionContext context, Biller biller, IList<FeeBracket> updateBrackets)
        {
            FeeSchedule feeSchedule = biller.CurrentFeeSchedule.FeeSchedule;
            foreach (FeeBracket orginalBracket in feeSchedule.Brackets)
            {
                foreach (FeeBracket updateBracket in updateBrackets)
                {
                    if (orginalBracket.BracketedRateID == updateBracket.BracketedRateID)
                    {
                        orginalBracket.LowerBound = updateBracket.LowerBound;
                        orginalBracket.UpperBound = updateBracket.UpperBound;
                        orginalBracket.Rate.SenderFeeRate.FixedAmount = updateBracket.Rate.SenderFeeRate.FixedAmount;
                        orginalBracket.Rate.SenderFeeRate.PercentageRate = updateBracket.Rate.SenderFeeRate.PercentageRate;
                        orginalBracket.Rate.SenderFeeRate.MinAmount = updateBracket.Rate.SenderFeeRate.MinAmount;
                        orginalBracket.Rate.SenderFeeRate.MaxAmount = updateBracket.Rate.SenderFeeRate.MaxAmount;
                        orginalBracket.Rate.ReceiverFeeRate.FixedAmount = updateBracket.Rate.ReceiverFeeRate.FixedAmount;
                        orginalBracket.Rate.ReceiverFeeRate.PercentageRate = updateBracket.Rate.ReceiverFeeRate.PercentageRate;
                        orginalBracket.Rate.ReceiverFeeRate.MinAmount = updateBracket.Rate.ReceiverFeeRate.MinAmount;
                        orginalBracket.Rate.ReceiverFeeRate.MaxAmount = updateBracket.Rate.ReceiverFeeRate.MaxAmount;
                        break;
                    }
                }
            }
            biller.Persist(context);
            return biller;
        }

        /// <summary>
        /// Create Biller
        /// </summary>
        /// <param name="context"></param>
        /// <param name="billerCatagory"></param>
        /// <param name="brackets"></param>
        /// <param name="ref1"></param>
        /// <param name="ref2"></param>
        /// <param name="ref3"></param>
        /// <param name="ref4"></param>
        /// <param name="servicName"></param>
        /// <param name="serviceCode"></param>
        /// <param name="detail"></param>
        /// <param name="effectiveForm"></param>
        /// <param name="effectiveTo"></param>
        /// <returns></returns>
        public static Biller CreateBilller(BizPortalSessionContext context, int billerCatagory, ref IList<FeeBracket> brackets,
            string ref1, string ref2, string ref3, string ref4,
            string servicName, string code, string detail, DateTime effectiveForm, DateTime effectiveTo)
        {
            string currencyCode = "";
            if (context.CurrentCurrency != null)
            {
                currencyCode = context.CurrentCurrency.Code;
            }
            string langeCode = "";
            if (context.CurrentLanguage != null)
            {
                langeCode = context.CurrentLanguage.Code;
            }

            TreeListNode parent = context.Configuration.BillerCategoryRootNode;
            TreeListNode category = null;
            if (parent != null)
                for (int i = 0; i < parent.Children.Count; i++)
                {
                    if (i == billerCatagory)
                    {
                        category = parent.Children[i];
                        break;
                    }
                }

            ServiceFeeSchedule feeSchdule = new ServiceFeeSchedule
            {
                EffectivePeriod = new TimeInterval(),
                CreateAction = new UserAction(context.User),
                LanguageCode = langeCode,
                MaximumFeePerTransaction = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                MinimumFeePerTransaction = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),

                FeeSchedule = new FeeSchedule
                {
                    Brackets = brackets,
                    LanguageCode = langeCode,
                    RateType = RateType.FixedRateOnly,
                    CreateAction = new UserAction(context.User),
                    CreatedBy = context.User,
                    CreatedTS = DateTime.Now,
                    EffectivePeriod = new TimeInterval(),
                    MaximumFeePerDay = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                    MaximumFeePerTransaction = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                }
            };

            Biller biller = new Biller
            {
                FeeCurrencyCode = currencyCode,
                BillerCode = "", //get 1P
                ServiceCode = "", //get 1p
                Code = code,
                Category = category,
                Ref1Title = new MultilingualString(new MLSValue("th-TH", ref1 != "" ? ref1 : "")),
                Ref2Title = new MultilingualString(new MLSValue("th-TH", ref2 != "" ? ref2 : "")),
                Ref3Title = new MultilingualString(new MLSValue("th-TH", ref3 != "" ? ref3 : "")),
                Ref4Title = new MultilingualString(new MLSValue("th-TH", ref4 != "" ? ref4 : "")),
                Title = new MultilingualString(new MLSValue("th-TH", servicName)),
                EffectivePeriod = new TimeInterval(effectiveForm, effectiveTo),
                CreateAction = new UserAction(context.User),
                Description = new MultilingualString(new string[] { "th-TH", detail }),
            };
            biller.FeeSchedules.Add(feeSchdule);
            biller.CurrentFeeSchedule = feeSchdule;

            //Session["tempservice"] = b;
            return biller;
        }

        public static Biller UpdateBiller(BizPortalSessionContext context, Biller biller, string accuntNo,
            string ref1, string ref2, string ref3, string ref4,
            string servicName, string code, string detail, DateTime effectiveForm, DateTime effectiveTo)
        {
            BankAccount bankAccount = null;
            if (biller.CurrentBillerBankAccount == null)
            {
                biller.CurrentBillerBankAccount = CreateBillerBankAccount(context, accuntNo, biller);
            }
            else
            {
                if (biller.CurrentBillerBankAccount.BankAccount.AccountNo != accuntNo)
                {
                    bankAccount = CreateBillerBankAccount(context, accuntNo, biller).BankAccount;

                    //if (bankAccount == null)
                    //{
                    //    warringCount++;
                    //    message = "บัญชีนี้มีอยู่ในระบบแล้ว";
                    //}
                    //else
                    biller.CurrentBillerBankAccount.BankAccount = bankAccount;
                }
            }
            biller.Code = code;
            biller.Title = new MultilingualString(new MLSValue("th-TH", servicName));
            biller.EffectivePeriod = new TimeInterval(effectiveForm, effectiveTo);
            biller.CreateAction = new UserAction(context.User);
            biller.Description = new MultilingualString(new string[] { "th-TH", detail });
            biller.Ref1Title = new MultilingualString(new string[] { "th-TH", ref1 });
            biller.Ref2Title = new MultilingualString(new string[] { "th-TH", ref2 });
            biller.Ref3Title = new MultilingualString(new string[] { "th-TH", ref3 });
            biller.Ref4Title = new MultilingualString(new string[] { "th-TH", ref4 });
            biller.Persist(context);
            return biller;
        }

        /// <summary>
        /// Save biller instance
        /// </summary>
        /// <param name="context"></param>
        /// <param name="account"></param>
        /// <param name="biller"></param>
        /// <param name="warningCount"></param>
        /// <param name="message"></param>
        public static void Save(BizPortalSessionContext context, BankAccount account, Biller biller,
            ref int warningCount, ref string message)
        {
            int FunctionID = (int)BankMakerFunctionID.AddBankServiceFeeSchedule;
            string lange = context.CurrentLanguage.Code;

            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (account == null)
                    {
                        warningCount++;
                        message = "บัญชีนี้มีอยู่ในระบบแล้ว";
                    }
                    if (biller == null)
                    {
                        warningCount++;
                        message = "บริการชำระค่าสาธารณูปโภค";
                    }

                    if (warningCount == 0)
                    {
                        biller.CurrentBillerBankAccount = new BillerBankAccount
                        {
                            BankAccount = account,
                            CreateAction = new UserAction(context.User),
                            EffectivePeriod = new TimeInterval(),
                            LanguageCode = lange,
                        };

                        biller.CreateAction = new UserAction(context.User);
                        biller.Persist(context);

                        message += String.Format("- {0} {1}",
                                    "เพิ่มบริการชำระค่าสาธารณูปโภค",
                                     biller.Title.ToString(lange));
                        tx.Commit();

                        context.Log(FunctionID, 0, 0, "เพิ่มบริการชำระค่าสาธารณูปโภค", message);
                        message += Messages.Genaral.Success.Format(lange);
                    }
                }
                catch (Exception ex)
                {
                    warningCount++;
                    tx.Rollback();
                    context.Log(FunctionID, 0, 0, "เพิ่มบริการชำระค่าสาธารณูปโภค",
                            message + Messages.Genaral.TransactionException.Format(lange, ex.Message));
                }
            }
        }

        public static void Update(BizPortalSessionContext context, Biller biller,
            ref int warningCount, ref string message)
        {
            int FunctionID = (int)BankMakerFunctionID.AddBankServiceFeeSchedule;
            string lange = context.CurrentLanguage.Code;

            using (ITransaction tx = context.PersistenceSession.BeginTransaction())
            {
                try
                {
                    if (biller == null)
                    {
                        warningCount++;
                        message = "บริการชำระค่าสาธารณูปโภค";
                    }

                    if (warningCount == 0)
                    {
                        biller.CreateAction = new UserAction(context.User);
                        biller.Persist(context);

                        message += String.Format("- {0} {1}",
                                    "การแก้ไขตารางค่าธรรมสาธารณูปโภค",
                                     biller.Title.ToString(lange));
                        tx.Commit();

                        context.Log(FunctionID, 0, 0, "การแก้ไขตารางค่าธรรมสาธารณูปโภค", message);
                        message += Messages.Genaral.Success.Format(lange);
                    }
                }
                catch (Exception ex)
                {
                    warningCount++;
                    tx.Rollback();
                    context.Log(FunctionID, 0, 0, "การแก้ไขตารางค่าธรรมสาธารณูปโภค",
                            message + Messages.Genaral.TransactionException.Format(lange, ex.Message));
                }
            }
        }

        public static BankAccount CheckBankAccountInSystem(BizPortalSessionContext context, string accountNo)
        {
            BankAccount bankAccount = null;
            foreach (BankAccount item in context.PersistenceSession
                                                       .QueryOver<BankAccount>()
                                                       .List<BankAccount>())
            {
                if (item.AccountNo == accountNo)
                {
                    bankAccount = item;
                    break;
                }
            }
            return bankAccount;
        }

        public static BillerBankAccount CreateBillerBankAccount(BizPortalSessionContext context, string accountNo, Biller biller)
        {
            BillerBankAccount billerBankAccount = new BillerBankAccount();

            BankAccount acc = CheckBankAccountInSystem(context, accountNo);

            //has system set Effactive reuse bank Account
            if (acc != null)
            {
                //if (acc.IsEffective)
                //{
                billerBankAccount.BankAccount = null;

                //}
                //else
                //{
                //    //acc.EffectivePeriod = new TimeInterval();
                //    billerBankAccount.BankAccount = acc;
                //}
            }

            // no system create new account give for biller
            else
            {
                //billerBankAccount.BankAccount = MTIB_Inhouse.Inhouse_verification(accountNo, "022");

                Organization bank = context.PersistenceSession
                    .QueryOver<Organization>()
                    .Where(org => org.Code == "CIMBT")
                    .SingleOrDefault();
                OrgUnit orgUnit = context.PersistenceSession
                    .QueryOver<OrgUnit>()
                    .Where(orgU => orgU.Code == "00001" && orgU.OrganizationParent == bank)
                    .SingleOrDefault();

                if (orgUnit == null)
                {
                    orgUnit = context.PersistenceSession
                       .QueryOver<OrgUnit>()
                       .Where(orgU => orgU.Code == "0001" && orgU.OrganizationParent == bank)
                       .SingleOrDefault();
                }

                billerBankAccount.BankAccount = new BankAccount
                {
                    AccountNo = accountNo,
                    AccountName = new MultilingualString("th-TH", "Test", "en-US", "Test"),
                    AccountType = BankAccountType.Current,
                    Bank = bank,
                    EffectivePeriod = new TimeInterval(),
                    CurrencyCode = context.CurrentCurrency.ISOCode,
                    LanguageCode = context.CurrentLanguage.Code,
                    UniqueAccountNo = accountNo,
                    Branch = orgUnit,
                    BranchCode = orgUnit.Code,
                    CreateAction = new UserAction(context.User),
                };
                billerBankAccount.EffectivePeriod = new TimeInterval();
                if (billerBankAccount.Biller == null)
                {
                    billerBankAccount.Biller = biller;
                    billerBankAccount.Biller.EffectivePeriod = new TimeInterval();
                }
            }
            return billerBankAccount;
        }

        //IR
        public static void CreateZone(BizPortalSessionContext context, ref IList<IRFeeSchedule> zone, ref int zoneCount, ref int count, string fromZone, string toZone)
        {
            if (zone == null)
            {
                zoneCount = 0;
                count = 0;
                zone = new List<IRFeeSchedule>
                {
                    new IRFeeSchedule
                    {
                        TempID = zoneCount,
                        FromRegion = Int32.Parse(fromZone),
                        ToRegion = Int32.Parse(toZone),
                        EffectivePeriod = new TimeInterval(DateTime.Now),

                        //IsReverselyApplicable = (bool)zoneIsReverselyApplicable.Value,
                        CreateAction = new UserAction(context.User),
                        FeeSchedule = new FeeSchedule
                        {
                            CreateAction = new UserAction(context.User),
                            CreatedTS = DateTime.Now,
                            MaximumFeePerDay = new Money(0m ,BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            MaximumFeePerTransaction = new Money(0m,BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            Brackets = new List<FeeBracket>(),
                        },
                    },
                };
            }
            else
            {
                IList<IRFeeSchedule> irFeeSchedule = zone;
                irFeeSchedule.Add(new IRFeeSchedule
                {
                    TempID = zoneCount,
                    FromRegion = Int32.Parse(fromZone),
                    ToRegion = Int32.Parse(toZone),
                    EffectivePeriod = new TimeInterval(DateTime.Now),

                    //IsReverselyApplicable = (bool)zoneIsReverselyApplicable.Value,
                    CreateAction = new UserAction(context.User),
                    FeeSchedule = new FeeSchedule
                    {
                        MaximumFeePerDay = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        MaximumFeePerTransaction = new Money(0m, BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        CreateAction = new UserAction(context.User),
                        CreatedTS = DateTime.Now,
                        Brackets = new List<FeeBracket>(),
                    },
                });

                //Session[SESSION_ZONE_IR] = irFeeSchedule;
            }
        }

        public static void CreateBracketIR(ref IList<FeeBracket> brackets, ref int mockBracketId, string lowerBound, string upperBound,
            string fixOwner, string rateOwner, string minOwner, string maxOwner,
            string fixRecive, string rateRecive, string minRecive, string maxRecive)
        {
            if (brackets == null)
            {
                IList<FeeBracket> moneys = new List<FeeBracket>
                {
                    new FeeBracket
                    {
                        TempID = mockBracketId++,
                        LowerBound = new Money(decimal.Parse(lowerBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        UpperBound = new Money(decimal.Parse(upperBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                        Rate = new DoubleFeeRate
                        {
                            SenderFeeRate = new FeeRate
                            {
                                FixedAmount = new Money(decimal.Parse(fixOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                PercentageRate = double.Parse(rateOwner),
                                MinAmount = new Money(decimal.Parse(minOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                MaxAmount = new Money(decimal.Parse(maxOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                            },
                            ReceiverFeeRate = new FeeRate
                            {
                                FixedAmount = new Money(decimal.Parse(fixRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                PercentageRate = double.Parse(rateRecive),
                                MinAmount = new Money(decimal.Parse(minRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                                MaxAmount = new Money(decimal.Parse(maxRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                            }
                        }
                    }
                };
                brackets = moneys;
            }
            else
            {
                IList<FeeBracket> moneys = brackets;

                moneys.Add(new FeeBracket
                {
                    TempID = mockBracketId++,
                    LowerBound = new Money(decimal.Parse(lowerBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                    UpperBound = new Money(decimal.Parse(upperBound), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),

                    //SeqNo = moneys.Count,
                    Rate = new DoubleFeeRate
                    {
                        SenderFeeRate = new FeeRate
                        {
                            FixedAmount = new Money(decimal.Parse(fixOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            PercentageRate = double.Parse(rateOwner),
                            MinAmount = new Money(decimal.Parse(minOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            MaxAmount = new Money(decimal.Parse(maxOwner), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                        },
                        ReceiverFeeRate = new FeeRate
                        {
                            FixedAmount = new Money(decimal.Parse(fixRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            PercentageRate = double.Parse(rateRecive),
                            MinAmount = new Money(decimal.Parse(minRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode),
                            MaxAmount = new Money(decimal.Parse(maxRecive), BizPortalConfiguration.CurrentConfiguration.DefaultCurrency.ISOCode)
                        }
                    }
                });
                brackets = moneys;
            }
        }
    }
}