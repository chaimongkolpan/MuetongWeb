var alertBorder = '2px solid #ff2020';
var normalBorder = '1px solid #ced4da';
var url = window.location.pathname;
if ((url.includes("/Po") || url.includes("/po"))
    && !(url.includes("/Approver") || url.includes("/approver"))) {
    $('#po-index').addClass("active");
} else if ((url.includes("/Po") || url.includes("/po"))
    && (url.includes("/Approver") || url.includes("/approver"))) {
    $('#po-check').addClass("active");
} else if ((url.includes("/Billing") || url.includes("/billing"))
    && !(url.includes("/Approver") || url.includes("/approver"))) {
    $('#billing-index').addClass("active");
} else if ((url.includes("/Billing") || url.includes("/billing"))
    && (url.includes("/Approver") || url.includes("/approver"))) {
    $('#billing-check').addClass("active");
} else if (url.includes("/Setting") || url.includes("/setting")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-setting').addClass("active");
} else if ((url.includes("/User") || url.includes("/user"))
    && !(url.includes("/ChangePassword") || url.includes("/changepassword"))) {
    $('#admin-dropdown').addClass("active");
    $('#admin-user').addClass("active");
} else if (url.includes("/Role") || url.includes("/role")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-role').addClass("active");
} else if (url.includes("/WorkLine") || url.includes("/workline")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-workline').addClass("active");
} else if (url.includes("/Customer") || url.includes("/customer")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-customer').addClass("active");
} else if (url.includes("/Contractor") || url.includes("/contractor")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-contractor').addClass("active");
} else if (url.includes("/Project") || url.includes("/project")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-project').addClass("active");
} else if (url.includes("/Store") || url.includes("/store")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-store').addClass("active");
} else if (url.includes("/Product") || url.includes("/product")) {
    $('#admin-dropdown').addClass("active");
    $('#admin-product').addClass("active");
} else if ((url.includes("/Pr") || url.includes("/pr"))
    && !(url.includes("/Approver") || url.includes("/approver"))
    && !(url.includes("/Receive") || url.includes("/receive"))) {
    $('#pr-index').addClass("active");
} else if ((url.includes("/Pr") || url.includes("/pr"))
    && (url.includes("/Approver") || url.includes("/approver"))) {
    $('#pr-check').addClass("active");
} else if ((url.includes("/Pr") || url.includes("/pr"))
    && (url.includes("/Receive") || url.includes("/receive"))) {
    $('#receive-check').addClass("active");
} else if ((url.includes("/User") || url.includes("/user"))
    && (url.includes("/ChangePassword") || url.includes("/changepassword"))) {
    $('#admin-dropdown').addClass("active");
    $('#admin-password').addClass("active");
}
function isEmpty(text) {
    return text == null || text == '' || text.length == 0;
}
function setBorderRed(id) {
    $('#' + id).css("border", alertBorder);
}
function setBorderNormal(id) {
    $('#' + id).css("border", normalBorder);
}
function checkTextInput(id) {
    $('#' + id).focusout(function () {
        if (isEmpty(this.value)) $(this).css("border", alertBorder);
        else $(this).css("border", normalBorder);
    });
}
function dateValue(date) {
    if (date == null) return '';
    var dateTime = new Date(date);
    var y = dateTime.getFullYear();
    var m = dateTime.getMonth() + 1;
    var d = dateTime.getDate();
    return y + '-' + ('0' + m).slice(-2) + '-' + ('0' + d).slice(-2);
}
function dateFormat(date) {
    if (date == null || date == '') return '';
    var dateTime = new Date(date);
    var y = dateTime.getFullYear() + 543;
    var m = dateTime.getMonth() + 1;
    var d = dateTime.getDate();
    return d + '/' + m + '/' + y;
}
function floatValue(num) {
    var tmp = parseFloat(num);
    if (isNaN(tmp))
        return 0;
    return tmp;
}
function floatFormat(num) {
    var tmp = parseFloat(num);
    if (isNaN(tmp))
        return '0.00';
    return tmp.toFixed(2);
}
function createAutocomplete(id) {
    const sorting = document.getElementById(id);
    try {
        const sortingchoices = new Choices(sorting, {
            placeholder: false,
            itemSelectText: '',
            sorter: function(a, b) {
                //return b.label.length - a.label.length;
                return b.label == 'ทั้งหมด' ? 99999 : a.label == 'ทั้งหมด' ? -99999 : a.label < b.label ? -1 : 1;
            }
        });
        let sortingClass = sorting.getAttribute('class');
        sorting.parentElement.setAttribute('class', sortingClass);
        return sortingchoices;
    } catch (e) {
        console.log('error choices', e);
    }
}
function dynamicCreateAutocomplete(pane, id, text, selectCode) {
    try {

        $('#' + pane).empty();
        $('#' + pane).append('<label class="input-group-text" for="' + id + '"><b>' + text + '</b></label>');
        $('#' + pane).append(selectCode);
        return createAutocomplete(id);
    } catch (e) {
        console.log('error choices', e);
    }
}
$(document).ready(function () {
    var jqxhr = $.get(mainUrl)
    .done(function (response) {
        console.log(response);
        $('#main_username').html(response.username);
        for (var i in response.permissions) {
            var permission = response.permissions[i];
            $('.' + permission.name).show();
        }
    })
    .fail(function (response) {
        console.log(response);
        alert(response);
        location.href = logoutUrl;
    });
});