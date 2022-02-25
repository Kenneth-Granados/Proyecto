// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var total_letras = 200;
    $('#CommentTextArea').keyup(function () {
        var longitud = $(this).val().length;
        var resto = total_letras - longitud;
        $('#numero').html("Escribe tu Comentario ( "+resto+" )");
        //$('#numero').val(resto.toString());
        if (resto <= 0) {
            $('#CommentTextArea').attr("maxlength",200);
        }
    });
    var screen = $('#loaderbody');
    LoadingScreen(screen);
});
function LoadingScreen(screen) {
    $(document).ajaxStart(function () {
        screen.fadeIn();
    }).ajaxStop(function () {
        screen.fadeOut();
    })
}

jQueryAjaxPostAddComentario = form => {
    $("#loaderbody").addClass('hide');
    try {
        let text = $("#CommentTextArea").val();
        if (text.length > 0) {
            
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        $("#loaderbody").removeClass('hide');
                        $('#view-all').html(res.html);
                        $("#CommentTextArea").val('');
                        $('#numero').html("Escribe tu Comentario");
                    }
                    else {
                        $("#loaderbody").removeClass('hide');
                        alert("Ocurrio un error");
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
        }
        else {
            alert("Completa la caja de texto");
        }

        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

