$(document).ready(function () {
    $("#show").click(function () {
        $.ajax({
            url: "../api/AdminApi",
            type: "GET",
            contentType: "application/json",
            dataType: "json",
            success: function (result) {
                generateTable(result);
            }
        });
        $("#show").remove();
    });
}
);

var generateTable = function (products) {
    for (var n in products) {
        addProductRow(products[n]);
    }
    $("body").append("<div class='text-center'>"
        + "<a href='#' class='btn btn-primary addLink'>Add Product</a>"
        + "</div>"
    )
}

    function addProductRow(p) {
        $("table").append(
            "<tr id = '" + p.productID + "'>"
            + "<td class='text-right'>" + p.productID + "</td>"
            + "<td>" + p.name + "</td>"
            + "<td class='text-right'>" + p.price + "  ₽</td>"
            + "<td class='text-center'>"
            + "<form>"
            + "<a id='Edit' class='btn btn-sm btn-warning editLink'>Edit</a>"
            + "<input type='hidden' name='ProductID' value='products[p].productID' />"
            + "<button id = '" + p.productID + "' type = 'submit'"
            + "class= 'ml-1 btn btn-danger btn-sm removeLink' > Delete</button > "
            + "</form>"
            + "</td>"
            + "</tr>");
    }

function DeleteProduct(id) {
    $.ajax({
        url: "../api/AdminApi/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function () {
            $("tr[id='" + id + "']").remove();
        }
    });
}

function AddProduct() {

}

function EditProduct(id) {
    $.ajax({
        url: "../api/AdminApi/" + id,
        contentType: "application/json",
        method: "DELETE",
        success: function () {
            $("tr[id='" + id + "']").remove();
        }
    });
}

// //нажимаем кнопку сохранить
//$("body").on("click", ".saveLink", function () {
//    CreateProduct()
//});


// нажимаем на кнопку Удалить
$("body").on("click", ".removeLink", function () {
    var id = $(this).attr('id');
    DeleteProduct(id);
});

// Edit Product Button
$("body").on("click", ".editLink", function () {
    var id = $(this).attr('id');
    EditProduct(id);
});

//add Product Button
$("body").on("click", ".addLink", function () {
    $(".productsTable").hide();
    $(this).hide();
    addProductForm();
});

var addProductForm = function () {
    $("body").append(
        "<form id = 'add'>"
        + "<input type='hidden' data-val='true' data-val-required='The ProductID field is required.' id='ProductID' name='ProductID' value='0' />"
        + "<div class='form-group'>"
        + "<label for='Name'>Name</label>"
        + "<div><span class='text-danger field-validation-valid' data-valmsg-for='Name' data-valmsg-replace='true'></span></div>"
        + "<input class='form-control' type='text' data-val='true' data-val-required='Please enter a product name' id='Name' name='Name' value='' />"
        + "</div>"
        + "<div class='form-group'>"
        + "<label for='Description'>Description</label>"
        + "<div><span class='text-danger field-validation-valid' data-valmsg-for='Description' data-valmsg-replace='true'></span></div>"
        + "<textarea class='form-control' id='Description' name='Description'>"
        + "</textarea>"
        + "</div>"
        + "<div class='form-group'>"
        + "<label for='Category'>Category</label>"
        + "<div><span class='text-danger field-validation-valid' data-valmsg-for='Category' data-valmsg-replace='true'></span></div>"
        + "<input class='form-control' type='text' id='Category' name='Category' value='' />"
        + "</div>"
        + "<div class='form-group'>"
        + "<label for='Price'>Price</label>"
        + "<div><span class='text-danger field-validation-valid' data-valmsg-for='Price' data-valmsg-replace='true'></span></div>"
        + "<input class='form-control' type='text' data-val='true' data-val-number='The field Price must be a number.' data-val-required='Please enter a positive price' id='Price' name='Price' value='0,00' />"
        + "</div>"
        + "<div class='text-center'>"
        + "<button class='btn btn-primary saveLink' type='submit'>Save</button>"
        + "<a class='ml-1 btn btn-secondary' href='#'>Cancel</a>"
        + "</div>"
        + "</form>"
    );
}

function CreateProduct(productId, productName, productDescription, productCategory, productPrice) {
    $.ajax({
        url: "../api/AdminApi",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            productID: productId,
            name: productName,
            description: productDescription,
            category: productCategory,
            price: productPrice
        }),
        success: function (result) {
            $("#add").hide();
            $(".productsTable").show();
            $(".addLink").show();
            $("table tbody").append(addProductRow(result));
        }
    });
}

$("body").on("click", ".saveLink", function () {
    $("form[id=add]").submit(function (e) {
        e.preventDefault();
        var id = this.elements["ProductID"].value;
        var name = this.elements["Name"].value;
        var description = this.elements["Description"].value;
        var category = this.elements["Category"].value;
        var price = this.elements["Price"].value;

        CreateProduct(id, name, description, category, price);
        //else
        //    EditUser(id, name, age);
    });
});




