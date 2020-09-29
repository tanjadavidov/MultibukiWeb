using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.aaaWebForme
{
    public partial class aaaKorisnik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                PopuniTrenutnePodatke();
                lblNaslov.Text = "AŽURIRANJE KORISNIČKOG NALOGA";
                aaaProvera();

                if (Session["Korisnik_PocetnaLozinka"].ToString() == "1")
                {
                    btnNazad.Visible = false;
                    ObradaiObavestenje("Vaš nalog je u potpunosti aktiviran, početna lozinka je uspešno kreirana!  \\n\\nUnesite novu lozinku i ažurirajte vaše lične podatke!");
                }
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

        protected void PopuniTrenutnePodatke()
        {
            //Session["Korisnik_IDKorisnik"]
            tbKorisnickoIme.Text = Session["Korisnik_KorisnickoIme"].ToString();
            tbIme.Text = Session["Korisnik_Ime"].ToString();
            tbPrezime.Text = Session["Korisnik_Prezime"].ToString();
            tbEposta.Text = Session["Korisnik_ePosta"].ToString();
            tbTelefon.Text = Session["Korisnik_Telefon"].ToString();

            lblKorisnik.Text = Session["Korisnik_Ime"].ToString() + ' ' + Session["Korisnik_Prezime"].ToString();

            if (Session["Slika"] == null)
                Image1.ImageUrl = "~/Images/user.png";
            else
                Image1.ImageUrl = "data:Image/png;base64," + Session["Slika"];

            if (Session["Slika"] == null)
                Image2.ImageUrl = "~/Images/user.png";
            else
                Image2.ImageUrl = "data:Image/png;base64," + Session["Slika"];

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

            //Провера права приступа
            bool imaPravo = false;

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            red.idFunkcija = 2;        //   ***********************************
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
                ObradaiObavestenje("Greška u proceduri KorisnikPravoNaFunkciju pri pozivu servisa za proveru naloga!  \\n\\n" + ex.Message);

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
                ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPravoNaFunkciju pri proveri naloga!  \\n\\n " + ex.Message);
                return;
            }

            if (!imaPravo)
                Response.Redirect("~/WebForms/AccessDenied.aspx");
        }


        protected void lbtnOdjava_Click(object sender, EventArgs e)
        {
            Session["Korisnik_IDKorisnik"] = null;
            Session["Korisnik_KorisnickoIme"] = null;
            Session["Korisnik_Ime"] = null;
            Session["Korisnik_Prezime"] = null;
            Session["Korisnik_ePosta"] = null;
            Session["Korisnik_Telefon"] = null;

            Response.Redirect("~/aaaWebForms/aaaPrijava.aspx");
        }

        protected void btnNazad_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebForms/Pocetna.aspx");
        }


        protected void btnPromena_Click(object sender, EventArgs e)
        {
            if (tbLozinka_Stara.Text == "")
            {
                ObradaiObavestenje("Unesite staru lozinku!");
                return;
            }
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

            string sLozinka_Stara = tbLozinka_Stara.Text;
            string sLozinka_Nova = tbLozinka_Nova.Text;
            string sLozinka_NovaPonovljeno = tbLozinka_NovaPonovljeno.Text;

            if (sLozinka_Nova != sLozinka_NovaPonovljeno)
            {
                ObradaiObavestenje("Unete nove lozinke se ne poklapaju!");
                return;
            }

            //Провера да ли је исправна стара лозинка
            ProveraStarihPodataka();
        }


        protected void ProveraStarihPodataka()
        {

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            red.korisnickoIme = Session["Korisnik_KorisnickoIme"].ToString();
            red.lozinka = tbLozinka_Stara.Text;

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
                ObradaiObavestenje("Greška u proceduri KorisnikPrijava pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPrijavaResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikPrijavaResult.dtGreska.Rows[0][0].ToString());
                else if (odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows.Count != 1)
                {
                    ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPrijava!  \\n\\nNije pronađen originalni nalog!");
                    return;
                }
                else
                {
                    //Промени податке
                    PromeniPodatke();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi KorisnikPrijava!  \\n\\n " + ex.Message);
                return;
            }
        }


        protected void PromeniPodatke()
        {


            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            //NOVI PODACI
            //IDKorisnik se ne menja
            red.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());


            if (tbKorisnickoIme.Text == "")
            {
                ObradaiObavestenje("Unesite korisničko ime!");
                return;
            }
            else
                red.korisnickoIme = tbKorisnickoIme.Text;

            if (tbIme.Text == "")
            {
                ObradaiObavestenje("Korisničko ime!");
                return;
            }
            else
                red.ime = tbIme.Text;

            if (tbPrezime.Text == "")
            {
                ObradaiObavestenje("Unesite prezime!");
                return;
            }
            else
                red.prezime = tbPrezime.Text;

            if (tbEposta.Text == "")
            {
                ObradaiObavestenje("Unesite e-poštu!");
                return;
            }
            else
                red.ePosta = tbEposta.Text;

            if (tbTelefon.Text == "")
            {
                ObradaiObavestenje("Unesite telefon!");
                return;
            }
            else
                red.telefon = tbTelefon.Text;

            red.lozinka = tbLozinka_Nova.Text;


            ulaz.dtaaa.AdddtaaaRow(red);
            wcfMultibuki.KorisnikPromenaPodatakaRequest zahtev = new wcfMultibuki.KorisnikPromenaPodatakaRequest(ulaz);
            wcfMultibuki.KorisnikPromenaPodatakaResponse odgovor = new wcfMultibuki.KorisnikPromenaPodatakaResponse();

            try
            {
                odgovor = client.KorisnikPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri KorisnikPromenaPodataka pri pozivu servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    //Session["Korisnik_IDKorisnik"] ---- Остаје не промењено!!!
                    Session["Korisnik_KorisnickoIme"] = tbKorisnickoIme.Text;
                    Session["Korisnik_Ime"] = tbIme.Text;
                    Session["Korisnik_Prezime"] = tbPrezime.Text;
                    Session["Korisnik_ePosta"] = tbEposta.Text;
                    Session["Korisnik_Telefon"] = tbTelefon.Text;
                    Session["Korisnik_PocetnaLozinka"] = "0";
                    lblKorisnik.Text = tbIme.Text + ' ' + tbPrezime.Text;

                    btnNazad.Visible = true;
                    PopuniTrenutnePodatke();

                    ObradaiObavestenje("Uspešno promenjeni podaci!");
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi PromeniPodatke!  \\n\\n" + ex.Message);
                return;
            }
        }


        protected void btnPromeniSliku_Click(object sender, EventArgs e)
        {
            if (IzmeniSliku())
            {
                PrikaziSliku();
                ObradaiObavestenje("Uspešno promenjena slika!");
            }
        }

        private void PrikaziSliku()
        {
            int IDKorisnik = Convert.ToInt32(Session["Korisnik_IDKorisnik"].ToString());

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiSlikuRequest zahtev = new wcfMultibuki.VratiSlikuRequest(IDKorisnik);
            wcfMultibuki.VratiSlikuResponse odgovor = new wcfMultibuki.VratiSlikuResponse();

            try
            {
                odgovor = client.VratiSliku(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u metodi PrikaziSliku pri pozivu procedure iz servisa VratiSliku!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiSlikuResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.VratiSlikuResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    string str = null;
                    try
                    {
                        byte[] bytes = (byte[])odgovor.VratiSlikuResult.dtVratiSliku.Rows[0]["Slika"];
                        str = Convert.ToBase64String(bytes);
                    }
                    catch (Exception)
                    {
                        str = null;
                    }
                    Session["Slika"] = str;

                    if (Session["Slika"] == null)
                        Image1.ImageUrl = "~/Images/user.png";
                    else
                        Image1.ImageUrl = "data:Image/png;base64," + Session["Slika"];

                    if (Session["Slika"] == null)
                        Image2.ImageUrl = "~/Images/user.png";
                    else
                        Image2.ImageUrl = "data:Image/png;base64," + Session["Slika"];

                    return;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi PrikaziSliku!  \\n\\n " + ex.Message);
                return;
            }
        }

        protected void btnObrisiSliku_Click(object sender, EventArgs e)
        {
            if (ObrisiSliku())
            {
                PrikaziSliku();
                ObradaiObavestenje("Uspešno obrisana slika!");
            }
        }

        private bool ObrisiSliku()
        {
            int IDKorisnik = Convert.ToInt32(Session["Korisnik_IDKorisnik"].ToString());

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.ObrisiSlikuRequest zahtev = new wcfMultibuki.ObrisiSlikuRequest(IDKorisnik);
            wcfMultibuki.ObrisiSlikuResponse odgovor = new wcfMultibuki.ObrisiSlikuResponse();

            try
            {
                odgovor = client.ObrisiSliku(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u metodi ObrisiSliku pri pozivu procedure iz servisa ObrisiSliku!  \\n\\n" + ex.Message);
                return false;
            }
            try
            {
                if (odgovor.ObrisiSlikuResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.ObrisiSlikuResult.dtGreska.Rows[0][0].ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi ObrisiSliku!  \\n\\n " + ex.Message);
                return false;
            }
        }

        private bool IzmeniSliku()
        {
            int IDKorisnik = Convert.ToInt32(Session["Korisnik_IDKorisnik"].ToString());

            byte[] Slika = null;
            HttpPostedFile postedFile = FileUpload1.PostedFile;
            string fileName = Path.GetFileName(postedFile.FileName);
            string fileExtension = Path.GetExtension(postedFile.FileName);
            int fileSize = postedFile.ContentLength;

            if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".bmp" ||
                fileExtension.ToLower() == ".gif" || fileExtension.ToLower() == ".png")
            {
                Stream stream = postedFile.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                Slika = binaryReader.ReadBytes((int)stream.Length);
            }
            else
            {
                ObradaiObavestenje("Niste odabrali sliku!");
                return false;
            }

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.PromenaSlikeRequest zahtev = new wcfMultibuki.PromenaSlikeRequest(IDKorisnik, Slika);
            wcfMultibuki.PromenaSlikeResponse odgovor = new wcfMultibuki.PromenaSlikeResponse();

            try
            {
                odgovor = client.PromenaSlike(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u metodi IzmeniSliku pri pozivu procedure iz servisa PromenaSlike!  \\n\\n" + ex.Message);
                return false;
            }
            try
            {
                if (odgovor.PromenaSlikeResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.PromenaSlikeResult.dtGreska.Rows[0][0].ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi IzmeniSliku!  \\n\\n " + ex.Message);
                return false;
            }
        }


    }

}