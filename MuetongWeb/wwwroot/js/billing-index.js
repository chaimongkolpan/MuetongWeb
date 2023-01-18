var stores = [];
var poColl = [];
var billingPo = [];




$('#add_billing_save_btn').click(function () {
    console.log(billingPo);
    const input = document.getElementById('billing_files');
    var addUrl = baseUrl + 'add';
    var data = new FormData();
    data.append("BillingNo", $('#billing_no').val());
    data.append("BillingDate", $('#billing_date').val());
    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    var jsonStr = JSON.stringify(billingPo);
    data.append("JsonDetails", jsonStr);
    console.log(jsonStr);
    $.ajax({
        type: "POST",
        url: addUrl,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            console.log(result);
            search();
            $('#AddBilling').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#AddBilling').modal('hide');
        }
    });
});
function remove_po(i) {
    billingPo.splice(i, 1);
    createBillingPo();
}
function createBillingPo() {
    $('#billing_po_table').empty();
    if (billingPo.length > 0) {
        var html = '';
        for (var i in billingPo) {
            var po = billingPo[i];
            var len = po.details.length;
            var rowspan = '';
            if (len > 1)
                rowspan = ' rowspan="' + len + '"';
            html += '<tr>';
            html += '<td' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td' + rowspan + '><span class="material-symbols-outlined" style="color:#A42206;" onclick="remove_po(' + i + ')">playlist_remove</span></td>';
            html += '<td' + rowspan + '>' + po.poNo + '</td>';
            html += '<td' + rowspan + '>' + po.remark + '</td>';
            html += '<td' + rowspan + '>' + po.status + '</td>';
            html += '<td' + rowspan + '>' + po.storeName + '</td>';
            if (len > 0) {
                var detail = po.details[0];
                html += '<td>1</td>';
                html += '<td>' + detail.projectName + '</td>';
                html += '<td>' + detail.prNo + '</td>';
                html += '<td>' + dateFormat(detail.createDate) + '</td>';
                if (detail.isAdvancePay) {
                    html += '<td><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
                    html += '<td>' + detail.contractorName + '</td>';
                } else {
                    html += '<td></td><td></td>';
                }
                html += '<td>' + detail.requesterName + '</td>';
                html += '<td>' + detail.name + '</td>';
                html += '<td>' + detail.code + '</td>';
                html += '<td>' + detail.status + '</td>';
                html += '<td>' + floatFormat(detail.quantity) + '</td>';
                html += '<td>' + detail.unit + '</td>';
                html += '<td>' + floatFormat(detail.pricePerUnit) + '</td>';
                html += '<td>' + floatFormat(detail.discount) + '</td>';
                html += '<td>' + floatFormat(detail.vat) + '</td>';
                html += '<td>' + floatFormat(detail.wht) + '</td>';
                html += '<td>' + floatFormat(detail.total) + '</td>';
                html += '<td>' + dateFormat(detail.useDate) + '</td>';
                html += '<td>' + detail.remark + '</td>';
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += '<td>' + (parseInt(j) + 1) + '</td>';
                    html += '<td>' + detail.projectName + '</td>';
                    html += '<td>' + detail.prNo + '</td>';
                    html += '<td>' + dateFormat(detail.createDate) + '</td>';
                    if (detail.isAdvancePay) {
                        html += '<td><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
                        html += '<td>' + detail.contractorName + '</td>';
                    } else {
                        html += '<td></td><td></td>';
                    }
                    html += '<td>' + detail.requesterName + '</td>';
                    html += '<td>' + detail.name + '</td>';
                    html += '<td>' + detail.code + '</td>';
                    html += '<td>' + detail.status + '</td>';
                    html += '<td>' + floatFormat(detail.quantity) + '</td>';
                    html += '<td>' + detail.unit + '</td>';
                    html += '<td>' + floatFormat(detail.pricePerUnit) + '</td>';
                    html += '<td>' + floatFormat(detail.discount) + '</td>';
                    html += '<td>' + floatFormat(detail.vat) + '</td>';
                    html += '<td>' + floatFormat(detail.wht) + '</td>';
                    html += '<td>' + floatFormat(detail.total) + '</td>';
                    html += '<td>' + dateFormat(detail.useDate) + '</td>';
                    html += '<td>' + detail.remark + '</td>';
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20"></td>';
                html += '</tr>';
            }
            html += '</tr>';
        }
        $('#billing_po_table').append(html);
    }
}
function createPoTable(pos) {
    poColl = pos;
    $('#po_table').empty();
    var html = '';
    for (var i in pos) {
        var po = pos[i];
        var len = po.details.length;
        var rowspan = '';
        if (len > 1)
            rowspan = ' rowspan="' + len + '"';
        html += '<tr>';
        html += '<td' + rowspan + '><input type="checkbox" class="custom-control-input" id="po_check__' + i + '"></td>';
        html += '<td' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
        html += '<td' + rowspan + '>' + po.poNo + '</td>';
        html += '<td' + rowspan + '>' + po.remark + '</td>';
        html += '<td' + rowspan + '>' + po.status + '</td>';
        html += '<td' + rowspan + '>' + po.storeName + '</td>';
        if (len > 0) {
            var detail = po.details[0];
            html += '<td>1</td>';
            html += '<td>' + detail.projectName + '</td>';
            html += '<td>' + detail.prNo + '</td>';
            html += '<td>' + dateFormat(detail.createDate) + '</td>';
            if (detail.isAdvancePay) {
                html += '<td><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
                html += '<td>' + detail.contractorName + '</td>';
            } else {
                html += '<td></td><td></td>';
            }
            html += '<td>' + detail.requesterName + '</td>';
            html += '<td>' + detail.name + '</td>';
            html += '<td>' + detail.code + '</td>';
            html += '<td>' + detail.status + '</td>';
            html += '<td>' + floatFormat(detail.quantity) + '</td>';
            html += '<td>' + detail.unit + '</td>';
            html += '<td>' + floatFormat(detail.pricePerUnit) + '</td>';
            html += '<td>' + floatFormat(detail.discount) + '</td>';
            html += '<td>' + floatFormat(detail.vat) + '</td>';
            html += '<td>' + floatFormat(detail.wht) + '</td>';
            html += '<td>' + floatFormat(detail.total) + '</td>';
            html += '<td>' + dateFormat(detail.useDate) + '</td>';
            html += '<td>' + detail.remark + '</td>';
            html += '</tr>';
            for (var j = 1; j < len;j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += '<td>' + (parseInt(j) + 1) + '</td>';
                html += '<td>' + detail.projectName + '</td>';
                html += '<td>' + detail.prNo + '</td>';
                html += '<td>' + dateFormat(detail.createDate) + '</td>';
                if (detail.isAdvancePay) {
                    html += '<td><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
                    html += '<td>' + detail.contractorName + '</td>';
                } else {
                    html += '<td></td><td></td>';
                }
                html += '<td>' + detail.requesterName + '</td>';
                html += '<td>' + detail.name + '</td>';
                html += '<td>' + detail.code + '</td>';
                html += '<td>' + detail.status + '</td>';
                html += '<td>' + floatFormat(detail.quantity) + '</td>';
                html += '<td>' + detail.unit + '</td>';
                html += '<td>' + floatFormat(detail.pricePerUnit) + '</td>';
                html += '<td>' + floatFormat(detail.discount) + '</td>';
                html += '<td>' + floatFormat(detail.vat) + '</td>';
                html += '<td>' + floatFormat(detail.wht) + '</td>';
                html += '<td>' + floatFormat(detail.total) + '</td>';
                html += '<td>' + dateFormat(detail.useDate) + '</td>';
                html += '<td>' + detail.remark + '</td>';
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20"></td>';
            html += '</tr>';
        }
    }
    $('#po_table').append(html);
}
$('#search_po').click(function () {
    var searchUrl = baseUrl + 'searchpo';
    var request = new FormData();
    request.append("StoreId", $('#add_store').val());
    request.append("PoNo", $('#add_pono').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            console.log(result);
            createPoTable(result.pos);
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#add_po').click(function () {
    $('input[id^="po_check__"]').each(function (i, obj) {
        if ($(obj).prop('checked')) {
            var po = poColl[i];
            if (billingPo.length == 0) {
                billingPo.push(po);
            } else {
                var ind = billingPo.findIndex(item => item.id == po.id);
                if (ind < 0) {
                    billingPo.push(po);
                }
            }
        }
    });
    createBillingPo();
});
$('#po_check_all').change(function () {
    $('input[id^="po_check__"]').prop('checked', $('#po_check_all').prop('checked'));
});
$('#add_billing').click(function () {
    billingPo = [];
    $('#billing_po_table').empty();
    $('#billing_no').val('');
    $('#billing_date').val('');
    $('#billing_files').val('');
});
function bindStore() {
    var storeUrl = baseUrl + 'store';
    $('#add_store').empty();
    var jqxhr = $.get(storeUrl)
        .done(function (response) {
            console.log(response);
            stores = response.stores;
            for (var i in response.stores) {
                var item = response.stores[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_store').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function search() {

}
$(document).ready(function () {
    console.log('ready', model);
    // get filter
    bindStore();
    search();
});