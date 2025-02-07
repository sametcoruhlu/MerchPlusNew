var listOfMemberRoute = [];
var submitButton;
var downloadButton;
var downloadAllButton;
(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Route Analiz Controller module. Let's get dirty...");
        PrepareNoktaSayisiContainer();
        PrepareIlMagazaSayisiContainer();
        PrepareIlKanalSayisiContainer();
        PrepareNatFirmaKirilimContainer();
        PrepareNatFormatKirilimContainer();
        PrepareLokalDagilimiContainer();
        MerchNoktaContainer();
        PrepareKisiBasiAverajNokta();
        PrepareMerchSayisi();
        PrepareMerchSayisiYuzde();
        PrepareFrekansKirilim();
    });

    function PrepareNoktaSayisiContainer() {


        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_KanalDagilimi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(resultObject);
                
                $("#noktaSayisiContainer").dxPieChart({
                    title: 'Kanal Dağılımı (Toplam : ' + resultObject.Content[0]["Total"] + ')',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "RetailCategoryName"
                    },
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "RetailCategoryName",
                        valueField: "Frequency",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function(arg) {
                                return '<center><b>' + arg.argumentText + '</b><br> ' + arg.valueText + " (" + arg.percentText + ")" + '</center>';
                            }
                        }
                    }]
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_KanalDagilimi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function PrepareIlMagazaSayisiContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_IlMagazaSayisi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(resultObject);

                $("#ilMagazaSayisiContainer").dxPieChart({
                    title: 'İl Mağaza Sayısı',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "CityName"
                    },
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "CityName",
                        valueField: "Frequency",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function (arg) {
                                return '<center style="font-size: 12pt !important;"><b>' + arg.argumentText + '</b><br> ' + arg.valueText + " (" + arg.percentText + ")" + '</center>';
                            }
                        }
                    }],
                    resolveLabelOverlapping: 'shift'
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareIlKanalSayisiContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_IlKanalSayisi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(resultObject);

                $("#ilKanalSayisiContainer").dxChart({
                    dataSource: resultObject.Content,
                    height: '500',
                    commonSeriesSettings: {
                        argumentField: "CityName",
                        type: "stackedBar"
                    },
                    series: [
                        { valueField: "NAT", name: "National" },
                        { valueField: "LOC", name: "Local" },
                    ],
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: 'top'
                    },
                    valueAxis: {
                        title: {
                            text: "Nokta Sayısı"
                        },
                        position: "left"
                    },
                    title: "İl Kanal Sayısı",
                    "export": {
                        enabled: true
                    },
                    tooltip: {
                        enabled: true,
                        location: "edge",
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.seriesName + " Nokta Sayısı: " + arg.valueText
                            };
                        }
                    },
                });


            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareNatFirmaKirilimContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_NatFirmaKirilimi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(resultObject);

                $("#natFirmaKirilimContainer").dxPieChart({
                    title: 'National Firma Dağılımı',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "RetailName"
                    },
                    resolveLabelOverlapping: 'shift',
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "RetailName",
                        valueField: "NAT",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function (arg) {
                                return '<center><b>' + arg.argumentText + '</b><br> ' + arg.valueText + " (" + arg.percentText + ")" + '</center>';
                            }
                        }
                    }]
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareNatFormatKirilimContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_NatFormatKirilimi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(resultObject);

                $("#natFormatKirilimContainer").dxPieChart({
                    title: 'National Format Dağılımı',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "RetailShopCategoryName"
                    },
                    resolveLabelOverlapping: 'shift',
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "RetailShopCategoryName",
                        valueField: "NAT",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function (arg) {
                                return '<center><b>' + arg.argumentText + '</b><br> ' + arg.valueText + " (" + arg.percentText + ")" + '</center>';
                            }
                        }
                    }]
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareLokalDagilimiContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_LokalDagilimi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(resultObject);

                $("#lokalDagilimiContainer").dxChart({
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "RetailName",
                        type: "stackedBar"
                    },
                    series: [
                        { valueField: "LOC", name: "Mağaza Sayısı" },
                    ],
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            overlappingBehavior: { mode: 'rotate', rotationAngle: -90 }
                        }
                    },
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: 'top'
                    },
                    valueAxis: {
                        title: {
                            text: "Nokta Sayısı"
                        },
                        position: "left"
                    },
                    title: "Lokal Dağılımı",
                    "export": {
                        enabled: true
                    },
                    tooltip: {
                        enabled: true,
                        location: "edge",
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.argumentText + " Nokta Sayısı: " + arg.valueText
                            };
                        }
                    }
                });


            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareKisiBasiAverajNokta() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_KisiBasiAverajNokta";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(resultObject);
                $("#kisiBasiAverajContainer").dxChart({
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "CityName",
                        type: "bar"
                    },
                    series: [
                        {
                            valueField: "EkipOrani", name: "Averaj Nokta Sayısı",
                            label: {
                                visible: true,
                            }
                        },
                    ],
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            overlappingBehavior: { mode: 'rotate', rotationAngle: -90 }
                        }
                    },
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: 'top'
                    },
                    valueAxis: {
                        title: {
                            text: "Nokta Sayısı"
                        },
                        position: "left"
                    },
                    title: "Kişi Başı Averaj Nokta Sayısı",
                    "export": {
                        enabled: true
                    },
                    tooltip: {
                        enabled: true,
                        location: "edge",
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.argumentText + " Nokta Sayısı: " + arg.valueText
                            };
                        }
                    }
                });


            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareMerchSayisi() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_KisiBasiAverajNokta";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(resultObject);
                $("#ekipSayisiContainer").dxChart({
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "CityName",
                        type: "bar"
                    },
                    series: [
                        {
                            valueField: "EkipSayisi", name: "Merch Sayısı",
                            label: {
                                visible: true,
                            }
                        },
                    ],
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            overlappingBehavior: { mode: 'rotate', rotationAngle: -90 }
                        }
                    },
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: 'top'
                    },
                    valueAxis: {
                        title: {
                            text: "Merch Sayısı"
                        },
                        position: "left"
                    },
                    title: "Merch Sayısı",
                    "export": {
                        enabled: true
                    },
                    tooltip: {
                        enabled: true,
                        location: "edge",
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.argumentText + " Merch Sayısı: " + arg.valueText
                            };
                        }
                    }
                });


            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareMerchSayisiYuzde() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_KisiBasiAverajNokta";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                $("#ekipYuzdesiContainer").dxPieChart({
                    title: 'Ekip - İl Dağılımı',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "CityName"
                    },
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "CityName",
                        valueField: "SehirOrani",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function (arg) {
                                return '<center style="font-size: 12pt !important;"><b>' + arg.argumentText + '</b><br> ' + arg.percentText + '</center>';
                            }
                        }
                    }],
                    resolveLabelOverlapping: 'shift'
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function PrepareFrekansKirilim() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_FrekansKirilimi";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                $("#frekansKirilimContainer").dxPieChart({
                    title: 'Frkans Kırılım',
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "FrekansText"
                    },
                    legend: {
                        visible: false
                    },
                    "export": {
                        enabled: true
                    },
                    series: [{
                        argumentField: "FrekansText",
                        valueField: "Frekans",
                        label: {
                            visible: true,
                            font: {
                                size: 16
                            },
                            connector: {
                                visible: true,
                                width: 0.5
                            },
                            position: "columns",
                            customizeText: function (arg) {
                                return '<center><b>' + arg.argumentText + '</b><br> ' + arg.valueText + " (" + arg.percentText + ")" + '</center>';
                            }
                        }
                    }],
                    resolveLabelOverlapping: 'shift'
                });

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }


    

    function MerchNoktaContainer() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteAnalysis_MerchNokta";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(resultObject);

                $("#merchNoktaContainer").dxChart({
                    dataSource: resultObject.Content,
                    commonSeriesSettings: {
                        argumentField: "MemberNameSurname",
                        type: "stackedBar"
                    },
                    series: [
                        {
                            valueField: "SAYI", name: "Nokta Sayısı",
                            label: {
                                visible: true,
                            }
                        },
                    ],
                    argumentAxis: {
                        tickInterval: 5,
                        label: {
                            overlappingBehavior: { mode: 'rotate', rotationAngle: -90 }
                        }
                    },
                    legend: {
                        verticalAlignment: "bottom",
                        horizontalAlignment: "center",
                        itemTextPosition: 'top'
                    },
                    valueAxis: {
                        title: {
                            text: "Nokta Sayısı"
                        },
                        position: "left"
                    },
                    title: "Merch Nokta Sayısı",
                    "export": {
                        enabled: true
                    },
                    tooltip: {
                        enabled: true,
                        location: "edge",
                        customizeTooltip: function (arg) {
                            return {
                                text: arg.argumentText + " Nokta Sayısı: " + arg.valueText
                            };
                        }
                    }
                });


            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteAnalysis_IlMagazaSayisi: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function GenerateReport() {

    }

    function PrepareGenerateButton() {
        submitButton = $('#btnGenerate').ladda();
        submitButton.click(function (e) {
            e.preventDefault();
            GenerateReport();
        });
    }
}());