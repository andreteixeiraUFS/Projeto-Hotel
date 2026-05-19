
const myFormLogin = document.getElementById('loginCliente');
myFormLogin.addEventListener('submit', function (event) {
    event.preventDefault();
    fetch('https://localhost:7262/cliente/login', {
        method: 'POST', //Para outros métodos, basta alterar aqui. Obs: Delete remove a parte do body e headers, e no get é conforme todos os exemploes feitos na Unidade interação com API 
        credentials:'include',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            nome: " ",
            email: document.getElementById("email").value,
            sexo: " ",
            senha: document.getElementById("senha").value
        }),
    }).then(response => {
        if (response.status == 404){
            alert("Email ou senha incorretos");
        } else{
            alert("Logado com sucesso");
            window.location.href= "index.html";
        }
    })
      
});