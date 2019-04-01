<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AIAForm.ascx.cs" Inherits="AIA.UserControls.AIAForm" %>

<h1><asp:Label ID="lblFormTitle" runat="server"></asp:Label></h1>
<asp:Label ID="lblLastDateModified" CssClass="text-muted sm" runat="server"></asp:Label>

<div class="mrgn-tp-lg">
    <asp:UpdatePanel ID="upForm" runat="server">
        <ContentTemplate>

            <asp:Panel ID="pnlForm" CssClass="panel panel-default" runat="server">
                <asp:Panel ID="pnlBody" CssClass="panel-body" runat="server"></asp:Panel>
                <asp:Panel ID="pnlFooter" CssClass="panel-footer text-center" runat="server"></asp:Panel>
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
</div>
