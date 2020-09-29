using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.UserControls
{
    public partial class ucPoslovniPartner : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                pnlUnos.Visible = false;
                rblTipLica.SelectedValue = Convert.ToString(1);
                int TipLica = int.Parse(rblTipLica.SelectedValue);
                int? Idpospar = null;
                string search = txtSearch.Text;
                Ucitaj(TipLica, Idpospar, search);
                gv.Columns[4].Visible = false;                
                ddlPosPar_popuni(TipLica, Idpospar, search);

            }
        }

        protected void btnObrisi_Click(object sender, EventArgs e)
        {
            /*  --refresuje grid
            wcfMultibuki.dsIzlaz izlaz = new wcfMultibuki.dsIzlaz();
            wcfMultibuki.dsIzlaz.dtVratiPoslovniPartnerRow  redSmestajne = izlaz.dtVratiPoslovniPartner.NewdtVratiPoslovniPartnerRow();

            for (int i = 0; i < this.gv.Rows.Count; i++)
            {
                redSmestajne = izlaz.dtVratiPoslovniPartner.NewdtVratiPoslovniPartnerRow();

                redSmestajne.idPosPar = int.Parse(((Label)gv.Rows[i].FindControl("lblidPosPar")).Text.ToString());
                redSmestajne.NazivPosPar  = ((TextBox)gv.Rows[i].FindControl("tbNaziv")).Text;
                redSmestajne.ImePosPar = ((TextBox)gv.Rows[i].FindControl("tbIme")).Text;
                redSmestajne.PrezimePosPar = ((TextBox)gv.Rows[i].FindControl("tbPrezime")).Text;
                redSmestajne.Adresa = ((TextBox)gv.Rows[i].FindControl("tbAdresa")).Text;
                redSmestajne.KucniBroj = int.Parse(((TextBox)gv.Rows[i].FindControl("tbKucniBroj")).Text);
                redSmestajne.KucniPodbroj = ((TextBox)gv.Rows[i].FindControl("tbKPodBroj")).Text;
                redSmestajne.Sprat = ((TextBox)gv.Rows[i].FindControl("tbSprat")).Text;
                redSmestajne.stan = int.Parse(((TextBox)gv.Rows[i].FindControl("tbStan")).Text);
                redSmestajne.TelefonHome = ((TextBox)gv.Rows[i].FindControl("tbTelefon")).Text;
                redSmestajne.PttBroj = int.Parse(((DropDownList)gv.Rows[i].FindControl("ddlMesto")).SelectedValue);
                
                izlaz.dtVratiPoslovniPartner.AdddtVratiPoslovniPartnerRow(redSmestajne);
            }
            //  int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent).Parent).RowIndex;
            int rowIndex = ((GridViewRow)(((Button)sender).Parent).Parent).RowIndex;          

            System.Data.DataRow deleteRow = izlaz.dtVratiPoslovniPartner[rowIndex];
            izlaz.dtVratiPoslovniPartner.Rows.Remove(deleteRow);

            gv.DataSource = izlaz.dtVratiPoslovniPartner;
            gv.DataBind();

            gv.Visible = true;
            */
          
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;
            // System.Data.DataRow deleteRow = redUGridu.RowIndex;
                     
            int Id = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblidPosPar")).Text);
            //  int IdKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());


            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();


            red = ulaz.dtdbo.NewdtdboRow();

            red.id = Id;
            // red.idKorisnikUnosa = IdKorisnikPromene;
            // red.vremeUnosa= DateTime.Now;
            red.status = true;  //ako je brisanje u pitanju

            ulaz.dtdbo.AdddtdboRow(red);

            wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest zahtev = new wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest(ulaz);
            wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse odgovor = new wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse();

            try
            {
                odgovor = client.PoslovniPartnerPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri PoslovniPartnerPromenaPodataka pri pozivu servisa! \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.PoslovniPartnerPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {

                    ObradaiObavestenje(odgovor.PoslovniPartnerPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    ObradaiObavestenje("Uspešno obrisan Poslovni partner!");                    
                    int TipLica = int.Parse(rblTipLica.SelectedValue);
                    int? Idpospar = null;
                    string search = txtSearch.Text;
                    Ucitaj(TipLica, Idpospar, search);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                   , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi brnBrisi!  \\n\\n" + ex.Message);
                return;
            }

        }


        protected void btnIzmeni_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                GridViewRow redGrid = gv.Rows[i];
                int red = redGrid.RowIndex;
                if (rowIndex == red)
                {
                  //  ((Label)(gv.Rows[i].FindControl("lblidPosPar"))).Enabled=true;
                 
                    ((TextBox)(gv.Rows[i].FindControl("tbNaziv"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbIme"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbPrezime"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbAdresa"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbKucniBroj"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbKPodBroj"))).Enabled = true;
                    ((DropDownList)(gv.Rows[i].FindControl("ddlSprat"))).Enabled = true;                  
                    ((TextBox)(gv.Rows[i].FindControl("tbStan"))).Enabled = true;
                    ((TextBox)(gv.Rows[i].FindControl("tbTelefon"))).Enabled = true;
                    ((DropDownList)(gv.Rows[i].FindControl("ddlMesto"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnPrikaziDetalje"))).Enabled = false;
                    
                }
            }
        }

       

        protected void btnSacuvajIzmene_Click(object sender, EventArgs e)
        {
            int TipLica = int.Parse(rblTipLica.SelectedValue);
            int? idpospar = null; 
            int? KucniBr; int? Stan;

            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            int IdPosPar = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblidPosPar")).Text);
            string Naziv = ((TextBox)gv.Rows[rowIndex].FindControl("tbNaziv")).Text;
            string Ime = ((TextBox)gv.Rows[rowIndex].FindControl("tbIme")).Text;
            string Prezime = ((TextBox)gv.Rows[rowIndex].FindControl("tbPrezime")).Text;
            string Adresa = ((TextBox)gv.Rows[rowIndex].FindControl("tbAdresa")).Text;

        
            if (((TextBox)gv.Rows[rowIndex].FindControl("tbKucniBroj")).Text== "")
            {
                KucniBr = null;                             
            }
            else
            {
                 KucniBr = int.Parse(((TextBox)gv.Rows[rowIndex].FindControl("tbKucniBroj")).Text);
            }
            string KucniPodBr = ((TextBox)gv.Rows[rowIndex].FindControl("tbKPodBroj")).Text;

            string idSprat = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlSprat")).SelectedValue;
            int idsprat = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlSprat")).SelectedIndex;

            int Sprat = 0;            
     
            try
            {
               // ddlSprat.SelectedIndex = idsprat;
                 Sprat = int.Parse(idSprat);
            }
            catch (Exception)
            {
                Sprat =0;
                
            }         


            string pttBroj = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlMesto")).SelectedValue;
            int pttbroj = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlMesto")).SelectedIndex;

           // Aleksino
             int PttBroj = 0;
            try
            {
              //  ddlMesto.SelectedIndex = pttbroj;
                //PttBroj = int.Parse(((DropDownList)gv.Rows[rowIndex].FindControl("ddlSprat")).SelectedValue);
                PttBroj = int.Parse(pttBroj);  //tanjaovde pukne
                //   ddlMesto.Items.FindByValue(pttMesto).Selected = true;
            }
            catch (Exception)
            {
                PttBroj = 0;

            }           

            if (((TextBox)gv.Rows[rowIndex].FindControl("tbStan")).Text == "")
            {
                Stan = null;          
            }
            else
            {
                Stan = int.Parse(((TextBox)gv.Rows[rowIndex].FindControl("tbStan")).Text);
            }

            string Telefon = ((TextBox)gv.Rows[rowIndex].FindControl("tbTelefon")).Text;
            string TelefonMob = null;
            string pib = null;
            string Email = null;
            string webAdresa = null;          
         

            if (TipLica==1)
                {
                    if (Ime == "" || Ime == " ")
                    {
                        ObradaiObavestenje("Neophodno je uneti Ime lica!");
                        return;
                    }
                    if (Prezime == "" || Prezime == " ")
                    {
                        ObradaiObavestenje("Neophodno je uneti Prezime lica!");
                        return;
                    }
                }
            else
            {
                if (Naziv == "" || Naziv == " ")
                {
                    ObradaiObavestenje("Neophodno je uneti Naziv partnera!");
                    return;
                }
            }            

            int IDKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());

            if (IzmeniPoslovnogPartnera(TipLica, IdPosPar, Naziv, Ime, Prezime, Adresa, KucniBr, KucniPodBr, Sprat, Stan, Telefon, TelefonMob, pib, Email, webAdresa, PttBroj, IDKorisnikPromene))

                {
                ObradaiObavestenje("Uspešno izmenjen podatak o poslovnom partneru!");

                TipLica = int.Parse(rblTipLica.SelectedValue);
                string search = txtSearch.Text;
                Ucitaj(TipLica, idpospar, search);  
                ddlPosPar_popuni(TipLica, idpospar, search);
            }
        }


        private bool IzmeniPoslovnogPartnera(int TipLica, int IdPosPar, string Naziv, string Ime, string Prezime, string Adresa, int? KucniBroj, string KucniPodBroj, int Sprat, 
                                            int? Stan, string Telefon, string TelefonMob, string pib, string Email, string webAdr, int PttBr, int IDKorisnikPromene)       
        {        
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsIzlaz izlaz = new wcfMultibuki.dsIzlaz();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();      
            wcfMultibuki.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            red.broj2 = TipLica;
            red.id = IdPosPar;
            red.naziv = Naziv;
            red.naziv1 = Ime;          
            red.naziv2 = Prezime;
            red.naziv3= Adresa;
            red.broj = KucniBroj.GetValueOrDefault();
            red.sifra = KucniPodBroj;
            red.id1 = Sprat;
            red.broj1 = Stan.GetValueOrDefault(); 
            red.naziv4 = Telefon;
            red.punNaziv = TelefonMob;
            red.skraceniNaziv = pib;
            red.text1 = Email;
            red.link = webAdr;
            red.id2 = PttBr;

            red.idKorisnikUnos = IDKorisnikPromene;
            red.vremeUnos = DateTime.Now;

             ulaz.dtdbo.AdddtdboRow(red);       

            wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest zahtev = new wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest(ulaz);
            wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse odgovor = new wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse();

            try
            {
                odgovor = client.PoslovniPartnerPromenaPodataka(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri PoslovniPartnerPromenaPodataka pri pozivu metode PoslovniPartnerPromenaPodataka iz servisa!  \\n\\n" + ex.Message);
                return false;

            }
            try
            {
                if (odgovor.PoslovniPartnerPromenaPodatakaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.PoslovniPartnerPromenaPodatakaResult.dtGreska.Rows[0][0].ToString());
                    return false;
                }
                else
                {                  
                    ObradaiObavestenje("Uspešno izmenjen podatak o Poslovnom partneru!");
                  
                    return true;
                }


            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);

                ObradaiObavestenje("Greška u aplikaciji u metodi IzmeniPoslovnogPartnera!  \\n\\n " + ex.Message);
                return false;
            }
        }

        protected void btnOtkaziIzmene_Click(object sender, EventArgs e)
        {
            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;

            DesableGrid(rowIndex);          
            //((Button)(gv.Rows[i].FindControl("btnPrikaziDetalje"))).Enabled = false;

            int TipLica = int.Parse(rblTipLica.SelectedValue);
            int? Idpospar = null;
            string search = txtSearch.Text;
            Ucitaj(TipLica, Idpospar, search);
        }

        protected void btnPrikaziDetalje_Click(object sender, EventArgs e)        
        {
            pnlUnos.Visible = true;
            btnUnesiNovog.Enabled = false;
            gv.Enabled = false;
          
            rblTipLica2.SelectedValue = rblTipLica.SelectedValue;
          //  int TipLica = int.Parse(rblTipLica2.SelectedValue);
          
            tbNazivPP.Enabled = false;
            tbPib.Enabled = false;        
            Focus1.Focus();

            GridViewRow redUGridu = (GridViewRow)(((Button)sender).Parent).Parent;
            int rowIndex = redUGridu.RowIndex;          

            PanelIzmene_Popuni(rowIndex);


           

        }


        private void PanelIzmene_Popuni(int rowIndex)
        {
            ddlMesto_popuni();
            ddlSprat_popuni();


            int TipLica = int.Parse(rblTipLica2.SelectedValue);
            int? Idpospar = null;
            string search = txtSearch.Text;


            if (TipLica == 1)
            {
                tbNazivPP.Enabled = false;
                tbPib.Enabled = false;
                Focus1.Focus();
            }
            else
            {
                tbNazivPP.Enabled = true;
                tbPib.Enabled = true;
                Focus1.Focus();
            }



            tbIdpp.Text = ((Label)gv.Rows[rowIndex].FindControl("lblidPosPar")).Text;
            Idpospar = int.Parse(((Label)gv.Rows[rowIndex].FindControl("lblidPosPar")).Text);
            tbNazivPP.Text= ((TextBox)gv.Rows[rowIndex].FindControl("tbNaziv")).Text;
            tbImePP.Text= ((TextBox)gv.Rows[rowIndex].FindControl("tbIme")).Text;
            tbPrezimePP.Text= ((TextBox)gv.Rows[rowIndex].FindControl("tbPrezime")).Text;
            tbAdresa.Text= ((TextBox)gv.Rows[rowIndex].FindControl("tbAdresa")).Text;         
            tbKucniBroj.Text = ((TextBox)gv.Rows[rowIndex].FindControl("tbKucniBroj")).Text;
            tbPodBroj.Text= ((TextBox)gv.Rows[rowIndex].FindControl("tbKPodBroj")).Text;            

            string idSprat = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlSprat")).SelectedValue;
            int idsprat = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlSprat")).SelectedIndex;
            if (idSprat =="")
            {
              //  ddlSprat.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                ddlSprat.SelectedIndex = 0;
            }
            else
            {  
            //  ddlSprat.SelectedIndex = int.Parse(idSprat);
               ddlSprat.SelectedIndex = idsprat;
            //    ddlSprat.SelectedValue = idSprat;

            }
         // ddlSprat.Items.FindByValue(idSprat).Selected = true;

            string pttMesto = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlMesto")).SelectedValue;
            int pttmesto = ((DropDownList)gv.Rows[rowIndex].FindControl("ddlMesto")).SelectedIndex;
            if (pttMesto == "")
            {
              //  ddlMesto.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                ddlMesto.SelectedIndex = 0;
            }
            else
            {  
                ddlMesto.SelectedIndex = pttmesto;
            }
            //   ddlMesto.Items.FindByValue(pttMesto).Selected = true;

            tbStan.Text = ((TextBox)gv.Rows[rowIndex].FindControl("tbStan")).Text;
       
            tbTelefon.Text = ((TextBox)gv.Rows[rowIndex].FindControl("tbTelefon")).Text;

          
           
            
            /*---------------------------------vrati iz procedure         */

            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiPoslovniPartnerRequest zahtev = new wcfMultibuki.VratiPoslovniPartnerRequest(TipLica, Idpospar, search);
            wcfMultibuki.VratiPoslovniPartnerResponse odgovor = new wcfMultibuki.VratiPoslovniPartnerResponse();

            try
            {
                odgovor = client.VratiPoslovniPartner(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za poslovne partnere u metodi VratiPoslovniPartner!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.VratiPoslovniPartnerResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiPoslovniPartnerResult.dtGreska.Rows[0][0].ToString());
                else
                {

                    if (odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows.Count == 0)
                    {
                        ObradaiObavestenje("Ne postoje podaci za tražene parametre !");
                        return;
                    }
                    else
                    {
                        tbEMail.Text = odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows[0]["eMail"].ToString();
                        tbWebAdresa.Text= odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows[0]["webAdresa"].ToString();
                        tbTelMobilni.Text= odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows[0]["MobTel"].ToString();
                        tbPib.Text = odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows[0]["pib"].ToString();
                    }                   

                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida poslovne partnere u metodi VratiPoslovniPartner!  \\n\\n " + ex.Message);
                return;
            }



            /*
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest zahtev = new wcfMultibuki.PoslovniPartnerPromenaPodatakaRequest(ulaz);
            wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse odgovor = new wcfMultibuki.PoslovniPartnerPromenaPodatakaResponse();

            try
                {
                    odgovor = client.VratiVoziloZaEstetskiP(zahtev);
                }
                catch (Exception ex)
                {

                    PrikaziObavestenje("Грешка у процедури uspVratiVoziloZaEstetskiP при позиву методе VratiVoziloZaEstetskiP из сервиса!  \\n\\n" + ex.Message);
                    return;
                }
                try
                {
                    if (odgovor.VratiVoziloZaEstetskiPResult.dtGreska.Rows.Count > 0)
                        PrikaziObavestenje(odgovor.VratiVoziloZaEstetskiPResult.dtGreska.Rows[0][0].ToString());
                    else
                    {
                        //   tbIdEstetskiP.Text= odgovor.VratiVoziloZaEstetskiPResult.dtVratiVoziloZaEstetskiP.Rows[0]["IDEstetskiP"].ToString();

                        tbIdEstetskiP.Text = odgovor.VratiVoziloZaEstetskiPResult.dtVratiVoziloZaEstetskiP.Rows[0]["IDEstetskiP"].ToString();



                        tbEMail.Text = odgovor.VratiVoziloZaEstetskiPResult.dtVratiVoziloZaEstetskiP.Rows[0]["Izdao_novi"].ToString();
                     }
            }
            catch (Exception ex)
            {
                PrikaziObavestenje("Грешка у апликацији у методи VratiVoziloZaEstetskiP!  \\n\\n " + ex.Message);

                return;
            }*/
        }


        protected void DesableGrid(int redIndex)
        {

            for (int i = 0; i < gv.Rows.Count; i++)
            {

                GridViewRow redGrid = gv.Rows[i];
                int red = redGrid.RowIndex;
                if (redIndex == red)
                {

                    ((TextBox)(gv.Rows[i].FindControl("tbNaziv"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbIme"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbPrezime"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbAdresa"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbKucniBroj"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbKPodBroj"))).Enabled = false;
                    ((DropDownList)(gv.Rows[i].FindControl("ddlSprat"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbStan"))).Enabled = false;
                    ((TextBox)(gv.Rows[i].FindControl("tbTelefon"))).Enabled = false;
                    ((DropDownList)(gv.Rows[i].FindControl("ddlMesto"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnIzmeni"))).Enabled = true;
                    ((Button)(gv.Rows[i].FindControl("btnSacuvajIzmene"))).Enabled = false;
                    ((Button)(gv.Rows[i].FindControl("btnOtkaziIzmene"))).Enabled = false;


                }
            }

        }


        protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv.PageIndex = e.NewPageIndex;
            int TipLica = int.Parse(rblTipLica.SelectedValue);
            int? idPosPar = null;
            string search = txtSearch.Text;
            Ucitaj(TipLica, idPosPar, search);

        }


        protected void btnUnesiNovog_onclick(object sender, EventArgs e)
        {
            pnlUnos.Visible = true;
            btnUnesiNovog.Enabled = false;
            gv.Enabled = false;
            
            ddlMesto_popuni();
            ddlSprat_popuni();

            rblTipLica2.SelectedValue = rblTipLica.SelectedValue;  
            int TipLica = int.Parse(rblTipLica2.SelectedValue);

            if (TipLica == 1)
            {
                tbNazivPP.Enabled = false;
                tbPib.Enabled = false;
                Focus1.Focus();
            }
            else
            {
                tbNazivPP.Enabled = true;
                tbPib.Enabled = true;
                Focus1.Focus();
            }         
        }



        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //ddlMesto puni
                 
                 wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
                 wcfMultibuki.VratiMestoRequest zahtev = new wcfMultibuki.VratiMestoRequest();
                 wcfMultibuki.VratiMestoResponse odgovor = new wcfMultibuki.VratiMestoResponse();

                 try
                 {
                     odgovor = client.VratiMesto(zahtev);       
                 }
                 catch (Exception ex)
                 {

                     ObradaiObavestenje("Greška u proceduri kod vraćanja podataka za ddl listu Mesto u metodi VratiMesto iz servisa!  \\n\\n" + ex.Message);
                     return;
                 }
                 try
                 {
                     if (odgovor.VratiMestoResult.dtGreska.Rows.Count > 0)
                         ObradaiObavestenje(odgovor.VratiMestoResult.dtGreska.Rows[0][0].ToString());
                     else
                     {
                         DropDownList ddlMesto = (e.Row.FindControl("ddlMesto") as DropDownList);
                         ddlMesto.DataSource = odgovor.VratiMestoResult.dtVratiMesto.Rows;
                         ddlMesto.DataValueField = "PttBroj";
                         ddlMesto.DataTextField = "NazivMesta";
                         ddlMesto.DataBind();
                         ddlMesto.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                         string PttBroj = (e.Row.FindControl("lblPttBroj") as Label).Text;
                        if (PttBroj == "0")
                        {
                    //     ddlMesto.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            ddlMesto.SelectedIndex = 0;
                        }
                            
                        else
                        {
                            ddlMesto.SelectedValue = PttBroj.ToString();
                            ddlMesto.Items.FindByValue(PttBroj).Selected = true;
                        }
                            
                        //tanjaovde   ddlMesto.Items.Insert(0, new ListItem(string.Empty, string.Empty));                   
                    }
                 }
                 catch (Exception ex)
                 {
                     ObradaiObavestenje("Greška u proceduri u metodi VratiMesto!  \\n\\n " + ex.Message);
                     return;
                 }              

                //ddlsprat 
                wcfMultibuki.IwcfMultibuki client1 = new wcfMultibuki.IwcfMultibukiClient();
                wcfMultibuki.VratiSpratRequest zahtev1 = new wcfMultibuki.VratiSpratRequest();
                wcfMultibuki.VratiSpratResponse odgovor1 = new wcfMultibuki.VratiSpratResponse();

                try
                {
                    odgovor1 = client1.VratiSprat(zahtev1);
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlSprat u metodi VratiSprat iz servisa!  \\n\\n" + ex.Message);
                    return;
                }

                try
                {
                    if (odgovor1.VratiSpratResult.dtGreska.Rows.Count > 0)
                        ObradaiObavestenje(odgovor1.VratiSpratResult.dtGreska.Rows[0][0].ToString());
                    else
                    {
                        DropDownList ddlSprat = (e.Row.FindControl("ddlSprat") as DropDownList);
                        ddlSprat.DataSource = odgovor1.VratiSpratResult.dtVratiSprat.Rows;
                        ddlSprat.DataValueField = "idSprat";
                        ddlSprat.DataTextField = "OpisSprata";
                        ddlSprat.DataBind();
                        ddlSprat.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        string idSprat = (e.Row.FindControl("lblidSprat") as Label).Text;                                       

                        if(idSprat =="0")
                        {
                       //     ddlSprat.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                            ddlSprat.SelectedIndex = 0;
                        }
                        else
                        {//punjenje value sa item
                         /*  ListItem myitem = ddlSprat.Items.FindByValue(idSprat);
                            ddlSprat.SelectedValue = myitem.Value; */
                            
                        // punjenjnje value sa value vrednosti
                            ddlSprat.SelectedValue = idSprat.ToString();
                        }

                         //  ddlSprat.Items.FindByValue(idSprat).Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Greška u  u metodi gv_RowDataBound!  \\n\\n " + ex.Message);
                    return;
                }

            }
        }
     
        protected void ddlMesto_popuni()
         {           
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiMestoRequest zahtev = new wcfMultibuki.VratiMestoRequest();
            wcfMultibuki.VratiMestoResponse odgovor = new wcfMultibuki.VratiMestoResponse();

            try
            {
                odgovor = client.VratiMesto(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlMesto u metodi VratiMesto iz servisa!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.VratiMestoResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiMestoResult.dtGreska.Rows[0][0].ToString());
                else
                {

                    ddlMesto.DataSource = odgovor.VratiMestoResult.dtVratiMesto.Rows;
                    ddlMesto.DataValueField = "pttBroj";
                    ddlMesto.DataTextField = "NazivMesta";
                    ddlMesto.DataBind();
                    ddlMesto.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri u metodi VratiMesto!  \\n\\n " + ex.Message);
                return;
            }
        }


        protected void ddlSprat_popuni()
        {
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiSpratRequest zahtev = new wcfMultibuki.VratiSpratRequest();
            wcfMultibuki.VratiSpratResponse odgovor = new wcfMultibuki.VratiSpratResponse();

            try
            {
                odgovor = client.VratiSprat(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za ddlSprat u metodi VratiSprat iz servisa!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.VratiSpratResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiSpratResult.dtGreska.Rows[0][0].ToString());
                else
                {
                    
                    ddlSprat.DataSource = odgovor.VratiSpratResult.dtVratiSprat.Rows;
                    ddlSprat.DataValueField = "idSprat";
                    ddlSprat.DataTextField = "OpisSprata";
                    ddlSprat.DataBind();
                    ddlSprat.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri u metodi VratiSprat!  \\n\\n " + ex.Message);
                return;
            }
        }


        protected void ddlPosPar_popuni(int TipL , int? idpospar, string search)
        {
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiPoslovniPartnerRequest zahtev = new wcfMultibuki.VratiPoslovniPartnerRequest(TipL, idpospar, search);
            wcfMultibuki.VratiPoslovniPartnerResponse odgovor = new wcfMultibuki.VratiPoslovniPartnerResponse();

            try
            {
                odgovor = client.VratiPoslovniPartner(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Greška u proceduri kod vraćanja podataka za ddlPosPar u metodi VratiPoslovniPartner iz servisa!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.VratiPoslovniPartnerResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiPoslovniPartnerResult.dtGreska.Rows[0][0].ToString());
                else
                {
                  
                        ddlPosPar.DataSource = odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows;
                        ddlPosPar.DataValueField = "idPosPar";

                        if (TipL == 1)
                        { ddlPosPar.DataTextField = "NazPosPar_fizicko"; }
                        else
                        { ddlPosPar.DataTextField = "NazPosPar_pravno"; }

                        ddlPosPar.DataBind();               
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Greška u proceduri u metodi VratiPoslovniPartner!  \\n\\n " + ex.Message);
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


        protected void Ucitaj(int TipPosPar, int? idPosPar, string Search)
        {

            TraceLogging.TraceLogger.trace(this.GetType(), (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this, "");

            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.VratiPoslovniPartnerRequest zahtev = new wcfMultibuki.VratiPoslovniPartnerRequest(TipPosPar, idPosPar, Search);
            wcfMultibuki.VratiPoslovniPartnerResponse odgovor = new wcfMultibuki.VratiPoslovniPartnerResponse();

            try
            {
                odgovor = client.VratiPoslovniPartner(zahtev);
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida za poslovne partnere u metodi VratiPoslovniPartner!  \\n\\n" + ex.Message);
                return;
            }

            try
            {
                if (odgovor.VratiPoslovniPartnerResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.VratiPoslovniPartnerResult.dtGreska.Rows[0][0].ToString());
                else
                {

                    if (odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows.Count == 0)
                    {
                        ObradaiObavestenje("Ne postoje podaci za tražene parametre !");
                        return;
                    }

                    gv.DataSource = odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows;
                    gv.DataBind();
                    gv.PageIndex = 0; 
                    lblUkupanBroj.Text = "Ukupan broj: " + odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows.Count;


                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u proceduri kod vraćanja grida poslovne partnere u metodi VratiPoslovniPartner!  \\n\\n " + ex.Message);
                return;
            }

        }

        protected void PromenaTipaLica(object sender, EventArgs e)
        {
            int TipPosPar = int.Parse(rblTipLica.SelectedValue);
            int? idPosPar = null;
            string search = txtSearch.Text;
          
            Ucitaj(TipPosPar, idPosPar, search);
        

            if (TipPosPar == 1)
            {
                gv.Columns[4].Visible = false;
            }
            else
           {
                gv.Columns[4].Visible = true;
            }
         //   search = ""; //tanjaovde
            ddlPosPar_popuni(TipPosPar, idPosPar, search);
            //txtSearch.Text = "";
        
        }       

        protected void btnUnesi_OnClick(object sender, EventArgs e)
        {
            int TipLica = int.Parse(rblTipLica2.SelectedValue);
            int? idpospar = null;
            int? KucniBr; int? Stan;
            int idPP; 
            

            try
            {
               idPP = int.Parse(tbIdpp.Text.ToString());   
            }
            catch (Exception)
            {
                idPP = 0;              
            }
         
            string Naziv = tbNazivPP.Text;
            string Ime = tbImePP.Text;
            string Prezime = tbPrezimePP.Text;
            string Adresa = tbAdresa.Text;           

            if (String.IsNullOrEmpty(tbKucniBroj.Text))
            {
            //    tbKucniBroj.Text = "0";
                KucniBr = null;
            }
            else
            { 
           KucniBr = int.Parse(tbKucniBroj.Text);
            }

            string KucniPodBr = tbPodBroj.Text;

            int Sprat;
            try
            {
                Sprat = int.Parse(ddlSprat.SelectedValue);
            }
            catch (Exception)
            {
                Sprat = 0;
            }

            if (String.IsNullOrEmpty(tbStan.Text))
            {               
                Stan = null;
            }
            else
            {
                Stan = int.Parse(tbStan.Text);
            }
          
            string Telefon = tbTelefon.Text;
            string TelefonMob = tbTelMobilni.Text;
            string pib = tbPib.Text;
            string Email = tbEMail.Text;
            string webAdresa = tbWebAdresa.Text; 
           
            int PttBroj;
            try
            {
                PttBroj = int.Parse(ddlMesto.SelectedValue);
            }
            catch (Exception)
            {
                PttBroj = 0;
            }

            if (TipLica == 1)
            {
                if (Ime == "" || Ime == " ")
                {
                    ObradaiObavestenje("Neophodno je uneti Ime lica!");
                    return;
                }
                if (Prezime == "" || Prezime == " ")
                {
                    ObradaiObavestenje("Neophodno je uneti Prezime lica!");
                    return;
                }
            }
            else
            {
                if (Naziv == "" || Naziv == " ")
                {
                    ObradaiObavestenje("Neophodno je uneti Naziv partnera!");
                    return;
                }
            }

            int IDKorisnikPromene = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            //tanjaovde za insert i update
            if (IzmeniPoslovnogPartnera(TipLica, idPP, Naziv, Ime, Prezime, Adresa, KucniBr, KucniPodBr, Sprat, Stan, Telefon, TelefonMob, pib, Email, webAdresa, PttBroj, IDKorisnikPromene))
           
                {
                ObradaiObavestenje("Uspešno insertovani podaci o poslovnom partneru!");

                TipLica = int.Parse(rblTipLica2.SelectedValue);
                string search = txtSearch.Text;
                Ucitaj(TipLica, idpospar, search);
                Isprazni();
                search = "";
                ddlPosPar_popuni(TipLica, idpospar, search);
                
              btnUnesiNovog.Enabled = true;
               pnlUnos.Visible = false;
                gv.Enabled = true;
            }

        }

        protected void Isprazni()
        {
            tbIdpp.Text = "";
            tbNazivPP.Text="";
            tbImePP.Text="";
            tbPrezimePP.Text="";
            tbAdresa.Text="";
            tbKucniBroj.Text="";
            tbPodBroj.Text="";
            ddlSprat.SelectedIndex = 0;
            tbStan.Text="";
            tbTelefon.Text="";
            tbTelMobilni.Text="";
            tbPib.Text="";
            tbEMail.Text="" ;
            tbWebAdresa.Text="";           
            ddlMesto.SelectedIndex = 0;
        }

       

        protected void btnOdustani_OnClick(object sender, EventArgs e)
        {
            btnUnesiNovog.Enabled = true;
            pnlUnos.Visible = false;
            gv.Enabled = true;

            Isprazni();

        }

        protected void PromenaTipaLica2(object sender, EventArgs e)
        {
            int TipPosPar = int.Parse(rblTipLica2.SelectedValue);
            int? idPosPar = null;                      


            if (TipPosPar == 1)
            {
                tbNazivPP.Enabled = false;
                tbPib.Enabled = false;               
                Focus1.Focus();
            }
            else
            {
                tbNazivPP.Enabled = true;
                tbPib.Enabled = true;              
                Focus1.Focus();
            }

            string search = txtSearch.Text;
            Ucitaj(TipPosPar, idPosPar, search);

            if (TipPosPar == 1)
            {
                gv.Columns[4].Visible = false;
            }
            else
            {
                gv.Columns[4].Visible = true;
            }
            search = "";
            ddlPosPar_popuni(TipPosPar, idPosPar, search);
            rblTipLica.SelectedValue = rblTipLica2.SelectedValue;
          
        }

        protected void ddlPosPar_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrikaziPosPar();
        }


        protected void PrikaziPosPar()
        {
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
         
            int TipLica = int.Parse(rblTipLica.SelectedValue);
            int? IDPosPar;
            txtSearch.Text = "";
            string search = txtSearch.Text;

            try
            {
                IDPosPar = int.Parse(ddlPosPar.SelectedValue);
            }
            catch (Exception)
            {
                ObradaiObavestenje("Niste odabrali poslovnog partnera u  dd listi");
                return;
            }

            wcfMultibuki.VratiPoslovniPartnerRequest zahtev = new wcfMultibuki.VratiPoslovniPartnerRequest(TipLica, IDPosPar, search);
            wcfMultibuki.VratiPoslovniPartnerResponse odgovor = new wcfMultibuki.VratiPoslovniPartnerResponse();

            try
            {
                odgovor = client.VratiPoslovniPartner(zahtev);
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка у процедури VratiPoslovniPartner при позиву методе VratiPoslovniPartner из сервиса!  \\n\\n" + ex.Message);
                return;
            }

            if (odgovor.VratiPoslovniPartnerResult.dtGreska.Rows.Count > 0)
            {
                ObradaiObavestenje(odgovor.VratiPoslovniPartnerResult.dtGreska.Rows[0][0].ToString());
                return;
            }
            else
            {
                try
                {
                    gv.Visible = true;
                    gv.DataSource = odgovor.VratiPoslovniPartnerResult.dtVratiPoslovniPartner.Rows;
                    gv.DataBind();
                }
                catch (Exception ex)
                {
                    ObradaiObavestenje("Грешка у апликацији у методи PrikaziPosPar!  \\n\\n " + ex.Message);
                    return;
                }
            }
        }

     /* protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
             foreach (GridViewRow red in gv.Rows)
              {
                  foreach (TableCell celija in red.Cells)
                  {
                      if (celija.Text.Contains(txtSearch.Text))
                      {
                          red.Visible = true;
                          break;
                      }
                      else
                      {
                          red.Visible = false;
                      }
                  }
              }

        }*/   

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            int TipLica = int.Parse(rblTipLica.SelectedValue);
            int? Idpospar = null;
            string search = txtSearch.Text;    
            
            Ucitaj(TipLica, Idpospar, search);
            ddlPosPar_popuni(TipLica, Idpospar, search);
        }

       
    }
}