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
var prOtherEdit = {
    poDetailId: 0,
    pricePerUnit: 0,
    discount: 0,
    isVat: false,
    vat: 0,
    isWht: false,
    wht: 0,
    total: 0
};
var prDetails = [];
var prEditDetails = [];
var insertDetailList = [];
var insertEditDetailList = [];
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
/*************************** Main **********************************/
function checkEditStatus(status) {
    if (status == "รอสินค้าจัดส่ง" || status == "จัดส่งสำเร็จ" || status == "ยกเลิก")
        return true;
    return false;
}
$('#ProjectId').change(function () {
    bindFilter($('#ProjectId').val());
    $('#PrNo').val('ทั้งหมด');
    $('#PoNo').val('ทั้งหมด');
    $('#RequesterId').val(0);
});
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
            if (checkEditStatus(po.status)) {
                html += '<td class="bgpo" ' + rowspan + '></td><td class="bgpo" ' + rowspan + '></td>';
            } else {
                html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_po(' + i + ',1)" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionPurchase">view_list</span></td>';
                html += '<td class="bgpo" ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_po(' + i + ',1)" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
            }
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
            if (len > 0) {
                var detail = po.details[0];
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
                html += '<td class="bgpr">' + 1 + '</td>';
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
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
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
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
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
            if (len > 0) {
                var detail = po.details[0];
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
                html += '<td class="bgpr">' + 1 + '</td>';
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
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
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
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
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
            if (len > 0) {
                var detail = po.details[0];
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
                html += '<td class="bgpr">' + 1 + '</td>';
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
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = po.details[j];
                    html += '<tr>';
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
                    html += '</tr>';
                }
            } else {
                html += '<td colspan="20" class="bgpr"></td>';
                html += '</tr>';
            }
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
    $('#EditProductId').empty();
    var jqxhr = $.get(productUrl)
        .done(function (response) {
            console.log(response);
            products = response;
            var html = '<option value="0">ทั้งหมด</option>';
            $('#AddProductId').append(html);
            $('#EditProductId').append(html);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#AddProductId').append(html);
                $('#EditProductId').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindStore() {
    var storeUrl = baseUrl + 'store';
    $('#add_store').empty();
    $('#edit_store').empty();
    var jqxhr = $.get(storeUrl)
        .done(function (response) {
            console.log(response);
            stores = response.stores;
            for (var i in response.stores) {
                var item = response.stores[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_store').append(html);
                $('#edit_store').append(html);
            }
            bindPayment($('#add_store').val());
            bindPaymentEdit($('#edit_store').val());
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindReceiveType() {
    var receiveUrl = baseUrl + 'receive';
    $('#add_billing_receive').empty();
    $('#add_receipt_receive').empty();
    $('#edit_billing_receive').empty();
    $('#edit_receipt_receive').empty();
    var jqxhr = $.get(receiveUrl)
        .done(function (response) {
            console.log(response);
            for (var i in response.billing) {
                var item = response.billing[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_billing_receive').append(html);
                $('#edit_billing_receive').append(html);
            }
            for (var i in response.receipt) {
                var item = response.receipt[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_receipt_receive').append(html);
                $('#edit_receipt_receive').append(html);
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
    $('#edit_payment_type').empty();
    $('#edit_credit_type').empty();
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
                $('#edit_credit_type').append(html);
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
                $('#edit_payment_type').append(html);
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
function bindPaymentEdit(id) {
    console.log(stores, id);
    var store = stores.find(x => x.id == id);
    $('#edit_payment_account').empty();
    for (var i in store.payments) {
        var item = store.payments[i];
        var html = '<option value="' + item.id + '">' + item.bank + ' ' + item.accountNo + '</option>';
        $('#edit_payment_account').append(html);
    }
    $('#edit_address').val(store.address);
    $('#edit_mobile_no').val(store.phoneNo);
    $('#edit_tax_no').val(store.taxNo);
}

function get_detail_check_id(id) {
    var arr = id.split('__');
    if (arr.length > 1)
        return parseInt(arr[1]);
    return -1;
}

/************************* End Main ********************************/

/*************************** Add ***********************************/

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
        html += '<td><span class="material-symbols-outlined" onclick="showFileAdd(' + i + ')" data-bs-toggle="modal" data-bs-target="#RefOrder">description</span></td>';
        html += '<td><span class="material-symbols-outlined" onclick="showApproveFileAdd(' + i + ')" data-bs-toggle="modal" data-bs-target="#RefApproverOrder">description</span></td>';
        html += '</tr>';
    }
    $('#add_pr_table').append(html);
}
function showFileAdd(i) {
    var pr = prDetails.details[i];
    console.log(pr);
    $('#pr_files_pane').empty();
    $('#pr_files_pane').append('<div class="file-loading"><input id="pr_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#" readonly="readonly"></div>');
    $('#pr_files').fileinput({
        language: "th",
        showUpload: false,
        initialPreview: pr.filePreviews,
        initialPreviewConfig: pr.files
    });
}
function showApproveFileAdd(i) {
    var pr = prDetails.details[i];
    console.log(pr);
    $('#pr_approve_files_pane').empty();
    $('#pr_approve_files_pane').append('<div class="file-loading"><input id="pr_approve_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#" readonly="readonly"></div>');
    $('#pr_approve_files').fileinput({
        language: "th",
        showUpload: false,
        initialPreview: pr.approveFilePreviews,
        initialPreviewConfig: pr.approveFiles
    });
}
function showFileEdit(i) {
    var pr = prEditDetails.details[i];
    console.log(pr);
    $('#pr_files_pane').empty();
    $('#pr_files_pane').append('<div class="file-loading"><input id="pr_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#" readonly="readonly"></div>');
    $('#pr_files').fileinput({
        language: "th",
        showUpload: false,
        initialPreview: pr.filePreviews,
        initialPreviewConfig: pr.files
    });
}
function showApproveFileEdit(i) {
    var pr = prEditDetails.details[i];
    console.log(pr);
    $('#pr_approve_files_pane').empty();
    $('#pr_approve_files_pane').append('<div class="file-loading"><input id="pr_approve_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#" readonly="readonly"></div>');
    $('#pr_approve_files').fileinput({
        language: "th",
        showUpload: false,
        initialPreview: pr.approveFilePreviews,
        initialPreviewConfig: pr.approveFiles
    });
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
$('#add_detail_add_btn').click(function () {
    $('input[id^="detail_check__"]').each(function (i, obj) {
        if ($(obj).prop('checked') && check_insert(i))
            insertDetail(i);
    });
    createDetailList();
});
$('#add_btn').click(function () {
    $('#AddProjectId').val(0);
    $('#AddPrNo').val('');
    $('#AddProductId').val(0);
    $('#po_detail_table').empty();
    insertDetailList = [];
    $('#add_non_credit_day').val('');
    $('#add_bg_contract_no').val('');
    $('#add_bg_date').val('');
    $('#add_port_check_no').val('');
    $('#add_port_check_date').val('');
    $('#add_after_transfer_day').val('');
    $('#add_after_billing_day').val('');
    $('#add_payment_date').val('');
    $('#add_plan_transfer_date').val('');
    $('#add_addition_check').prop('checked', false);
    $('#add_addition_other').val('');
    $('#po_wht_value').val(3);
    $('#add_files').val('');
    $('#add_files').fileinput('clear');
    calAll();
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
$('#add_store').change(function () {
    bindPayment($('#add_store').val());
});
$('#detail_check_all').change(function () {
    $('input[id^="detail_check__"]').prop('checked', $('#detail_check_all').prop('checked'));
});
function check_insert(i) {
    if (insertDetailList.length == 0) return true;
    return insertDetailList.findIndex(item => item.id == prDetails.details[i].id) < 0;
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
    location.href = "#po_detail_table";
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
$('#po_wht_value').focusout(function () {
    $('#po_foot_wht_text').html('หัก ณ ที่จ่าย ' + $('#po_wht_value').val() + '%');
    for (var i in insertDetailList) calRecord(i);
    if ($('#add_addition_check').prop('checked')) calRecordOther(insertDetailList.length);
});
$('#po_wht_value').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        $('#po_foot_wht_text').html('หัก ณ ที่จ่าย ' + $('#po_wht_value').val() + '%');
        for (var i in insertDetailList) calRecord(i);
        if ($('#add_addition_check').prop('checked')) calRecordOther(insertDetailList.length);
    }
});

/************************* End Add *********************************/

/*************************** Edit **********************************/

function createTablePrEdit() {
    console.log(prEditDetails.details);
    $('#edit_pr_table').empty();
    var html = '';
    for (var i in prEditDetails.details) {
        var pr = prEditDetails.details[i];
        html += '<tr>';
        html += '<td><div class="custom-control custom-checkbox"><input type="checkbox" class="custom-control-input" id="edit_detail_check__' + pr.id + '"></div></td>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + pr.projectName + '</td>';
        html += '<td>' + pr.prNo + '</td>';
        html += '<td>' + pr.requesterName + '</td>';
        html += '<td>' + pr.productName + '</td>';
        html += '<td>' + pr.quantity + '</td>';
        html += '<td>' + pr.unit + '</td>';
        html += '<td>' + dateFormat(pr.useDate) + '</td>';
        html += '<td>' + pr.projectCode + '</td>';
        html += '<td>' + pr.status + '</td>';
        html += '<td>' + pr.remark + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="showFileEdit(' + i + ')" data-bs-toggle="modal" data-bs-target="#RefOrder">description</span></td>';
        html += '<td><span class="material-symbols-outlined" onclick="showApproveFileEdit(' + i + ')" data-bs-toggle="modal" data-bs-target="#RefApproverOrder">description</span></td>';
        html += '</tr>';
    }
    $('#edit_pr_table').append(html);
}
function searchPrEdit() {
    var searchUrl = baseUrl + 'searchpr';
    var request = new FormData();
    request.append("ProjectId", $('#EditProjectId').val());
    request.append("PrNo", $('#EditPrNo').val());
    request.append("ProductId", $('#EditProductId').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            prEditDetails = result;
            createTablePrEdit();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}
$('#edit_search').click(function () {
    searchPrEdit()
});
$('#edit_detail_add_btn').click(function () {
    $('input[id^="edit_detail_check__"]').each(function (i, obj) {
        if ($(obj).prop('checked') && edit_check_insert(i))
            editInsertDetail(i);
    });
    editCreateDetailList();
});
$('#edit_store').change(function () {
    bindPaymentEdit($('#edit_store').val());
});
$('#edit_detail_check_all').change(function () {
    $('input[id^="edit_detail_check__"]').prop('checked', $('#edit_detail_check_all').prop('checked'));
});
function edit_check_insert(i) {
    if (insertEditDetailList.length == 0) return true;
    return insertEditDetailList.findIndex(item => item.id == prEditDetails.details[i].id) < 0;
}
function editInsertDetail(i) {
    var tmp = Object.assign({}, prEditDetails.details[i]);
    tmp.pricePerUnit = 0;
    tmp.discount = 0;
    tmp.isVat = false;
    tmp.vat = 0;
    tmp.isWht = false;
    tmp.wht = 0;
    tmp.total = 0;
    insertEditDetailList.push(tmp);
}
function ediDeleteDetailList(i) {
    insertEditDetailList.splice(i, 1);
    editCreateDetailList();
}
function editCreateDetailList() {
    location.href = "#edit_po_detail_table";
    $('#edit_po_detail_table').empty();
    var html = '';
    for (var i in insertEditDetailList) {
        var detail = insertEditDetailList[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="ediDeleteDetailList(' + i + ')" style="color:#A42206;">playlist_remove</span></td>';
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
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.pricePerUnit) + '" id="edit_po_detail_price__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.discount) + '" id="edit_po_detail_discount__' + i + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="edit_po_detail_vat_check__' + i + '"' + (detail.isVat ? 'checked' : '') + '><br /><label class="custom-control-label" id="edit_po_detail_vat_text__' + i + '">' + floatFormat(detail.vat) + '</label></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="edit_po_detail_wht_check__' + i + '"' + (detail.isWht ? 'checked' : '') + '><br /><label class="custom-control-label" id="edit_po_detail_wht_text__' + i + '">' + floatFormat(detail.wht) + '</label></td>';
        html += '<td><label class="custom-control-label" id="edit_po_detail_total__' + i + '">' + floatFormat(detail.total) + '</label></td>';
        html += '</tr>';
        calEditRecord(i);
    }
    if ($('#edit_addition_check').prop('checked')) {
        var name = 'ค่าขนส่ง';
        if ($('#edit_addition').val() == 'อื่นๆ') name = $('#edit_addition_other').val();
        var detail = prOtherEdit;
        html += '<tr>';
        html += '<td>' + (insertEditDetailList.length + 1) + '</td>';
        html += '<td colspan="4"></td>';
        html += '<td>' + name + '</td>';
        html += '<td colspan="6"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.pricePerUnit) + '" id="edit_po_detail_price__' + insertEditDetailList.length + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="text" class="form-control" value="' + floatFormat(detail.discount) + '" id="edit_po_detail_discount__' + insertEditDetailList.length + '" aria-describedby="basic-addon1" style="width: 120px;"></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="edit_po_detail_vat_check__' + insertEditDetailList.length + '"' + (detail.isVat ? 'checked' : '') + '><br /><label class="custom-control-label" id="edit_po_detail_vat_text__' + insertEditDetailList.length + '">' + floatFormat(detail.vat) + '</label></td>';
        html += '<td><input type="checkbox" class="custom-control-input" id="edit_po_detail_wht_check__' + insertEditDetailList.length + '"' + (detail.isWht ? 'checked' : '') + '><br /><label class="custom-control-label" id="edit_po_detail_wht_text__' + insertEditDetailList.length + '">' + floatFormat(detail.wht) + '</label></td>';
        html += '<td><label class="custom-control-label" id="edit_po_detail_total__' + insertEditDetailList.length + '">' + floatFormat(detail.total) + '</label></td>';
        html += '</tr>';
    }
    $('#edit_po_detail_table').append(html);
    $('input[id^="edit_po_detail_price__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertEditDetailList.length) {
            prOtherEdit.pricePerUnit = floatValue($(this).val());
            calEditRecordOther(i);
        } else {
            insertEditDetailList[i].pricePerUnit = floatValue($(this).val());
            calEditRecord(i);
        }
        $(this).val(floatFormat($(this).val()));
    });
    $('input[id^="edit_po_detail_price__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            if (i == insertEditDetailList.length) {
                prOtherEdit.pricePerUnit = floatValue($(this).val());
                calEditRecordOther(i);
            } else {
                insertEditDetailList[i].pricePerUnit = floatValue($(this).val());
                calEditRecord(i);
            }
            $(this).val(floatFormat($(this).val()));
        }
    });
    $('input[id^="edit_po_detail_discount__"]').focusout(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertEditDetailList.length) {
            prOtherEdit.discount = floatValue($(this).val());
            calEditRecordOther(i);
        } else {
            insertEditDetailList[i].discount = floatValue($(this).val());
            calEditRecord(i);
        }
        $(this).val(floatFormat($(this).val()));
    });
    $('input[id^="edit_po_detail_discount__"]').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            var i = get_detail_check_id($(this)[0].id);
            if (i == insertEditDetailList.length) {
                prOtherEdit.discount = floatValue($(this).val());
                calEditRecordOther(i);
            } else {
                insertEditDetailList[i].discount = floatValue($(this).val());
                calEditRecord(i);
            }
            $(this).val(floatFormat($(this).val()));
        }
    });
    $('input[id^="edit_po_detail_vat_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertEditDetailList.length) {
            prOtherEdit.isVat = $(this).prop('checked');
            calEditRecordOther(i);
        } else {
            insertEditDetailList[i].isVat = $(this).prop('checked');
            calEditRecord(i);
        }
    });
    $('input[id^="edit_po_detail_wht_check__"]').change(function () {
        var i = get_detail_check_id($(this)[0].id);
        if (i == insertEditDetailList.length) {
            prOtherEdit.isWht = $(this).prop('checked');
            calEditRecordOther(i);
        } else {
            insertEditDetailList[i].isWht = $(this).prop('checked');
            calEditRecord(i);
        }
    });
}
$('#edit_addition_other').focusout(function () {
    editCreateDetailList();
});
$('#edit_addition_other').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        editCreateDetailList();
    }
});
$('#edit_po_vat').change(function () {
    $('input[id^="edit_po_detail_vat_check__"]').each(function (i, obj) {
        $(obj).prop('checked', $('#edit_po_vat').prop('checked'));
        var j = get_detail_check_id($(obj)[0].id);
        insertEditDetailList[j].isVat = $(obj).prop('checked');
        calEditRecord(j);
    });
});
$('#edit_po_wht').change(function () {
    $('input[id^="edit_po_detail_wht_check__"]').each(function (i, obj) {
        $(obj).prop('checked', $('#edit_po_wht').prop('checked'));
        var j = get_detail_check_id($(obj)[0].id);
        insertEditDetailList[j].isWht = $(obj).prop('checked');
        calEditRecord(j);
    });
});
function calEditRecordOther(i) {
    var vat = 0;
    var wht = 0;
    var total = prOtherEdit.pricePerUnit - prOtherEdit.discount;
    if (prOtherEdit.isVat) vat = vatRate;
    $('#edit_po_detail_vat_text__' + i).html(floatFormat(total * vat));
    prOtherEdit.vat = floatValue(total * vat);
    var tmp = floatValue($('#edit_po_wht_value').val());
    if (prOtherEdit.isWht) wht = tmp / 100;
    $('#edit_po_detail_wht_text__' + i).html(floatFormat(total * wht));
    prOtherEdit.wht = floatValue(total * wht);
    var grandtotal = total + prOtherEdit.vat - prOtherEdit.wht;
    $('#edit_po_detail_total__' + i).html(floatFormat(grandtotal));
    prOtherEdit.total = grandtotal;
    calEditAll();
}
function calEditRecord(i) {
    var vat = 0;
    var wht = 0;
    var subTotal = insertEditDetailList[i].pricePerUnit * insertEditDetailList[i].quantity;
    var total = subTotal - insertEditDetailList[i].discount;
    if (insertEditDetailList[i].isVat) vat = vatRate;
    $('#edit_po_detail_vat_text__' + i).html(floatFormat(total * vat));
    insertEditDetailList[i].vat = floatValue(total * vat);
    var tmp = floatValue($('#edit_po_wht_value').val());
    if (insertEditDetailList[i].isWht) wht = tmp / 100;
    $('#edit_po_detail_wht_text__' + i).html(floatFormat(total * wht));
    insertEditDetailList[i].wht = floatValue(total * wht);
    var grandtotal = total + insertEditDetailList[i].vat - insertEditDetailList[i].wht;
    $('#edit_po_detail_total__' + i).html(floatFormat(grandtotal));
    insertEditDetailList[i].total = grandtotal;
    calEditAll();
}
function calEditAll() {
    var subtotal = 0;
    var discount = 0;
    var vat = 0;
    var total = 0;
    var wht = 0;
    var grandtotal = 0;
    for (var i in insertEditDetailList) {
        var tmp = insertEditDetailList[i];
        subtotal += tmp.total;
        discount += tmp.discount;
        vat += tmp.vat;
        wht += tmp.wht;
        total += tmp.total - tmp.discount;
        grandtotal += (tmp.total - tmp.discount) + tmp.vat - tmp.wht;
    }
    if ($('#edit_addition_check').prop('checked')) {
        subtotal += prOtherEdit.pricePerUnit;
        discount += prOtherEdit.discount;
        vat += prOtherEdit.vat;
        wht += prOtherEdit.wht;
        total += prOtherEdit.pricePerUnit - prOtherEdit.discount;
        grandtotal += (prOtherEdit.pricePerUnit - prOtherEdit.discount) + prOtherEdit.vat - prOtherEdit.wht;
    }
    $('#edit_po_foot_subtotal').html(floatFormat(subtotal));
    $('#edit_po_foot_discount').html(floatFormat(discount));
    $('#edit_po_foot_vat').html(floatFormat(vat));
    $('#edit_po_foot_total').html(floatFormat(total));
    $('#edit_po_foot_wht').html(floatFormat(wht));
    $('#edit_po_foot_grandtotal').html(floatFormat(grandtotal));
}
function edit_hide_credit_type() {
    $('#edit_non_credit_day_pane').hide();
    $('#edit_bg_contract_no_pane').hide();
    $('#edit_bg_date_pane').hide();
    $('#edit_port_check_no_pane').hide();
    $('#edit_port_check_date_pane').hide();
    $('#edit_after_transfer_day_pane').hide();
    $('#edit_after_billing_day_pane').hide();
    $('#edit_blank_pane').hide();
}
$('#edit_credit_type').change(function () {
    if ($(this).val() == creditType.non) {
        edit_hide_credit_type();
        $('#edit_non_credit_day_pane').show();
    } else if ($(this).val() == creditType.bg) {
        edit_hide_credit_type();
        $('#edit_bg_contract_no_pane').show();
        $('#edit_bg_date_pane').show();
        $('#edit_blank_pane').show();
    } else if ($(this).val() == creditType.check) {
        edit_hide_credit_type();
        $('#edit_port_check_no_pane').show();
        $('#edit_port_check_date_pane').show();
        $('#edit_blank_pane').show();
    } else if ($(this).val() == creditType.afTransfer) {
        edit_hide_credit_type();
        $('#edit_after_transfer_day_pane').show();
    } else if ($(this).val() == creditType.afBilling) {
        edit_hide_credit_type();
        $('#edit_after_billing_day_pane').show();
    } else {
        edit_hide_credit_type();
    }
});
$('#edit_payment_type').change(function () {
    if ($(this).val() == paymentType.transfer) {
        $('#edit_payment_account').prop('disabled', false);
    } else {
        $('#edit_payment_account').prop('disabled', 'disabled');
    }
});
$('#edit_addition_check').change(function () {
    if ($('#edit_addition_check').prop('checked')) {
        $('#edit_addition').prop('disabled', false);
        $('#edit_addition').val('ค่าขนส่ง');
        $('#edit_addition_other').prop('disabled', 'disabled');
        $('#edit_addition_other').val('');
    } else {
        $('#edit_addition').prop('disabled', 'disabled');
        $('#edit_addition').val('ค่าขนส่ง');
        $('#edit_addition_other').prop('disabled', 'disabled');
        $('#edit_addition_other').val('');
    }
    editCreateDetailList();
});
$('#edit_addition').change(function () {
    $('#edit_addition_other').val('');
    if ($(this).val() == 'อื่นๆ') {
        $('#edit_addition_other').prop('disabled', false);
    } else {
        $('#edit_addition_other').prop('disabled', 'disabled');
    }
    editCreateDetailList();
});
$('#edit_po_wht_value').focusout(function () {
    $('#edit_po_foot_wht_text').html('หัก ณ ที่จ่าย ' + $('#edit_po_wht_value').val() + '%');
    for (var i in insertEditDetailList) calEditRecord(i);
    if ($('#edit_addition_check').prop('checked')) calEditRecordOther(insertEditDetailList.length);
});
$('#edit_po_wht_value').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        $('#edit_po_foot_wht_text').html('หัก ณ ที่จ่าย ' + $('#edit_po_wht_value').val() + '%');
        for (var i in insertEditDetailList) calEditRecord(i);
        if ($('#edit_addition_check').prop('checked')) calEditRecordOther(insertEditDetailList.length);
    }
});
$('#edit_btn').click(function () {
    const input = document.getElementById('edit_files');
    var updateUrl = baseUrl + 'update/' + $('#edit_id').val();
    var data = new FormData();
    data.append("StoreId", $('#edit_store').val());
    data.append("CreditType", $('#edit_credit_type').val());
    if ($('#edit_credit_type').val() == creditType.non) {
        data.append("DateValue", $('#edit_non_credit_day').val());
    } else if ($('#edit_credit_type').val() == creditType.bg) {
        data.append("BgContractNo", $('#edit_bg_contract_no').val());
        data.append("DateSpecific", $('#edit_bg_date').val());
    } else if ($('#edit_credit_type').val() == creditType.check) {
        data.append("ChequeNo", $('#edit_port_check_no').val());
        data.append("DateSpecific", $('#edit_port_check_date').val());
    } else if ($('#edit_credit_type').val() == creditType.afTransfer) {
        data.append("DateValue", $('#edit_after_transfer_day').val());
    } else if ($('#edit_credit_type').val() == creditType.afBilling) {
        data.append("DateValue", $('#edit_after_billing_day').val());
    }
    data.append("PaymentType", $('#edit_payment_type').val());
    data.append("PaymentAccountId", $('#edit_payment_account').val());
    data.append("PayDate", $('#edit_payment_date').val());
    data.append("ReceiveBillingTypeId", $('#edit_billing_receive').val());
    data.append("ReceiveReceiptTypeId", $('#edit_receipt_receive').val());
    data.append("PlanTransferDate", $('#edit_plan_transfer_date').val());
    data.append("HasAdditional", $('#edit_addition_check').prop('checked'));
    data.append("AdditionalType", $('#edit_addition').val());
    data.append("AdditionalOther", $('#edit_addition_other').val());
    data.append("VatRate", vatRate);
    data.append("WhtRate", $('#edit_po_wht_value').val() / 100);
    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    var details = [];
    for (var i = 0; i < insertEditDetailList.length; i++) {
        var detail = {
            Id: insertEditDetailList[i].poDetailId,
            PrDetailId: insertEditDetailList[i].id,
            PricePerUnit: insertEditDetailList[i].pricePerUnit,
            Discount: insertEditDetailList[i].discount,
            IsVat: insertEditDetailList[i].isVat,
            IsWht: insertEditDetailList[i].isWht
        };
        details.push(detail);
    }
    if ($('#edit_addition_check').prop('checked')) {
        var other = {
            Id: prOtherEdit.poDetailId,
            PricePerUnit: prOtherEdit.pricePerUnit,
            Discount: prOtherEdit.discount,
            IsVat: prOtherEdit.isVat,
            IsWht: prOtherEdit.isWht
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
        url: updateUrl,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            console.log(result);
            search();
            $('#ActionPurchase').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
            $('#ActionPurchase').modal('hide');
        }
    });
});
function edit_po(index, tab) {
    if (tab == 1) {
        bindDataEdit(poColl.all[index]);
    } else if (tab == 2) {
        bindDataEdit(poColl.waiting[index]);
    } else {
        $('#edit_files').val('');
        $('#edit_files').fileinput('clear');
        $('#EditProjectId').val(0);
        $('#EditPrNo').val('');
        $('#EditProductId').val(0);
        $('#edit_po_detail_table').empty();
        insertEditDetailList = [];
        $('#edit_non_credit_day').val('');
        $('#edit_bg_contract_no').val('');
        $('#edit_bg_date').val('');
        $('#edit_port_check_no').val('');
        $('#edit_port_check_date').val('');
        $('#edit_after_transfer_day').val('');
        $('#edit_after_billing_day').val('');
        $('#edit_payment_date').val('');
        $('#edit_plan_transfer_date').val('');
        $('#edit_addition_check').prop('checked', false);
        $('#edit_addition_other').val('');
        $('#edit_po_wht_value').val(3);
        calEditAll();
    }
}
function bindDataEdit(po) {
    console.log(po);
    $('#edit_files_pane').empty();
    $('#edit_files_pane').append('<div class="file-loading"><input id="edit_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#"></div>');
    $('#edit_files').fileinput({
        language: "th",
        showUpload: false,
        initialPreview: po.filePreviews,
        initialPreviewConfig: po.files
    });

    $('#edit_id').val(po.id);
    $('#edit_po_wht_value').val(po.whtRate);
    $('#EditProjectId').val(0);
    $('#EditPrNo').val('');
    $('#EditProductId').val(0);
    $('#edit_po_detail_table').empty();
    $('#edit_store').val(po.storeId);
    $('#edit_address').val(po.storeAddress);
    $('#edit_mobile_no').val(po.storePhoneNo);
    $('#edit_tax_no').val(po.storeTaxNo);
    $('#edit_credit_type').val(po.creditTypeId);

    edit_hide_credit_type();
    if (po.creditTypeId == creditType.non) {
        $('#edit_non_credit_day_pane').show();
        $('#edit_non_credit_day').val(po.dateValue);
    } else if (po.creditTypeId == creditType.bg) {
        $('#edit_bg_contract_no_pane').show();
        $('#edit_bg_date_pane').show();
        $('#edit_blank_pane').show();
        $('#edit_bg_contract_no').val(po.contractNo);
        $('#edit_bg_date').val(dateValue(po.dateSpecific));
    } else if (po.creditTypeId == creditType.check) {
        $('#edit_port_check_no_pane').show();
        $('#edit_port_check_date_pane').show();
        $('#edit_blank_pane').show();
        $('#edit_port_check_no').val(po.chequeNo);
        $('#edit_port_check_date').val(dateValue(po.dateSpecific));
    } else if (po.creditTypeId == creditType.afTransfer) {
        $('#edit_after_transfer_day_pane').show();
        $('#edit_after_transfer_day').val(po.dateValue);
    } else if (po.creditTypeId == creditType.afBilling) {
        $('#edit_after_billing_day_pane').show();
        $('#edit_after_billing_day').val(po.dateValue);
    }
    $('#edit_payment_type').val(po.paymentTypeId);
    $('#edit_payment_account').val(po.paymentAccountId);
    $('#edit_billing_receive').val(po.billingReceiveTypeId);
    $('#edit_receipt_receive').val(po.receiptReceiveTypeId);
    $('#edit_payment_date').val(dateValue(po.paymentDate));
    $('#edit_plan_transfer_date').val(dateValue(po.planTransferDate));
    insertEditDetailList = [];
    $('#edit_addition_check').prop('checked', false);
    $('#edit_addition').val('ค่าขนส่ง');
    $('#edit_addition_other').val('');
    for (var i in po.details) {
        var detail = po.details[i];
        if (detail.productId != 0) {
            var tmp = {
                id: detail.id,
                poDetailId: detail.poDetailId,
                projectName: detail.projectName,
                prNo: detail.prNo,
                requesterName: detail.requesterName,
                productName: detail.name,
                useDate: detail.useDate,
                projectCode: detail.code,
                remark: detail.remark,
                status: detail.status,
                quantity: detail.quantity,
                unit: detail.unit,
                pricePerUnit: detail.pricePerUnit,
                discount: detail.discount,
                isVat: detail.vat != 0,
                vat: detail.vat,
                isWht: detail.wht != 0,
                wht: detail.wht,
                total: detail.total
            };
            insertEditDetailList.push(tmp);
        } else {
            prOtherEdit = {
                poDetailId: detail.poDetailId,
                pricePerUnit: detail.pricePerUnit,
                discount: detail.discount,
                isVat: detail.vat != 0,
                vat: detail.vat,
                isWht: detail.wht != 0,
                wht: detail.wht,
                total: detail.total
            };
            $('#edit_addition_check').prop('checked', true);
            $('#edit_addition').val(detail.additionalCode);
            if (detail.additionalCode == 'อื่นๆ') {
                $('#edit_addition_other').val(detail.additionalOtherCode);
                $('#edit_addition_other').prop('disabled', false);
            }
            else {
                $('#edit_addition_other').val('');
                $('#edit_addition_other').prop('disabled', 'disabled');
            }
        }
    }
    editCreateDetailList();
}

/************************* End Edit ********************************/

$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    bindProduct();
    bindStore();
    bindReceiveType();
    bindPaymentType();
    $('#add_files').fileinput({
        language: "th",
        showUpload: false,
    });
    search();
});