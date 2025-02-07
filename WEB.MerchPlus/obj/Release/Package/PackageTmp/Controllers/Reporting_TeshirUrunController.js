var listOfMemberRoute = [];
var submitButton;
var downloadButton;
var downloadAllButton;
(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Teshir Urun Controller module. Let's get dirty...");
        PrepareDatePickers();
        PrepareGenerateButton();
    });

    function ExportImages() {
        if (listOfMemberRoute.length == 0) {
            swal('Hoppa', 'Dışa aktarılacak hiç resim bulunamadı!', 'error');
            return;
        }

        swal({
            title: 'Resimler İndirilecek',
            text: 'Seçilen tarih aralığında bulunan ' + listOfMemberRoute.length + ' ziyaretin tüm resimleri indirilecek. Bu işlem biraz zaman alabilir. İndirme bağlantısını görene kadar ekranı kapatmayınız!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Devam Et',
            cancelButtonText: 'Vazgeç',
        }).then(function () {
            ExportImagesFromServer();
        })
    }
    function ExportImagesAll() {
        swal({
            title: 'Resimler İndirilecek',
            text: 'Seçilen tarih aralığında bulunan tüm ziyaretlerin tüm resimleri indirilecek. Bu işlem biraz zaman alabilir. İndirme bağlantısını görene kadar ekranı kapatmayınız!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Devam Et',
            cancelButtonText: 'Vazgeç',
        }).then(function () {
            ExportImagesFromServerAll();
        })
    }

    function GenerateReport() {
        if ($('#wleStartDate').val() == '' || $('#wleStartDate').datepicker('getDate') == 'Invalid Date') {
            swal('Hata', 'Başlangıç tarihi seçiniz!', 'error');
            return;
        }
        if ($('#wleEndDate').val() == '' || $('#wleEndDate').datepicker('getDate') == 'Invalid Date') {
            swal('Hata', 'Bitiş tarihi seçiniz!', 'error');
            return;
        }

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/TeshirUrunRaporu";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        data.StartDate = ConvertDateToSqlDate($('#wleStartDate').datepicker('getDate'));
        data.EndDate = ConvertDateToSqlDate($('#wleEndDate').datepicker('getDate'));
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                submitButton.ladda('start');
            },
            success: function (resultObject) {
                
                listOfMemberRoute = [];
                $.each(resultObject.Content, function (index, value) {
                    if (listOfMemberRoute.indexOf(value.MemberRouteId) == -1)
                        listOfMemberRoute.push(value.MemberRouteId);
                });

                console.log(listOfMemberRoute);

                console.log(JSON.stringify(resultObject, null, 3));
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
                        fileName: "Teşhir Ürün Raporu",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            dataField: "MerchNameSurname",
                            caption: "Merch",
                        },
                        {
                            dataField: "DateOfVisit",
                            caption: "Tarih",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                        },
                        {
                            dataField: "Day",
                            caption: "Gün",
                        },
                        {
                            caption: '',
                            width: '75px',
                            cellTemplate: function (container, options) {
                                $('<button type="button" bsm-id="' + options.data.MemberRouteId + '" class="btn btn-default btn-xs btnShowPhoto"><i class="fa fa-camera"></i> Foto</button>').appendTo(container);
                            },
                            cssClass: 'alignMiddle'
                        },
                        {
                            dataField: "RetailName",
                            caption: "Müşteri",
                        },
                        {
                            dataField: "RetailShopName",
                            caption: "Mağaza",
                        },
                        {
                            dataField: "PromotionTypeName",
                            caption: "Teşhir Türü",
                        },
                        {
                            dataField: "CustomerProductName",
                            caption: "Ürün",
                        },
                        {
                            caption: ' ',
                            cellTemplate: function (container, options) {
                                $('<button type="button" bsm-memberrouteid="' + options.data.MemberRouteId + '" bsm-promotiontypeid="' + options.data.PromotionTypeId + '" class="btn btn-default btn-xs btnShowHistory"><i class="fa fa-history"></i></button>').appendTo(container);
                            }
                        }
                    ],
                    onContentReady: function (e) {
                        $('.btnShowPhoto').off('click');
                        $('.btnShowPhoto').click(function (e) {
                            e.preventDefault();
                            ShowPhotoByMemberRouteId($(this).attr('bsm-id'));
                        });

                        $('.btnShowHistory').off('click');
                        $('.btnShowHistory').click(function (e) {
                            e.preventDefault();
                            ShowHistory($(this).attr('bsm-memberrouteid'), $(this).attr('bsm-promotiontypeid'));
                        });

                        $("#gridContainer").css('height', '330px');
                        setTimeout(function () {
                            repaintCounter += 1;
                            if (repaintCounter <= 5)
                                dataGrid.repaint();
                        }, 100);
                    }
                }).dxDataGrid('instance');

                submitButton.ladda('stop');
            },
            error: function (err) {
                submitButton.ladda('stop');
                console.log('An error occured in api/ReportingController/TeshirOzelRaporu: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function ShowPhotoByMemberRouteId(memberRouteId) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/SelectMemberRoutePhotoByMemberRouteId";
        var data = {};
        data.MemberRouteId = memberRouteId;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                var output = '';
                $.each(resultObject.Content, function (index, value) {
                    output += '<div style="float: left; font-family: Trebuchet MS; padding-left: 20px;"><a class="example-image-link" href="http://mp.bsmyazilim.com/' + value.ProfilePicturePath + '" data-lightbox="example-set" data-title="' + value.RetailShopName + ' | ' + value.NameSurname + ' | ' + moment(value.CreatedOn).format('DD.MM.YYYY HH:mm') + '"><img class="img-responsive img-circle" style="height: 100px;" src="http://mp.bsmyazilim.com/' + value.ProfilePicturePath + '" alt=""/></a></div>';
                });

                $('#lstPhoto').html(output);

                $('#pnlPhoto').modal('show');

            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/SelectMemberRoutePhotoByMemberRouteId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function ShowHistory(memberRouteId, promotionTypeId) {
        console.log(memberRouteId + ', ' + promotionTypeId);
        $('#pnlHistory').modal('show');
        $('#lstHistory').html('Lütfen bekleyin... Raporunuz oluşturuluyor.');

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteDetailController/SelectPromotionHistoryByMemberRouteIdPromotionTypeId";
        var data = {};
        data.MemberRouteId = memberRouteId;
        data.PromotionTypeId = promotionTypeId;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                var output = '';
                $.each(resultObject.Content, function (index, value) {
                    output += '<h2 style="color: #336699; font-weight: bold;">' + value.CustomerProduct.Name + '</h2>';

                    var outputHistory = '<table class="table table-condensed table-borders table-hover" style="width: 50%;">';
                    $.each(value.History, function (index_History, value_History) {
                        outputHistory += '<tr>';
                        outputHistory += '<td>' + moment(new Date(value_History.EffectiveDate)).format("DD.MM.YYYY") + '</td>';
                        outputHistory += '<td>' + value_History.PromotionTypeName + '</td>';
                        outputHistory += '</tr>';
                    });
                    if (value.History.length == 0)
                        outputHistory = '<tr><td><span style="color: red;">Son sekiz ziyarette ürün için bilgi girişi yapılmamış</span></td></tr>';

                    output += outputHistory;
                    output += '</table>';

                });

                $('#lstHistory').html(output);

            },
            error: function (err) {
                console.log('An error occured in api/MemberRouteDetailController/SelectPromotionHistoryByMemberRouteIdPromotionTypeId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function PrepareGenerateButton() {
        submitButton = $('#btnGenerate').ladda();
        submitButton.click(function (e) {
            e.preventDefault();
            GenerateReport();
        });
    }

    function PrepareDatePickers() {
        $('#wleStartDate').datepicker({
            format: 'dd.mm.yyyy',
            autoclose: true,
            todayBtn: true,
            todayHighlight: true,
            language: 'tr',
        });

        $('#wleEndDate').datepicker({
            format: 'dd.mm.yyyy',
            endDate: '0d',
            autoclose: true,
            todayBtn: true,
            todayHighlight: true,
            language: 'tr',
        });

        $('#wleStartDate').datepicker('setDate', GetCurrentDate());
        $('#wleEndDate').datepicker('setDate', GetCurrentDate());
    }

}());