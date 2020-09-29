<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucUnosNovogKorisnika.ascx.cs" Inherits="MultibukiWeb.UserControls.ucUnosNovogKorisnika" %>
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
                myButton.value = "OBRAĐUJE SE ...";
            }
            return true;
        }

</script>




<asp:UpdatePanel ID="lista" runat="server">
    <ContentTemplate>
        <br />      
        <div class="container" style="background: rgba(255,255,255,.85);">
            <br />
            <div class="row">
                <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                    <asp:Label ID="Label2" CssClass="float-md-right" runat="server" Text="Korisničko ime" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                </div>
                <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                    <asp:TextBox ID="tbImePrezime" runat="server" CssClass="glowing-border" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
                </div>
                <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                    <asp:Label ID="Label12" runat="server" CssClass="slova" Text="(Unesite latinicom ime.prezime)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                </div>
                <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
                    <asp:Button ID="btnPretrazi" runat="server" OnClick="btnPretrazi_OnClick"  CssClass="dugmeTamno150 dugmeMobilni150" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;"
                        Text="Proveri" Enabled="true" UseSubmitBehavior="false" /> 
                </div>
                <div class="col-xl-2 col-lg-6 col-md-6 col-sm-6 col-6 razmak">
                    <asp:Button ID="btnOcisti" runat="server" CssClass="dugmeTamno150 dugmeMobilni150" Enabled="true"  OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Očisti ekran" UseSubmitBehavior="false" OnClick="btnOcisti_OnClick" />  <%----%>
                </div>
            </div>



            <asp:Panel ID="pnlUnos" runat="server">

                 <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label1" CssClass="float-md-right" runat="server" Text="Firma korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlFirmaKorisnik" CssClass="glowing-border" runat="server" Width="225px" Height="24px" AppendDataBoundItems="false">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label5" CssClass="float-md-right" runat="server" Text="Ime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="tbImeKorisnika" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label6" CssClass="float-md-right" runat="server" Text="Prezime korisnika" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="tbPrezimeKorisnika" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
                    </div>
                </div>

                  <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label4" CssClass="float-md-right" runat="server" Text="Početna lozinka" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="tbPocetnaLozinka" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-12 col-sm-12 col-12">
                        <asp:Label ID="Label11" runat="server" CssClass="slova" Text="(Unesite latinicom imegodina)" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label7" CssClass="float-md-right" runat="server" Text="E-pošta" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="tbEPosta" CssClass="glowing-border" runat="server" MaxLength="150" Width="225px" Height="24px"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label8" CssClass="float-md-right" runat="server" Text="Telefon" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:TextBox ID="tbTelefon" CssClass="glowing-border" runat="server" MaxLength="50" Width="225px" Height="24px"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Label ID="Label9" CssClass="float-md-right" runat="server" Text="Uloga" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlUloga" CssClass="glowing-border" runat="server" Width="225px" Height="24px" AppendDataBoundItems="false">
                        </asp:DropDownList>
                    </div>
                </div>

           
                <br />
                <div class="row">
                    <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                    </div>
                    <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                        <asp:Button ID="btnSacuvaj" runat="server" CssClass="dugmeTamno150" Width="225px" Enabled="true" OnClick="btnSacuvaj_OnClick" OnClientClick="ClientSideClick(this)" Style="text-align: center; font-size: 14px;" Text="Sačuvaj" UseSubmitBehavior="false" />   <%--  --%>
                    </div>
                </div>
            </asp:Panel>



            <br />
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

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


