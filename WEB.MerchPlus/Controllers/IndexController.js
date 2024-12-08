(function () {

    //#region View Load

    $(function () {

        //#region Console writeout

        console.log("Hello, IIFE loaded for IndexController module. Let's get dirty...");

        //#endregion

        //#region Event hookup

        $('#frmLogin').submit(frmLogin_onSubmit);

        //#endregion

        //#region UI View Code

        //  Place bsm_ajax preset on this line

        //#endregion

    });

    //#endregion

    //#region Events

    function frmLogin_onSubmit(event) {
        event.preventDefault();
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/Token";
        var data = {};
        data.grant_type = 'password';
        data.username = $('#txtUsername').val();
        data.password = $('#txtPassword').val();
              
        $.ajax({
            type: 'POST',
            url: url,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            contentType: 'application/x-www-form-urlencoded; charset=utf-8',
            data: data,
            success: function (resultObject) {
                console.log('api/Token Response\r\n' +
                            '------------------\r\n' +
                            JSON.stringify(resultObject, null, 3));
                
                sessionStorage.setItem('token', JSON.stringify(resultObject));
                SelectMemberByUsername();
            },
            error: function (err) {
                console.log('An error occured in api/Token: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', "Sizi tanıyamadık... Şifrenizi yanlış girmiş olabilir misiniz?", 'error');
                return;
            },
            complete: function () {
                console.log('Finished');
            }
        });
        
    };

    function SelectMemberTitle() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberTitleController/SelectMemberTitleById";
        var data = {};
        data.Id = member.MemberTitleId;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                sessionStorage.setItem('entMemberTitle', JSON.stringify(resultObject));
                window.location.href = 'CustomerSelect.html?v=1';
            },
            error: function (err) {
                console.log('An error occured in api/MemberTitleController/SelectMemberTitleById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error').then(function () {
                    //sessionStorage.clear();
                    //window.location.href = 'index.html';
                });
            }
        });

    }

    function SelectMemberByUsername() {
        var token = JSON.parse(sessionStorage.getItem('token'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/Account/MemberEntity";
        $.ajax({
            type: 'GET',
            url: url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                sessionStorage.setItem('entMember', JSON.stringify(resultObject));
                SelectMemberTitle();
            },
            error: function (err) {
                console.log('An error occured in api/AccountController/: ' + JSON.stringify(err, null, 3));
            }
        });
    };

    //var btnLogin_Click = function () {
    //  console.log('Clicked...');
    //};

    //#endregion

}());