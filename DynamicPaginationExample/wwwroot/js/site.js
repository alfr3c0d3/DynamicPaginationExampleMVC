// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#pagination-demo").twbsPagination({
        totalPages: $("#total-pages").val(),
        // the current page that show on start
        startPage: 1,

        // maximum visible pages
        visiblePages: 5,

        // template for pagination links
        href: true,

        // variable name in href template for page number
        pageVariable: "page",

        // Text labels
        first: "First",
        prev: "Previous",
        next: "Next",
        last: "Last",

        // pagination Classes
        paginationClass: "pagination",
        nextClass: "page-item next",
        prevClass: "page-item prev",
        lastClass: "page-item last",
        firstClass: "page-item first",
        pageClass: "page-item",
        activeClass: "active",
        disabledClass: "disabled",
        anchorClass: "page-link"

    });
});