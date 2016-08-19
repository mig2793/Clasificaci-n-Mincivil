
$(document).ready(function () {

    var id = "";
    var codItem = new Array();
    var checked;

    function ajaxCategorias() {

        $.ajax({
            type: "POST",
            url: "serviceClasifi.asmx/GetCategorias",
            data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccessCategorias,
            error: OnErrorCallCategorias,
            complete: function () {
                hideLoad();
            }
        });

        function OnSuccessCategorias(response) {
            console.log(response.d);
            var list = response.d;
            var listDrop = "";
            var table = "";

            for (i = 0; i < list.length; i++) {
                listDrop += '<li class="droppable-list" id="' + Number(i + 1) + '"><div>' + Number(i + 1) + ". " + list[i]._categorias + '</div></li>'
            }

            $("#lista-clasificacion ul").html(listDrop);
        }

        function OnErrorCallCategorias(xhr) {
            alert(xhr.responseText);
        }
    }

    $("#button-search").click(function (e) {
        e.preventDefault();
        ajaxServiceSelect($("#optionValue").val())

    })

    $("#second-filter, #input-search").keypress(function (e) {
        var code = e.keycode || e.which;
        if (code == 13) {
            ajaxServiceSelect($("#optionValue").val());
        }
    });

    function ajaxServiceSelect(option) {
        console.log(option);
        var input = $("#input-search").val().trim();
        var inputOptional = $("#second-filter").val().trim();
        option = Number(option);

        if (input != undefined && input != "") {

            if (inputOptional != undefined && inputOptional != "") {
                showLoad();
                $.ajax({
                    type: "POST",
                    url: "serviceClasifi.asmx/GetSelectOptional",
                    data: "{ 'condicion': '" + input + "','inputOptional': '" + inputOptional + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    error: OnErrorCall,
                    complete: function () {
                        hideLoad();
                    }
                });

            } else {
                showLoad();
                $.ajax({
                    type: "POST",
                    url: "serviceClasifi.asmx/GetSelect",
                    data: "{ 'condicion': '" + input + "', 'option': " + option + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    error: OnErrorCall,
                    complete: function () {
                        hideLoad();
                    }
                });
            }

            function OnSuccess(response) {
                console.log(response.d)
                var list = response.d;
                var newCodItem = "";
                var listDrag = "";
                var table = "";
                $("#count").text("(" +list.length +")");
                list = sortByKey(list, 'DescripcionItem');
                table += '<table>'
                table += '<tr>'
                table += '<td>Numero Id</td>'
                table += '<td>Cod item</td>'
                table += '<td>Descripcion item</td>'
                table += '<td>Descripcion partida ptal</td>'
                table += '<td>Clasificacion</td>'
                table += '</tr>'
                
                for (i = 0; i < list.length;i++){
                    table += '<tr>'
                    table += '<td>' + Number(i+1) + '</td>'
                    table += '<td>'+ list[i].CodItem +'</td>'
                    newCodItem = list[i].CodItem.replace(/\./g, "_");
                    table += '<td><span ><div id="' + newCodItem + '" class="draggable-list ' + newCodItem + '"><input type="checkbox" id="check_'+ i +'" value="' + i + '" />' + list[i].DescripcionItem + '</div></span></td>'
                    table += '<td>' + list[i].DescriptionPptal + '</td>'
                    table += '<td>' + list[i]._Clasifi + '</td>'
                    table += '</tr>'
                }
                table += '</table>'
                $("#lista-bd").html(table);
                $("#lista-arrastrar ul").html(listDrag);
                dragDrop();
                checked = true;
                $("#selectAllButton").click(SelectAllCheck);
                $("#counttable").html($("#lista-bd table tr").length - 1)
            }
            function OnErrorCall(xhr) {
                alert(xhr.responseText);
            }

        } else {
            alert("Escribe tu consulta");
        }
    }

    function SelectAllCheck(e) {
        var lengthCheck = $('#lista-bd table input:checkbox').length;
        console.log(checked);
        if (checked == true) {
            $("#selectAllButton").text("Deseleccionar todos");
            for (i = 0; i < lengthCheck; i++){
                $('#check_' + i).prop("checked", true);
            }
        } else if (checked == false) {
            $("#selectAllButton").text("Seleccionar todos");
            for (i = 0; i < lengthCheck; i++) {
                $('#check_' + i).prop("checked", false);
            }
        }
        checked = !checked;
        console.log(checked);
    }

    function dragDrop() {

            var subCatContainer = $("#lista-arrastrar ul");
            subCatContainer.scroll(function () {
                subCatContainer.scrollTop($("#lista-bd table").scrollTop());
            });

        $(".draggable-list").draggable({
            opacity: 0.5,
            revert: true,
            helper: 'clone',
            appendTo: 'body',

            drag: function (event, ui) {
                console.log($(this).text());
            },
            start: function (event, ui) {
                codItem = [];
                codItem.push( $(this).attr('id'));
        }
        })
        $("#dragAllChecked").draggable({
            opacity: 0.8,
            revert: true,

            drag: function (event, ui) {
                console.log($(this).text());
            },
            start: function (event, ui) {
                codItem=[];
                var ComboCheck = $('#lista-bd table input:checkbox:checked');
                var lengthCheck = $('#lista-bd table input:checkbox:checked').length;
                var values = $(":checkbox:checked")
                  .map(function () {
                      return this.parentElement.id
                  })
                for (i = 0; i < lengthCheck; i++) {
                    codItem.push(values[i]);
                }
                
            }
        })
        
        $("#lista-clasificacion ul li").droppable({

            tolerance: "intersect",

            over: function (event, ui) {
                $(this)
                    .css("background-color", "#EF0003");
            },
            out: function (event, ui) {
                $(this)
                    .css("background-color", "rgb(0, 161, 78)");
            },
            drop: function (event, ui) {
                console.log($(this).text());

                $(this)
                    .css("background-color", "rgb(0, 161, 78)");

                id = $(this).attr('id');
                if (codItem.length > 1) {
                    for (i = 0; i < codItem.length; i++) {
                        ajaxUpdateCategorias(id, codItem[i]);
                    }
                }
                else {
                    ajaxUpdateCategorias(id, codItem[0]);
                }
            }
        });
    }

    function ajaxUpdateCategorias(id, codItem) {
        showLoad();
        var clasificacion = Number(id);
        codItem = codItem.replace(/_/gi, ".");
        $.ajax({
            type: "POST",
            url: "serviceClasifi.asmx/UpdateClasification",
            data: "{ '_clasifi': " + clasificacion + ", '_codItem': '" + codItem + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            error: OnError,
            complete: function () {
                hideLoad();
            }
        });

        function OnSuccess(response) {
            console.log(response.d)
            var codItemRemove = codItem.replace(/\./g, "_");
            if (response.d >= 1) {
                $("." + codItemRemove).parent().parent().parent().remove();
            }
            $("#counttable").html($("#lista-bd table tr").length - 1);
        }

        function OnError(xhr) {
            alert(xhr.responseText);
        }
    }

    function sortByKey(array, key) {
        return array.sort(function (a, b) {
            var x = a[key]; var y = b[key];
            return ((x < y) ? -1 : ((x > y) ? 1 : 0));
        });
    }

    function hideLoad() {
        $(".image-load").hide();
    }
    function showLoad() {
        $(".image-load").show();
    }

    ajaxCategorias();

});

