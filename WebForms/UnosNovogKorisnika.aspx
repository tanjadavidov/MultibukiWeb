﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Multibuki.Master" AutoEventWireup="true" CodeBehind="UnosNovogKorisnika.aspx.cs" Inherits="MultibukiWeb.WebForms.UnosNovogKorisnika" %>
<%@ Register src="../UserControls/ucUnosNovogKorisnika.ascx" tagname="ucUnosNovogKorisnika" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
       
    <uc1:ucUnosNovogKorisnika ID="ucUnosNovogKorisnika1" runat="server" />
               <div id="bg1" style="background-image: linear-gradient(rgba(0,0,0,0.4), rgba(0,0,0,0.4)), url('../Images/promenaPrava.jpg'); position: fixed; top: 0; left: 0; min-width: 100%; min-height: 100%; z-index: -1000000; background-size: cover; background-position:center center; background-repeat:no-repeat;"></div>

</asp:Content>
