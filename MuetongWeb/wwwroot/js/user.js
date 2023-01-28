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
function bindLine() {
    var url = baseUrl + 'workline';
    $('#add_line').empty();
    $('#edit_line').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_line').append(html);
                $('#edit_line').append(html);
            }
            bindDepartment($('#edit_line').val(), 'edit');
            bindDepartment($('#add_line').val(), 'add');
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindDepartment(id, name) {
    var url = baseUrl + 'department/' + id;
    $('#' + name + '_department').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#' + name + '_department').append(html);
            }
            bindSubdepartment($('#' + name + '_department').val(), name);
        })
        .fail(function (response) {
            console.log(response);
        });
}
function bindSubdepartment(id, name) {
    var url = baseUrl + 'subdepartment/' + id;
    $('#' + name + '_subdepartment').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#' + name + '_subdepartment').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
function setDepartment(lineId, deId, subId) {
    var url = baseUrl + 'department/' + lineId;
    $('#edit_department').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response) {
                var item = response[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#edit_department').append(html);
            }
            $('#edit_department').val(deId);
            var suburl = baseUrl + 'subdepartment/' + deId;
            $('#edit_subdepartment').empty();
            var jqxhr = $.get(suburl)
                .done(function (response) {
                    console.log(response);
                    for (var i in response) {
                        var item = response[i];
                        var html = '<option value="' + item.id + '">' + item.name + '</option>';
                        $('#edit_subdepartment').append(html);
                    }
                    $('#edit_subdepartment').val(subId);
                })
                .fail(function (response) {
                    console.log(response);
                });
        })
        .fail(function (response) {
            console.log(response);
        });
}
$('#edit_line').change(function () {
    bindDepartment($('#edit_line').val(), 'edit');
});
$('#add_line').change(function () {
    bindDepartment($('#add_line').val(), 'add');
});
$('#edit_department').change(function () {
    bindSubdepartment($('#edit_department').val(), 'edit');
});
$('#add_department').change(function () {
    bindSubdepartment($('#add_department').val(), 'add');
});
function addClick() {
    $('#add_username').val('');
    $('#add_firstname').val('');
    $('#add_lastname').val('');
    $('#add_idcard').val('');
    $('#add_phone').val('');
    $('#add_address').val('');
    $('#add_province').val(1);
    $('#add_employeeid').val('');
    $('#add_email').val('');
}
function editClick(id) {
    $('#edit_id').val(id);
    var url = baseUrl + id;
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            $('#edit_username').val(response.username);
            $('#edit_firstname').val(response.firstname);
            $('#edit_lastname').val(response.lastname);
            $('#edit_idcard').val(response.citizenId);
            $('#edit_phone').val(response.phoneNo);
            $('#edit_address').val(response.address);
            $('#edit_province').val(response.provinceId);
            $('#edit_employeeid').val(response.employeeId);
            $('#edit_email').val(response.email);
            $('#edit_line').val(response.lineId);
            setDepartment(response.lineId, response.departmentId, response.subDepartmentId);
        })
        .fail(function (response) {
            console.log(response);
        });
}
function deleteClick(id, username) {
    $('#delete_id').val(id);
    $('#delete_text').html('ท่านต้องการลบผู้ใช้งาน ' + username + ' ใช่หรือไม่ ?');
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
            search('', 1);
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
    request.append("Username", $('#add_username').val());
    request.append("Firstname", $('#add_firstname').val());
    request.append("Lastname", $('#add_lastname').val());
    request.append("Idcard", $('#add_idcard').val());
    request.append("Phone", $('#add_phone').val());
    request.append("Address", $('#add_address').val());
    request.append("ProvinceId", $('#add_province').val());
    request.append("EmployeeId", $('#add_employeeid').val());
    request.append("Email", $('#add_email').val());
    request.append("RoleId", $('#add_role').val());
    request.append("SubDepartmentId", $('#add_subdepartment').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            search('', 1);
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
    request.append("Firstname", $('#edit_firstname').val());
    request.append("Lastname", $('#edit_lastname').val());
    request.append("Idcard", $('#edit_idcard').val());
    request.append("Phone", $('#edit_phone').val());
    request.append("Address", $('#edit_address').val());
    request.append("ProvinceId", $('#edit_province').val());
    request.append("EmployeeId", $('#edit_employeeid').val());
    request.append("Email", $('#edit_email').val());
    request.append("RoleId", $('#edit_role').val());
    request.append("SubDepartmentId", $('#edit_subdepartment').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            search('', 1);
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
function search(query, page) {
    $('#Query').val(query);
    $('#Page').val(page);
    $('#PageSize').val(defaultPageSize);
    $('#search').submit();
}
$(document).ready(function () {
    console.log(model);
    bindProvince();
    bindLine();
    $('#pagina').pagination({
        dataSource: model.Pages,
        pageSize: 1,
        pageNumber: currentPage,
        showGoInput: true,
        showGoButton: true,
        callback: function (data, pagination) {
            console.log(data, pagination, currentPage);
            if (pagination.pageNumber != currentPage)
                search('', pagination.pageNumber);
        }
    });
});