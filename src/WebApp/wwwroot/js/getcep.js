$(document).ready(function () {
    $('#getCep').click(function () {
        // Chame o método de exclusão na sua controller
        $.ajax({
            type: 'POST',
            url: '@Url.Action("GetCep", "Cep")',
            data: { codigo: $("#Cep_Codigo").val() },
            dataType: "json",
            headers: { 'RequestVerificationToken': $('#antiForgeryForm')[0][0].value },
            success: function (response) {
                if (response != null) {
                    $("#Cep_Cidade").val(response.city);
                    $("#Cep_Estado").val(response.state);
                    $("#Cep_Bairro").val(response.district);
                    $("#Cep_Endereco").val(response.address);
                }
            },
            error: function () {
                // Trate o erro aqui, se necessário
                alert('Erro ao localizar CEP.');
            }
        });
    });
});