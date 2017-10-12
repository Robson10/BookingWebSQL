<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KlientRezerwuj.aspx.cs" Inherits="HotelWebSQL.Users.Klient.KlientRezerwuj" %>

<%@ Register Assembly="HotelWebSQL" Namespace="HotelWebSQL.Users" TagPrefix="cc1" %>
<%@ Register Assembly="CustomCalendar" Namespace="HotelWebSQL.Users" TagPrefix="cc2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: lightgrey">
    <form id="form1" runat="server">
        <center>
        <div>
            <asp:Label Text="Rezerwacja" runat="server" />
            <table>
                <tr>
                    <cc2:CustomCalendar ID="KalendarzOd" runat="server" ImageButtonImageUrl="E:\Programming\Hotel\HotelWebSQL\HotelWebSQL\Images\calendar.jpg" style="width:30px;" labelText="Od"/>
                    <cc2:CustomCalendar ID="KalendarzDo" runat="server" ImageButtonImageUrl="E:\Programming\Hotel\HotelWebSQL\HotelWebSQL\Images\calendar.jpg" style="width:30px;" labelText="Do"/>
                </tr>
            </table>  
        </div>
    
        <div>
            <asp:Label Text="Pokoj" runat="server" />
            <asp:DropDownList ID="PokojIluOsobowy" runat="server" AutoPostBack="True" /> 
            <asp:Label Text="osobowy" runat="server" />
            <br />
            <br />
            <asp:Button ID="Szukaj" runat="server" Text="Szukaj" OnClick="Szuakj_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
       </div>
    <br/>
    <div>
        <asp:GridView ID="Grid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
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
    <br/>

    <br/>
    <div>
        <asp:Button ID="Zatwierdz" runat="server" Text="Zatwierdz" OnClick="Zatwierdz_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Anuluj" runat="server" Text="Anuluj" OnClick="Anuluj_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>

    </div>
    </center>
    </form>
</body>
</html>
