$(document).ready(function () {
    cargarDoctores();

    function cargarDoctores() {
        $.get("/Doctor/GetAllDoctors", function (data) {
            let doctores = JSON.parse(data);
            let filas = "";
            doctores.forEach(d => {
                filas += `<tr>
                    <td>${d.nombre}</td>
                    <td>${d.especialidad}</td>
                    <td>
                        <a href="/Doctor/Edit/${d.id}" class="btn btn-sm btn-warning">Editar</a>
                        <button onclick="eliminar(${d.id})" class="btn btn-sm btn-danger">Eliminar</button>
                    </td>
                </tr>`;
            });
            $("#tablaDoctores tbody").html(filas);
        });
    }

    window.eliminar = function (id) {
        if (confirm("¿Eliminar este doctor?")) {
            $.ajax({
                url: `/Doctor/DeleteDoctor?id=${id}`,
                type: "DELETE",
                success: function () {
                    alert("Doctor eliminado");
                    cargarDoctores();
                },
                error: function () {
                    alert("Error al eliminar");
                }
            });
        }
    };
});