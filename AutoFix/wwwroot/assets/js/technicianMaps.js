var element = $(this);
var map;
function initialize(myCenter) {
    var marker = new google.maps.Marker({
        position: myCenter
    });

    var mapProp = {
        center: myCenter,
        zoom: 8,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("map-canvas"), mapProp);
    marker.setMap(map);
};

$('#myMapModal').on('show.bs.modal', function (e) {
    var element = $(e.relatedTarget);
    var data = element.data("lat").split(',');
    var latlng = new google.maps.LatLng(data[0], data[1]);
    initialize(latlng);
    $("#lat").html(latlng.lat() + ", " + latlng.lng());
    google.maps.event.trigger(map, 'resize');
});