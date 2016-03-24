var conString;
var id;
function repotable() {
    closediv();
    $('#divRpot').show();
    $('#divSearch').hide();
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
        case "btn13": conString = "tblCompany";
            break;
        default: conString = "";
            break;
    }
    if (conString == "tblDC" || conString == "tblCompany" || conString == "tblPO") {
        $("#tblRepoStock").hide();
        $("#tblRepo").show();
        $('#divSearch').show();
        openSearch();
        var fin = conString;
        try {
            $.ajax({
                type: "POST",
                url: "GetbtnDCDetails.ashx",
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
                    var rwDt = tblUserDt[i].split(';');
                    $("#tblRepo tbody").append(
                        '<tr>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[0] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[1] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[2] + '</div></td>' +
                        '</tr>' + '</hr>');

                    var len = $('#tblRepo tbody tr').length;
                    $('#label2').text(len);
                }

            }
        }
        $("#tblRepo tbody").empty();
    }
    else {
        $("#tblRepo").hide();
        $("#divSearch").show();
        openSearch();
        var fin = conString;
        try {
            $.ajax({
                type: "POST",
                url: "GetDesktopDetails.ashx",
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
            $("#tblRepoStock").show();
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
            $('#label2').text(len);

        }
        $("#tblRepoStock tbody").empty();
    }
}
function fucSearch() {
    var selectedVal;
    selectedVal = $('#ddlSearch option:selected').text() + "," + conString;
    try {
        $.ajax({
            type: "POST",
            url: "GetItemDetails.ashx",
            cache: false,
            data: selectedVal,
            dataType: "json",
            success: getRepotItem,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    }
    catch (e) {
    }

}
function getRepotItem(srcres) {
    var searchitm = [];
    for (var i = 0; i <= srcres.length - 1; i++) {
        searchitm.push(srcres[i]);
    }
    $('#txt1').autocomplete({
        source: searchitm
    });
}
function btnSearch() {
    var strItem = $("#txt1").val();
    var chk = [];
    var strCth;
    var ddlSelect = $("#ddlSearch").val();
    var tblLen = $("#tblRepoStock tbody tr").length;
    for (var i = 0; i < tblLen - 1; i++) {
        if (ddlSelect == 1) {
            strCth = $("#tblRepoStock tbody tr td:nth-child(1)").eq(i - 1).text();
        }
        if (ddlSelect == 2) {
            strCth = $("#tblRepoStock tbody tr td:nth-child(2)").eq(i - 1).text();
        }
        if (ddlSelect == 3) {
            strCth = $("#tblRepoStock tbody tr td:nth-child(5)").eq(i - 1).text();
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

    for (var j = 0; j <= tblLen - 1; j++) {
        var x = j + 1;
        $("#tblRepoStock tbody tr#" + x).remove();
    }
    var tblLen1 = $("#tblRepoStock tbody tr").length;
    $('#label2').text(tblLen1);
    closeSearch();
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
    $('#ddlSearch').hide();
}
function openSearch() {
    $('#txt1').show();
    $('#btnSearch').show();
    $('#ddlSearch').show();
}