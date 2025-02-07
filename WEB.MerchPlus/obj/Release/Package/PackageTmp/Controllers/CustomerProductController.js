(function () {

    $(function () {

        console.log("Hello, IIFE loaded for Customer Product module. Let's get dirty...");

        LoadCategories();

        function LoadReport() {
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/SelectCustomerProductByCustomerIdGridData";
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
                    //console.log(JSON.stringify(resultObject, null, 3));
                    var dataGrid = $("#gridContainer").dxDataGrid({
                        height: '300px',
                        contentReadyAction: function () {
                            $("#gridContainer .dx-scrollable").dxScrollable({ showScrollbar: 'always' });
                        },
                        onContentReady: function (e) {
                            $('.btnEdit').off('click');
                            $('.btnEdit').click(function (e) {
                                console.log($(this).attr('bsm-id'));
                                OpenWindow($(this).attr('bsm-id'));
                            });
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
                            fileName: "Ürün Listesi",
                            allowExportSelectedData: false
                        },
                        showRowLines: true,
                        hoverStateEnabled: true,
                        columns: [
                            {
                                dataField: "CustomerBrandCategoryParentParentName",
                                caption: "Marka",
                            },
                            {
                                dataField: "CustomerBrandCategoryParentName",
                                caption: "Kategori",
                            },
                            {
                                dataField: "CustomerBrandCategoryName",
                                caption: "Ürün Grubu",
                            },
                            {
                                dataField: "Name",
                                caption: "Ürün Adı",
                            },
                            {
                                dataField: "ProductCode",
                                caption: "Ürün Kodu",
                            },
                            {
                                dataField: "ProductBarcode",
                                caption: "Ürün Barkodu",
                            },
                            {
                                dataField: "SuggestedListPrice",
                                caption: "Tavsiye Fiyat",
                            },
                            {
                                dataField: "SuggestedActivityPrice",
                                caption: "Aktivite Fiyatı",
                            },
                            {
                                dataField: "PackagingSize",
                                caption: "Label",
                            },
                            {
                                dataField: "CaseEach",
                                caption: "Koli İçi",
                            },
                            {
                                caption: "Aksiyonlar",
                                cellTemplate: function (container, options) {
                                    $('<button type="button" class="btnEdit btn btn-default btn-xs" bsm-id="' + options.data.Id + '">Düzenle</button>').appendTo(container);
                                }
                            },

                        ],
                        summary: {
                            totalItems: [
                                {
                                    column: "Name",
                                    summaryType: "count",
                                    valueFormat: "number",
                                    customizeText: function (data) {
                                        return "Σ: " + numeral(data.value).format();
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
                    console.log('An error occured in api/CustomerProductController/SelectCustomerProductByCustomerIdGridData: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });
        }

        function LoadCategories() {
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerBrandController/SelectCustomerBrandGridData";
            var data = {};
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    //console.log(JSON.stringify(resultObject, null, 3));

                    $('#wleCustomerBrandCategoryId').combotree();
                    console.log('combotree initilized');
                    $('#wleCustomerBrandCategoryId').combotree('loadData', resultObject.Content);
                    console.log('data loaded');
                    LoadReport();

                },
                error: function (err) {
                    console.log('An error occured in api/CustomerBrandController/SelectCustomerBrandGridData: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                        //sessionStorage.clear();
                        //window.location.href = 'index.html';
                    });
                }
            });
        }

        function OpenWindow(id) {
            $('#winEdit').modal('show');
            $('#lblId').val(id);

            if (id == 0) {
                $('#txtName').val('');
                $('#txtProductBarcode').val('');
                $('#txtProductCode').val('');
                $('#txtCaseEach').val('');
                $('#txtSuggestedListPrice').val(0);
                $('#txtSuggestedActivityPrice').val(0);
                $('#txtPackagingSize').val(0);
                $('#btnDelete').hide();
            } else {
                var token = JSON.parse(sessionStorage.getItem('token'));
                var member = JSON.parse(sessionStorage.getItem('entMember'));
                var eventInstance = event;
                var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/SelectCustomerProductById";
                var data = {};
                data.Id = $('#lblId').val();
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: data,
                    beforeSend: function (xhr) {
                        xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                    },
                    success: function (resultObject) {
                        console.log(JSON.stringify(resultObject, null, 3));
                        $('#txtName').val(resultObject.CustomerProduct.Name);
                        $('#txtProductBarcode').val(resultObject.CustomerProduct.ProductBarcode);
                        $('#txtProductCode').val(resultObject.CustomerProduct.ProductCode);
                        $('#txtCaseEach').val(resultObject.CustomerProduct.CaseEach);
                        $('#txtSuggestedListPrice').val(resultObject.CustomerProduct.SuggestedListPrice);
                        $('#txtSuggestedActivityPrice').val(resultObject.CustomerProduct.SuggestedActivityPrice);
                        $('#txtPackagingSize').val(resultObject.CustomerProduct.PackagingSize);
                        $('#wleCustomerBrandCategoryId').combotree('setValue', resultObject.CustomerProduct.CustomerBrandCategoryId);
                        $('#btnDelete').show();
                        $('#btnDelete').attr('bsm-id', resultObject.CustomerProduct.Id);
                        $('#btnDelete').off('click');
                        $('#btnDelete').click(function (e) {
                            $('#winEdit').modal('hide');
                            var hede = $(this).attr('bsm-id');
                            swal('Emin misiniz?', 'Bu ürünü silerek ürüne ait tüm tarihçeyi, rakip ürün eşleştirmelerini ve ürünün mağaza eşleştirmelerini de silmiş olacaksınız. Devam edelim mi?', 'warning').then(function () {
                                DeleteCustomerProductById(hede);
                            });
                        });
                    },
                    error: function (err) {
                        console.log('An error occured in api/CustomerProductController/SelectCustomerProductById: ' + JSON.stringify(err, null, 3));
                        swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                    }
                });
            }
        }

        function DeleteCustomerProductById(idToDelete) {

            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/DeleteCustomerProductByIdWisely";
            var data = {};
            data.Id = idToDelete;
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    swal('Silindi', 'Ürün başarıyla silindi', 'success');
                    LoadReport();
                },
                error: function (err) {
                    console.log('An error occured in api/CustomerProductController/DeleteCustomerProductByIdWisely: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        }

        $('#btnSave').click(function (e) {

            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/UpdateCustomerProductById";
            var data = {};
            data.Id = $('#lblId').val();
            data.Name = $('#txtName').val();
            data.ProductBarcode = $('#txtProductBarcode').val();
            data.ProductCode = $('#txtProductCode').val();
            data.CaseEach = $('#txtCaseEach').val();
            data.SuggestedListPrice = $('#txtSuggestedListPrice').val();
            data.SuggestedActivityPrice = $('#txtSuggestedActivityPrice').val();
            data.PackagingSize = $('#txtPackagingSize').val();
            var t = $('#wleCustomerBrandCategoryId').combotree('tree');
            var n = t.tree('getSelected');		
            data.CustomerBrandCategoryId = n.id;
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    swal('OK', 'Güncelleme başarıyla tamamlandı', 'success');
                    $('#winEdit').modal('hide');
                    LoadReport();
                },
                error: function (err) {
                    console.log('An error occured in api/CustomerProductController/UpdateCustomerProductById: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                        //sessionStorage.clear();
                        //window.location.href = 'index.html';
                    });
                }
            });

        });

        $('#btnAddNew').click(function (e) {
            e.preventDefault();
            OpenWindow(0);
        });
       
    });

}());
