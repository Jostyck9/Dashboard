// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('label#error_urlVideo').hide();
    $('.alert').hide();
    $("button#video_submit").click(function () {
        $(".alert-success").hide();
        $('.alert-danger').hide();
        var delay = $("input#refreshVideo").val();
        if (delay == "")
            delay = "20";
        var url = $("input#urlVideo").val();
        if (url == "") {
            $(".alert-danger").show();
            $("input#urlVideo").focus();
            return false;
        }
        $.post("Edit/AddVideoYoutube", { url: url, delay: delay },
            function (result) {
                if (result == false) {
                    $('div#error_video').show("fast");
                } else {
                    $('div#succes_video').show("fast");
                    $("button.close").click();
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
        var delay = $("input#refreshChannel").val();
        if (delay == "")
            delay = "20";
        var url = $("input#idChannel").val();
        if (url == "") {
            $(".alert-danger").show();
            $("input#idChannel").focus();
            return false;
        }
        $.post("Edit/AddChannelYoutube", { id: url, delay: delay },
            function (result) {
                if (result == false) {
                    $('div#error_channel').show("fast");
                } else {
                    $('div#succes_channel').show("fast");
                    $("button.close").click();
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
        var delay = $("input#refreshWeather").val();
        if (delay == "")
            delay = "20";
        var location = $("input#location").val();
        if (location == "") {
            $(".alert-danger").show();
            $("input#location").focus();
            return false;
        }
        $.post('Edit/AddWeather', { location: location, delay: delay },
            function (result) {
                if (result == false) {
                    $('div#error_weather').show("fast");
                } else {
                    $('div#succes_weather').show("fast");
                    $("button.close").click();
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
        var delay = $("input#refreshSteam").val();
        if (delay == "")
            delay = "20";
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
        $.post(urlRedirection, { appId: appId, delay: delay },
            function (result) {
                if (result == false) {
                    $('div#error_steam').show("fast");
                } else {
                    $('div#succes_steam').show("fast");
                    $("button.close").click();
                }
            });
        return false;
    });
});


$(document).ready(function () {
    $("#deleteWidgethzbaduyzd").click(function () {
        var id = $(this).attr('idWidget');
        $.ajax({
            type: "POST",
            url: "Edit/DeleteWidget",
            data: "id=" + id,
            success: function (result) {
            }
        });
        return false;
    });
});

function deleteWidget(id) {
    $.ajax({
        type: "POST",
        url: "Edit/DeleteWidget",
        data: "id=" + id,
        success: function (result) {
            $.get('/home/Widgets', function (result) {
                $('#refreshColumn').html(result);
            });
        }
    });
    return false;
}

function refresh() {
    $.get('/home/Widgets', function (result) {
        $('#refreshColumn').html(result);
    });
}
setInterval(refresh, 20000)