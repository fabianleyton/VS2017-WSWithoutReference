<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestSoapWebService.aspx.cs" Inherits="testSoapService.TestSoapWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:TextBox ID="lblResult" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnRequest" runat="server" Text="Button" OnClick="btnRequest_Click" />
            </div>
        </div>
    </form>
</body>
</html>
