// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//import { error } from "jquery";

// Write your JavaScript code.

Showpopup = (url, tittle) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(tittle);
            $("#form-modal").modal("show");
        }
    });
}
//$(function () {
//    tinymce.init();
//});

jQueryAjaxPost = (form, id) => {

    try {
        tinyMCE.triggerSave(true, true);        
        //$("#" + id + "").submit();       
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $("#" + id + "").load("/Student/GetView", { id: $("#Studentid").val(), viewName: id })
                    $("#form-modal .modal-body").html("");
                    $("#form-modal .modal-title").html("");
                    $("#form-modal").modal("hide");
                    $.notify("Uploaded Successfully", { globalPosition: 'top center', className: 'success' });
                }
                //else
                //    $("form-modal .modal-body").html(res);
            },
            error: function (err) {

            }
        })

    } catch (e) {
        console.log(e);
    }

    return false;
}

jQsupportRequest = () => {
    alert($("#complaintText").val());
    alert($("#txtEmail").val());
    alert($("#txtStdID").val());

    try {
        tinyMCE.triggerSave(true, true);
        //$("#" + id + "").submit();       
        $.ajax({
            type: 'POST',
            url: '@Url.Action("UploadFilesAjax", "Company")',
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#supportticketModal').modal('toggle');
                    $.notify("Uploaded Successfully", { globalPosition: 'top center', className: 'success' });
                }
                //else
                //    $("form-modal .modal-body").html(res);
            },
            error: function (err) {

            }
        })

    } catch (e) {
        console.log(e);
    }

    return false;

}