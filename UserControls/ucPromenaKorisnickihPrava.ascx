<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPromenaKorisnickihPrava.ascx.cs" Inherits="MultibukiWeb.UserControls.ucPromenaKorisnickihPrava" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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



<br />

<asp:updatepanel ID="lista1" runat="server">
    <ContentTemplate>
<div class="container" style="background: rgba(255,255,255,.85);">
    <br />
    <div class="row">
        <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:Label ID="Label2" CssClass="float-md-right" runat="server" Text="Корисничко име" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
            <asp:TextBox ID="tbImePrezime" runat="server" CssClass="glowing-border" Width="225px" Height="24px" MaxLength="50"></asp:TextBox>
        </div>
        <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
            <asp:Label ID="Label12" runat="server" CssClass="slova" Text="(Унесите латиницом ime.prezime)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnPretrazi" runat="server" OnClick="btnPretrazi_OnClick" CssClass="dugmeTamno150 dugmeMobilni150" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;"
                    Text="Proveri" Enabled="true" UseSubmitBehavior="false"/>
        </div>
        <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
            <asp:Button ID="btnPretrazi0" runat="server" CssClass="dugmeTamno150 dugmeMobilni150" Enabled="true" OnClick="btnOcisti_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Očisti ekran" UseSubmitBehavior="false" />
        </div>
    </div>

     <asp:Panel ID="pnlUnos" runat="server">

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label5" CssClass="float-md-right" runat="server" Text="Ime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbImeKorisnika" CssClass="glowing-border" runat="server" Width="225px" Height="24px" MaxLength="50" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label6" CssClass="float-md-right" runat="server" Text="Prezime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbPrezimeKorisnika" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label9" CssClass="float-md-right" runat="server" Text="Uloga" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:DropDownList ID="ddlUloga" CssClass="glowing-border" runat="server" Width="225px" Height="24px">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label13" CssClass="float-md-right" runat="server" Text="Status" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:DropDownList ID="ddlStatus" CssClass="glowing-border" runat="server" Width="225px" Height="24px">
                    <asp:ListItem Value="False">Neaktivan</asp:ListItem>
                    <asp:ListItem Value="True">Aktivan</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>        

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="lblFirmaKorisnik" CssClass="float-md-right" runat="server" Text="Firma korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:DropDownList ID="ddlFirmaKorisnik" CssClass="glowing-border" runat="server" Width="225px" Height="24px" AppendDataBoundItems="false">
                </asp:DropDownList>
            </div>
        </div>


        <br />

        <div class="row">
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">

            </div>
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Button ID="btnSacuvaj" runat="server" CssClass="dugmeTamno150" Width="225px" Enabled="true" OnClick="btnSacuvaj_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Sačuvaj" UseSubmitBehavior="false" />
            </div>
        </div>

    </asp:Panel>
    <br />
</div>
         </ContentTemplate>
</asp:updatepanel>

<asp:HiddenField ID="hdIme" runat="server" />
<asp:HiddenField ID="hdMail" runat="server" />
<asp:HiddenField ID="hdPrezime" runat="server" />
<asp:HiddenField ID="hdMailDodatni" runat="server" />

<%--<script>
    $(document).ready(function Confirm() {
        $('#<%=ddlUstanove.ClientID%>').chosen();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function Confirm() {
        $('#<%=ddlUstanove.ClientID%>').chosen();
    });
</script>--%>
<br />