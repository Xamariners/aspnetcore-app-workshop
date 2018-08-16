$.noConflict();
var $ = jQuery;
$(document).ready(function($) {

    /* smooth-scrolling	*/
    $(function() {
        $('.scrollbtn a[href*="#"]:not([href="#"])').on('click', function() {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    if ($(window).width() > 1169) {
                        $('html, body').animate({
                            scrollTop: target.offset().top - 72
                        }, 1000);
                        return false;
                    } else {
                        $('html, body').delay(200).animate({
                            scrollTop: target.offset().top - 72
                        }, 1000);
                        return false;
                    }
                }
            }
        });
    });

    //  NAVBAR CLOSE ICON 
    $(".navbar-toggle").on("click", function() {
        $(this).toggleClass("active");
        $("#main-header").toggleClass("open-nav");

    });

    $('#main-header nav ul li a').on('click', function() {
        $('#main-header').removeClass('open-nav');
        $('.navbar-toggle').removeClass('active');
        $('.navbar-collapse').removeClass('in');
        $('body').removeClass('hidden');
    });

    if ($(window).width() > 1024) {
        new WOW().init();
    };    

    // Back To top Js
    var offset = 300,
        offset_opacity = 1200,
        scroll_top_duration = 700,
        $back_to_top = $('.back-to-top');

    $(window).scroll(function(){

        ( $(this).scrollTop() > offset ) ? $back_to_top.addClass('cd-is-visible') : $back_to_top.removeClass('cd-is-visible cd-fade-out');
        if( $(this).scrollTop() > offset_opacity ) { 
            $back_to_top.addClass('cd-fade-out');
        }

        var scroll = $(window).scrollTop();
        if (scroll >= 50) {
            $("#main-header").addClass("darkHeader");
        } else {
            $("#main-header").removeClass("darkHeader");
        }

    });

    $back_to_top.on('click', function(event){
        event.preventDefault();
        $('body,html').animate({
            scrollTop: 0 ,
            }, scroll_top_duration
        );
    });

});

