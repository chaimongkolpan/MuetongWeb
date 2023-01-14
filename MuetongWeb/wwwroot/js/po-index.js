var add_detail = [];
var edit_detail = [];
var products = [];
var poColl = {};
var prDetails = [];
var insertDetailList = [];
function bindFilter(projectId) {
    var requesterUrl = baseUrl + 'requester/' + projectId;
    var jqxhr = $.get(requesterUrl)
        .done(function (response) {
            console.log(response);
            $('#RequesterId').empty();
            $('#RequesterId').append('<option value="0" selected>ทั้งหมด</option>');
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.fullname + '</option>';
                $('#RequesterId').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
            $('#RequesterId').empty();
            $('#RequesterId').append('<option value="0" selected>ทั้งหมด</option>');
        });
    var prnoUrl = baseUrl + 'prno/' + projectId;
    var jqxhr = $.get(prnoUrl)
        .done(function (response) {
            console.log(response);
            $('#PrNo').empty();
            $('#PrNo').append('<option selected>ทั้งหมด</option>');
            for (var i in response) {
                var item = response[i];
                var html = '<option>' + item + '</option>';
                $('#PrNo').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
            $('#PrNo').empty();
            $('#PrNo').append('<option selected>ทั้งหมด</option>');
        });
    var ponoUrl = baseUrl + 'pono/' + projectId;
    var jqxhr = $.get(ponoUrl)
        .done(function (response) {
            console.log(response);
            $('#PoNo').empty();
            $('#PoNo').append('<option selected>ทั้งหมด</option>');
            for (var i in response) {
                var item = response[i];
                var html = '<option>' + item + '</option>';
                $('#PoNo').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
            $('#PoNo').empty();
            $('#PoNo').append('<option selected>ทั้งหมด</option>');
        });
}
function bindProduct() {
    var productUrl = baseUrl + 'product';
    $('#AddProductId').empty();
    var jqxhr = $.get(productUrl)
        .done(function (response) {
            console.log(response);
            products = response;
            var html = '<option value="0">ทั้งหมด</option>';
            $('#AddProductId').append(html);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#AddProductId').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
$('#ProjectId').change(function () {
    bindFilter($('#ProjectId').val());
    $('#PrNo').val('ทั้งหมด');
    $('#PoNo').val('ทั้งหมด');
    $('#RequesterId').val(0);
});
$('#detail_check_all').change(function () {
    $('input[id^="detail_check__"]').prop('checked', $('#detail_check_all').prop('checked'));
});
$('#add_detail_add_btn').click(function () {
    $('input[id^="detail_check__"]').each(function (i, obj) {
        if ($(obj).prop('checked') && check_insert(i))
            insertDetail(i);
    });
    createDetailList();
});
function check_insert(i) {
    if (insertDetailList.length == 0) return true;
    return insertDetailList.findIndex(item => item.id == prDetails.details[i].id) < 0;
}
function get_detail_check_id(id) {
    var arr = id.split('__');
    if (arr.length > 1)
        return parseInt(arr[1]);
    return -1;
}
function insertDetail(i) {
    var tmp = Object.assign({}, prDetails.details[i]);
    tmp.pricePerUnit = 0;
    tmp.discount = 0;
    tmp.isVat = false;
    tmp.vat = 0;
    tmp.isWht = false;
    tmp.wht = 0;
    tmp.total = 0;
    insertDetailList.push(tmp);
}
function deleteDetailList(i) {
    insertDetailList.splice(i, 1);
    createDetailList();
}
function createDetailList() {
    $('#po_detail_table').empty();
    var html = '';
    for (var i in insertDetailList) {
        var detail = insertDetailList[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="deleteDetailList(' + i + ')" style="color:#A42206;">playlist_remove</span></td>';
        html += '<td>' + detail.projectName + '</td>';
        html += '<td>' + detail.prNo + '</td>';
        if (false) {
            html += '<td><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
            html += '<td>ชื่อผู้รับเหมา</td>';
        } else {
            html += '';
        }
        html += '<td>' + detail.requesterName + '</td>';
        html += '<td>' + detail.productName + '</td>';
        html += '<td>' + dateFormat(detail.useDate) + '</td>';
        html += '<td>' + detail.projectCode + '</td>';
        html += '<td>' + detail.remark + '</td>';
        html += '<td>' + detail.status + '</td>';
        html += '<td>' + detail.quantity + '</td>';
        html += '<td>' + detail.unit + '</td>';
        html += '<td><input type="text" class="form-control" value="0.00" id="po_detail_price__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="text" class="form-control" value="0.00" id="po_detail_discount__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_vat_check__' + i + '"><br /><label class="custom-control-label" id="po_detail_vat_text__' + i + '">0.00</label></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_wht_check__' + i + '"><br /><label class="custom-control-label" id="po_detail_wht_text__' + i + '">0.00</label></td>';
        html += '<td><label class="custom-control-label" id="po_detail_total__' + i + '">0.00</label></td>';
        html += '</tr>';
    }
    // check add other
    $('#po_detail_table').append(html);
    $('input[id^="po_detail_price__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        insertDetailList[i].pricePerUnit = floatValue($(this).val());
        calRecord(i);
    });
    $('input[id^="po_detail_price__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            insertDetailList[i].pricePerUnit = floatValue($(this).val());
            calRecord(i);
        }
    });
    $('input[id^="po_detail_discount__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        insertDetailList[i].discount = floatValue($(this).val());
        calRecord(i);
    });
    $('input[id^="po_detail_discount__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            insertDetailList[i].discount = floatValue($(this).val());
            calRecord(i);
        }
    });
    $('input[id^="po_detail_vat_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        insertDetailList[i].isVat = $(this).prop('checked');
        calRecord(i);
    });
    $('input[id^="po_detail_wht_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        insertDetailList[i].isWht = $(this).prop('checked');
        console.log(i, insertDetailList[i]);
        calRecord(i);
    });
}
$('#po_vat').change(function () {
    $('input[id^="po_detail_vat_check__"]').each(function (i, obj) {
        $(obj).prop('checked', $('#po_vat').prop('checked'));
        var j = get_detail_check_id($(obj)[0].id);
        insertDetailList[j].isVat = $(obj).prop('checked');
        calRecord(j);
    });
});
$('#po_wht').change(function () {
    $('input[id^="po_detail_wht_check__"]').each(function (i, obj) {
        $(obj).prop('checked', $('#po_wht').prop('checked'));
        var j = get_detail_check_id($(obj)[0].id);
        insertDetailList[j].isWht = $(obj).prop('checked');
        calRecord(j);
    });
});
function calRecord(i) {
    var vat = 0;
    var wht = 0;
    var subTotal = insertDetailList[i].pricePerUnit * insertDetailList[i].quantity;
    var total = subTotal - insertDetailList[i].discount;
    if (insertDetailList[i].isVat) vat = 0.07;
    $('#po_detail_vat_text__' + i).html(floatFormat(total * vat));
    insertDetailList[i].vat = floatValue(total * vat);
    var tmp = floatValue($('#po_wht_value').val());
    if (insertDetailList[i].isWht) wht = tmp / 100;
    $('#po_detail_wht_text__' + i).html(floatFormat(total * wht));
    insertDetailList[i].wht = floatValue(total * wht);
    var grandtotal = total + insertDetailList[i].vat - insertDetailList[i].wht;
    $('#po_detail_total__' + i).html(floatFormat(grandtotal));
    insertDetailList[i].total = grandtotal;
    calAll();
}
function calAll() {
    var subtotal = 0;
    var discount = 0;
    var vat = 0;
    var total = 0;
    var wht = 0;
    var grandtotal = 0;
    for (var i in insertDetailList) {
        var tmp = insertDetailList[i];
        subtotal += tmp.total;
        discount += tmp.discount;
        vat += tmp.vat;
        wht += tmp.wht;
        total += tmp.total - tmp.discount;
        grandtotal += (tmp.total - tmp.discount) + tmp.vat - tmp.wht;
    }
    $('#po_foot_subtotal').html(floatFormat(subtotal));
    $('#po_foot_discount').html(floatFormat(discount));
    $('#po_foot_vat').html(floatFormat(vat));
    $('#po_foot_total').html(floatFormat(total));
    $('#po_foot_wht').html(floatFormat(wht));
    $('#po_foot_grandtotal').html(floatFormat(grandtotal));
}
function createTable() {
    console.log(poColl);
    $('#pr_table').empty();
    if (poColl.pr != null) {
        var html = '';
        for (var i in poColl.pr) {
            var pr = poColl.pr[i];
            var len = pr.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td ' + rowspan + '>' + pr.projectName + '</td>';
            html += '<td ' + rowspan + '>' + pr.prNo + '</td>';
            if (pr.isAdvancePay) {
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td><td ' + rowspan + '>' + pr.contractorName + '</td>';
            } else {
                html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
            }
            html += '</td>';
            html += '<td ' + rowspan + '>' + pr.requesterName + '</td>';
            html += '<td ' + rowspan + '>' + pr.approverName + '</td>';
            if (len > 0) {
                var detail = pr.details[0];
                html += '<td>1</td>';
                html += '<td>' + detail.name + '</td>';
                html += '<td>' + detail.quantity + '</td>';
                html += '<td>' + detail.unit + '</td>';
                html += '<td>' + dateFormat(detail.useDate) + '</td>';
                html += '<td>' + detail.code + '</td>';
                html += '<td>' + detail.remark + '</td>';
                html += '<td>' + detail.status + '</td>';
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = pr.details[j];
                    html += '<tr>';
                    html += '<td>' + (parseInt(j) + 1) + '</td>';
                    html += '<td>' + detail.name + '</td>';
                    html += '<td>' + detail.quantity + '</td>';
                    html += '<td>' + detail.unit + '</td>';
                    html += '<td>' + dateFormat(detail.useDate) + '</td>';
                    html += '<td>' + detail.code + '</td>';
                    html += '<td>' + detail.remark + '</td>';
                    html += '<td>' + detail.status + '</td>';
                    html += '</tr>';
                }
            } else {
                html += '<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
                html += '</tr>';
            }
        }
        $('#pr_table').append(html);
        if (poColl.prCount > 0)
            $('#pr_tab').html('สั่งสินค้า&nbsp;<span class="badge bg-primary">' + poColl.prCount + '</span>');
        else
            $('#pr_tab').html('สั่งสินค้า');
    }
}
function search() {
    var searchUrl = baseUrl + 'search';
    var request = new FormData();
    request.append("ProjectId", $('#ProjectId').val());
    request.append("PrNo", $('#PrNo').val());
    request.append("RequesterId", $('#RequesterId').val());
    request.append("Status", $('#Status').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            poColl = result;
            createTable();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}
$('#search_btn').click(function () {
    search()
});
function createTablePr() {
    console.log(prDetails.details);
    $('#add_pr_table').empty();
    var html = '';
    for (var i in prDetails.details) {
        var pr = prDetails.details[i];
        html += '<tr>';
        html += '<td><div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="detail_check__' + pr.id + '"></div></td>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + pr.projectName + '</td>';
        html += '<td>' + pr.prNo + '</td>';
        html += '<td>' + pr.requesterName + '</td>';
        html += '<td>' + pr.productName + '</td>';
        html += '<td>' + pr.quantity + '</td>';
        html += '<td>' + pr.unit + '</td>';
        html += '<td>' + dateFormat(pr.useDate)  + '</td>';
        html += '<td>' + pr.projectCode + '</td>';
        html += '<td>' + pr.status + '</td>';
        html += '<td>' + pr.remark + '</td>';
        html += '<td><span class="material-symbols-outlined" data-bs-toggle="modal" data-bs-target="#RefOrder">description</span></td>';
        html += '<td><span class="material-symbols-outlined" data-bs-toggle="modal" data-bs-target="#RefApproverOrder">description</span></td>';
        html += '</tr>';
    }
    $('#add_pr_table').append(html);
}
function searchPr() {
    var searchUrl = baseUrl + 'searchpr';
    var request = new FormData();
    request.append("ProjectId", $('#AddProjectId').val());
    request.append("PrNo", $('#AddPrNo').val());
    request.append("ProductId", $('#AddProductId').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            prDetails = result;
            createTablePr();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}
$('#add_search').click(function () {
    searchPr()
});
$('#add_purchase').click(function () {
    console.log(insertDetailList);
    // make data
});
$('#add_store').change(function () {
    console.log(insertDetailList);
    // get payment type
});
function hide_credit_type() {
    $('#add_non_credit_day_pane').hide();
    $('#add_bg_contract_no_pane').hide();
    $('#add_bg_date_pane').hide();
    $('#add_port_check_no_pane').hide();
    $('#add_port_check_date_pane').hide();
    $('#add_after_transfer_day_pane').hide();
    $('#add_after_billing_day_pane').hide();
    $('#add_blank_pane').hide();
}
$('#add_credit_type').change(function () {
    if ($(this).val() == 'NonCredit') {
        hide_credit_type();
        $('#add_non_credit_day_pane').show();
    } else if ($(this).val() == 'BG') {
        hide_credit_type();
        $('#add_bg_contract_no_pane').show();
        $('#add_bg_date_pane').show();
        $('#add_blank_pane').show();
    } else if ($(this).val() == 'PortCheck') {
        hide_credit_type();
        $('#add_port_check_no_pane').show();
        $('#add_port_check_date_pane').show();
        $('#add_blank_pane').show();
    } else if ($(this).val() == 'AfterTransfer') {
        hide_credit_type();
        $('#add_after_transfer_day_pane').show();
    } else if ($(this).val() == 'AfterBilling') {
        hide_credit_type();
        $('#add_after_billing_day_pane').show();
    } else {
        hide_credit_type();
    }
});
$('#add_payment_type').change(function () {
    if ($(this).val() == 'โอนเงิน') {
        $('#add_payment_account').prop('disabled', false);
    } else {
        $('#add_payment_account').prop('disabled', 'disabled');
    }
});
$('#add_addition_check').change(function () {
    if ($('#add_addition_check').prop('checked')) {
        $('#add_addition').prop('disabled', false);
        $('#add_addition').val('ค่าขนส่ง');
        $('#add_addition_other').prop('disabled', 'disabled');
        $('#add_addition_other').val('');
    } else {
        $('#add_addition').prop('disabled', 'disabled');
        $('#add_addition').val('ค่าขนส่ง');
        $('#add_addition_other').prop('disabled', 'disabled');
        $('#add_addition_other').val('');
    }
});
$('#add_addition').change(function () {
    $('#add_addition_other').val('');
    if ($(this).val() == 'อื่นๆ') {
        $('#add_addition_other').prop('disabled', false);
    } else {
        $('#add_addition_other').prop('disabled', 'disabled');
    }

});
$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    bindProduct();
    search();
});