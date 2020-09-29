<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPocetna.ascx.cs" Inherits="MultibukiWeb.UserControls.ucPocetna" %>
<script type="text/javascript" src="https://ajax.microsoft.com/ajax/jquery/jquery-1.6.2.min.js"></script> 

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

    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../Images/Kontrole/minusz.png");
        $(this).attr("alt", "-");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../Images/Kontrole/plusz.png");
        $(this).attr("alt", "+");
        $(this).closest("tr").next().remove();
    });

</script>



<style type="text/css">
  
    @media screen and (max-width: 1200px) and (min-width: 576px) {
        .razmak1 {
            padding-top: 16px;
        }
    }

    @media screen and (max-width: 576px) {
        .razmak2 {
            padding-top: 16px;
        }
    }
</style>

<br />

<div class="container" style="background: rgba(255,255,255,.85);">
    <br />

     <div class="row">
        <div class="col-xl-7 col-12" style="overflow-x:auto;">
            <asp:GridView ID="gvVesti" runat="server" AutoGenerateColumns="False" EmptyDataText="НЕМА ВЕСТИ ЗА ПРИКАЗ"
                OnPageIndexChanging="gvVesti_PageIndexChanging" OnRowDataBound="gvVesti_OnRowDataBound"
                DataKeyNames="IDVest" EnableViewState="True" Visible="true" AllowPaging="True" PageSize="15">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="20px">
                        <ItemTemplate>
                            <img alt="+" style="cursor: pointer" src="../Images/Kontrole/plusz.png" />
                            <asp:Panel ID="pnlVesti" runat="server" Style="display:none">
                                <asp:TextBox ID="tbTekstVesti" TextMode="MultiLine"
                                        MaxLength="1500" runat="server" Width="99%"
                                        Font-Names="Arial" Font-Size="10pt" Height="75px">
                                </asp:TextBox>
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="IDVest" HeaderText="IDVest" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        Visible="False"/>
                    <asp:BoundField DataField="DatumOd" HeaderText="Датум" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="True" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                        Visible="True"/>
                    <asp:BoundField DataField="DatumDo" HeaderText="Датум до" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="True" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                        Visible="false"/>
                    <asp:BoundField DataField="Naslov" HeaderText="ВЕСТИ" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="True" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                        Visible="True" HeaderStyle-Width="550px"/>
                    <asp:BoundField DataField="Tekst" HeaderText="Текст" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="True" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                        Visible="false"/>
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <EditRowStyle BackColor="#2461BF"/>
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#17f1d7" Font-Bold="True" ForeColor="Black" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>

        <div class="col-xl-1"></div>

        <div class="col-xl-4 col-12 razmak1 razmak2" style="overflow-x:auto;">
            <asp:GridView ID="gvKorisniLinkovi" runat="server" AutoGenerateColumns="False" EmptyDataText="НЕМА КОРИСНИХ ЛИНКОВА ЗА ПРИКАЗ"
                DataKeyNames="Url" EnableViewState="True" Visible="true" ShowHeader="true" HeaderStyle-HorizontalAlign="Center"
                BorderColor="White" BorderStyle="None" BorderWidth="0px" Font-Italic="False" GridLines="None"
                OnPageIndexChanging="gvKorisniLinkovi_PageIndexChanging" AllowPaging="True" PageSize="15">
                <Columns>
                    <asp:BoundField DataField="IDKorisniLink" HeaderText="IDKorisniLink" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top"
                        Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Wrap="False"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField DataField="Url" HeaderText="Url" ReadOnly="True"
                        HeaderStyle-Wrap="false" ItemStyle-Wrap="True" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"
                        Visible="False">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="True"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Korisni linkovi">
                        <ItemTemplate>
                            <asp:Button ID="btnIdiNaKorisniLink" runat="server" CssClass="dugmeTamno300 mt-2" OnClick="btnIdiNaKorisniLink_Click"
                                Text='<%# Eval("Naziv") %>' UseSubmitBehavior="false" />                                       
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <RowStyle BackColor="White" />
                <EditRowStyle BackColor="White"/>
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#17f1d7" Font-Bold="True" ForeColor="Black" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>

</div>
<br />