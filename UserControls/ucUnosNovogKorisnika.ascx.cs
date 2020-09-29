using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.UserControls
{
    public partial class ucUnosNovogKorisnika : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                pnlUnos.Visible = false;
                tbImePrezime.Enabled = true;
                tbImePrezime.Focus();
                ddlUlogaPopuni();
                ddlFirmaKorisnikPopuni();               
            }
        }

        protected void ddlFirmaKorisnikPopuni()
        {

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiFirmaKorisnikRequest zahtev = new wcfMultibuki.VratiFirmaKorisnikRequest();
            wcfMultibuki.VratiFirmaKorisnikResponse odgovor = new wcfMultibuki.VratiFirmaKorisnikResponse();

            try
            {
                odgovor = client.VratiFirmaKorisnik(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri ddlFirmaKorisnikPopuni pri pozivanju metode VratiFirmaKorisnik iz servisa!  \\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiFirmaKorisnikResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiFirmaKorisnikResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ddlFirmaKorisnik.DataSource = odgovor.VratiFirmaKorisnikResult.dtVratiFirmaKorisnik.Rows;
                    ddlFirmaKorisnik.DataValueField = "idFirmaKorisnik";
                    ddlFirmaKorisnik.DataTextField = "NazivFirmeKorisnika";
                    ddlFirmaKorisnik.DataBind();
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi ddlFirmaKorisnikPopuni  \\n" + ex.Message);
                return;
            }
        }

        protected void ddlUlogaPopuni()
        {

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiUloguRequest zahtev = new wcfMultibuki.VratiUloguRequest();
            wcfMultibuki.VratiUloguResponse odgovor = new wcfMultibuki.VratiUloguResponse();

            try
            {
                odgovor = client.VratiUlogu(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri ddlUlogaPopuni pri pozivanju metode VratiUlogu iz servisa!  \\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiUloguResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiUloguResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ddlUloga.DataSource = odgovor.VratiUloguResult.dtVratiUlogu.Rows;
                    ddlUloga.DataValueField = "IDUloga";
                    ddlUloga.DataTextField = "NazivUloge";
                    ddlUloga.DataBind();
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi ddlUlogaPopuni  \\n" + ex.Message);
                return;
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

        public void btnPretrazi_OnClick(object sender, EventArgs e)
        {
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();

            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsIzlaz izlaz = new wcfMultibuki.dsIzlaz();
            string KorisnickoIme = tbImePrezime.Text;
            wcfMultibuki.VratiKorisnikaRequest zahtev = new wcfMultibuki.VratiKorisnikaRequest(KorisnickoIme);
            wcfMultibuki.VratiKorisnikaResponse odgovor = new wcfMultibuki.VratiKorisnikaResponse();

            try
            {
                odgovor = client.VratiKorisnika(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška pri pozivu servisa u proceduri VratiKorisnika!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.VratiKorisnikaResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiKorisnikaResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    if (odgovor.VratiKorisnikaResult.dtVratiKorisnika.Rows[0]["IDKorisnik"].ToString() != "0")
                        ObradaiObavestenje("Takav korisnik već postoji");
                    else
                    {
                        if (String.IsNullOrEmpty(tbImePrezime.Text))
                        {
                            ObradaiObavestenje("Morate popunite korisničko ime (kao što je naznačeno)");
                            return;
                        }
                        tbImePrezime.Enabled = false;
                        pnlUnos.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u metodi btnPretrazi_OnClick!  \\n\\n " + ex.Message);
                return;
            }
        }


        protected void btnSacuvaj_OnClick(object sender, EventArgs e)
        {
            //wcfKategorizacijaPS.IwcfKategorizacijaPS client = new wcfKategorizacijaPS.IwcfKategorizacijaPSClient();

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();

            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();           
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();
            red = ulaz.dtaaa.NewdtaaaRow();


            //ulazni podaci
            string KorisnickoIme = tbImePrezime.Text;
            if (tbPocetnaLozinka.Text == "")
            {
                ObradaiObavestenje("Morate popuniti početnu lozinku (u zadatom formatu)");
                return;
            }
            string Lozinka = tbPocetnaLozinka.Text;
            //ime
            if (tbImeKorisnika.Text == "" || tbImeKorisnika.Text == " ")
            {
                ObradaiObavestenje("Unesite ime korisnika! ");
                return;
            }
            if (tbImeKorisnika.Text.Length != 0)
            {
                if (tbImeKorisnika.Text.Length <= 1)
                {
                    ObradaiObavestenje("Ime korisnika ne može biti kraće od 2 slova!");
                    return;
                }

                proveraImena();
                if (hdIme.Value == "1")
                {
                    ObradaiObavestenje("Ime korisnika: možete uneti crticu i slovne znakov (ćirilice ili latinice) ");
                    return;
                }

            }
            string Ime = tbImeKorisnika.Text;
          //  string lower = "UPPERCASE string";
        //    string upper = lower.ToUpper();

            //prezime
            if (tbPrezimeKorisnika.Text == "" || tbPrezimeKorisnika.Text == " ")
            {
                ObradaiObavestenje("Unesite prezime korisnika! ");
                return;
            }
            if (tbPrezimeKorisnika.Text.Length != 0)
            {
                if (tbPrezimeKorisnika.Text.Length <= 1)
                {

                    ObradaiObavestenje("Prezime korisnika ne može biti kraće od 2 slova!");
                    return;
                }

                proveraPrezimena();
                if (hdPrezime.Value == "1")
                {
                    ObradaiObavestenje("Prezime korisnika: možete uneti crticu i slovne znakove (ćirilice ili latinice)");
                    return;
                }
            }
            string Prezime = tbPrezimeKorisnika.Text;
            if (tbEPosta.Text == "" || tbEPosta.Text == " ")
            {
                ObradaiObavestenje("Unesite e Mail adresu korisnika! ");
                return;
            }
            //  mail
            if (tbEPosta.Text.Length < 8)
            {
                ObradaiObavestenje("Preverite da li ste dobro uneli eMail adresu!");
                return;
            }

            if (tbEPosta.Text.Length > 7) proveraMail();  //mk@gmail.com ??? 
            if (hdMail.Value == "2")
            {
                ObradaiObavestenje("eMail adresa nije validna, proverite unos!");
                return;
            }

            dodatnaProveraMail();
            if (hdMailDodatni.Value == "1")
            {
                ObradaiObavestenje("eMail adresa nije validna, ponovo proverite unos!");
                return;
            }
            dodatnaProveraMail();
            if (hdMailDodatni.Value == "1")
            {
                ObradaiObavestenje("eMail adresa nije validna, ponovo proverite unos!");
                return;
            }
            string Eposta = tbEPosta.Text;
            if (ddlUloga.SelectedValue == "0")
            {
                ObradaiObavestenje("Morate uneti ulogu korisnika");
                return;
            }
            int IdUloga = int.Parse(ddlUloga.SelectedValue);
            
            string Telefon = tbTelefon.Text;

            if (ddlFirmaKorisnik.SelectedValue == "0")
            {
                ObradaiObavestenje("Morate uneti firmu korisnika");
                return;
            }
            int IdFirmaKorisnik = int.Parse(ddlFirmaKorisnik.SelectedValue);

            red.korisnickoIme = KorisnickoIme;
            red.lozinka = Lozinka;
            red.ime = Ime;
            red.prezime = Prezime;
            red.ePosta = Eposta;
            red.telefon = Telefon;
            red.idUloga = IdUloga;
            red.idFirmaKorisnik = IdFirmaKorisnik;

            ulaz.dtaaa.AdddtaaaRow(red);           
            wcfMultibuki.KorisnikUnosRequest zahtev = new wcfMultibuki.KorisnikUnosRequest(ulaz);
            wcfMultibuki.KorisnikUnosResponse odgovor = new wcfMultibuki.KorisnikUnosResponse();

            try
            {
                odgovor = client.KorisnikUnos(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri aaaKorisnikUnos pri pozivu metode KorisnikUnos iz servisa!  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikUnosResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.KorisnikUnosResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    ObradaiObavestenje("Uspešno sačuvani podaci o korisniku!");
                    btnSacuvaj.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u aplikaciji u proceduri KorisnikUnos!  \\n\\n " + ex.Message);
                return;
            }
        }

        private void proveraImena()
        {
            string ime = "";
            ime = tbImeKorisnika.Text;

           string strRegex = @"^[a-zžšđčćA-ZŽŠĐČĆабвгдђежзијклљмнњопрстћуфхцчџшАБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ\-\s]+$";       //  а-шА-Ш\-\     "^[a-zA-Z\\s]+$"   ЉЊЖШЂЧЋ

            Regex re = new Regex(strRegex);
            if (re.IsMatch(ime))
                hdIme.Value = "0";   //true
            else
                hdIme.Value = "1"; //false
        }

        private void proveraPrezimena()
        {

            string prezime = "";
            prezime = tbPrezimeKorisnika.Text;

            string strRegex = @"^[a-zžšđčćA-ZŽŠĐČĆабвгдђежзијклљмнњопрстћуфхцчџшАБВГДЂЕЖЗИЈКЛЉМНЊОПРСТЋУФХЦЧЏШ\-\s]+$";       //  а-шА-Ш\-\     "^[a-zA-Z\\s]+$"   ЉЊЖШЂЧЋ
            Regex re = new Regex(strRegex);
            if (re.IsMatch(prezime))
                hdPrezime.Value = "0";   //true
            else
                hdPrezime.Value = "1"; //false

        }

        private void proveraMail()
        {
            int manki;
            int poslednjaTacka;
            string adresa;
            adresa = tbEPosta.Text;
            manki = adresa.IndexOf("@");
            poslednjaTacka = adresa.LastIndexOf(".");
            if (manki < 1 || poslednjaTacka < manki + 2 || poslednjaTacka + 2 >= adresa.Length)
            {
                hdMail.Value = "2";  //pogrešna Zvanični Mail adresa
            }
            else
            {
                hdMail.Value = "0";  //pogrešna Zvanični Mail adresa
            }
        }

        private void dodatnaProveraMail()
        {
            string inputEmail;
            inputEmail = tbEPosta.Text;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                hdMailDodatni.Value = "0";   //true
            else
                hdMailDodatni.Value = "1"; //false

        }

        protected void btnOcisti_OnClick(object sender, EventArgs e)
        {
            //Response.Redirect("UnosNovogKorisnika.aspx?");
            pnlUnos.Visible = false;
            tbImePrezime.Enabled = true;
            tbImePrezime.Text = "";
            tbPocetnaLozinka.Text = "";
            tbImeKorisnika.Text = "";
            tbPrezimeKorisnika.Text = "";
            tbEPosta.Text = "";
            tbTelefon.Text = "";
            ddlUlogaPopuni();
            ddlFirmaKorisnikPopuni();
        }

    }
}