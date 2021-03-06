using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using iSabaya;
using WebHelper;

public partial class ctrls_RelationShipControl : iSabayaControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IList<Relationship> relationships =
                Relationship.List(iSabayaContext);
            foreach (Relationship c in relationships)
            {
                ComboRelationShip.Items.Add(c.Description.ToString(), c.ID);
            }
            ComboRelationShip.SelectedIndex = 0;
            ComboRelationShip.SetValidation(ValidationGroup, IsRequiredField);
        }
    }

    public Relationship Relationship
    {
        get
        {
            if (ComboRelationShip.SelectedItem == null) return null;
            String id = (String)ComboRelationShip.SelectedItem.Value;
            Relationship curr = Relationship.Find(iSabayaContext, int.Parse(id));
            return curr;
        }
        set
        {
            if (value != null)
            {
                foreach (ListEditItem item in ComboRelationShip.Items)
                {
                    if (item.Value.Equals(value.ID.ToString()))
                    {
                        ComboRelationShip.SelectedItem = item;
                        break;
                    }
                }
            }
        }
    }

    public override string Text
    {
        get
        {
            return Relationship != null ? Relationship.Description.ToString() : "" ;
        }
    }

    #region Validation Section

    private bool isRequiredField = false;

    public bool IsRequiredField
    {
        get { return isRequiredField; }
        set { this.isRequiredField = value; }
    }

    private String validationGroup;

    /// <summary>
    /// Get or sets the group of controls for which the editor forces validation when it posts back to the server.
    /// </summary>
    public String ValidationGroup
    {
        get { return validationGroup; }
        set { this.validationGroup = value; }
    }

    #endregion Validation Section
}