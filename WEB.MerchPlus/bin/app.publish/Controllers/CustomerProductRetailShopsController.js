(function () {

    //#region View Load

    $(function () {
        //console.log("Hello, IIFE loaded for Customer Product Retail Shops Controller module. Let's get dirty...");
        GenerateReport();

        $('#btnNew_AssignProductByChannel').click(function (e) {
            e.preventDefault();
            ShowChannelEntry();
        });
        $('#winRetailCategory_btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#winRetailCategory_wleRetailCategoryId').val() == '')
            {
                swal('Hoppa', 'Kanal seçmeyi unuttunuz!', 'error');
                return;
            }

            var selectedItems = [];
            selectedItems = $('#winRetailCategory_tblProductList input:checked');
            if (selectedItems.length == 0)
            {
                swal('Hoppa', 'Hiç ürün seçmediniz. Ekleyecek birşey bulamadım?', 'error');
                return;
            }

            swal({
                title: 'Emin misiniz?',
                text: "Seçtiğiniz " + selectedItems.length + " ürün seçilen kanal altındaki tüm mağaza şubelerine tanımlanacak.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, Tanımla',
                cancelButtonText: 'Vazgeç'
            }).then(function () {
                InsertCustomerProductRetailShopWisely_RetailCategoryMode();
            })
        });

        $('#btnNew_AssignProductByRetail').click(function (e) {
            e.preventDefault();
            ShowRetailEntry();
        });
        $('#winRetail_btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#winRetail_wleRetailId').val() == '') {
                swal('Hoppa', 'Müşteri seçmeyi unuttunuz!', 'error');
                return;
            }

            var selectedItems = [];
            selectedItems = $('#winRetail_tblProductList input:checked');
            if (selectedItems.length == 0) {
                swal('Hoppa', 'Hiç ürün seçmediniz. Ekleyecek birşey bulamadım?', 'error');
                return;
            }

            swal({
                title: 'Emin misiniz?',
                text: "Seçtiğiniz " + selectedItems.length + " ürün seçilen müşterinin tüm şubelerine tanımlanacak.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, Tanımla',
                cancelButtonText: 'Vazgeç'
            }).then(function () {
                InsertCustomerProductRetailShopWisely_RetailMode();
            })
        });

        $('#btnNew_AssignProductByRetailShopCategory').click(function (e) {
            e.preventDefault();
            ShowRetailShopCategoryEntry();
        });
        $('#winRetailShopCategory_btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#winRetailShopCategory_wleRetailShopCategoryId').val() == '') {
                swal('Hoppa', 'Formatı seçmeyi unuttunuz!', 'error');
                return;
            }

            var selectedItems = [];
            selectedItems = $('#winRetailShopCategory_tblProductList input:checked');
            if (selectedItems.length == 0) {
                swal('Hoppa', 'Hiç ürün seçmediniz. Ekleyecek birşey bulamadım?', 'error');
                return;
            }

            swal({
                title: 'Emin misiniz?',
                text: "Seçtiğiniz " + selectedItems.length + " ürün seçilen müşteri formatının tüm şubelerine tanımlanacak.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, Tanımla',
                cancelButtonText: 'Vazgeç'
            }).then(function () {
                InsertCustomerProductRetailShopWisely_RetailShopCategoryMode();
            })
        });

        $('#btnNew_AssignProductByRetailShop').click(function (e) {
            e.preventDefault();
            ShowRetailShopEntry();
        });
        $('#winRetailShop_btnAdd').click(function (e) {
            e.preventDefault();
            if ($('#winRetailShop_wleRetailShopId').val() == '') {
                swal('Hoppa', 'Mağaza seçmeyi unuttunuz!', 'error');
                return;
            }

            var selectedItems = [];
            selectedItems = $('#winRetailShop_tblProductList input:checked');
            if (selectedItems.length == 0) {
                swal('Hoppa', 'Hiç ürün seçmediniz. Ekleyecek birşey bulamadım?', 'error');
                return;
            }

            swal({
                title: 'Emin misiniz?',
                text: "Seçtiğiniz " + selectedItems.length + " ürün seçilen mağazaya tanımlanacak.",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Evet, Tanımla',
                cancelButtonText: 'Vazgeç'
            }).then(function () {
                InsertCustomerProductRetailShopWisely_RetailShopMode();
            })
        });

    });

    function InsertCustomerProductRetailShopWisely_RetailCategoryMode() {

        var customerProductId = [];
        $.each($('#winRetailCategory_tblProductList input:checked'), function (index, value) {
            customerProductId.push($(value).attr('bsm-id'));
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailCategoryMode";
        var data = {};
        data.CustomerId = customer.Id;
        data.CustomerProductId = customerProductId;
        data.RetailCategoryId = $('#winRetailCategory_wleRetailCategoryId').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                swal('Ok', 'Tanımlamalar gerçekleşti. Listeden kontrol edebilirsiniz.', 'success');
                GenerateReport();
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailCategoryMode: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function InsertCustomerProductRetailShopWisely_RetailMode() {

        var customerProductId = [];
        $.each($('#winRetail_tblProductList input:checked'), function (index, value) {
            customerProductId.push($(value).attr('bsm-id'));
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailMode";
        var data = {};
        data.CustomerId = customer.Id;
        data.CustomerProductId = customerProductId;
        data.RetailId = $('#winRetail_wleRetailId').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                swal('Ok', 'Tanımlamalar gerçekleşti. Listeden kontrol edebilirsiniz.', 'success');
                GenerateReport();
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailMode: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function InsertCustomerProductRetailShopWisely_RetailShopCategoryMode() {

        var customerProductId = [];
        $.each($('#winRetailShopCategory_tblProductList input:checked'), function (index, value) {
            customerProductId.push($(value).attr('bsm-id'));
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailShopCategoryMode";
        var data = {};
        data.CustomerId = customer.Id;
        data.CustomerProductId = customerProductId;
        data.RetailShopCategoryId = $('#winRetailShopCategory_wleRetailShopCategoryId').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                swal('Ok', 'Tanımlamalar gerçekleşti. Listeden kontrol edebilirsiniz.', 'success');
                GenerateReport();
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailShopCategoryMode: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function InsertCustomerProductRetailShopWisely_RetailShopMode() {

        var customerProductId = [];
        $.each($('#winRetailShop_tblProductList input:checked'), function (index, value) {
            customerProductId.push($(value).attr('bsm-id'));
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailShopMode";
        var data = {};
        data.CustomerId = customer.Id;
        data.CustomerProductId = customerProductId;
        data.RetailShopId = $('#winRetailShop_wleRetailShopId').val();
        data.CreatedBy = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                swal('Ok', 'Tanımlamalar gerçekleşti. Listeden kontrol edebilirsiniz.', 'success');
                GenerateReport();
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/InsertCustomerProductRetailShopWisely_RetailShopMode: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function GenerateReport() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/SelectProductListingPerRetailShopByCustomerId";
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
                ////console.log(JSON.stringify(resultObject, null, 3));
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
                        pageSize: 9
                    },
                    pager: {
                        showNavigationButtons: true,
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
                        fileName: "Listeli Ürünler",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
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
                            dataField: "CityName",
                            caption: "Şehir",
                        },
                        {
                            dataField: "CustomerProductName",
                            caption: currentCustomer.Name + ' Ürünü'
                        },
                        {
                            dataField: "CompetitorProductName",
                            caption: 'Rakip Ürün'
                        },
                        {
                            dataField: "Id",
                            caption: 'Aksiyonlar',
                            cellTemplate: function (container, options) {
                                $('<button type="button" class="btnDelete btn btn-danger btn-xs" bsm-id="' + options.data.Id + '"><i class="fa fa-trash"> </i></button>').appendTo(container);
                            }
                        },
                    ],
                    onContentReady: function () {
                        $('.btnDelete').off('click');
                        $('.btnDelete').click(function (e) {
                            DeleteMappingById($(this).attr('bsm-id'));
                        });
                    }
                }).dxDataGrid('instance');
            },
            error: function (err) {
                //console.log('An error occured in api/ReportingController/SelectProductListingPerRetailShopByCustomerId: ' + JSON.stringify(err, null, 3));
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

    function DeleteMappingById(id) {
        swal({
            title: 'Emin misiniz?',
            text: "Seçtiğiniz ürün mağaza eşleştirilmesi silincek.",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil',
            cancelButtonText: 'Vazgeç'
        }).then(function () {

            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/DeleteCustomerProductRetailShopById";
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
                    GenerateReport();
                    swal('Başarılı', 'Tanımlama başarıyla silindi. Birazdan raporunuz güncellenecek.', 'success');
                },
                error: function (err) {
                    //console.log('An error occured in api/CustomerProductRetailShopController/DeleteCustomerProductRetailShopById: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        })
    }
    function ShowChannelEntry() {
        $.when(LoadRetailCategory(), LoadProducts()).done(function (result_RetailCategory, result_CustomerProduct) {
            $('#winRetailCategory').modal('show');
        });

    }
    function ShowRetailEntry() {
        $.when(LoadRetail(), LoadProducts()).done(function (result_Retail, result_CustomerProduct) {
            $('#winRetail').modal('show');
        });
    }
    function ShowRetailShopCategoryEntry() {
        $.when(LoadRetailShopCategory(), LoadProducts()).done(function (result_RetailShopCategory, result_CustomerProduct) {
            $('#winRetailShopCategory').modal('show');
        });
    }
    function ShowRetailShopEntry() {
        $.when(LoadRetailShop(), LoadProducts()).done(function (result_RetailShop, result_CustomerProduct) {
            $('#winRetailShop').modal('show');
        });
    }

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
                ////console.log(JSON.stringify(resultObject, null, 3));

                var dataMapped_RetailCategory = $.map(resultObject.Content, function (obj) {
                    obj.id = obj.id || obj.Id; // replace pk with your identifier
                    obj.text = obj.text || obj.Name;
                    return obj;
                });

                $('#winRetailCategory_wleRetailCategoryId').html('<option></option>');
                $('#winRetailCategory_wleRetailCategoryId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                    data: dataMapped_RetailCategory,
                    templateResult: function (a) {
                        var $result = $(
                            '<ul class="sortable-list connectList agile-list ui-sortable" style="padding: 0px; font-family: Trebuchet MS;">' +
                            '    <li class="info-element" style="margin: 0px !important;">' +
                            '        <h2 style="font-size:14pt; margin-top:0px; color:#4F4F4F;">' + a.Name + '</h2>' +
                            '    </li>' +
                            '</ul>'
                        );
                        return $result;
                    },
                    templateSelection: function (a, b) {
                        if (a.Id == null)
                            return $(
                                '<span>Seçiniz...</span>'
                            );

                        var $result = $(
                            '<span>' + a.Name + '</span>'
                        );
                        return $result;
                    },
                });
                $('#winRetailCategory_wleRetailCategoryId').change(function (e) {
                    //console.log($('#winRetailCategory_wleRetailCategoryId').val());
                    MarkAllItemsByRetailCategoryId($('#winRetailCategory_wleRetailCategoryId').val());
                });

            },
            error: function (err) {
                //console.log('An error occured in api/RetailCategoryController/SelectRetailCategory: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function LoadRetail() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailController/SelectRetail";
        var data = {};
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                ////console.log(JSON.stringify(resultObject, null, 3));

                var dataMapped_Retail = $.map(resultObject.Content, function (obj) {
                    obj.id = obj.id || obj.Id; // replace pk with your identifier
                    obj.text = obj.text || obj.Name;
                    return obj;
                });

                $('#winRetail_wleRetailId').html('<option></option>');
                $('#winRetail_wleRetailId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                    data: dataMapped_Retail,
                    templateResult: function (a) {
                        var $result = $(
                            '<ul class="sortable-list connectList agile-list ui-sortable" style="padding: 0px; font-family: Trebuchet MS;">' +
                            '    <li class="info-element" style="margin: 0px !important;">' +
                            '        <h2 style="font-size:14pt; margin-top:0px; color:#4F4F4F;">' + a.Name + '</h2>' +
                            '    </li>' +
                            '</ul>'
                        );
                        return $result;
                    },
                    templateSelection: function (a, b) {
                        if (a.Id == null)
                            return $(
                                '<span>Seçiniz...</span>'
                            );

                        var $result = $(
                            '<span>' + a.Name + '</span>'
                        );
                        return $result;
                    },
                });
                $('#winRetail_wleRetailId').change(function (e) {
                    //console.log($('#winRetail_wleRetailId').val());
                    MarkAllItemsByRetailId($('#winRetail_wleRetailId').val());
                });

            },
            error: function (err) {
                //console.log('An error occured in api/RetailCategoryController/SelectRetailCategory: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function LoadRetailShopCategory() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopCategoryController/SelectRetailShopCategoryGridData";
        var data = {};
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                ////console.log(JSON.stringify(resultObject, null, 3));

                var dataMapped_Retail = $.map(resultObject.Content, function (obj) {
                    obj.id = obj.id || obj.Id; // replace pk with your identifier
                    obj.text = obj.text || obj.Name;
                    return obj;
                });

                $('#winRetailShopCategory_wleRetailShopCategoryId').html('<option></option>');
                $('#winRetailShopCategory_wleRetailShopCategoryId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                    data: dataMapped_Retail,
                    templateResult: function (a) {
                        var $result = $(
                            '<ul class="sortable-list connectList agile-list ui-sortable" style="padding: 0px; font-family: Trebuchet MS;">' +
                            '    <li class="info-element" style="margin: 0px !important;">' +
                            '        <h2 style="font-size:14pt; margin-top:0px; color:#4F4F4F;">' + a.Name + '</h2>' +
                            '    </li>' +
                            '</ul>'
                        );
                        return $result;
                    },
                    templateSelection: function (a, b) {
                        if (a.Id == null)
                            return $(
                                '<span>Seçiniz...</span>'
                            );

                        var $result = $(
                            '<span>' + a.Name + '</span>'
                        );
                        return $result;
                    },
                });
                $('#winRetailShopCategory_wleRetailShopCategoryId').change(function (e) {
                    //console.log($('#winRetailShopCategory_wleRetailShopCategoryId').val());
                    MarkAllItemsByRetailShopCategoryId($('#winRetailShopCategory_wleRetailShopCategoryId').val());
                });

            },
            error: function (err) {
                //console.log('An error occured in api/RetailCategoryController/SelectRetailShopCategoryGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function LoadRetailShop() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopController/SelectRetailShopMapView";
        var data = {};
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                ////console.log(JSON.stringify(resultObject, null, 3));

                var dataMapped_Retail = $.map(resultObject.Content, function (obj) {
                    obj.id = obj.id || obj.Id; // replace pk with your identifier
                    obj.text = obj.text || obj.Name;
                    return obj;
                });

                $('#winRetailShop_wleRetailShopId').html('<option></option>');
                $('#winRetailShop_wleRetailShopId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                    data: dataMapped_Retail,
                    templateResult: function (a) {
                        var $result = $(
                            '<ul class="sortable-list connectList agile-list ui-sortable" style="padding: 0px; font-family: Trebuchet MS;">' +
                            '    <li class="info-element" style="margin: 0px !important;">' +
                            '        <h2 style="font-size:14pt; margin-top:0px; color:#4F4F4F;">' + a.Name + '</h2>' +
                            '    </li>' +
                            '</ul>'
                        );
                        return $result;
                    },
                    templateSelection: function (a, b) {
                        if (a.Id == null)
                            return $(
                                '<span>Seçiniz...</span>'
                            );

                        var $result = $(
                            '<span>' + a.Name + '</span>'
                        );
                        return $result;
                    },
                });
                $('#winRetailShop_wleRetailShopId').change(function (e) {
                    //console.log($('#winRetailShop_wleRetailShopId').val());
                    MarkAllItemsByRetailShopId($('#winRetailShop_wleRetailShopId').val());
                });

            },
            error: function (err) {
                //console.log('An error occured in api/RetailCategoryController/SelectRetailShopCategoryGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }
    function LoadProducts() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/SelectCustomerProductByCustomerId";
        var data = {};
        data.CustomerId = customer.Id;
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));

                var output = '';
                $.each(resultObject.Content, function (index, value) {
                    output += '<tr>' +
                        '<td style="text-align: center;"><input type="checkbox" class="checkboxSelector" bsm-id="' + value.Id + '" style="width: 30px;" name="animal" value="" />' + '</td>' +
                        '<td style="vertical-align: middle;">' + (value.ProductCode == null ? '' : value.ProductCode) + '</td>' +
                        '<td style="vertical-align: middle;">' + value.Name + '</td>' +
                        '<td style="vertical-align: middle;">' + (value.ProductBarcode == null ? '' : value.ProductBarcode) + '</td>' +
                        '</tr>';
                });


                $('#winRetailCategory_tblProductList').html(output);
                $('#winRetail_tblProductList').html(output);
                $('#winRetailShopCategory_tblProductList').html(output);
                $('#winRetailShop_tblProductList').html(output);

            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductController/SelectCustomerProductByCustomerId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function MarkAllItemsByRetailShopId(id) {

        $.each($('#winRetailShop_tblProductList :input'), function (index, value) {
            $(this).attr('checked', false);
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/SelectCustomerProductRetailShopByRetailShopId";
        var data = {};
        data.RetailShopId = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                $.each(resultObject.Content, function (indexContent, valueContent) {
                    $('#winRetailShop_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]').prop('checked', true);
                    ////console.log($('#winRetailShop_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]'));
                });
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });


    }
    function MarkAllItemsByRetailShopCategoryId(id) {

        $.each($('#winRetailShopCategory_tblProductList :input'), function (index, value) {
            $(this).attr('checked', false);
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/SelectCustomerProductRetailShopByRetailCategoryId";
        var data = {};
        data.RetailCategoryId = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                $.each(resultObject.Content, function (indexContent, valueContent) {
                    $('#winRetailShopCategory_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]').prop('checked', true);
                    ////console.log($('#winRetailShop_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]'));
                });
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });


    }
    function MarkAllItemsByRetailId(id) {

        $.each($('#winRetail_tblProductList :input'), function (index, value) {
            $(this).attr('checked', false);
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/SelectCustomerProductRetailShopByRetailId";
        var data = {};
        data.RetailId = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                $.each(resultObject.Content, function (indexContent, valueContent) {
                    $('#winRetail_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]').prop('checked', true);
                    ////console.log($('#winRetailShop_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]'));
                });
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });


    }
    function MarkAllItemsByRetailCategoryId(id) {

        $.each($('#winRetailCategory_tblProductList :input'), function (index, value) {
            $(this).attr('checked', false);
        });

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductRetailShopController/SelectCustomerProductRetailShopByRetailChannelId";
        var data = {};
        data.RetailCategoryId = id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                $.each(resultObject.Content, function (indexContent, valueContent) {
                    $('#winRetailCategory_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]').prop('checked', true);
                    ////console.log($('#winRetailShop_tblProductList :input[bsm-id="' + valueContent.CustomerProductId + '"]'));
                });
            },
            error: function (err) {
                //console.log('An error occured in api/CustomerProductRetailShopController/: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });


    }
}());