(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Var Yok Controller module. Let's get dirty...");
        PrepareDatePickers();
        PrepareGenerateButton();
    });

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
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/VarYokRaporu";
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
            },
            success: function (resultObject) {
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
                        fileName: "Raf Penetrasyon Raporu",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            dataField: "DateOfVisit",
                            caption: "Tarih",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                        },
                        {
                            dataField: "CityName",
                            caption: "Şehir",
                        },
                        {
                            dataField: "RetailCategoryName",
                            caption: "Kanal",
                        },
                        {
                            dataField: "RetailName",
                            caption: "Müşteri",
                        },
                        {
                            dataField: "RetailShopCategoryName",
                            caption: "Format",
                        },
                        {
                            dataField: "RetailShopName",
                            caption: "Mağaza",
                        },
                        {
                            dataField: "CustomerBrandName",
                            caption: "Firma",
                        },
                        {
                            dataField: "ProductName",
                            caption: "Ürün",
                        },
                        {
                            dataField: "IsAvailable",
                            caption: "Var/Yok?",
                        },
                        //{
                        //    caption: '',
                        //    width: '75px',
                        //    cellTemplate: function (container, options) {
                        //        $('<button type="button" bsm-id="' + options.data.MemberRouteId + '" class="btn btn-default btn-xs btnShowPhoto ' + (options.data.PhotoCount == 0?' hidden':'') + '"><i class="fa fa-camera"></i> Foto</button>').appendTo(container);
                        //    },
                        //    cssClass: 'alignMiddle'
                        //},
                        {
                            dataField: "ShelfStock",
                            caption: "Raf Stok",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span style="font-weight: bold;">' + numeral(options.data.ShelfStock).format() + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "WarehouseStock",
                            caption: "Depo Stok",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span style="font-weight: bold;">' + numeral(options.data.WarehouseStock).format() + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "TotalStock",
                            caption: "Toplam Stok",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span style="font-weight: bold; color: #336699;">' + numeral(options.data.TotalStock).format() + '</span>').appendTo(container);
                            }
                        },
                    ],
                    onContentReady: function (e) {
                        $('.btnShowPhoto').off('click');
                        $('.btnShowPhoto').click(function (e) {
                            e.preventDefault();
                            ShowPhotoByMemberRouteId($(this).attr('bsm-id'));
                        });

                        $("#gridContainer").css('height', '330px');
                        setTimeout(function () {
                            repaintCounter += 1;
                            if (repaintCounter <= 5)
                                dataGrid.repaint();
                        }, 100);
                    }
                }).dxDataGrid('instance');
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/VarYokRaporu: ' + JSON.stringify(err, null, 3));
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
                    output += '<div style="float: left; padding-left: 20px;"><a class="example-image-link" href="http://mp.bsmyazilim.com/' + value.ProfilePicturePath + '" data-lightbox="example-set" data-title="' + value.RetailShopName + ' | ' + value.NameSurname + ' | ' + moment(value.CreatedOn).format('DD.MM.YYYY HH:mm') + '"><img class="img-responsive img-circle" style="height: 100px;" src="http://mp.bsmyazilim.com/' + value.ProfilePicturePath + '" alt=""/></a></div>';
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

    function PrepareGenerateButton() {
        $('#btnGenerate').off('click');
        $('#btnGenerate').click(function (e) {
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