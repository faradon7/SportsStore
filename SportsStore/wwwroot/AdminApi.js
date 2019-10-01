$(document).ready(function () {
    $.ajax({
        url: "../api/AdminApi",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        //data: JSON.stringify({
        //    productid: this.elements["ProductID"].value,
        //    name: this.elements["Name"].value,
        //    price: this.elements["Price"].value
        //}),
        success: function (result) {
            addTaЬleRow(result);
        }
    });
});

var addTaЬleRow = function (products) {
    for (var p in products) {
        $("table").append("<tr id = '" + products[p].productID + "'><td class='text-right'>" + products[p].productID
            + "</td><td>"
            + products[p].name + "</td><td class='text-right'>"
            + products[p].price + "  ₽</td><td class='text-center'>"
            + "<form>"
            + "<a id='Edit' class='btn btn-sm btn-warning'>Edit</a>"
            + "<input type='hidden' name='ProductID' value='products[p].productID' />"
            + "<button id = '" + products[p].productID + "' type = 'submit'"
            + "class= 'ml-1 btn btn-danger btn-sm removeLink' > Delete</button > "
            + "</form></td></tr>");
    }
    $("body").append("<div class='text-center'>"
        + "<a href='#' class='btn btn-primary'>Add Product</a>"
        + "</div>"
    )
}

function DeleteUser(id) {
    $.ajax({
        url: "../api/AdminApi/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function (id) {
            $("tr[id='" + id + "']").remove();
        }
    })
}

// нажимаем на ссылку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).attr('id');
    DeleteUser(id);
});