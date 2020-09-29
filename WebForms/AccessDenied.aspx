<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="MultibukiWeb.WebForme_UserControle.AccessDenied" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Multibuki:Nemate pravo pristupa</title>

    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="icon" href="~/Images/BeogradLogo.png" />

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
                myButton.value = "Obrađuje se...";
            }
            return true;
        }
    </script>

    <link rel="stylesheet" type="text/css" href="~/CSS/stilovi.css" />

    <style type="text/css">
        .tekst {
            position:absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%);
            text-align:center;
        }

       
    </style>
</head>
<body>
    <form id="form1" runat="server">     
        <asp:Label runat="server" ID="lblNemataPravoPristupa" Font-Bold="True" ForeColor="#C3010D" CssClass="tekst">Ovoj web strani nemate pravo pristupa!  !!!</asp:Label>   
    </form>
</body>
</html>
