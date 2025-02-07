var iscountyselected = false;
var previouscountyselected = "blank";
var start = true;
var past = null;
var content_dir = "details";

function initMap() {
    var map = new google.maps.Map(document.getElementById('mapGoogle'), {
        zoom: 3,
        center: { lat: 0, lng: -180 },
        mapTypeId: 'terrain'
    });
}

(function () {


    $(function () {

        console.log("Hello, IIFE loaded for Dashboard Controller module. Let's get dirty...");
        LoadDashboardWidgets();
        GetMapData();
        $("#map svg path").hover(
		  function () {
		      var id = $(this).attr("id");
		      $("#sehir").text(id);
		  });

        LoadPlannedVersusActualGraphData();
        LoadSalesOrderGraphData();

    });

    function LoadSalesOrderGraphData() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DashboardController/SelectSalesOrderGraphDataByCustomerIdEndDate";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        data.EndDate = GetCurrentDateSql();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                var chart = $("#pnlSalesOrderGraph").dxChart({
                    palette: "Harmony Light",
                    dataSource: resultObject.GraphData,
                    title: {
                        text: "Sipariş Grafiği (Son 30 Gün)",
                        horizontalAlignment: 'left',
                    },
                    commonSeriesSettings: {
                        argumentField: "EffectiveDate"
                    },
                    series: [
                        {
                            valueField: "SalesOrderCount", name: "Sipriş Adedi (Koli)", type: 'line', color: '#336699',
                            label: {
                                visible: true,
                            }
                        },
                    ],
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        argumentType: 'datetime',
                        label: {
                            type: 'discrete',
                            format: 'dd.MM.yyyy'
                        }
                    },
                    "export": {
                        enabled: false
                    },
                    legend: {
                        verticalAlignment: "top",
                        horizontalAlignment: "center"
                    }
                }).dxChart("instance");

            },
            error: function (err) {
                console.log('An error occured in api/DashboardController/SelectPlannedVersusActualGraphDataByCustomerIdEndDate: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });
    }

    function LoadPlannedVersusActualGraphData() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DashboardController/SelectPlannedVersusActualGraphDataByCustomerIdEndDate";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        data.EndDate = GetCurrentDateSql();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                resultObject.GraphData = $.map(resultObject.GraphData, function (item) {
                    item.ActualCount = item.ActualCount == 0 ? null : item.ActualCount;
                    item.NotVisitedCount = item.NotVisitedCount == 0 ? null : item.NotVisitedCount;
                    return item;
                });

                var chart = $("#pnlPlannedVersusActualGraph").dxChart({
                    palette: "Harmony Light",
                    dataSource: resultObject.GraphData,
                    title: {
                        text: "Route Gerçekleşme Grafiği (Son 30 Gün)",
                        horizontalAlignment: 'left',
                    },
                    commonSeriesSettings: {
                        argumentField: "EffectiveDate"
                    },
                    series: [
                        {
                            valueField: "ActualCount", name: "Gerçekleşen", type: 'fullStackedBar', color: '#23c6c8',
                            label: {
                                visible: true,
                            }
                        },
                        {
                            valueField: "NotVisitedCount", name: "Gerçekleşmeyen", type: 'fullStackedBar', color: '#ca1523',
                            label: {
                                visible: true,
                            }
                        },
                    ],
                    margin: {
                        bottom: 20
                    },
                    argumentAxis: {
                        valueMarginsEnabled: false,
                        argumentType: 'datetime',
                        label: {
                            type: 'discrete',
                            format: 'dd.MM.yyyy'
                        }
                    },
                    "export": {
                        enabled: false
                    },
                    legend: {
                        verticalAlignment: "top",
                        horizontalAlignment: "center"
                    }
                }).dxChart("instance");

            },
            error: function (err) {
                console.log('An error occured in api/DashboardController/SelectPlannedVersusActualGraphDataByCustomerIdEndDate: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    var availableCities = [];
    function GetMapData() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DashboardController/GetMapDataByCustomerIdCurrentDate";
        var data = {};
        data.CustomerId = customer.Id;
        data.CurrentDate = GetCurrentDateSql();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                availableCities = resultObject.Content;
                LoadMap();
            },
            error: function (err) {
                console.log('An error occured in api/DashboardController/GetMapDataByCustomerIdCurrentDate: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadMap() {
        var r = Raphael('map'),
	            attributes = {
	                fill: '#FFF',
	                stroke: '#000',
	                'stroke-width': 1.2,
	                'stroke-linejoin': 'round',
	            },
	            arr = new Array();

        for (var county in paths) {

            var obj = r.path(paths[county].path);
            obj.attr(attributes);
            arr[obj.id] = county;

            if (arr[obj.id] != 'blank') {
                obj.data('selected', 'notSelected');
                obj.node.id = arr[obj.id];
                obj.attr(attributes).attr({ title: paths[arr[obj.id]].name });
                $.each(availableCities, function (index, value) {
                    if (value.Name == paths[county].name) {
                        obj.attr('fill','#d71625');
                    }   
                });
                obj.hover(function () {
                    $('#coatOfArms').addClass(arr[this.id] + 'large sprite-largecrests');
                    $('#countyInfo').text(paths[arr[this.id]].name);
                    $('#searchResults').stop(true, true);
                }, function () {
                    $('#coatOfArms').removeClass();
                    if (paths[arr[this.id]].value == 'notSelected') {
                        $('.' + paths[arr[this.id]].name)
                                .slideUp('slow', function () {
                                    $(this).remove();
                                });
                    }
                });

                $("svg a").qtip({

                    content: {
                        attr: 'title'
                    },
                    show: 'mouseover',
                    hide: 'mouseout',
                    position: {
                        target: 'leave'
                    },
                    style: {
                        classes: 'ui-tooltip-tipsy ui-tooltip-shadow',
                        tip: false
                    }
                });

                obj.click(function () {
                    //console.log($(this)[0].attrs.title);
                    OpenRouteDetailsByCity($(this)[0].attrs.title);
                });

                var countyCrest = {
                    content: {
                        attr: 'title'
                    },
                    position: {
                        target: 'mouse'
                    },
                    style: {
                        classes: 'ui-tooltip-tipsy ui-tooltip-shadow',
                        tip: true
                    }
                };

                function hoverin(e) {
                    //if(paths[arr[this.id]].value == 'notSelected')
                    //	this.animate({
                    //		fill: '#15d4f5'}, 50);						
                }

                function hoverout(e) {
                    //if(paths[arr[this.id]].value == 'notSelected')
                    //	this.animate({
                    //		fill: '#666'}, 300);
                }

                obj.mouseout(hoverout);
                obj.mouseover(hoverin);
                $('#countyInfo').hide();
                $('#spinner').hide();

            }

        }
    }

    function OpenRouteDetailsByCity(cityName) {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DashboardController/SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName";
        var data = {};
        data.CityName = cityName;
        data.CustomerId = customer.Id;
        data.CurrentDate = GetCurrentDateSql();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                var globalOutput = '';

                $.each(resultObject.Members, function (indexMember, valueMember) {
                    var personelInfo = '<div class="col-md-12" style="padding-left: 0px; padding-right: 0px;"> ' +
                                       '     <div class="ibox-content text-center"> ' +
                                       '         <div class="m-b-sm"> ' +
                                       '                 <img alt="image" style="width: 48px;" class="img-circle" src="' + valueMember.ProfilePicturePath + '"> ' +
                                       '         </div> ' +
                                       '         <h1>' + valueMember.NameSurname + '</h1> ' +
                                       '         <h2 style="font-size: 8pt;"><button class="btn btn-danger btnShowHeatmap" bsm-memberId="'+ valueMember.Id + '" bsm-effectiveDate="' + GetCurrentDateSql() + '"><i class="fa fa-map-marker"></i> Dolaşım Raporu</button></h2>' +
                                       '     </div> ' +
                                       '</div> ';
                    
                    var tableOutput = personelInfo + '<table class="table table-bordered table-condensed"> ' +
                                      '  <thead>' +
                                      '      <tr>' +
                                      '          <td>Kategori</td>' +
                                      '          <td>Zincir</td>' +
                                      '          <td>Mağaza</td>' +
                                      '          <td>Durum</td>' +
                                      '          <td>Giriş</td>' +
                                      '          <td>Çıkış</td>' +
                                      '          <td>Süre</td>' +
                                      '      </tr>' +
                                      '  </thead>' +
                                      '  <tbody>{0}</tbody>' +
                                      '</table>';

                    var lines = '';
                    $.each(resultObject.RouteDetails, function (indexRoute, valueRoute) {
                        if (valueRoute.MemberId == valueMember.Id) {
                            lines += '<tr>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + valueRoute.RetailShopCategory + '</td>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + valueRoute.RetailName + '</td>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + valueRoute.RetailShopName + '</td>';
                            lines += '     <td>' + (valueRoute.Status == 'Gerçekleşti' ? '<span style="color: green;">Gerçekleşti</span>' : '<span style="color: red;">Gerçekleşmedi!</span>') + '</td>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (valueRoute.EntryOn == null ? '-' : moment(new Date(valueRoute.EntryOn)).format("HH:mm:ss")) + '</td>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (valueRoute.ExitOn == null ? '-' : moment(new Date(valueRoute.ExitOn)).format('HH:mm:ss')) + '</td>';
                            lines += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (valueRoute.TotalTimeSpent == null ? '-' : valueRoute.TotalTimeSpent + ' dak') + '</td>';
                            lines += '</tr>';
                        }
                    });

                    tableOutput = tableOutput.replace("{0}", lines);

                    globalOutput += tableOutput;

                });

                $('#lblContent').html(globalOutput);

                $('.btnShowHeatmap').off('click');
                $('.btnShowHeatmap').click(function (e) {
                    e.preventDefault();
                    LoadHeatMap($(this).attr("bsm-memberId"), $(this).attr("bsm-effectiveDate"));
                });

            },
            error: function (err) {
                console.log('An error occured in api/DashboardController/SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

        $('#pnlDailyRoute').modal('show');

    }

    function LoadHeatMap(memberId, effectiveDate) {
        console.log(memberId);
        console.log(effectiveDate);

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberCoordinateController/SelectMemberCoordinateByMemberIdToday";
        var data = {};
        data.MemberId = memberId;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                map = new google.maps.Map(document.getElementById('mapGoogle'), {
                    zoom: 3,
                    center: { lat: 0, lng: -180 },
                    mapTypeId: 'terrain'
                });

                var flightPlanCoordinates = resultObject.Content;
                var flightPath = new google.maps.Polyline({
                    path: flightPlanCoordinates,
                    geodesic: true,
                    strokeColor: '#FF0000',
                    strokeOpacity: 0.3,
                    strokeWeight: 10
                });

                flightPath.setMap(map);

                var myLatLng = { lat: resultObject.Content[0].lat, lng: resultObject.Content[0].lng };
                var image = 'http://mp.bsmyazilim.com/images/currentPosition.png';
                var retailPointImage = 'http://mp.bsmyazilim.com/images/retailPointSmall.png';
                var marker = new google.maps.Marker({
                    position: myLatLng,
                    map: map,
                    icon: 'http://mp.bsmyazilim.com/images/startFlag.png',
                    title: 'Buradan başladım',
                });

                var myLatLngStart = { lat: resultObject.Content[resultObject.Content.length - 1].lat, lng: resultObject.Content[resultObject.Content.length - 1].lng };
                var markerStart = new google.maps.Marker({
                    position: myLatLngStart,
                    map: map,
                    title: 'Şu an buradayım',
                    icon: image,
                });

                $.each(resultObject.RetailShops, function (indexShop, valueShop) {
                    var myLatLngShop = { lat: valueShop.CoordinateX, lng: valueShop.CoordinateY };
                    var markerShop = new google.maps.Marker({
                        position: myLatLngShop,
                        map: map,
                        title: valueShop.Name,
                        icon: (valueShop.IsVisited == 0 ? 'http://mp.bsmyazilim.com/images/retailNotVisited.png' : 'http://mp.bsmyazilim.com/images/retailVisited.png')
                    });
                });

                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < flightPlanCoordinates.length; i++) {
                    bounds.extend(flightPlanCoordinates[i]);
                }

                bounds.getCenter();
                map.fitBounds(bounds);

            },
            error: function (err) {
                console.log('An error occured in api/MemberCoordinateController/SelectMemberCoordinateByMemberIdToday: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

        $('#pnlDailyRoute').modal('hide');
        $('#pnlHeatmap').modal('show');

        $('#btnHeatmapClosed').off('click');
        $('#btnHeatmapClosed').click(function (e) {
            e.preventDefault();
            $('#pnlDailyRoute').modal('show');
        });

        $('#btnHeatmapClosed2').off('click');
        $('#btnHeatmapClosed2').click(function (e) {
            e.preventDefault();
            $('#pnlDailyRoute').modal('show');
        });

    }

    function LoadDashboardWidgets() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DashboardController/SelectDashboardWidgetValuesByCustomerIdCurrentDate";
        var data = {};
        data.CustomerId = customer.Id;
        data.CurrentDate = GetCurrentDateSql();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                $('#lblMemberCount').html(resultObject.Content[0].NumberOfMembers);
                $('#lblAllMembers').html(resultObject.Content[0].AllMembers);
                $('#lblVisitCountPlanned').html(resultObject.Content[0].PlannedVisitCount);
                $('#lblActualVisitCount').html(resultObject.Content[0].ActualVisitCount);
                $('#lblActualPercent').html(numeral(resultObject.Content[0].ActualPercent).format('0.00 %'));
            },
            error: function (err) {
                console.log('An error occured in api/DashboardController/SelectDashboardWidgetValuesByCustomerIdCurrentDate: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

}());