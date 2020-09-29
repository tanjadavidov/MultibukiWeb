<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaaKorisnik.aspx.cs" Inherits="MultibukiWeb.aaaWebForme.aaaKorisnik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multibuki:Korisnik</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="Tanja Davidov" />
<%--    <meta http-equiv="X-UA-Compatible" content="ie=edge" />--%>
    
    <link rel="icon" href="~/Images/h2o.png" /> 

        <!-- Custom styles for this template -->
   <link rel="stylesheet" type="text/css" href="~/CSS/stilovi.css" />
   <link href="~/CSS/simple-sidebar.css" rel="stylesheet" />

  <%--    <link href="../../CSS/simple-sidebar.css" rel="stylesheet" />--%>  
      
   <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.4.1/css/all.css" />
       
    <!-- Bootstrap core CSS -->
   <link href="~/CSS/bootstrap.min.css" rel="stylesheet" />  <%-- tanjaovde da li ovo ovde treba--%>
    
    <script type="text/javascript">
        function ClientSideClick(myButton) {
            //Client side validation
            if (typeof (Page_ClientValidate) == 'function') {
                if (Page_ClientValidate() == false)
                { return false; }
            }

            //make sure the button is not of type "submit" but "button"
            if (myButton.getAttribute('type') == 'button') {
                //disable the button
                myButton.disabled = true;
                myButton.value = "Sačekajte ...";
            }
            return true;
        }
    </script>

     <style type="text/css">
        body {
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            text-align: left;
        }
    </style>


</head>
<body>

   <div id="bg1" style="background-image: linear-gradient(rgba(0,0,0,0.4), rgba(0,0,0,0.4)), url('../../Images/settings.jpg'); position: fixed; top: 0; left: 0; min-width: 100%; min-height: 100%; z-index: -1000000; background-size: cover; background-position:center center; background-repeat:no-repeat;"></div>
    <form id="form1" runat="server">
           <header id="top">
            <div class="row21">
              <div id="logo"><a href="http://rs.zepter.com/MainMenu/Products/HomeCare/Cleansy-Water/ProductRange.aspx" target="_blank"><img src="../../Images/h2o.png" /></a></div>             

              <div id="naslov">
                  <asp:Label ID="Label1" runat="server" Text="Multibuki"
                     Font-Bold="True" ForeColor="black"></asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                   
                  <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="black"> <i>čista voda</i></asp:Label>
              </div>
          
              <div id="tabela1" class="ml-auto">
                <div class="row">
                  <div class="text-center">
                         <div style="float: left !important; padding-top: 3px; padding-right: 15px;">
                        <asp:Image ID="Image1" CssClass="imageUser" runat="server" Height="65px" Width="65px" />
                          </div>
                    <div style="float: right !important; ">
                      <asp:Label ID="lblKorisnik" runat="server" Visible="true" Font-Bold="True" ForeColor="black"></asp:Label>
                    <div style="padding-top: 10px;"></div>
                        <asp:LinkButton ID="lbtnOdjava" CssClass="btn-danger"  Width="150px" runat="server" Visible="true" Font-Bold="True" ForeColor="white" OnClick="lbtnOdjava_Click"><i class="fas fa-power-off "></i>Odjavite se</asp:LinkButton>    <%-- --%>
                    </div>
                  </div>                      
                </div>
              </div>
            </div>
          </header>
       
          <div class="d-flex" id="wrapper">
            <!-- Sidebar -->
            <!-- Page Content -->
            <div id="page-content-wrapper">

              <nav class="navbar navbar-expand-lg navbar-light bg-light border-bottom" >
                <div id="nazivStranice1">
                    <asp:Label ID="lblNaslov" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>
                </div>
              </nav>

              <br />
             <%-- <br />--%>
              <div class="container-fluid" >
              <!-- Ovde se unose Web Forme -->
                  <div class="container" style="background: rgba(255,255,255,.9);">
                      <br />
                      <div class="row">
                          <div class="col-12 text-center"">
                              <asp:Button ID="btnNazad" runat="server" CssClass="dugme250"  OnClick="btnNazad_Click" OnClientClick="ClientSideClick(this)" Text="Nazad na aplikaciju" UseSubmitBehavior="false" Font-Size="12px" />
                        </div>
                       <%-- <div class="col-md-3 col-sm-6 col-12">
                            <asp:Button ID="btnNazad" runat="server" CssClass="dugme250"  OnClick="btnNazad_Click" OnClientClick="ClientSideClick(this)" Text="Nazad na aplikaciju" UseSubmitBehavior="false" Font-Size="12px" />
                        </div>--%>
                      </div>

                      <br />
                    <div class="row">
                     <div class="col-xl-6 col-12">

                     <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblKorisnickoIme" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Korisničko ime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbKorisnickoIme" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblIme" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Ime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbIme" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblPrezime" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Prezime</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbPrezime" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblEposta" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">E-pošta</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbEposta" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblTelefon" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Telefon</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbTelefon" CssClass="glowing-border" runat="server" Width="250px" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                      <br />

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_Stara" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Stara lozinka</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_Stara" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_Nova" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Nova lozinka</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_Nova" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            <asp:Label ID="lblLozinka_NovaPonovljeno" CssClass="float-md-right" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Potvrdite novu lozinku</asp:Label>
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:TextBox ID="tbLozinka_NovaPonovljeno" CssClass="glowing-border" runat="server" Width="250px" TextMode="Password" Height="24px"></asp:TextBox>
                        </div>
                    </div>

                      <br />

                    <div class="row">
                        <div class="col-md-5 col-sm-6 col-12">
                            
                        </div>
                        <div class="col-md-3 col-sm-6 col-12">
                            <asp:Button ID="btnPromena" runat="server" CssClass="dugme250"  OnClick="btnPromena_Click" OnClientClick="ClientSideClick(this)" Text="Promeni podatke" UseSubmitBehavior="false" Font-Size="12px" />
                        </div>
                        </div>

                    </div>

                        <div class="col-xl-6 col-12" id="slika" runat="server">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="lblSlika" runat="server" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif">Слика</asp:Label>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col-12" >
                                    <asp:Image ID="Image2" runat="server" Height="255px" Width="255px" />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-12" >
                                    <asp:FileUpload ID="FileUpload1" CssClass="lista1" runat="server" Font-Size="14px" Font-Bold="true" />
                                </div>
                            </div>

                            <br />

                            <div class="row">
                                <div class="col-md-4">
                                    <asp:Button ID="btnPromeniSliku" runat="server" CssClass="dugme150"  OnClick="btnPromeniSliku_Click" OnClientClick="ClientSideClick(this)" Text="ПРОМЕНИ СЛИКУ" UseSubmitBehavior="false" Font-Size="12px" />
                                </div>

                                <div class="col-md-4 razmak2">
                                    <asp:Button ID="btnObrisiSliku" runat="server" CssClass="dugme150"  OnClick="btnObrisiSliku_Click" OnClientClick="if(!confirm('Да ли сте сигурни?')) return false;" Text="ОБРИШИ СЛИКУ" UseSubmitBehavior="false" Font-Size="12px" />
                                </div>
                            </div>
                        </div>


                  </div>

                </div>
              <!-- Kraj unosa Web Formi -->
              </div>
            </div>
            <!-- /#page-content-wrapper -->
        </div>

        <br />

         <!-- Footer -->
          <footer class="page-footer font-small bg-light fixed-bottom ">
            <!-- Copyright -->
            <div class="footer-copyright text-center">© <a href="https://hr.wikipedia.org/wiki/Elektrane_na_biomasu_i_otpad" target="_blank">Bio isplativa farma</a>, Novo orahovo, Bačka Topola
            </div>
            <!-- Copyright -->
          </footer>
          <!-- Footer -->


    </form>
</body>
</html>
