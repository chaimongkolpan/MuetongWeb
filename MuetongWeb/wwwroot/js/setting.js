$('#bill_receive_btn').click(function () {
    addSetting($('#bill_receive_name').val(), 'ReceiveBilling');
});
$('#receipt_receive_btn').click(function () {
    addSetting($('#receipt_receive_name').val(), 'ReceiveReceipt');
});
$('#credit_type_btn').click(function () {
    addSetting($('#credit_type_name').val(), 'CreditType');
});
$('#payment_type_btn').click(function () {
    addSetting($('#payment_type_name').val(), 'PaymentType');
});
$('#extra_type_btn').click(function () {
    addSetting($('#extra_type_name').val(), 'ExtraType');
});
function addSetting(name, type) {
    console.log('add', name, type);
    var url = baseUrl + 'add';
    var jqxhr = $.post(url, {name: name, type: type})
        .done(function (response) {
            console.log(response);
            alert('บันทึกสำเร็จ');
            bindData();
        })
        .fail(function (response) {
            console.log(response);
            alert('บันทึกไม่สำเร็จ');
        });
}
function deleteSetting(id) {
    console.log('delete', id);
    var url = baseUrl + 'delete/' + id;
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            alert('บันทึกสำเร็จ');
            bindData();
        })
        .fail(function (response) {
            console.log(response);
            alert('บันทึกไม่สำเร็จ');
        });
}
function bindData() {
    var url = baseUrl + 'all';
    $('#bill_receive_table').empty();
    $('#receipt_receive_table').empty();
    $('#credit_type_table').empty();
    $('#payment_type_table').empty();
    $('#extra_type_table').empty();
    var jqxhr = $.get(url)
        .done(function (response) {
            console.log(response);
            for (var i in response.billingReceive) {
                var item = response.billingReceive[i];
                var html = '';
                html += '<tr>';
                html += '<td>' + (parseInt(i) + 1) + '</td>';
                html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteSetting(' + item.id + ');">delete</span></td>';
                html += '<td>' + item.name + '</td>';
                html += '</tr>';
                $('#bill_receive_table').append(html);
            }
            for (var i in response.receiptReceive) {
                var item = response.receiptReceive[i];
                var html = '';
                html += '<tr>';
                html += '<td>' + (parseInt(i) + 1) + '</td>';
                html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteSetting(' + item.id + ');">delete</span></td>';
                html += '<td>' + item.name + '</td>';
                html += '</tr>';
                $('#receipt_receive_table').append(html);
            }
            for (var i in response.creditType) {
                var item = response.creditType[i];
                var html = '';
                html += '<tr>';
                html += '<td>' + (parseInt(i) + 1) + '</td>';
                html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteSetting(' + item.id + ');">delete</span></td>';
                html += '<td>' + item.name + '</td>';
                html += '</tr>';
                $('#credit_type_table').append(html);
            }
            for (var i in response.paymentType) {
                var item = response.paymentType[i];
                var html = '';
                html += '<tr>';
                html += '<td>' + (parseInt(i) + 1) + '</td>';
                html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteSetting(' + item.id + ');">delete</span></td>';
                html += '<td>' + item.name + '</td>';
                html += '</tr>';
                $('#payment_type_table').append(html);
            }
            for (var i in response.extraType) {
                var item = response.extraType[i];
                var html = '';
                html += '<tr>';
                html += '<td>' + (parseInt(i) + 1) + '</td>';
                html += '<td><span class="material-symbols-outlined" style="color:#A42206;" onclick="deleteSetting(' + item.id + ');">delete</span></td>';
                html += '<td>' + item.name + '</td>';
                html += '</tr>';
                $('#extra_type_table').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
$(document).ready(function () {
    console.log('ready', model);
    bindData();
});