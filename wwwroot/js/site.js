// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('label#error_urlVideo').hide();
    $('.alert').hide();
    $("button#channel_submit").click(function () {
        $('.alert-danger').hide();
        var url = $("input#urlVideo").val();
        if (url == "") {
            $(".alert-danger").show();
            $("input#urlVideo").focus();
            return false;
        }
        $.ajax({
            type: "POST",
            url: "Edit/AddVideoYoutube",
            data: "url=" + url,
            success: function () {
                $('div#succes_video').show("fast");
            },
            error: function () {
                $('div#error_video').show("fast");
            }
        });
        return false;
    });
});

$(function () {
    $('label#error_location').hide();
    $('.alert').hide();
    $("button#weather_submit").click(function () {
        $('.alert-danger').hide();
        var location = $("input#location").val();
        if (location == "") {
            $(".alert-danger").show();
            $("input#location").focus();
            return false;
        }
        $.ajax({
            type: "POST",
            url: "Edit/AddWeather",
            data: "location=" + location,
            success: function () {
                $('div#succes_weather').show("fast");
            },
            error: function () {
                $('div#error_weather').show("fast");
            }
        });
        return false;
    });
});