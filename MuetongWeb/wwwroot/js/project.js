var defaultPageSize = 10;
function bindProvince() {
    var url = baseUrl + 'province';
    $('#add_province').empty();
    $('#edit_province').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.nameTh + '</option>';
                $('#add_province').append(html);
                $('#edit_province').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindCustomer() {
    var url = baseUrl + 'customer';
    $('#add_customer').empty();
    $('#edit_customer').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_customer').append(html);
                $('#edit_customer').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindUser() {
    var url = baseUrl + 'user';
    $('#add_user').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.fullname + '</option>';
                $('#add_user').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindContractor() {
    var url = baseUrl + 'contractor';
    $('#add_contractor').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_contractor').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function addClick() {
    $('#add_customer').val($("#add_customer option:first").val());
    $('#add_name').val('');
    $('#add_contractno').val('');
    $('#add_address').val('');
    $('#add_province').val(1);
}
function editClick(id) {
    $('#edit_id').val(id);
    var response = model.Projects.find(x => x.Id == id);
    $('#edit_customer').val(response.CustomerId);
    $('#edit_name').val(response.Name);
    $('#edit_contractno').val(response.ContractNo);
    $('#edit_address').val(response.Address);
    $('#edit_province').val(response.ProvinceId);
}
function deleteClick(id, name) {
    $('#delete_id').val(id);
    $('#delete_text').html('ท่านต้องการลบโครงการ ' + name + ' ใช่หรือไม่ ?');
}
function importCodeClick() {
    const input = document.getElementById('add_code_files');
    var url = baseUrl + $('#edit_code_id').val() + '/code/import';
    var request = new FormData();
    if (input.files.length > 0) {
        request.append("File", input.files[0]);
    }
    var project = model.Projects.find(x => x.Id == $('#edit_code_id').val());
    console.log(project);
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            project.Codes = [];
            for (var i in result.codes) {
                var code = result.codes[i];
                project.Codes.push(createCodeObject(code));
            }
            showCode($('#edit_code_id').val());
            $('#code_row_' + project.Id).empty();
            $('#code_row_' + project.Id).append(createCode(project.Id, project.Codes));
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
}
function createCodeObject(code) {
    return {
        Id: code.id,
        ProjectId: code.projectId,
        Code: code.code,
        Detail: code.detail,
        Budjet: code.budjet,
        Cost: code.cost,
        ParentId: code.parentId
    };
}
function createUserObject(user) {
    return {
        Id: user.id,
        Username: user.username,
        Fullname: user.fullname,
        EmployeeId: user.employeeId,
        Role: user.role,
        Workline: user.workline
    };
}
function createContractorObject(contractor) {
    return {
        Id: contractor.id,
        Name: contractor.name,
        Address: contractor.address,
        ProvinceId: contractor.provinceId,
        ProvinceName: contractor.provinceName,
        PhoneNo: contractor.phoneNo,
        Email: contractor.email,
        TaxNo: contractor.taxNo,
        Type: contractor.type,
        DirectorName: contractor.directorName,
        CanDelete: contractor.canDelete
    };
}
function addUserClick() {
    var project = model.Projects.find(x => x.Id == $('#edit_user_id').val());
    console.log(project);
    var url = baseUrl + $('#edit_user_id').val() + '/user/add/' + $('#add_user').val();
    var jqxhr = $.get(url)
        .done(function (response) {
            project.Users = [];
            for (var i in response.users) {
                var user = response.users[i];
                project.Users.push(createUserObject(user));
            }
            showUser($('#edit_user_id').val());
            $('#user_row_' + project.Id).empty();
            $('#user_row_' + project.Id).append(createUser(project.Id, project.Users));
    })
    .fail(function (response) {
        console.log(response);
    });
}
function deleteUserClick(id) {
    var project = model.Projects.find(x => x.Id == $('#edit_user_id').val());
    console.log(project);
    var url = baseUrl + $('#edit_user_id').val() + '/user/delete/' + id;
    var jqxhr = $.get(url)
        .done(function (response) {
            project.Users = [];
            for (var i in response.users) {
                var user = response.users[i];
                project.Users.push(createUserObject(user));
            }
            showUser($('#edit_user_id').val());
            $('#user_row_' + project.Id).empty();
            $('#user_row_' + project.Id).append(createUser(project.Id, project.Users));
    })
    .fail(function (response) {
        console.log(response);
    });
}
function addContractorClick() {
    var project = model.Projects.find(x => x.Id == $('#edit_contractor_id').val());
    console.log(project);
    var url = baseUrl + $('#edit_contractor_id').val() + '/contractor/add/' + $('#add_contractor').val();
    var jqxhr = $.get(url)
        .done(function (response) {
            project.Contractors = [];
            for (var i in response.contractors) {
                var contractor = response.contractors[i];
                project.Contractors.push(createContractorObject(contractor));
            }
            showContractor($('#edit_contractor_id').val());
            $('#contractor_row_' + project.Id).empty();
            $('#contractor_row_' + project.Id).append(createContractor(project.Id, project.Contractors));
    })
    .fail(function (response) {
        console.log(response);
    });
}
function deleteContractorClick(id) {
    var project = model.Projects.find(x => x.Id == $('#edit_contractor_id').val());
    console.log(project);
    var url = baseUrl + $('#edit_contractor_id').val() + '/contractor/delete/' + id;
    var jqxhr = $.get(url)
        .done(function (response) {
            project.Contractors = [];
            for (var i in response.contractors) {
                var contractor = response.contractors[i];
                project.Contractors.push(createContractorObject(contractor));
            }
            showContractor($('#edit_contractor_id').val());
            $('#contractor_row_' + project.Id).empty();
            $('#contractor_row_' + project.Id).append(createContractor(project.Id, project.Contractors));
    })
    .fail(function (response) {
        console.log(response);
    });
}
function showCode(id) {
    $('#edit_code_id').val(id);
    $('#code_table').empty();
    var project = model.Projects.find(x => x.Id == id);
    var items = project.Codes;
    var html = '';
    for (var i in items) {
        var item = items[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>' + item.Code + '</td>';
        html += '<td>' + item.Detail + '</td>';
        html += '</tr>';
    }
    $('#code_table').append(html);
}
function showUser(id) {
    $('#edit_user_id').val(id);
    $('#user_table').empty();
    var project = model.Projects.find(x => x.Id == id);
    var items = project.Users;
    var html = '';
    for (var i in items) {
        var item = items[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteUserClick(' + item.Id + ');">delete</span></td>';
        html += '<td>' + item.Fullname + '</td>';
        html += '</tr>';
    }
    $('#user_table').append(html);
}
function showContractor(id) {
    $('#edit_contractor_id').val(id);
    $('#contractor_table').empty();
    var project = model.Projects.find(x => x.Id == id);
    var items = project.Contractors;
    var html = '';
    for (var i in items) {
        var item = items[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteContractorClick(' + item.Id + ');">delete</span></td>';
        html += '<td>' + item.Name + '</td>';
        html += '</tr>';
    }
    $('#contractor_table').append(html);
}
$('#delete_btn').click(function () {
    var id = $('#delete_id').val();
    var url = baseUrl + 'delete/' + id;
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        success: function (result) {
            location.reload();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#add_btn').click(function () {
    var url = baseUrl + 'add';
    var request = new FormData();
    request.append("CustomerId", $('#add_customer').val());
    request.append("Name", $('#add_name').val());
    request.append("ContractNo", $('#add_contractno').val())
    request.append("Address", $('#add_address').val());
    request.append("ProvinceId", $('#add_province').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            location.reload();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#edit_btn').click(function () {
    var id = $('#edit_id').val();
    var url = baseUrl + 'update/' + id;
    var request = new FormData();
    request.append("CustomerId", $('#edit_customer').val());
    request.append("Name", $('#edit_name').val());
    request.append("ContractNo", $('#edit_contractno').val());
    request.append("Address", $('#edit_address').val());
    request.append("ProvinceId", $('#edit_province').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            location.reload();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
function createCode(id, codes) {
    var html = '';
    html += '<div class="accordion" id="code_dropdown_' + id + '">';
    html += '<div class="accordion-item">';
    html += '<h2 class="accordion-header" id="code_headingOne_' + id + '">';
    html += '<button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#code_collapseOne_' + id + '" aria-expanded="false" aria-controls="code_collapseOne_' + id + '">';
    html += '<b>Code โครงการ</b></button></h2>';
    html += '<div id="code_collapseOne_' + id + '" class="accordion-collapse collapse" aria-labelledby="code_headingOne_' + id + '">';
    html += '<div class="accordion-body">';
    html += '<div class="row"><div class="col-12 p-3" style="overflow-x:auto;">';
    html += '<table class="table table-hover">';
    html += '<thead>';
    html += '<tr><th>Code</th><th>กลุ่มตุ้นทุน</th></tr>';
    html += '</thead>';
    html += '<tbody>';
    for (var i in codes) {
        var code = codes[i];
        html += '<tr><td>' + code.Code + '</td><td>' + code.Detail + '</td></tr>';
    }
    html += '</tbody>';
    html += '</table>';
    html += '</div></div></div></div>';
    return html;
}
function createUser(id, users) {
    var html = '';
    html += '<div class="accordion" id="user_dropdown_' + id + '">';
    html += '<div class="accordion-item">';
    html += '<h2 class="accordion-header" id="user_headingOne_' + id + '">';
    html += '<button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#user_collapseOne_' + id + '" aria-expanded="false" aria-controls="user_collapseOne_' + id + '">';
    html += '<b>ผู้ใช้งานโครงการ</b></button></h2>';
    html += '<div id="user_collapseOne_' + id + '" class="accordion-collapse collapse" aria-labelledby="user_headingOne_' + id + '">';
    html += '<div class="accordion-body">';
    html += '<div class="row"><div class="col-12 p-3" style="overflow-x:auto;">';
    html += '<table class="table table-hover">';
    for (var i in users) {
        var user = users[i];
        html += '<tr><td>' + user.Fullname + '</td></tr>';
    }
    html += '</table>';
    html += '</div></div></div></div>';
    return html;
}
function createContractor(id, contractors) {
    var html = '';
    html += '<div class="accordion" id="contractor_dropdown_' + id + '">';
    html += '<div class="accordion-item">';
    html += '<h2 class="accordion-header" id="contractor_headingOne_' + id + '">';
    html += '<button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#contractor_collapseOne_' + id + '" aria-expanded="false" aria-controls="contractor_collapseOne_' + id + '">';
    html += '<b>ผู้รับเหมาโครงการ</b></button></h2>';
    html += '<div id="contractor_collapseOne_' + id + '" class="accordion-collapse collapse" aria-labelledby="contractor_headingOne_' + id + '">';
    html += '<div class="accordion-body">';
    html += '<div class="row"><div class="col-12 p-3" style="overflow-x:auto;">';
    html += '<table class="table table-hover">';
    for (var i in contractors) {
        var contractor = contractors[i];
        html += '<tr><td>' + contractor.Name + '</td></tr>';
    }
    html += '</table>';
    html += '</div></div></div></div>';
    return html;
}
function search(projects) {
    $('#project_table').empty();
    var html = '';
    for (var i in projects) {
        var project = projects[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>';
        html += '<span class="material-symbols-outlined" style="color:#0752AE;" onclick="editClick(' + project.Id + ');" data-bs-toggle="modal" data-bs-target="#edit-modal">edit</span>';
        html += '<span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteClick(' + project.Id + ', \'' + project.Name + '\');" data-bs-toggle="modal" data-bs-target="#delete-modal">delete</span>';
        html += '</td>';
        html += '<td>' + project.Name + '</td>';
        html += '<td>' + project.ContractNo + '</td>';
        html += '<td>' + project.Address + '</td>';
        html += '<td>' + project.ProvinceName + '</td>';
        html += '<td>' + project.CustomerName + '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="showCode(' + project.Id + ')" data-bs-toggle="modal" data-bs-target="#code-add-modal">open_in_new</span></td>';
        html += '<td id="code_row_' + project.Id + '">';
        html += createCode(project.Id, project.Codes);
        html += '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="showUser(' + project.Id + ')" data-bs-toggle="modal" data-bs-target="#user-add-modal">open_in_new</span></td>';
        html += '<td id="user_row_' + project.Id + '">';
        html += createUser(project.Id, project.Users);
        html += '</td>';
        html += '<td><span class="material-symbols-outlined" onclick="showContractor(' + project.Id + ')" data-bs-toggle="modal" data-bs-target="#contractor-add-modal">open_in_new</span></td>';
        html += '<td id="contractor_row_' + project.Id + '">';
        html += createContractor(project.Id, project.Contractors);
        html += '</td>';
        html += '</tr>';
    }
    $('#project_table').append(html);
}
$(document).ready(function () {
    console.log('ready', model);
    $('#project_total').html('Total Projects : ' + model.Total);
    bindProvince();
    bindCustomer();
    bindUser();
    bindContractor();
    $('#pagina').pagination({
        dataSource: model.Projects,
        pageSize: defaultPageSize,
        showGoInput: true,
        showGoButton: true,
        callback: function (data, pagination) {
            console.log(data, pagination, currentPage);
            if (pagination.pageNumber != currentPage) {
                currentPage = pagination.pageNumber;
                search(data);
            }
        }
    });
});