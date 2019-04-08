function getCookie(name) {
    var matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;
}
function jsonchik(name) {
    var json = JSON.parse(getCookie(name));
    console.log(json);  
    return json;
}

function bake_cookie(name, value) {
    var cookie = [name, '=', JSON.stringify(value), '; domain=.', window.location.host.toString(), '; path=/;'].join('');
    document.cookie = cookie;
}

function ChangeQuantity(i,value) {
    var s =jsonchik("Cart");
    s.Lines[i].Quantity = value;
    console.log(s);  
    bake_cookie("Cart", s)
}