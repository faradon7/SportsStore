$(document).ready(function () {
    $("form").submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "../api/AccountApi",
            contentType: "application/json",
            method: "POST",
            data: JSON.stringify({
                name: this.elements["Name"].value,
                password: this.elements["Password"].value
            }),
            success: function (data) {
                window.location.href = "http://localhost:53114/AdminApi/Index";
                //$.ajax({
                //    url: "../api/AdminApi",
                //    contentType: "application/json",
                //    type: "GET"
                //});
            }
        })
    });
});
