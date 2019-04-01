<%@ Page Title="" Language="C#" MasterPageFile="~/gcweb-theme/MasterPage/GCWebMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AIA.Default" %>
<%@ Register Src="~/UserControls/AIAForm.ascx" TagPrefix="uc1" TagName="AIAForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:AIAForm ID="AIAForm" runat="server"  />

</asp:Content>
