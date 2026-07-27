(function () {

    //#region View Load

    $(function () {
        console.log("CustomerFormFieldVisibility Controller loaded");

        // (Opsiyonel) 500 veren profil görseline fallback
        imgFallback();

        // Load member details for sidebar
        loadMemberDetails();

        // Initialize page
        loadCustomers();
        loadFieldNames();

        // Event handlers
        $('#customerSelect').on('change', function () {
            var customerId = $(this).val();
            if (customerId) {
                loadFieldVisibility(customerId);
            } else {
                $('#fieldVisibilityTable tbody').empty();
            }
        });

        $('#btnAddNew').on('click', function () {
            clearModal();
            $('#modalTitle').text('Alan Görünürlük Ayarı Ekle');
            $('#fieldModal').modal('show');
        });

        $('#btnSaveField').on('click', function () {
            var fieldId = $('#hiddenFieldId').val();
            if (fieldId) {
                updateFieldVisibility();
            } else {
                saveFieldVisibility();
            }
        });

        $('#btnSaveAll').on('click', function () {
            saveAllFieldVisibility();
        });

        $('#btnDeleteAll').on('click', function () {
            deleteAllFieldVisibility();
        });
    });

    //#endregion

    //#region Helpers (token/baseUrl/headers/log)

    function getTokenOrFail() {
        var t = JSON.parse(sessionStorage.getItem('token'));
        if (!t || !t.access_token) {
            showError('Oturum süresi dolmuş. Lütfen tekrar giriş yapın.');
            throw new Error('No token');
        }
        return t.access_token;
    }

    function baseUrl() {
        var b = (window.InfrastructureVariables && InfrastructureVariables.WebServicePath2) || '';
        return b && b.slice(-1) !== '/' ? (b + '/') : b;
    }

    function commonHeaders() {
        return { 'Authorization': 'Bearer ' + getTokenOrFail() };
    }

    function ajaxErr(where) {
        return function (xhr, statusText) {
            console.log('Error at ' + where, {
                status: xhr && xhr.status,
                statusText: xhr && xhr.statusText,
                resp: xhr && xhr.responseText
            });
            swal('Hoppa...', 'İstek sırasında bir hata oluştu.', 'error');
        };
    }

    function imgFallback() {
        $('img[src$="Images/ProfilePhotos/noProfile.png"]').on('error', function () {
            this.onerror = null;
            this.src = 'data:image/svg+xml;base64,PHN2ZyB3aWR0aD0iNDAiIGhlaWdodD0iNDAiIHZpZXdCb3g9IjAgMCA0MCA0MCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPGNpcmNsZSBjeD0iMjAiIGN5PSIyMCIgcj0iMjAiIGZpbGw9IiNlZWVlZWUiLz4KPHN2ZyB4PSI4IiB5PSI4IiB3aWR0aD0iMjQiIGhlaWdodD0iMjQiIHZpZXdCb3g9IjAgMCAyNCAyNCIgZmlsbD0ibm9uZSIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIj4KPHBhdGggZD0iTTEyIDEyQzE0LjIwOTEgMTIgMTYgMTAuMjA5MSAxNiA4QzE2IDUuNzkwODYgMTQuMjA5MSA0IDEyIDRDOS43OTA4NiA0IDggNS43OTA4NiA4IDhDOCAxMC4yMDkxIDkuNzkwODYgMTIgMTIgMTJaIiBmaWxsPSIjOTk5OTk5Ii8+CjxwYXRoIGQ9Ik0xMiAxNEM5Ljc5MDg2IDE0IDggMTUuNzkwOSA4IDE4VjIwSDE2VjE4QzE2IDE1Ljc5MDkgMTQuMjA5MSAxNCAxMiAxNFoiIGZpbGw9IiM5OTk5OTkiLz4KPC9zdmc+Cjwvc3ZnPgo=';
        });
    }

    function loadMemberDetails() {
        try {
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var memberTitle = JSON.parse(sessionStorage.getItem('entMemberTitle'));
            
            if (member && member.ProfilePicturePath) {
                $('#pctProfilePicture').attr('src', member.ProfilePicturePath);
            }
            if (memberTitle && memberTitle.Name) {
                $('#lblTitle').html(memberTitle.Name);
            }
            if (member && member.NameSurname) {
                $('#lblNameSurname').html(member.NameSurname);
            }
        } catch (e) {
            console.log('Error loading member details:', e);
        }
    }

    //#endregion

    //#region API Functions

    function loadCustomers() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var url = InfrastructureVariables.WebServicePath + "api/CustomerController/SelectCustomer";
        var data = {};
        console.log('loadCustomers - API URL:', url);

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
                    output += '<option value="' + value.Id + '">' + value.Name + '</option>';
                });
                
                var select = $('#customerSelect');
                select.empty();
                select.append('<option value="">Müşteri seçin...</option>');
                select.append(output);
            },
            error: function (err) {
                console.log('An error occured in api/CustomerController/SelectCustomer: ' + JSON.stringify(err, null, 3));
            }
        });
    }

    function loadFieldNames() {
        // Sabit field names - servis çağrısı yapmadan direkt kullan
        var fieldNames = [
            "FaceCount",
            "ShelfIndex",
            "ShelfStock",
            "Price",
            "WarehouseStock",
            "ActivityType",
            "DiscountPrice"
        ];

        var output = '';
        $.each(fieldNames, function (index, value) {
            output += '<option value="' + value + '">' + getFieldDisplayName(value) + '</option>';
        });
        
        var select = $('#txtFieldName');
        select.empty();
        select.append('<option value="">Alan seçin...</option>');
        select.append(output);
        
        console.log('Field names loaded locally:', fieldNames);
    }

    function loadFieldVisibility(customerId) {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var url = InfrastructureVariables.WebServicePath2 + "CustomerFormFieldVisibility/SelectByCustomerId";
        var data = { CustomerId: parseInt(customerId) };
        console.log('loadFieldVisibility - API URL:', url);
        console.log('loadFieldVisibility - Data:', JSON.stringify(data));
        console.log('loadFieldVisibility - Token:', token ? 'Token exists' : 'No token');

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                if (token && token.access_token) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                    console.log('Authorization header set');
                } else {
                    console.log('No token available, skipping authorization header');
                }
            },
            success: function (resultObject) {
                console.log('SUCCESS - loadFieldVisibility response:', JSON.stringify(resultObject));

                var output = '';
                if (resultObject.Content && resultObject.Content.length > 0) {
                    $.each(resultObject.Content, function (index, value) {
                        output += '<tr>' +
                            '<td>' + getFieldDisplayName(value.FieldName) + '</td>' +
                            '<td><span class="badge ' + (value.IsVisible ? 'badge-success' : 'badge-danger') + '">' + (value.IsVisible ? 'Evet' : 'Hayır') + '</span></td>' +
                            '<td><span class="badge ' + (value.IsRequired ? 'badge-warning' : 'badge-secondary') + '">' + (value.IsRequired ? 'Evet' : 'Hayır') + '</span></td>' +
                            '<td>' + (value.DisplayOrder || 1) + '</td>' +
                            '<td>' +
                            '<button class="btn btn-sm btn-primary" onclick="editFieldVisibility(' + value.Id + ')">Düzenle</button> ' +
                            '<button class="btn btn-sm btn-danger" onclick="deleteFieldVisibility(' + value.Id + ')">Sil</button>' +
                            '</td>' +
                            '</tr>';
                    });
                } else {
                    output = '<tr><td colspan="5">Bu müşteri için henüz alan görünürlük ayarı bulunmamaktadır.</td></tr>';
                }
                
                var tbody = $('#fieldVisibilityTable tbody');
                tbody.empty();
                tbody.append(output);
            },
            error: function (err) {
                console.log('ERROR - loadFieldVisibility failed:', JSON.stringify(err, null, 3));
                console.log('Error details:', err.status, err.statusText, err.responseText);
            }
        });
    }

    function saveFieldVisibility() {
        var customerId = $('#customerSelect').val();
        var fieldName = $('#txtFieldName').val();
        var isVisible = $('#chkIsVisible').is(':checked');
        var isRequired = $('#chkIsRequired').is(':checked');
        var displayOrder = parseInt($('#txtDisplayOrder').val()) || 1;

        if (!customerId) return showError('Lütfen bir müşteri seçin.');
        if (!fieldName)  return showError('Lütfen bir alan seçin.');

        var token;
        try { token = getTokenOrFail(); } catch { return; }

        var member = JSON.parse(sessionStorage.getItem('entMember')) || {};
        var data = {
            CustomerId: parseInt(customerId),
            FieldName: fieldName,
            IsVisible: isVisible,
            IsRequired: isRequired,
            DisplayOrder: displayOrder,
            CreatedBy: member.NameSurname || 'currentUser',
            MemberId: member.Id,
            MemberTitleId: 1,
            UserName: 'currentUser'
        };

        var url = baseUrl() + "CustomerFormFieldVisibility/Insert";

        $.ajax({
            type: 'POST',
            url: url,
            data: $.param(data),
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            crossDomain: true,
            timeout: 600000,
            headers: { 'Authorization': 'Bearer ' + token },
            success: function (response) {
                if (response && response.Result === 'OK') {
                    showSuccess('Alan görünürlük ayarı başarıyla kaydedildi.');
                    $('#fieldModal').modal('hide');
                    loadFieldVisibility(customerId);
                } else {
                    showError('Kaydetme hatası: ' + (response && response.Reason));
                }
            },
            error: ajaxErr('api/CustomerFormFieldVisibility/Insert')
        });
    }

    function updateFieldVisibility() {
        var customerId = $('#customerSelect').val();
        var fieldId = $('#hiddenFieldId').val();
        var fieldName = $('#txtFieldName').val();
        var isVisible = $('#chkIsVisible').is(':checked');
        var isRequired = $('#chkIsRequired').is(':checked');
        var displayOrder = parseInt($('#txtDisplayOrder').val()) || 1;

        if (!customerId) return showError('Lütfen bir müşteri seçin.');
        if (!fieldId) return showError('Alan ID bulunamadı.');
        if (!fieldName)  return showError('Lütfen bir alan seçin.');

        var token;
        try { token = getTokenOrFail(); } catch { return; }

        var member = JSON.parse(sessionStorage.getItem('entMember')) || {};
        var data = {
            Id: parseInt(fieldId),
            CustomerId: parseInt(customerId),
            FieldName: fieldName,
            IsVisible: isVisible,
            IsRequired: isRequired,
            DisplayOrder: displayOrder,
            ModifiedBy: member.NameSurname || 'currentUser',
            IsActive: true,
            MemberId: member.Id,
            MemberTitleId: 1,
            UserName: 'currentUser'
        };

        var url = baseUrl() + "CustomerFormFieldVisibility/Update";

        $.ajax({
            type: 'POST',
            url: url,
            data: $.param(data),
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            crossDomain: true,
            timeout: 600000,
            headers: { 'Authorization': 'Bearer ' + token },
            success: function (response) {
                if (response && response.Result === 'OK') {
                    showSuccess('Alan görünürlük ayarı başarıyla güncellendi.');
                    $('#fieldModal').modal('hide');
                    loadFieldVisibility(customerId);
                } else {
                    showError('Güncelleme hatası: ' + (response && response.Reason));
                }
            },
            error: ajaxErr('api/CustomerFormFieldVisibility/Update')
        });
    }

    function clearModal() {
        $('#hiddenFieldId').val('');
        $('#txtFieldName').val('');
        $('#chkIsVisible').prop('checked', true);
        $('#chkIsRequired').prop('checked', false);
        $('#txtDisplayOrder').val('1');
    }

    function saveAllFieldVisibility() {
        var customerId = $('#customerSelect').val();
        if (!customerId) return showError('Lütfen bir müşteri seçin.');

        var token;
        try { token = getTokenOrFail(); } catch { return; }

        var member = JSON.parse(sessionStorage.getItem('entMember')) || {};
        var fields = getDefaultFields();
        var savedCount = 0;
        var totalCount = fields.length;

        fields.forEach(function (field) {
            var fieldData = {
                CustomerId: parseInt(customerId),
                FieldName: field.FieldName,
                IsVisible: field.IsVisible,
                IsRequired: field.IsRequired,
                DisplayOrder: field.DisplayOrder,
                CreatedBy: member.NameSurname || 'currentUser',
                MemberId: member.Id,
                MemberTitleId: 1,
                UserName: 'currentUser'
            };

            var url = baseUrl() + "CustomerFormFieldVisibility/Insert";

            $.ajax({
                type: 'POST',
                url: url,
                data: $.param(fieldData),
                contentType: "application/x-www-form-urlencoded; charset=UTF-8",
                crossDomain: true,
                timeout: 600000,
                headers: { 'Authorization': 'Bearer ' + token },
                success: function (response) {
                    savedCount++;
                    if (savedCount === totalCount) {
                        showSuccess('Tüm alan görünürlük ayarları başarıyla kaydedildi.');
                        loadFieldVisibility(customerId);
                    }
                },
                error: ajaxErr('api/CustomerFormFieldVisibility/Insert')
            });
        });
    }

    function deleteFieldVisibility(id) {
        if (typeof Swal !== 'undefined') {
            Swal.fire({
                title: 'Emin misiniz?',
                text: 'Bu alan görünürlük ayarını silmek istediğinizden emin misiniz?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Evet, Sil!',
                cancelButtonText: 'İptal'
            }).then(function (result) {
                if (result.isConfirmed) {
                    performDelete(id);
                }
            });
        } else {
            if (confirm('Bu alan görünürlük ayarını silmek istediğinizden emin misiniz?')) {
                performDelete(id);
            }
        }
    }

    function performDelete(id) {
        var token;
        try { token = getTokenOrFail(); } catch { return; }

        var url = baseUrl() + "CustomerFormFieldVisibility/Delete";
        var data = { Id: id };

        $.ajax({
            type: 'POST',
            url: url,
            data: $.param(data),
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            crossDomain: true,
            timeout: 600000,
            headers: { 'Authorization': 'Bearer ' + token },
            success: function (response) {
                if (response && response.Result === 'OK') {
                    showSuccess('Alan görünürlük ayarı başarıyla silindi.');
                    var customerId = $('#customerSelect').val();
                    if (customerId) {
                        loadFieldVisibility(customerId);
                    }
                } else {
                    showError('Silme hatası: ' + (response && response.Reason));
                }
            },
            error: ajaxErr('api/CustomerFormFieldVisibility/Delete')
        });
    }

    function deleteAllFieldVisibility() {
        var customerId = $('#customerSelect').val();
        if (!customerId) return showError('Lütfen bir müşteri seçin.');

        if (typeof Swal !== 'undefined') {
            Swal.fire({
                title: 'Emin misiniz?',
                text: 'Bu müşterinin tüm alan görünürlük ayarlarını silmek istediğinizden emin misiniz?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Evet, Sil!',
                cancelButtonText: 'İptal'
            }).then(function (result) {
                if (result.isConfirmed) {
                    performDeleteAll(customerId);
                }
            });
        } else {
            if (confirm('Bu müşterinin tüm alan görünürlük ayarlarını silmek istediğinizden emin misiniz?')) {
                performDeleteAll(customerId);
            }
        }
    }

    function performDeleteAll(customerId) {
        var token;
        try { token = getTokenOrFail(); } catch { return; }

        var url = baseUrl() + "CustomerFormFieldVisibility/DeleteByCustomerId";
        var data = { CustomerId: parseInt(customerId) };

        $.ajax({
            type: 'POST',
            url: url,
            data: $.param(data),
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            crossDomain: true,
            timeout: 600000,
            headers: { 'Authorization': 'Bearer ' + token },
            success: function (response) {
                if (response && response.Result === 'OK') {
                    showSuccess('Tüm alan görünürlük ayarları başarıyla silindi.');
                    loadFieldVisibility(customerId);
                } else {
                    showError('Silme hatası: ' + (response && response.Reason));
                }
            },
            error: ajaxErr('api/CustomerFormFieldVisibility/DeleteByCustomerId')
        });
    }

    //#endregion

    //#region Utility Functions (display names, defaults, alerts)

    function getFieldDisplayName(fieldName) {
        var displayNames = {
            'CustomerName': 'Müşteri Adı',
            'CustomerAddress': 'Müşteri Adresi',
            'CustomerPhone': 'Müşteri Telefonu',
            'CustomerEmail': 'Müşteri E-posta',
            'ContactPerson': 'İletişim Kişisi',
            'TaxNumber': 'Vergi Numarası',
            'TaxOffice': 'Vergi Dairesi',
            'PaymentTerms': 'Ödeme Koşulları',
            'CreditLimit': 'Kredi Limiti',
            'Notes': 'Notlar',
            'FaceCount': 'Yüz Sayısı',
            'ShelfIndex': 'Raf İndeksi',
            'ShelfStock': 'Raf Stoku',
            'Price': 'Fiyat',
            'WarehouseStock': 'Depo Stoku',
            'ActivityType': 'Aktivite Türü',
            'DiscountPrice': 'İndirimli Fiyat'
        };
        return displayNames[fieldName] || fieldName;
    }

    function getFieldNameFromDisplayName(displayName) {
        var displayNames = {
            'Müşteri Adı': 'CustomerName',
            'Müşteri Adresi': 'CustomerAddress',
            'Müşteri Telefonu': 'CustomerPhone',
            'Müşteri E-posta': 'CustomerEmail',
            'İletişim Kişisi': 'ContactPerson',
            'Vergi Numarası': 'TaxNumber',
            'Vergi Dairesi': 'TaxOffice',
            'Ödeme Koşulları': 'PaymentTerms',
            'Kredi Limiti': 'CreditLimit',
            'Notlar': 'Notes',
            'Yüz Sayısı': 'FaceCount',
            'Raf İndeksi': 'ShelfIndex',
            'Raf Stoku': 'ShelfStock',
            'Fiyat': 'Price',
            'Depo Stoku': 'WarehouseStock',
            'Aktivite Türü': 'ActivityType',
            'İndirimli Fiyat': 'DiscountPrice'
        };
        return displayNames[displayName] || displayName;
    }

    function getDefaultFields() {
        return [
            { FieldName: 'FaceCount', IsVisible: true, IsRequired: true, DisplayOrder: 1 },
            { FieldName: 'ShelfIndex', IsVisible: true, IsRequired: false, DisplayOrder: 2 },
            { FieldName: 'ShelfStock', IsVisible: true, IsRequired: false, DisplayOrder: 3 },
            { FieldName: 'Price', IsVisible: true, IsRequired: true, DisplayOrder: 4 },
            { FieldName: 'WarehouseStock', IsVisible: true, IsRequired: false, DisplayOrder: 5 },
            { FieldName: 'ActivityType', IsVisible: true, IsRequired: false, DisplayOrder: 6 },
            { FieldName: 'DiscountPrice', IsVisible: true, IsRequired: false, DisplayOrder: 7 }
        ];
    }

    function showSuccess(message) {
        if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'success',
                title: 'Başarılı!',
                text: message,
                timer: 2000,
                showConfirmButton: false
            });
        } else {
            alert('Başarılı: ' + message);
        }
    }

    function showError(message) {
        if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'error',
                title: 'Hata!',
                text: message
            });
        } else {
            alert('Hata: ' + message);
        }
    }

    function showWarning(message) {
        if (typeof Swal !== 'undefined') {
            Swal.fire({
                icon: 'warning',
                title: 'Uyarı!',
                text: message
            });
        } else {
            alert('Uyarı: ' + message);
        }
    }

    //#endregion

    //#region Global Functions (for HTML onclick events)

    window.editFieldVisibility = function (id) {
        console.log('Edit field visibility:', id);
        
        // Find the field data from the table
        var fieldData = null;
        $('#fieldVisibilityTable tbody tr').each(function() {
            var editBtn = $(this).find('button[onclick*="editFieldVisibility(' + id + ')"]');
            if (editBtn.length > 0) {
                var row = $(this);
                fieldData = {
                    Id: id,
                    FieldName: row.find('td:first').text(),
                    IsVisible: row.find('.badge-success').length > 0,
                    IsRequired: row.find('.badge-warning').length > 0,
                    DisplayOrder: parseInt(row.find('td:nth-child(4)').text()) || 1
                };
                return false; // break the loop
            }
        });

        if (fieldData) {
            // Populate modal with existing data
            $('#hiddenFieldId').val(fieldData.Id);
            $('#txtFieldName').val(getFieldNameFromDisplayName(fieldData.FieldName));
            $('#chkIsVisible').prop('checked', fieldData.IsVisible);
            $('#chkIsRequired').prop('checked', fieldData.IsRequired);
            $('#txtDisplayOrder').val(fieldData.DisplayOrder);
            $('#modalTitle').text('Alan Görünürlük Ayarı Düzenle');
            $('#fieldModal').modal('show');
        } else {
            showError('Alan verisi bulunamadı.');
        }
    };

    window.deleteFieldVisibility = function (id) {
        deleteFieldVisibility(id);
    };

    //#endregion

})();
