using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.WebForme_UserControle
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !IsCallback)
            {
                ((Label)Page.Master.FindControl("lblNaslov")).Text = "";

                aaaProvera();

                Response.AppendHeader("Cache-Control", "no-cache, no-store, must-revalidate"); // HTTP 1.1.
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.
            }
        }

        protected void aaaProvera()
        {

            //Провери да ли је улогован

            if (Session["Korisnik_IDKorisnik"] == null || Session["Korisnik_IDKorisnik"].ToString() == "")
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_KorisnickoIme"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Ime"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Prezime"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_ePosta"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_Telefon"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_PocetnaLozinka"] == null)
                Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
            if (Session["Korisnik_PocetnaLozinka"].ToString() == "1")
                Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");

            //Провера права приступа
            bool imaPravo = false;

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            red.idFunkcija = 1;   //  ****************************************************************************
            ulaz.dtaaa.AdddtaaaRow(red);
            wcfMultibuki.KorisnikPravoNaFunkcijuRequest zahtev = new wcfMultibuki.KorisnikPravoNaFunkcijuRequest(ulaz);
            wcfMultibuki.KorisnikPravoNaFunkcijuResponse odgovor = new wcfMultibuki.KorisnikPravoNaFunkcijuResponse();

            try
            {
                odgovor = client.KorisnikPravoNaFunkciju(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri KorisnikPravoNaFunkciju pri pozivu servisa za proveru naloga! \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPravoNaFunkcijuResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikPravoNaFunkcijuResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    string sPravo = odgovor.KorisnikPravoNaFunkcijuResult.dtKorisnikPravoNaFunkciju.Rows[0]["PravoNaFunkciju"].ToString();
                    if (sPravo == "1")
                        imaPravo = true;
                    else
                        imaPravo = false;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPravoNaFunkciju! pri proveri naloga!  \\n\\n " + ex.Message);
                return;
            }

            if (!imaPravo)
                Response.Redirect("~/WebForms/AccessDenied.aspx");
        }

        private void ObradaiObavestenje(string poruka)
        {
            try
            {
                poruka = (poruka.Replace("\n", "")).Replace("'", "");
                poruka = (poruka.Replace("\r", "")).Replace("'", "");
                string script = "alert('" + poruka + "');";
                Page page1 = (Page)HttpContext.Current.Handler;
                ScriptManager.RegisterStartupScript(page1, typeof(Page), "Prikaži obaveštenje", script, true);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
            }
        }

    }
}