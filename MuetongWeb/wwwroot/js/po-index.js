var products = [];
var stores = [];
var poColl = {};
var prOther = {
    pricePerUnit: 0,
    discount: 0,
    isVat: false,
    vat: 0,
    isWht: false,
    wht: 0,
    total: 0
};
var prDetails = [];
var insertDetailList = [];
var creditType = {
    non: 0,
    bg: 0,
    check: 0,
    afTransfer: 0,
    afBilling: 0
};
var paymentType = {
    cash: 0,
    transfer: 0,
    credit: 0
};
var vatRate = 0.07;
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
            bindPayment($('#add_store').val());
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindReceiveType() {
    var receiveUrl = baseUrl + 'receive';
    $('#add_billing_receive').empty();
    $('#add_receipt_receive').empty();
    var jqxhr = $.get(receiveUrl)
        .done(function (response) {
            console.log(response);
            for (var i in response.billing) {
                var item = response.billing[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_billing_receive').append(html);
            }
            for (var i in response.receipt) {
                var item = response.receipt[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_receipt_receive').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindPaymentType() {
    var receiveUrl = baseUrl + 'Type';
    $('#add_payment_type').empty();
    $('#add_credit_type').empty();
    var jqxhr = $.get(receiveUrl)
        .done(function (response) {
            console.log(response);
            for (var i in response.creditType) {
                var item = response.creditType[i];
                if (item.name == 'Non credit (ไม่มีหลักประกัน)') {
                    creditType.non = item.id;
                } else if (item.name == 'BG (หนังสือค้ำประกันธนาคาร)') {
                    creditType.bg = item.id;
                } else if (item.name == 'Port Check') {
                    creditType.check = item.id;
                } else if (item.name == 'หลังรับสินค้า') {
                    creditType.afTransfer = item.id;
                } else if (item.name == 'หลังรับวางบิล') {
                    creditType.afBilling = item.id;
                }
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_credit_type').append(html);
            }
            for (var i in response.paymentType) {
                var item = response.paymentType[i];
                if (item.name == 'เงินสด') {
                    paymentType.cash = item.id;
                } else if (item.name == 'โอนเงิน') {
                    paymentType.transfer = item.id;
                } else if (item.name == 'บัตรเครดิต') {
                    paymentType.credit = item.id;
                }
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_payment_type').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindPayment(id) {
    console.log(stores);
    var store = stores.find(x => x.id == id);
    $('#add_payment_account').empty();
    for (var i in store.payments) {
        var item = store.payments[i];
        var html = '<option value="' + item.id + '">' + item.bank + ' ' + item.accountNo + '</option>';
        $('#add_payment_account').append(html);
    }
    $('#add_address').val(store.address);
    $('#add_mobile_no').val(store.phoneNo);
    $('#add_tax_no').val(store.taxNo);
}
$('#add_store').change(function () {
    bindPayment($('#add_store').val());
});
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
$('#add_btn').click(function () {
    // clear data modal
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
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.pricePerUnit) + '" id="po_detail_price__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.discount) + '" id="po_detail_discount__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_vat_check__' + i + '"' + (detail.isVat ? 'checked' : '') + '><br /><label class="custom-control-label" id="po_detail_vat_text__' + i + '">' + floatFormat(detail.vat) + '</label></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_wht_check__' + i + '"' + (detail.isWht ? 'checked' : '') + '><br /><label class="custom-control-label" id="po_detail_wht_text__' + i + '">' + floatFormat(detail.wht) + '</label></td>';
        html += '<td><label class="custom-control-label" id="po_detail_total__' + i + '">' + floatFormat(detail.total) + '</label></td>';
        html += '</tr>';
        calRecord(i);
    }
    if ($('#add_addition_check').prop('checked')) {
        var name = 'ค่าขนส่ง';
        if ($('#add_addition').val() == 'อื่นๆ') name = $('#add_addition_other').val();
        var detail = prOther;
        html += '<tr>';
        html += '<td>' + (insertDetailList.length + 1) + '</td>';
        html += '<td colspan="4"></td>';
        html += '<td>' + name + '</td>';
        html += '<td colspan="6"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.pricePerUnit) + '" id="po_detail_price__' + insertDetailList.length + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.discount) + '" id="po_detail_discount__' + insertDetailList.length + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_vat_check__' + insertDetailList.length + '"' + (detail.isVat ? 'checked' : '') + '><br /><label class="custom-control-label" id="po_detail_vat_text__' + insertDetailList.length + '">' + floatFormat(detail.vat) + '</label></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="po_detail_wht_check__' + insertDetailList.length + '"' + (detail.isWht ? 'checked' : '') + '><br /><label class="custom-control-label" id="po_detail_wht_text__' + insertDetailList.length + '">' + floatFormat(detail.wht) + '</label></td>';
        html += '<td><label class="custom-control-label" id="po_detail_total__' + insertDetailList.length + '">' + floatFormat(detail.total) + '</label></td>';
        html += '</tr>';
    }
    $('#po_detail_table').append(html);
    $('input[id^="po_detail_price__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertDetailList.length) {
            prOther.pricePerUnit = floatValue($(this).val());
            calRecordOther(i);
        } else {
            insertDetailList[i].pricePerUnit = floatValue($(this).val());
            calRecord(i);
        }
        $(this).val(floatFormat($(this).val()));
    });
    $('input[id^="po_detail_price__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            if (i == insertDetailList.length) {
                prOther.pricePerUnit = floatValue($(this).val());
                calRecordOther(i);
            } else {
                insertDetailList[i].pricePerUnit = floatValue($(this).val());
                calRecord(i);
            }
            $(this).val(floatFormat($(this).val()));
        }
    });
    $('input[id^="po_detail_discount__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertDetailList.length) {
            prOther.discount = floatValue($(this).val());
            calRecordOther(i);
        } else {
            insertDetailList[i].discount = floatValue($(this).val());
            calRecord(i);
        }
        $(this).val(floatFormat($(this).val()));
    });
    $('input[id^="po_detail_discount__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            if (i == insertDetailList.length) {
                prOther.discount = floatValue($(this).val());
                calRecordOther(i);
            } else {
                insertDetailList[i].discount = floatValue($(this).val());
                calRecord(i);
            }
            $(this).val(floatFormat($(this).val()));
        }
    });
    $('input[id^="po_detail_vat_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertDetailList.length) {
            prOther.isVat = $(this).prop('checked');
            calRecordOther(i);
        } else {
            insertDetailList[i].isVat = $(this).prop('checked');
            calRecord(i);
        }
    });
    $('input[id^="po_detail_wht_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertDetailList.length) {
            prOther.isWht = $(this).prop('checked');
            calRecordOther(i);
        } else {
            insertDetailList[i].isWht = $(this).prop('checked');
            calRecord(i);
        }
    });
}
$('#add_addition_other').focusout(function () {
    createDetailList();
});
$('#add_addition_other').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        createDetailList();
    }
});
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
function calRecordOther(i) {
    var vat = 0;
    var wht = 0;
    var total = prOther.pricePerUnit - prOther.discount;
    if (prOther.isVat) vat = vatRate;
    $('#po_detail_vat_text__' + i).html(floatFormat(total * vat));
    prOther.vat = floatValue(total * vat);
    var tmp = floatValue($('#po_wht_value').val());
    if (prOther.isWht) wht = tmp / 100;
    $('#po_detail_wht_text__' + i).html(floatFormat(total * wht));
    prOther.wht = floatValue(total * wht);
    var grandtotal = total + prOther.vat - prOther.wht;
    $('#po_detail_total__' + i).html(floatFormat(grandtotal));
    prOther.total = grandtotal;
    calAll();
}
function calRecord(i) {
    var vat = 0;
    var wht = 0;
    var subTotal = insertDetailList[i].pricePerUnit * insertDetailList[i].quantity;
    var total = subTotal - insertDetailList[i].discount;
    if (insertDetailList[i].isVat) vat = vatRate;
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
    if ($('#add_addition_check').prop('checked')) {
        subtotal += prOther.pricePerUnit;
        discount += prOther.discount;
        vat += prOther.vat;
        wht += prOther.wht;
        total += prOther.pricePerUnit - prOther.discount;
        grandtotal += (prOther.pricePerUnit - prOther.discount) + prOther.vat - prOther.wht;
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
    $('#all_table').empty();
    $('#wait_table').empty();
    $('#pr_table').empty();
    $('#cancel_table').empty();
    if (poColl.all != null) {
        var html = '';
        for (var i in poColl.all) {
            var po = poColl.all[i];
            var len = po.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            html += '<td class="bgpo" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_po(' + i + ',1)" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionPurchase">view_list</span></td>';
            html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_po(' + i + ',1)" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.poNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeName + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeAddress + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storePhoneNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeTaxNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.creditType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.beforeDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.contractNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.dateSpecific + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.afterDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccount + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccountType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.paymentDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.billingReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.receiptReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.planTransferDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.requesterName + '</td>';

            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '</tr>';
            /*
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
            */
        }
        $('#all_table').append(html);
    }
    if (poColl.waiting != null) {
        var html = '';
        for (var i in poColl.waiting) {
            var po = poColl.waiting[i];
            var len = po.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            html += '<td class="bgpo" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_po(' + i + ',2)" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionPurchase">view_list</span></td>';
            html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_po(' + i + ',2)" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.poNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeName + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeAddress + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storePhoneNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeTaxNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.creditType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.beforeDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.contractNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.dateSpecific + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.afterDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccount + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccountType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.paymentDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.billingReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.receiptReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.planTransferDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.requesterName + '</td>';

            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '</tr>';
            /*
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
            */
        }
        $('#wait_table').append(html);
        //if (poColl.waitingCount > 0) 
        //    $('#pr_tab').html('สั่งซื้อ&nbsp;<span class="badge bg-primary">' + poColl.waitingCount + '</span>');
        //else
        //    $('#pr_tab').html('สั่งซื้อ');
    }
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
    if (poColl.cancel != null) {
        var html = '';
        for (var i in poColl.cancel) {
            var po = poColl.cancel[i];
            var len = po.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            if (po.isReadCancel) {
                html += '<td class="bgpo" ' + rowspan + '></td>';
            } else {
                html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="read_cancel_po(' + i + ')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionApproveCancel">assignment_late</span></td>';
            }
            html += '<td class="bgpo" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.poNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeName + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeAddress + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storePhoneNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.storeTaxNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.creditType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.beforeDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.contractNo + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.dateSpecific + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.afterDay + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccount + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccountType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.paymentDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.billingReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.receiptReceiveType + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.planTransferDate) + '</td>';
            html += '<td class="bgpo" ' + rowspan + '>' + po.requesterName + '</td>';

            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '<td class="bgpr"></td>';
            html += '</tr>';
            /*
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
            */
        }
        $('#cancel_table').append(html);
        if (poColl.unreadCancelCount > 0) 
            $('#cancel_tab').html('ยกเลิก&nbsp;<span class="badge bg-danger">' + poColl.unreadCancelCount + '</span>');
        else
            $('#cancel_tab').html('ยกเลิก');
    }
}
$('#read_po_btn').click(function () {
    var id = $('#cancel_read_id').val();
    var readUrl = baseUrl + 'read/' + id;
    $.ajax({
        type: "GET",
        url: readUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#ActionApproveCancel').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionApproveCancel').modal('hide');
        }
    });
});
$('#cancel_btn').click(function () {
    var id = $('#cancel_id').val();
    var cancelUrl = baseUrl + 'cancel/' + id + '?remark=' + $('#cancel_remark').val();
    $.ajax({
        type: "GET",
        url: cancelUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#ActionCancel').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionCancel').modal('hide');
        }
    });
});
$('#edit_btn').click(function () {

});
function read_cancel_po(index) {
    var po = poColl.cancel[index];
    $('#cancel_read_id').val(po.id);
    $('#cancel_read_text').html('ท่านรับทราบการยกเลิกการสั่งซื้อ เลขที่ ' + po.poNo);
}
function cancel_po(index,tab) {
    var po = {};
    if (tab == 2)
        po = poColl.waiting[index];
    else
        po = poColl.all[index];
    $('#cancel_id').val(po.id);
    $('#cancel_remark').val('');
    $('#po_cancel_text').html('ท่านต้องการยกเลิกการสั่งซื้อ เลขที่ ' + po.poNo);
}
function edit_po(index, tab) {
    // bind data
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
    const input = document.getElementById('add_files');
    var addUrl = baseUrl + 'add';
    var data = new FormData();
    data.append("StoreId", $('#add_store').val());
    data.append("CreditType", $('#add_credit_type').val());
    if ($('#add_credit_type').val() == creditType.non) {
        data.append("DateValue", $('#add_non_credit_day').val());
    } else if ($('#add_credit_type').val() == creditType.bg) {
        data.append("BgContractNo", $('#add_bg_contract_no').val());
        data.append("DateSpecific", $('#add_bg_date').val());
    } else if ($('#add_credit_type').val() == creditType.check) {
        data.append("ChequeNo", $('#add_port_check_no').val());
        data.append("DateSpecific", $('#add_port_check_date').val());
    } else if ($('#add_credit_type').val() == creditType.afTransfer) {
        data.append("DateValue", $('#add_after_transfer_day').val());
    } else if ($('#add_credit_type').val() == creditType.afBilling) {
        data.append("DateValue", $('#add_after_billing_day').val());
    }
    data.append("PaymentType", $('#add_payment_type').val());
    data.append("PaymentAccountId", $('#add_payment_account').val());
    data.append("PayDate", $('#add_payment_date').val());
    data.append("ReceiveBillingTypeId", $('#add_billing_receive').val());
    data.append("ReceiveReceiptTypeId", $('#add_receipt_receive').val());
    data.append("PlanTransferDate", $('#add_plan_transfer_date').val());
    data.append("HasAdditional", $('#add_addition_check').prop('checked'));
    data.append("AdditionalType", $('#add_addition').val());
    data.append("AdditionalOther", $('#add_addition_other').val());
    data.append("VatRate", vatRate);
    data.append("WhtRate", $('#po_wht_value').val() / 100);
    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    var details = [];
    for (var i = 0; i < insertDetailList.length; i++) {
        var detail = {
            PrDetailId: insertDetailList[i].id,
            PricePerUnit: insertDetailList[i].pricePerUnit,
            Discount: insertDetailList[i].discount,
            IsVat: insertDetailList[i].isVat,
            IsWht: insertDetailList[i].isWht
        };
        details.push(detail);
    }
    if ($('#add_addition_check').prop('checked')) {
        var other = {
            PricePerUnit: prOther.pricePerUnit,
            Discount: prOther.discount,
            IsVat: prOther.isVat,
            IsWht: prOther.isWht
        };
        var jsonStrOther = JSON.stringify(other);
        data.append("JsonOther", jsonStrOther);
    }
    console.log(details);
    var jsonStr = JSON.stringify(details);
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
            $('#AddPurchase').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#AddPurchase').modal('hide');
        }
    });
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
    if ($(this).val() == creditType.non) {
        hide_credit_type();
        $('#add_non_credit_day_pane').show();
    } else if ($(this).val() == creditType.bg) {
        hide_credit_type();
        $('#add_bg_contract_no_pane').show();
        $('#add_bg_date_pane').show();
        $('#add_blank_pane').show();
    } else if ($(this).val() == creditType.check) {
        hide_credit_type();
        $('#add_port_check_no_pane').show();
        $('#add_port_check_date_pane').show();
        $('#add_blank_pane').show();
    } else if ($(this).val() == creditType.afTransfer) {
        hide_credit_type();
        $('#add_after_transfer_day_pane').show();
    } else if ($(this).val() == creditType.afBilling) {
        hide_credit_type();
        $('#add_after_billing_day_pane').show();
    } else {
        hide_credit_type();
    }
});
$('#add_payment_type').change(function () {
    if ($(this).val() == paymentType.transfer) {
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
    createDetailList();
});
$('#add_addition').change(function () {
    $('#add_addition_other').val('');
    if ($(this).val() == 'อื่นๆ') {
        $('#add_addition_other').prop('disabled', false);
    } else {
        $('#add_addition_other').prop('disabled', 'disabled');
    }
    createDetailList();
});
$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    bindProduct();
    bindStore();
    bindReceiveType();
    bindPaymentType();
    search();
});