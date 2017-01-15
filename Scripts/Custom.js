$(function () {

    //var urlAction = "http://localhost:54003/Payment/";
    var urlAction = "http://t2ua1282fc3.doitt.nycnet/DOHPaymentPortal/Payment/";

    $(document).ready(function () {
        //function will be called on button click having id btnsave
        $("#btnAddDocument").click(function () {
            $.ajax(
            {
                type: "POST", //HTTP POST Method  
                url: urlAction + "AddDocumentsRow", // Controller/View   
                data: { //Passing data  
                    Category: $('#ddlDocumentCategory option:selected').text(),
                    Description: $("#txtDescription").val(),
                    UploadDate: $("#txtUploadDate").val(),
                    ModifiedDate: $("#txtModifiedDate").val(),
                    FileName: $("#filebutton").val()
                },
                success: function (data) {
                    alert('Added successfully');
                    window.location.href = urlAction + "Index";
                },
                error: function () {
                    alert('Error saving data.');
                }

            });
        });
    });


    $('.edit').hide();
    $('.edit-case').on('click', function () {
        var tr = $(this).parents('tr:first');
        tr.find('.edit, .read').toggle();
    });

    $('.update-case').on('click', function (e) {
        e.preventDefault();
        var tr = $(this).parents('tr:first');
        id = $(this).prop('id');
        var category = tr.find('#Category').val();
        var fileName = tr.find('#FileName').val();
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: urlAction + "Edit",
            data: JSON.stringify({ "id": id, "category": category, "fileName": fileName }),
            dataType: "json",
            success: function (data) {
                tr.find('.edit, .read').toggle();
                $('.edit').hide();
                tr.find('#Category').text(data.document.Category);
                tr.find('#FileName').text(data.document.FileName);
            },
            error: function (err) {
                alert("error");
            }
        });
    });
    $('.cancel-case').on('click', function (e) {
        e.preventDefault();
        var tr = $(this).parents('tr:first');
        var id = $(this).prop('id');
        tr.find('.edit, .read').toggle();
        $('.edit').hide();
    });
    $('.delete-case').on('click', function (e) {
        e.preventDefault();
        var tr = $(this).parents('tr:first');
        id = $(this).prop('id');
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: urlAction + "Delete/" + id,
            dataType: "json",
            success: function (data) {
                alert('Delete success');
                // this will refresh entire view 
                window.location.href = urlAction + "Index";
            },
            error: function () {
                alert('Error occured during delete.');
            }
        });
    });


    //$(document).ready(function ($) {
    //    $('#myaccordion').find('.accordion-toggle').click(function () {

    //        //Expand or collapse this panel
    //        $(this).next().slideToggle('fast');

    //        //Hide the other panels
    //        $(".accordion-content").not($(this).next()).slideUp('fast');

    //    });
    //});

    //var activeIndex = parseInt(getCookie('openedPane'));
    //$("#myaccordion").accordion({
    //    collapsible: true, clearStyle: true, heightStyle: "content", navigation: true, active: activeIndex,
    //    activate: function (event, ui) {
    //        var index = $(this).children('h3').index(ui.newHeader);
    //        setCookie('openedPane', index, 1);
    //    },
    //    create: function (event, ui) {
    //        deleteCookie('openedPane');   //Deleting the cookie when accordion is created         
    //    }
    //});

    //function setCookie(name, value, days) {
    //    if (days) {
    //        var date = new Date();
    //        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    //        var expires = "; expires=" + date.toGMTString();
    //    }
    //    else var expires = "";
    //    document.cookie = name + "=" + value + expires + "; path=/";
    //}

    //function getCookie(name) {
    //    var nameEQ = name + "=";
    //    var ca = document.cookie.split(';');
    //    for (var i = 0; i < ca.length; i++) {
    //        var c = ca[i];
    //        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
    //        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    //    }
    //    return null;
    //}

    //function deleteCookie(name) {
    //    setCookie(name, "", -1);
    //}
    

});