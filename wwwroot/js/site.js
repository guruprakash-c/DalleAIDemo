$(document).ready(() => {
    $(".loader").hide();
    $('#btnGen').click(function () {
        debugger;
        
        var input = {};
        input.n = parseInt($('#quantityTxtBx').val());
        input.prompt = $('#queryTxtBx').val();
        input.size = $('#sizeCmbBx').find(":selected").val();

        $.ajax({
            url: '/Home/GenerateImages',
            method: 'post',
            contentType: 'application/json',
            data: JSON.stringify(input),
            beforeSend: function () {
                $(".loader").show();
            },
            complete: function () {
                $(".loader").hide();
            },
            success: function (data) {
                if (typeof data.data !== undefined && data.data !== null && data.data != "") {
                    $.each(data.data, function () {
                        $('#genImages').append(
                            '<div class="col-lg-3">' +
                            '   <img class="img-thumbnail img-fluid bordered-0 rounded-3 bg-light" loading="lazy" decoding="async" src = "' + this.url + '"/>' +
                            '</div>');
                    });
                } else {
                    alert("[\"" + input.prompt + "\"]=>" + data.statusMessage);
                }
            },
            error: function (err) {
                alert(err.Message);
            }
    });
    });
});