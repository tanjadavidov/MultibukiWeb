using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.UserControls
{
    public partial class ucPromenaLozinkeKorisnika : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
               
                pnlUnos.Visible = false;
                tbImePrezime.Enabled = true;
                tbImePrezime.Focus();
                tbImePrezime.Text = "";
                tbLozinkaStara.Text = "";
             
            }
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

        protected void btnPretrazi_OnClick(object sender, EventArgs e)
        {
            string KorisnickoIme, Lozinka;

            if (tbImePrezime.Text == "" || tbImePrezime.Text == " ")
            {
                ObradaiObavestenje("Unesite korisničko ime!");
                return;
            }
            else
                 KorisnickoIme = tbImePrezime.Text;

            if (tbLozinkaStara.Text == "")
            {
                ObradaiObavestenje("Unesite lozinku!");
                tbLozinkaStara.Focus();
                return;
            }
            Lozinka = tbLozinkaStara.Text;
         
            if (ProveraStarihPodataka(KorisnickoIme, Lozinka))
            {
                tbLozinka_Nova.Focus();
                tbImePrezime.Enabled = false;
                tbLozinkaStara.Enabled = false;                
            }            
        }


        private bool ProveraStarihPodataka(string korisnickoIme, string lozinka)
        {

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.korisnickoIme = korisnickoIme; 
            red.lozinka = lozinka;
            ulaz.dtaaa.AdddtaaaRow(red);

            wcfMultibuki.KorisnikPrijavaRequest zahtev = new wcfMultibuki.KorisnikPrijavaRequest(ulaz);
            wcfMultibuki.KorisnikPrijavaResponse odgovor = new wcfMultibuki.KorisnikPrijavaResponse();

            try
            {
                odgovor = client.KorisnikPrijava(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška pri pozivu servisa u proceduri KorisnikPrijava!  \\n\\n" + ex.Message);
                return false;
            }
            try
            {
                if (odgovor.KorisnikPrijavaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.KorisnikPrijavaResult.dtGreska.Rows[0][0].ToString());
                    return false;
                }
                else if (odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows.Count != 1)
                {
                    ObradaiObavestenje("Greška u podacima u proceduri KorisnikPrijava!  \\n\\nNije pronađen odgovarajući podatak!");
                    return false;
                }
                else
                {
                    ObradaiObavestenje("Uspešna prijava!");
                    pnlUnos.Visible = true;
                    return true;                                     
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi ProveraStarihPodataka!  \\n\\n " + ex.Message);
                return false;
            }
        }

        protected void btnOcisti_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PromenaLozinkeKorisnika.aspx?");
        }

        protected void btnPromeniLozinku_OnClick(object sender, EventArgs e)
        {

            //if (tbLozinkaStara.Text == "")
            //{
            //    ObradaiObavestenje("Unesite staru lozinku!");
            //    return;
            //}

            if (tbLozinka_Nova.Text == "")
            {
                ObradaiObavestenje("Unesite novu lozinku!");
                return;
            }
            if (tbLozinka_NovaPonovljeno.Text == "")
            {
                ObradaiObavestenje("Potvrdite novu lozinku!");
                return;
            }
        
            string sLozinka_Nova = tbLozinka_Nova.Text;
            string sLozinka_NovaPonovljeno = tbLozinka_NovaPonovljeno.Text;

            if (sLozinka_Nova != sLozinka_NovaPonovljeno)
            {
                ObradaiObavestenje("Unete nove lozinke se ne poklapaju!");
                tbLozinka_NovaPonovljeno.Focus();
                return;
            }

             wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();

            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.korisnickoIme = tbImePrezime.Text; //int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            red.lozinka = sLozinka_NovaPonovljeno;     
            ulaz.dtaaa.AdddtaaaRow(red);

            wcfMultibuki.RestartLozinkeRequest zahtev = new wcfMultibuki.RestartLozinkeRequest(ulaz);
            wcfMultibuki.RestartLozinkeResponse odgovor = new wcfMultibuki.RestartLozinkeResponse();

            try
            {
                odgovor = client.RestartLozinke(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška pri pozivu servisa u proceduri RestartLozinke!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.RestartLozinkeResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.RestartLozinkeResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ObradaiObavestenje("Uspešno promenjena lozinka!");
                    btnPromeniLozinku.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi btnPromeniLozinku_OnClick!  \\n\\n " + ex.Message);
                return;
            }








            //Провера да ли је исправна стара лозинка
            //   ProveraStarihPodataka();
        }


        protected void tbImePrezime_TextChanged(object sender, EventArgs e)
        {
            tbLozinkaStara.Focus();
        }

        protected void tbLozinkaStara_TextChanged(object sender, EventArgs e)
        {
            tbLozinkaStara.Attributes["type"] = "password";
           // btnPretrazi.Focus();
        }


      /*  private void tbLozinkaStara_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Proceed code
                MessageBox.Show("Enter Key Pressed ");
            }


           // btnPretrazi_OnClick();
        }*/


        


}
}