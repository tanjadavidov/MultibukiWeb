<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucArtikal.ascx.cs" Inherits="MultibukiWeb.UserControls.ucArtikal" %>

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
                
                <div class="col-xl-5 col-md-6 col-sm-6 col-12">
                    <asp:RadioButtonList ID="rbTip" style="color:white; font-size:15px" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="PromenaTipa" AutoPostBack="true" OnDisposed="PromenaTipa"   >                 
                        <asp:ListItem Value="1">&nbsp; Uređaj prečistač &nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="2">&nbsp; Arikal za servis &nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="3">&nbsp; Rezervni deo &nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
          
                 <div class="col-xl-4 col-md-6 col-sm-6 col-12 ">
                       <asp:DropDownList ID="ddlArtikal" runat="server" Width="90%" Height="24px" CssClass="glowing-border" Enabled="true"
                           OnSelectedIndexChanged="ddlArtikal_SelectedIndexChanged" AutoPostBack="true"    ></asp:DropDownList>

                    </div>
             
           
                    <div class="col-xl-1 col-lg-4 col-md-6 col-sm-6 col-12">
                <asp:Label ID="Label26" CssClass="float-md-left" style="color:white; font-size:14px; font-weight: bold;"  Width="100px" runat="server" Text="Pronađi"  Font-Size="14px" Font-Names="Sans-serif"></asp:Label>
                   </div>

              
                  <div class="col-xl-2 col-md-6 col-sm-6 col-12">                      
                     <asp:TextBox ID="txtSearch" CssClass="glowing-border search"   runat="server"   Width="180px"  Height="24px" OnTextChanged="txtSearch_TextChanged"  AutoPostBack="true" ></asp:TextBox>   
                  </div>

                  </div>                       
            
        </div>
    </ContentTemplate>
</asp:UpdatePanel>