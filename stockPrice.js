document.getElementById("get-request").addEventListener('click', asyncRequest);

var stockPrice = document.querySelector("#response-info");
var request = new XMLHttpRequest();

function asyncRequest() {
    request.open("GET", "https://www.cbr-xml-daily.ru/daily_json.js", true);
    request.send();

    request.onload = function () {
        if (request.status != 200) {
            alert("Error ${request.status}: ${request.statusText}");
        } else {
            var stock = JSON.parse(request.responseText);
            stockPrice.innerHTML = "<h3>" + stock.Valute.USD.Value + " RUB</h3>";
        }
    };
}