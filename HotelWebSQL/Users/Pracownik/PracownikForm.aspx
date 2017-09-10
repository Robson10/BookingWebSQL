<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PracownikForm.aspx.cs" Inherits="HotelWebSQL.Users.Pracownik.PracownikForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background:lightgrey">
<form id="form1" runat="server">
<div>
    <asp:Button ID="WylogujBT" runat="server" Text="Wyloguj" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px;float:right" OnClick="WylogujBT_Click"/>
    <asp:Button ID="PrzegladajDaneKlientowBT" runat="server" Text="Przeglądaj dane Klientów" OnClick="PrzegladajDaneKlientowBT_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
    &nbsp;<asp:Button ID="PrzegladajRezerwacjeBT" runat="server" Text="Przegladaj Rezerwacje" OnClick="PrzegladajRezerwacjeBT_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
    &nbsp;<asp:Button ID="PrzegladajPokojeBT" runat="server" Text="Przegladaj Pokoje" OnClick="PrzegladajPokojeBT_Click" style="border: none;font-size: 16px;color: wheat;background-color: dimgray;height:30px"/>
  </div>
<br/>
<div>
    <asp:GridView ID="GridKlienci" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="O_ID">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="O_Imie" HeaderText="Imie" SortExpression="O_Imie" />
            <asp:BoundField DataField="O_Nazwisko" HeaderText="Nazwisko" SortExpression="O_Nazwisko" />
            <asp:BoundField DataField="O_Mail" HeaderText="Mail" SortExpression="O_Mail" />
            <asp:BoundField DataField="O_Tel" HeaderText="Tel" SortExpression="O_Tel" />
            <asp:BoundField DataField="O_ID" HeaderText="O_ID" InsertVisible="False" ReadOnly="True" SortExpression="O_ID" Visible="False" />
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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Database_1ConnectionString %>" SelectCommand="SELECT [O_Imie], [O_Nazwisko], [O_Mail], [O_Tel], [O_ID] FROM [Osoba]" DeleteCommand="DELETE FROM [Osoba] WHERE [O_ID] = @O_ID" InsertCommand="INSERT INTO [Osoba] ([O_Imie], [O_Nazwisko], [O_Mail], [O_Tel]) VALUES (@O_Imie, @O_Nazwisko, @O_Mail, @O_Tel)" UpdateCommand="UPDATE [Osoba] SET [O_Imie] = @O_Imie, [O_Nazwisko] = @O_Nazwisko, [O_Mail] = @O_Mail, [O_Tel] = @O_Tel WHERE [O_ID] = @O_ID">
        <DeleteParameters>
            <asp:Parameter Name="O_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="O_Imie" Type="String" />
            <asp:Parameter Name="O_Nazwisko" Type="String" />
            <asp:Parameter Name="O_Mail" Type="String" />
            <asp:Parameter Name="O_Tel" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="O_Imie" Type="String" />
            <asp:Parameter Name="O_Nazwisko" Type="String" />
            <asp:Parameter Name="O_Mail" Type="String" />
            <asp:Parameter Name="O_Tel" Type="String" />
            <asp:Parameter Name="O_ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="RezerwacjeGrid" runat="server" Height="138px" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="RezerwacjeGrid_RowCancelingEdit" OnRowEditing="RezerwacjeGrid_RowEditing" OnRowUpdating="RezerwacjeGrid_RowUpdating">
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
    <asp:GridView ID="GridPokoje" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" Visible="False" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True" AllowSorting="True" DataKeyNames="P_ID">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowSelectButton="True" />
            <asp:BoundField DataField="P_Nr" HeaderText="P_Nr" SortExpression="P_Nr" >
            </asp:BoundField>
            <asp:BoundField DataField="P_IluOsobowy" HeaderText="P_IluOsobowy" SortExpression="P_IluOsobowy">
            </asp:BoundField>
            <asp:BoundField DataField="P_KosztDzienny" HeaderText="P_KosztDzienny" SortExpression="P_KosztDzienny">
            </asp:BoundField>
            <asp:CheckBoxField DataField="P_CzyGotowy" HeaderText="P_CzyGotowy" SortExpression="P_CzyGotowy" >
            </asp:CheckBoxField>
            <asp:BoundField DataField="P_ID" HeaderText="P_ID" InsertVisible="False" ReadOnly="True" SortExpression="P_ID" />
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
    <asp:SqlDataSource ID="SqlDataSource5" OnUpdated="SqlDataSource5_Updated" runat="server" ConnectionString="<%$ ConnectionStrings:Database_1ConnectionString %>" 
                       SelectCommand="SELECT [P_Nr], [P_IluOsobowy], [P_KosztDzienny], [P_CzyGotowy], [P_ID] FROM [Pokoj] ORDER BY [P_CzyGotowy], [P_Nr]" DeleteCommand="DELETE FROM [Pokoj] WHERE [P_ID] = @P_ID" InsertCommand="INSERT INTO [Pokoj] ([P_Nr], [P_IluOsobowy], [P_KosztDzienny], [P_CzyGotowy]) VALUES (@P_Nr, @P_IluOsobowy, @P_KosztDzienny, @P_CzyGotowy)" UpdateCommand="UPDATE [Pokoj] SET [P_Nr] = @P_Nr, [P_IluOsobowy] = @P_IluOsobowy, [P_KosztDzienny] = @P_KosztDzienny, [P_CzyGotowy] = @P_CzyGotowy WHERE [P_ID] = @P_ID">
        <DeleteParameters>
            <asp:Parameter Name="P_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="P_Nr" Type="Int32" />
            <asp:Parameter Name="P_IluOsobowy" Type="Int32" />
            <asp:Parameter Name="P_KosztDzienny" Type="Decimal" />
            <asp:Parameter Name="P_CzyGotowy" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="P_Nr" Type="Int32" />
            <asp:Parameter Name="P_IluOsobowy" Type="Int32" />
            <asp:Parameter Name="P_KosztDzienny" Type="Decimal" />
            <asp:Parameter Name="P_CzyGotowy" Type="Boolean" />
            <asp:Parameter Name="P_ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
</div>
</form>
</body>
</html>
