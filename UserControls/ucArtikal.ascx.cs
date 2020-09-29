using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.UserControls
{
    public partial class ucArtikal : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                //pnlUnos.Visible = false;
                //rblTipLica.SelectedValue = Convert.ToString(1);
                //int TipLica = int.Parse(rblTipLica.SelectedValue);
                //int? Idpospar = null;
                //string search = txtSearch.Text;
                //Ucitaj(TipLica, Idpospar, search);
                //gv.Columns[4].Visible = false;
                //ddlPosPar_popuni(TipLica, Idpospar, search);

            }
        }

        protected void PromenaTipa(object sender, EventArgs e)
        {

        }

        protected void ddlArtikal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }

}