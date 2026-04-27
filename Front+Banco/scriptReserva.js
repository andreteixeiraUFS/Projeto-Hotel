const myForm1 = document.getElementById('reservar');
myForm1.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('http://localhost:5011/reserva', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            idCliente: document.getElementById("IdCliente").value,
            datachegada: document.getElementById("dataChegada").value,
            noites: parseInt(document.getElementById("noites").value),
            hospedes: parseInt(document.getElementById("hospedes").value),
            mensagem: document.getElementById("mensagem").value
        }),
    }).then(response => response.json())
        .then(data => {
            document.getElementById("respostaReserva").innerHTML ="<h4>Reserva cadastrada com sucesso!</h4>";        
        })
});



const myForm2 = document.getElementById('consultarReservas');
myForm2.addEventListener('submit', function (event) {
    event.preventDefault();

    //esse fetch é um GET, por isso não precisa de headers e de body
    fetch('http://localhost:5011/reserva/reservasCliente/'+document.getElementById("IdClienteConsulta").value)
    .then(response => response.json())
        .then(data => {
            console.log (data);
           var resposta = document.getElementById("respostaConsulta");
           resposta.innerHTML ="<h4>Segue Lista de suas reservas</h4> ";      
           for (i =0; i < data.length; i++){
                resposta.innerHTML += "<ul> <li> Cliente: "+data[i].cliente+"</li>";
                resposta.innerHTML += "<li> Diárias (noites): "+data[i].noites+"</li>";
                resposta.innerHTML += "<li> Hóspedes: "+data[i].hospedes+"</li>";
                resposta.innerHTML += "<li> Data: "+data[i].reservas+"</li> </ul> <hr> ";
           }  
          
        })
});