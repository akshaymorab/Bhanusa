//DCTable
function dctable() {
    btnenable();
    closediv();

    $("#tblDC tbody").empty();
    closediv();
    var fin = "hi";
    try {
        $.ajax({
            type: "POST",
            url: "GetDCDetails.ashx",
            cache: false,
            data: fin,
            dataType: "json",
            success: getDCSuccess,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    } catch (e) {
    }
    function getDCSuccess(dc) {
        var dcrec = dc.Response;
        dcrec = dcrec.split(';');
        for (var i = 0; i <= dcrec.length - 1; i++) {
            rowCount = i + 1;
            var dcitemrec = dcrec[i].split('*');
            $("#tblDC tbody").append(
                '<tr >' +
                '<td style="display:none" class="btd"><div class="tabstyle" style="display:none">' + rowCount + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[0] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[1] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[2] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[3] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[4] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[5] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[6] + '</div></td>' +
                '<td><div class="tabstyle1">' + dcitemrec[7] + '</div></td>' +
                '<td><div class="tabstyle1" style="cursor:pointer">Edit</div></td>' +
                '</tr>' + '<hr/>');
        }
        $(".tabstyle1").unbind('click').bind('click', editDC);
        //$(".cross").unbind('click').bind('click', Delete);
    }
    $("#btnAddDC").show();
    $("#divDC").show();
    document.getElementById("btnDC").disabled = true;
}

//ADD DC Details
function adddc() {
    closediv();
    $("#form").show();
    $('#formDC').show();
    cleartext();
    var len = $('#tblDC tbody tr').length;
    len = len + 1;
    $('#txtDCNo').val(len);
    var dt = new Date();
    $('#txtDate').val("");
    $('#txtDate').val(dt.toLocaleDateString());
    $('#tblDCDetails tbody tr').remove();
    $('#btnAddDCItem').show();
    $('#btnUpdtDCItem').hide();
}

//ADD DC Items
function adddcitem() {
    var len = $('#tblDCDetails tbody tr').length;
    var srno;
    srno = len + 1;
    var rtCode = $('#txtDCNo').val();
    rtCode = rtCode + "r" + len;
    if (srno == 1) {
        $("#btnDCPdf").show();
        genTbl(srno, rtCode);
    }
    else {
        var selPar, sts, conf, sernu, qty;
        $('#tblDCDetails  tbody  tr td:nth-child(2)').eq(len - 1).each(function () {
            selPar = $(this).find('select option:selected').val();
        });
        $('#tblDCDetails  tbody  tr td:nth-child(7)').eq(len - 1).each(function () {
            sts = $(this).find('select option:selected').val();
        });
        conf = $('#tblDCDetails tbody tr td:nth-child(3) input').eq(len - 1).text();
        sernu = $('#tblDCDetails tbody tr td:nth-child(4) input').eq(len - 1).text();
        qty = $('#tblDCDetails tbody tr td:nth-child(6) input').eq(len - 1).text();
        if (selPar == "Select" && sts == "0" && conf == "" && sernu == "" && qty == "") {
            alert("Please fill all fields");
        }
        else { genTbl(srno, rtCode); }
    }
    
}

//Generate DC Details table
function genTbl(serialnumb, rntcd) {
    var srno = serialnumb;
    var rtCode=rntcd

    $("#tblDCDetails tbody").append(
                '<tr >' +
       /*1*/    '<td><div class="tabstyle1">' + srno + '</div></td>' +
       /*2*/    '<td><div class="tabstyle1"><select class="ddlParticular"><option value="Select">--Select--</option><option value="Desktop">Desktop</option><option value="Laptop">Laptop</option><option value="Server">Server</option><option value="Printer">Printer</option><option value="Projector">Projector</option><option value="Mobile">Mobile</option><option value="Tablet">Tablet</option><option value="Accessories">Accessories</option><option value="Others">Others</option></select></div></td>' +
       /*3*/    '<td><input type="text" class="txtBig"/></td>' +
       /*4*/    '<td><input type="text" class="txtModelNo"/></td>' +
       /*5*/    '<td><input type="text" class="qty" /></td>' +
       /*6*/    '<td><input type="text" class="remarks"/></div></td>' +
       /*7*/    '<td><div class="tabstyle1"><select class="ddlStatus"><option value="0">--Select--</option><option value="1">Addition</option><option value="2">Advance Rep.</option><option value="3">Returned</option></select></div></td>' +
       /*8*/    '<td class="btd"><div class="divrwitm" style="display:none"></div></td>' +
       /*9*/    '<td class="btd"><div class="divrtcode" style="display:none">' + rtCode + '</div></td>' +
       /*10*/   '<td class="btd"><div class="divparticular" style="display:none"></div></td>' +
       /*11*/   '<td class="btd"><div class="divStatus" style="display:none"></div></td>' +
                '<td> <div class="btnclose1" id="btnClr">X</div> </td>'+
                '</tr>' + '<hr/>');
    $(".ddlParticular").unbind('change').bind('change', selItm);
    $(".ddlStatus").unbind('change').bind('change', selSts);
    $(".btnclose1").unbind('click').bind('click', funClose);
}

//Edit DC Details
function editDC() {
    adddc();
    $('#btnAddDCItem').hide();
    $('#btnUpdtDCItem').show();
    $("#btnDCPdf").show();
    var rw = $(this).parent().parent();
    var dcno = rw.children('td:nth-child(2)').text();
    $('#txtDCNo').val(rw.children('td:nth-child(2)').text());
    $('#txtCompany').val(rw.children('td:nth-child(3)').text());
    $('#txtCompAddress').val(rw.children('td:nth-child(4)').text());
    $('#txtLocation').val(rw.children('td:nth-child(5)').text());
    $('#txtRemarks').val(rw.children('td:nth-child(7)').text());

    try {
        $.ajax({
            type: "POST",
            url: "GetDCItem.ashx",
            cache: false,
            data: dcno,
            dataType: "json",
            success: getDCEdit,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    } catch (e) {
    }
    function getDCEdit(edit) {
        var dc = edit.Response;
        $('#txtDate').val("");
        dc = dc.split('&');
        $('#txtDate').val(dc[1]);
        var dcrec = dc[0];
        dcrec = dcrec.split('%');
        for (var k = 0; k <= dcrec.length - 1; k++) {
            var rent = dcrec[k].split('?');
            var rntDet = rent[0].split('>');
            var qty = rntDet[2];
            var items = rent[1].split('$');
            var mdln = items[0];
            var rmks = items[1];
            var srno = k + 1;
            $("#tblDCDetails tbody").append(
                '<tr >' +
                '<td><div class="tabstyle1">' + srno + '</div></td>' +
                '<td><div class="tabstyle1"><select class="ddlParticular"><option value="Select">--Select--</option><option value="Desktop">Desktop</option><option value="Laptop">Laptop</option><option value="Server">Server</option><option value="Printer">Printer</option><option value="Projector">Projector</option><option value="Mobile">Mobile</option><option value="Tablet">Tablet</option><option value="Accessories">Accessories</option><option value="Others">Others</option></select></div></td>' +
                '<td><input type="text" class="txtBig"/></td>' +
                '<td><input type="text" class="txtModelNo"/></td>' +
                '<td><input type="text" class="qty" /></td>' +
                '<td><input type="text" class="remarks"/></div></td>' +
                '<td><div class="tabstyle1"><select class="ddlStatus" onchange="funHist()"><option value="0">--Select--</option><option value="1">Addition</option><option value="2">Advance Rep.</option><option value="3">Returned</option></select></div></td>' +
                '<td class="btd"><div class="divrwitm" style="display:none">' + mdln + '</div></td>' +
                '<td class="btd"><div class="divrtcode" style="display:none">' + rntDet[4] + '</div></td>' +
                '<td class="btd"><div class="divparticular" style="display:none">' + rntDet[0] + '</div></td>' +
                '<td class="btd"><div class="divStatus" style="display:none">' + rntDet[3] + '</div></td>' +
                '</tr>' + '<hr/>');
            $(".ddlParticular").unbind('change').bind('change', selItm);
            $(".ddlStatus").unbind('change').bind('change', selSts);
            $('#tblDCDetails tbody tr td:nth-child(3) input').eq(srno - 1).val(rntDet[1]);
            $('#tblDCDetails tbody tr td:nth-child(4) input').eq(srno - 1).val(mdln);
            $('#tblDCDetails tbody tr td:nth-child(5) input').eq(srno - 1).val(qty);
            $('#tblDCDetails tbody tr td:nth-child(6) input').eq(srno - 1).val(rmks);
            var len = $('#tblDCDetails tbody tr').length;
            var dd = $('.ddlParticular option');
            var dds = $('.ddlStatus option');
            var x;
            var ddl = rntDet[0];
            for (x = 0; x <= dd.length - 1; x++) {
                if (ddl == dd[x].text) {
                    break;
                }
            }
            $('#tblDCDetails  tbody  tr td:nth-child(2)').eq(len - 1).each(function () {
                $(this).find('select option')[x].selected = true;
            });
            $('#tblDCDetails  tbody  tr td:nth-child(7)').eq(len - 1).each(function () {
                $(this).find('select option')[rntDet[3]].selected = true;
            });
        }
    }
    $('#tblDCDetails').show();
}

//Change Particular
function selItm() {

    var srno = $(this).closest('tr').index();
    var selectedVal;
    $('#tblDCDetails  tbody  tr').each(function () {
        selectedVal = $(this).find('.ddlParticular option:selected').text();
        //$(this).find('.txtModelNo').val("");
        try {
            $.ajax({
                type: "POST",
                url: "GetItemDetails.ashx",
                cache: false,
                data: selectedVal,
                dataType: "json",
                success: getDCItem,
                error: function getFail(cpnmsg) {
                    alert(cpnmsg.Response);
                }
            });
        } catch (e) {
        }
        function getDCItem(itemres) {
            if (itemres.length != 0) {
                var len = srno + 1;
                var str;
                var resmdl = [];
                var resrmks, x;
                for (var p = 0; p <= itemres.length - 1; p++) {
                    str = itemres[p];
                    str = str.split('^');
                    resmdl[p] = str[0];
                }

                $('.txtModelNo').autocomplete({
                    source: resmdl,
                    select: function (event, ui) {
                        var strrmk = ui.item.label;
                        //$('#tblDCDetails tbody tr td:nth-child(5)').eq(len - 1).html('<div style="display:none">' + strrmk + '</div>');
                        x = resmdl.indexOf(strrmk);
                        resrmks = itemres[x];
                        resrmks = resrmks.split('^');
                        $('#tblDCDetails tbody tr td:nth-child(6)').eq(len - 1).find('input').val(resrmks[1]);
                        $('#tblDCDetails tbody tr td:nth-child(8)').eq(len - 1).html('<div class="divrwitm" style="display:none">' + strrmk + '</div>');
                        $('#tblDCDetails tbody tr td:nth-child(10)').eq(len - 1).html('<div class="divparticular" style="display:none">' + selectedVal + '</div>');
                        //if (ui.item.label != "") { document.getElementById("btnDCItmUpdt").disabled = false; }
                    }
                });
            }
        }

    });
}

//Change Status
function selSts() {

    var srno = $(this).closest('tr').index();
    var selectedVal;
    var len = srno + 1;
    $('#tblDCDetails  tbody  tr').each(function () {
        selectedVal = $(this).find('.ddlStatus option:selected').val();
        $('#tblDCDetails tbody tr td:nth-child(11)').eq(len - 1).html('<div class="divStatus" style="display:none">' + selectedVal + '</div>');
    });
}
//Delete Row
function funClose() {
    $('#btnAddDCItem').hide();
    $('#btnUpdtDCItem').show();
    $("#btnDCPdf").show();
    var rw = $(this).parent().parent();
    rw.remove();
}
function funHist(){
    if ($('#addMoreRows(btnUpdtDcItem').click() == true) {
        try {
            $.ajax({
                type: "POST",
                url: "tblRentHistory.ashx",
                cache: false,
                data: text,
                dataType: "json",
                success: logsuc,
                error: function logerr(msg) {
                    alert(msg.Response);
                }
            })
        }
        catch (e) {
            alert(e.message);
        }
        function logsuc(logmsg) {
            $('#tblRentHistory').show();
            var tblUserDt = gtus.Response;
            tblUserDt = tblUserDt.split('%');
            var rowLength = tblUserDt.length;
            if (rowLength != 0) {
                for (var i = 0; i <= rowLength - 1; i++) {
                    var rwDt = tblUserDt[i].split(';');
                    $("#tblRentHistory tbody").append(
                        '<tr>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[0] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[1] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[2] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[3] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[4] + '</div></td>' +
                        '<td><div style="color:black; right:inherit">' + rwDt[5] + '</div></td>' +
                        '</tr>' + '</hr>');

                    var len = $('#tblRentHistory tbody tr').length;

                }
            }
        }

    }

}