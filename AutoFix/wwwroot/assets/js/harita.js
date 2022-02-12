

function konumBul() {
    if (navigator.geolocation) {
        console.log(); ('Geolocation destekliyor')
        navigator.geolocation.getCurrentPosition(function (data) {
            console.log(data);
            var directionsService = new google.maps.DirectionsService();
            var directionsRenderer = new google.maps.DirectionsRenderer();

            const konum = {
                lat: data.coords.latitude,
                lng: data.coords.longitude
            };
            //  const haritaDiv =document.getElementById('map');
            //    googleMap = new google.maps.Map(haritaDiv,
            //     {
            //     center:konum,
            //     zoom:15,
            //     mapTypeId: 'satellite'
            // });

            $(document).ready(function () {

                var map = new google.maps.Map($('#map')[0], {
                    zoom: 15,
                    //center: new google.maps.LatLng(40.747688, -74.004142),
                    center: konum,

                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });

                google.maps.event.addListener(map, 'click', function (e) {
                    var marker = new google.maps.Marker({
                        position: e["latLng"],
                        title: "Hello World!"
                    });
                    marker.setMap(map);
                    document.getElementById("lat").value = marker.getPosition().lat();
                    document.getElementById("lng").value = marker.getPosition().lng();

                    console.log(marker.getPosition().lat());
                    console.log(marker.getPosition().lng());


                });
            });
            //placeMarker(konum);
        }, function (error) {
            alert.apply(error.message);
        })
    } else {
        alert('Geolocation desteklemiyor')
    }

}

//function placeMarker(location) {
//    if (!marker || !marker.setPosition) {
//        marker = new google.maps.Marker({
//            position: location,
//            map: map,
//        });
//    } else {
//        marker.setPosition(location);
//    }
//    if (!!infowindow && !!infowindow.close) {
//        infowindow.close();
//    }
//    infowindow = new google.maps.InfoWindow({
//        content: 'Latitude: ' + location.lat() + '<br>Longitude: ' + location.lng()
//    });
//    infowindow.open(map, marker);
//}

//google.maps.event.addDomListener(window, 'load', initialize);

konumBul();

// var marker;

// function placeMarker(location) {
//   if ( marker ) {
//     marker.setPosition(location);
//   } 
//   else {
//     marker = new google.maps.Marker({
//       position: location,
//       map: map
//     });
//   }
// }