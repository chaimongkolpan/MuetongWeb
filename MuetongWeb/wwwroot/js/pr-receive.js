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
            var html = '<select id="RequesterId" name="RequesterId">';
            html += '<option value="0" selected>ทั้งหมด</option>';
            for (var i in response) {
                var item = response[i];
                html += '<option value="' + item.id + '">' + item.fullname + '</option>';
            }
            html += '</select>';
            dynamicCreateAutocomplete('RequesterPane', 'RequesterId', 'ผู้สั่งสินค้า', html);
        })
        .fail(function (response) {
            console.log(response);
            var html = '<select id="RequesterId" name="RequesterId">';
            html += '<option value="0" selected>ทั้งหมด</option>';
            html += '</select>';
            dynamicCreateAutocomplete('RequesterPane', 'RequesterId', 'ผู้สั่งสินค้า', html);
        });
    var prnoUrl = baseUrl + 'prnoComplete/' + projectId;
    var jqxhr = $.get(prnoUrl)
        .done(function (response) {
            console.log(response);
            var html = '<select id="PrNo" name="PrNo">';
            html += '<option selected>ทั้งหมด</option>';
            for (var i in response) {
                var item = response[i];
                html += '<option>' + item + '</option>';
            }
            html += '</select>';
            dynamicCreateAutocomplete('PrNoPane', 'PrNo', 'เลขที่ PR', html);
        })
        .fail(function (response) {
            console.log(response);
            var html = '<select id="PrNo" name="PrNo">';
            html += '<option selected>ทั้งหมด</option>';
            html += '</select>';
            dynamicCreateAutocomplete('PrNoPane', 'PrNo', 'เลขที่ PR', html);
        });
}
function createReceiveTable(receives, all, tab) {
    var html = '';
    html += '<tr>';
    if (tab == 1)
        html += '<td colspan="9">';
    else
        html += '<td colspan="9">';
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
function showRefFiles(id, type, text) {
    $('#show_files').val('');
    $('#show_files').fileinput('clear');
    $('#show_file_text').html(text);
    var fileUrl = baseUrl + 'files/' + id + '/' + type;
    $.ajax({
        type: "GET",
        url: fileUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            $('#show_file_pane').empty();
            $('#show_file_pane').append('<div class="file-loading"><input id="show_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#"></div>');
            $('#show_files').fileinput({
                language: "th",
                showUpload: false,
                showRemove: false,
                previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
                overwriteInitial: false,
                initialPreviewAsData: true,
                initialPreview: result.filePreviews,
                initialPreviewConfig: result.files
            });
            $('#show_files').prop('disabled', 'disabled');
            $('#show_file_pane .kv-file-remove').hide();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
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
            if (detail.receives.length > 0) rece = createReceiveTable(detail.receives, detail.quantity, tab);
            if (tab == 1 || (tab == 2 && detail.status != 'จัดส่งสำเร็จ')) {
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
                if (detail.receives.length > 0 && rece.remain <= 0 && detail.status == 'จัดส่งสำเร็จ') {
                    if (true) // has permission
                        html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr_detail_disapprove(' + i + ',' + tab + ',' + j + ')" style="color:red;" data-bs-toggle="modal" data-bs-target="#DisapproveDetail">cancel</span></td>'
                    else
                        html += '<td ' + rowspan + '></td>'
                } else {
                    html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr_detail(' + i + ',' + tab + ',' + j + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionDetail">edit</span>';
                    if (true && rece.remain <= 0) // has permission
                        html += '<span class="material-symbols-outlined" onclick="edit_pr_detail_approve(' + i + ',' + tab + ',' + j + ')" style="color:green;" data-bs-toggle="modal" data-bs-target="#ApproveDetail">check_circle</span>';

                    html += '</td>';
                }
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
                //if(tab == 1)
                    html += '<td><span class="material-symbols-outlined" onclick="showRefFiles(' + detail.id + ',\'prreceive\',\'เอกสารอ้างอิงรับสินค้า\')" data-bs-toggle="modal" data-bs-target="#RefShowFile">description</span></td>';
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
    if (detail.receives.length > 0) rece = createReceiveTable(detail.receives, detail.quantity, tab);
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
        if (detail.receives.length > 0) html += createReceiveTable(detail.receives, detail.quantity, tab);
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
            if (detail.receives.length > 0) html += createReceiveTable(detail.receives, detail.quantity, tab);
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
function edit_pr_detail_approve(i, tab, j) {
    var pr = {}
    if (tab == 1) {
        pr = prColl.all[i];
    } else if (tab == 2) {
        pr = prColl.waiting[i];
    } else {
        //init blank data
    }
    var detail = pr.details[j];
    console.log(pr, detail);
    $('#approve_id').val(detail.id);
    $('#approve_text').html('ยืนยันตรวจสอบสินค้า ' + detail.name + ' จาก PR เลขที่ : ' + pr.prNo);
    console.log('approve');
}
function edit_pr_detail_disapprove(i, tab, j) {
    var pr = {}
    if (tab == 1) {
        pr = prColl.all[i];
    } else if (tab == 2) {
        pr = prColl.waiting[i];
    } else {
        //init blank data
    }
    var detail = pr.details[j];
    console.log(pr, detail);
    $('#disapprove_id').val(detail.id);
    $('#disapprove_text').html('ยกเลิกการตรวจสอบสินค้า ' + detail.name + ' จาก PR เลขที่ : ' + pr.prNo);
    console.log('disapprove');
}
$('#approve_btn').click(function () {
    var approveUrl = baseUrl + 'ApproveReceive/' + $('#approve_id').val();
    $.ajax({
        type: "GET",
        url: approveUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#ApproveDetail').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#disapprove_btn').click(function () {
    var disapproveUrl = baseUrl + 'DisapproveReceive/' + $('#disapprove_id').val();
    $.ajax({
        type: "GET",
        url: disapproveUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            search();
            $('#DisapproveDetail').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
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
    $('#receive_files').on('fileselect', function (event, numFiles, label) {
        $('.kv-file-upload').hide();
    });
    $('#receive_id').val(detail.id);
    $('#receive_project_name').val(pr.projectName);
    $('#receive_pr_no').val(pr.prNo);
    $('#receive_create_date').val(dateValue(pr.createDate));
    $('#receive_requester').val(pr.requesterName);
    $('#receive_detail_table').empty();
    var all = detail.quantity;
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
    var sum = 0;
    for (var i in detail.receives) {
        var receive = detail.receives[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        if (detail.receives.length - 1 == i)
            html += '<td><span class="material-symbols-outlined" onclick="bindUpdateReceive(' + receive.id + ',' + receive.quantity
                + ',\'' + receive.remark + '\',\'' + dateValue(receive.createDate) + '\')" style="color:#0752AE;">edit</span></td>';
        else
            html += '<td></td>';
        html += '<td>' + detail.name + '</td>';
        sum += receive.quantity;
        html += '<td>' + receive.quantity + '</td>';
        html += '<td>' + detail.unit + '</td>';
        html += '<td>' + dateFormat(receive.createDate) + '</td>';
        html += '<td>' + receive.receiverName + '</td>';
        html += '<td>' + receive.remark + '</td>';
        html += '</tr>';
    }
    $('#receive_remain').val(all-sum);
    $('#receive_table').append(html);
    $('#receive_quantity').val('');
    $('#receive_remark').val('');
}
function bindUpdateReceive(id, quantity, remark, date) {
    $('#update_mode').val(1);
    $('#update_receive_id').val(id);
    $('#receive_quantity').val(quantity);
    $('#receive_remark').val(remark);
    $('#receive_date').val(date);
}
$('#receive_btn').click(function () {
    var mode = $('#update_mode').val();
    var remain = $('#receive_remain').val();
    var quantity = $('#receive_quantity').val();
    if ((remain > 0 && remain >= quantity) || (remain <= 0)) {
        if (confirm('คุณยืนยันที่จะรับสินค้านี้ใช่หรือไม่ ?')) {
            const input = document.getElementById('receive_files');
            var updateUrl = baseUrl + 'receive';
            if (mode == 1) updateUrl = baseUrl + 'updatereceive/' + $('#update_receive_id').val();
            var data = new FormData();
            data.append("DetailId", $('#receive_id').val());
            data.append("Quantity", quantity);
            data.append("CreateDate", $('#receive_date').val());
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
                    $('#update_mode').val(0);
                    search();
                    $('#ActionDetail').modal('hide');
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                    alert('บันทึกไม่สำเร็จ');
                    $('#update_mode').val(0);
                    $('#ActionDetail').modal('hide');
                }
            });
        }
    } else {
        alert('คุณต้องใส่ตัวเลขไม่เกินจำนวนที่เหลืออยู่ !!!');
    }
});
$(document).ready(function () {
    console.log('ready', model);
    bindFilter(0)
    createAutocomplete('ProjectId');
    search();
});