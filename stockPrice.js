document.getElementById("get-request").addEventListener('click', asyncRequest);

var stockPrice = document.querySelector("#response-info");
var request = new XMLHttpRequest();

function asyncRequest() {
    request.open("GET", "https://query1.finance.yahoo.com/v8/finance/chart/BLUE?region=US&lang=en-US&include" +
        "PrePost=false&interval=5m&useYfid=true&range=1d&corsDomain=finance.yahoo.com&.tsrc=finance", true);
    request.send();

    request.onload = function () {
        if (request.status != 200) {
            alert(`Ошибка ${request.status}: ${request.statusText}`);
        } else {
            var stock = JSON.parse(request.responseText);
            stockPrice.innerHTML = '<b>$ ' + stock.chart.result[0].meta.regularMarketPrice + '</b>';
        }
    };
}