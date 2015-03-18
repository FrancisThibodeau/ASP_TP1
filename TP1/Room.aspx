<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="TP1.ListerUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .gridview {
            border-collapse: collapse;
        }

        .gridview th {
            border: 1px solid black;
            font-family: Arial;
            font-size: 12px;
            padding: 5px;
        }

        .gridview th {
            border: 1px solid black;
            font-family: Arial;
            font-size: 12px;
            padding: 5px;
        }

        table tr:first-child td {
            color: black;
            background-color: lightgray;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main_Content" runat="server">
    <asp:Panel ID="PN_GridView" CssClass="gridview" runat="server"></asp:Panel>
</asp:Content>
