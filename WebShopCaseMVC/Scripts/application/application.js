/*
 * Loads a complete HTML with a list from the server 
 */
function loadList(page) {
        
        var articleDiv = $("#articlesList");
        $.ajax({
            cache: false,
            type: "GET",
            url: "/Home/ArticleList",
            data: { "id": page },
            success: function (data) {
                articleDiv.html('');
                articleDiv.html(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve articles.');
            }
        });
    }

/*
 * Loads a JSON from the server and changes the text is some divs. All using JQuery
 */
function loadDetail(articleId) {
        
    var articleDetailDiv = $("#articleDetailList");
    articleDetailDiv.hide();
    $.ajax({
        cache: false,
        type: "GET",
        url: "/api/Article",
        data: { "id": articleId },
        success: function (data) {
                           

            if(data!=null)
            {
                $('#detailName').text(data.Name);
                $('#detailPriceWithoutVat').text(data.TotalWithoutVAT);
                $('#detailPriceWithVat').text(data.TotalWithVAT);
                $('#detailDescription').text(data.Description);
                $("#addItemToCart").attr("onClick", "addItemToCart(" + data.Id + ",1)");

                articleDetailDiv.show();
            }


        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve article details.');
        }
    });
}


/*
 * Adds an item to the cart through Ajax.
 */
function addItemToCart(id,quantity) {
                       
    $.ajax({
        cache: false,
        type: "POST",
        url: "/Home/addItemToCart",
        data: { "id": id,"quantity":quantity },
        success: function (data) {
            alert('added to cart');
            $('#cart').text("Cart("+data.Count+")");
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve articles.');
        }
    });
}


/*
 * Removes an item to the cart through Ajax.
 */
function removeItemFromCart(id,thisTD) { 

    $.ajax({
        cache: false,
        type: "DELETE",
        url: "/Home/removeItemFromCart",
        data: { "id": id },
        success: function (data) {
            alert('removed from cart');
            $('#cart').text("Cart(" + data.Count + ")");
            
            thisTD.closest('tr').remove();

                     
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve articles.');
        }
    });


}


