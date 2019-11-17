// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('label#error_urlVideo').hide();
    $('.alert').hide();
    $("button#video_submit").click(function () {
        $(".alert-success").hide();
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
                $("button.close").click();
            },
            error: function () {
                $('div#error_video').show("fast");
            }
        });
        return false;
    });
});

$(function () {
    $('label#error_idChannel').hide();
    $('.alert').hide();
    $("button#channel_submit").click(function () {
        $(".alert-success").hide();
        $('.alert-danger').hide();
        var url = $("input#idChannel").val();
        if (url == "") {
            $(".alert-danger").show();
            $("input#idChannel").focus();
            return false;
        }
        $.ajax({
            type: "POST",
            url: "Edit/AddChannelYoutube",
            data: "id=" + url,
            success: function () {
                $('div#succes_channel').show("fast");
                $("button.close").click();
            },
            error: function () {
                $('div#error_channel').show("fast");
            }
        });
        return false;
    });
});

$(function () {
    $('label#error_location').hide();
    $('.alert').hide();
    $("button#weather_submit").click(function () {
        $(".alert-success").hide();
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
                $("button.close").click();
            },
            error: function () {
                $('div#error_weather').show("fast");
            }
        });
        return false;
    });
});

$(function () {
    $('label#error_appId').hide();
    $('.alert').hide();
    $("button#steam_submit").click(function () {
        $(".alert-success").hide();
        $('.alert-danger').hide();
        var appId = $("input#appId").val();
        if (appId == "") {
            $(".alert-danger").show();
            $("input#appId").focus();
            return false;
        }
        var type = $('input[name=radiosSteam]:checked').val();
        var urlRedirection = "Edit/"
        if (type == "players") {
            urlRedirection += "AddPlayersSteam";
        } else if (type == "news") {
            urlRedirection += "AddNewsSteam";
        } else if (type == "achievements") {
            urlRedirection += "AddAchievementSteam";
        }
        $.ajax({
            type: "POST",
            url: urlRedirection,
            data: "appId=" + appId,
            success: function () {
                $('div#error_steam').show("fast");
            },
            error: function () {
                $('div#succes_steam').show("fast");
            }
        });
        return false;
    });
});