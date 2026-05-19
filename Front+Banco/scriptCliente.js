const myForm = document.getElementById('cadastroCliente');
myForm.addEventListener('submit', function (event) {
    // 1. Prevenir o recarregamento da página ao submeter form
    event.preventDefault();

    fetch('https://localhost:7262/cliente', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: document.getElementById("nome").value,
            email: document.getElementById("email").value,
            sexo: document.getElementById("sexo").value,
            senha: document.getElementById("senha").value
        }),
    }).then(response => response.json())
        .then(data => {
            document.getElementById("respostaCliente").innerHTML ="<h4>Cliente cadastrado com sucesso! <br>"
            +"Seu ID gerado foi: "+data.id+"</h4>";        
        })
});
