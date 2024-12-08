String.prototype.turkishToUpper = function () {
    var string = this;
    var letters = { "i": "İ", "ş": "Ş", "ğ": "Ğ", "ü": "Ü", "ö": "Ö", "ç": "Ç", "ı": "I" };
    string = string.replace(/(([iışğüçö]))+/g, function (letter) { return letters[letter]; })
    return string.toUpperCase();
}

Pace.options = {
    ajax: true,
    document: true,
}

var localizationobj = {};
localizationobj.filterclearstring = 'Temizle';
localizationobj.filterstring = 'Filtrele';
localizationobj.filtershowrowstring = "Şu kayıtları göster:";
localizationobj.filterselectallstring = "(Tümü)";
localizationobj.sortascendingstring = "Küçükten Büyüğe";
localizationobj.sortdescendingstring = "Büyükten Küçüğe";
localizationobj.sortremovestring = "Sıralamayı Kaldır";
localizationobj.groupbystring = "Bu sütuna göre gurupla";
localizationobj.groupremovestring = "Guruplardan kaldır";
localizationobj.groupsheaderstring = "Guruplamak istediğiniz sütunu bu alana sürükleyiniz...";

var InfrastructureVariables = {};
InfrastructureVariables.WebServicePath = 'http://localhost/API.MerchPlus/';
//InfrastructureVariables.WebServicePath = "https://merchplusapi.azurewebsites.net/";
//InfrastructureVariables.WebServicePath = "http://localhost:54166/";
function GetCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = dd + '.' + mm + '.' + yyyy;
    return today;
}

function GetCurrentDateSql() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    return today;
}

function ConvertDateToSqlDate(incomingDate) {
    var today = incomingDate;
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;
    return today;
}

$(document).ready(function () {

    $('Spinner').load('Spinner.html');

    $(document).on({
        ajaxStart: function () { $('#winSpinner').css('display','block'); },
        ajaxStop: function () { $('#winSpinner').css('display', 'none'); }
    });

    $('TopMenu').load('TopMenu.html', null, function () {
        $('SideMenu').load('SideMenu.html', null, function () {
            

            // MetsiMenu
            $('#side-menu').metisMenu();

            // Close menu in canvas mode
            $('.close-canvas-menu').on('click', function () {
                $("body").toggleClass("mini-navbar");
                SmoothlyMenu();
            });

            // Run menu of canvas
            $('body.canvas-menu .sidebar-collapse').slimScroll({
                height: '100%',
                railOpacity: 0.2,
                wheelStep: 5
            });

            //Collpased menu persistence
            if (sessionStorage.getItem('menuCollapsed') == 1) {
                $("body").addClass('mini-navbar');
            } else {
                $("body").removeClass('mini-navbar');
            }

            $('.navbar-minimalize').on('click', function () {
                $("body").toggleClass("mini-navbar");
                if ($('body').hasClass('mini-navbar')) {
                    sessionStorage.setItem('menuCollapsed', 1);
                } else {
                    sessionStorage.setItem('menuCollapsed', 0);
                }
                SmoothlyMenu();
            });

            fix_height();

            // Fixed Sidebar
            $(window).bind("load", function () {
                if ($("body").hasClass('fixed-sidebar')) {
                    $('.sidebar-collapse').slimScroll({
                        height: '100%',
                        railOpacity: 0.2,
                        wheelStep: 5
                    });
                }
            });

            $(window).bind("load resize scroll", function () {
                if (!$("body").hasClass('body-small')) {
                    fix_height();
                }
            });

            $("[data-toggle=popover]")
                .popover();

            // Add slimscroll to element
            $('.full-height-scroll').slimscroll({
                height: '100%',
                railOpacity: 0.2,
                wheelStep: 5
            });

        });
    });
    

    // Add body-small class if window less than 768px
    if ($(this).width() < 769) {
        $('body').addClass('body-small')
    } else {
        $('body').removeClass('body-small')
    }

    
        

});

// Full height of sidebar
function fix_height() {
    var heightWithoutNavbar = $("body > #wrapper").height() - 61;
    $(".sidebard-panel").css("min-height", heightWithoutNavbar + "px");

    var navbarHeigh = $('nav.navbar-default').height();
    var wrapperHeigh = $('#page-wrapper').height();

    if (navbarHeigh > wrapperHeigh) {
        $('#page-wrapper').css("min-height", navbarHeigh + "px");
    }

    if (navbarHeigh < wrapperHeigh) {
        $('#page-wrapper').css("min-height", $(window).height() + "px");
    }

    if ($('body').hasClass('fixed-nav')) {
        if (navbarHeigh > wrapperHeigh) {
            $('#page-wrapper').css("min-height", navbarHeigh + "px");
        } else {
            $('#page-wrapper').css("min-height", $(window).height() - 60 + "px");
        }
    }

}

// Minimalize menu when screen is less than 768px
$(window).bind("resize", function () {
    if ($(this).width() < 769) {
        $('body').addClass('body-small')
    } else {
        $('body').removeClass('body-small')
    }
});

// Local Storage functions
// Set proper body class and plugins based on user configuration
$(document).ready(function () {
    if (localStorageSupport()) {

        var collapse = localStorage.getItem("collapse_menu");
        var fixedsidebar = localStorage.getItem("fixedsidebar");
        var fixednavbar = localStorage.getItem("fixednavbar");
        var boxedlayout = localStorage.getItem("boxedlayout");
        var fixedfooter = localStorage.getItem("fixedfooter");

        var body = $('body');

        if (fixedsidebar == 'on') {
            body.addClass('fixed-sidebar');
            $('.sidebar-collapse').slimScroll({
                height: '100%',
                railOpacity: 0.2,
                wheelStep: 5
            });
        }

        if (collapse == 'on') {
            if (body.hasClass('fixed-sidebar')) {
                if (!body.hasClass('body-small')) {
                    body.addClass('mini-navbar');
                }
            } else {
                if (!body.hasClass('body-small')) {
                    body.addClass('mini-navbar');
                }

            }
        }

        if (fixednavbar == 'on') {
            $(".navbar-static-top").removeClass('navbar-static-top').addClass('navbar-fixed-top');
            body.addClass('fixed-nav');
        }

        if (boxedlayout == 'on') {
            body.addClass('boxed-layout');
        }

        if (fixedfooter == 'on') {
            $(".footer").addClass('fixed');
        }
    }
});

// check if browser support HTML5 local storage
function localStorageSupport() {
    return (('localStorage' in window) && window['localStorage'] !== null)
}

// For demo purpose - animation css script
function animationHover(element, animation) {
    element = $(element);
    element.hover(
        function () {
            element.addClass('animated ' + animation);
        },
        function () {
            //wait for animation to finish before removing classes
            window.setTimeout(function () {
                element.removeClass('animated ' + animation);
            }, 2000);
        });
}

function SmoothlyMenu() {
    if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        $('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 200);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        $('#side-menu').removeAttr('style');
    }
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}