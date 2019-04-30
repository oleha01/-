function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}

function chanGeQuantity(value,i) {
    var Cart = getCookie("Cart");
    var arr = JSON.parse(Cart);
    arr.Lines[i].Quantity = value;
    var json = JSON.stringify(arr);
    var date = new Date(new Date().getTime() + (60 * 1000) * 60);
    var s = "Cart1=" + json + "; path=/; secure=true;";
    document.cookie = s;
}
function CartSumm() {   
    var Cart = getCookie("Cart");
    var arr = JSON.parse(Cart);
    var summ = 0;
    var i = 0;
   for (i = 0; i < arr.Lines.length; i++)
    {
       summ = +summ + +arr.Lines[i].Quantity * +arr.Lines[i].Product.SellingPrice;
    }
    document.getElementById("cartSumm").innerHTML = summ;
}

function removeHidden() {
    $('.cart_check_confirm_buy').removeClass('hidden');
    $('.cart_check_confirm_buy_background').removeClass('hidden');
}
function AddHidden() {
    $('.cart_check_confirm_buy').addClass('hidden');
    $('.cart_check_confirm_buy_background').addClass('hidden');
}
$(document).ready(function () { CartSumm();});