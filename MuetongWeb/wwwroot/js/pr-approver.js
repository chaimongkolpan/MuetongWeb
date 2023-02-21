var edit_detail = [];
var products = [];
var projectCodes = [];
var prColl = {};
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
}
function bindContractor(projectId) {
    var contractorUrl = baseUrl + 'contractor/' + projectId;
    $('#add_contractor').empty();
    var jqxhr = $.get(contractorUrl)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                //console.log(item);
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_contractor').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindContractorEdit(projectId) {
    var contractorUrl = baseUrl + 'contractor/' + projectId;
    $('#edit_contractor').empty();
    var jqxhr = $.get(contractorUrl)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                //console.log(item);
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#edit_contractor').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindCode(projectId) {
    var codeUrl = baseUrl + 'code/' + projectId;
    $('#add_detail_project_code').empty();
    var jqxhr = $.get(codeUrl)
        .done(function (response) {
            console.log(response);
            projectCodes = response;
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.code + '</option>';
                $('#add_detail_project_code').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindCodeEdit(projectId) {
    var codeUrl = baseUrl + 'code/' + projectId;
    $('#edit_detail_project_code').empty();
    var jqxhr = $.get(codeUrl)
        .done(function (response) {
            projectCodes = response;
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.code + '</option>';
                $('#edit_detail_project_code').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindCodeEditCallback(projectId, callback) {
    var codeUrl = baseUrl + 'code/' + projectId;
    $('#edit_detail_project_code').empty();
    var jqxhr = $.get(codeUrl)
        .done(function (response) {
            projectCodes = response;
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.code + '</option>';
                $('#edit_detail_project_code').append(html);
            }
            callback();
        })
        .fail(function (response) {
            console.log(response);
            callback();
        });
}
function bindProduct() {
    var productUrl = baseUrl + 'product';
    $('#edit_detail_product').empty();
    var jqxhr = $.get(productUrl)
        .done(function (response) {
            console.log(response);
            products = response;
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#edit_detail_product').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
$('#ProjectId').change(function () {
    bindFilter($('#ProjectId').val());
    $('#PrNo').val('ทั้งหมด');
    $('#RequesterId').val(0);
});
function checkEditStatus(status) {
    if (status == "สั่งสินค้า" || status == "รอสั่งซื้อ" || status == "รอสินค้าจัดส่ง" || status == "จัดส่งสำเร็จ" || status == "ยกเลิก")
        return true;
    return false;
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
function createTable() {
    console.log(prColl);
    $('#all_table').empty();
    $('#wait_table').empty();
    $('#cancel_table').empty();
    if (prColl.all != null) {
        var html = '';
        for (var i in prColl.all) {
            var pr = prColl.all[i];
            var len = pr.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            if (checkEditStatus(pr.status)) {
                html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
            } else {
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr(' + pr.id + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionApproveOrder">view_list</span></td>';
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_pr(' + pr.id + ',\'' + pr.prNo + '\')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
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
            html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="showRefFiles(' + pr.id + ',\'pr\',\'เอกสารอ้างอิงสั่งสินค้า\')" data-bs-toggle="modal" data-bs-target="#RefShowFile">description</span></td>';
            html += '<td ' + rowspan + '>' + pr.approverName + '</td>';
            html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="showRefFiles(' + pr.id + ',\'prapprove\',\'เอกสารอ้างอิงตรวจสอบสั่งสินค้า\')" data-bs-toggle="modal" data-bs-target="#RefShowFile">description</span></td>';
            if (len > 0) {
                var detail = pr.details[0];
                html += '<td>1</td>';
                html += '<td>' + detail.name + '</td>';
                html += '<td>' + detail.quantity + '</td>';
                html += '<td>' + detail.unit + '</td>';
                html += '<td>' + dateFormat(detail.useDate) + '</td>';
                html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
                html += '<td>' + detail.code + '</td>';
                html += '<td>' + detail.remark + '</td>';
                html += '<td>' + detail.status + '</td>';
                html += '<td><span class="material-symbols-outlined" onclick="showRefFiles(' + detail.id + ',\'prreceive\',\'เอกสารอ้างอิงรับสินค้า\')" data-bs-toggle="modal" data-bs-target="#RefShowFile">description</span></td>';
                html += '</tr>';
                for (var j = 1; j < len; j++) {
                    var detail = pr.details[j];
                    html += '<tr>';
                    html += '<td>' + (parseInt(j) + 1) + '</td>';
                    html += '<td>' + detail.name + '</td>';
                    html += '<td>' + detail.quantity + '</td>';
                    html += '<td>' + detail.unit + '</td>';
                    html += '<td>' + dateFormat(detail.useDate) + '</td>';
                    html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
                    html += '<td>' + detail.code + '</td>';
                    html += '<td>' + detail.remark + '</td>';
                    html += '<td>' + detail.status + '</td>';
                    html += '<td><span class="material-symbols-outlined" onclick="showRefFiles(' + detail.id + ',\'prreceive\',\'เอกสารอ้างอิงรับสินค้า\')" data-bs-toggle="modal" data-bs-target="#RefShowFile">description</span></td>';
                    html += '</tr>';
                }

            } else {
                html += '<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
                html += '</tr>';
            }
        }
        $('#all_table').append(html);
    }
    if (prColl.waiting != null) {
        var html = '';
        for (var i in prColl.waiting) {
            var pr = prColl.waiting[i];
            var len = pr.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="approve_pr(' + pr.id + ',\'' + pr.prNo + '\')" style="color:green;" data-bs-toggle="modal" data-bs-target="#ActionConfirm">check_circle</span></td>';
            html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            if (checkEditStatus(pr.status)) {
                html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
            } else {
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr(' + pr.id + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionApproveOrder">view_list</span></td>';
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="cancel_pr(' + pr.id + ',\'' + pr.prNo + '\')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionCancel">delete</span></td>';
            }
            html += '<td ' + rowspan + '>' + pr.projectName + '</td>';
            html += '<td ' + rowspan + '>' + pr.prNo + '</td>';
            html += '<td ' + rowspan + '>' + dateFormat(pr.createDate) + '</td>';
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
                html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
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
                    html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
                    html += '<td>' + detail.code + '</td>';
                    html += '<td>' + detail.remark + '</td>';
                    html += '<td>' + detail.status + '</td>';
                    html += '</tr>';
                }

            } else {
                html += '<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
                html += '</tr>';
            }
        }
        $('#wait_table').append(html);
        if (prColl.waitingCount > 0)
            $('#wait_tab').html('สั่งสินค้า&nbsp;<span class="badge bg-primary">' + prColl.waitingCount + '</span>');
        else
            $('#wait_tab').html('สั่งสินค้า');
    }
    if (prColl.cancel != null) {
        var html = '';
        for (var i in prColl.cancel) {
            var pr = prColl.cancel[i];
            var len = pr.details.length;
            var rowspan = '';
            if (len > 1) {
                rowspan = 'rowspan="' + len + '"';
            }
            html += '<tr>';
            if (pr.isReadCancel) {
                html += '<td ' + rowspan + '></td>';
            } else {
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="read_pr_cancel(' + pr.id + ',\'' + pr.prNo + '\')" style="color:#A42206;" data-bs-toggle="modal" data-bs-target="#ActionApproveCancel">assignment_late</span></td>';
            }
            html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            html += '<td ' + rowspan + '>' + pr.projectName + '</td>';
            html += '<td ' + rowspan + '>' + pr.prNo + '</td>';
            html += '<td ' + rowspan + '>' + dateFormat(pr.createDate) + '</td>';
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
                html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
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
                    html += '<td>' + dateFormat(detail.planTransferDate) + '</td>';
                    html += '<td>' + detail.code + '</td>';
                    html += '<td>' + detail.remark + '</td>';
                    html += '<td>' + detail.status + '</td>';
                    html += '</tr>';
                }

            } else {
                html += '<td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td>';
                html += '</tr>';
            }
        }
        $('#cancel_table').append(html);
        if (prColl.unreadCancelCount > 0)
            $('#cancel_tab').html('ยกเลิก&nbsp;<span class="badge bg-danger">' + prColl.unreadCancelCount + '</span>');
        else
            $('#cancel_tab').html('ยกเลิก');
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
function add_edit_detail(index) {
    $('#add_detail_edit_id').val(index);
    var detail = add_detail[index];
    //console.log(index, detail);
    $('#add_detail_btn').hide();
    $('#add_detail_edit_btn').show();
    $('#add_detail_product').val(detail.ProductId);
    $('#add_detail_project_code').val(detail.ProjectCodeId);
    $('#add_detail_quantity').val(detail.Quantity);
    $('#add_detail_usedate').val(detail.UseDate);
    $('#add_detail_remark').val(detail.Remark);
    $('#add_detail_pane').show();
}
function add_delete_detail(index) {
    add_detail.splice(index, 1);
    bindDetailTable();
}
function edit_edit_detail(index) {
    $('#edit_detail_edit_id').val(index);
    var detail = edit_detail[index];
    //console.log(index, detail);
    $('#edit_detail_btn').hide();
    $('#edit_detail_edit_btn').show();
    $('#edit_detail_product').val(detail.ProductId);
    $('#edit_detail_project_code').val(detail.ProjectCodeId);
    $('#edit_detail_quantity').val(detail.Quantity);
    $('#edit_detail_usedate').val(detail.UseDate);
    $('#edit_detail_remark').val(detail.Remark);
    $('#edit_detail_pane').show();
}
function edit_delete_detail(index) {
    edit_detail.splice(index, 1);
    bindDetailTableEdit();
}
function bindDetailTableEdit() {
    $('#edit_detail_table').empty();
    for (var i in edit_detail) {
        var detail = edit_detail[i];
        var html = '';
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + (detail.Product == null ? '' : detail.Product.name) + '</td>';
        html += '<td>' + detail.Quantity + '</td>';
        html += '<td>' + (detail.Product == null ? '' : detail.Product.unit) + '</td>';
        html += '<td>' + dateFormat(detail.UseDate) + '</td>';
        html += '<td>' + (detail.ProjectCode == null ? '' : detail.ProjectCode.code) + '</td>';
        html += '<td>' + detail.Remark + '</td>';
        html += '</tr>';
        $('#edit_detail_table').append(html);
    }
}
$('#edit_project').change(function () {
    var projectId = $('#edit_project').val();
    bindContractorEdit(projectId);
    projectCodes = [];
    bindCodeEdit(projectId);
});
$('#show_edit_btn').click(function () {
    $('#edit_detail_quantity').val('');
    $('#edit_detail_usedate').val(null);
    $('#edit_detail_remark').val('');
    $('#edit_detail_btn').show();
    $('#edit_detail_edit_btn').hide();
    $('#edit_detail_pane').show();
});
function edit_pr(id) {
    var editUrl = baseUrl + id
    $('#edit_files_pane').empty();
    $('#edit_approve_files').fileinput('clear');
    $.ajax({
        type: "GET",
        url: editUrl,
        contentType: false,
        processData: false,
        success: function (result) {
            console.log(result);
            projectCodes = [];
            bindContractorEdit(result.projectId);

            $('#edit_files_pane').append('<div class="file-loading"><input id="edit_files" class="file" type="file" multiple data-preview-file-type="any" data-upload-url="#"></div>');
            $('#edit_files').fileinput({
                language: "th",
                showUpload: false,
                previewFileIcon: "<i class='glyphicon glyphicon-king'></i>",
                overwriteInitial: false,
                initialPreviewAsData: true,
                initialPreview: result.filePreviews,
                initialPreviewConfig: result.files
            });
            $('#edit_files').prop('disabled', 'disabled');
            $('#edit_id').val(id);
            $('#edit_project').val(result.projectId);
            $('#edit_detail_pane').hide();
            $('#edit_pr_no').val(result.prNo);
            if (result.isAdvancePay) {
                $('#edit_advance_pay').prop('checked', true);
                $('#edit_contractor').val(result.contractId);
            } else {
                $('#edit_advance_pay').prop('checked', false);
            }
            $('#edit_contractor').prop('disabled', 'disabled');
            bindCodeEditCallback(result.projectId, function () {
                console.log(projectCodes);
                edit_detail = [];
                for (var i in result.details) {
                    var detail = result.details[i];
                    var product = products.find(x => x.id == detail.productId);
                    var projectCode = projectCodes.find(x => x.id == detail.projectCodeId);
                    var tmp = {
                        Id: detail.Id,
                        ProductId: detail.productId,
                        Product: product,
                        Quantity: detail.quantity,
                        ProjectCodeId: detail.projectCodeId,
                        ProjectCode: projectCode,
                        UseDate: dateValue(detail.useDate),
                        Remark: detail.remark
                    };
                    edit_detail.push(tmp);
                }
                bindDetailTableEdit();
            });
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}
$('#edit_detail_btn').click(function () {
    var product = products.find(x => x.id == $('#edit_detail_product').val());
    var projectCode = projectCodes.find(x => x.id == $('#edit_detail_project_code').val());
    var detail = {
        Id: 0,
        ProductId: product.id,
        Product: product,
        Quantity: $('#edit_detail_quantity').val(),
        ProjectCodeId: projectCode.id,
        ProjectCode: projectCode,
        UseDate: $('#edit_detail_usedate').val(),
        Remark: $('#edit_detail_remark').val()
    };
    edit_detail.push(detail);
    $('#edit_detail_pane').hide();
    bindDetailTableEdit();
});
$('#edit_detail_edit_btn').click(function () {
    var product = products.find(x => x.id == $('#edit_detail_product').val());
    var projectCode = projectCodes.find(x => x.id == $('#edit_detail_project_code').val());
    var detail = {
        Id: 0,
        ProductId: product.id,
        Product: product,
        Quantity: $('#edit_detail_quantity').val(),
        ProjectCodeId: projectCode.id,
        ProjectCode: projectCode,
        UseDate: $('#edit_detail_usedate').val(),
        Remark: $('#edit_detail_remark').val()
    };
    var index = $('#edit_detail_edit_id').val();
    detail.Id = edit_detail[index].Id;
    edit_detail[index] = detail;
    $('#edit_detail_pane').hide();
    bindDetailTableEdit();
});
$('#edit_pr_btn').click(function () {
    console.log(edit_detail);
    var id = $('#edit_id').val();
    const input = document.getElementById('edit_approve_files');
    var updateUrl = baseUrl + 'update/' + id;
    var data = new FormData();
    data.append("ProjectId", $('#edit_project').val());
    data.append("PrNo", $('#edit_pr_no').val());
    data.append("AdvancePay", $('#edit_advance_pay').prop('checked'));
    data.append("ContractorId", $('#edit_contractor').val());

    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    for (var i = 0; i < edit_detail.length; i++) {
        var detail = edit_detail[i];
        delete detail['Product'];
        delete detail['ProjectCode'];
    }
    var jsonStr = JSON.stringify(edit_detail);
    data.append("JsonDetails", jsonStr);
    console.log(data);
    $.ajax({
        type: "POST",
        url: updateUrl,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            console.log(result);
            search();
            $('#ActionApproveOrder').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
function cancel_pr(id, prno) {
    console.log(id, prno);
    $('#cancel_id').val(id);
    $('#pr_cancel_text').html('ท่านต้องการยกเลิกการสั่งสินค้า เลขที่ PR ' + prno);
}
$('#cancel_btn').click(function () {
    var id = $('#cancel_id').val();
    var cancelUrl = baseUrl + 'cancel/' + id;
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
function approve_pr(id, prno) {
    console.log(id, prno);

    $('#edit_approve_files').val('');
    $('#edit_approve_files').fileinput('clear');
    $('#approve_id').val(id);
    $('#pr_approve_text').html('ท่านต้องการยืนยันตรวจสอบสั่งสินค้า เลขที่ PR ' + prno);
}
$('#approve_btn').click(function () {
    var id = $('#approve_id').val();
    var approveUrl = baseUrl + 'approve/' + id;
    const input = document.getElementById('edit_approve_files');
    var data = new FormData();
    for (var i = 0; i < input.files.length; i++) {
        data.append("Files", input.files[i]);
    }
    $.ajax({
        type: "POST",
        url: approveUrl,
        contentType: false,
        processData: false,
        data: data,
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
function read_pr_cancel(id, prno) {
    $('#cancel_read_id').val(id);
    $('#cancel_read_text').html('ท่านรับทราบการยกเลิกการสั่งสินค้า เลขที่ PR ' + prno);
}
$('#read_pr_btn').click(function () {
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
$(document).ready(function () {
    console.log('ready', model);

    $('#edit_approve_files').fileinput({
        language: "th",
        showUpload: false,
    });
    $('#edit_approve_files').on('fileselect', function (event, numFiles, label) {
        $('.kv-file-upload').hide();
    });

    bindFilter(0)
    bindProduct();
    search();
});