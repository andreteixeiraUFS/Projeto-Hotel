const myForm1 = document.getElementById('reservar');
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
        if (response.status == 401) {
            document.getElementById("respostaReserva").innerHTML = "<h4>Realize login antes de reservar!</h4>";
        }
        response.text();
    })
        .then(data => {

            document.getElementById("respostaReserva").innerHTML = "<h4>Reserva cadastrada com sucesso!</h4>";
            window.location.href = "index.html";
        })
});


