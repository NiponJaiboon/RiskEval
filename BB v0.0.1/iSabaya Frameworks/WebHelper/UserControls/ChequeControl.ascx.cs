using System;
using iSabaya;
using WebHelper;

public partial class ctrls_ChequeControl : iSabayaControl
{
    //private ISession session;

    //public ISession Session
    //{
    //    get { return session; }
    //    set { session = value; }
    //}

    //private String currentLanguage;

    //public void ChangeLanguage(String currentLanguage)
    //{
    //    this.currentLanguage = currentLanguage;
    //    //change
    //}

    //public void InitializeControls(ISession session, String currentLanguage)
    //{
    //    this.session = session;
    //    this.currentLanguage = currentLanguage;

    //    //BankAccountControl1.InitializeControls(currentLanguage, session);
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        DateCheque.Date = DateTime.Now;
        //OrganizationControlMini1.InitializeControls("th", PersistenceLayer.WebSessionManager.PersistenceSession);
    }

    public Organization Bank
    {
        get
        {
            return BankControl1.Organization;
        }
    }

    public String GetChequeNumber()
    {
        return TextChequeNumber.Text;
    }

    public DateTime GetChequeDate()
    {
        return (DateTime)DateCheque.Date;
    }
}
