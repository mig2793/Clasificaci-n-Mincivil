<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webClasification.aspx.cs" Inherits="webClasificacion.webClasification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clasificación</title>
    <link rel="stylesheet" type="text/css" href="css/normalize.min.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
</head>
<body>
    <div class="image-load">
        <div class="load"></div>
    </div>
    <div id="content">
        <header>
            <h3>Clasificación</h3> <span id="count"></span>
        </header>
        <div id="busqueda">
            <select  id="optionValue">
                <option value="1">Igual a</option>
                <option value="2">Comienza con</option>
                <option value="3">Termina con</option>
                <option value="4">Buscar por clasificacion</option>
                <!--<option value="5">No clasificadas</option>-->
                <option value="6">Detalle productos</option>
            </select>
            <input type="text" placeholder="Buscar" id="input-search" name="input-search"/>
            <input type="text" placeholder="Segundo filtro(Opcional)" id="second-filter" name="second-filter"/>
            <button id="button-search">Buscar</button><span id="counttable"></span><br/>
        </div>
        <div id="lista-bd">

        </div>
        <div id="buttons-check">
            <button id="selectAllButton" class="button-check">Seleccionar todos</button>
            <span id="dragAllChecked" class="button-check">Pasar a</span>
        </div>
        <div id="lista-clasificacion" class="table-bd">
            <ul>
            </ul>
        </div>
    </div>

    <script type="text/javascript" src="js/jquery-1.12.2.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui.min.js"></script>
    <script type="text/javascript" src="js/clasificacion.js"></script>
</body>

</html>
