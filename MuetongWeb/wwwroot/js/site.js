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