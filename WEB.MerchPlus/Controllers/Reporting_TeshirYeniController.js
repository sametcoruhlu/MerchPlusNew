var listOfMemberRoute = [];
var submitButton;
var downloadButton;
var downloadAllButton;
(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Teshir Yeni Controller module. Let's get dirty...");
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
            DownloadPhotosAsZipClientSide();
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

    function ExportImagesFromServer() {
        if (listOfMemberRoute.length === 0) {
            swal('Hoppa', 'Dışa aktarılacak hiç resim bulunamadı!', 'error');
            return;
        }

        swal({
            title: 'Resimler ZIP Olarak İndirilecek',
            text: 'Seçilen tarih aralığında bulunan ' + listOfMemberRoute.length +
                ' ziyaretin tüm resimleri ZIP olarak indirilecek. Bu işlem biraz zaman alabilir. Lütfen bekleyin!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Devam Et',
            cancelButtonText: 'Vazgeç',
        }).then(function () {
            var token = JSON.parse(sessionStorage.getItem('token'));
            var url = InfrastructureVariables.WebServicePath2 + "MemberRoutePhotoController/DownloadMemberRouteDisplayPhotoByMemberRouteList";

            var data = {
                MemberRouteList: listOfMemberRoute
            };

            $.ajax({
                type: 'POST',
                url: url,
                data: $.param(data), // JSON yerine x-www-form-urlencoded kullan
                contentType: "application/x-www-form-urlencoded",
                timeout: 600000, // Timeout süresi artırıldı (10 dakika)
                beforeSend: function (xhr) {
                    downloadButton.ladda('start');
                },
                success: function (resultObject) {
                    downloadButton.ladda('stop');
                    console.log(JSON.stringify(resultObject, null, 3));

                    if (resultObject.Content) {
                        var zipFileUrl = 'http://merchplus.net/Images/ShopPhotos/TempExport/' + resultObject.Content;

                        // ZIP dosyasını otomatik indirmek için
                        var a = document.createElement("a");
                        a.href = zipFileUrl;
                        a.download = "exported_images.zip"; // ZIP dosyasının adı
                        document.body.appendChild(a);
                        a.click();
                        document.body.removeChild(a);
                    } else {
                        swal('Hata', 'ZIP dosyası oluşturulamadı.', 'error');
                    }
                },
                error: function (err) {
                    downloadButton.ladda('stop');
                    console.log('An error occurred: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });
        });
    }
    function ExportImagesFromServerAll() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRoutePhotoController/DownloadMemberRoutePhotoByCustomerIdStartDateEndDate";
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
                downloadAllButton.ladda('start');
            },
            success: function (resultObject) {
                downloadAllButton.ladda('stop');
                console.log(JSON.stringify(resultObject, null, 3));
                var redirectWindow = window.open('http://merchplus.net/Images/ShopPhotos/TempExport/' + resultObject.Content, '_blank');
                redirectWindow.location;
            },
            error: function (err) {
                downloadAllButton.ladda('stop');
                console.log('An error occured in api/MemberRoutePhotoController/DownloadMemberRoutePhotoByCustomerIdStartDateEndDate: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

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
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var url = InfrastructureVariables.WebServicePath2 + "ReportingController/TeshirYeniRaporu";
        var data = {
            CustomerId: currentCustomer.Id,
            StartDate: ConvertDateToSqlDate($('#wleStartDate').datepicker('getDate')),
            EndDate: ConvertDateToSqlDate($('#wleEndDate').datepicker('getDate'))
        };

        $.ajax({
            type: 'POST',
            url: url,
            data: $.param(data), // JSON yerine x-www-form-urlencoded kullan
            contentType: "application/x-www-form-urlencoded",
            success: function (resultObject) {
                $('#btnExportPhoto').removeClass('hidden');
                $('#btnExportPhotoAll').removeClass('hidden');
                console.log(JSON.stringify(resultObject, null, 3));

                listOfMemberRoute = [];

                $.each(resultObject.Content, function (index, value) {

                    listOfMemberRoute.push({
                        MemberRouteId: value.MemberRouteId,          // Üye rotası ID'si
                        MerchNameSurname: value.MerchNameSurname,    // Merch (Saha Çalışanı) Adı
                        RetailShopName: value.RetailShopName,        // Mağaza Adı
                        DisplayPicturePath: value.DisplayPicturePath, // Fotoğraf Yolu
                        DateOfVisit: value.DateOfVisit
                    });

                });

                $("#gridContainer").dxDataGrid({
                    height: '400px',
                    allowColumnReordering: true,
                    allowColumnResizing: true,
                    columnAutoWidth: true,
                    columnChooser: { enabled: true },
                    columnFixing: { enabled: true },
                    dataSource: resultObject.Content,
                    filterRow: { visible: true, applyFilter: "auto" },
                    paging: { pageSize: 100 },
                    scrolling: { mode: "virtual" },
                    searchPanel: { visible: true, width: '250px', placeholder: "Search" },
                    headerFilter: { visible: true },
                    selection: { mode: "single" },
                    "export": { enabled: true, fileName: "Teşhir Raporu", allowExportSelectedData: false },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        { dataField: "MerchNameSurname", caption: "Merch" },
                        { dataField: "DateOfVisit", caption: "Tarih", dataType: 'date', format: 'dd.MM.yyyy' },
                        { dataField: "Day", caption: "Gün" },
                        { dataField: "RetailName", caption: "Müşteri" },
                        { dataField: "RetailShopName", caption: "Mağaza" },
                        { dataField: "PromotionTypeName", caption: "Teşhir Adı" },
                        { dataField: "PromotionCount", caption: "Teşhir Adedi" },
                        { dataField: "CustomerProductName", caption: "Ürün Adı" },
                        { dataField: "CustomerProductCount", caption: "Ürün Adeti" },
                        {
                            caption: 'Fotoğraf',
                            width: '100px',
                            alignment: "center",
                            cellTemplate: function (container, options) {
                                if (options.data.DisplayPicturePath) {
                                    var imageUrl = "http://merchplus.net/" + options.data.DisplayPicturePath;
                                    $('<button type="button" class="btn btn-primary btn-xs btnShowPhoto" data-img="' + imageUrl + '">')
                                        .html('<i class="fa fa-camera"></i> Foto')
                                        .appendTo(container);
                                } else {
                                    $('<span style="color: gray;">Fotoğraf Yok</span>').appendTo(container);
                                }
                            },
                            cssClass: 'alignMiddle'
                        }
                    ],
                    onContentReady: function (e) {
                        $(".btnShowPhoto").off("click").on("click", function () {
                            var imageUrl = $(this).attr("data-img");
                            ShowImagePopup(imageUrl);
                        });
                    },
                    summary: {
                        totalItems: [{
                            column: "PromotionCount",
                            summaryType: "sum",
                            valueFormat: "number",
                            customizeText: function (data) {
                                return "Σ: " + numeral(data.value).format();
                            }
                        }]
                    }
                }).dxDataGrid('instance');
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/TeshirRaporu: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    async function DownloadPhotosAsZipClientSide() {
        if (listOfMemberRoute.length === 0) {
            swal('Hata', 'İndirilecek fotoğraf bulunamadı!', 'error');
            return;
        }

        swal({
            title: 'Fotoğraflar ZIP Olarak İndirilecek',
            text: 'Seçilen tarih aralığında bulunan ' + listOfMemberRoute.length +
                ' ziyaretin tüm fotoğrafları ZIP olarak indirilecek. Lütfen bekleyin!',
            type: 'info',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'İndir',
            cancelButtonText: 'İptal'
        }).then(async function () {
            const zip = new JSZip();
            let fileCount = 0;

            // 📥 Tüm fotoğrafları indir ve işlem yap
            for (const route of listOfMemberRoute) {
                if (route.DisplayPicturePath) {
                    const imageUrl = "http://merchplus.net/" + route.DisplayPicturePath;
                    try {
                        const response = await fetch(imageUrl);
                        if (!response.ok) throw new Error('Resim indirilemedi: ' + imageUrl);

                        const blob = await response.blob();
                        const image = await createImageWithText(blob, route.RetailShopName, route.MerchNameSurname, route.DateOfVisit);

                        const fileName = `${route.MerchNameSurname}_${fileCount}_${route.RetailShopName}.jpg`;
                        zip.file(fileName, image);
                        fileCount++;

                    } catch (error) {
                        console.error("Resim indirilemedi: ", imageUrl, error);
                    }
                }
            }

            // 📦 ZIP dosyasını oluştur ve indir
            if (fileCount > 0) {
                zip.generateAsync({ type: "blob" }).then(function (content) {
                    const zipFileName = `FotografAlbumu_${new Date().toISOString().replace(/[-:T]/g, "").slice(0, 14)}.zip`;

                    const a = document.createElement("a");
                    a.href = URL.createObjectURL(content);
                    a.download = zipFileName;
                    document.body.appendChild(a);
                    a.click();
                    document.body.removeChild(a);
                });
            } else {
                swal('Hata', 'ZIP dosyasına eklenebilecek fotoğraf bulunamadı.', 'error');
            }
        });
    }

    async function createImageWithText(blob, retailShopName, merchNameSurname, dateOfVisit) {
        return new Promise((resolve, reject) => {
            const img = new Image();
            img.src = URL.createObjectURL(blob);
            img.onload = function () {
                const canvas = document.createElement("canvas");
                const ctx = canvas.getContext("2d");

                // 🖼 Resmin boyutlarını belirle
                canvas.width = img.width;
                canvas.height = img.height + 40; // Altta yazı için ekstra alan ekleyelim

                // 📷 Resmi çiz
                ctx.drawImage(img, 0, 0);

                // 🖌 Arka plan kutusu ekleyelim
                ctx.fillStyle = "rgba(0, 0, 0, 0.7)"; // Yarı saydam siyah
                ctx.fillRect(0, img.height, img.width, 40);

                // 📝 Yazıyı ekleyelim
                ctx.font = "20px Arial";
                ctx.fillStyle = "white";
                ctx.fillText(`${retailShopName} | ${merchNameSurname} | ${dateOfVisit}`, 20, img.height + 28);

                // 🎨 Yeni resmi blob olarak döndür
                canvas.toBlob((newBlob) => resolve(newBlob), "image/jpeg");
            };

            img.onerror = reject;
        });
    }
    function ShowImagePopup(imageUrl) {
        if (!imageUrl) {
            swal("Hata", "Fotoğraf mevcut değil!", "error");
            return;
        }

        var modalHtml = `
        <div class="modal fade" id="photoModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-fullscreen" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Fotoğraf Önizleme</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body text-center d-flex align-items-center justify-content-center" style="height: 100vh; padding: 0;">
                        <img src="${imageUrl}" class="img-fluid img-thumbnail" style="max-width: 100%; max-height: 100vh; object-fit: contain;" />
                    </div>
                </div>
            </div>
        </div>`;

        // Önceki modal varsa kaldır
        $("#photoModal").remove();

        // Yeni modal'ı body'ye ekle ve göster
        $("body").append(modalHtml);
        $("#photoModal").modal("show");
    }




    function ShowPhotoByMemberRouteId(displayPicturePath) {

        //var token = JSON.parse(sessionStorage.getItem('token'));
        //var member = JSON.parse(sessionStorage.getItem('entMember'));
        //var eventInstance = event;
        //var url = InfrastructureVariables.WebServicePath + "api/ReportingController/SelectMemberRoutePhotoByMemberRouteId";
        //var data = {};
        //data.MemberRouteId = memberRouteId;
        $('#lstPhoto').html('Lütfen bekleyin...');
        //$.ajax({
        //    type: 'POST',
        //    url: url,
        //    data: data,
        //    beforeSend: function (xhr) {
        //        xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
        //    },
        //    success: function (resultObject) {
        //        console.log(JSON.stringify(resultObject, null, 3));
        var output = '';
        //$.each(resultObject.Content, function (index, value) {
        output += '<div style="float: left; padding-left: 20px;"><a class="example-image-link" href="http://mp.bsmyazilim.com/' + displayPicturePath + '" data-lightbox="example-set" data-title="deneme"><img class="img-responsive img-circle" style="height: 100px;" src="http://mp.bsmyazilim.com/' + displayPicturePath + '" alt=""/></a></div>';
        //});

        if (output == '')
            output += '<h3 style="color:red;">Saha personeli tarafından hiç fotoğraf çekilmemiş.</h3>';

        $('#lstPhoto').html(output);

        $('#pnlPhoto').modal('show');

        //    },
        //    error: function (err) {
        //        console.log('An error occured in api/ReportingController/SelectMemberRoutePhotoByMemberRouteId: ' + JSON.stringify(err, null, 3));
        //        swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
        //    }
        //});

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

        downloadButton = $('#btnExportPhoto').ladda();
        downloadButton.click(function (e) {
            e.preventDefault();
            ExportImages();
        });

        downloadAllButton = $('#btnExportPhotoAll').ladda();
        downloadAllButton.click(function (e) {
            e.preventDefault();
            ExportImagesAll();
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