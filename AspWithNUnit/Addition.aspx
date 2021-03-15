<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Addition.aspx.cs" Inherits="AspWithNUnit.Addition" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asp.Net Example (Using DevOps)</title>

     <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
    <link rel="stylesheet" href="~/css/site.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"
          asp-fallback-href="~/lib/bootstrap/dist/css/bootstrap.min.css"
          asp-fallback-test-class="sr-only" asp-fallback-test-property="position" asp-fallback-test-value="absolute" />
    <link rel="stylesheet" href="~/css/site.min.css" asp-append-version="true" />
</head>
<body>
      <div class="container body-content" style="vertical-align:central">
     <h2 style="background-color:darkgray; color:white; padding:inherit"   >Asp.Net Example (Using DevOps)</h2>
<h2>Add Test</h2>

<h4>Addition of 2 Numbers</h4>
    <hr />
<div class="row">
    <div class="col-md-4" style="vertical-align:central">

    <form id="form1" runat="server">
       
          <div  class="text-danger"></div>
            <div class="form-group">
                <label  class="control-label">Num1</label>
                <asp:TextBox ID="txtnum1" runat="server" class="form-control"></asp:TextBox>
            </div>
        <div class="form-group">
                  <label  class="control-label">Num1</label>
                    <asp:TextBox ID="txtNum2" runat="server" class="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                 <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-default" OnClick="btnAdd_Click" />
            </div>
            <div class="form-group">
                 <label  class="control-label">Addition</label>
                 <asp:Label ID="lblAdd" runat="server" ></asp:Label>
                        <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        <div>
            
        </div>
        
    </form>
        </div>
    </div>
           <hr />
        <footer>
            <p>&copy; 2021 - Company Name Ltd</p>
        </footer>


    </div>


</body>
</html>
