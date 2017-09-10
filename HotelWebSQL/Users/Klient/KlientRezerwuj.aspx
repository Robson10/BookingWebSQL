<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KlientRezerwuj.aspx.cs" Inherits="HotelWebSQL.Users.Klient.KlientRezerwuj" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body  style="background:lightgrey">
<form id="form1" runat="server">
    <div>
        <div style="width:400px">
            <center>Rezerwacja</center>
            <div style="float:left">
                <center>Od<br /></center>
            <asp:Calendar ID="KalendarzOd" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            </div>
            <div style ="width: 200px; float:left">
                <center>Do</center>
            <asp:Calendar ID="KalendarzDo" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <WeekendDayStyle BackColor="#FFFFCC" />
            </asp:Calendar>
            </div>
        <center>
            Pokoj:<asp:DropDownList ID="PokojIluOsobowy" runat="server" AutoPostBack="True" /> osobowy
            <br />
            <asp:Button ID="Szukaj" runat="server" Text="Szukaj" OnClick="Szuakj_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
    
        </center>
       
    </div>
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
    <div style="width: 400px;">
        <center>
        <asp:Button ID="Zatwierdz" runat="server" Text="Zatwierdz" OnClick="Zatwierdz_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Anuluj" runat="server" Text="Anuluj" OnClick="Anuluj_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
        </center>
    </div>
</form>
</body>
</html>
