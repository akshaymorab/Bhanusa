var conString;
var id;
function repotable() {
    closediv();
    $('#divRpot').show();
    $('#divSearch').show();
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
    if (conString == "tblDC" || conString == "tblEmployeeDetails" || conString == "tblPO") {
        $("#tblRepoStock").hide();
        $("#tblRepo").show();
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
                    $("#tblRepoStock tbody").append(
                        '<tr>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[0] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[1] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[2] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[3] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[4] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[5] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[6] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[7] + '</div></td>' +
                        '</tr>' + '</hr>');
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