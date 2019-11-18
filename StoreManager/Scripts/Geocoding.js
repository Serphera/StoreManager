let map;
let marker;
let geoUrl;
let reverseGeoUrl;

var key = '601d0423a05044';
let skip = { address: false, city: false, zip: false, country: false };

var markerOptions = {
    title: "storeLocation",
    draggable: true
}

function myMap(methodPoint) {

    geoUrl = methodPoint.geo;
    reverseGeoUrl = methodPoint.reverse;

    // Add layers that we need to the map
    var streets = L.tileLayer.Unwired({ key: key, scheme: "streets" });

    let start = [0, 0];
    let zoom = 2;

    // If coords already exist
    if (methodPoint.lat) {

        start = [methodPoint.lat, methodPoint.lon];
        zoom = 12;
    }
    // Initialize the map
    map = L.map('map', {
        center: start, //map loads with this location as center
        zoom: zoom,
        layers: [streets] // Show 'streets' by default
    });

    // Add the 'scale' control
    L.control.scale().addTo(map);

    // Add the 'layers' control
    L.control.layers({
        "Streets": streets
    }).addTo(map);

    marker = L.marker(start, markerOptions).addTo(map);

    marker.on('dragend', function (e) {

        let point = marker.getLatLng();
        ReverseGeoASP({ lat: point.lat, lng: point.lng });
    });
}


function UpdatePos(data) {

    let lon = data.lon;
    let lat = data.lat;

    if (lat.length > 16) {
        lat = lat.substring(0, 14);
    }
    if (lon.length > 16) {
        lon = lon.substring(0, 14);
    }

    document.getElementById('Store_Longitude').value = lon;
    document.getElementById('Store_Latitude').value = lat;

    if (!skip.city) {

        let _city = data.display_name.split(", ")[1];
        document.getElementById('Store_City').value = _city;
    }
    if (!skip.country) {
        document.getElementById('Store_Country').value = data.address.country;
    }
    if (!skip.zip) {
        document.getElementById('Store_Zip').value = data.address.postcode;
    }
    if (!skip.address) {
        document.getElementById('Store_Address').value = data.address.road;
    }

    skip = { address: false, city: false, zip: false, country: false };
};


function UpdateMapASP() {

    this.preventdefault;

    let city = document.getElementById('Store_City').value;
    let country = document.getElementById('Store_Country').value;
    let address = document.getElementById('Store_Address').value;
    let zip = document.getElementById('Store_Zip').value;

    if (country && address && zip) {

        skip.address = true;
        skip.country = true;
        skip.zip = true;

        address = address + "+" + country + "+" + zip;
    }
    else if (address && country) {

        skip.address = true;
        skip.country = true;

        address = address + "+" + country;
    }
    else if (address && zip) {

        skip.address = true;
        skip.zip = true;

        address = address + "+" + zip;
    }
    else if (address) {

        skip.address = true;
    }
    else if (zip && city) {

        skip.zip = true;
        skip.city = true;

        address = city + "+" + zip;
    }
    else if (city) {

        skip.city = true;

        address = city;
    }
    else if (zip) {

        address = zip;
    }

    if (!address) {

        alert("You must enter at least one piece of location info");
    }
    else {

        let delim = address.replace(" ", "+");
        $.ajax({

            url: geoUrl,
            data: { searchParam: delim },
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                let _lat = data[0].lat;
                let _long = data[0].lon;

                ReverseGeoASP({ lat: _lat, lng: _long });

                marker.setLatLng(new L.LatLng(_lat, _long));
                map.panTo(new L.LatLng(_lat, _long));
            }
        });

    }
}


function ReverseGeoASP(pos) {

    $.ajax({

        url: reverseGeoUrl,
        data: { lat: pos.lat, lng: pos.lng },
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            UpdatePos(data);
        }
    });
}
