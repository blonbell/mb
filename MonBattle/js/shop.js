function startTime(trainD) {
    var today = new Date();

    if (today < trainD) {
        var remain = new Date();
        remain.setHours(trainD.getHours() - today.getHours());
        remain.setMinutes(trainD.getMinutes() - today.getMinutes());
        remain.setSeconds(trainD.getSeconds() - today.getSeconds());

        var h = remain.getHours();
        var m = remain.getMinutes();
        var s = remain.getSeconds();

        m = formatT(m);
        s = formatT(s);
        $("#lblTimer").text(" " + h + ":" + m + ":" + s);

        setTimeout(function () { startTime(trainD) }, 500);
    } else {
        $("#lblTimer").text("0:00:00");
        //show button
        $(".runFinish").fadeIn();
    }
}

function formatT(i) {
    if (i < 10) { i = "0" + i };  // add zero in front of numbers < 10
    return i;
}