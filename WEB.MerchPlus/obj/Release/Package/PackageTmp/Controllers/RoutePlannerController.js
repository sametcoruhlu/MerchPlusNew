(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Customer Product Retail Shops Controller module. Let's get dirty...");

        LoadScheduler();
        LoadMembers();
        SetFrequencyCombobox();
        SetOrderIndexCombobox();
        LoadRetailShop();
        SetAddVisitButton();
        SetDeleteButtons();
    });

    function SetDeleteButtons() {
        $('#winDeleteVisit_btnDeleteVisit').click(function (e) {
            e.preventDefault();
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/MemberRouteController/DeleteMemberRouteWiselyFromCalendar";
            var data = {};
            data.DeleteAll = 0;
            data.MemberRouteId = $('#winDeleteVisit_hdnId').val();
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    SelectMemberRouteByMemberIdSchedulerData();
                },
                error: function (err) {
                    console.log('An error occured in api/MemberRouteController/DeleteMemberRouteWiselyFromCalendar: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        });
        $('#winDeleteVisit_btnDeleteVisitAll').click(function (e) {
            e.preventDefault();
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/MemberRouteController/DeleteMemberRouteWiselyFromCalendar";
            var data = {};
            data.DeleteAll = 1;
            data.MemberRouteId = $('#winDeleteVisit_hdnId').val();
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    SelectMemberRouteByMemberIdSchedulerData();
                },
                error: function (err) {
                    console.log('An error occured in api/MemberRouteController/DeleteMemberRouteWiselyFromCalendar: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        });
    }

    function SetAddVisitButton() {
        $('#winAddVisit_btnAddVisit').click(function (e) {
            e.preventDefault();
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/MemberRouteController/InsertMemberRouteWiselyFromCalendar";
            var data = {};
            data.OrderIndex = $('#winAddVisit_wleOrderIndex').val();
            data.MemberId = $('#wleMemberId').val();
            data.EffectiveDate = $('#winAddVisit_hdnDate').val();
            data.RetailShopId = $('#winAddVisit_wleRetailShopId').val();
            data.FrequencyId = $('#winAddVisit_wleFrequencyId').val();
            data.CustomerId = customer.Id;
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    SelectMemberRouteByMemberIdSchedulerData();
                },
                error: function (err) {
                    console.log('An error occured in api/MemberRouteController/InsertMemberRouteWiselyFromCalendar: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        });
    }

    function LoadRetailShop() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/RetailShopController/SelectRetailShopMapView";
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

                var dataMapped_Retail = $.map(resultObject.Content, function (obj) {
                    obj.id = obj.id || obj.Id; // replace pk with your identifier
                    obj.text = obj.text || obj.Name;
                    return obj;
                });

                $('#winAddVisit_wleRetailShopId').html('<option></option>');
                $('#winAddVisit_wleRetailShopId').select2({
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

            },
            error: function (err) {
                console.log('An error occured in api/RetailCategoryController/SelectRetailShopCategoryGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function SetOrderIndexCombobox() {
        $('#winAddVisit_wleOrderIndex').select2({
            width: '100%',
            theme: 'bootstrap',
            placeholder: 'Seçiniz...',
            allowClear: false,
            dropdownParent: $('#winAddVisit')
        });
    }

    function SetFrequencyCombobox() {
        $('#winAddVisit_wleFrequencyId').select2({
            width: '100%',
            theme: 'bootstrap',
            placeholder: 'Seçiniz...',
            allowClear: false,
            dropdownParent: $('#winAddVisit')
        });
    }

    function LoadMembers() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberController/SelectMemberByMemberTitleIdCustomerId";
        var data = {};
        data.MemberTitleId = 5; //Merch
        data.CustomerId = customer.Id;
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
                    output += '<option value="' + value.Id + '">' + value.NameSurname + '</option>';
                });
                $('#wleMemberId').html(output);
                $('#wleMemberId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Merch Seçiniz...',
                    allowClear: true
                });
                $('#wleMemberId').change(function () {
                    SelectMemberRouteByMemberIdSchedulerData();
                });
            },
            error: function (err) {
                console.log('An error occured in api/MemberController/SelectMemberByMemberTitleIdCustomerId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function SelectMemberRouteByMemberIdSchedulerData() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteController/SelectMemberRouteByMemberIdSchedulerData";
        var data = {};
        data.MemberId = $('#wleMemberId').val();
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));

                try {
                    $('#gridContainer').fullCalendar('destroy');
                } catch (e) {

                }

                var scheduler = $('#gridContainer').fullCalendar({
                    firstDay: 1,
                    defaultView: 'agendaWeek',
                    height: 400,
                    allDaySlot: false,
                    monthNames: ['Ocak', 'Şubat', 'Mart', 'Nisan', 'Mayıs', 'Haziran', 'Temmuz', 'Ağustos', 'Eylül', 'Ekim', 'Kasım', 'Aralık'],
                    monthNamesShort: ['Oca', 'Şub', 'Mar', 'Nis', 'May', 'Haz', 'Tem', 'Ağu', 'Eyl', 'Eki', 'Kas', 'Ara'],
                    dayNames: ['Pazar', 'Pazartesi', 'Salı', 'Çarşamba', 'Perşembe', 'Cuma', 'Cumartesi'],
                    dayNamesShort: ['Paz', 'Pzt', 'Sal', 'Çar', 'Per', 'Cum', 'Cts'],
                    timeFormat: 'H(:mm)',
                    minTime: '07:00:00',
                    maxTime: '21:00:00',
                    titleFormat: 'DD.MM.YYYY',
                    columnFormat: 'DD.MM ddd',
                    slotDuration: '00:30:00',
                    slotLabelFormat: 'H:mm',
                    displayEventTime: false,
                    displayEventEnd: false,
                    editable: true,
                    header: {
                        left: 'prev,next today',
                        center: 'title',    
                        right: 'agendaWeek, agendaDay'
                    },
                    businessHours: {
                        dow: [1, 2, 3, 4, 5, 6],
                        start: '08:00',
                        end: '19:00',
                    },
                    eventDrop: function(event, delta, revertFunc) {
                        console.log(event.id + " was dropped on " + event.start.format());
                        DragAndDropHelper(event.memberRouteId, moment(event.start).format('YYYY-MM-DD HH:mm:ss'));
                    },
                    events: resultObject.Content,
                    //eventClick: function (calEvent, jsEvent, view) {
                    //    console.log(calEvent);
                    //    console.log(calEvent.start);
                    //    //alert('Event: ' + calEvent.title);
                    //    //alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
                    //    //alert('View: ' + view.name);

                    //},
                    eventRender: function (event, element) {
                        if (event.selectable)
                            if(event.id != -1)
                                element.html('<table border="0" style="border: none !important; border-width: 0px !important;">' +
                                             '    <tr>' +
                                             '     <td style="width: 70%; border-width: 0px !important; vertical-align: middle;">' + event.title + '</td>' +
                                             '     <td style="border-width: 0px !important; vertical-align: middle;">' +
                                             '        <button type="button" class="btnDeleteVisit pull-right btn btn-danger btn-xs" bsm-id="' + event.memberRouteId + '"><i class="fa fa-trash"></i></button>' +
                                             '     </td>' +
                                             '    </tr>' +
                                             '</table>');
                            else
                                element.html('<button type="button" class="btnAddVisit btn btn-default" bsm-date="' + moment(event.start).format('YYYY-MM-DD') + '" bsm-memberid="' + event.MemberId + '" style="width: 100%; height: 100%; color: #000;"><i class="fa fa-plus"></i> Ziyaret Ekle</button>');
                        else
                            element.html(event.title);
                    },
                    eventAfterAllRender: function (view) {
                        $('.btnAddVisit').off('click');
                        $('.btnAddVisit').click(function (e) {
                            e.preventDefault();
                            console.log($(this).attr('bsm-date') + ', ' + $(this).attr('bsm-memberid'));
                            $('#winAddVisit_hdnDate').val($(this).attr('bsm-date'));
                            $('#winAddVisit_lblMemberId').val($(this).attr('bsm-memberid'));
                            $('#winAddVisit_lblEndDate').html(moment(new Date().setFullYear(new Date().getFullYear() + 1)).format('DD.MM.YYYY'));

                            var eventCount = 0;
                            $('#gridContainer').fullCalendar('clientEvents', function (eventObj) {
                                if (moment(eventObj.start._i).format('YYYY-MM-DD') == $('#winAddVisit_hdnDate').val() && eventObj.id != -1) {
                                    eventCount += 1;
                                } else {
                                    eventCount += 0;
                                }
                            });
                            console.log(eventCount);
                            $('#winAddVisit_wleOrderIndex').val(eventCount + 1).trigger('change');

                            $('#winAddVisit').modal('show');
                        });

                        $('.btnDeleteVisit').off('click');
                        $('.btnDeleteVisit').click(function (e) {
                            e.preventDefault();
                            var idToDelete = $(this).attr('bsm-id');
                            $('#winDeleteVisit_hdnId').val(idToDelete);
                            $('#winDeleteVisit').modal('show');
                        });
                    }
                });


                //defaultDate: GetCurrentDateSql(),

                

            },
            error: function (err) {
                console.log('An error occured in api/MemberRouteController/SelectMemberRouteByMemberIdSchedulerData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });
    }

    function LoadScheduler() {
        
    }

    function DragAndDropHelper(memberRouteId, movedToDate) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteController/DragAndDropHelper";
        var data = {};
        data.Id = memberRouteId;
        data.EffectiveDate = movedToDate;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                toastr.options = {
                    closeButton: true,
                    progressBar: true,
                    showMethod: 'slideDown',
                    timeOut: 3000
                };
                SelectMemberRouteByMemberIdSchedulerData();
                toastr.success('İşlem Tamamlandı');
            },
            error: function (err) {
                console.log('An error occured in api/MemberRouteController/DragAndDropHelper: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

}());