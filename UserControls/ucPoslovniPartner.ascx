<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPoslovniPartner.ascx.cs" Inherits="MultibukiWeb.UserControls.ucPoslovniPartner" %>

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

<asp:UpdatePanel ID="UpdatePanel5" runat="server">
    <ContentTemplate>
        <div class="container">

              <div class="row">
              <div class="col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">
                  <p>Prikaži poslovne partnere</p>
               </div>
               </div>

            <div class="row">
                <%--<div class="col-xl-2 col-md-6 col-sm-6 col-12">
                    <asp:Label ID="Label2" runat="server" Visible="false" Text="Врста лица:" CssClass="float-md-right desno" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                </div>--%>
                <div class="col-xl-3 col-md-6 col-sm-6 col-12">
                    <asp:RadioButtonList ID="rblTipLica" style="color:white; font-size:15px" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="PromenaTipaLica" AutoPostBack="true" OnDisposed="PromenaTipaLica"   >                 
                        <asp:ListItem Value="1">&nbsp; Fizičko lice &nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">&nbsp; Pravno lice &nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
          
                 <div class="col-xl-5 col-md-6 col-sm-6 col-12 ">
                       <asp:DropDownList ID="ddlPosPar" runat="server" Width="80%" Height="24px" CssClass="glowing-border" Enabled="true"
                           OnSelectedIndexChanged="ddlPosPar_SelectedIndexChanged" AutoPostBack="true"    ></asp:DropDownList>

                    </div>
             
           
                    <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label26" CssClass="float-md-left" style="color:white; font-size:14px; font-weight: bold;" runat="server" Text="Pronađi"  Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                   </div>

              
                  <div class="col-xl-3 col-md-6 col-sm-6 col-12">                      
                     <asp:TextBox ID="txtSearch" CssClass="glowing-border search"   runat="server"   Width="200px"  Height="24px" OnTextChanged="txtSearch_TextChanged"  AutoPostBack="true" ></asp:TextBox>   
                  </div>

                  </div>
                

       
            
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


            

    <asp:UpdatePanel ID="Panel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PrikazPanel2" runat="server" Width="100%" Visible="true">
                           <%--   <div class="container">--%>
                          
                             


                        <br />
                            <div class="row">
                                <div class="col-12" style="overflow-x: auto;">
                                    
                                    <asp:GridView ID="gv" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" EmptyDataText="Nema podataka!" DataKeyNames="IdPosPar"
                                        OnRowDataBound="gv_RowDataBound" Width="99%" Visible="true" PageSize="10"  OnPageIndexChanging="gv_PageIndexChanging" AllowPaging="true"  >
                                        <Columns>

                                              <asp:TemplateField HeaderText="PttBroj" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPttBroj" runat="server" Text='<%# Eval("PttBroj") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>    
                                               <asp:TemplateField HeaderText="Sprat" Visible="false">
                                              <ItemTemplate>
                                                    <asp:Label ID="lblidSprat" runat="server" Text='<%# Eval("idSprat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>  



                                              <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" headerstyle-width="3%" >
                                                <ItemTemplate>
                                                      <asp:Button ID="btnObrisi" runat="server"   CssClass="dugme100" OnClick="btnObrisi_Click" OnClientClick="if(!confirm('Sigurni ste?')) return false;" 
                                                          Text="Obriši" UseSubmitBehavior="false" Width="99%" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="šifra" Visible="true" headerstyle-width="2%">
                                                <ItemTemplate>
                                                        <asp:Label ID="lblidPosPar" runat="server" Text='<%# Eval("idPosPar") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                             


                                             <asp:TemplateField HeaderText="Naziv Pos. partnera" Visible="true" headerstyle-width="20%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbNaziv" runat="server" style="text-align: left" Text='<%# Eval("NazivPosPar") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Ime" Visible="true" headerstyle-width="10%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbIme" runat="server" style="text-align: left" Text='<%# Eval("ImePosPar") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Prezime" Visible="true" headerstyle-width="10%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbPrezime" runat="server" style="text-align: left" Text='<%# Eval("PrezimePosPar") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Adresa" Visible="true" headerstyle-width="15%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbAdresa" runat="server" style="text-align: left" Text='<%# Eval("Adresa") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="kućni broj" Visible="true" headerstyle-width="3%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbKucniBroj" runat="server" style="text-align: center" Text='<%# Eval("KucniBroj") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="pod broj" Visible="true" headerstyle-width="3%"  >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbKPodBroj" runat="server" style="text-align: center" Text='<%# Eval("KucniPodbroj") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                            <asp:TemplateField HeaderText="sprat" headerstyle-width="5%" HeaderStyle-CssClass="poravnavanjeTekstaCentar vertikalnoCentriranTekst" ItemStyle-Font-Size="8" >
                                                <ItemTemplate >
<%--                                                       <asp:TextBox ID="tbSprat" runat="server" Text='<%# Eval("Sprat") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"  ></asp:TextBox>                                                                                                       --%>
                                                      <asp:DropDownList ID="ddlSprat" runat="server" Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="stan" headerstyle-width="3%"  >
                                                <ItemTemplate>
                                                       <asp:TextBox ID="tbStan" runat="server" Text='<%# Eval("stan") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox>                                                                                                       
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Telefon" Visible="true" headerstyle-width="13%"  >
                                                <ItemTemplate>
                                                   <%-- <asp:Label ID="lblPttBroj" runat="server" Text='<%# Eval("PttBroj") %>'></asp:Label>--%>
                                                    <asp:TextBox ID="tbTelefon" runat="server" style="text-align: left" Text='<%# Eval("TelefonHome") %>' Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:TextBox> 
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                              <asp:TemplateField HeaderText="Mesto" Visible="true" headerstyle-width="15%" >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMesto" runat="server" Width="99%" Height="24px" CssClass="glowing-border" Enabled="false"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                              



                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" headerstyle-width="5%">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnIzmeni" runat="server"  CssClass="dugme100" Width="99%" OnClick="btnIzmeni_Click" OnClientClick="ClientSideClick(this)" Text="Izmeni" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  headerstyle-width="8%">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnSacuvajIzmene" Enabled="false" runat="server" Width="99%"  CssClass="dugme100" OnClick="btnSacuvajIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Sačuvaj" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  headerstyle-width="8%">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnOtkaziIzmene" Enabled="false" runat="server" Width="99%"  CssClass="dugme100" OnClick="btnOtkaziIzmene_Click" OnClientClick="ClientSideClick(this)" Text="Otkaži" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center"  headerstyle-width="8%">
                                                <ItemTemplate>
                                                   <asp:Button ID="btnPrikaziDetalje"  runat="server" Width="99%"  CssClass="dugme100" OnClick="btnPrikaziDetalje_Click" OnClientClick="ClientSideClick(this)" Text="Detalji" UseSubmitBehavior="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <RowStyle BackColor="#EFF3FB"  />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#005094" />
                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                        <HeaderStyle BackColor="#dacef2" ForeColor="Black" Font-Size="Small" HorizontalAlign="Center"/>
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                   
                                             <div class="row">
                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12" style="text-align:left">
                                <asp:Label ID="lblUkupanBroj" runat="server" Text="Broj Redova" CssClass="float-md-right" Font-Bold="true" Font-Size="11px" Font-Names="Sans-serif"></asp:Label>
                              </div>
                             </div>

                                </div>
                            </div>
                          <%--  </div>--%>

                

           

    
            
  <div class="container">
    <div class="row">
   <%-- <div class="col-12" style="overflow-x:auto; font-style: italic ">--%>
           <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
<asp:Button ID="btnUnesiNovog" runat="server" CssClass="dugme300"  Text="Unos novog poslovnog partnera" Style="font-size: 14px; "
    OnClick="btnUnesiNovog_onclick" OnClientClick="ClientSideClick(this)" UseSubmitBehavior="False"  />
        </div>       
    </div>

    </div>
          </div>



        </asp:Panel>
    </ContentTemplate>
     </asp:UpdatePanel>  


<br />
<br />



                            
  



      <asp:UpdatePanel ID="Panel1" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="pnlUnos" runat="server" Width="100%" Visible="false" BorderStyle="Double">
                         
                      <div class="container" >   
                           <div class="row"  >
                         
                                

                                <div class="col-xl-6 col-md-6 col-sm-6 col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;" >
                                 Unos podataka o poslovnom partneru
                               </div>
                                  
                                <div class="col-xl-6 col-md-6 col-sm-6 col-12" style="font-weight: bold; font-style: italic; text-decoration: underline; background-color: #dacef2;">                                  
                                <asp:RadioButtonList ID="rblTipLica2" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="PromenaTipaLica2" AutoPostBack="true" >                      
                                    <asp:ListItem Value="1">&nbsp; Fizičko lice &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="2">&nbsp; Pravno lice &nbsp;&nbsp;</asp:ListItem>
                                </asp:RadioButtonList>
                                      
                                 </div>
                              </div>
                         
                 <div class="row">
                      <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label25" CssClass="float-md-left" runat="server" Text="Id poslovnog partnera" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
                 <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                            <asp:TextBox ID="tbIdpp" CssClass="glowing-border" runat="server" MaxLength="50"  Height="24px" Enabled="false"></asp:TextBox>   <%--Width="225px" OnTextChanged="tbNazivJedMere_TextChanged"--%>
                        </div>
                 </div>
     <div class="row">
        <%--  <div class="col-12" style="overflow-x: auto;">--%>
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label4" CssClass="float-md-left" runat="server" Text="Naziv" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbNazivPP" CssClass="glowing-border" runat="server" MaxLength="50" Width="180px"  Height="24px" ></asp:TextBox>   <%--Width="225px" OnTextChanged="tbNazivJedMere_TextChanged"--%>
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label11" runat="server" Text="(Unesi naziv)"  Font-Bold="False"   Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
    
          <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label2" CssClass="float-md-left" runat="server" Text="Ime"  Width="10%" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbImePP" CssClass="glowing-border" runat="server" MaxLength="50" Width="180px" Height="24px"    ></asp:TextBox>   <%--OnTextChanged="tbNazivJedMere_TextChanged"--%>
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label3" runat="server" Text="(ime)"    Font-Bold="False"  Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>       
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label5" CssClass="float-md-left" runat="server" Text="Prezime"  Width="10%" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbPrezimePP" CssClass="glowing-border" runat="server" MaxLength="50" Width="180px" Height="24px" ></asp:TextBox> <%--  OnTextChanged="tbSkracNazJedMere_TextChanged"--%>
            </div>
             <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label1" runat="server" Text="(prezime)"  Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
   <%--     </div>--%>
</div>


      <div class="row">
          <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label6" CssClass="float-md-left" runat="server" Text="Adresa"   Font-Bold="true"  Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbAdresa" CssClass="glowing-border" runat="server" MaxLength="50" Width="180px" Height="24px" ></asp:TextBox>   <%--Width="225px"  OnTextChanged="tbNazivJedMere_TextChanged"--%>
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label7" runat="server" Text="(Unesi ulicu)"   Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>

       
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label8" CssClass="float-md-left" runat="server" Text="Kućni broj"  Width="50%"  Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
               <asp:TextBox ID="tbKucniBroj" Text="" CssClass="glowing-border" runat="server" MaxLength="50" Width="50px" Height="24px" ></asp:TextBox>             
              <b style="color:black;font-size:12px; font-family:Sans-serif">  /podBroj</b> 
               <asp:TextBox ID="tbPodBroj" Text="" CssClass="glowing-border" runat="server" MaxLength="50" Width="40px" Height="24px" ></asp:TextBox> 
            </div>
             
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label15" runat="server" Text="(kućni broj /podbroj)" Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>

         
           <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label13" CssClass="float-md-left" runat="server" Text="sprat" Width="50%" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
              <%--  <asp:TextBox ID="tbSprat" Text="" CssClass="glowing-border" runat="server" MaxLength="50" Width="50px" Height="24px" ></asp:TextBox>  --%>
           <asp:DropDownList ID="ddlSprat" runat="server" Width="70px" Height="24px" CssClass="glowing-border" ></asp:DropDownList>
          <b style="color:black;font-size:12px; font-family:Sans-serif">  /stan</b>   
<%--            <asp:Label ID="Label26" CssClass="float-md-left" runat="server" Text=" /stan "  Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label> --%>
                <asp:TextBox ID="tbStan" Text="" CssClass="glowing-border" runat="server" MaxLength="50" Width="40px" Height="24px" ></asp:TextBox> 
            </div>
             <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label14" runat="server" Text="(sprat /stan)"  Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>

          
        </div>

                               <div class="row">
        <%--  <div class="col-12" style="overflow-x: auto;">--%>
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label9" CssClass="float-md-left" runat="server" Text="Telefon" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbTelefon" CssClass="glowing-border" runat="server" MaxLength="50" Width="150px"  Height="24px" ></asp:TextBox>  
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label10" runat="server" Text="(Unesi telefon)"  Font-Bold="False"   Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
    
          <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label12" CssClass="float-md-left" runat="server" Text="Telefon mobilni" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbTelMobilni" CssClass="glowing-border" runat="server" MaxLength="50" Width="150px" Height="24px"    ></asp:TextBox>  
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label16" runat="server" Text="(tel. mobilni)"   Font-Bold="False"  Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>       
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label17" CssClass="float-md-left" runat="server" Text="PIB" Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbPib" CssClass="glowing-border" runat="server" MaxLength="50" Width="150px" Height="24px" ></asp:TextBox>
            </div>
             <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label18" runat="server" Text="(unesi PIB)"  Font-Bold="False" Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
   <%--     </div>--%>
</div>
  <div class="row">
       
            <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label19" CssClass="float-md-left" runat="server" Text="e-mail" Font-Bold="true" Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbEMail" CssClass="glowing-border" runat="server" Width="180px" MaxLength="50"  Height="24px" ></asp:TextBox>  
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label20" runat="server" Text="(Unesi e-mail)"  Font-Bold="False"   Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>
    
          <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label21" CssClass="float-md-left" runat="server" Text="Web adresa"  Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:TextBox ID="tbWebAdresa" CssClass="glowing-border" runat="server" Width="180px" MaxLength="50"  Height="24px"    ></asp:TextBox>  
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label22" runat="server" Text="(Unesi Web adresu)"   Font-Bold="False"  Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>       
          
      <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label23" CssClass="float-md-left" runat="server" Text="Mesto"  Font-Bold="true" Font-Size="12px" Font-Names="Sans-serif"></asp:Label>
            </div>
            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 col-12">
         <%--       <asp:TextBox ID="TextBox1" CssClass="glowing-border" runat="server" Width="180px" MaxLength="50"  Height="24px"    ></asp:TextBox>  --%>
                 <asp:DropDownList ID="ddlMesto" runat="server" Width="99%" Height="24px" CssClass="glowing-border" ></asp:DropDownList>
            </div>
            <div class="col-xl-1 col-lg-4 col-md-12 col-sm-12 col-12">
                <asp:Label ID="Label24" runat="server" Text="(Izabri mesto)"   Font-Bold="False"  Font-Italic="True" Font-Size="Small" ForeColor="Red"></asp:Label>
            </div>    
  
</div>

        <div class="row">
          
            <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Button ID="btnUnesi" runat="server" CssClass="dugme150" Width="225px" Enabled="true"  OnClientClick="ClientSideClick(this)" 
                    Style="text-align: center; font-size: 14px;" Text="Unesi" UseSubmitBehavior="false"    OnClick="btnUnesi_OnClick"  />
            </div>
             <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">

        </div>
      <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 col-12">
         <asp:Button ID="btnOdustani" runat="server" CssClass="dugme150" Width="225px" Enabled="true"  OnClientClick="ClientSideClick(this)" 
           OnClick="btnOdustani_OnClick"   Style="text-align: center; font-size: 14px;" Text="Odustani" UseSubmitBehavior="false" />  
       </div>
            
           

             
        </div>
    <div class="row">
       <asp:TextBox runat="server" id="Focus1" BorderStyle="None" Width="0px" ></asp:TextBox>   <%--style="background-color:#509ee3"--%>
     </div>             
   
     </div>

    </asp:Panel>
    </ContentTemplate>
     </asp:UpdatePanel>  




<script>
    $(document).ready(function Confirm() {
        $('#<%=ddlPosPar.ClientID%>').chosen();
    });

    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_endRequest(function Confirm() {
        $('#<%=ddlPosPar.ClientID%>').chosen();
    });
</script>