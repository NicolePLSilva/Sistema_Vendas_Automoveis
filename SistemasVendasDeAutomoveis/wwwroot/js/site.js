// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#select-estado").on("change", function () {
        
        if ($(this).val() === "NOVO") {
            $(".campoKM").prop("readonly", true).val('Carro 0 KM');
        } else if ($(this).val() === "USADO")
        {
            $(".campoKM").prop("readonly", false).val('000.000.000');
        }
        else {
            $(".campoKM").prop("readonly", true).val("Selecione o estado do veículo");
        }
    });
});

$('.close').click(function () {
    $('.alert').hide('hide');
    $('.modal').hide('hide');
});

$('#excluirVeiculo').click(function () {
    $('.modal-excluir').show('show');
});

$('#excluirUsuario').click(function () {
    $('.modal-excluir').show('show');
});

$('#dropdownMenuButton').click(function () {
    $('.dropdown-toggle').dropdown('toggle');
});