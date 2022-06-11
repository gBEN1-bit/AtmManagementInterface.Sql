var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function formatnumber(money) {
    money = money.toString()
        .trim().replace(/,/g, '');
    if (money == "") return money;
    var integerPrefix = Math.trunc(Number(money))
        .toString()
        .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    if (money.lastIndexOf(".") !== -1) {
        return integerPrefix + "."
            + Number(
                Number("." + money.slice(money.lastIndexOf(".") + 1))
                    .toFixed(5)
            ).toString().split(".", 2)[1];
    }
    return integerPrefix;
}


var accountId;
function bulkFormatter(value, row, index) {
    return [     
        '<a style="color:white"  title="Remove chart" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.bulkEvents = {

    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        accountId = $('#ID').val(row.Id);
        $.ajax({
            url: 'ExcelBulkUpload/RemoveBulk',
            type: 'POST',
            data: { ID: row.id },
            success: function (data) {
                swal({
                    title: "Are you sure?",
                    text: "You are about to delete this record!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#ff9800",
                    confirmButtonText: "Yes, proceed",
                    cancelButtonText: "No, cancel!",
                    showLoaderOnConfirm: true,
                    preConfirm: function () {
                        return new Promise(function (resolve) {
                            setTimeout(function () {
                                resolve();
                            }, 4000);
                        });
                    }
                }).then(function (isConfirm) {
                    if (isConfirm) {



                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#batchTable').
                            bootstrapTable(
                                'refresh', { url: 'ExcelBulkUpload/listGeneralupload' });

                        //return false;
                    }
                    else {
                        swal("Batch Upload", "You cancelled delete upload.", "error");
                    }

                });
                return

            },

            error: function (e) {
                //alert("An exception occured!");
                swal("An exception occured!");
            }
        });
    }
};




$(document).ready(function ($) {

    $('#btnSend').on('click', function () {

        batchTransactions();
    });

    $('#btnSend').on('click', function () {
        debugger

        batchTransactions();
    });
    $('#btnSendAll').on('click', function () {
        debugger
        var Descriptions = $('#comment').val()
        if (/[^a-z\d_: ]/i.test(Descriptions)) {

            $('#comment').val('');


        }
        $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
        batchTransactionAll();
    });
    $('btnDownloads').on('click', function () {
        debugger;
        $.ajax({
            type: "post",
            url: 'fundtransfer/Download',

        }).done(function (data) {

            if (data.fileName != "") {

                window.location.href = "@Url.RouteUrl(new { Controller = 'BulkUpload', Action = 'Index' })/?file=" + data.fileName
            }

        });

    });


    $('#btnDownload').on('click', function () {
        debugger
        var data = new FormData();
        jQuery.each(jQuery('#file')[0].files, function (i, file) {
            data.append('file-' + i, file);
        });
        $("input[type=submit]").attr("disabled", "disabled");

        $.ajax({
            type: 'POST',
            url: url_path + '/download',
            cache: false,
            contentType: false,
            processData: false,
            method: 'POST',
            success: function (result) {


            }
        });

    });

});




function batchTransactions() {
    debugger
   var batchData = $batchTables.bootstrapTable('getAllSelections');
   


    $("input[type=submit]").attr("disabled", "disabled");

    swal({
        title: "Are you sure?",
        text: "Transaction(s) will be sent!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#ff9800",
        confirmButtonText: "Yes, continue",
        cancelButtonText: "No, stop!",
        showLoaderOnConfirm: true,
        preConfirm: function () {
            return new Promise(function (resolve) {
                setTimeout(function () {
                    resolve();
                }, 4000);
            });
        }
    }).then(function (isConfirm) {
        if (isConfirm) {
            swal({
                title: 'Sending for Approval...',
                html: 'Please wait...',
                showConfirmButton: false,

                allowEscapeKey: false,
                allowOutsideClick: false,
                didOpen: () => {
                    swal.showLoading()
                }
            });
            debugger
            $('#btnSend').attr('disabled', 'disabled');
            if (batchData["length"] !== 0) {

                $.ajax({
                    //contentType: 'application/json',
                    dataType: 'json',
                    url: 'ExcelBulkUpload/batchuploadTransfer',
                    type: 'POST',
                    data: { 'batchData': batchData},
                    success: function (result) {
                        debugger
                        if (result.message !== " ") {
                            swal({ title: 'General Upload ', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                            $('#batchTable').bootstrapTable('refresh', {
                                silent: true
                            });
                            $('#btnSend').removeAttr('disabled');
                        }
                        else {
                            swal({ title: 'General Upload ', text: 'General upload sent for  approval!', type: 'success' }).then(function () { });

                            $('#batchTable').bootstrapTable('refresh', {
                                silent: true
                            });
                            $('#btnSend').removeAttr('disabled');
                        }
                    },
                    error: function (e) {
                        swal({ title: 'General Upload', text: 'General Upload failed', type: 'error' }).then(function () { clear(); });

                    }
                });

            }

        }

    }),

        function (dismiss) {
            swal({
                title: 'Transfer Reversal',
                text: 'Reversal encountered an error',
                type: 'error',
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(function () {
                $("#wizardComponent .btn-finish").attr("enabled", "true");
            });

        }

}

var $batchTables = $('#batchTable');

$('#BtnUploadTemp').on('click', function () {
    debugger
    $.ajax({
        url: "ExcelBulkUpload/GetExistingRecord",
    }).then(function (response) {
        debugger
        if (response > 0) {

            swal({
                text: "You have unsubmitted file on the grid!",
                title: "Are you sure you want to override?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    debugger
                    $.ajax({
                        url: "ExcelBulkUpload/RemoveExistingRecord",
                    })

                    tblStaffInformation();

                }


            })
        }
        else {
            tblStaffInformation();
        }
    });

})
function tblStaffInformation() {
    $("input[type=submit]").attr("disabled", "disabled");
    $("#BtnUploadTemp").attr("disabled", "disabled");
    var Staffsignature = $("#doctemplate").get(0).files;
    debugger;

    if (!Staffsignature) {
        return;
    }

    swal({
        title: 'Uploading Sheet...',
        html: 'Please wait...',
        showConfirmButton: false,

        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
            swal.showLoading()
        }
    });

    var signatureimgData = new FormData();
    signatureimgData.append("Staffsignature", $("#doctemplate").get(0).files[0]);
    debugger;

    $.ajax({
            url: url_path + "ExcelBulkUpload/Import",

        method: "POST",
        contentType: false,
        processData: false,
        data: signatureimgData,
        success: function (result) {
            debugger
            var readMesaage = result.message;

            if (result.message == "") {
                swal({ title: 'Upload Transaction', text: 'Transaction uploaded successfully!', type: 'success' }).then(function () { window.location.reload(true); });

                $('#batchTable').
                    bootstrapTable(
                        'refresh', { url: url_path + '/listbatchupload' });
                $("#BtnUploadTemp").removeAttr("disabled", true);

            }
            if (result.message.includes("Sum of credit not equal to sum of debit with the difference of")) {
                swal({
                    title: 'Upload Transaction', text: 'Something went wrong:' + result.message, type: 'error'
                }).then(function () { window.location.reload(true); });
            }
            if (result.message.includes("The sheet name has been changed")) {
                swal({
                    title: 'Upload Transaction', text: 'Something went wrong:' + result.message, type: 'error'
                }).then(function () { window.location.reload(true); });
            }
            if (result.message.includes("One of the row is empty:")) {
                swal({
                    title: 'Upload Transaction', text: 'Something went wrong:' + result.message, type: 'error'
                }).then(function () { window.location.reload(true); });
            }
            else {

                var $tables = $('#errorTable');
                $tables.bootstrapTable("destroy");

                $tables.bootstrapTable({
                    data: result.message,

                    columns: [
                        {
                            field: 'accountNo',
                            title: 'GL No'
                        },
                        {
                            field: 'transactionType',
                            title: 'Tran. Type '
                        },
                        {
                            field: 'reference',
                            title: 'Reference'
                        }
                        ,
                        {
                            field: 'miscode',
                            title: 'MISCODE'
                        },
                        {
                            field: "narration",
                            title: 'Narration'
                        }

                    ]
                });

                $('#ErrorDownload').modal("show");

                $("#BtnUploadTemp").removeAttr("disabled", true);
            }
        },
        error: function (e) {
            swal({ title: 'File Upload ', text: 'You have not selected any file for upload, try again', type: 'error' }).then(function () { window.location.reload(true); });

        }
    });
}
