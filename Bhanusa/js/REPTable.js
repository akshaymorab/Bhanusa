var conString;
var id;
function repotable() {
    closediv();
    $('#divRpot').show();
    $("#btnReport").css({ "padding-bottom": "300%" });
}
function FUC(id) {

    switch (id) {
        case "btn1": conString = "tblDC";
            break;
        case "btn2": conString = "tblEmployeeDetails";
            break;
        case "btn3": conString = "tblPO";
            break;
        case "btn4": conString = "tblDesktop";
            break;
        case "btn5": conString = "tblLaptop";
            break;
        case "btn6": conString = "tblServer";
            break;
        case "btn7": conString = "tblPrinter";
            break;
        case "btn8": conString = "tblProjector";
            break;
        case "btn9": conString = "tblMobile";
            break;
        case "btn10": conString = "tblTablet";
            break;
        case "btn11": conString = "tblAccessories";
            break;
        case "btn12": conString = "tblOthers";
            break;
        case "btn13": conString = "tblRentHistory";
            break;
        default: conString = "";
            break;
    }
    if (conString == "tblDC" || conString == "tblCompany") {
        $("#divRepoStock").hide();
        $("#rentHistory").hide();
        $("#divRepo").show();
        $('#divSearch').show();
        $('#ddlSearch').hide();
        $('#ddlDC').show();
        openSearch();
        var fin = conString;
        try {
            $.ajax({
                type: "POST",
                url: "Get/GetbtnDCDetails.ashx",
                cache: false,
                data: fin,
                dataType: "json",
                success: getbtnDCSuccess,
                error: function getbtnFail(cpnmsg) {
                    alert(cpnmsg.Response);
                }
            })
        } catch (e) {
        }
        function getbtnDCSuccess(gtus) {

            var tblUserDt = gtus.Response;
            tblUserDt = tblUserDt.split('%');
            var rowLength = tblUserDt.length;
            if (rowLength != 0) {
                for (var i = 0; i <= rowLength - 1; i++) {
                    var p=i+1;
                    var rwDt = tblUserDt[i].split(';');
                    $("#tblRepo tbody").append(
                        '<tr id='+p+'>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[0] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[1] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[2] + '</div></td>' +
                        '</tr>' + '</hr>');
                    var len = $('#tblRepo tbody tr').length;
                    $('#label2').text(len);
                   
                }
            }
        }
        $('#label3').hide();
        $('#label4').hide();
        $("#tblRepo tbody").empty();
    }
    else if (conString == "tblRentHistory") {
        $("#tblRentHistory tbody tr").remove();
        $("#divRepo").hide();
        $("#divRepoStock").hide();
        $("#tblRentHistory").show();
        $('#divSearch').show();
        $('#ddlSearch').show();
        $('#ddlDC').hide();
        $('#btnETE').show();
        openSearch();

        //Add Code---Pending Try
            try {
                $.ajax({
                    type: "POST",
                    url: "Get/GetRentHistory.ashx",
                    cache: false,
                    data: fin,
                    dataType: "json",
                    success: getRentHistSuc,
                    error: function getbtnFail(cpnmsg) {
                        alert(cpnmsg.Response);
                    }
                });
            }
            catch (e) {
            }
            function getRentHistSuc(rnt) {
                var rntHis = rnt.Response;
                rntHis = rntHis.split('%');
                for (var i = 0; i <= rntHis.length - 1; i++) {
                    var rntClm = rntHis[i].split('^');
                    var k = i + 1;
                    $("#tblRentHistory tbody").append(
                           '<tr id=' + k + '>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[0] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[1] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[2] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[3] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[4] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[5] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[6] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + rntClm[7] + '</div></td>' +
                           '</tr>' + '</hr>');
                }
            }

    }
    else {
        
        $("#divRepo").hide();
        $("#rentHistory").hide();
        $("#divRepoStock").show();
        $('#divSearch').show();
        $('#ddlSearch').show();
        $('#ddlDC').hide();
        $('#btnETE').show();
        openSearch();
        var fin = conString;
        try {
            $.ajax({
                type: "POST",
                url: "Get/GetDesktopDetails.ashx",
                cache: false,
                data: fin,
                dataType: "json",
                success: getDesktopSuccess,
                error: function getbtnFail(cpnmsg) {
                    alert(cpnmsg.Response);
                }
            });
        }
        catch (e) {
        }
        function getDesktopSuccess(det) {
            $("#tblRepoStock tbody").empty();
            
            var tblStockDt = det.Response;
            tblStockDt = tblStockDt.split('%');
            var rowLength = tblStockDt.length;
            var rwDt;
            if (rowLength != 0) {

                for (var i = 0; i <= rowLength - 1; i++) {
                    rwDt = tblStockDt[i].split(';');
                    appendVal(rwDt, i);
                }
            }
            var len = $('#tblRepoStock tbody tr').length;
            //var len1 = $('#tblRepoStock tbody tr td:nth-child()')
            $('#label2').text(len);
            var count = 0;
            var count1 = 0;
            $('#tblRepoStock tbody tr').each(function () {

                var lbl = $(this).children('td:nth-child(3)').text();
                if (lbl == "Rent") {
                    count = count + 1;
                }
                if (lbl == "InStock") {
                    count1 = count1 + 1;
                }
            })
            $('#label3').text(count);
            $('#label4').text(count1);
        }
        $("#tblRepoStock tbody").empty();
    }
}
function fucSearch() {
    var selectedVal;
    var strCmp = [];


    if (conString == "tblDC") {
        selectedVal = $('#ddlDC option:selected').text() + "," + conString;
        var splitVal = selectedVal.split(',');
        var tbllen = $("#tblRepo tbody tr").length;
        if (splitVal[0] == "--Select--") {
            alert("Please Select from the dropdown below");
        }
        else if (splitVal[0] == "Company") {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepo tbody tr td:nth-child(2)").eq(i - 1).text();
            }
            getRepotItem(strCmp);
        }
        else if (splitVal[0] == "DCNo") {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepo tbody tr td:nth-child(1)").eq(i - 1).text();
            }
            getRepotItem(strCmp)
        }
        else {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepo tbody tr td:nth-child(3)").eq(i - 1).text();
            }
            getRepotItem(strCmp)
        }
    }
    else {
        selectedVal = $('#ddlSearch option:selected').text() + "," + conString;
        var splitVal = selectedVal.split(',');
        var tbllen = $("#tblRepoStock tbody tr").length;
        if (splitVal[0] == "--Select--") {
            alert("Please Select from the dropdown below");
        }
        else if (splitVal[0] == "Company") {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepoStock tbody tr td:nth-child(5)").eq(i - 1).text();
            }
            getRepotItem(strCmp);
        }
        else if (splitVal[0] == "SerialNumber") {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepoStock tbody tr td:nth-child(1)").eq(i - 1).text();
            }
            getRepotItem(strCmp)
        }
        else if (splitVal[0] == "DCNo") {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepoStock tbody tr td:nth-child(4)").eq(i - 1).text();
            }
            getRepotItem(strCmp)
        }
        else {
            for (var i = 0; i <= tbllen; i++) {
                strCmp[i] = $("#tblRepoStock tbody tr td:nth-child(2)").eq(i - 1).text();
            }
            getRepotItem(strCmp)
        }
    }
}
function getRepotItem(srcres) {
    var searchitm = [];
    //for (var i = 0; i <= srcres.length - 1; i++) {
    //searchitm.push(srcres[i]);
    $.each(srcres, function (i, el) {
        if ($.inArray(el, searchitm) === -1) searchitm.push(el);
    });

    //}
    $('#txt1').autocomplete({
        source: searchitm
    });
}
function btnSearch() {
    var strItem = $("#txt1").val();
    $('#btnETE').hide();
    var chk = [];
    var strCth;
    if (conString == "tblDC") {
        var ddlSelect = $("#ddlDC").val();
        var tblLen = $("#tblRepo tbody tr").length;
        for (var i = 0; i <= tblLen - 1; i++) {
            if (ddlSelect == 1) {
                strCth = $("#tblRepo tbody tr td:nth-child(1)").eq(i - 1).text();
            }
            if (ddlSelect == 2) {
                strCth = $("#tblRepo tbody tr td:nth-child(2)").eq(i - 1).text();
            }
            if (ddlSelect == 3) {
                strCth = $("#tblRepo tbody tr td:nth-child(3)").eq(i - 1).text();
            }
            if (strItem == strCth) {
                chk[0] = $("#tblRepo tbody tr td:nth-child(1)").eq(i - 1).text();
                chk[1] = $("#tblRepo tbody tr td:nth-child(2)").eq(i - 1).text();
                chk[2] = $("#tblRepo tbody tr td:nth-child(3)").eq(i - 1).text();
                var tblLen2 = $("#tblRepo tbody tr").length + 1;

                $("#tblRepo tr:last").after(
                    '<tr id=' + tblLen2 + '>' +
                              '<td><div style="color:black; right:inherit">' + chk[0] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[1] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[2] + '</div></td>' +
                           '</tr>' + '</hr>');
            }


        }
        for (var j = 0; j <= tblLen - 1; j++) {
            var x = j + 1;
            $("#tblRepo tbody tr#" + x).remove();
        }
        var tblLen1 = $("#tblRepo tbody tr").length;
        $('#label2').text(tblLen1);
        closeSearch();
        $('#ddlDC').hide();
    }
    else {
        var ddlSelect = $("#ddlSearch").val();
        var tblLenn = $("#tblRepoStock tbody tr").length;
        for (var i = 0; i <= tblLenn - 1; i++) {
            if (ddlSelect == 1) {
                strCth = $("#tblRepoStock tbody tr td:nth-child(1)").eq(i - 1).text();
            }
            if (ddlSelect == 2) {
                strCth = $("#tblRepoStock tbody tr td:nth-child(2)").eq(i - 1).text();
            }
            if (ddlSelect == 3) {
                strCth = $("#tblRepoStock tbody tr td:nth-child(5)").eq(i - 1).text();
            }
            if (ddlSelect == 4) {
                strCth = $("#tblRepoStock tbody tr td:nth-child(4)").eq(i - 1).text();
            }
            if (strItem == strCth) {
                chk[0] = $("#tblRepoStock tbody tr td:nth-child(1)").eq(i - 1).text();
                chk[1] = $("#tblRepoStock tbody tr td:nth-child(2)").eq(i - 1).text();
                chk[2] = $("#tblRepoStock tbody tr td:nth-child(3)").eq(i - 1).text();
                chk[3] = $("#tblRepoStock tbody tr td:nth-child(4)").eq(i - 1).text();
                chk[4] = $("#tblRepoStock tbody tr td:nth-child(5)").eq(i - 1).text();
                chk[5] = $("#tblRepoStock tbody tr td:nth-child(6)").eq(i - 1).text();
                chk[6] = $("#tblRepoStock tbody tr td:nth-child(7)").eq(i - 1).text();
                chk[7] = $("#tblRepoStock tbody tr td:nth-child(8)").eq(i - 1).text();
                var tblLen2 = $("#tblRepoStock tbody tr").length + 1;

                $("#tblRepoStock tr:last").after(
                    '<tr id=' + tblLen2 + '>' +
                              '<td><div style="color:black; right:inherit">' + chk[0] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[1] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[2] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[3] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[4] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[5] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[6] + '</div></td>' +
                              '<td><div style="color:black; right:inherit">' + chk[7] + '</div></td>' +
                           '</tr>' + '</hr>');

            }


        }

        for (var j = 0; j <= tblLenn - 1; j++) {
            var x = j + 1;
            $("#tblRepoStock tbody tr#" + x).remove();
        }
        var tblLen1 = $("#tblRepoStock tbody tr").length;
        $('#label2').text(tblLen1);
        closeSearch();
        $('#ddlSearch').hide();
        $('#label3').text("");
        $('#label4').text("");
        var count = 0;
        var count1 = 0;
        $('#tblRepoStock tbody tr').each(function () {

            var lbl = $(this).children('td:nth-child(3)').text();
            if (lbl == "Rent") {
                count = count + 1;
            }
            if (lbl == "InStock") {
                count1 = count1 + 1;
            }
        })
        $('#label3').text(count);
        $('#label4').text(count1);
    }
}
function appendVal(inChk, k) {
    k = k + 1;
    $("#tblRepoStock tbody").append(
                           '<tr id=' + k + '>' +
                           '<td><div style="color:black; right:inherit">' + inChk[0] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[1] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[2] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[3] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[4] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[5] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[6] + '</div></td>' +
                           '<td><div style="color:black; right:inherit">' + inChk[7] + '</div></td>' +
                           '</tr>' + '</hr>');
}
function closeSearch() {
    $('#txt1').hide();
    $('#btnSearch').hide();
}
function openSearch() {
    $('#txt1').show();
    $('#btnSearch').show();
    $('#txt1').val("");
    $('#ddlSearch').val("0");
}

//Export to Excel Reports
function btnExcel() {
    if (conString == "tblRentHistory") {
        var a = document.createElement('a');
        //getting data from our div that contains the HTML table
        var data_type = 'data:application/vnd.ms-excel';
        var table_div = document.getElementById('rentHistory');
        var table_html = table_div.outerHTML.replace(/ /g, '%20');
        a.href = data_type + ', ' + table_html;
        //setting the file name
        a.download = 'Rent_Report.xls';
        //triggering the function
        a.click();
    }
    else if (conString == "tblDC") {
        var a = document.createElement('a');
        //getting data from our div that contains the HTML table
        var data_type = 'data:application/vnd.ms-excel';
        var table_div = document.getElementById('divRepo');
        var table_html = table_div.outerHTML.replace(/ /g, '%20');
        a.href = data_type + ', ' + table_html;
        //setting the file name
        a.download = 'DC_Report.xls';
        //triggering the function
        a.click();
    }
    else {
        var a = document.createElement('a');
        //getting data from our div that contains the HTML table
        var data_type = 'data:application/vnd.ms-excel';
        var table_div = document.getElementById('divRepoStock');
        var table_html = table_div.outerHTML.replace(/ /g, '%20');
        a.href = data_type + ', ' + table_html;
        //setting the file name
        a.download = conString + '_Report.xls';
        //triggering the function
        a.click();
    }
}
function fucClose(cls) {

}