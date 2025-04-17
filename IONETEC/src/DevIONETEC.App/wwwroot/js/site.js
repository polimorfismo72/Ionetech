/*--------------------------Endereço e Contacto---------------------------------*/
function SetModal() {
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}
function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    $('#EnderecoContatoTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });

        SetModal();
        return false;
    });
}

/*--------------------------Get Preço do Produto----------------------------*/
function SetPreco() {
    $(document).ready(function () {
        $("#txtPrecoUnit").val(0);
        $("#ProdutoId").change(function () {
            var produtoId = $("#ProdutoId").val();
            if (produtoId == "") {
                //$("#txtPrecoUnit").empty();
                $("#txtPrecoUnit").val('0.00')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetItemUnitPrice(produtoId);
            }
        });
        /*$("input[type=text]").change(function () {*/
        //$("input").change(function () {
        //  CalcularPercentualDesconto();
        //});
    });
}
//function SetPrecoPer() {
//    $(document).ready(function () {
//        $("#txtPrecoUnit").val(0);
      
//        /*$("input[type=text]").change(function () {*/
//        $("input").change(function () {
//            CalcularPercentualDesconto();
//        });
//    });
//}
function GetItemUnitPrice(produtoId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        //url: '@Url.Action("GetPrecoProdutoUnitario")',
        //url: '@Url.Action("GetPrecoProdutoUnitario","Clientes")',
        url: '/Clientes/GetPrecoProdutoUnitario',

        data: { produtoId: produtoId },
        
        success: function (data) {
            $("#txtPrecoUnit").val(parseFloat(data).toFixed(2))
            .css("background-color", "#E9ECEF")
                         .css("color", "#000000"); 
        }
        //,
        //error: function (req,status,error) {
        //    alert("Há um problema para obter o preço unitário");
        //    //$("#txtPrecoUnit").empty();
        //    //$("#txtPrecoUnit").hide();
        //}
    });
    SetPreco();
    return false;
}

//function Login() {
//$(document).ready(function () {
//    $("#what").click(function () { //call event
//        $(".hello").hide();
//        $("#msg_box").fadeOut(2500);
//        return false
//    });
//});
//}

//function CalcularPercentualDesconto() {
//   /* var UnitPrice = $("#txtPrecoUnit").val();*/
//    var Quantidade = $("#txtQuantidade").val();
//    var ValorDesconto = $("#txtValorDesconto").val();

//    var PercDesconto = (ValorDesconto * 100) / (SetPreco() * Quantidade);

//    $("#txtPercentualDesconto").val(parseFloat(PercDesconto).toFixed(2));

//}

/*--------------------------Get Nome Produto -----------------------------*/

function SetNome() {
    $(document).ready(function () {
        $("#txtNome").val(0);
        $("#ProdutoId").change(function () {
            var produtoId = $("#ProdutoId").val();
            if (produtoId == "") {
                //$("#txtPrecoUnit").empty();
                $("#txtNome").val('0.00')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetItemUnitPrice(produtoId);
            }
        });
        /*$("input[type=text]").change(function () {*/
        //$("input").change(function () {
        //  CalcularPercentualDesconto();
        //});
    });
}

/*--------------------------Get NIF---------------------------------*/
function SetNif() {
    $(document).ready(function () {
        $("#txtNif").val('0123000');
        $("#ClienteId").change(function () {
            var clienteId = $("#ClienteId").val();
            if (clienteId == "") {
                $("#txtNif").val('0123000')
                    .css("background-color", "#E10E1C")
                    .css("color", "#FFFFFF");
            }
            else {
                GetItemNif(clienteId);
            }
        });
    });
}
function GetItemNif(clienteId) {
    $.ajax({
        async: true,
        type: 'GET',
        dataType: 'JSON',
        contentType: 'application/json; charset=utf-8',
        url: '/Pedidos/GetNifCliente',

        data: { clienteId: clienteId },

        success: function (data) {
            $("#txtNif").val(data)
                .css("background-color", "#E9ECEF")
                .css("color", "#000000");
        }
    });
    SetNif();
    return false;
}


/*-------------------------- Tempo para Modal desaparecer ----------------------------*/
$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
    $("#mensagem").fadeOut(30000);
});

//$('#LoginAalerta').on('closed.bs.alert', function () {
//    // do something...
//})
