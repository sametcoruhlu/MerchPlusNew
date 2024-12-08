(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Route Uyumluluk Controller module. Let's get dirty...");
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
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteUyumlulukRaporu";
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
                        fileName: "Günlük Route Uyumluluk Raporu",
                        allowExportSelectedData: false
                    },
                    onContentReady: function (e) {
                        $(".btnViewDetails").off('click');
                        $(".btnViewDetails").click(function (e) {
                            e.preventDefault();
                            LoadDetailsByMemberIdEffectiveDate($(this).attr('bsm-memberId'), new Date($(this).attr('bsm-date')));
                        });
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
                            fixed: true
                        },
                        {
                            fixed: true,
                            dataField: "Id",
                            caption: ' ',
                            cellTemplate: function (container, options) {
                                $('<button class="btn btn-default btn-xs btnViewDetails" bsm-memberId="' + options.data.MemberId + '" bsm-date="' + options.data.DateOfVisit + '">Plan</button>&nbsp;').appendTo(container);
                            }
                        },
                        {
                            dataField: "Day",
                            caption: "Gün",
                        },
                        {
                            dataField: "PlannedLocationCount",
                            caption: "Planlanan",
                        },
                        {
                            dataField: "ActualLocationCount",
                            caption: "Gerçekleşen",
                        },
                        {
                            dataField: "SuccessPercent",
                            caption: "Başarı (%)",
                        },
                        {
                            dataField: "OffRouteDescription",
                            caption: "Açıklama",
                        },
                        {
                            dataField: "OffRouteCreatedBy",
                            caption: "Yetkili",
                        },
                    ],
                    summary: {
                        totalItems: [
                            {
                                column: "PlannedLocationCount",
                                summaryType: "sum",
                                valueFormat: "number",
                                customizeText: function (data) {
                                    return "Σ: " + numeral(data.value).format();
                                }
                            },
                            {
                                column: "ActualLocationCount",
                                summaryType: "sum",
                                valueFormat: "number",
                                customizeText: function (data) {
                                    return "Σ: " + numeral(data.value).format();
                                }
                            },
                            {
                                column: "SuccessPercent",
                                summaryType: "avg",
                                valueFormat: "number",
                                customizeText: function (data) {
                                    return "Ort: " + numeral(data.value).format() + " %";
                                }
                            },
                        ]
                    }
                }).dxDataGrid('instance');

                setTimeout(function () {
                    dataGrid.repaint();
                }, 2000);
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteUyumlulukRaporu: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadDetailsByMemberIdEffectiveDate(memberId, effectiveDate) {
        var dateToGet = ConvertDateToSqlDate(effectiveDate);
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate";
        var data = {};
        data.EffectiveDate = dateToGet;
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

                var output = '';
                $.each(resultObject.Content, function (index, value) {
                    output += '<tr>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + value.RetailShopCategory + '</td>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + value.RetailName + '</td>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + value.RetailShopName + '</td>';
                    output += '     <td>' + (value.Status == 'Gerçekleşti' ? '<span style="color: green;">Gerçekleşti</span>' : '<span style="color: red;">Gerçekleşmedi!</span>') + '</td>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (value.EntryOn == null ? '-' : moment(new Date(value.EntryOn)).format("HH:mm:ss")) + '</td>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (value.ExitOn == null ? '-' : moment(new Date(value.ExitOn)).format('HH:mm:ss')) + '</td>';
                    output += '     <td style="font-family: Trebuchet MS; font-size: 10pt;">' + (value.TotalTimeSpent == null ? '-' : value.TotalTimeSpent + ' dak') + '</td>';
                    output += '</tr>';
                });

                $('#pnlDetails').html(output);
                $('#myModal5').modal('show');
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate: ' + JSON.stringify(err, null, 3));
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