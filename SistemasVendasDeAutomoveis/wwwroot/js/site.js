// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    $("#select-estado").on("change", function () {
        
        if ($(this).val() === "NOVO") {
            $(".campoKM").prop("disabled", true).val(0 + " KM");
        } else if ($(this).val() === "")
        {
            $(".campoKM").prop("disabled", true).val("Selecione o estado do veículo");
        }
        else {
            $(".campoKM").prop("disabled", false).val('000.000.000');
        }
    });
});