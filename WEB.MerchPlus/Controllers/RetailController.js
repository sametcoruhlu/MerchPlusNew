
(function () {

    $(function () {
        console.log("Hello, IIFE loaded for Retail module. Let's get dirty...");

        LoadRetailCategory();
        LoadRetail();

        $('#btnSave').click(function (e) {
            e.preventDefault();
            UpdateRetailById();
        });

        $('#btnNew').click(function (e) {
            e.preventDefault();
            $('#lblId').val('0');
            $('#txtName').val('');
            $('#wleRetailCategoryId').val([]);
            $('#winEdit').modal('show');
        });

        $('#btnRetailShopCategorySave').click(function (e) {
            e.preventDefault();
            InsertNewRetailShopCategory();
        });

    });

    function InsertNewRetailShopCategory() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopCategoryController/InsertRetailShopCategory";
        var data = {};
        data.RetailId = $('#lblRetailId').val();
        data.Name = $('#txtRetailCategoryName').val();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                $('#winRetailShopCategory').modal('hide');
                swal('İşlem Tamam', 'Kanal başarıyla eklendi.', 'success');
            },
            error: function (err) {
                console.log('An error occured in api/RetailShopCategoryController/InsertRetailShopCategory: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function UpdateRetailById() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailController/UpdateRetailWisely";
        var data = {};
        data.Id = $('#lblId').val();
        data.Name = $('#txtName').val();
        data.RetailCategoryId = $('#wleRetailCategoryId').val();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                $('#winEdit').modal('hide');
                LoadRetail();
                swal('İşlem Tamam', 'Müşteri tanımı başarıyla güncellendi', 'success');
            },
            error: function (err) {
                console.log('An error occured in api/RetailController/UpdateRetailWisely: ' + JSON.stringify(err, null, 3));
                $('#winEdit').modal('hide');
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadRetailCategory() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailCategoryController/SelectRetailCategory";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                var output = '<option></option>';
                $.each(resultObject.Content, function (index, value) {
                    output += '<option value="' + value.Id + '">' + value.Name + '</option>';
                });
                $('#wleRetailCategoryId').html(output);
                $('#wleRetailCategoryId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                    dropdownParent: $('#winEdit')
                });
            },
            error: function (err) {
                console.log('An error occured in api/RetailCategoryController/SelectRetailCategory: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadRetail() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailController/SelectRetailGridData";
        var data = {};
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                $("#gridContainer").dxDataGrid({
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
                            dataField: "Id",
                            caption: "Ref",
                            width: '60px',
                            dataType: 'string'
                        },
                        {
                            dataField: "Name",
                            caption: "Müşteri Adı",
                            width: '300px'
                        },
                        {
                            dataField: "RetailCategoryName",
                            caption: "Kanal",
                            width: '100px'
                        },
                        {
                            caption: " ",
                            cellTemplate: function (container, options) {
                                $('<button type="button" class="btn btn-default btn-xs btnEdit" bsm-name="' + options.data.Name + '" bsm-id="' + options.data.Id + '" bsm-retailcategoryid="' + options.data.RetailCategoryId + '">Düzenle</button>&nbsp;&nbsp;&nbsp;' +
                                  '<button type="button" class="btn btn-primary btn-xs btnFormat" bsm-name="' + options.data.Name + '" bsm-id="' + options.data.Id + '" bsm-retailcategoryname="' + options.data.RetailCategoryName + '">Formatlar</button>').appendTo(container);
                            }
                        },
                    ],
                    onContentReady: function (e) {
                        $('.btnEdit').off('click');
                        $('.btnEdit').click(function (e) {
                            $('#lblId').val($(this).attr('bsm-id'));
                            $('#txtName').val($(this).attr('bsm-name'));
                            console.log($(this).attr('bsm-id'));
                            console.log($(this).attr('bsm-retailcategoryid'));
                            console.log($(this).attr('bsm-name'));
                            $('#wleRetailCategoryId').val($(this).attr('bsm-retailcategoryid'));
                            $('#wleRetailCategoryId').trigger('change');
                            $('#winEdit').modal('show');
                        });

                        $('.btnFormat').off('click');
                        $('.btnFormat').click(function (e) {
                            $('#lblRetailId').val($(this).attr('bsm-id'));
                            $('#lblRetailName').html($(this).attr('bsm-name'));
                            $('#lblRetailCategoryName').html($(this).attr('bsm-retailcategoryname'));
                            SelectRetailShopCategoryByRetailId($(this).attr('bsm-id'));
                            $('#winRetailShopCategory').modal('show');
                        });
                    }
                }).dxDataGrid('instance');
            },
            error: function (err) {
                console.log('An error occured in api/RetailController/SelectRetailGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function SelectRetailShopCategoryByRetailId(idToSelect) {


        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopCategoryController/SelectRetailShopCategoryByRetailId";
        var data = {};
        data.RetailId = idToSelect;
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
                    output += '<tr>' +
                              '<td style="width: 75px;">' + '<button class="btn btn-warning btn-xs btnEditRetailShopCategory" bsm-id="' + value.Id + '">Düzenle</button>' + '</td>' +
                              '<td>' + value.Name + '</td>' +
                              '</tr>';
                });

                if (output == '')
                    output = '<tr><td colspan="2">Tanımlı format yok</td></tr>';

                $('#pnlRetailShopCategory').html(output);

                $('.btnEditRetailShopCategory').off('click');
                $('.btnEditRetailShopCategory').click(function (e) {
                    e.preventDefault();
                    console.log($(this).attr('bsm-id'));
                });
            },
            error: function (err) {
                console.log('An error occured in api/RetailShopCategoryController/SelectRetailShopCategoryByRetailId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });


    }

}());
