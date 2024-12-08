var map;
var reader = new FileReader();
var idToReset;
var idToEdit;
var idToTimeOff;
function initMap() {

    var mapDiv = document.getElementById('map');
    map = new google.maps.Map(mapDiv, {
        center: { lat: 41.0931353, lng: 29.0863633 },
        zoom: 18,
        scrollwheel: false,
        draggable: false,
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        styles: [{ "featureType": "water", "stylers": [{ "saturation": 43 }, { "lightness": -11 }, { "hue": "#0088ff" }] }, { "featureType": "road", "elementType": "geometry.fill", "stylers": [{ "hue": "#ff0000" }, { "saturation": -100 }, { "lightness": 99 }] }, { "featureType": "road", "elementType": "geometry.stroke", "stylers": [{ "color": "#808080" }, { "lightness": 54 }] }, { "featureType": "landscape.man_made", "elementType": "geometry.fill", "stylers": [{ "color": "#ece2d9" }] }, { "featureType": "poi.park", "elementType": "geometry.fill", "stylers": [{ "color": "#ccdca1" }] }, { "featureType": "road", "elementType": "labels.text.fill", "stylers": [{ "color": "#767676" }] }, { "featureType": "road", "elementType": "labels.text.stroke", "stylers": [{ "color": "#ffffff" }] }, { "featureType": "poi", "stylers": [{ "visibility": "off" }] }, { "featureType": "landscape.natural", "elementType": "geometry.fill", "stylers": [{ "visibility": "on" }, { "color": "#b8cb93" }] }, { "featureType": "poi.park", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.sports_complex", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.medical", "stylers": [{ "visibility": "on" }] }, { "featureType": "poi.business", "stylers": [{ "visibility": "simplified" }] }]
    });
}

function geocodeAddress(geocoder, resultsMap) {
    var address = document.getElementById('winNew_txtAddress').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            resultsMap.setCenter(results[0].geometry.location);
            var marker = new google.maps.Marker({
                map: resultsMap,
                position: results[0].geometry.location
            });

            document.getElementById('winNew_txtAddressCoordinateX').value = results[0].geometry.location.lat();
            document.getElementById('winNew_txtAddressCoordinateY').value = results[0].geometry.location.lng();

            //console.log(results[0]);
            var cityName = '';
            var townName = '';
            for (var i = 0; i < results[0].address_components.length; i++) {
                for (var b = 0; b < results[0].address_components[i].types.length; b++) {
                    if (results[0].address_components[i].types[b] == "administrative_area_level_1") {
                        cityName = results[0].address_components[i].long_name;
                        console.log(cityName.turkishToUpper());
                    }
                    if (results[0].address_components[i].types[b] == "administrative_area_level_2") {
                        townName = results[0].address_components[i].long_name;
                        console.log(townName);
                    }
                    if (cityName != '' && townName != '')
                        break;
                }
            }

            var dropdownlist = $("#winNew_wleAddressCity").data("kendoComboBox");
            dropdownlist.select(function (dataItem) {
                return dataItem.Name === cityName.turkishToUpper();
            });

        } else {
            swal('Hoppa...', 'Girdiğiniz adresi Google üzerinde bulamadık. Tekrar deneyebilir misiniz?<br><br>' + status, 'error').then(function () {
                $('#myModal5').modal('show');
            });
            //alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}

(function () {

    //#region View Load

    $(function () {
        //#region Console writeout

        console.log("Hello, IIFE loaded for Members module. Let's get dirty...");

        LoadMembers($('.js-switch').is(':checked'));
        SetDatePickers();
        LoadCustomerTitle();
        LoadDirectReport();
        InitOnOffSwitches();
        LoadCities();
        MaskPhoneNumbers();
        LoadDeviceModel();
        LoadCustomers();
        LoadDistricts();

        reader.addEventListener("load", function () {
            $('#winNew_lblBase64Encoded').val(reader.result);
            console.log('Encoded Image Data : ' + reader.result);
            $('#winNew_pctProfilePicture').attr('src', reader.result);
        }, false);

        $('#winNew_btnAddPhoto').click(function () {
            $('input[type=file]').trigger('click');
        });

        $('input[type=file]').change(function () {
            var file = document.querySelector('input[type=file]').files[0];
            if (file) {
                reader.readAsDataURL(file);
            }
        });

        $('#winNew_btnSearchAddress').click(function () {
            initMap();
            var geocoder = new google.maps.Geocoder();
            geocodeAddress(geocoder,map);
        });

        $('#btnNew').click(function () {
            CleanNewForm();
            changeSwitcheryState($('#winNew_chkIsActive'), true);
            $('#myModal5').modal('show');
            setTimeout(function () {
                google.maps.event.trigger(map, 'resize');
            }, 1000);
            //$('#winNew_pnlContent').slimscroll({
            //    height: '400px'
            //})
        });

        $('#btnSave').click(function () {
            //var maskedtextbox = $("#winNew_txtDataNumber").data("kendoMaskedTextBox");
            //console.log(maskedtextbox.raw());
            //maskedtextbox.value('5332084656');

            var envelope = {};
            envelope.NameSurname = $('#winNew_txtNameSurname').val();
            envelope.UserName = $('#winNew_txtUserName').val();
            envelope.HiringDate = $('#winNew_txtHiringDate').val();
            envelope.MemberTitleId = $('#winNew_wleMemberTitle').val();
            envelope.SocialSecurityNumber = $('#winNew_txtSocialSecurityNumber').val();
            envelope.Email = $('#winNew_txtEmail').val();
            envelope.LeavingDate = $('#winNew_txtLeavingDate').val();
            envelope.DirectReportId = $('#winNew_wleDirectReport').val();
            var clickCheckbox = document.querySelector('#winNew_chkIsActive');
            envelope.IsActive = clickCheckbox.checked;

            var dataNumber = $("#winNew_txtDataNumber").data("kendoMaskedTextBox");
            envelope.DataNumber = dataNumber.raw();

            var phoneNumber = $("#winNew_txtPhoneNumber").data("kendoMaskedTextBox");
            envelope.PhoneNumber = phoneNumber.raw();
            
            envelope.Address = $('#winNew_txtAddress').val();
            envelope.AddressCityId = $('#winNew_wleAddressCity').val();
            envelope.AddressCoordinateX = $('#winNew_txtAddressCoordinateX').val();
            envelope.AddressCoordinateY = $('#winNew_txtAddressCoordinateY').val();
            envelope.DeviceModelId = $('#winNew_wleDeviceModel').val();
            envelope.IMEINumber = $('#winNew_txtIMEINumber').val();
            envelope.Base64Encoded = $('#winNew_lblBase64Encoded').val();
            var companyIds = '';
            $('.chkCompany').each(function (index) {
                if ($(this).prop('checked') == true) {
                    if (companyIds != '')
                        companyIds += ',';

                    companyIds += $(this).attr('bsm-id');

                }
            });
            envelope.MemberCustomerId = companyIds;
            console.log('Company Ids: ' + companyIds);

            var checked_ids = [];
            checked_ids = $("#pnlTowns").jstree('get_checked');
            var districtIds = '';
            $.each(checked_ids, function (index, value) {
                if (value.indexOf('city') != 0) {
                    if (districtIds != '')
                        districtIds += ',';

                    districtIds += value;
                }
            });

            envelope.MemberDistrictId = districtIds;
            console.log('District Ids: ' + districtIds);

            if (ValidateInput())
            {
                return;
            }
            else
            {
                if ($('#hdn_Id').val() == 0) {
                    //New member. We should register first
                    var token = JSON.parse(sessionStorage.getItem('token'));
                    var member = JSON.parse(sessionStorage.getItem('entMember'));
                    var eventInstance = event;
                    var url = InfrastructureVariables.WebServicePath + "api/Account/Register";
                    var data = {};
                    data.Email = $('#winNew_txtEmail').val();
                    data.NameSurname = $('#winNew_txtNameSurname').val();
                    data.SocialSecurityNumber = $('#winNew_txtSocialSecurityNumber').val();
                    data.UserName = $('#winNew_txtUserName').val();
                    data.Password = '12345678';
                    data.ConfirmPassword = '12345678';
                    data.MemberTitleId = $('#winNew_wleMemberTitle').val();
                    $.ajax({
                        type: 'POST',
                        url: url,
                        data: data,
                        beforeSend: function (xhr) {
                            xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                        },
                        success: function (resultObject) {
                            console.log(JSON.stringify(resultObject));
                            $('#hdn_Id').val(resultObject.Id);
                            envelope.Id = resultObject.Id;

                            //Update user specs and send welcome SMS
                            UpdateUserById(true, envelope);

                        },
                        error: function (err) {
                            var errorEnvelope = {};
                            console.log(err.responseJSON);
                           
                            console.log('An error occured in api/AccountController/Register: ' + JSON.stringify(err, null, 3));
                            swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.<br><br><span style="font-size: 8pt; text-align: left;">' + JSON.stringify(err.responseJSON,null,3) + '</span>', 'error').then(function () {
                                //sessionStorage.clear();
                                //window.location.href = 'index.html';
                            });
                        }
                    });

                }
                else {
                    //Existing member, just update
                    envelope.Id = $('#hdn_Id').val();
                    UpdateUserById(false, envelope);
                }

            }
        });

        $('#btnSaveTimeOff').click(function () {
            var envelope = {};
            envelope.StartDate = $('#winTimeOff_wleStartDate').val();
            envelope.EndDate = $('#winTimeOff_wleEndDate').val();
            envelope.Description = $('#winTimeOff_txtDescription').val();
            var clickCheckbox = document.querySelector('#winTimeOff_chkIsOffRoute');
            envelope.IsOffRoute = clickCheckbox.checked;
            envelope.MemberId = idToTimeOff;
            var createdByEntity = {};
            createdByEntity = JSON.parse(sessionStorage.getItem('entMember'));
            envelope.CreatedBy = createdByEntity.Id;

            if (ValidateInputTimeOff()) {
                return;
            }
            else {
                var token = JSON.parse(sessionStorage.getItem('token'));
                var member = JSON.parse(sessionStorage.getItem('entMember'));
                var eventInstance = event;
                var url = InfrastructureVariables.WebServicePath + "api/MemberTimeOffController/InsertMemberTimeOff";
                var data = {};
                data = envelope;
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: data,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                    },
                    success: function (resultObject) {
                        console.log(JSON.stringify(resultObject));
                        
                        //Update user specs and send welcome SMS
                        SelectMemberTimeOffByMemberId();
                        CleanNewFormTimeOff();

                    },
                    error: function (err) {
                        var errorEnvelope = {};
                        console.log(err.responseJSON);

                        console.log('An error occured in api/MemberTimeOffController/InsertMemberTimeOff: ' + JSON.stringify(err, null, 3));
                        swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.<br><br><span style="font-size: 8pt; text-align: left;">' + JSON.stringify(err.responseJSON, null, 3) + '</span>', 'error').then(function () {
                            //sessionStorage.clear();
                            //window.location.href = 'index.html';
                        });
                    }
                });
            }
        });

    });

    //#endregion

    //#region Events

    //var btnLogin_Click = function () {
    //  console.log('Clicked...');
    //};

    function SendWelcomeSMS(incomingData) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/WelcomeSMS";
        var data = {};
        data = incomingData;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/SendWelcomeSMS: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function UpdateUserById(isFirstTime,parameters) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/RegisterPozitif";
        var data = {};
        data = parameters;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                swal('Kullanıcı Kaydedildi', '', 'success').then(function () {
                    $('#myModal5').modal('hide');
                    CleanNewForm();
                    LoadMembers($('.js-switch1').is(':checked'));

                    if (isFirstTime)
                        SendWelcomeSMS(parameters);
                });
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/RegisterPozitif: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function ValidateInput() {
        var hasErrors = false;
        var messages = '<div style="width: 100%; text-align: left; color: red; font-size: 12px;"><ul>';
        if ($('#winNew_txtNameSurname').val() == '') {
            messages += '<li>Adı Soyadı zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtUserName').val() == '') {
            messages += '<li>Kullanıcı adı zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtHiringDate').val() == '') {
            messages += '<li>Giriş Tarihi zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_wleMemberTitle').val() == '') {
            messages += '<li>Ünvan zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtSocialSecurityNumber').val() == '') {
            messages += '<li>TCKN zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtEmail').val() == '') {
            messages += '<li>E-Posta zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_wleDirectReport').val() == '') {
            messages += '<li>Yönetici seçimi zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtPhoneNumber').val() == '') {
            messages += '<li>GSM no zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtDataNumber').val() == '') {
            messages += '<li>Data no zorunludur. Eğer data hattı yoksa, GSM numarasını kullanın.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtAddress').val() == '') {
            messages += '<li>Adres zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winNew_wleAddressCity').val() == '') {
            messages += '<li>Adres Şehri seçilmelidir.</li>';
            hasErrors = true;
        }
        if ($('#winNew_txtAddressCoordinateX').val() == '' || $('#winNew_txtAddressCoordinateY').val() == '') {
            messages += '<li>Adres X ve Y Koordinatları girilmesi zorunludur. koordinat bilinmiyorsa adres arama çubuğunu kullanabilirsiniz.</li>';
            hasErrors = true;
        }
        if ($('#winNew_wleDeviceModel').val() == '') {
            messages += '<li>Cihaz modeli girişi zorunludur. MerchPlus Android uygulaması kullanılmayacaksa Diğer seçeneğini seçiniz.</li>';
            hasErrors = true;
        }
        if ($('#winNew_wleDeviceModel').val() != '' && $('#winNew_wleDeviceModel').text() != 'Diğer' && $('#winNew_txtIMEINumber').val() == '') {
            messages += '<li>Seçilen cihaz modeli için IMEI girişi zorunludur.</li>';
            hasErrors = true;
        }

        var numberOfCompanies = 0;
        $('.chkCompany').each(function (index) {
            if ($(this).prop('checked') == true)
                numberOfCompanies += 1;
        });
        if (numberOfCompanies == 0) {
            messages += '<li>Kullanıcıya atanan en az 1 müşteri olmalıdır.</li>';
            hasErrors = true;
        }



        messages += '</ul></div>';

        if (hasErrors)
        {
            swal('Hoppa...', messages, 'error').then(function () {
                return hasErrors;
            });
        }

        return hasErrors;
    }

    function ValidateInputTimeOff() {
        var hasErrors = false;
        var messages = '<div style="width: 100%; text-align: left; color: red; font-size: 12px;"><ul>';
        if ($('#winTimeOff_wleStartDate').val() == '') {
            messages += '<li>Başlangıç Tarihi zorunludur.</li>';
            hasErrors = true;
        }
        if ($('#winTimeOff_wleEndDate').val() == '') {
            messages += '<li>Bitiş Tarihi zorunludur.</li>';
            hasErrors = true;
        }

        var startDate = new Date(
            $('#winTimeOff_wleStartDate').val().substring(6),
            $('#winTimeOff_wleStartDate').val().substring(3, 5)-1,
            $('#winTimeOff_wleStartDate').val().substring(0, 2)
            );
        var endDate = new Date(
            $('#winTimeOff_wleEndDate').val().substring(6),
            $('#winTimeOff_wleEndDate').val().substring(3, 5)-1,
            $('#winTimeOff_wleEndDate').val().substring(0, 2)
            );

        //alert(startDate);
        //alert(endDate);
        if (endDate < startDate)
        {
            messages += '<li>Başlangıç Tarihi seçtiğiniz Bitiş Tarihinden ileride.</li>';
            hasErrors = true;
        }

        if ($('#winTimeOff_txtDescription').val() == '') {
            messages += '<li>Açıklama girilmesi zorunludur!</li>';
            hasErrors = true;
        }

        messages += '</ul></div>';

        if (hasErrors) {
            swal('Hoppa...', messages, 'error').then(function () {
                return hasErrors;
            });
        }

        return hasErrors;
    }

    function LoadDistricts() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DistrictController/SelectDistrictGridData";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                $("#pnlTowns").jstree({
                    "checkbox": {
                        "keep_selected_style": false
                    },
                    "core": {
                       'data': resultObject.Content
                    },
                    "plugins": ["checkbox","massload"]
                });
            },
            error: function (err) {
                console.log('An error occured in api/TownController/SelectTownGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function LoadCustomers() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerController/SelectCustomer";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                var output = '';
                $.each(resultObject.Content, function (index, value) {
                    output += '<td style="text-align: center; padding:20px;">' +
                              '    <img src="' + value.ProfilePicturePath + '" class="img-circle" style="width: 100px; border: solid 1px #CCC;" /><br />' +
                              '    <input class="chkCompany" type="checkbox" bsm-id="' + value.Id + '" style="-webkit-transform: scale(3);" />' +
                              '</td>';
                });
                $('#pnlCustomerList').html(output);

            },
            error: function (err) {
                console.log('An error occured in api/CustomerController/SelectCustomer: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function LoadDeviceModel() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/DeviceModelController/SelectDeviceModel";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                $("#winNew_wleDeviceModel").kendoComboBox({
                    dataTextField: "Name",
                    dataValueField: "Id",
                    dataSource: resultObject.Content,
                    filter: "contains",
                    suggest: true,
                    index: 3,
                    sort: { field: "Name", dir: "asc" }
                });
                $("#winNew_wleDeviceModel").data("kendoComboBox").value("");

            },
            error: function (err) {
                console.log('An error occured in api/DeviceModelController/SelectDeviceModel: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function MaskPhoneNumbers() {
        $("#winNew_txtPhoneNumber").kendoMaskedTextBox({
            mask: "(999) 000-0000"
        });
        $("#winNew_txtDataNumber").kendoMaskedTextBox({
            mask: "(999) 000-0000"
        });
    }

    function LoadCities() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CityController/SelectCity";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                $("#winNew_wleAddressCity").kendoComboBox({
                    dataTextField: "Name",
                    dataValueField: "Id",
                    dataSource: resultObject.Content,
                    filter: "contains",
                    suggest: true,
                    index: 3,
                    sort: { field: "Name", dir: "asc" }
                });
                $("#winNew_wleAddressCity").data("kendoComboBox").value("");

            },
            error: function (err) {
                console.log('An error occured in api/CityController/SelectCity: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function changeSwitcheryState(el, value) {
        if ($(el).is(':checked') != value) {
            $(el).trigger("click")
        }
    }

    function InitOnOffSwitches() {
        var elem = document.querySelector('.js-switch');
        var switchery = new Switchery(elem, { color: '#1AB394' });

        var elem2 = document.querySelector('#winTimeOff_chkIsOffRoute');
        var switchery2 = new Switchery(elem2, { color: '#1AB394' });

        var elem3 = document.querySelector('.js-switch1');
        var switchery3 = new Switchery(elem3, { color: '#1AB394' });

        $('.js-switch1').change(function (e) {
            LoadMembers($('.js-switch1').is(':checked'));
        });
    }

    function LoadDirectReport() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/SelectMemberByCustomerIdGridData";
        var data = {};
        var envelope = {};
        envelope = JSON.parse(sessionStorage.getItem('currentCustomer'));
        data.Data = envelope;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                $("#winNew_wleDirectReport").kendoComboBox({
                    dataTextField: "NameSurname",
                    dataValueField: "Id",
                    dataSource: resultObject.Content,
                    filter: "contains",
                    suggest: true,
                    index: 3
                });
                $("#winNew_wleDirectReport").data("kendoComboBox").value("");

            },
            error: function (err) {
                console.log('An error occured in api/MemberController/SelectMemberByCustomerIdGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function LoadCustomerTitle() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberTitleController/SelectMemberTitle";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                $("#winNew_wleMemberTitle").kendoComboBox({
                    dataTextField: "Name",
                    dataValueField: "Id",
                    dataSource: resultObject.Content,
                    filter: "contains",
                    suggest: true,
                    index: 3
                });
                $("#winNew_wleMemberTitle").data("kendoComboBox").value("");
                $("#winNew_wleMemberTitle").change(function () {
                    console.log($("#winNew_wleMemberTitle").val());
                });
            },
            error: function (err) {
                console.log('An error occured in var url = InfrastructureVariables.WebServicePath + "api/MemberTitleController/SelectMemberTitle: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function SetDatePickers() {
        $('#winNew_txtHiringDate').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd.mm.yyyy',
        });

        $('#winNew_txtLeavingDate').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd.mm.yyyy',
        });

        $('#winTimeOff_wleStartDate').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd.mm.yyyy',
            weekStart: 1,
        });

        $('#winTimeOff_wleEndDate').datepicker({
            todayBtn: "linked",
            keyboardNavigation: false,
            forceParse: false,
            calendarWeeks: true,
            autoclose: true,
            format: 'dd.mm.yyyy',
            weekStart: 1
        });
    }

    function CleanNewForm() {
        $('#hdn_id').val('0');
        $('#winNew_lblBase64Encoded').val('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAABHNCS…DO3CVpIkoDWABwBou7qYYBnBaRMSIaE5FjXq93fMnDmpz/By+xMHYU7gPUAAAAAElFTkSuQmCC');
        $('#winNew_txtNameSurname').val(null);
        $('#winNew_txtUserName').val(null);
        $('#winNew_txtHiringDate').val(null);
        
        var dropdownlist_MemberTitle = $("#winNew_wleMemberTitle").data("kendoComboBox");
        dropdownlist_MemberTitle.value(null);

        $('#winNew_txtSocialSecurityNumber').val(null);
        $('#winNew_txtEmail').val(null);
        $('#winNew_txtLeavingDate').val(null);
        
        var dropdownlist_DirectReport = $("#winNew_wleDirectReport").data("kendoComboBox");
        dropdownlist_DirectReport.value(null);

        $('#winNew_txtPhoneNumber').val(null);
        $('#winNew_txtDataNumber').val(null);
        $('#winNew_txtAddress').val(null);
        
        var dropdownlist_AddressCity = $("#winNew_wleAddressCity").data("kendoComboBox");
        dropdownlist_AddressCity.value(null);

        $('#winNew_txtAddressCoordinateX').val(null);
        $('#winNew_txtAddressCoordinateY').val(null);
        $('#winNew_txtIMEINumber').val(null);

        var dropdownlist_DeviceModel = $("#winNew_wleDeviceModel").data("kendoComboBox");
        dropdownlist_DeviceModel.value(null);

        $('#lblCaption').html('Yeni Kullanıcı');

        $('.chkCompany').each(function () {
            $(this).prop('checked', false);
        });

        //var tree = $('#pnlTowns');
        //tree.jstree(true).deselect_all();


    }

    function CleanNewFormTimeOff() {
        $('#winTimeOff_txtDescription').val(null);
        $('#winTimeOff_wleStartDate').val(null);
        $('#winTimeOff_wleEndDate').val(null);
    }

    function LoadMembers() {


        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/Members_MemberListGridData";
        var data = {};
        var currentCustomer = {};
        currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        data.Data = currentCustomer;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject));

                dataGrid = $("#gridContainer").dxDataGrid({
                    height: '400px',
                    contentReadyAction: function () {
                        $("#gridContainer .dx-scrollable").dxScrollable({ showScrollbar: 'always' });
                    },
                    allowColumnReordering: true,
                    allowColumnResizing: true,
                    columnAutoWidth: true,
                    columnChooser: {
                        enabled: true
                    },
                    columnFixing: {
                        enabled: true
                    },
                    dataSource: resultObject.Content,
                    filterRow: {
                        visible: true,
                        applyFilter: "auto"
                    },
                    paging: {
                        pageSize: 100
                    },
                    scrolling: {
                        mode: "virtual"
                    },
                    searchPanel: {
                        visible: true,
                        width: '250px',
                        placeholder: "Search"
                    },
                    headerFilter: {
                        visible: true
                    },
                    selection: {
                        mode: "single"
                    },
                    "export": {
                        enabled: true,
                        fileName: "Günlük Route Uyumluluk Raporu",
                        allowExportSelectedData: false
                    },
                    onContentReady: function (e) {
                        $('.btnResetPassword').off('click');
                        $('.btnEditUser').off('click');
                        $('.btnUsertTimeOff').off('click');
                        $('.btnResetPassword').click(function () {
                            idToReset = $(this).attr('bsm-id');
                            //console.log(idToReset);
                            swal({
                                title: "Onayınız Gerekiyor",
                                text: "Seçilen kullanıcının şifresi sıfırlanacak ve kendisine SMS olarak gönderilecektir.",
                                type: "warning",
                                showCancelButton: true,
                                confirmButtonColor: "#DD6B55",
                                confirmButtonText: "Evet, Şifreyi sıfırla",
                                cancelButtonText: "Hayır, Vazgeçtim"
                            }).then(function () {
                                ChangePasswordByUserId();
                            }, function (dismiss) {
                                ;
                            });
                        });

                        $('.btnEditUser').click(function () {
                            idToEdit = $(this).attr('bsm-id');

                            SelectMemberByIdForEdit();

                        });
                        $('.btnUsertTimeOff').click(function () {
                            CleanNewFormTimeOff();
                            idToTimeOff = $(this).attr('bsm-id');
                            SelectMemberTimeOffByMemberId();
                        });
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            fixed: true,
                            dataField: "Id",
                            caption: ' ',
                            width: '300px',
                            cellTemplate: function (container, options) {
                                $('<button type="button" style="padding: 3px 13px;" bsm-id="' + options.data.Id + '" class="btnEditUser btn btn-default pull-left"><i class="fa fa-edit"></i></button>').appendTo(container);
                                $('<button type="button" style="padding: 3px 13px;" bsm-id="' + options.data.Id + '" class="btnUsertTimeOff btn btn-default pull-left"><i class="fa fa-calendar"></i> Off Günler</button>').appendTo(container);
                                $('<button type="button" style="padding: 3px 13px;" bsm-id="' + options.data.Id + '" class="btnResetPassword btn btn-default pull-left"><i class="fa fa-key"></i> Şifre Sıfırla</button>').appendTo(container);
                            }
                        },
                        {
                            dataField: "MemberTitleName",
                            caption: "Ünvan",
                            fixed: true,
                        },
                        {
                            dataField: "NameSurname",
                            caption: "Adı Soyadı",
                            fixed: true,
                        },
                        {
                            dataField: "PhoneNumber",
                            caption: "Tel No",
                        },
                        {
                            dataField: "DataNumber",
                            caption: "Data No",
                        },
                        {
                            dataField: "DeviceModelName",
                            caption: "Cihaz",
                        },
                    ],
                }).dxDataGrid('instance');

                setTimeout(function () {
                    dataGrid.repaint();
                }, 2000);

                /* ESKI IMPLEMENTASYON
                var output = '';
                var lastTitle = '';

                $.each(resultObject.Content, function (index, value) {

                    if ($('.js-switch1').is(':checked')) {

                        if (value.MemberTitleId == 3 || value.MemberTitleId == 5) {
                            if (lastTitle != value.MemberTitleName) {
                                lastTitle = value.MemberTitleName;
                                output += '<div style="clear: both;"></div><h1 style="padding-left: 20px; padding-bottom: 20px;">' + value.MemberTitleName + '</h1>';
                            }

                            var dataNumber = '(' + value.DataNumber.substring(0, 3) + ') ' + value.DataNumber.substring(3, 6) + ' - ' + value.DataNumber.substring(6);
                            var phoneNumber = '(' + value.PhoneNumber.substring(0, 3) + ') ' + value.PhoneNumber.substring(3, 6) + ' - ' + value.PhoneNumber.substring(6);

                            output += '<div class="col-lg-6">' +
                                '   <div class="contact-box">' +
                                '       <div class="col-sm-3">' +
                                '           <div class="text-center">' +
                                '               <img alt="image" class="img-circle m-t-xs img-responsive" src="' + value.ProfilePicturePath + '">' +
                                '           </div>' +
                                '       </div>' +
                                '       <div class="col-sm-9">' +
                                '           <h2 style="color:#336699;"><strong>' + value.NameSurname + '</strong></h2>' +
                                '           <h4>' + value.MemberTitleName + '</h4>' +
                                '           <hr class="hr-line-dashed" />' +
                                '           <p><i class="fa fa-wifi">&nbsp;&nbsp;&nbsp;&nbsp;</i>' + dataNumber + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-phone">&nbsp;&nbsp;&nbsp;&nbsp;</i>' + phoneNumber + '</p>' +
                                '           <p></p>' +
                                '           <p>&nbsp;<i class="fa fa-mobile">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</i> ' + value.DeviceModelName + '</p>' +
                                '           <p>&nbsp;<i class="fa fa-tag">&nbsp;&nbsp;&nbsp;</i> ' + value.IMEINumber + '</p>' +
                                '           <hr class="hr-line-dashed" />' +
                                '           <address>' +
                                '               <strong>Adres</strong><br>' +
                                '               ' + value.Address +
                                '           </address>' +
                                '           <button type="button" bsm-id="' + value.Id + '" class="btnEditUser btn btn-default pull-left"><i class="fa fa-edit"></i></button>' +
                                '           <button type="button" bsm-id="' + value.Id + '" class="btnUsertTimeOff btn btn-default pull-left"><i class="fa fa-calendar"></i> Off Günler</button>' +
                                '           <button type="button" bsm-id="' + value.Id + '" class="btnResetPassword btn btn-default pull-left"><i class="fa fa-key"></i> Şifre Sıfırla</button>' +
                                '       </div>' +
                                '       <div class="clearfix"></div>' +
                                '   </div>' +
                                '</div>';
                        }
                    }
                    else
                    {
                        if (lastTitle != value.MemberTitleName) {
                            lastTitle = value.MemberTitleName;
                            output += '<div style="clear: both;"></div><h1 style="padding-left: 20px; padding-bottom: 20px;">' + value.MemberTitleName + '</h1>';
                        }

                        var dataNumber = '(' + value.DataNumber.substring(0, 3) + ') ' + value.DataNumber.substring(3, 6) + ' - ' + value.DataNumber.substring(6);
                        var phoneNumber = '(' + value.PhoneNumber.substring(0, 3) + ') ' + value.PhoneNumber.substring(3, 6) + ' - ' + value.PhoneNumber.substring(6);

                        output += '<div class="col-lg-6">' +
                            '   <div class="contact-box">' +
                            '       <div class="col-sm-3">' +
                            '           <div class="text-center">' +
                            '               <img alt="image" class="img-circle m-t-xs img-responsive" src="' + value.ProfilePicturePath + '">' +
                            '           </div>' +
                            '       </div>' +
                            '       <div class="col-sm-9">' +
                            '           <h2 style="color:#336699;"><strong>' + value.NameSurname + '</strong></h2>' +
                            '           <h4>' + value.MemberTitleName + '</h4>' +
                            '           <hr class="hr-line-dashed" />' +
                            '           <p><i class="fa fa-wifi">&nbsp;&nbsp;&nbsp;&nbsp;</i>' + dataNumber + '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-phone">&nbsp;&nbsp;&nbsp;&nbsp;</i>' + phoneNumber + '</p>' +
                            '           <p></p>' +
                            '           <p>&nbsp;<i class="fa fa-mobile">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</i> ' + value.DeviceModelName + '</p>' +
                            '           <p>&nbsp;<i class="fa fa-tag">&nbsp;&nbsp;&nbsp;</i> ' + value.IMEINumber + '</p>' +
                            '           <hr class="hr-line-dashed" />' +
                            '           <address>' +
                            '               <strong>Adres</strong><br>' +
                            '               ' + value.Address +
                            '           </address>' +
                            '           <button type="button" bsm-id="' + value.Id + '" class="btnEditUser btn btn-default pull-left"><i class="fa fa-edit"></i></button>' +
                            '           <button type="button" bsm-id="' + value.Id + '" class="btnUsertTimeOff btn btn-default pull-left"><i class="fa fa-calendar"></i> Off Günler</button>' +
                            '           <button type="button" bsm-id="' + value.Id + '" class="btnResetPassword btn btn-default pull-left"><i class="fa fa-key"></i> Şifre Sıfırla</button>' +
                            '       </div>' +
                            '       <div class="clearfix"></div>' +
                            '   </div>' +
                            '</div>';

                    }

                });

                $('#pnlMemberList').html(output);
                */
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/SelectMemberByCustomerIdGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });


    };

    function SelectMemberTimeOffByMemberId() {


        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberTimeOffController/SelectMemberTimeOffByMemberId";
        var data = {};
        data.MemberId = idToTimeOff;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject));

                $("#pnlTimeOffList").kendoGrid({
                    dataSource: {
                        data: resultObject.Content,
                        schema: {
                            model: {
                                fields: {
                                    MemberId: { type: "string" },
                                    StartDate: { type: "date" },
                                    EndDate: { type: "date" },
                                    IsOffRoute: { type: "boolean" },
                                    IsOffRouteLabel: { type: "string" },
                                    Description: { type: "string" },
                                    CreatedByNameSurname: { type: "string" }
                                }
                            }
                        },
                    },
                    scrollable: false,
                    sortable: true,
                    filterable: true,
                    pageable: false,
                    noRecords: {
                        template: "<div style='width: 100%; height: 100px; text-align: center; padding-top: 70px;'>Giriş bulunmuyor</div>"
                    },
                    columns: [
                        { field: 'StartDate', title: 'Başlangıç', format: '{0:dd.MM.yyyy}', width: '100px', filterable: { multi: true } },
                        { field: 'EndDate', title: 'Bitiş', format: '{0:dd.MM.yyyy}', width: '100px', filterable: { multi: true } },
                        { field: 'IsOffRouteLabel', title: 'Off-Route?', width: '120px', filterable: { multi: true } },
                        { field: 'Description', title: 'Açıklama', filterable: { multi: true } },
                        { field: 'CreatedByNameSurname', title: 'Oluşturan', width: '175px', filterable: { multi: true } },
                        {
                            command:
                              {
                                  name: 'cmdDeleteMemberTimeOff',
                                  click: deleteMemberTimeOff,
                                  text: '<i class="fa fa-trash"></i>',
                                  width: '30px'
                              }, title: " ", width: "30px"
                        }
                    ]
                });

                var gridElement = $("#pnlTimeOffList");
                gridElement.resize();

            },
            error: function (err) {
                console.log('An error occured in api/MemberTimeOffController/SelectMemberTimeOffByMemberId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });
        

        $('#myModal6').modal('show');
    }

    function deleteMemberTimeOff(e) {
        e.preventDefault();

        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));

        swal({
            title: "Onayınız Gerekiyor",
            text: "Kullanıcının Time-Off kaydı silinecek. Bu işlem geri alınamaz.<br><br>Devam etmek istiyor musunuz?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Evet, kaydı sil",
            cancelButtonText: "Hayır, vazgeçtim"
        }).then(function () {
            DeleteMemberTimeOffById(dataItem["Id"])
        }, function (dismiss) {
            ;
        });
    }

    function DeleteMemberTimeOffById(id) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberTimeOffController/DeleteMemberTimeOffById";
        var data = {};
        data.Id = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                SelectMemberTimeOffByMemberId();
            },
            error: function (err) {
                console.log('An error occured in api/MemberTimeOffController/DeleteMemberTimeOffById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function resizeGrid() {
        var gridElement = $("#grid");
        var dataArea = gridElement.find(".k-grid-content");
        var newHeight = gridElement.parent().innerHeight() - 40;
        var diff = gridElement.innerHeight() - dataArea.innerHeight();
        gridElement.height(newHeight);
        dataArea.height(newHeight - diff);
    }

    function ChangePasswordByUserId() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/Account/ChangePassword";
        var data = {};
        data.UserId = idToReset;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject));
                swal('Şifre Değiştirildi', 'Kullanıcının şifresi Data hattına SMS olarak gönderildi.', 'success');
                SendChangePasswordSMS(data.UserId);
            },
            error: function (err) {
                console.log('An error occured in api/Account/ChangePassword: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function SendChangePasswordSMS(userId) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/ChangePasswordSMS";
        var data = {};
        data.Id = userId;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject));
                idToReset = '';
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/ChangePasswordSMS: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function SelectMemberByIdForEdit() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/SelectMemberByIdForEdit";
        var data = {};
        data.Id = idToEdit;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject));

                $('#myModal5').modal('show');
                $('#winNew_lblBase64Encoded').val(null);
                $('#hdn_Id').val(resultObject.Member.Id);
                $('#lblCaption').html(resultObject.Member.NameSurname);
                $('#winNew_txtNameSurname').val(resultObject.Member.NameSurname);
                $('#winNew_txtUserName').val(resultObject.Member.UserName);
                var hiringDate = new Date(resultObject.Member.HiringDate);
                $('#winNew_txtHiringDate').datepicker("setDate", hiringDate);
                
                var dropdownlist_MemberTitle = $("#winNew_wleMemberTitle").data("kendoComboBox");
                dropdownlist_MemberTitle.select(function (dataItem) {
                    return dataItem.Id === resultObject.Member.MemberTitleId;
                });

                $('#winNew_pctProfilePicture').attr('src', resultObject.Member.ProfilePicturePath);
                $('#winNew_txtSocialSecurityNumber').val(resultObject.Member.SocialSecurityNumber);
                $('#winNew_txtEmail').val(resultObject.Member.Email);

                if (resultObject.Member.LeavingDate != null) {
                    var leavingDate = new Date(resultObject.Member.LeavingDate);
                    $('#winNew_txtLeavingDate').datepicker("setDate", leavingDate);
                }
                else {
                    $('#winNew_txtLeavingDate').datepicker("setDate", null);
                }

                changeSwitcheryState($('#winNew_chkIsActive'), resultObject.Member.IsActive);
                
                var dropdownlist_DirectReport = $("#winNew_wleDirectReport").data("kendoComboBox");
                dropdownlist_DirectReport.select(function (dataItem) {
                    return dataItem.Id === resultObject.Member.DirectReportId;
                });

                var phoneNumber = $("#winNew_txtPhoneNumber").data("kendoMaskedTextBox");
                phoneNumber.value(resultObject.Member.PhoneNumber);

                var dataNumber = $("#winNew_txtDataNumber").data("kendoMaskedTextBox");
                dataNumber.value(resultObject.Member.DataNumber);

                $('#winNew_txtAddress').val(resultObject.Member.Address);
                
                var dropdownlist_AddressCity = $("#winNew_wleAddressCity").data("kendoComboBox");
                dropdownlist_AddressCity.select(function (dataItem) {
                    return dataItem.Id === resultObject.Member.AddressCityId;
                });

                $('#winNew_txtAddressCoordinateX').val(resultObject.Member.AddressCoordinateX);
                $('#winNew_txtAddressCoordinateY').val(resultObject.Member.AddressCoordinateY);
                
                var dropdownlist_DeviceModel = $("#winNew_wleDeviceModel").data("kendoComboBox");
                dropdownlist_DeviceModel.select(function (dataItem) {
                    return dataItem.Id === resultObject.Member.DeviceModelId;
                });

                $('#winNew_txtIMEINumber').val(resultObject.Member.IMEINumber);

                $('.chkCompany').each(function () {
                    //Clear all checks before setting permissions
                    $(this).prop('checked', false);
                });
                $.each(resultObject.MemberCustomer, function (index, value) {
                    $('.chkCompany[bsm-id="' + value.Id + '"]').prop('checked', true);
                });

                var tree = $('#pnlTowns');
                tree.jstree(true).deselect_all();
                $.each(resultObject.MemberDistrict, function (index, value) {
                    tree.jstree(true).check_node(value.DistrictId);
                });

                initMap();
                setTimeout(function () {
                    google.maps.event.trigger(map, 'resize');
                    map.setCenter(new google.maps.LatLng(document.getElementById('winNew_txtAddressCoordinateX').value, document.getElementById('winNew_txtAddressCoordinateY').value));
                    var marker = new google.maps.Marker({
                        map: map,
                        position: new google.maps.LatLng(document.getElementById('winNew_txtAddressCoordinateX').value, document.getElementById('winNew_txtAddressCoordinateY').value)
                    });
                }, 1000);
                
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/SelectMemberByIdForEdit: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    $(window).resize(function () {
        resizeGrid();
    });

    //#endregion

}());
