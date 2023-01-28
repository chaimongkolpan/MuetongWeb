var defaultPageSize = 10;

function addClick() {
    $('#add_name').val('');
    $('#add_unit').val('');
}
function editClick(id) {
    $('#edit_id').val(id);
    var response = model.find(x => x.Id == id);
    $('#edit_name').val(response.Name);
    $('#edit_unit').val(response.Unit);
}
function deleteClick(id, name) {
    $('#delete_id').val(id);
    $('#delete_text').html('ท่านต้องการลบสินค้า ' + name + ' ใช่หรือไม่ ?');
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
    request.append("Unit", $('#add_unit').val());
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
    request.append("Unit", $('#edit_unit').val());
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
function search(page) {
    $('#product_table').empty();
    var html = '';
    var items = model.slice(page * defaultPageSize, (page + 1) * defaultPageSize);
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
        html += '<td>' + item.Unit + '</td>';
        html += '</tr>';
    }
    $('#product_table').append(html);
}
$(document).ready(function () {
    console.log(model);
    search(currentPage - 1);
    $('#pagina').pagination({
        dataSource: model,
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