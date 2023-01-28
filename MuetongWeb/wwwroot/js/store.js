var defaultPageSize = 10;
var accounts = [];
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
function addClick() {
    $('#add_name').val('');
    $('#add_taxno').val('');
    $('#add_phone').val('');
    $('#add_address').val('');
    $('#add_province').val('');
    $('#add_contractname').val('');
    $('#add_email').val('');
}
function editClick(id) {
    $('#edit_id').val(id);
    var response = model.Stores.find(x => x.Id == id);
    $('#edit_name').val(response.Name);
    $('#edit_taxno').val(response.TaxNo);
    $('#edit_phone').val(response.PhoneNo);
    $('#edit_address').val(response.Address);
    $('#edit_province').val(response.ProvinceId);
    $('#edit_contractname').val(response.ContractName);
    $('#edit_email').val(response.Email);
    $('#edit_accounts').empty();
    $('#account_pane').hide();
    if (response.Payments != null && response.Payments.length > 0) {
        accounts = response.Payments;
        createAccountTable();
    }
}
function deleteClick(id, name) {
    $('#delete_id').val(id);
    $('#delete_text').html('ท่านต้องการลบร้านค้า ' + name + ' ใช่หรือไม่ ?');
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
    request.append("Name", $('#add_name').val());
    request.append("Address", $('#add_address').val());
    request.append("ProvinceId", $('#add_province').val());
    request.append("PhoneNo", $('#add_phone').val());
    request.append("TaxNo", $('#add_taxno').val());
    request.append("ContractName", $('#add_contractname').val());
    request.append("Email", $('#add_email').val());
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
    request.append("Name", $('#edit_name').val());
    request.append("Address", $('#edit_address').val());
    request.append("ProvinceId", $('#edit_province').val());
    request.append("PhoneNo", $('#edit_phone').val());
    request.append("TaxNo", $('#edit_taxno').val());
    request.append("ContractName", $('#edit_contractname').val());
    request.append("Email", $('#edit_email').val());
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
function createAccountTable() {
    $('#edit_accounts').empty();
    $('#account_pane').hide();
    var html = '';
    for (var i in accounts) {
        var account = accounts[i];
        html += '<tr>';
        html += '<td>' + (parseInt(i) + 1) + '</td>';
        html += '<td>';
        if (account.CanDelete) {
            html += '<span class="material-symbols-outlined" style="color:#0752AE;" onclick="editAccountClick(' + i + ');">edit</span>';
            html += '<span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteAccountClick(' + account.Id + ', \'' + account.AccountNo + '\');">delete</span>';
        } else {
            html += '<span class="material-symbols-outlined" style="color:#0752AE;" onclick="editAccountClick(' + i + ');">edit</span>';
        }
        html += '</td>';
        html += '<td>' + account.AccountNo + '</td>';
        html += '<td>' + account.AccountName + '</td>';
        html += '<td>' + account.Bank + '</td>';
        html += '<td>' + account.Type + '</td>';
        html += '</tr>';
    }
    $('#edit_accounts').append(html);
}
function addAccountClick() {
    $('#account_pane').show();
    $('#edit_account_no').val('');
    $('#edit_account_name').val('');
    $('#edit_bank').val('');
    $('#edit_type').val('ออมทรัพย์');
    $('#edit_account_id').val(0);
    $('#edit_account_edit_btn').hide();
    $('#edit_account_add_btn').show();
}
function editAccountClick(i) {
    $('#account_pane').show();
    var account = accounts[i];
    $('#edit_account_no').val(account.AccountNo);
    $('#edit_account_name').val(account.AccountName);
    $('#edit_bank').val(account.Bank);
    $('#edit_type').val(account.Type);
    $('#edit_account_id').val(account.Id);
    $('#edit_account_edit_btn').show();
    $('#edit_account_add_btn').hide();
}
function deleteAccountClick(id, no) {
    if (confirm("คุณต้องการลบบัญชี " + no + " ใช่หรือไม่ ?")) {
        var storeid = $('#edit_id').val();
        var url = baseUrl + storeid + '/account/delete/' + id;
        $.ajax({
            type: "POST",
            url: url,
            contentType: false,
            processData: false,
            success: function (result) {
                var index = accounts.findIndex(x => x.Id == id);
                accounts.splice(index, 1);
                createAccountTable();
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });
    }
}
$('#edit_account_add_btn').click(function () {
    var storeid = $('#edit_id').val();
    var url = baseUrl + storeid + '/account/add';
    var request = new FormData();
    request.append("StoreId", storeid);
    request.append("AccountNo", $('#edit_account_no').val());
    request.append("AccountName", $('#edit_account_name').val());
    request.append("Bank", $('#edit_bank').val());
    request.append("Type", $('#edit_type').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            accounts.push({
                Id: result,
                StoreId: storeid,
                AccountNo: $('#edit_account_no').val(),
                AccountName: $('#edit_account_name').val(),
                Bank: $('#edit_bank').val(),
                Type: $('#edit_type').val(),
            });
            createAccountTable();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
$('#edit_account_edit_btn').click(function () {
    var storeid = $('#edit_id').val();
    var id = $('#edit_account_id').val();
    var url = baseUrl + storeid + '/account/update/' + id;
    var request = new FormData();
    request.append("StoreId", storeid);
    request.append("AccountNo", $('#edit_account_no').val());
    request.append("AccountName", $('#edit_account_name').val());
    request.append("Bank", $('#edit_bank').val());
    request.append("Type", $('#edit_type').val());
    $.ajax({
        type: "POST",
        url: url,
        contentType: false,
        processData: false,
        data: request,
        success: function (result) {
            var account = accounts.find(x => x.Id == id);
            account.AccountNo = $('#edit_account_no').val();
            account.AccountName = $('#edit_account_name').val();
            account.Bank = $('#edit_bank').val();
            account.Type = $('#edit_type').val();
            createAccountTable();
        },
        error: function (xhr, status, p3, p4) {
            var err = "Error " + " " + status + " " + p3 + " " + p4;
            if (xhr.responseText && xhr.responseText[0] == "{")
                err = JSON.parse(xhr.responseText).Message;
            console.log(err);
        }
    });
});
function search(page) {
    $('#store_table').empty();
    var html = '';
    var items = model.Stores.slice(page * defaultPageSize, (page + 1) * defaultPageSize);
    for (var i in items) {
        var item = items[i];
        html += '<tr>';
        html += '<td>' + ((page * defaultPageSize) + parseInt(i) + 1) + '</td>';
        html += '<td>';
        if (item.CanDelete) {
            html += '<span class="material-symbols-outlined" style="color:#0752AE;" onclick="editClick(' + item.Id + ');" data-bs-toggle="modal" data-bs-target="#edit-modal">edit</span>';
            html += '<span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteClick(' + item.Id + ', \'' + item.Name + '\');" data-bs-toggle="modal" data-bs-target="#delete-modal">delete</span>';
        } else {
            html += '<span class="material-symbols-outlined" style="color:#0752AE;" onclick="editClick(' + item.Id + ');" data-bs-toggle="modal" data-bs-target="#edit-modal">edit</span>';
        }
        html += '</td>';
        html += '<td>' + item.Name + '</td>';
        html += '<td>' + item.Address + '</td>';
        html += '<td>' + item.ProvinceName + '</td>';
        html += '<td>' + item.PhoneNo + '</td>';
        html += '<td>' + item.TaxNo + '</td>';
        html += '<td>' + item.ContractName + '</td>';
        html += '<td>' + item.Email + '</td>';
        html += '</tr>';
    }
    $('#store_table').append(html);
}
$(document).ready(function () {
    console.log(model);
    bindProvince();
    search(currentPage - 1);
    $('#pagina').pagination({
        dataSource: model.Stores,
        pageSize: defaultPageSize,
        pageNumber: currentPage,
        showGoInput: true,
        showGoButton: true,
        callback: function (data, pagination) {
            console.log(data, pagination, currentPage);
            if (pagination.pageNumber != currentPage) {
                currentPage = pagination.pageNumber;
                search(pagination.pageNumber - 1);
            }
        }
    });
});