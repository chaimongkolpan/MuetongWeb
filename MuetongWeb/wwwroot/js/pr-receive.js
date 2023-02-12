var edit_detail = [];
var products = [];
var prColl = {};
var allNum = 0;
var waitNum = 0;
function bindFilter(projectId) {
    var requesterUrl = baseUrl + 'requesterComplete/' + projectId;
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
    var prnoUrl = baseUrl + 'prnoComplete/' + projectId;
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
}
function createReceiveTable(receives, all) {
    var html = '';
    html += '<tr>';
    html += '<td colspan="8">';
    html += '<table class="table table-hover align-middle">';
    html += '<thead>';
    html += '<tr>';
    html += '<th scope="col">ครั้งที่</th>';
    html += '<th scope="col">จำนวนรับของ</th>';
    html += '<th scope="col">วันที่รับของ</th>';
    html += '<th scope="col">ผู้รับสินค้า</th>';
    html += '</tr>';
    html += '</thead>';
    html += '<tbody>';
    var sum = 0
    for (var i in receives) {
        var receive = receives[i];
        sum += receive.quantity;
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + receive.quantity + '</td>';
        html += '<td>' + dateFormat(receive.createDate) + '</td>';
        html += '<td>' + receive.receiverName + '</td>';
        html += '</tr>';
    }
    html += '</tbody>';
    html += '<tfoot>';
    html += '<tr>';
    html += '<td><b>คงเหลือ</b></td>';
    html += '<td><b>' + (all - sum) + '</b></td>';
    html += '</tr>';
    html += '</tfoot>';
    html += '</table>';
    html += '</td>';
    return {
        html: html,
        remain: (all - sum)
    };
}
function createRowExpand(pr, tab, i) {
    var html = '';
    if (pr.details.length > 0) {
        for (var j = 0; j < pr.details.length; j++) {
            var detail = pr.details[j];
            var len = (detail.receives.length > 0 ? 1 : 0) + 1;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            var rece = {};
            if (detail.receives.length > 0) rece = createReceiveTable(detail.receives, detail.quantity);
            if (tab == 1 || (tab == 2 && rece.remain != 0)) {
                html += '<tr>';
                var num = 0;
                if (tab == 1) {
                    num = allNum;
                    allNum++;
                } else {
                    num = waitNum;
                    waitNum++;
                }
                html += '<td ' + rowspan + '>' + (parseInt(num) + 1) + '</td>';
                if (detail.receives.length > 0 && rece.remain == 0)
                    html += '<td ' + rowspan + '></td>'
                else
                    html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr_detail(' + i + ',' + tab + ',' + j + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionDetail">edit</span></td>';
                html += '<td ' + rowspan + '>' + pr.projectName + '</td>';
                html += '<td ' + rowspan + '>' + pr.prNo + '</td>';
                html += '<td ' + rowspan + '>' + dateFormat(pr.createDate) + '</td>';
                if (pr.isAdvancePay) {
                    html += '<td ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td><td ' + rowspan + '>' + pr.contractorName + '</td>';
                } else {
                    html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
                }
                html += '<td ' + rowspan + '>' + pr.requesterName + '</td>';
                html += '<td>' + (parseInt(j) + 1) + '</td>';
                html += '<td>' + detail.name + '</td>';
                html += '<td>' + detail.quantity + '</td>';
                html += '<td>' + detail.unit + '</td>';
                html += '<td>' + dateFormat(detail.useDate) + '</td>';
                //html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
                html += '<td>' + detail.code + '</td>';
                html += '<td>' + detail.remark + '</td>';
                html += '<td>' + detail.status + '</td>';
                html += '</tr>';
                if (detail.receives.length > 0) html += rece.html;
            }
        }
    }
    return html;
}
function createRowGroup(pr, tab, i) {
    var html = '';
    var len = pr.details.length + pr.receiveCount;
    var rowspan = '';
    if (len > 1) {
        rowspan = 'rowspan="' + len + '"';
    }
    var rece = {};
    if (detail.receives.length > 0) rece = createReceiveTable(detail.receives, detail.quantity);
    html += '<tr>';
    html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
    if (detail.receives.length > 0 && rece.remain == 0)
        html += '<td ' + rowspan + '></td>'
    else
        html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr(' + i + ',' + tab + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionDetail">edit</span></td>';
    //html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_pr(' + pr.id + ',\'' + pr.prNo + '\')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
    html += '<td ' + rowspan + '>' + pr.projectName + '</td>';
    html += '<td ' + rowspan + '>' + pr.prNo + '</td>';
    html += '<td ' + rowspan + '>' + dateFormat(pr.createDate) + '</td>';
    if (pr.isAdvancePay) {
        html += '<td ' + rowspan + '><span class="material-symbols-outlined" style="color:#000;">done</span></td><td ' + rowspan + '>' + pr.contractorName + '</td>';
    } else {
        html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
    }
    html += '<td ' + rowspan + '>' + pr.requesterName + '</td>';
    if (pr.details.length > 0) {
        var detail = pr.details[0];
        html += '<td>1</td>';
        html += '<td>' + detail.name + '</td>';
        html += '<td>' + detail.quantity + '</td>';
        html += '<td>' + detail.unit + '</td>';
        html += '<td>' + dateFormat(detail.useDate) + '</td>';
        //html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
        html += '<td>' + detail.code + '</td>';
        html += '<td>' + detail.remark + '</td>';
        html += '<td>' + detail.status + '</td>';
        html += '</tr>';
        if (detail.receives.length > 0) html += createReceiveTable(detail.receives, detail.quantity);
        for (var j = 1; j < pr.details.length; j++) {
            var detail = pr.details[j];
            html += '<tr>';
            html += '<td>' + (parseInt(j) + 1) + '</td>';
            html += '<td>' + detail.name + '</td>';
            html += '<td>' + detail.quantity + '</td>';
            html += '<td>' + detail.unit + '</td>';
            html += '<td>' + dateFormat(detail.useDate) + '</td>';
            //html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
            html += '<td>' + detail.code + '</td>';
            html += '<td>' + detail.remark + '</td>';
            html += '<td>' + detail.status + '</td>';
            html += '</tr>';
            if (detail.receives.length > 0) html += createReceiveTable(detail.receives, detail.quantity);
        }
    } else {
        html += '<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
        html += '</tr>';
    }
    return html;
}
function createTable() {
    console.log(prColl);
    $('#all_table').empty();
    $('#wait_table').empty();
    allNum = 0;
    waitNum = 0;
    if (prColl.all != null) {
        var html = '';
        for (var i in prColl.all) {
            var pr = prColl.all[i];
            //html += createRowGroup(pr, 1, i);
            html += createRowExpand(pr, 1, i);
        }
        $('#all_table').append(html);
    }
    if (prColl.waiting != null) {
        var html = '';
        for (var i in prColl.waiting) {
            var pr = prColl.waiting[i];
            //html += createRowGroup(pr, 2, i);
            html += createRowExpand(pr, 2, i);
        }
        $('#wait_table').append(html);
        if (prColl.waitingCount > 0)
            $('#wait_tab').html('จัดส่ง&nbsp;<span class="badge bg-primary">' + prColl.waitingCount + '</span>');
        else
            $('#wait_tab').html('จัดส่ง');
    }
}
function search() {
    var searchUrl = baseUrl + 'searchReceive';
    var request = new FormData();
    request.append("ProjectId", $('#ProjectId').val());
    request.append("PrNo", $('#PrNo').val());
    request.append("RequesterId", $('#RequesterId').val());
    $.ajax({
        type: "POST",
        url: searchUrl,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            prColl = result;
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
$('#ProjectId').change(function () {
    bindFilter($('#ProjectId').val());
    $('#PrNo').val('ทั้งหมด');
    $('#RequesterId').val(0);
});
function edit_pr(i, tab) {
    if (tab == 1) {
        bindReceiveData(prColl.all[i]);
    } else if (tab == 2) {
        bindReceiveData(prColl.waiting[i]);
    } else {
        //init blank data
    }
}
function edit_pr_detail(i, tab, j) {
    if (tab == 1) {
        bindReceiveDataDetail(prColl.all[i], j);
    } else if (tab == 2) {
        bindReceiveDataDetail(prColl.waiting[i], j);
    } else {
        //init blank data
    }
}
function bindReceiveData(pr) {
    console.log(pr);
}
function bindReceiveDataDetail(pr, j) {
    var detail = pr.details[j];
    console.log(pr, detail);

    $('#receive_files_pane').empty();
    $('#receive_files_pane').append('<div class="file-loading"><input id="receive_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#"></div>');
    $('#receive_files').fileinput({
        language: "th",
        showUpload: false,
        previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
        overwriteInitial: false,
        initialPreviewAsData: true,
        initialPreview: detail.filePreviews,
        initialPreviewConfig: detail.files
    });
    $('#receive_id').val(detail.id);
    $('#receive_project_name').val(pr.projectName);
    $('#receive_pr_no').val(pr.prNo);
    $('#receive_create_date').val(dateValue(pr.createDate));
    $('#receive_requester').val(pr.requesterName);
    $('#receive_detail_table').empty();
    var detailHtml = '';
    detailHtml += '<tr>';
    detailHtml += '<td>' + detail.name + '</td>';
    detailHtml += '<td>' + detail.quantity + '</td>';
    detailHtml += '<td>' + detail.unit + '</td>';
    detailHtml += '<td>' + dateFormat(detail.useDate) + '</td>';
    detailHtml += '<td>' + detail.code + '</td>';
    detailHtml += '<td>' + detail.remark + '</td>';
    detailHtml += '<td>' + detail.status + '</td>';
    detailHtml += '</tr>';
    $('#receive_detail_table').append(detailHtml);
    $('#receive_table').empty();
    var html = '';
    for (var i in detail.receives) {
        var receive = detail.receives[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + detail.name + '</td>';
        html += '<td>' + receive.quantity + '</td>';
        html += '<td>' + detail.unit + '</td>';
        html += '<td>' + dateFormat(receive.createDate) + '</td>';
        html += '<td>' + receive.receiverName + '</td>';
        html += '<td>' + receive.remark + '</td>';
        html += '</tr>';
    }
    $('#receive_table').append(html);
    $('#receive_quantity').val('');
    $('#receive_remark').val('');
}
$('#receive_btn').click(function () {
    if (confirm('คุณยืนยันที่จะรับสินค้านี้ใช่หรือไม่ ?')) {
        const input = document.getElementById('receive_files');
        var updateUrl = baseUrl + 'receive';
        var data = new FormData();
        data.append("DetailId", $('#receive_id').val());
        data.append("Quantity", $('#receive_quantity').val());
        data.append("Remark", $('#receive_remark').val());
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
                console.log(result);
                alert('บันทึกสำเร็จ');
                search();
                $('#ActionDetail').modal('hide');
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
                alert('บันทึกไม่สำเร็จ');
                $('#ActionDetail').modal('hide');
            }
        });
    }
});
$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    search();
});