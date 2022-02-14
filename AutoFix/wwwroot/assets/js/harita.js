

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

            $(document).ready(function () {

                var map = new google.maps.Map($('#map')[0], {
                    zoom: 15,
                    //center: new google.maps.LatLng(40.747688, -74.004142),
                    center: konum,

                    mapTypeId: google.maps.MapTypeId.ROADMAP
                });
                var marker;
                google.maps.event.addListener(map, 'click', function (e) {
                    if (marker == null) {
                        marker = new google.maps.Marker({
                            position: e["latLng"],
                            title: "Hello World!"
                        });
                        marker.setMap(map);
                        console.log(marker.getPosition().lat());
                    }
                    else {
                        marker.setMap(null);
                        marker = new google.maps.Marker({
                            position: e["latLng"],
                            title: "Hello World!"
                        });
                        marker.setMap(map);
                        console.log(marker.getPosition().lat());
                        console.log(marker.getPosition().lng());
                        document.getElementById("lat").value = marker.getPosition().lat();
                        document.getElementById("lng").value = marker.getPosition().lng();

                    }
                });
                
                /*console.log(marker.getPosition().lat());*/

            });

        }, function (error) {
            alert.apply(error.message);
        })
    } else {
        alert('Geolocation desteklemiyor')
    }

}

konumBul();
