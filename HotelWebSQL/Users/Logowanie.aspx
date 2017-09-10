<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logowanie.aspx.cs" Inherits="HotelWebSQL.Users.Logowanie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background:lightgrey">
<form id="form1" runat="server">
    <%--<center>--%>
    <div style="width:100%;height:100%;">
        <%--<div style ="width:100%; margin-right:auto;margin-left:auto">--%>
        <div style="text-align:center;width:350px;height:200px; margin:auto auto">
            <asp:Login ID="Login1" runat="server" 
                       BackColor="DarkGray" BorderColor="Transparent" BorderStyle="none" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="Wheat" LoginButtonStyle-BackColor="Gray" OnLoggingIn="Login1_LoggingIn">
                <LoginButtonStyle BackColor="Gray" BorderStyle="None"  ></LoginButtonStyle>
                <TitleTextStyle BackColor="Gray" Font-Bold="True" ForeColor="Wheat" />
            </asp:Login>
        </div>
    </div>
    <%--</center>--%>

</form>
</body>
</html>
