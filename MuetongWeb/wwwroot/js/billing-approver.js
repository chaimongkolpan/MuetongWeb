var stores = [];


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
    console.log(readUrl);
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
$('#approve_btn').click(function () {
    var id = $('#approve_id').val();
    var approveUrl = baseUrl + 'approve/' + id;
    $.ajax({
        type: "GET",
        url: approveUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#ActionConfirm').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionConfirm').modal('hide');
        }
    });
});
function cancel(i, tab) {
    var bill = {};
    if (tab == 3)
        bill = billingColl.waiting[i];
    else if (tab == 2)
        bill = billingColl.waitingApprove[i];
    else if (tab == 4)
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
function approve(i) {
    var bill = billingColl.waitingApprove[i];
    $('#approve_id').val(bill.id);
    $('#approve_text').html('ท่านต้องการยืนยันตรวจสอบวางบิล เลขที่ ' + bill.billingNo);
}
function edit(i, tab) {
    var bill = {};
    if(tab == 1)
        bill = billingColl.all[i];
    else if (tab == 2)
        bill = billingColl.waitingApprove[i];
    $('#edit_table').empty();
    var html = '';
    html += createRowEditTable(bill);
    $('#edit_table').append(html);
}
function createRowApproveTable(bill, i, tab) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="approve(' + i + ')" style="color:green;" data-bs-toggle="modal" data-bs-target="#ActionConfirm"> check_circle</span></td>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit(' + i + ',' + tab + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionAll">view_list</span></td>';
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
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" style="color:#A42206;" onclick="read(' + i + ')" data-bs-toggle="modal" data-bs-target="#ActionApproveCancel">assignment_late</span>';
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
function createRowFullTable(bill, i, tab) {
    var html = '';
    var rowspan = '';
    if (bill.rowspan > 1) {
        rowspan = 'rowspan="' + bill.rowspan + '"';
    }
    html += '<tr>';
    html += '<td class="bgpc" ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    html += '<td class="bgpc" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit(' + i + ',' + tab + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionAll">view_list</span></td>';
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
            html += createRowFullTable(billingColl.all[i], i, 1);
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
            html += createRowApproveTable(billingColl.waitingApprove[i], i, 2);
        }
        $('#approve_table').append(html);
        if (billingColl.waitingApproveCount > 0)
            $('#waiting_tab').html('รับวางบิล&nbsp;<span class="badge bg-primary">' + billingColl.waitingApproveCount + '</span>');
        else
            $('#waiting_tab').html('รับวางบิล');
    }
    if (billingColl.waitingReceipt != null) {
        var html = '';
        for (var i in billingColl.waitingReceipt) {
            html += createRowFullTable(billingColl.waitingReceipt[i], i, 4);
        }
        $('#receipt_table').append(html);
        if (billingColl.waitingReceiptCount > 0)
            $('#receipt_tab').html('รอใบเสร็จรับเงิน/ใบกำกับภาษี&nbsp;<span class="badge bg-primary">' + billingColl.waitingReceiptCount + '</span>');
        else
            $('#receipt_tab').html('รอใบเสร็จรับเงิน/ใบกำกับภาษี');
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