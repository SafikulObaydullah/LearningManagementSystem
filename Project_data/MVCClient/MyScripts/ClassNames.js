$(document).ready(function () {
    loadData();
    /*$("#h2").html("Add Employee");*/
})
function SaveProduct() {
    var url = "https://localhost:44372/api/ClassNames";
    var objectProduct = {
        Name: $().val(),
        Price: $("#txtProductPrice").val(),
        Quantity: $("#txtProductQuantity").val(),
        Active: 1
    }
    if (objectProduct) {
        $.ajax({
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(objectProduct),
            type: "Post",
            success: function (result) {
                clear();
                getProductData();
                /*alert(result);*/

            },
            error: function (msg) {
                alert(msg);
            }
        });
    }
}