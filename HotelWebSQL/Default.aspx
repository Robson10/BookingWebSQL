<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HotelWebSQL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <center>
        <div style ="width:100%; margin-right:auto;margin-left:auto">
            <asp:Login ID="Login1" runat="server"
                       BackColor="DarkGray" BorderColor="Transparent" BorderStyle="none" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" ForeColor="Wheat" LoginButtonStyle-BackColor="Gray" OnLoggingIn="Login1_LoggingIn">
                <LoginButtonStyle BackColor="Gray" BorderStyle="None"  ></LoginButtonStyle>
                <TitleTextStyle BackColor="Gray" Font-Bold="True" ForeColor="Wheat" />
            </asp:Login>
        </div>
    </center>

</asp:Content>
