﻿var add_detail = [];
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
    $('#add_detail_product').empty();
    $('#edit_detail_product').empty();
    var jqxhr = $.get(productUrl)
    .done(function (response) {
        console.log(response);
        products = response;
        for (var i in response) {
            var item = response[i];
            var html = '<option value="' + item.id + '">' + item.name + '</option>';
            $('#add_detail_product').append(html);
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
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr(' + pr.id + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionOrder">view_list</span></td>';
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
            html += '<td ' + rowspan + '>' + (parseInt(i) + 1) + '</td>';
            if (checkEditStatus(pr.status)) {
                html += '<td ' + rowspan + '></td><td ' + rowspan + '></td>';
            } else {
                html += '<td ' + rowspan + '><span class="material-symbols-outlined" onclick="edit_pr(' + pr.id + ')" style="color:#0752AE;" data-bs-toggle="modal" data-bs-target="#ActionOrder">view_list</span></td>';
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
                for (var j = 1;j < len;j++) {
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
function bindDetailTable() {
    $('#add_detail_table').empty();
    for (var i in add_detail) {
        var detail = add_detail[i];
        var html = '';
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="add_edit_detail(' + i + ')" style="color:#0752AE;">edit</span></td>';
        html += '<td><span class="material-symbols-outlined" onclick="add_delete_detail(' + i + ')" style="color:#A42206;">delete</span></td>';
        html += '<td>' + (detail.Product == null ? '' : detail.Product.name) + '</td>';
        html += '<td>' + detail.Quantity + '</td>';
        html += '<td>' + (detail.Product == null ? '' : detail.Product.unit) + '</td>';
        html += '<td>' + dateFormat(detail.UseDate) + '</td>';
        html += '<td>' + (detail.ProjectCode == null ? '' : detail.ProjectCode.code) + '</td>';
        html += '<td>' + detail.Remark + '</td>';
        html += '</tr>';
        $('#add_detail_table').append(html);
    }
}
function bindDetailTableEdit() {
    $('#edit_detail_table').empty();
    for (var i in edit_detail) {
        var detail = edit_detail[i];
        var html = '';
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="edit_edit_detail(' + i + ')" style="color:#0752AE;">edit</span></td>';
        html += '<td><span class="material-symbols-outlined" onclick="edit_delete_detail(' + i + ')" style="color:#A42206;">delete</span></td>';
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
$('#add_project').change(function () {
    var projectId = $('#add_project').val();
    bindContractor(projectId);
    projectCodes = [];
    bindCode(projectId);
});
$('#add_btn').click(function () {
    $('#add_detail_pane').hide();
    $('#add_detail_table').empty();
    $('#add_pr_no').val('');
    $('#add_files').val('');
    $('#add_files').fileinput('clear');
    $('#add_advance_pay').prop('checked', false);
    $('#add_contractor').prop('disabled', 'disabled');
    var projectId = $('#add_project').val();
    bindContractor(projectId);
    projectCodes = [];
    bindCode(projectId);
    add_detail = [];
});
$('#add_advance_pay').change(function () {
    if (this.checked) {
        $('#add_contractor').prop('disabled', false);
    } else {
        $('#add_contractor').prop('disabled', 'disabled');
    }
});
$('#show_add_btn').click(function () {
    $('#add_detail_quantity').val('');
    $('#add_detail_usedate').val(null);
    $('#add_detail_remark').val('');
    $('#add_detail_btn').show();
    $('#add_detail_edit_btn').hide();
    $('#add_detail_pane').show();
});
$('#add_detail_btn').click(function () {
    var product = products.find(x => x.id == $('#add_detail_product').val());
    var projectCode = projectCodes.find(x => x.id == $('#add_detail_project_code').val());
    var detail = {
        ProductId: product.id,
        Product: product,
        Quantity: $('#add_detail_quantity').val(),
        ProjectCodeId: projectCode.id,
        ProjectCode: projectCode,
        UseDate: $('#add_detail_usedate').val(),
        Remark: $('#add_detail_remark').val()
    };
    add_detail.push(detail);
    $('#add_detail_pane').hide();
    bindDetailTable();
});
$('#add_detail_edit_btn').click(function () {
    var product = products.find(x => x.id == $('#add_detail_product').val());
    var projectCode = projectCodes.find(x => x.id == $('#add_detail_project_code').val());
    var detail = {
        ProductId: product.id,
        Product: product,
        Quantity: $('#add_detail_quantity').val(),
        ProjectCodeId: projectCode.id,
        ProjectCode: projectCode,
        UseDate: $('#add_detail_usedate').val(),
        Remark: $('#add_detail_remark').val()
    };
    var index = $('#add_detail_edit_id').val();
    add_detail[index] = detail;
    $('#add_detail_pane').hide();
    bindDetailTable();
});
$('#add_pr_btn').click(function () {
    console.log(add_detail);
    const input = document.getElementById('add_files');
    var addUrl = baseUrl + 'add';
    var data = new FormData();
    data.append("ProjectId", $('#add_project').val());
    data.append("PrNo", $('#add_pr_no').val());
    data.append("AdvancePay", $('#add_advance_pay').prop('checked'));
    data.append("ContractorId", $('#add_contractor').val());
    for (var i = 0;i < input.files.length;i++) {
        data.append("Files", input.files[i]);
    }
    for (var i = 0; i < add_detail.length;i++) {
        var detail = add_detail[i];
        delete detail['Product'];
        delete detail['ProjectCode']; 
    }
    var jsonStr = JSON.stringify(add_detail);
    data.append("JsonDetails", jsonStr);
    console.log(data);
    $.ajax({
        type: "POST",
        url: addUrl,
        contentType: false,
        processData: false,
        data: data,
        success: function (result) {
            console.log(result);
            search();
            $('#AddOrder').modal('hide');
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#edit_project').change(function () {
    var projectId = $('#edit_project').val();
    bindContractorEdit(projectId);
    projectCodes = [];
    bindCodeEdit(projectId);
});
$('#edit_advance_pay').change(function () {
    if (this.checked) {
        $('#edit_contractor').prop('disabled', false);
    } else {
        $('#edit_contractor').prop('disabled', 'disabled');
    }
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
    var editUrl = baseUrl + id;
    $('#edit_files_pane').empty();
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
                initialPreview: result.filePreviews,
                initialPreviewConfig: result.files
            });

            $('#edit_id').val(id);
            $('#edit_project').val(result.projectId);
            $('#edit_detail_pane').hide();
            $('#edit_pr_no').val(result.prNo);
            if (result.isAdvancePay) {
                $('#edit_advance_pay').prop('checked', true);
                $('#edit_contractor').prop('disabled', false);
                $('#edit_contractor').val(result.contractId);
            } else {
                $('#edit_advance_pay').prop('checked', false);
                $('#edit_contractor').prop('disabled', 'disabled');
            }
            bindCodeEditCallback(result.projectId, function () {
                console.log(projectCodes);
                edit_detail = [];
                for (var i in result.details) {
                    var detail = result.details[i];
                    var product = products.find(x => x.id == detail.productId);
                    var projectCode = projectCodes.find(x => x.id == detail.projectCodeId);
                    var tmp = {
                        Id: detail.id,
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
    const input = document.getElementById('edit_files');
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
            $('#ActionOrder').modal('hide');
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
    bindFilter(0)
    bindProduct();
    $('#add_files').fileinput({
        language: "th",
        showUpload: false,
    });
    search();
});