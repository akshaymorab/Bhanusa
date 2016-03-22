//POTable
function potable() {
    var poNo, poCompany, poStart, poEnd, poRenew;
    $("#tblPO tbody").empty();
    closediv();
    $("#divPO").show();
    var fin = "hi";
    try {
        $.ajax({
            type: "POST",
            url: "GetPODetails.ashx",
            cache: false,
            data: fin,
            dataType: "json",
            success: getPOSuccess,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    } catch (e) {
    }
    function getPOSuccess(po) {
        var porec;
        porec = po.split(';');
        for (var i = 0; i <= porec.length - 1; i++) {
            rowCount = i + 1;
            var poitemrec = porec[i].split('%');
            $("#tblPO tbody").append(
                '<tr >' +
                '<td style="display:none" class="btd"><div class="tabstyle" style="display:none">' + rowCount + '</div></td>' +
                '<td><div class="tabstyle1">' + poitemrec[0] + '</div></td>' +
                '<td><div class="tabstyle1">' + poitemrec[1] + '</div></td>' +
                '<td><div class="tabstyle1">' + poitemrec[2] + '</div></td>' +
                '<td><div class="tabstyle1">' + poitemrec[3] + '</div></td>' +
                '<td><div class="tabstyle1">' + poitemrec[4] + '</div></td>' +
                '<td><div class="tabstyle1">Edit</div></td>' +
                '</tr>' + '<hr/>');
        }
        //$(".tabstyle1").unbind('click').bind('click', editPO);
        //$(".cross").unbind('click').bind('click', Delete);
    }
}

//Open PO FORM
function addPO() {
    closediv();
    $("#form").show();
    $('#formPO').show();
    cleartext();
    var len = $('#tblPO tbody tr').length;
    len = len + 1;
    $('#txtPoNo').val(len);
    $('#tblPO tbody tr').remove();
}

//Add PO Item
function addpodet() {
    var podet = $('#txtPoNo').val() + "%" + $('#txtPoComp').val() + "%" + $('#txtStartDate').val() + "%" + $('#txtEndDate').val() + "%" + $('#txtRenewDate').val();
        try {
            $.ajax({
                type: "POST",
                url: "PostPODetail.ashx",
                cache: false,
                data: podet,
                dataType: "json",
                success: postPOSuccess,
                error: function getFail(cpnmsg) {
                    alert(cpnmsg.Response);
                }
            });
        }
        catch (e) { alert(e.message); }
    function postPOSuccess(pores) {
        if (pores.length != 0) {
            closediv();
            alert(pores.Response)
        }
    }
    cleartext();
}

//AutoComplete PO
function autoPO() {
    var selval = "";
    try {
        $.ajax({
            type: "POST",
            url: "GetPODetails.ashx",
            cache: false,
            data: selval,
            dataType: "json",
            success: getPOComp,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    } catch (e) {
    }
    function getPOComp(pores) {
        if (pores.length != 0) {
            var poDetails = [];

            for (var i = 0; i <= pores.length - 1; i++) {
                poDetails.push(pores[i]);
            }
            $('#txtPoNumb').autocomplete({ source: poDetails });
        }
    }
}