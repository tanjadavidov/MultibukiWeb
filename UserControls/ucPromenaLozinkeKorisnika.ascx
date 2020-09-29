<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPromenaLozinkeKorisnika.ascx.cs" Inherits="MultibukiWeb.UserControls.ucPromenaLozinkeKorisnika" %>

<script type ="text/javascript">
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
                myButton.value = "Obrađuje se ...";
            }
            return true;
    }


</script>





<asp:updatepanel ID="lista1" runat="server">
    <ContentTemplate>


<div class="container" style="background: rgba(255,255,255,.9);">
    <br />
    <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label1" CssClass="float-md-right" runat="server" Text="Korisničko ime" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbImePrezime" runat="server" CssClass="glowing-border" Width="225px" Height="24px" AutoPostBack="true" OnTextChanged="tbImePrezime_TextChanged"></asp:TextBox>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label12" runat="server" CssClass="slova" Text="(Unesite latinicom ime.prezime)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnPretrazi" runat="server" OnClick="btnPretrazi_OnClick" CssClass="dugmeTamno150 dugmeMobilni150" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;"
                    Text="Proveri" Enabled="true" UseSubmitBehavior="false"/>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnOcisti" runat="server" CssClass="dugmeTamno150 dugmeMobilni150" Enabled="true" OnClick="btnOcisti_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Očisti ekran" UseSubmitBehavior="false" />
        </div>
    </div>

    <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label2" CssClass="float-md-right" runat="server" Text="Lozinka" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbLozinkaStara"  CssClass="glowing-border" runat="server" Width="225px" Height="24px" autocomplete="off" OnTextChanged="tbLozinkaStara_TextChanged"  AutoPostBack="true"  ></asp:TextBox>   <%-- OnKeyDown="tbLozinkaStara_KeyDown"--%>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label11" runat="server" CssClass="slova" Text="(Unesi latinicom imegodina)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
    </div>
   
     <asp:Panel ID="pnlUnos" runat="server" >

     <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label3" CssClass="float-md-right" runat="server" Text="Lozinka nova" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbLozinka_Nova" CssClass="glowing-border" runat="server" Width="225px" Height="24px" TextMode="Password"></asp:TextBox>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label4" runat="server" CssClass="slova" Text="(Unesi lozinku)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
    </div>

     <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label5" CssClass="float-md-right" runat="server" Text="Ponovi lozinku" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbLozinka_NovaPonovljeno" CssClass="glowing-border" runat="server" Width="225px" Height="24px" TextMode="Password"></asp:TextBox>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label6" runat="server" CssClass="slova" Text="(Unesi lozinku)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
    </div>
    <br />

    <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">

        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Button ID="btnPromeniLozinku" runat="server" CssClass="dugmeTamno150" Width="225px" Enabled="true" OnClick="btnPromeniLozinku_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Promeni lozinku" UseSubmitBehavior="false" />
        </div>
    </div>
    

         </asp:Panel>
    <br />
</div>

 </ContentTemplate>
</asp:updatepanel>