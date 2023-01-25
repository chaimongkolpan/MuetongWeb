var stores = [];
var poColl = [];
var billingPo = [];
var billingColl = {};
var paymentType = [];
var extraType = [];
var paymentAccount = [];
var payTypeTransfer = 1;
var extraOther = 1;

/*********************** Add ******************************/
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
    $('#add_po_table').empty();
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
    $('#add_po_table').append(html);
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
    $('#add_po_table').empty();
    $('#po_check_all').prop('checked', false);
    $('#billing_no').val('');
    $('#billing_date').val('');
    $('#billing_files').val('');
    $('#billing_files').fileinput('clear');
});
/******************** End Add *****************************/
/********************** Edit ******************************/
/******************** End Add *****************************/
/*********************** Main *****************************/
$('#ProjectId').change(function () {
    bindFilter($('#ProjectId').val());
    $('#PrNo').val('ทั้งหมด');
    $('#PoNo').val('ทั้งหมด');
    $('#BillingNo').val('ทั้งหมด');
    $('#RequesterId').val(0);
});
function bindFilter(id) {
    var requesterUrl = baseUrl + 'requester/' + id;
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
    var prnoUrl = baseUrl + 'prno/' + id;
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
    var ponoUrl = baseUrl + 'pono/' + id;
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
    var billingnoUrl = baseUrl + 'billingno/' + id;
    var jqxhr = $.get(billingnoUrl)
        .done(function (response) {
            console.log(response);
            $('#BillingNo').empty();
            $('#BillingNo').append('<option selected>ทั้งหมด</option>');
            for (var i in response) {
                var item = response[i];
                var html = '<option>' + item + '</option>';
                $('#BillingNo').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
            $('#BillingNo').empty();
            $('#BillingNo').append('<option selected>ทั้งหมด</option>');
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
        })
        .fail(function (response) {
            console.log(response);
        });
}
function search() {
    var searchUrl = baseUrl + 'search';
    var request = new FormData();
    request.append("ProjectId", $('#ProjectId').val());
    request.append("PrNo", $('#PrNo').val());
    request.append("PoNo", $('#PoNo').val());
    request.append("BillingNo", $('#BillingNo').val());
    request.append("RequesterId", $('#RequesterId').val());
    request.append("Status", $('#Status').val());
    request.append("NoPayment", $('#no_payment').val());
    request.append("NoReceipt", $('#no_receipt').val());
    request.append("NoInvoice", $('#no_invoice').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            billingColl = result;
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
$('#read_btn').click(function () {
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
$('#edit_approve').click(function () {
    var id = $('#edit_id').val();
    var sendUrl = baseUrl + 'sendapprove/' + id;
    $.ajax({
        type: "GET",
        url: sendUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#ActionBilling').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionBilling').modal('hide');
        }
    });
});
$('#edit_save_btn').click(function () {
    const input = document.getElementById('edit_files');
    var id = $('#edit_id').val();
    var updateUrl = baseUrl + 'update/' + id;
    var data = new FormData();
    data.append("BillingNo", $('#edit_billing_no').val());
    data.append("BillingDate", $('#edit_billing_date').val());
    data.append("PaymentDate", $('#edit_payment_date').val());
    data.append("PaymentType", $('#edit_payment_type').val());
    data.append("PaymentAccount", $('#edit_payment_account').val());
    data.append("Amount", floatValue($('#edit_amount').val()));
    data.append("Remark", $('#edit_remark').val());
    data.append("HasReceipt", $('#edit_has_receipt').prop('checked'));
    if ($('#edit_has_receipt').prop('checked')) data.append("ReceiptNo", $('#edit_receipt_no').val());
    data.append("HasExtra", $('#edit_has_extra').prop('checked'));
    if ($('#edit_has_extra').prop('checked')) {
        data.append("ExtraType", $('#edit_extra_type').val());
        data.append("ExtraOther", $('#edit_extra_other').val());
        data.append("ExtraAmount", floatValue($('#edit_extra_amount').val()));
    }
    data.append("HasInvoice", $('#edit_has_invoice').prop('checked'));
    if ($('#edit_has_invoice').prop('checked')) data.append("InvoiceNo", $('#edit_invoice_no').val());
    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    $.ajax({
        type: "POST",
        url: updateUrl,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            alert('บันทึกสำเร็จ');
            search();
            $('#ActionBilling').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionBilling').modal('hide');
        }
    });
});
function cancel(i, tab) {
    var bill = {};
    if (tab == 3)
        bill = billingColl.waiting[i];
    else if (tab == 2)
        bill = billingColl.waitingApprove[i];
    else if(tab == 4)
        bill = billingColl.waitingReceipt[i];
    else
        bill = billingColl.all[i];
    $('#cancel_id').val(bill.id);
    $('#cancel_remark').val('');
    $('#cancel_text').html('ท่านต้องการยกเลิกการวางบิล เลขที่ ' + bill.billingNo);
}
function read(i) {
    var bill = billingColl.cancel[i];
    $('#cancel_read_id').val(bill.id);
    $('#cancel_read_text').html('ท่านรับทราบการยกเลิกการวางบิล เลขที่ ' + bill.billingNo);
}
function show(i) {
    var bill = billingColl.all[i];
    $('#show_table').empty();
    var html = '';
    html += createRowEditTable(bill);
    $('#show_table').append(html);
}
$('#edit_payment_type').change(function () {
    if ($(this).val() == payTypeTransfer) {
        $('#edit_payment_account').prop('disabled', false);
    } else {
        $('#edit_payment_account').prop('disabled', 'disabled');
    }
});
$('#edit_extra_type').change(function () {
    checkExtraType($(this).val());
});
function checkExtraType(ext) {
    if (ext == extraOther) {
        $('#edit_extra_other').prop('disabled', false);
    } else {
        $('#edit_extra_other').prop('disabled', 'disabled');
    }
}
$('#edit_has_receipt').change(function () {
    if ($(this).prop('checked')) {
        $('#edit_receipt_no').prop('disabled', false);
    } else {
        $('#edit_receipt_no').prop('disabled', 'disabled');
    }
});
$('#edit_has_extra').change(function () {
    if ($(this).prop('checked')) {
        $('#edit_extra_type').prop('disabled', false);
        $('#edit_extra_amount').prop('disabled', false);
        checkExtraType($('#edit_extra_type').val())
    } else {
        $('#edit_extra_type').prop('disabled', 'disabled');
        $('#edit_extra_other').prop('disabled', 'disabled');
        $('#edit_extra_amount').prop('disabled', 'disabled');
    }
});
$('#edit_has_invoice').change(function () {
    if ($(this).prop('checked')) {
        $('#edit_invoice_no').prop('disabled', false);
    } else {
        $('#edit_invoice_no').prop('disabled', 'disabled');
    }
});
function bindEditData(bill) {
    $('#edit_billing_no').val(bill.billingNo);
    $('#edit_billing_date').val(dateValue(bill.billingDate));
    $('#edit_payment_date').val(dateValue(bill.paymentDate));
    $('#edit_payment_type').val(bill.paymentType);
    if (bill.paymentType == payTypeTransfer) {
        $('#edit_payment_account').prop('disabled', false);
        $('#edit_payment_account').val(bill.paymentAccountId);
    } else $('#edit_payment_account').prop('disabled', 'disabled');
    $('#edit_amount').val(floatFormat(bill.amount));
    $('#edit_remark').val(bill.remark);
    $('#edit_has_receipt').prop('checked', bill.hasReceipt);
    if (bill.hasReceipt) {
        $('#edit_receipt_no').prop('disabled', false);
        $('#edit_receipt_no').val(bill.receiptNo);
    } else {
        $('#edit_receipt_no').prop('disabled', 'disabled');
        $('#edit_receipt_no').val('');
    }
    $('#edit_has_extra').prop('checked', bill.hasExtra);
    if (bill.hasExtra) {
        $('#edit_extra_type').val(bill.extraType);
        $('#edit_extra_amount').prop('disabled', false);
        $('#edit_extra_amount').val(floatFormat(bill.extraCost));
        if (bill.extraType == extraOther) {
            $('#edit_extra_other').val(bill.extraOther);
            $('#edit_extra_other').prop('disabled', false);
        } else {
            $('#edit_extra_other').val('');
            $('#edit_extra_other').prop('disabled', 'disabled');
        }
    } else {
        $('#edit_extra_type').prop('disabled', 'disabled');
        $('#edit_extra_other').val('');
        $('#edit_extra_other').prop('disabled', 'disabled');
        $('#edit_extra_amount').val('0.00');
        $('#edit_extra_amount').prop('disabled', 'disabled');
    }
    $('#edit_has_invoice').prop('checked', bill.hasInvoice);
    if (bill.hasInvoice) {
        $('#edit_invoice_no').prop('disabled', false);
        $('#edit_invoice_no').val(bill.invoiceNo);
    } else {
        $('#edit_invoice_no').prop('disabled', 'disabled');
        $('#edit_invoice_no').val('');
    }
    $('#edit_files').val('');
    $('#edit_files').fileinput('clear');
}
function edit(i, tab) {
    var bill = {};
    if (tab == 2)
        bill = billingColl.waitingApprove[i];
    else
        bill = billingColl.waitingReceipt[i];
    var storeUrl = baseUrl + 'payment/' + bill.id;
    $('#edit_extra_type').empty();
    $('#edit_payment_account').empty();
    $('#edit_payment_type').empty();
    var jqxhr = $.get(storeUrl)
        .done(function (response) {
            console.log(response);
            paymentAccount = response.payments;
            paymentType = response.paymentType;
            extraType = response.extraType;
            for (var i in response.payments) {
                var item = response.payments[i];
                var html = '<option value="' + item.id + '">' + item.bank + ' ' + item.accountNo + '</option>';
                $('#edit_payment_account').append(html);
            }
            for (var i in response.paymentType) {
                var item = response.paymentType[i];
                if (item.name == "โอนเงิน") payTypeTransfer = item.id;
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#edit_payment_type').append(html);
            }
            for (var i in response.extraType) {
                var item = response.extraType[i];
                if (item.name == "อื่นๆ") extraOther = item.id;
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#edit_extra_type').append(html);
            }
            bindEditData(bill);
            $('#edit_id').val(bill.id);
            $('#edit_table').empty();
            var html = '';
            html += createRowEditTable(bill);
            $('#edit_table').append(html);
        })
        .fail(function (response) {
            console.log(response);
            bindEditData(bill);
            $('#edit_id').val(bill.id);
            $('#edit_table').empty();
            var html = '';
            html += createRowEditTable(bill);
            $('#edit_table').append(html);
        });
}
function createRowEditTable(bill) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>1</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.paymentDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentTypeName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentAccount + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.amount) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.extraCost) + '</td>';
    if (bill.isPay) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasReceipt) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.receiptNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasInvoice) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.invoiceNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.approverName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.status + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowApproveTable(bill, i) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit(' + i + ',2)" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionBilling">view_list</span></td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel(' + i + ',2)" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.paymentDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentTypeName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentAccount + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.amount) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.extraCost) + '</td>';
    if (bill.isPay) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasReceipt) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.receiptNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasInvoice) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.invoiceNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.approverName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.status + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowCancelTable(bill, i) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="read(' + i + ')" style="color:#A42206;"  data-bs-toggle="modal" data-bs-target="#ActionApproveCancel">assignment_late</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.paymentDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentTypeName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentAccount + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.amount) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.extraCost) + '</td>';
    if (bill.isPay) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasReceipt) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.receiptNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasInvoice) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.invoiceNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.approverName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.status + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowTable(bill, i, tab) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionBilling">view_list</span></td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel(' + i + ',' + tab + ')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowFullTable(bill, i, tab) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit(' + i + ',' + tab + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionBilling">view_list</span></td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel(' + i + ',' + tab + ')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.paymentDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentTypeName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentAccount + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.amount) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.extraCost) + '</td>';
    if (bill.isPay) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasReceipt) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.receiptNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasInvoice) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.invoiceNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.approverName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.status + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowAllTable(bill, i, tab) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="show(' + i + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionAll">view_list</span></td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel(' + i + ',' + tab + ')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.billingNo + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.billingDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + dateFormat(bill.paymentDate) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentTypeName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.paymentAccount + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.amount) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + floatFormat(bill.extraCost) + '</td>';
    if (bill.isPay) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasReceipt) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.receiptNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    if (bill.hasInvoice) {
        html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td>';
        html += '<td class="bgpc" ' + rowspan + '>' + bill.invoiceNo + '</td>';
    } else {
        html += '<td class="bgpc" ' + rowspan + '></td><td class="bgpc" ' + rowspan + '></td>';
    }
    html += '<td class="bgpc" ' + rowspan + '>' + bill.requesterName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.approverName + '</td>';
    html += '<td class="bgpc" ' + rowspan + '>' + bill.status + '</td>';
    if (bill.details.length > 0) {
        var po = bill.details[0];
        html += poInner(po, null);
        if (po.details.length > 0) {
            var detail = po.details[0];
            html += prInner(detail, 0);
            html += '</tr>';
            for (var j = 1; j < po.details.length; j++) {
                var detail = po.details[j];
                html += '<tr>';
                html += prInner(detail, j);
                html += '</tr>';
            }
        } else {
            html += '<td colspan="20" class="bgpr"></td>';
            html += '</tr>';
        }
        for (var k = 1; k < bill.details.length; k++) {
            var po = bill.details[k];
            html += '<tr>';
            html += poInner(po, null);
            if (po.details.length > 0) {
                var detail = po.details[0];
                html += prInner(detail, 0);
                html += '</tr>';
                for (var j = 1; j < po.details.length; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
                    html += prInner(detail, j);
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
        }
    } else {
        html += '<td colspan="19" class="bgpo"></td>';
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function createRowPoTable(po, i) {
    var html = '';
    html += '<tr>';
    html += poInner(po, i);
    if (po.details.length > 0) {
        var detail = po.details[0];
        html += prInner(detail, 0);
        html += '</tr>';
        for (var j = 1; j < po.details.length; j++) {
            var detail = po.details[j];
            html += '<tr>';
            html += prInner(detail, j);
            html += '</tr>';
        }
    } else {
        html += '<td colspan="20" class="bgpr"></td>';
        html += '</tr>';
    }
    return html;
}
function poInner(po, i) {
    var html = '';
    var len = po.details.length;
    var rowspan = '';
    if (len > 1) {
        rowspan = 'rowspan="' + len + '"';
    }
    if (i != undefined && i != null) html += '<td class="bgpo" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.poNo + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.storeName + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.storeAddress + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.storePhoneNo + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.storeTaxNo + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.creditType + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.beforeDay + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.contractNo + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.dateSpecific) + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.afterDay + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.paymentType + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccount + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.paymentAccountType + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.paymentDate) + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.billingReceiveType + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.receiptReceiveType + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + dateFormat(po.planTransferDate) + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.requesterName + '</td>';
    html += '<td class="bgpo" ' + rowspan + '>' + po.approverName + '</td>';
    return html;
}
function prInner(detail, j) {
    var html = '';
    html += '<td class="bgpr">' + detail.projectName + '</td>';
    html += '<td class="bgpr">' + detail.prNo + '</td>';
    html += '<td class="bgpr">' + dateFormat(detail.createDate) + '</td>';
    html += '<td class="bgpr">' + detail.requesterName + '</td>';
    html += '<td class="bgpr">' + detail.approverName + '</td>';
    if (detail.isAdvancePay) {
        html += '<td class="bgpr"><span class="material-symbols-outlined" style="color:#000;">done</span></td><td class="bgpr">' + detail.contractorName + '</td>';
    } else {
        html += '<td class="bgpr"></td><td class="bgpr"></td>';
    }
    html += '<td class="bgpr">' + (parseInt(j) + 1) + '</td>';
    html += '<td class="bgpr">' + detail.name + '</td>';
    html += '<td class="bgpr">' + (detail.id != null ? detail.code : detail.additionalCode) + '</td>';
    html += '<td class="bgpr">' + (detail.id != null ? detail.quantity : 1) + '</td>';
    html += '<td class="bgpr">' + detail.unit + '</td>';
    html += '<td class="bgpr">' + detail.pricePerUnit + '</td>';
    html += '<td class="bgpr">' + detail.discount + '</td>';
    html += '<td class="bgpr">' + detail.vat + '</td>';
    html += '<td class="bgpr">' + detail.wht + '</td>';
    html += '<td class="bgpr">' + detail.total + '</td>';
    html += '<td class="bgpr">' + dateFormat(detail.useDate) + '</td>';
    html += '<td class="bgpr">' + (detail.id != null ? detail.remark : detail.additionalOtherCode) + '</td>';
    html += '<td class="bgpr">' + detail.status + '</td>';
    return html;
}
function createTable() {
    console.log(billingColl);
    $('#all_table').empty();
    $('#approve_table').empty();
    $('#payment_table').empty();
    $('#receipt_table').empty();
    $('#po_table').empty();
    $('#cancel_table').empty();
    if (billingColl.all != null) {
        var html = '';
        for (var i in billingColl.all) {
            html += createRowAllTable(billingColl.all[i], i, 1);
        }
        $('#all_table').append(html);
    }
    if (billingColl.waiting != null) {
        var html = '';
        for (var i in billingColl.waiting) {
            html += createRowFullTable(billingColl.waiting[i], i, 3);
        }
        $('#payment_table').append(html);
        if (billingColl.waitingCount > 0)
            $('#payment_tab').html('รอชำระเงิน&nbsp;<span class="badge bg-primary">' + billingColl.waitingCount + '</span>');
        else
            $('#payment_tab').html('รอชำระเงิน');
    }
    if (billingColl.waitingApprove != null) {
        var html = '';
        for (var i in billingColl.waitingApprove) {
            html += createRowApproveTable(billingColl.waitingApprove[i], i);
        }
        $('#approve_table').append(html);
        //if (billingColl.waitingApproveCount > 0)
        //    $('#waiting_tab').html('ตรวจสอบรับวางบิล&nbsp;<span class="badge bg-primary">' + billingColl.waitingApproveCount + '</span>');
        //else
        //    $('#waiting_tab').html('ตรวจสอบรับวางบิล');
    }
    if (billingColl.waitingReceipt != null) {
        var html = '';
        for (var i in billingColl.waitingReceipt) {
            html += createRowFullTable(billingColl.waitingReceipt[i], i, 4);
        }
        $('#receipt_table').append(html);
        if (billingColl.waitingReceiptCount > 0)
            $('#receipt_tab').html('รอชำระเงิน/ใบเสร็จ/ใบกำกับภาษี&nbsp;<span class="badge bg-primary">' + billingColl.waitingReceiptCount + '</span>');
        else
            $('#receipt_tab').html('รอชำระเงิน/ใบเสร็จ/ใบกำกับภาษี');
    }
    if (billingColl.po != null) {
        var html = '';
        for (var i in billingColl.po) {
            html += createRowPoTable(billingColl.po[i], i);
        }
        $('#po_table').append(html);
        if (billingColl.poCount > 0)
            $('#po_tab').html('สั่งซื้อ&nbsp;<span class="badge bg-primary">' + billingColl.poCount + '</span>');
        else
            $('#po_tab').html('สั่งซื้อ');
    }
    if (billingColl.cancel != null) {
        var html = '';
        for (var i in billingColl.cancel) {
            html += createRowCancelTable(billingColl.cancel[i], i);
        }
        $('#cancel_table').append(html);
        if (billingColl.unreadCancelCount > 0)
            $('#cancel_tab').html('ยกเลิก&nbsp;<span class="badge bg-danger">' + billingColl.unreadCancelCount + '</span>');
        else
            $('#cancel_tab').html('ยกเลิก');
    }
}
$('#search_btn').click(function () {
    search();
});
/******************** End Add *****************************/

$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    bindStore();
    search();
});