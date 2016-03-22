//Get Stock Table
function stocktable() {
    $("#stkItem").show();
}
function stocks(id) {
    closediv();
    var btnid;
    $('#tblStock tbody tr').remove();
    $('#tblCompany tbody tr').remove();
    if (id == "btnDesktop") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnDesktop").disabled = true;
        btnid = "Desktop";
    }
    if (id == "btnLaptop") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnLaptop").disabled = true;
        btnid = "Laptop";
    }
    if (id == "btnServer") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnLaptop").disabled = true;
        btnid = "Server";
    }
    if (id == "btnPrinter") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnPrinter").disabled = true;
        btnid = "Printer";
    }
    if (id == "btnProjector") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnProjector").disabled = true;
        btnid = "Projector";
    }
    if (id == "btnMobile") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnMobile").disabled = true;
        btnid = "Mobile";
    }
    if (id == "btnTablet") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnTablet").disabled = true;
        btnid = "Tablet";
    }
    if (id == "btnAccessories") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnAccessories").disabled = true;
        btnid = "Accessories";
    }
    if (id == "btnOthers") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').show()
        $('#tblCompany').hide();
        document.getElementById("btnOthers").disabled = true;
        btnid = "Others";
    }
    if (id == "btnCompany") {
        $('#divStock').show();
        $('#stkItem').hide();
        $('#tblStock').hide()
        $('#tblCompany').show();
        document.getElementById("btnCompany").disabled = true;
        btnid = "Company";
    }
    try {
        $.ajax({
            type: "POST",
            url: "GetStockDetails.ashx",
            cache: false,
            data: btnid,
            dataType: "json",
            success: getStockSuccess,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    } catch (e) {
    }
    function getStockSuccess(stk) {
        var stkrec = stk.Response;
        stkrec = stkrec.split(';');
        if (btnid != "Company") {
            for (var i = 0; i <= stkrec.length - 1; i++) {
                rowCount = i + 1;
                var stkitemrec = stkrec[i].split('^');
                $("#tblStock tbody").append(
                    '<tr >' +
                    '<td style="display:none" class="btd"><div class="tabstyle" style="display:none">' + rowCount + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[0] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[1] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[2] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[3] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[4] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[5] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[6] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[7] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[8] + '</div></td>' +
                    '<td><div class="tabstyle1">Edit</div></td>' +
                    '</tr>' + '<hr/>');
            }
            $(".tabstyle1").unbind('click').bind('click', editStk);
            $('#btnAddStock').show();
            $('#btnAddComp').hide();
        }
        else {
            for (var i = 0; i <= stkrec.length - 1; i++) {
                rowCount = i + 1;
                var stkitemrec = stkrec[i].split('%');
                $("#tblCompany tbody").append(
                    '<tr >' +
                    '<td style="display:none" class="btd"><div class="tabstyle" style="display:none">' + rowCount + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[0] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[1] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[2] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[3] + '</div></td>' +
                    '<td><div class="tabstyle1">' + stkitemrec[4] + '</div></td>' +
                    '<td><div class="tabstyle1">Edit</div></td>' +
                    '</tr>' + '<hr/>');
            }
            $(".tabstyle1").unbind('click').bind('click', editComp);
            $('#btnAddStock').hide();
            $('#btnAddComp').show();
        }

    }
}

//ADD Stock Details
function stockclick() {
    closediv();
    $("#form").show();
    $('#formStock').show();
    cleartext();
    var len = $('#tblStock tbody tr').length;
    var parti = $('#tblStock tbody tr td:nth-child(3)').eq(len - 1).text();
    $('#lblParticular').text(parti);
    $('#btnStkUpdt').hide();
    $('#btnStkAdd').show();
}

//Edit Stock Details
function editStk() {
    closediv();
    var rw = $(this).parent().parent();
    $('#txtSrNo').val(rw.children('td:nth-child(2)').text());
    $('#lblParticular').text(rw.children('td:nth-child(3)').text());
    $('#txtBrand').val(rw.children('td:nth-child(5)').text());
    document.getElementById('txtPurchaseDate').value= rw.children('td:nth-child(6)').text();
    $('#txtStkMdln').val(rw.children('td:nth-child(7)').text());
    $('#txtStkRemark').val(rw.children('td:nth-child(8)').text());
    $('#lblRentCode').text(rw.children('td:nth-child(10)').text());
    if ($('#ddlSelect option:selected').text() == "Brand") { $('#ddlSelect option:selected').val("0"); }
    else { $('#ddlSelect option:selected').val("1"); }
    if ($('#ddlStatus option:selected').text() == "Rent") { $('#ddlSelect option:selected').val("1"); }
    else { $('#ddlStatus option:selected').val("0"); }

    $("#form").show();
    $('#formStock').show();
    $('#btnStkUpdt').show();
    $('#btnStkAdd').hide();
}

//Company Text Field Click
function compclick() {
    closediv();
    $("#form").show();
    $('#formCompany').show();
    var len = $('#tblCompany tbody tr').length;
    len = len + 1;
    var compId = "BCId" + len;
    $('#txtCompId').val(compId);

    $('#btnCompAdd').show();
    $('#btnCompUpdt').hide();
}

//Add Company Detail to DB
function addcompdet(id) {
    closediv();
    var compId = $('#txtCompId').val();
    var compName = $('#txtCompName').val();
    var compAdd = $('#txtAddress').val();
    var compPhone = $('#txtPhone').val();
    var compLoc = $('#txtCompLoc').val();
    var jsonCompDet = compId + '%' + compName + '%' + compAdd + '%' + compPhone + '%' + compLoc+'%'+id;
    try {
        $.ajax({
            type: "POST",
            url: "PostCompDetail.ashx",
            cache: false,
            data: jsonCompDet,
            dataType: "json",
            success: postCompSuccess,
            error: function getFail(cpnmsg) {
                alert(cpnmsg.Response);
            }
        });
    }
    catch (e) { }
    function postCompSuccess(pcompres) {
        if (pcompres.length != 0) {
            closediv();
            alert(pcompres.Response)
        }
    }
}

//Edit Company Details to DB
function editComp() {
    closediv();
    var rw = $(this).parent().parent();
    $('#txtCompId').val(rw.children('td:nth-child(2)').text());
    $('#txtCompName').val(rw.children('td:nth-child(3)').text());
    $('#txtAddress').val(rw.children('td:nth-child(4)').text());
    $('#txtPhone').val(rw.children('td:nth-child(5)').text());
    $('#txtCompLoc').val(rw.children('td:nth-child(6)').text());

    $("#form").show();
    $('#formCompany').show();
    $('#btnCompAdd').hide();
    $('#btnCompUpdt').show();
}

