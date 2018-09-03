// Write your Javascript code.
var BingMapKey = "AiU3mYe3yzqyTu1tvRTg_TDUmSVOOPDmHZD_TC16dGEcAPk7uUzX3WdYfvltN4ib";
var renderRequestsMap = function (divIdForMap, requestData) {
    if (requestData) {
        var bingMap = createBingMap(divIdForMap);
        addRequestPins(bingMap, requestData);
    }
}

function createBingMap(divIdForMap) {
    return new Microsoft.Maps.Map(
        document.getElementById(divIdForMap), {
            credentials: BingMapKey
        });
}

function addRequestPins(bingMap, requestData) {
    var locations = [];
    $.each(requestData, function (index, data) {
        var location = new Microsoft.Maps.Location(data.lat, data.long);

        locations.push(location);

        var pin = new Microsoft.Maps.Pushpin(location, { title: data.name, color: data.color });
        
        Microsoft.Maps.Events.addHandler(pin, 'click', function () {
            window.location.href = data.url;
        });

        bingMap.entities.push(pin);
    });
    var rect = Microsoft.Maps.LocationRect.fromLocations(locations);
    bingMap.setView({ bounds: rect, padding: 80 });
}