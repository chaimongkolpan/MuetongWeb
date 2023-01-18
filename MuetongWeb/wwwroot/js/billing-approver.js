var stores = [];


function bindStore() {
    var storeUrl = baseUrl + 'store';
    $('#add_store').empty();
    var jqxhr = $.get(storeUrl)
        .done(function (response) {
            console.log(response);
            stores = response.stores;
            for (var i in response.stores) {
                var item = response.stores[i];
                var html = '<option value="' + item.id + '">' + item.name + '</option>';
                $('#add_store').append(html);
            }
        })
        .fail(function (response) {
            console.log(response);
        });
}
$(document).ready(function () {
    console.log('ready', model);

    bindStore();
    // search();
});