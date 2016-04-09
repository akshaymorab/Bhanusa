<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuperAdmin.aspx.cs" Inherits="Bhanusa.SuperAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bhanusaa</title>
    <script src="js/jspdf.js"></script>
    <link rel="stylesheet" href="css/superadmin.css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="//cdn.rawgit.com/MrRio/jsPDF/master/dist/jspdf.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf-autotable/2.0.16/jspdf.plugin.autotable.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/list.js/1.2.0/list.min.js"></script>
    <script type="text/javascript" src="//cdn.rawgit.com/niklasvh/html2canvas/0.5.0-alpha2/dist/html2canvas.min.js"></script>
    <script src="js/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="js/calendar-en.min.js" type="text/javascript"></script>
    <link href="css/calendar-blue.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/DCTable.js"></script>
    <script type="text/javascript" src="../js/StockTable.js"></script>
    <script type="text/javascript" src="../js/POTable.js"></script>
    <script type="text/javascript" src="../js/REPTable.js"></script>
    <script type="text/javascript" src="../js/tableToExcel.js"></script>


    <!--Employee Page-->
    <script type="text/javascript">
        var queryString = new Array();
        $(function () {
            if (queryString.length == 0) {
                if (window.location.search.split('?').length > 1) {
                    var params = window.location.search.split('?')[1].split('&');
                    for (var i = 0; i < params.length; i++) {
                        var key = params[i].split('=')[0];
                        var value = decodeURIComponent(params[i].split('=')[1]);
                        queryString[key] = value;
                    }
                }
            }
            if (queryString["EmpName"] != null) {
                var data = "Welcome, " + queryString["EmpName"];
                $("#lblWelcome").text(data);
            }
        });
    </script>

    <!--Tables Home Page-->
    <script>
        //UserTable
        function usertable() {
            $("#tblUser tbody").empty();
            closediv();
            var fin = "hi";
            try {
                $.ajax({
                    type: "POST",
                    url: "Get/GetUserDetails.ashx",
                    cache: false,
                    data: fin,
                    dataType: "json",
                    success: getUserSuccess,
                    error: function getFail(cpnmsg) {
                        alert(cpnmsg.Response);
                    }
                });
            } catch (e) {
            }
            function getUserSuccess(users) {
                var userrec = users.Response;
                userrec = userrec.split(';');
                for (var i = 0; i <= userrec.length - 1; i++) {
                    rowCount = i + 1;
                    var itemrec = userrec[i].split('*');
                    $("#tblUser tbody").append(
                        '<tr >' +
                        '<td class="btd" style="display:none"><div class="tabstyle" style="display:none">' + rowCount + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[0] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[1] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[2] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[3] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[4] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[5] + '</div></td>' +
                        '<td><div class="tabstyle1">' + itemrec[6] + '</div></td>' +
                        '<td><div class="tabstyle1">Edit</div></td>' +
                        '</tr>' + '<hr/>');
                }
                //$(".tabstyle1").unbind('click').bind('click', editValue);
                //$(".cross").unbind('click').bind('click', Delete);
            }

            $("#divUser").show();
            $("#btnAddUser").show();
            document.getElementById("btnUser").disabled = true;
        }
    </script>

    <!--Tables DC Page-->
    <script>
        var selval = ""
        //Company Details
        function autoCompany() {
            try {
                $.ajax({
                    type: "POST",
                    url: "Get/GetCompDetails.ashx",
                    cache: false,
                    data: selval,
                    dataType: "json",
                    success: getDCComp,
                    error: function getFail(cpnmsg) {
                        alert(cpnmsg.Response);
                    }
                });
            } catch (e) {
            }
            function getDCComp(compres) {
                if (compres.length != 0) {
                    var compDetails = [];
                    var compdet;
                    var compName = [];

                    for (var i = 0; i <= compres.length - 1; i++) {
                        compDetails.push(compres[i]);
                        compdet = compres[i].split('%');
                        compName.push(compdet[0]);
                    }
                    var x, resdet;
                    $('#txtCompany').autocomplete({
                        source: compName,
                        select: function (event, ui) {
                            var strdet = ui.item.value;
                            x = compName.indexOf(strdet);
                            resdet = compDetails[x];
                            resdet = resdet.split('%');
                            $('#txtCompAddress').val(resdet[2]);
                            $('#txtLocation').val(resdet[4]);
                        }
                    });
                    $('#txtPoComp').autocomplete({ source: compName });
                }
            }
        }
        //Clear Items
        function btnClearItem() {
            $('#tblDCItem tbody tr').remove();
            var srno = $('#divSrNo').val();
            $('#tblDCDetails tbody tr td:nth-child(5)').eq(srno).html('<div class="divrwitm" style="display:none"></div>');
            $('#tblDCDetails tbody tr td:nth-child(4)').eq(srno).html('<div class=qty>Add</div>');
            $(".qty").unbind('click').bind('click', addQty);
            //$('#dicDCItemChange').hide();
        }

    </script>

    <!--Tables User Page-->
    <script>

        //ADD
        function adduser() {
            closediv();
            $("#form").show();
            $('#formUser').show();
        }

    </script>

    <!--Common Functions-->
    <script>

        function frmclose(id) {
            if (id == "btnclose") {
                closediv();
                $('#divDC').show();
            }
            if (id == "btnLoginDiv") {
                closediv();
                $('#divUser').show();
            }

            if (id == "btnDCClose") {
                closediv();
                $('#tblDCDetails').show();
            }
            if (id == "btnStkClose") {
                closediv();
                $('#divStock').show();
            }
        }

        function userLogout() {
            var url = "Index.aspx";
            window.location.href = url;
        }

        //close all div
        function closediv() {
            $('#divRpot').hide();
            $("#divDC").hide();
            $("#divStock").hide();
            $("#divUser").hide();
            $('#divPO').hide();
            $("#form").hide();
            $('#formUser').hide();
            $('#formDC').hide();
            $('#formPO').hide();
            $('#stkItem').hide();
            $('#formStock').hide();
            $('#btnAddDCItem').hide();
            $('#formCompany').hide();
        }
        //close all labels
        function closelab() {
            $('#label1').hide();
            $('#label2').hide();
            $('#label3').hide();
            $('#label4').hide();
        }
        //Show all labels
        function showLab() {
            $('#label1').show();
            $('#label2').show();
            $('#label3').show();
            $('#label4').show();
        }
        //enable buttons
        function btnenable() {
            document.getElementById("btnUser").disabled = false;
            document.getElementById("btnDC").disabled = false;
            document.getElementById("btnStock").disabled = false;
            document.getElementById("btnReport").disabled = false;
        }

        //clear text fields
        function cleartext() {
            $('#txtEmpid').val("");
            $('#txtEmpName').val("");
            $('#txtEmpRole').val("");
            $('#txtEmpPass').val("");
            $('#txtMobile').val("");
            $('#txtUserAddress').val("");
            $('#txtDCNo').val("");
            $('#txtCompany').val("");
            $('#txtCompAddress').val("");
            $('#txtLocation').val("");
            $('#txtRemarks').val("");
            $('#txtSrNo').val("");
            $('#txtBrand').val("");
            $('#txtStkMdln').val("");
            $('#txtStkRemark').val("");
        }
    </script>

    <!--Add Details in DBTable-->
    <script>

        //DC
        function addMoreRows(id) {
            if (id == "btnSubmit") {
                var rowCount = $('#tblUser tbody tr').length;
                rowCount++;
                var strUser = $('#txtEmpid').val() + '*' + $('#txtEmpName').val() + '*' + $('#txtEmpRole').val() + '*' + $('#txtEmpPass').val() + '*' + $('#txtMobile').val() + '*' + $('#txtEmail').val() + '*' + $('#txtUserAddress').val();
                try {
                    $.ajax({
                        type: "POST",
                        url: "Post/PostUserDetail.ashx",
                        cache: false,
                        data: strUser,
                        dataType: "json",
                        success: postUserSuccess,
                        error: function getFail(cpnmsg) {
                            alert(cpnmsg.Response);
                        }
                    });
                } catch (e) {
                }
                function postUserSuccess(pstres) {
                    if (pstres.length != 0) {
                        $('#lblAddUser').show();
                        $('#lblAddUser').text(pstres.Response);
                    }
                }
            }
            if (id == "btnAddDCItem" || id == "btnUpdtDCItem") {

                var tbllength = $('#tblDCDetails tbody tr').length;
                var selPar, sts, conf, sernu, qty;
                $('#tblDCDetails  tbody  tr td:nth-child(2)').eq(tbllength - 1).each(function () {
                    selPar = $(this).find('select option:selected').val();
                });
                $('#tblDCDetails  tbody  tr td:nth-child(7)').eq(tbllength - 1).each(function () {
                    sts = $(this).find('select option:selected').val();
                });
                conf = $('#tblDCDetails tbody tr td:nth-child(3) input').eq(tbllength - 1).val();
                sernu = $('#tblDCDetails tbody tr td:nth-child(4) input').eq(tbllength - 1).val();
                qty = $('#tblDCDetails tbody tr td:nth-child(6) input').eq(tbllength - 1).val();
                if (selPar != "Select" && sts != "0" && conf != "" && sernu != "" && qty != "" && $('#txtCompany').val() != "") {
                    var rwItem, rwConfig, rwQty, rwMdlno, rwstatus, rwrtcode, rwsrno;
                    var welcome = $("#lblWelcome").text();
                    var txtwel = welcome.split(', ');
                    var strDC = $('#txtDCNo').val() + '>' + $('#txtCompany').val() + '>' + $('#txtCompAddress').val() + '>' + $('#txtLocation').val() + '>' + $('#txtRemarks').val() + '>' + txtwel[1];
                    for (var i = 0; i <= tbllength - 1; i++) {
                        if (i == 0) {
                            rwItem = $('#tblDCDetails tbody tr td:nth-child(10)').eq(i - 1).text();
                            rwMdlno = $('#tblDCDetails tbody tr td:nth-child(8)').eq(i - 1).text();
                            rwConfig = $('#tblDCDetails tbody tr td:nth-child(3) input').eq(i - 1).val();
                            rwQty = $('#tblDCDetails tbody tr td:nth-child(5) input').eq(i - 1).val();
                            rwstatus = $('#tblDCDetails tbody tr td:nth-child(11)').eq(i - 1).text();
                            rwrtcode = $('#tblDCDetails tbody tr td:nth-child(9)').eq(i - 1).text();
                        }
                        else {
                            rwItem = rwItem + '%' + $('#tblDCDetails tbody tr td:nth-child(10)').eq(i - 1).text();
                            rwMdlno = rwMdlno + '%' + $('#tblDCDetails tbody tr td:nth-child(8)').eq(i - 1).text();
                            rwConfig = rwConfig + '%' + $('#tblDCDetails tbody tr td:nth-child(3) input').eq(i - 1).val();
                            rwQty = rwQty + '%' + $('#tblDCDetails tbody tr td:nth-child(5) input').eq(i - 1).val();
                            rwstatus = rwstatus + '%' + $('#tblDCDetails tbody tr td:nth-child(11)').eq(i - 1).text();
                            rwrtcode = rwrtcode + '%' + $('#tblDCDetails tbody tr td:nth-child(9)').eq(i - 1).text();
                        }
                    }
                    strDC = strDC + '>' + rwItem + '>' + rwMdlno + '>' + rwConfig + '>' + rwQty + '>' + rwstatus + '>' + rwrtcode + '>' + id + '>' + $('#txtDate').val();
                    try {
                        $.ajax({
                            type: "POST",
                            url: "Post/PostDCDetail.ashx",
                            cache: false,
                            data: strDC,
                            dataType: "json",
                            success: postDCSuccess,
                            error: function getFail(cpnmsg) {
                                alert(cpnmsg.Response);
                            }
                        });
                    } catch (e) {
                    }
                    function postDCSuccess(pstres) {
                        if (pstres.length != 0) {
                            closediv();

                            alert(pstres.Response)

                        }
                    }
                }
                else { alert("Please fill in all details"); }
            }
        }
        //Add stock details on DB
        function addstkitem(id) {
            var jsonStkDetail, status, dte, type;
            var stock = $('#lblParticular').text();
            var mdno = $('#txtSrNo').val();
            var brnd = $('#txtBrand').val();
            var mdln = $('#txtStkMdln').val();
            var rmks = $('#txtStkRemark').val();
            var rentCode = $('#lblRentCode').text();
            if (id == "btnStkAdd") {
                type = $('#ddlSelect option:selected').text();
                dte = document.getElementById('<%=txtPurchaseDate.ClientID%>').value;
                status = $('#ddlStatus option:selected').val();

            }
            else {
                type = $('#ddlSelect option:selected').text();
                dte = document.getElementById('<%=txtPurchaseDate.ClientID%>').value;
                status = $('#ddlStatus option:selected').val();
            }
            jsonStkDetail = stock + '%' + mdno + '%' + type + '%' + brnd + '%' + dte + '%' + mdln + '%' + rmks + '%' + status + '%' + rentCode + '%' + id;

            try {
                $.ajax({
                    type: "POST",
                    url: "Post/PostStockDetail.ashx",
                    cache: false,
                    data: jsonStkDetail,
                    dataType: "json",
                    success: postStkSuccess,
                    error: function getFail(cpnmsg) {
                        alert(cpnmsg.Response);
                    }
                });
            }
            catch (e) { alert(e.message); }
            function postStkSuccess(pstkres) {
                if (pstkres.length != 0) {
                    closediv();
                    alert(pstkres.Response)
                }
            }
            cleartext();
        }

    </script>

    <!--Generate PDF-->
    <script>
        function genPdf() {
            doc = new jsPDF({
                unit: 'px',
                format: 'a4'
            });
            var currentDate = new Date();
            doc.setFontSize(10);
            doc.text("Bhanusaa", 200, 50);
            doc.text("DELIVERY CHALLAN", 180, 70);
            doc.text("Date:", 300, 90);
            doc.text($('#txtDate').val(), 340, 90);
            doc.text("DC Number:", 40, 90);
            doc.text($('#txtDCNo').val(), 90, 90);
            doc.text("Company:", 40, 110);
            doc.text($('#txtCompany').val(), 80, 110);
            doc.text("Address:", 40, 130);
            doc.text($('#txtCompAddress').val(), 80, 130);
            doc.text("Location:", 300, 110);
            doc.text($('#txtLocation').val(), 350, 110);
            var columns = [{ title: "SNo", dataKey: "id" },
                { title: "Particular", dataKey: "parti" }, { title: "Configuration", dataKey: "config" }, { title: "Serial Number", dataKey: "sno" },
                { title: "Model Number", dataKey: "mno" }, { title: "Quantity", dataKey: "qty" }];
            doc.autoTable(columns, getData(), { startY: 150 });
            doc.text("Remarks:", 60, 500);
            doc.text($('#txtRemarks').val(), 90, 500);
            var dc = "DC#" + $('#txtDCNo').val() + ".pdf";
            //doc.addImage(img, 'JPEG', 40, 80);
            doc.save(dc);
        }

        function getData() {
            var len = $('#tblDCDetails tbody tr').length;
            var sts;
            var data = [];
            var k = 0;
            for (var i = 0; i <= len - 1; i++) {
                $('#tblDCDetails  tbody  tr td:nth-child(7)').eq(i - 1).each(function () {
                    sts = $(this).find('select option:selected').val();
                });
                if (sts != "3") {

                    data.push({
                        id: k + 1, parti: $('#tblDCDetails tbody tr td:nth-child(10)').eq(i - 1).text(),
                        config: $('#tblDCDetails tbody tr td:nth-child(3) input').eq(i - 1).val(),
                        sno: $('#tblDCDetails tbody tr td:nth-child(8)').eq(i - 1).text(),
                        mno: $('#tblDCDetails tbody tr td:nth-child(6) input').eq(i - 1).val(),
                        qty: $('#tblDCDetails tbody tr td:nth-child(5) input').eq(i - 1).val()
                    })
                    k++;
                }
            }
            return data;
        }
    </script>

    <!--Date-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtPurchaseDate.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m, %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                //displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>

</head>

<body>
    <!-- Header -->
    <div id="header" class="header">
        <div id="lblLogo">Bhanusaa</div>
        <div id="lblWelcome" style="float: left; position: relative; top: -50px;"></div>
        <input type="button" class="btn" id="btnLogout" value="Logout" onclick="userLogout();" />
    </div>

    <!-- Menu -->
    <div id="menu" class="menu">
        <div id="btnDC" class="btnMenu" onclick="dctable();">DC</div>
        <div id="btnStock" class="btnMenu" onclick="stocktable();">Stock</div>
        <div id="btnUser" class="btnMenu" onclick="usertable();">User</div>
        <div id="btnPO" class="btnMenu" onclick="potable();">PO</div>
        <div id="btnReport" class="btnMenu" onclick="repotable();">Reports</div>

    </div>
    <!--Stock Menu-->
    <div id="stkItem" class="btnStkMenu">
        <div id="btnDesktop" class="stock" onclick="stocks(this.id);">Desktop</div>
        <div id="btnLaptop" class="stock" onclick="stocks(this.id);">Laptop</div>
        <div id="btnServer" class="stock" onclick="stocks(this.id);">Server</div>
        <div id="btnPrinter" class="stock" onclick="stocks(this.id);">Printer</div>
        <div id="btnProjector" class="stock" onclick="stocks(this.id);">Projector</div>
        <div id="btnMobile" class="stock" onclick="stocks(this.id);">Mobile</div>
        <div id="btnTablet" class="stock" onclick="stocks(this.id);">Tablet</div>
        <div id="btnAccessories" class="stock" onclick="stocks(this.id);">Accessories</div>
        <div id="btnOthers" class="stock" onclick="stocks(this.id);">Others</div>
        <div id="btnCompany" class="stock" onclick="stocks(this.id);">Company</div>
    </div>

    <!-- Table -->
    <div id="table" class="table">

        <!--DC-->
        <div id="divDC" class="table">
            <input type="button" id="btnAddDC" value="Add" class="btn" onclick="adddc();" />
            <table id="tblDC" class="tblDC">
                <thead>
                    <tr>
                        <th style="display: none" class="chkouttblhead">SNo</th>
                        <th class="chkouttblhead">DCNo</th>
                        <th class="chkouttblhead">Company</th>
                        <th class="chkouttblhead">Address</th>
                        <th class="chkouttblhead">Location</th>
                        <th class="chkouttblhead">Date</th>
                        <th class="chkouttblhead">Remarks</th>
                        <th class="chkouttblhead">Updated By</th>
                        <th class="chkouttblhead">Updated Time</th>
                        <th class="chkouttblhead">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>

        <!--Stock-->
        <div id="divStock" class="table">
            <input type="button" id="btnAddStock" value="Add" style="display: none" class="btn" onclick="stockclick();" />
            <table id="tblStock" class="tblStock">
                <thead>
                    <tr>
                        <th style="display: none" class="chkouttblhead">SNo</th>
                        <th class="chkouttblhead">Serial Number</th>
                        <th class="chkouttblhead">Particular</th>
                        <th class="chkouttblhead">Type</th>
                        <th class="chkouttblhead">Brand</th>
                        <th class="chkouttblhead">PurchaseDate</th>
                        <th class="chkouttblhead">Model Number</th>
                        <th class="chkouttblhead">Remarks</th>
                        <th class="chkouttblhead">Status</th>
                        <th class="chkouttblhead">Rent Code</th>
                        <th class="chkouttblhead">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <input type="button" id="btnAddComp" value="Add" style="display: none" class="btn" onclick="compclick();" />
            <table id="tblCompany" class="tblStock">
                <thead>
                    <tr>
                        <th style="display: none" class="chkouttblhead">SNo</th>
                        <th class="chkouttblhead">Company id</th>
                        <th class="chkouttblhead">Name</th>
                        <th class="chkouttblhead">Address</th>
                        <th class="chkouttblhead">Phone</th>
                        <th class="chkouttblhead">Location</th>
                        <th class="chkouttblhead">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>

        <!--User-->
        <div id="divUser" class="table">
            <input type="button" id="btnAddUser" value="Add" class="btn" onclick="adduser();" />
            <label id="lblAddUser" visible="false" style="width: 100%" />
            <table id="tblUser" class="tblUser">
                <thead>
                    <tr>
                        <th style="display: none" class="chkouttblhead">SNo</th>
                        <th class="chkouttblhead">id</th>
                        <th class="chkouttblhead">Name</th>
                        <th class="chkouttblhead">Role</th>
                        <th class="chkouttblhead">Password</th>
                        <th class="chkouttblhead">Mobile</th>
                        <th class="chkouttblhead">Email</th>
                        <th class="chkouttblhead">Address</th>
                        <th class="chkouttblhead">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>

        <!--PO-->
        <div id="divPO" class="table">
            <input type="button" id="btnAddPO" value="Add" class="btn" onclick="addPO();" />
            <table id="tblPO" class="tblPO">
                <thead>
                    <tr>
                        <th style="display: none" class="chkouttblhead">SNo</th>
                        <th class="chkouttblhead">PO Number</th>
                        <th class="chkouttblhead">PO Company</th>
                        <th class="chkouttblhead">Start Date</th>
                        <th class="chkouttblhead">End Date</th>
                        <th class="chkouttblhead">Renew Date</th>
                        <th class="chkouttblhead">Edit</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>


    </div>
    <!--Report-->
    <div id="divRpot" class="table" style="margin: 5% 5px 0 20px; display: none;">
        <input type="button" id="btn1" value="DC" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn4" value="Desktop" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn5" value="Laptop" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn6" value="Server" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn7" value="Printer" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn8" value="Projector" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn9" value="Mobile" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn10" value="Tablet" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn11" value="Accessories" class="btn" onclick="FUC(this.id);" />
        <input type="button" id="btn12" value="Others" class="btn" onclick="FUC(this.id);" /><br />
        <input type="button" id="btn13" value="History" class="btn" onclick="FUC(this.id);" /><br />
        <div id="divSearch" class="table" style="display: none">
            <label id="label1">Count:</label><label id="label2" style="margin: 0.4%"></label><label id="strRent">Rent</label><label id="label3" style="margin: 0.4%"></label><label id="strInStock">InStock:</label><label id="label4" style="margin: 0.4%"></label>
            <label id="strColmn">Select Column:</label>
            <select id="ddlSearch" style="display: none">
                <option value="0">--Select--</option>
                <option value="1">SerialNumber</option>
                <option value="2">ModelNumber</option>
                <option value="3">Company</option>
                <option value="4">DCNo</option>
            </select>
            <select id="ddlDC" style="display: none">
                <option value="0">--Select--</option>
                <option value="1">DCNo</option>
                <option value="2">Company</option>
                <option value="3">Date</option>
            </select>

            <input type="text" id="txt1" autocomplete="on" onclick="fucSearch();" />
            <input type="button" value="Search" id="btnSearch" onclick="btnSearch()" />
            <input type="button" value="Export" id="btnETE" onclick="btnExcel()" />
        </div>

        <div id="divRepoStock">
            <table id="tblRepoStock" class="tblPO" style="display: none">
                <thead>
                    <tr>
                        <th class="chkouttblhead">Serial Number</th>
                        <th class="chkouttblhead">Model Number</th>
                        <th class="chkouttblhead">StockStatus</th>
                        <th class="chkouttblhead">DC No</th>
                        <th class="chkouttblhead">Company</th>
                        <th class="chkouttblhead">Configuration</th>
                        <th class="chkouttblhead">DCStatus</th>
                        <th class="chkouttblhead">Quantity</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
        <div id="divRepo">
            <table id="tblRepo" class="tblPO" style="display: none">
                <thead>
                    <tr>
                        <th class="chkouttblhead">DC NO.</th>
                        <th class="chkouttblhead">COMPANY</th>
                        <%--<th class="chkouttblhead">PO NO.</th>--%>
                        <th class="chkouttblhead">DATE</th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <!--End Report-->
    <!--Rent History-->
    <div id="rentHistory">
        <table id="tblRentHistory" style="width: 100%;display: none">
            <thead>
                <tr>
                    <th class="chkouttblhead">DCNo</th>
                    <th class="chkouttblhead">Company</th>
                    <th class="chkouttblhead">SerialNumber</th>
                    <th class="chkouttblhead">ModelNumber</th>
                    <th class="chkouttblhead">Configurtion</th>
                    <th class="chkouttblhead">Quantity</th>
                    <th class="chkouttblhead">StartDate</th>
                    <th class="chkouttblhead">EndDate</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <!--End History-->
    <!--Form-->
    <div id="form" class="form">
        <!--DC-->
        <div id="formDC" class="form" style="display: none">
            <form id="frmDC" method="post">
                <div class="btnclose" id="btnclose" onclick="frmclose(this.id);">X</div>
                <div id="divFormDC">
                    <div class="fieldgroup signinOptionContainer">
                        <label id="lblDCNo" class="dclabel">DC Number:</label>
                        <input type="text" class="dctext" name="dcno" autocomplete="off" required="required" id="txtDCNo" />
                    </div>
                    <div style="float: right; margin: 15px 40px 0px 0px">
                        <label id="Label15" class="dclabel">Date:</label>
                        <input type="text" class="dctext" name="dcno" autocomplete="off" required="required" id="txtDate" />
                    </div>
                    <div style="float: right; margin: 15px 40px 0px 0px">
                        <label id="Label21" class="dclabel">PO Number:</label>
                        <input type="text" class="dctext" name="dcno" autocomplete="off" required="required" id="txtPoNumb" onclick="autoPO();" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="lblCompany" class="dclabel">Company:  &nbsp &nbsp</label>
                        <input type="text" class="dctext" name="company" autocomplete="off" required="required" id="txtCompany" onclick="autoCompany();" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="lblAddress" class="dclabel">Address:&nbsp &nbsp &nbsp&nbsp</label>
                        <input type="text" class="dctext" name="address" autocomplete="off" required="required" id="txtCompAddress" />
                        <!--<textarea style="height:40px;" required="required" id="txtCompAddress" />-->
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="lblLocation" class="dclabel">Location: &nbsp &nbsp &nbsp</label>
                        <input type="text" class="dctext" name="address" autocomplete="off" required="required" id="txtLocation" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="lblRemarks" class="dclabel">Remarks:&nbsp &nbsp &nbsp</label>
                        <input type="text" class="dctext" name="address" autocomplete="off" required="required" id="txtRemarks" />
                    </div>

                    <table id="tblDCDetails" style="width: 100%">
                        <thead>
                            <tr>
                                <th class="chkouttblhead">SNo</th>
                                <th class="chkouttblhead">Particular</th>
                                <th class="chkouttblhead">Configurations</th>
                                <th class="chkouttblhead">Serial Number</th>
                                <th class="chkouttblhead">Quantity</th>
                                <th class="chkouttblhead">Model Number</th>
                                <th class="chkouttblhead">Status</th>
                                <th>Delete</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div id="ignorePDF">
                    <input type="button" style="position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnAddItem" onclick="adddcitem();" value="Add" />
                    <input type="button" style="position: relative; margin: 10px auto; top: 10px; left: 33%; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px; display: none" id="btnAddDCItem" onclick="addMoreRows(this.id);" value="Create" />
                    <input type="button" style="position: relative; margin: 10px auto; top: 10px; left: 33%; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px; display: none" id="btnUpdtDCItem" onclick="addMoreRows(this.id);" value="Update" />
                    <input type="button" style="position: relative; margin: 10px auto; top: 10px; left: 33%; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px; display: none" id="btnDCPdf" onclick="genPdf();" value="Generate DC" />
                </div>
            </form>
        </div>

        <!--Stock-->
        <div id="formStock" class="form" style="display: none;">
            <form id="frmStock" method="post" runat="server">
                <div class="btnclose" id="btnStkClose" onclick="frmclose(this.id);"></div>
                <div id="divfrmStock">
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label1" class="dclabel">Serial Number:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtSrNo" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label2" class="dclabel">Particular:</label>
                        <label id="lblParticular" class="dclabel"></label>
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label3" class="dclabel">Type:</label>
                        <select id="ddlSelect">
                            <option value="0">Brand</option>
                            <option value="1">Assembled</option>
                        </select>
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label4" class="dclabel">Brand:</label>
                        <input type="text" class="dctext" autocomplete="off" required="required" id="txtBrand" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label5" class="dclabel">Purchase Date:</label>
                        <asp:TextBox ID="txtPurchaseDate" runat="server" />
                        <img src="images/Arrow.png" style="height: 10px; width: 10px;" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label14" class="dclabel">Model Number:</label>
                        <input type="text" class="dctext" autocomplete="off" required="required" id="txtStkMdln" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label6" class="dclabel">Remarks:</label>
                        <input type="text" class="dctext" autocomplete="off" required="required" id="txtStkRemark" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label7" class="dclabel">Status:</label>
                        <select id="ddlStatus">
                            <option value="0">In Stock</option>
                            <option value="1">Rent</option>
                        </select>
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label8" class="dclabel">Rent Code:</label>
                        <label id="lblRentCode" class="dclabel"></label>
                    </div>
                </div>
                <input type="button" style="display: none; position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnStkAdd" onclick="addstkitem(this.id);" value="Add" />
                <input type="button" style="display: none; position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnStkUpdt" onclick="addstkitem(this.id);" value="Update" />
            </form>
        </div>

        <!--Company-->
        <div id="formCompany" class="form" style="display: none;">
            <form id="frmComp" method="post">
                <div class="btnclose" id="btnFrmComp" onclick="frmclose(this.id);"></div>
                <div id="div3">
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label9" class="dclabel">Company Id:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtCompId" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label10" class="dclabel">Company Name:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtCompName" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label11" class="dclabel">Address:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtAddress" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label13" class="dclabel">Phone:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtPhone" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label12" class="dclabel">Location:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtCompLoc" />
                    </div>
                </div>
                <input type="button" style="display: none; position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnCompAdd" onclick="addcompdet(this.id);" value="Add" />
                <input type="button" style="display: none; position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnCompUpdt" onclick="addcompdet(this.id);" value="Update" />
            </form>
        </div>

        <!--User-->
        <div id="formUser" class="form">
            <div class="shadowbox" style="opacity: 0.7;">
            </div>
            <div class="shadowbox logindiv">
                <div class="btnclose" id="btnLoginDiv" onclick="frmclose(this.id);"></div>
                <b class="signin">Employee Login</b><br />
                < class="signina">We need your Details</>
                <br />
                <form id="here" autocomplete="on" method="post">
                    <div id="form-content">
                        <div class="fieldgroup signinOptionContainer">
                            <input type="text" name="empId" placeholder="Employee Id  :" maxlength="4" autocomplete="off" required="required" class="signinOptionTxt" id="txtEmpId" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="text" name="name" placeholder="Employee Name  :" class="signinOptionTxt" autocomplete="off" required="required" id="txtEmpName" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="text" name="role" placeholder="Employee Role  :" class="signinOptionTxt" autocomplete="off" required="required" id="txtEmpRole" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="password" name="password" placeholder="enter password with 4 or more characters " class="signinOptionTxt" id="txtEmpPass" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="text" name="mobile" placeholder="Mobile  :" maxlength="10" autocomplete="off" required="required" class="signinOptionTxt" id="txtMobile" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="email" name="email" placeholder="E-mail  :" autocomplete="off" required="required" class="signinOptionTxt" id="txtEmail" />
                        </div>
                        <div class="fieldgroup signinOptionContainer">
                            <input type="text" name="address" placeholder="Address :" autocomplete="off" required="required" class="signinOptionTxt" id="txtUserAddress" />
                        </div>
                        <input type="submit" style="position: relative; margin: 10px auto; top: 10px; left: 33%; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnSubmit" onclick="addMoreRows(this.id);" value="Create" />
                    </div>
                </form>
            </div>
        </div>

        <!--PO-->
        <div id="formPO" class="form" style="display: none;">
            <form id="frmPO" method="post">
                <div class="btnclose" id="Div2" onclick="frmclose(this.id);"></div>
                <div id="divfrmPO">
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label16" class="dclabel">PO Number:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtPoNo" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label17" class="dclabel">PO Company:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtPoComp" onclick="autoCompany();" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label18" class="dclabel">Start Date:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtStartDate" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label19" class="dclabel">End Date:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtEndDate" />
                    </div>
                    <div class="fieldgroup signinOptionContainer">
                        <label id="Label20" class="dclabel">Renew Date:</label>
                        <input type="text" class="dctext" name="dcno" required="required" id="txtRenewDate" />
                    </div>
                </div>
                <input type="button" style="position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="btnAddPo" onclick="addpodet();" value="Add" />
                <input type="button" style="display: none; position: relative; margin: 10px auto; top: 10px; width: 150px; background-color: #0eb89b; border: 1px; border-radius: 4px; cursor: pointer; padding: 15px 30px 15px 30px" id="Button2" onclick="addpodet();" value="Update" />
            </form>
        </div>
    </div>

</body>
</html>
