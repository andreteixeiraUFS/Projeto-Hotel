const myForm1 = document.getElementById('reservar');
if (myForm1 != null) {
myForm1.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('https://localhost:7262/reserva', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
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
        if (response.status ==401){
            alert ("Faça login antes de reservar!");
            window.location.href="index.html";
        }
        response.json();})
        .then(data => {
            document.getElementById("respostaReserva").innerHTML ="<h4>Reserva cadastrada com sucesso!</h4>";        
        })
});
}

fetch('https://localhost:7262/reserva/reservasCliente/',
    { 
        credentials: 'include' 
})
    .then(response => {
        if (response.status==401){
            document.getElementById("botoes").innerHTML =
             "<button onclick=window.location.href=\"login.html\">Login ou Cadastro</button>";
            document.getElementById("respostaConsulta").innerHTML = "<h4> Faça login para ver suas Reservas</h4>";
        }else{
            document.getElementById("botoes").innerHTML = 
            "<button onclick=\"logout()\">Logout </button> <button onclick=window.location.href=\"cadastroReserva.html\">Realizar Reserva</button>";
            return response.json();
        }
       
    })
    .then(data => {
        if(data.length >0){
        var resposta = document.getElementById("respostaConsulta");
        resposta.innerHTML = "<h4>Segue Lista de suas reservas</h4> ";
        for (i = 0; i < data.length; i++) {
            resposta.innerHTML += "<li> Cliente: " + data[i].cliente + "</li>";
            resposta.innerHTML += "Diárias (noites): <input type='number' id='noites"+data[i].reservas+"' value='" + data[i].noites + "'>";
            resposta.innerHTML += "Hóspedes: <input type='number' id='hospedes"+data[i].reservas+"' value='" + data[i].hospedes + "'>";
            resposta.innerHTML += "Data Chegada: <input type='date' id='dataChegada"+data[i].reservas+"' value='" + data[i].dataChegada + "'>";
            resposta.innerHTML += "Mensagem: <input type='text' id='mensagem"+data[i].reservas+"' value='" + data[i].mensagem + "'>";
            resposta.innerHTML += "<button onclick='editaReserva("+data[i].reservas+")'>Editar Reserva </button>";
            resposta.innerHTML += "<button onclick='deletaReserva("+data[i].reservas+")'>Deletar Reserva </button> <hr>";

        }
    }
    });

    function deletaReserva(idReserva){
        fetch('https://localhost:7262/reserva/'+idReserva, {
            method: 'DELETE', 
            credentials: 'include'
  
        }).then(response => {
            alert("Reserva excluída");
            window.location.href="index.html";
        })
    }

    function editaReserva (idReserva){
        fetch('https://localhost:7262/reserva/'+idReserva, {
            method: 'PUT',   
            credentials: 'include',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                datachegada: document.getElementById("dataChegada"+idReserva).value,
                noites: parseInt(document.getElementById("noites"+idReserva).value),
                hospedes: parseInt(document.getElementById("hospedes"+idReserva).value),
                mensagem: document.getElementById("mensagem"+idReserva).value
            }),
        }).then(response => {
            if (response.status ==401){
                alert ("Faça login antes de editar!");
                window.location.href="index.html";
            }else{
                alert ("Reserva editada!");
            }})
           
    }