var RequestVerificationToken = document.querySelector("[name='__RequestVerificationToken']").value
var botoesExcluir = document.querySelectorAll('.btn-link')

cancelReload()
function confirmarAcao(id) {
    Swal.fire({
        title: 'Deseja excluir ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        cancelButtonText: 'Nao',
        confirmButtonText: 'Sim, excluir!'
    }).then((result) => {
        if (result.value) {
            excluir(id);
        }

    })
}

function cancelReload(){
    botoesExcluir.forEach(function(botao){
        botao.addEventListener('click',(e)=>{
         e.preventDefault()
        })
    })
 }

function excluir(id) {
    console.log(id)
    $.ajax({
        type: "POST",
        url: "/LeiturasDoRelogio/Excluir/",
        data: {id},
        headers: {RequestVerificationToken},
        success: function () {
            notificarSucesso();
        },
        error: function () {
            notificarErro();
        }
    });
}

function notificarSucesso() {
    Swal.fire({
        icon: 'success',
        title: 'Ação realizada com sucesso!',
        confirmButtonColor: '#3085d6',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.value) {
            window.location.reload()
        }
    })
}

function notificarErro() {
    Swal.fire({
        icon: 'error',
        title: 'Ocorreu um erro!',
    })
}