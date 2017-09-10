<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KlientForm.aspx.cs" Inherits="HotelWebSQL.Users.Klient.KlientForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background:lightgrey">
<form id="form1" runat="server">
    <div>
        <asp:Button ID="Logout" runat="server" Text="Wyloguj"  style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px;float:right" OnClick="Logout_Click" />
        <asp:Button ID="TwojeRezerwacje" runat="server" Text="Twoje Rezerwacje" OnClick="TwojeRezerwacje_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
        <asp:Button ID="Rezerwuj" runat="server" Text="Rezerwuj" OnClick="Rezerwuj_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
    </div>
    <div>

        <asp:GridView ID="Grid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="Vertical">
            <RowStyle HorizontalAlign ="Center" />
            <AlternatingRowStyle BackColor="White" />
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>

    </div>
</form>
</body>
</html>
