using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class global_Address : iSabayaControl
{
    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;

    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    private bool enabled = true;

    public bool Enabled
    {
        get { return this.enabled; }
        set { this.enabled = value; }
    }

    private bool visibleNodeType = false;

    public bool VisibleNodeType
    {
        get { return this.visibleNodeType; }
        set { this.visibleNodeType = value; }
    }

    public Table TableContent
    {
        get { return this.tableContent; }
    }

    public bool isSetCSSStyle
    {
        get;
        set;
    }

    public TreeListNode AddressNodeType
    {
        get { return ctrlAddressNode.SelectedNode; }
        set { ctrlAddressNode.SelectedNode = value; }
    }

    private String title;

    public String Title
    {
        set
        {
            this.title = value;
        }
    }

    private Unit width = Unit.Pixel(170);

    public Unit Width
    {
        get { return this.width; }
        set { this.width = value; }
    }

    private String[] titleList = new String[] { "ประเภทที่อยู่", "เลขที่", "อาคาร", "ชั้น", "ห้อง", "ซอย - ถนน", "ตำบล/แขวง", "รหัสไปรษณีย์",
                    "ประเทศ","จังหวัด","เขต/อำเภอ","โทรศัพท์","แฟกซ์","","","","","",""};

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (!Page.IsCallback)
        {
            InitializeControls();
        }

        ctrlRegionLevel1.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(ctrlRegionLevel1_Callback);
        ctrlRegionLevel2.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(ctrlRegionLevel2_Callback);
    }

    private void ctrlRegionLevel1_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        String input = e.Parameter;
        if (!string.IsNullOrEmpty(input))
        {
            int countryID = int.Parse(input);
            Country country = Country.Find(iSabayaContext, countryID);
            if (country != null)
            {
                //ctrlRegionLevel1.ParentNode = iSabayaContext.Configuration.CountryParentNode.GetChild(country.Code3);
                ctrlRegionLevel1.DataBind();
            }
            ctrlRegionLevel1.Text = string.Empty;
        }
        else
        {
            ctrlRegionLevel1.ParentNode = null;
            ctrlRegionLevel1.DataBind();
        }
    }

    public void resetHiddenField()
    {
    }

    private void ctrlRegionLevel2_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        String input = e.Parameter;
        if (!string.IsNullOrEmpty(input))
        {
            ctrlRegionLevel2.ParentNodeID = Convert.ToInt32(input);
            ctrlRegionLevel2.DataBind();
            ctrlRegionLevel2.Text = string.Empty;
        }

        else
        {
            ctrlRegionLevel2.ParentNode = null;
            ctrlRegionLevel2.DataBind();
        }
        ctrlRegionLevel2.Text = string.Empty;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.EnableControls(true);
        if (IsRequiredField && this.enabled)
        {
            //เลขที่
            mlsAddressNo.IsRequiredField = this.IsRequiredField;
            mlsAddressNo.ValidationGroup = this.ValidationGroup;
            //ประเทศ
            cboCountry.SetValidation(ValidationGroup);
            //จังหวัด
            ctrlRegionLevel1.SetValidation(this.ValidationGroup);
            //อำเภอ
            ctrlRegionLevel2.SetValidation(this.ValidationGroup);
            //รหัสไปรษณีย์
            txtPostalCode.SetValidation(this.ValidationGroup);
            ctrlAddressNode.SetValidation(this.ValidationGroup);
        }
    }

    public void InitializeControls()
    {
        if (isSetCSSStyle)
        {
            String cssPostfix = txtFax.CssPostfix;
            tableContent.SetCssHtmlTable_V(cssPostfix);
        }
        if (VisibleNodeType)
        {
            tableContent.setTableRowVisible(true, 0);
            ctrlAddressNode.ParentNode = iSabayaContext.Configuration.GeographicAddressCategoryRootNode;
            ctrlAddressNode.DataBind();
        }
        else
        {
            tableContent.setTableRowVisible(false, 0);
        }
        tableContent.setLabelTitle(titleList);

        ctrlAddressNode.Width = this.Width;
        mlsAddressNo.Width = this.Width;
        mlsBuilding.Width = this.Width;
        txtFloor.Width = this.Width;
        txtRoomNo.Width = this.Width;
        mlsAddressNo.Width = this.Width;
        mlsStreet1.Width = this.Width;
        mlsStreet2.Width = this.Width;
        txtPostalCode.Width = this.Width;
        cboCountry.Width = this.Width;
        ctrlRegionLevel1.Width = this.Width;
        ctrlRegionLevel2.Width = this.Width;
        txtPhone.Width = this.Width;
        txtFax.Width = this.Width;

        cboCountry.ValueType = typeof(int);
        IList<Country> countrys = Country.List(iSabayaContext);
        foreach (Country country in countrys)
            cboCountry.Items.Add(country.Name.ToString(base.LanguageCode), country.ID);
        cboCountry.SelectedIndex = -1;

        ctrlRegionLevel1.ClientInstanceName = this.ClientID + ctrlRegionLevel1.ID;
        ctrlRegionLevel2.ClientInstanceName = this.ClientID + ctrlRegionLevel2.ID;
        cboCountry.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
        {
            var item = s.GetSelectedItem();
            if(item != null)
            {
                " + ctrlRegionLevel1.ClientInstanceName + @".PerformCallback(item.value);
                hdfAddess.Set('Country',item.value);
            }
            else
            {
                " + ctrlRegionLevel1.ClientInstanceName + @".PerformCallback();
            }
            " + ctrlRegionLevel2.ClientInstanceName + @".ClearItems();
        }";
        ctrlRegionLevel1.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
        {
            var item = s.GetSelectedItem();
            var index = s.GetSelectedIndex();
            if(item != null)
            {
                " + ctrlRegionLevel2.ClientInstanceName + @".PerformCallback(item.value);
                hdfAddess.Set('Province',item.value);
                hdfAddess.Set('ProvinceIndex',index);
            }
        }";

        ctrlRegionLevel2.ClientSideEvents.SelectedIndexChanged = @"function(s,e)
        {
            var item = s.GetSelectedItem();
            var index = s.GetSelectedIndex();
            hdfAddess.Set('Distinct',item.value);
            hdfAddess.Set('DistinctIndex',index);
        }";

        ctrlRegionLevel1.ClientSideEvents.Init = @"function(s,e)
        {
            var node = hdfAddess.Get('Country');
            var index = hdfAddess.Get('ProvinceIndex');
            if( index != null )
                " + ctrlRegionLevel1.ClientInstanceName + @".PerformCallback( node );
        }";

        ctrlRegionLevel2.ClientSideEvents.Init = @"function(s,e)
        {
            var node = hdfAddess.Get('Province');
            var index = hdfAddess.Get('DistinctIndex');
            if( index != null )
                " + ctrlRegionLevel2.ClientInstanceName + @".PerformCallback( node );
        }";

        ctrlRegionLevel1.ClientSideEvents.EndCallback = @"function(s,e)
        {
            var index = hdfAddess.Get('ProvinceIndex');
            if( index != null )
                " + ctrlRegionLevel1.ClientInstanceName + @".SetSelectedIndex( index );
        }";

        ctrlRegionLevel2.ClientSideEvents.EndCallback = @"function(s,e)
        {
            var index = hdfAddess.Get('DistinctIndex');
            if( index != null )
                " + ctrlRegionLevel1.ClientInstanceName + @".SetSelectedIndex( index );
        }";
    }

    public void EnableControls(bool enabled)
    {
        mlsAddressNo.Enabled = enabled;
        mlsBuilding.Enabled = enabled;
        txtFloor.ClientEnabled = enabled;
        txtRoomNo.ClientEnabled = enabled;
        mlsAddressNo.Enabled = enabled;
        mlsStreet1.Enabled = enabled;
        mlsStreet2.Enabled = enabled;
        txtPostalCode.ClientEnabled = enabled;
        cboCountry.ClientEnabled = enabled;
        ctrlRegionLevel1.ClientEnabled = enabled;
        ctrlRegionLevel2.ClientEnabled = enabled;
        txtPhone.ClientEnabled = enabled;
        txtFax.ClientEnabled = enabled;
    }

    public void setTableToConfirmStage()
    {
        tableContent.setTableColumnToConfirm();
    }

    public new GeographicAddress Value
    {
        get
        {
            GeographicAddress address = null;
            if (!string.IsNullOrEmpty(hddAddressID.Value))
                address = GeographicAddress.Find(iSabayaContext, int.Parse(hddAddressID.Value));
            if (address == null)
                address = new GeographicAddress();
            address.AddressNo = mlsAddressNo.Value;//บ้านเลขที่
            address.Building = mlsBuilding.Value;//อาคาร
            address.RoomNo = txtRoomNo.Text;
            address.Floor = txtFloor.Text;//ชั้น
            address.Street1 = mlsStreet1.Value;//ซอย //ถนน
            address.Street2 = mlsStreet2.Value; //ตำบล
            address.PostalCode = txtPostalCode.Text;
            address.Phones = txtPhone.Text;
            address.Faxes = txtFax.Text;
            if (cboCountry.SelectedItem != null)
            {
                int countryID = Convert.ToInt32(cboCountry.SelectedItem.Value);
                if (address.Country != null && address.Country.ID != countryID)
                    address.Country = Country.Find(iSabayaContext, countryID);
                else if (address.Country == null)
                    address.Country = Country.Find(iSabayaContext, countryID);
            }
            else
                address.Country = null;
            // Comment out by Kunakorn Unknown error
            //if (address.RegionLevel1 == null || address.RegionLevel1.NodeID != Convert.ToInt32(ctrlRegionLevel1.SelectedItemValue))
            //    address.RegionLevel1 = ctrlRegionLevel1.SelectedNode;
            //if (address.RegionLevel2 == null || address.RegionLevel2.NodeID != Convert.ToInt32(ctrlRegionLevel2.SelectedItemValue))
            //    address.RegionLevel2 = ctrlRegionLevel2.SelectedNode;
            return address;
        }
        set
        {
            GeographicAddress address = value;
            //this.EnableControls(this.enabled);
            if (value != null)
            {
                hddAddressID.Value = value.ID.ToString();

                mlsAddressNo.Value = address.AddressNo;
                mlsBuilding.Value = address.Building;
                txtRoomNo.Text = address.RoomNo;
                txtFloor.Text = address.Floor;
                mlsStreet1.Value = address.Street1;
                mlsStreet2.Value = address.Street2;
                txtPostalCode.Text = address.PostalCode;
                txtPhone.Text = address.Phones;
                txtFax.Text = address.Faxes;
                if (address.Country != null)
                {
                    ListEditItem item = cboCountry.Items.FindByValue(address.Country.ID);
                    cboCountry.SelectedItem = item;
                }
                // Comment out by Kunakorn Unknown error
                //ctrlRegionLevel1.ParentNode = address.RegionLevel1.Parent;
                //ctrlRegionLevel1.DataBind();
                //ctrlRegionLevel1.SelectedNode = address.RegionLevel1;

                //ctrlRegionLevel2.ParentNode = address.RegionLevel2.Parent;
                //ctrlRegionLevel2.DataBind();
                //ctrlRegionLevel2.SelectedNode = address.RegionLevel2;
            }
            else
            {
                hddAddressID.Value = string.Empty;
                mlsAddressNo.Value = null;
                mlsBuilding.Value = null;
                txtFloor.Text = string.Empty;
                txtRoomNo.Text = string.Empty;
                mlsStreet1.Value = null;
                mlsStreet2.Value = null;
                txtPostalCode.Text = string.Empty;
                txtPhone.Text = string.Empty;
                txtFax.Text = string.Empty;
                Country country = iSabayaContext.Configuration.DefaultCountry;
                ListEditItem item = cboCountry.Items.FindByValue(country.ID);

                cboCountry.SelectedItem = item;
                //TreeListNode countryNode = iSabayaContext.Configuration.CountryParentNode.GetChild(country.Code3);
                //ctrlRegionLevel1.ParentNode = countryNode;
                ctrlRegionLevel1.DataBindItems();
                ctrlRegionLevel1.SelectedItem = null;
                ctrlRegionLevel2.ParentNode = null;
                ctrlRegionLevel2.DataBindItems();
                ctrlRegionLevel2.SelectedItem = null;
                hdfAddess.Clear();
            }
        }
    }
}