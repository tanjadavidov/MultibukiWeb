using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.UserControls
{
    public partial class ucPocetna : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                gridVestiPopuni();
                gridKorisniLinkoviPopuni();
                //gridDokumetna_Popuni();
               
            }
        }

        private void PrikaziObavestenje(string poruka)
        {
            try
            {
                poruka = (poruka.Replace("\n", "")).Replace("'", "");
                poruka = (poruka.Replace("\r", "")).Replace("'", "");
                string script = "alert('" + poruka + "');";
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "Прикази обавештење", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
            }
        }

        protected void gridVestiPopuni()
        {
            //TraceLogging.TraceLogger.trace(this.GetType()
            //    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();

            wcfMultibuki.VratiVestDetaljnoRequest zahtev = new wcfMultibuki.VratiVestDetaljnoRequest();
            wcfMultibuki.VratiVestDetaljnoResponse odgovor = new wcfMultibuki.VratiVestDetaljnoResponse();

            try
            {
                odgovor = client.VratiVestDetaljno(zahtev);
            }
            catch (Exception ex)
            {
                //ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                //    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u proceduri gridVestiPopuni pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiVestDetaljnoResult.dtGreska.Rows.Count > 0)
                    PrikaziObavestenje(odgovor.VratiVestDetaljnoResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    Session["ucPocenta_gvVesti_Lista"] = odgovor.VratiVestDetaljnoResult.dtVratiVestDetaljno.Rows;
                    gvVesti.DataSource = odgovor.VratiVestDetaljnoResult.dtVratiVestDetaljno.Rows;
                    gvVesti.DataBind();
                }
            }
            catch (Exception ex)
            {
                //ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                //    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u aplikaciji u metodi gridVestiPopuni!  \\n\\n " + ex.Message);
                return;
            }
        }

        protected void gvVesti_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int IdVesti = int.Parse(gvVesti.DataKeys[e.Row.RowIndex].Value.ToString());
                TextBox tbTekstVesti_Izabrani = e.Row.FindControl("tbTekstVesti") as TextBox;
                tbTekstVesti_Izabrani.ReadOnly = true;
                tbTekstVesti_Izabrani.Text = DateTime.Now.ToString();

                //TraceLogging.TraceLogger.trace(this.GetType()
                //, (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

                wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
                wcfMultibuki.VratiSelectZaIDVestRequest zahtev = new wcfMultibuki.VratiSelectZaIDVestRequest(IdVesti);
                wcfMultibuki.VratiSelectZaIDVestResponse odgovor = new wcfMultibuki.VratiSelectZaIDVestResponse();

                try
                {
                    odgovor = client.VratiSelectZaIDVest(zahtev);
                }
                catch (Exception ex)
                {
                    //ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    //    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    PrikaziObavestenje("Greška u proceduri gvVesti_OnRowDataBound pri pozivu servisa!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.VratiSelectZaIDVestResult.dtGreska.Rows.Count > 0)
                        PrikaziObavestenje(odgovor.VratiSelectZaIDVestResult.dtGreska.Rows[0][0].ToString());
                    else
                    {
                        tbTekstVesti_Izabrani.Text = odgovor.VratiSelectZaIDVestResult.dtVratiSelectZaIDVest.Rows[0]["Tekst"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    //ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    //    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                    PrikaziObavestenje("Greška u aplikaciji u metodi gvVesti_OnRowDataBound!  \\n\\n " + ex.Message);
                    return;
                }
            }
        }

        protected void gvVesti_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvVesti.PageIndex = e.NewPageIndex;
                DataRowCollection tabelaIzSesije = (DataRowCollection)Session["ucPocenta_gvVesti_Lista"];
                if (tabelaIzSesije != null)
                {
                    gvVesti.DataSource = tabelaIzSesije;
                    gvVesti.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u proceduri gvVesti_PageIndexChanging pri pozivu vesti!  \\n\\n" + ex);
            }
        }

      
        protected void gridKorisniLinkoviPopuni()
        {
            TraceLogging.TraceLogger.trace(this.GetType()
                , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiKorisniLinkDetaljnoRequest zahtev = new wcfMultibuki.VratiKorisniLinkDetaljnoRequest();
            wcfMultibuki.VratiKorisniLinkDetaljnoResponse odgovor = new wcfMultibuki.VratiKorisniLinkDetaljnoResponse();

            try
            {
                odgovor = client.VratiKorisniLinkDetaljno(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u proceduri gridKorisniLinkovi_Popuni pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiKorisniLinkDetaljnoResult.dtGreska.Rows.Count > 0)
                    PrikaziObavestenje(odgovor.VratiKorisniLinkDetaljnoResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    Session["ucPocenta_gvKorisniLinkovi_Lista"] = odgovor.VratiKorisniLinkDetaljnoResult.dtVratiKorisniLinkDetaljno.Rows;
                    gvKorisniLinkovi.DataSource = odgovor.VratiKorisniLinkDetaljnoResult.dtVratiKorisniLinkDetaljno.Rows;
                    gvKorisniLinkovi.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u aplikaciji u metodi gvKorisniLinkovi_Popuni!  \\n\\n " + ex.Message);
                return;
            }
        }

        protected void btnIdiNaKorisniLink_Click(object sender, EventArgs e)
        {
            string url = "";
            try
            {
                GridViewRow red = (GridViewRow)(((Button)sender).Parent).Parent;
                int rowIndex = red.RowIndex;
                url = gvKorisniLinkovi.DataKeys[rowIndex].Values[0].ToString();
            }
            catch (Exception ex)
            {
                PrikaziObavestenje("Greška u proceduri btnIdiNaKorisniLink_Click pri prozivu korisnog linka!  \\n\\n" + ex.Message);
                return;
            }
            finally
            {
                if (url != "")
                {
                    Response.Write("<script>");
                    Response.Write("window.open('" + url + "','_blank')");
                    Response.Write("</script>");
                }
            }
        }

        protected void gvKorisniLinkovi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvKorisniLinkovi.PageIndex = e.NewPageIndex;
                DataRowCollection tabelaIzSesije = (DataRowCollection)Session["ucPocenta_gvKorisniLinkovi_Lista"];
                if (tabelaIzSesije != null)
                {
                    gvKorisniLinkovi.DataSource = tabelaIzSesije;
                    gvKorisniLinkovi.DataBind();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                PrikaziObavestenje("Greška u proceduri gvKorisniLinkovi_PageIndexChanging pri prikazu korisnih linkova!  \\n\\n" + ex);
            }
        }

    }
}