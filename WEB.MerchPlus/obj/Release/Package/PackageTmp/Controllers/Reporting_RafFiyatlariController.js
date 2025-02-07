var submitButton;
(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Raf Fiyatları Controller module. Let's get dirty...");
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
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RafFiyatlariRaporu";
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
                //console.log(JSON.stringify(resultObject, null, 3));
                dataGrid = $("#gridContainer").dxDataGrid({
                    height: '300px',
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
                        fileName: "Raf Fiyatları Raporu",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            dataField: "MerchNameSurname",
                            caption: "Merch",
                            fixed: true,
                        },
                        {
                            dataField: "DateOfVisit",
                            caption: "Tarih",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                            fixed: true,
                        },
                        {
                            dataField: "Day",
                            caption: "Gün",
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
                            dataField: "CustomerProductBrand",
                            caption: "Marka",
                        },
                        {
                            dataField: "CustomerProduct",
                            caption: "Ürün",
                        },
                        {
                            dataField: "PackagingSize",
                            caption: "Boyut",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span>' + numeral(options.data.PackagingSize).format('0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "PackagingType",
                            caption: "Cinsi",
                        },
                        {
                            dataField: "ShelfPrice",
                            caption: currentCustomer.Name + " Raf Fiyatı",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span><b>' + numeral(options.data.ShelfPrice).format('0,0.00') + '</b></span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "SuggestedListPrice",
                            caption: currentCustomer.Name + " Tavsiye Fiyat",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span><b>' + numeral(options.data.SuggestedListPrice).format('0,0.00') + '</b></span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "SuggestedListPriceRatio",
                            caption: "Tavsiye Fiyat Oran",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                if (options.data.SuggestedListPriceRatio * 100 == 0.00)
                                    $('<span style="color: green;"><b>' + numeral(options.data.SuggestedListPriceRatio * 100).format('0,0.00') + ' %</b></span>').appendTo(container);
                                else if (options.data.SuggestedListPriceRatio * 100 < 0.00)
                                    $('<span style="color: red;"><b>' + numeral(options.data.SuggestedListPriceRatio * 100).format('0,0.00') + ' %</b></span>').appendTo(container);
                                else
                                    $('<span style="color: orange;"><b>' + numeral(options.data.SuggestedListPriceRatio * 100).format('0,0.00') + ' %</b></span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "CustomerProductBrand2",
                            caption: "Rakip Marka",
                        },
                        {
                            dataField: "CustomerProduct2",
                            caption: "Rakip Ürün",
                        },
                        {
                            dataField: "PackagingSize2",
                            caption: "Boyut ",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span>' + numeral(options.data.PackagingSize2).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "PackagingType2",
                            caption: "Cinsi ",
                        },
                        {
                            dataField: "CompetitorShelfPrice",
                            caption: "Rakip Raf Fiyat ",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span>' + numeral(options.data.CompetitorShelfPrice).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "CustomerKilogramPrice",
                            caption: currentCustomer.Name + " KG Fiyat ",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span>' + numeral(options.data.CustomerKilogramPrice).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "CompetitorKilogramPrice",
                            caption: "Rakip KG Fiyat ",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                $('<span>' + numeral(options.data.CompetitorKilogramPrice).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "RatioNew",
                            caption: "Oran",
                            dataType: 'number',
                            cellTemplate: function (container, options) {
                                if (options.data.Ratio * 100 < 0)
                                    $('<span style="color: red;"><b>' + numeral(options.data.RatioNew * 100).format('0,0.00') + ' %</b></span>').appendTo(container);
                                else
                                    $('<span style="color: green;"><b>' + numeral(options.data.RatioNew * 100).format('0,0.00') + ' %</b></span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "ActivityPrice",
                            caption: "Aktivite Fiyatı",
                            dataType: 'number',
                            cellTemplate: function(container, options) {
                                $('<span>' + numeral(options.data.ActivityPrice).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "SuggestedActivityPrice",
                            caption: "Tavsiye Edilen Aktivite Fiyatı",
                            dataType: 'number',
                            cellTemplate: function(container, options) {
                                $('<span>' + numeral(options.data.SuggestedActivityPrice).format('0,0.00') + '</span>').appendTo(container);
                            }
                        },

                    ],
                }).dxDataGrid('instance');

                submitButton.ladda('stop');

                setTimeout(function () {
                    dataGrid.repaint();
                }, 2000);
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RafFiyatlariRaporu: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                submitButton.ladda('stop');
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