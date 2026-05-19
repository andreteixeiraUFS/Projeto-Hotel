function deletaReserva(idReserva) {
    fetch('https://localhost:7262/reserva/' + idReserva, {
        method: 'DELETE',
        credentials: 'include'
    }).then(response => window.location.href = "index.html")
}

function logout() {
    fetch('https://localhost:7262/cliente/logout', {
        credentials: 'include'
    }).then(response => window.location.href = "index.html")
}

fetch('https://localhost:7262/reserva/reservasCliente',
    {
        credentials: 'include'
    }
).then(response => response.json())
    .then(data => {
        if (data.length >0){
        var resposta = document.getElementById("respostaConsulta");
        resposta.innerHTML = "<h4>Segue Lista de suas reservas</h4> ";
        for (i = 0; i < data.length; i++) {
            resposta.innerHTML += "<li> Cliente: " + data[i].cliente + "</li>";
            resposta.innerHTML += "Diárias (noites): <input type='number' id='noites' value='" + data[i].noites +"'>";
            resposta.innerHTML +=  "Hóspedes: <input type='number' id='hospedes' value='" + data[i].hospedes +"'>";
            resposta.innerHTML +=  "Data: <input type='date' id='dataChegada' value='" + data[i].dataChegada +"'>";
             resposta.innerHTML +=  "Mensagem: <input type='text' id='mensagem' value='" + data[i].mensagem +"'>";
            resposta.innerHTML += "<button onclick='editarReserva(" + data[i].reservas + ")'> Editar Tarefa</button>"
            resposta.innerHTML += "<button onclick='deletaReserva(" + data[i].reservas+")'> Deletar Tarefa</button> <hr>"
        }
    }
    });


    function editarReserva (idReserva){
        fetch('https://localhost:7262/reserva/'+idReserva, {
        method: 'PUT', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            datachegada: document.getElementById("dataChegada").value,
            noites: parseInt(document.getElementById("noites").value),
            hospedes: parseInt(document.getElementById("hospedes").value),
            mensagem: document.getElementById("mensagem").value
        }),
    }).then(response => {
        if (response.status == 401) {
            document.getElementById("respostaReserva").innerHTML = "<h4>Realize login antes de reservar!</h4>";
        }
        response.text();
    })
        .then(data => {

            document.getElementById("respostaReserva").innerHTML = "<h4>Reserva cadastrada com sucesso!</h4>";
            window.location.href = "index.html";
        })
    }

