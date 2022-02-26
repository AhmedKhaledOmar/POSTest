var connection = new signalR.HubConnectionBuilder().withUrl("/productHub").build();

var totalPrice = 0;
var Items = window.localStorage.getItem("products");
var ItemsConvert = JSON.parse(Items);
if (ItemsConvert != null) {
    var length = ItemsConvert.length;
}

$(document).ready(function () {
    
        for (var index = 0; index < length; ++index) {
            Show(ItemsConvert[index], length, index);
        
    }
});


connection.on("showProduct", function (Item, length, index) {
    Show(Item, length, index);
});

connection.on("cancelProduct", function (indexofItem) {
    NewItem = [];
    for (var i = 0; i < length; ++i) {
        if (i == indexofItem)
            continue;
        NewItem.push(ItemsConvert[i]);
    }
    window.localStorage.setItem("products", JSON.stringify(NewItem));
    var elements = document.getElementsByClassName("superClass");
    elements[indexofItem].parentNode.removeChild(elements[indexofItem]);
});


function Show(Item, length, index) {
    totalPrice = totalPrice + Item.productPrice + Item.productSizePrice;

    var productAdded = $('<div class="superClass list-group list-group-flush ">' +
        '<a href=#  class="px-4 py-3 list-group-item d-flex">' +
        '<div class="flex-shrink-0">' +
        '<figure class="avatar mr-3">' +
        '<img class="rounded" src="/images/' + Item.productPicture + '">' +
        '</figure>' +
        '</div>' +
        '<div class="flex-grow-1">' +
        '<p class="mb-0 line-height-20 d-flex justify-content-between">' +
        (Item.productName + '-' + Item.productSizeName) +
        '<i onclick="Cancel(' + index + ')" title="Close" data-toggle="tooltip"' +
        ' class="hide-show-toggler-item small ti-close">' +
        '</i>' +
        '</p>' +
        '<span class="text-muted small">X' + (Item.productPrice + Item.productSizePrice) +
        '</span>' +
        '</div>' +
        '</a>' +
        '</div>');
    $('#newProduct').append(productAdded);
    $('#totalPrice').text(totalPrice);
    $('#countofProduct').text(length + " products");
    return 0;
}

connection.start();

function Cancel(indexofItem)
{
    console.log(indexofItem);
    connection.invoke("CancelProduct",indexofItem);
}





