var map;
var markers = [];
var gridData = {};
var gridDataSource = [];
var markerCluster;
function initMap() {
    var mapDiv = document.getElementById('map');
    map = new google.maps.Map(mapDiv, {
        center: { lat: 38.8713186, lng: 30.7530756 },
        zoom: 6,
        scrollwheel: true,
        draggable: true,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: [{ "featureType": "all", "stylers": [{ "saturation": 0 }, { "hue": "#e7ecf0" }] }, { "featureType": "road", "stylers": [{ "saturation": -70 }] }, { "featureType": "transit", "stylers": [{ "visibility": "off" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "stylers": [{ "visibility": "simplified" }, { "saturation": -60 }] }]
    });
}

(function () {

    $(function () {

        function InitOnOffSwitches() {
            var elem = document.querySelector('.js-switch');
            var switchery = new Switchery(elem, { color: '#1AB394' });
            $('.js-switch').change(function (e) {
                LoadAllRetailShops($('.js-switch').is(':checked'));
            });
        }

        InitOnOffSwitches();

        //#region Console writeout
        console.log("Hello, IIFE loaded for RetailShops module. Let's get dirty...");

        LoadAllRetailShops(true);

        $('#btnNew').click(function(e) {
            e.preventDefault();
            
            $('#hdn_Id').val('0');

            $.when(LoadRetailCategory(),
                LoadCity()
            ).done(function(result_RetailCategory, result_City) {

                //Unregister events
                $('#winNew_wleRetailCategoryId').off('change');
                $('#winNew_wleRetailId').off('change');
                $('#winNew_wleCityId').off('change');
                $('#btnShowOnMap').off('click');
                $('#btnSave').off('click');

                //Show Model
                $('#winNew').modal('show');

                //Bind Data - RetailShopName
                $('#winNew_txtRetailShopName').val('');

                //Bind Data - RetailShopAddress
                $('#winNew_txtRetailShopAddress').val('');

                //Bind Data - RetailShopCoordinates
                $('#winNew_CoordinateX').val('0.00');
                $('#winNew_CoordinateY').val('0.00');

                //Bind Data - Dropdown Data
                BindData('winNew_wleRetailCategoryId', result_RetailCategory[0].Content, 'Name', 'Id');
                BindData('winNew_wleCityId', result_City[0].Content, 'Name', 'Id');

                //Register Events
                $('#winNew_wleRetailCategoryId').change(function() {
                    $('#winNew_wleRetailId').val('').trigger('change');
                    $('#winNew_wleRetailShopCategoryId').val('').trigger('change');
                    $.when(LoadRetailByRetailCategoryId($('#winNew_wleRetailCategoryId').val())).done(function(result_Retail) {
                        BindData('winNew_wleRetailId', result_Retail.Content, 'Name', 'Id');
                    });
                });

                $('#winNew_wleRetailId').change(function() {
                    $('#winNew_wleRetailShopCategoryId').val('').trigger('change');
                    $.when(LoadRetailShopCategoryByRetailId($('#winNew_wleRetailId').val())).done(function(result_RetailShopCategory) {
                        BindData('winNew_wleRetailShopCategoryId', result_RetailShopCategory.Content, 'Name', 'Id');
                    });
                });

                $('#winNew_wleCityId').change(function() {
                    $('#winNew_wleDistrictId').val('').trigger('change');
                    $.when(LoadDistrictByCityId($('#winNew_wleCityId').val())).done(function(result_District) {
                        BindData('winNew_wleDistrictId', result_District.Content, 'Name', 'Id');
                    });
                });

                $('#btnShowOnMap').click(function(e) {
                    e.preventDefault();
                    window.open('https://maps.google.com/maps?q=loc:' + $('#winNew_CoordinateX').val() + ',' + $('#winNew_CoordinateY').val(), '_blank');
                });

                $('#btnSave').click(function(e) {
                    e.preventDefault();

                    var hasErrors = false;
                    var errorMessages = '';
                    if ($('#winNew_wleRetailCategoryId').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Kanal türü boş bırakılamaz!</li>';
                    }
                    if ($('#winNew_wleRetailId').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Müşteri boş bırakılamaz!</li>';
                    }
                    if ($('#winNew_wleRetailShopCategoryId').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Format boş bırakılamaz!</li>';
                    }
                    if ($('#winNew_txtCoordinateX').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Mağaza X Koordinatı eksik!</li>';
                    }
                    if ($('#winNew_txtCoordinateY').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Mağaza Y Koordinatı eksik!</li>';
                    }
                    if ($('#winNew_wleCityId').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>Şehir seçilmemiş!</li>';
                    }
                    if ($('#winNew_wleDistrictId').val() == '') {
                        hasErrors = true;
                        errorMessages += '<li>İlçe seçilmemiş!</li>';
                    }

                    if (hasErrors) {
                        $('#pnlErrors').css('display', 'block');
                        $('#pnlErrorsContent').html('<ul>' + errorMessages + '</ul>');
                        return;
                    }

                    if ($('#hdn_Id').val() != '0')
                        UpdateRetailShopById();
                    else
                        InsertRetailShop();

                });

            });

        });

    });

    function LoadRetailCategory() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailCategoryController/SelectRetailCategory";
        var data = {};
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                console.log('An error occured in api/RetailCategoryController/SelectRetailCategory: ' + JSON.stringify(err, null, 3));
            }
        });

    }

    function LoadRetailByRetailCategoryId(retailCategoryId) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailController/SelectRetailByRetailCategoryId";
        var data = {};
        data.RetailCategoryId = retailCategoryId;
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                console.log('An error occured in api/RetailController/SelectRetailByRetailCategoryId: ' + JSON.stringify(err, null, 3));
            }
        });

    }

    function LoadCity() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CityController/SelectCity";
        var data = {};
        data.Username = $('#txtUsername').val();
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                console.log('An error occured in api/CityController/SelectCity: ' + JSON.stringify(err, null, 3));
            }
        });

    }

    function LoadDistrictByCityId(cityId) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DistrictController/SelectDistrictByCityId";
        var data = {};
        data.CityId = cityId;
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                console.log('An error occured in api/DistrictController/SelectDistrictByCityId: ' + JSON.stringify(err, null, 3));
            }
        });

    }

    function LoadRetailShopCategoryByRetailId(retailId) {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopCategoryController/SelectRetailShopCategoryByRetailId";
        var data = {};
        data.RetailId = retailId;
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                console.log('An error occured in api/RetailShopCategoryController/SelectRetailShopCategoryByRetailId: ' + JSON.stringify(err, null, 3));
            }
        });
    }

    function BindData(controlName, dataSource, displayField, valueField) {
        $('#' + controlName).html('');
        var options = '<option></option>';
        $.each(dataSource, function (index, value) {
            options += '<option value="' + value[valueField] + '">' + value[displayField] + '</option>';
        });
        $('#' + controlName).html(options);
        $('#' + controlName).select2({
            width: '100%',
            theme: 'bootstrap',
            placeholder: 'Seçiniz...',
            allowClear: true,
            dropdownParent: $('#winNew')
        });
    }

    function UpdateRetailShopById() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopController/UpdateRetailShopById";
        var data = {};
        data.Id = $('#hdn_Id').val();
        data.RetailId = $('#winNew_wleRetailId').val();
        data.RetailShopCategoryId = $('#winNew_wleRetailShopCategoryId').val();
        data.Name = $('#winNew_txtRetailShopName').val();
        data.Address = $('#winNew_txtRetailShopAddress').val();
        data.CityId = $('#winNew_wleCityId').val();
        data.DistrictId = $('#winNew_wleDistrictId').val();
        data.CoordinateX = $('#winNew_CoordinateX').val();
        data.CoordinateY = $('#winNew_CoordinateY').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                $('#winNew').modal('hide');
                swal('Güncellendi!', 'Mağaza bilgileri başarıyla güncellendi ve mağaza listesi tekrar yüklendi.', 'success');
                LoadAllRetailShops($('.js-switch').is(':checked'));
            },
            error: function (err) {
                console.log('An error occured in api/RetailShopController/UpdateRetailShopById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function InsertRetailShop() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopController/InsertRetailShop";
        var data = {};
        data.RetailId = $('#winNew_wleRetailId').val();
        data.RetailShopCategoryId = $('#winNew_wleRetailShopCategoryId').val();
        data.Name = $('#winNew_txtRetailShopName').val();
        data.Address = $('#winNew_txtRetailShopAddress').val();
        data.CityId = $('#winNew_wleCityId').val();
        data.DistrictId = $('#winNew_wleDistrictId').val();
        data.CoordinateX = $('#winNew_CoordinateX').val();
        data.CoordinateY = $('#winNew_CoordinateY').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function(xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function(resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                $('#winNew').modal('hide');
                swal('Eklendi!', 'Mağaza bilgileri başarıyla kaydedildi. Mağaza listesinde görebilirsiniz.', 'success');
                LoadAllRetailShops($('.js-switch').is(':checked'));
            },
            error: function(err) {
                console.log('An error occured in api/RetailShopController/InsertRetailShop: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function DeleteRetailShopById(id) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopController/DeleteRetailShopById";
        var data = {};
        data.Id = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function(xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function(resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                LoadAllRetailShops($('.js-switch').is(':checked'));
                swal('Silindi', 'Mağaza silindi ve mağaza listesi güncellendi.', 'success');
            },
            error: function(err) {
                console.log('An error occured in api/RetailShopController/DeleteRetailShopById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadAllRetailShops(showAllShops) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url;
        if(showAllShops)
            url = InfrastructureVariables.WebServicePath + "api/RetailShopController/SelectRetailShopMapView";
        else
            url = InfrastructureVariables.WebServicePath + "api/RetailShopController/SelectRetailShopMapViewByCustomerId";

        var data = {};
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));

        data.CustomerId = customer.Id;



        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {

                try {
                    markerCluster.clearMarkers();
                } catch (e) {

                }
                

                for (var i = 0; i < markers.length; i++) {
                    markers[i].setMap(null);
                }
                markers = [];


                $.each(resultObject.Content, function (index, value) {

                    var marker = new google.maps.Marker({
                        position: { lat: value.CoordinateX, lng: value.CoordinateY },
                        map: map,
                        title: value.Name,
                    });
                    markers.push(marker);

                });

                markerCluster = new MarkerClusterer(map, markers, {
                    imagePath: 'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m'
                });

                var bounds = new google.maps.LatLngBounds();
                for (var i = 0; i < markers.length; i++) {
                    bounds.extend(markers[i].getPosition());
                }
                map.fitBounds(bounds);

                gridDataSource = resultObject.Content;
                dataGrid = $("#gridContainer").dxDataGrid({
                    dataSource: gridDataSource,
                    sorting: {
                        mode: "multiple"
                    },
                    filterRow: {
                        visible: true,
                        applyFilter: "auto"
                    },
                    paging: {
                        pageSize: 10
                    },
                    pager: {
                        showNavigationButtons: true
                    },
                    searchPanel: {
                        visible: true,
                        placeholder: "Listede ara"
                    },
                    headerFilter: {
                        visible: true
                    },
                    selection: {
                        mode: "single"
                    },
                    "export": {
                        enabled: true,
                        fileName: "Mağaza Şubeleri Listesi",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    allowColumnResizing: true,
                    hoverStateEnabled: true,
                    onContentReady: function (e) {

                        $('.btnDelete').off('click');
                        $('.btnDelete').click(function(e) {
                            e.preventDefault();
                            setTimeout(function() {
                                var dataGrid = $('#gridContainer').dxDataGrid('instance');
                                var keys = dataGrid.getSelectedRowKeys();
                                console.log(keys);

                                swal({
                                    title: 'Mağazayı silmek üzeresiniz!',
                                    text: "Bu mağazayı sildiğinizde buna bağlı tüm tarihsel veriyi, route atamalarını ve bilgi girişlerini de silmiş olacaksınız. Bu işlemin geri dönüşü yoktur. Silmek istediğinizden emin misiniz?",
                                    type: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Evet, mağazayı sil!'
                                }).then(function() {
                                    DeleteRetailShopById(keys[0].Id);
                                });

                            }, 200);
                        });


                        $('.btnView').off('click');
                        $('.btnView').click(function (e) {
                            e.preventDefault();
                            setTimeout(function () {
                                var dataGrid = $('#gridContainer').dxDataGrid('instance');
                                var keys = dataGrid.getSelectedRowKeys();
                                console.log(keys);

                                //console.log(keys[0].Id);
                                $('#hdn_Id').val(keys[0].Id);

                                $.when(LoadRetailCategory(),
                                    LoadRetailByRetailCategoryId(keys[0].RetailCategoryId),
                                    LoadCity(),
                                    LoadDistrictByCityId(keys[0].CityId),
                                    LoadRetailShopCategoryByRetailId(keys[0].RetailId)
                                ).done(function (result_RetailCategory, result_Retail, result_City, result_District, result_retailShopCategory) {

                                    //Unregister events
                                    $('#winNew_wleRetailCategoryId').off('change');
                                    $('#winNew_wleRetailId').off('change');
                                    $('#winNew_wleCityId').off('change');
                                    $('#btnShowOnMap').off('click');
                                    $('#btnSave').off('click');

                                    //Show Model
                                    $('#winNew').modal('show');

                                    //Bind Data - RetailShopName
                                    $('#winNew_txtRetailShopName').val(keys[0].Name);

                                    //Bind Data - RetailShopAddress
                                    $('#winNew_txtRetailShopAddress').val(keys[0].Address);

                                    //Bind Data - RetailShopCoordinates
                                    $('#winNew_CoordinateX').val(keys[0].CoordinateX);
                                    $('#winNew_CoordinateY').val(keys[0].CoordinateY);

                                    //Bind Data - Dropdown Data
                                    BindData('winNew_wleRetailCategoryId', result_RetailCategory[0].Content, 'Name', 'Id');
                                    BindData('winNew_wleRetailId', result_Retail[0].Content, 'Name', 'Id');
                                    BindData('winNew_wleCityId', result_City[0].Content, 'Name', 'Id');
                                    BindData('winNew_wleDistrictId', result_District[0].Content, 'Name', 'Id');
                                    BindData('winNew_wleRetailShopCategoryId', result_retailShopCategory[0].Content, 'Name', 'Id');

                                    //Bind Data - Form Values
                                    $('#winNew_wleRetailCategoryId').val(keys[0].RetailCategoryId).trigger('change');
                                    $('#winNew_wleRetailId').val(keys[0].RetailId).trigger('change');
                                    $('#winNew_wleRetailShopCategoryId').val(keys[0].RetailShopCategoryId).trigger('change');
                                    $('#winNew_wleCityId').val(keys[0].CityId).trigger('change');
                                    $('#winNew_wleDistrictId').val(keys[0].DistrictId).trigger('change');


                                    //Register Events
                                    $('#winNew_wleRetailCategoryId').change(function () {
                                        $('#winNew_wleRetailId').val('').trigger('change');
                                        $('#winNew_wleRetailShopCategoryId').val('').trigger('change');
                                        $.when(LoadRetailByRetailCategoryId($('#winNew_wleRetailCategoryId').val())).done(function (result_Retail) {
                                            BindData('winNew_wleRetailId', result_Retail.Content, 'Name', 'Id');
                                        });
                                    });

                                    $('#winNew_wleRetailId').change(function () {
                                        $('#winNew_wleRetailShopCategoryId').val('').trigger('change');
                                        $.when(LoadRetailShopCategoryByRetailId($('#winNew_wleRetailId').val())).done(function (result_RetailShopCategory) {
                                            BindData('winNew_wleRetailShopCategoryId', result_RetailShopCategory.Content, 'Name', 'Id');
                                        });
                                    });

                                    $('#winNew_wleCityId').change(function () {
                                        $('#winNew_wleDistrictId').val('').trigger('change');
                                        $.when(LoadDistrictByCityId($('#winNew_wleCityId').val())).done(function (result_District) {
                                            BindData('winNew_wleDistrictId', result_District.Content, 'Name', 'Id');
                                        });
                                    });

                                    $('#btnShowOnMap').click(function (e) {
                                        e.preventDefault();
                                        window.open('https://maps.google.com/maps?q=loc:' + keys[0].CoordinateX + ',' + keys[0].CoordinateY, '_blank');
                                    });

                                    $('#btnSave').click(function (e) {
                                        e.preventDefault();

                                        var hasErrors = false;
                                        var errorMessages = '';
                                        if ($('#winNew_wleRetailCategoryId').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Kanal türü boş bırakılamaz!</li>';
                                        }
                                        if ($('#winNew_wleRetailId').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Müşteri boş bırakılamaz!</li>';
                                        }
                                        if ($('#winNew_wleRetailShopCategoryId').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Format boş bırakılamaz!</li>';
                                        }
                                        if ($('#winNew_txtCoordinateX').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Mağaza X Koordinatı eksik!</li>';
                                        }
                                        if ($('#winNew_txtCoordinateY').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Mağaza Y Koordinatı eksik!</li>';
                                        }
                                        if ($('#winNew_wleCityId').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>Şehir seçilmemiş!</li>';
                                        }
                                        if ($('#winNew_wleDistrictId').val() == '') {
                                            hasErrors = true;
                                            errorMessages += '<li>İlçe seçilmemiş!</li>';
                                        }

                                        if (hasErrors) {
                                            $('#pnlErrors').css('display', 'block');
                                            $('#pnlErrorsContent').html('<ul>' + errorMessages + '</ul>');
                                            return;
                                        }

                                        if ($('#hdn_Id').val() != '0')
                                            UpdateRetailShopById();
                                        else
                                            InsertRetailShop();

                                    });

                                });

                            }, 200);
                        });
                    },
                    columns: [
                        {
                            caption: ' ',
                            width: '60px',
                            cellTemplate: function (container, options) {
                                $('<button type="button" bsm-rowIndex="' + options.rowIndex + '" bsm-id="' + options.data.Id + '" class="btn btn-default btn-xs btnView"><i class="fa fa-pencil"></i></button>' +
                                  '<button type="button" bsm-rowIndex="' + options.rowIndex + '" bsm-id="' + options.data.Id + '" class="btn btn-danger btn-xs btnDelete"><i class="fa fa-trash"></i></button>').appendTo(container);
                            }
                        },
                        {
                            dataField: "RetailCategoryName",
                            caption: 'Kanal',
                            width: '90px',
                        },
                        {
                            dataField: "CityName",
                            caption: 'Şehir',
                            width: '100px',
                            sortOrder: "asc"
                        },
                        {
                            dataField: "DistrictName",
                            caption: 'İlçe',
                            width: '100px'
                        },
                        {
                            dataField: "RetailName",
                            caption: "Müşteri",
                            width: '150px',
                            cssClass: 'darkBlue',
                            sortOrder: "asc"
                        },
                        {
                            dataField: "Name",
                            caption: 'Mağaza',
                            width: '145px',
                            cssClass: 'lightBlue',
                            sortOrder: "asc"
                        },
                        {
                            dataField: "RetailShopCategoryName",
                            caption: "Format",
                            width: '100px',
                            cssClass: 'lightBlue'
                        },
                        {
                            dataField: "CoordinateX",
                            caption: 'X',
                            width: '100px',
                        },
                        {
                            dataField: "CoordinateY",
                            caption: 'Y',
                            width: '100px',
                        },
                        {
                            dataField: "Address",
                            caption: 'Adres',
                        },
                    ]
                }).dxDataGrid('instance');

                
            },
            error: function (err) {
                console.log('An error occured in api/RetailShopController/SelectRetailShopMapView: ' + JSON.stringify(err, null, 3));
            }
        });

    }


}());
