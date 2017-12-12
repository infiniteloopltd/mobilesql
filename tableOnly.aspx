<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tableOnly.aspx.cs" Inherits="MobileSQL.tableOnly" %>
<html>
<asp:DataGrid id="dgData" Width="100%" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
    <EditItemStyle BackColor="#999999" />
    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:DataGrid>

<asp:label id="lblError" runat="server"  BorderColor="Red"></asp:label>

</html>
