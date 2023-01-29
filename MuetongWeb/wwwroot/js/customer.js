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
function addClick() {
    $('#add_name').val('');
    $('#add_detail').val('');
    $('#add_taxno').val('');
    $('#add_phone').val('');
    $('#add_address').val('');
    $('#add_province').val(1);
    $('#add_email').val('');
    $('#add_branchno').val('');
}
function editClick(id) {
    $('#edit_id').val(id);
    var url = baseUrl + id;
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            $('#edit_name').val(response.name);
            $('#edit_detail').val(response.detail);
            $('#edit_taxno').val(response.taxNo);
            $('#edit_phone').val(response.phoneNo);
            $('#edit_address').val(response.address);
            $('#edit_province').val(response.provinceId);
            $('#edit_email').val(response.email);
            $('#edit_branchno').val(response.branchNo);
        })
        .fail(function (response) {
            console.log(response);
        });
}
function deleteClick(id, name) {
    $('#delete_id').val(id);
    $('#delete_text').html('ท่านต้องการลบลูกค้า ' + name + ' ใช่หรือไม่ ?');
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
    request.append("Name", $('#add_name').val());
    request.append("Detail", $('#add_detail').val());
    request.append("TaxNo", $('#add_taxno').val());
    request.append("PhoneNo", $('#add_phone').val());
    request.append("Address", $('#add_address').val());
    request.append("ProvinceId", $('#add_province').val());
    request.append("BranchNo", $('#add_branchno').val());
    request.append("Email", $('#add_email').val());
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
    request.append("Name", $('#edit_name').val());
    request.append("Detail", $('#edit_detail').val());
    request.append("TaxNo", $('#edit_taxno').val());
    request.append("PhoneNo", $('#edit_phone').val());
    request.append("Address", $('#edit_address').val());
    request.append("ProvinceId", $('#edit_province').val());
    request.append("BranchNo", $('#edit_branchno').val());
    request.append("Email", $('#edit_email').val());
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