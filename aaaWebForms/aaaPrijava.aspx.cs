using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MultibukiWeb.aaaWebForme
{
    public partial class aaaPrijava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !Page.IsCallback)
            {
                Session.Clear();
               ddlFirmaKorisnik_Popuni();
            }
        }


        protected void btnPrijava_Click(object sender, EventArgs e)
        {
            bool uspesnaPrijava = false;
            bool pocetnaLozinka = false;


            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtaaaRow red = ulaz.dtaaa.NewdtaaaRow();

            if (tbKorisnickoIme.Text == "" || tbKorisnickoIme.Text == " ")
            {
                ObradaiObavestenje("Unesite korisničko ime!");
                return;
            }
            else
                red.korisnickoIme = tbKorisnickoIme.Text;

            if (tbLozinka.Text == "" || tbLozinka.Text == " ")
            {
                ObradaiObavestenje("Unesite lozinku!");
                return;
            }
            else
                red.lozinka = tbLozinka.Text;

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
                ObradaiObavestenje("Greška u proceduri KorisnikPrijava pri pozivu servisa! \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.KorisnikPrijavaResult.dtGreska.Rows.Count > 0)
                {
                    ObradaiObavestenje(odgovor.KorisnikPrijavaResult.dtGreska.Rows[0][0].ToString());
                    return;
                }
                else
                {
                    Session["Korisnik_IDKorisnik"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["IDKorisnik"].ToString();
                    Session["Korisnik_KorisnickoIme"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["KorisnickoIme"].ToString();
                    Session["Korisnik_Ime"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Ime"].ToString();
                    Session["Korisnik_Prezime"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Prezime"].ToString();
                    Session["Korisnik_ePosta"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["ePosta"].ToString();
                    Session["Korisnik_Telefon"] = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Telefon"].ToString();

                    string str = null;
                    try
                    {
                        byte[] bytes = (byte[])odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["Slika"];
                        str = Convert.ToBase64String(bytes);
                    }
                    catch (Exception)
                    {
                        str = null;
                    }
                    Session["Slika"] = str;


                    string s1 = odgovor.KorisnikPrijavaResult.dtKorisnikPrijava.Rows[0]["PocetnaLozinka"].ToString();
                    bool b1 = bool.Parse(s1);
                    if (b1)
                    {
                        //Налог није у потпуности активиран! Потребно је унети нову лозинку
                        Session["Korisnik_PocetnaLozinka"] = "1";
                        pocetnaLozinka = true;
                    }
                    else
                    {
                        Session["Korisnik_PocetnaLozinka"] = "0";
                        pocetnaLozinka = false;
                    }
                    uspesnaPrijava = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                    , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                ObradaiObavestenje("Greška u aplikaciji u metodi btnPrijava_Click pri proveri naloga!  \\n\\n " + ex.Message);
                return;
            }


            //Popuni Mеni
            try
               {

                wcfMultibuki.IwcfMultibuki clientZaMeni = new wcfMultibuki.IwcfMultibukiClient();
                wcfMultibuki.dsUlaz ulazZaMeni = new wcfMultibuki.dsUlaz();
                wcfMultibuki.dsUlaz.dtaaaRow redZaMeni = ulazZaMeni.dtaaa.NewdtaaaRow();

                   redZaMeni.idKorisnik = int.Parse(Session["Korisnik_IDKorisnik"].ToString());
                   ulazZaMeni.dtaaa.AdddtaaaRow(redZaMeni);
                wcfMultibuki.VratiFunkcijuZaKorisnikaRequest zahtevZaMeni = new wcfMultibuki.VratiFunkcijuZaKorisnikaRequest(ulazZaMeni);
                wcfMultibuki.VratiFunkcijuZaKorisnikaResponse odgovorZaMeni = new wcfMultibuki.VratiFunkcijuZaKorisnikaResponse();


                   try
                   {
                       odgovorZaMeni = clientZaMeni.VratiFunkcijuZaKorisnika(zahtevZaMeni);
                   }
                   catch (Exception ex)
                   {
                       ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                           , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                       ObradaiObavestenje("Greška u proceduri aaa.VratiFunkcijuZaKorisnika pri pozivu servisa za popunjavanje menija!  \\n\\n" + ex.Message);
                       return;
                   }
                   try
                   {
                       if (odgovorZaMeni.VratiFunkcijuZaKorisnikaResult.dtGreska.Rows.Count > 0)
                       {
                           ObradaiObavestenje(odgovorZaMeni.VratiFunkcijuZaKorisnikaResult.dtGreska.Rows[0][0].ToString());
                       }
                       else
                       {
                           ////popunjava se menu sa dtMeni iz dsIzlaz, a brišu se sve ostale u Izlazu koje nam ne trebaju             
                           odgovorZaMeni.VratiFunkcijuZaKorisnikaResult.Tables.Remove("dtGreska");

                           string xmlStr = odgovorZaMeni.VratiFunkcijuZaKorisnikaResult.GetXml().Replace("xmlns=\"http://tempuri.org/dsIzlaz.xsd\"", "");
                           xmlStr = xmlStr.Replace("<dsIzlaz >", "<dsIzlaz>");
                           xmlStr = xmlStr.Replace("</dsIzlaz>", "</dsIzlaz>");
                           xmlStr = xmlStr.Replace("<dtVratiFunkcijuZaKorisnika>", "<siteMap");
                           xmlStr = xmlStr.Replace("</dtVratiFunkcijuZaKorisnika>", "\" />");

                           xmlStr = xmlStr.Replace("<NazivFunkcije>", " Funkcija=\"");
                           xmlStr = xmlStr.Replace("</NazivFunkcije>", " ");
                           xmlStr = xmlStr.Replace("<Url>", "\" Url=\"");
                           xmlStr = xmlStr.Replace("</Url>", "");
                           xmlStr = xmlStr.Replace("\r\n", "");
                           xmlStr = xmlStr.Replace("  ", "");
                           xmlStr = xmlStr.Replace(" \"", "\"");
                           Session["Menu"] = xmlStr;
                       }
                   }
                   catch (Exception ex)
                   {
                       ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                           , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                       ObradaiObavestenje("Greška u aplikaciji u metodi btnPrijava_Click kod popunjavanja menija!  \\n\\n " + ex.Message);
                       return;
                   }
               }
               catch (Exception ex)
               {
                   ExceptionLogger.ExceptionLogger.logError(ex, this.GetType()
                       , (new System.Diagnostics.StackTrace(true)).GetFrame(0).GetMethod().Name, this);
                   ObradaiObavestenje(ex.Message);
               }

               if (uspesnaPrijava)
                   if (pocetnaLozinka)
                       Response.Redirect("~/aaaWebForms/aaaKorisnik.aspx");
                   else
                        ExpireAllCookies();
                        Response.Redirect("~/WebForms/Pocetna.aspx");


        }

        private void ExpireAllCookies()
        {

            //HttpCookie cookie = Request.Cookies["ToolsTVExpand"];
            //cookie.Expires = DateTime.Now.AddDays(-1);

            if (HttpContext.Current != null)
            {
                // HttpCookie cookie = Request.Cookies["ToolsTVExpand"];

                HttpCookie cookie = HttpContext.Current.Request.Cookies["ToolsTVExpand"];
                if (cookie != null)
                {
                    var expiredCookie = new HttpCookie(cookie.Name)
                    {
                        Expires = DateTime.Now.AddDays(-1),
                        Domain = cookie.Domain
                    };
                    HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
                }


                // clear cookies server side
                HttpContext.Current.Request.Cookies.Clear();
            }

            //if (HttpContext.Current != null)
            //{
            //    int cookieCount = HttpContext.Current.Request.Cookies.Count;
            //    for (var i = 0; i < cookieCount; i++)
            //    {
            //        var cookie = HttpContext.Current.Request.Cookies[i];
            //        if (cookie != null)
            //        {
            //            var expiredCookie = new HttpCookie(cookie.Name)
            //            {
            //                Expires = DateTime.Now.AddDays(-1),
            //                Domain = cookie.Domain
            //            };
            //            HttpContext.Current.Response.Cookies.Add(expiredCookie); // overwrite it
            //        }
            //    }

            //    // clear cookies server side
            //    HttpContext.Current.Request.Cookies.Clear();
            //}
        }


        private void ddlFirmaKorisnik_Popuni()
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

                ObradaiObavestenje("Грешка у процедури FunkcijaVrati  при позиву методе VratiFirmaKorisnik из сервиса!  \\n\\n" + ex.Message);
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

                    Session["Firma_idFirmaKorisnik"] = int.Parse(ddlFirmaKorisnik.SelectedValue);
                    int idFirmaKorisnik= int.Parse(ddlFirmaKorisnik.SelectedValue);
                    
                    FirmaKorisnik_Popuni(idFirmaKorisnik);




                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка у апликацији у методи VratiFirmaKorisnik!  \\n\\n " + ex.Message);
                return;
            }
        }

        private void FirmaKorisnik_Popuni(int idFirmaKorisnik)
        {



            //  wcfMultibuki.IwcfMultibuki clientZaMeni = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.dsUlaz ulaz = new wcfMultibuki.dsUlaz();
            wcfMultibuki.dsUlaz.dtdboRow red = ulaz.dtdbo.NewdtdboRow();

            wcfMultibuki.dsIzlaz izlaz = new wcfMultibuki.dsIzlaz();

            red.id = idFirmaKorisnik;  //int.Parse(Session["Korisnik_IDKorisnik"].ToString());
            ulaz.dtdbo.AdddtdboRow(red);
       
            wcfMultibuki.IwcfMultibuki client = new wcfMultibuki.IwcfMultibukiClient();
            wcfMultibuki.FirmaKorisnikPrikaziRequest zahtev = new wcfMultibuki.FirmaKorisnikPrikaziRequest(ulaz);
            wcfMultibuki.FirmaKorisnikPrikaziResponse odgovor = new wcfMultibuki.FirmaKorisnikPrikaziResponse();


            try
            {

                odgovor = client.FirmaKorisnikPrikazi(zahtev);
            }
            catch (Exception ex)
            {

                ObradaiObavestenje("Грешка pri pozivu сервиса у процедури FirmaKorisnikPrikazi !  \\n\\n" + ex.Message);
                return;
            }
            try
            {
                if (odgovor.FirmaKorisnikPrikaziResult.dtGreska.Rows.Count > 0)
                    ObradaiObavestenje(odgovor.FirmaKorisnikPrikaziResult.dtGreska.Rows[0][0].ToString());
                else
                {
                  
                Session["Firma_idFirmaKorisnik"] = odgovor.FirmaKorisnikPrikaziResult.dtFirmaKorisnikPrikazi.Rows[0]["idFirmaKorisnik"].ToString();
                Session["Firma_NazivFirmeKorisnika"] = odgovor.FirmaKorisnikPrikaziResult.dtFirmaKorisnikPrikazi.Rows[0]["NazivFirmeKorisnika"].ToString();
                Session["Firma_Opis"] = odgovor.FirmaKorisnikPrikaziResult.dtFirmaKorisnikPrikazi.Rows[0]["Opis"].ToString();
                Session["Firma_Opis1"] = odgovor.FirmaKorisnikPrikaziResult.dtFirmaKorisnikPrikazi.Rows[0]["Opis1"].ToString();            
                }
            }
            catch (Exception ex)
            {
                ObradaiObavestenje("Грешка у апликацији у методи FirmaKorisnik_Popuni!  \\n\\n " + ex.Message);
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



    }
}