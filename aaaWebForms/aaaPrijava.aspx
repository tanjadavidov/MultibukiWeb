<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aaaPrijava.aspx.cs" Inherits="MultibukiWeb.aaaWebForme.aaaPrijava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multibuki:Prijava</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="Tanja Davidov" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    
    <link rel="icon" href="~/Images/h2o.png" />
     <!-- Custom styles for this template -->   
   <link href="~/CSS/login-style.css" type="text/css" rel="stylesheet" /> 

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
    
</head>
<body>
<%--    <div id="bg1" style="background-image: linear-gradient(rgba(0,0,0,0.3), rgba(0,0,0,0.3)), url('../../Images/clean-water-topbanner.jpg'); background-size: cover; background-position:center center; background-repeat:no-repeat;"></div>--%>
        <div id="bg1" style="background-image: linear-gradient(rgba(0,0,0,0.3), rgba(0,0,0,0.3)), url('../../Images/slika.jpg'); background-size: cover; background-position:center center; background-repeat:no-repeat;"></div>

    <form id="form1" runat="server" defaultbutton="btnPrijava">

        <header id="top">
        
          <div id="logo"><a href="http://rs.zepter.com/MainMenu/Products/HomeCare/Cleansy-Water/ProductRange.aspx" target="_blank"><img src="../../Images/h2o.png" /></a></div>
          <div id="naslov">
<%--            <h3 style="font-size:17px;">Multibuki ... čista voda</h3>--%>
            <%--<h6>Multibuki</h6>--%>

          </div>
           
        </header>

        <div>  &nbsp;&nbsp;
             <asp:DropDownList ID="ddlFirmaKorisnik" runat="server" AutoPostBack="False" Width="200px"></asp:DropDownList>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblNapomena" Visible="false" runat="server" Font-Bold="True" ForeColor="#006878">Da bi pristupili ovoj aplikacije morate biti prijavljeni</asp:Label>

        </div>

        <div class="loginbox" style="background-image: url('../../Images/notebook.png'); background-size: 310px 400px; background-repeat: no-repeat;">
            <img src="../../Images/avatarPurple.png" class="avatar" />
            <h1>Prijava</h1>
            <div id="form2" >
                <p>Korisničko ime</p>
                <asp:TextBox runat="server"   ID="tbKorisnickoIme" type="text" name="" placeholder="Unesite korisničko ime"></asp:TextBox>
                
                <p>Lozinka</p>
                <asp:TextBox runat="server"  ID="tbLozinka" TextMode="Password" type="password" name="" placeholder="Unesite lozinku"></asp:TextBox>
            
                <asp:Button ID="btnPrijava" type="submit" name=""  runat="server" Text="Prijavite se"  OnClientClick="ClientSideClick(this)" UseSubmitBehavior="false" OnClick="btnPrijava_Click"/> <%--CssClass="dugmeLogin" --%>
            </div>
        </div>
   
    </form>
</body>
</html>
