(function () {

    //#region View Load

    $(function () {

        //#region Console writeout

        console.log("Hello, IIFE loaded for CustomerSelect module. Let's get dirty...");

        SetNameSurnameTitleProfilePhoto();
        SelectCustomerByMemberId();

        //#endregion

        //#region Event hookup

        //$('#btnLogin').click(btnLogin_Click);

        //#endregion

        //#region UI View Code

        //  Place bsm_ajax preset on this line

        //#endregion

    });

    //#endregion

    //#region Events

    //var btnLogin_Click = function () {
    //  console.log('Clicked...');
    //};

    function AnimateContents() {
        setTimeout(function () {
            $('#pnlMainContainer').css('display', 'block');
            $('#pnlMainContainer').addClass('animated fadeInDown');
        }, 500);

        setTimeout(function () {
            $('#lblSlogan').addClass('animated flash');
        }, 3000);
    }

    function SetNameSurnameTitleProfilePhoto () {
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        $('#lblNameSurname').html(member.NameSurname);
        $('#lblTitle').html(member.Title);
        $('#pctProfilePhoto').attr('src', member.ProfilePicturePath);
    }

    function SelectCustomerByMemberId () {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberCustomerController/SelectMemberCustomerByMemberId";
        var data = {};
        data.MemberId = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                if (resultObject.Content.length == 0)
                {
                    swal('Hoppa...', 'Yetkiniz dahilinde hiçbir müşteri bulunmuyor. Sistem yöneticinizle temasa geçin.', 'error').then(function () {
                        sessionStorage.clear();
                        window.location.href = 'index.html';
                    })
                    return;
                }
                var output = '';
                var count = 0;
                $.each(resultObject.Content, function (index, value) {
                    count += 1;
                    output += "<tr style='cursor: pointer;' bsm-data='" + JSON.stringify(value) + "'>" +
                              '  <td style="padding-top: 25px; width: 125px;">' +
                              '      <a href="#">' +
                              '          <img src="' + value.ProfilePicturePath + '" width="75" class="thumbnail" />' +
                              '      </a>' +
                              '  </td>' +
                              '  <td style="padding-top: 27px;">' +
                              '      <h1 style="font-weight:bold; color: #4C4C4C;">' + value.Name + '</h1>' +
                              '  </td>' +
                              '</tr>';
                });

                if (count == 1)
                {
                    sessionStorage.setItem('currentCustomer', JSON.stringify(resultObject.Content[0]));

                    setTimeout(function () {
                        window.location.href = 'Dashboard.html';
                        return;
                    }, 1000);
                }

                $('#pnlModuleContainer').html(output);

                AnimateContents();

                $('tr').click(function () {
                    var currentCustomer = JSON.parse($(this).attr('bsm-data'));
                    sessionStorage.setItem('currentCustomer', JSON.stringify(currentCustomer));
                    window.location.href = 'Dashboard.html';
                });

            },
            error: function (err) {
                console.log('An error occured in api/MemberCustomerController/SelectMemberCustomerByMemberId: ' + JSON.stringify(err, null, 3));
            }
        });
    };


    //#endregion

}());