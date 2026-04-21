const API_URL = "http://localhost:5296";
let veiculos = [];              // lista principal de veículos
let veiculosFiltrados = [];     // lista após aplicar filtros
const inputPlaca = document.getElementById("inputPlaca");

// filtros
const selectMarca   = document.getElementById("fMarca");
const selectAno     = document.getElementById("fAno");
const selectVendido = document.getElementById("fVendido");

// tabela
const tabela = document.getElementById("tabela");

//formulário
const inputMarca     = document.getElementById("marca");
const inputModelo    = document.getElementById("modelo");
const inputAno       = document.getElementById("ano");
const inputInspecao  = document.getElementById("inspecao");
const inputVendido   = document.getElementById("vendido");

const form           = document.getElementById("formVeiculo");
const editIndexInput = document.getElementById("editIndex");

async function carregarVeiculos() { //utiliza o async, pois a função é assincrona, esperando a resposta da rede
    try{
        const resposta = await fetch (`${API_URL}/carros`)
        if (!resposta.ok) throw new Error("Erro ao carregar veículos");

        veiculos = await resposta.json(); //recebe a lista do VeiculoViewDTO

        veiculos.forEach(v => {
            v.ultimaInspecao = new Date(v.ultimaInspecao)
        });

        atualizarFiltros();
        render();

    } catch (erro) {
        console.error(erro);
        alert("Erro ao contectar com a API!")
    }

}

async function carregarSelectMarcas() {
    try {
        const resposta = await fetch(`${API_URL}/marcas`); // Sua rota de marcas
        const marcas = await resposta.json();
        
        inputMarca.innerHTML = '<option value="">Selecione uma marca</option>';
        marcas.forEach(m => {
            const opt = document.createElement("option");
            opt.value = m.marcaID; // O ID que o banco usa
            opt.textContent = m.nomeMarca; // O nome que o humano vê
            inputMarca.appendChild(opt);
        });
    } catch (erro) {
        console.error("Erro ao carregar marcas:", erro);
    }
}

inputMarca.addEventListener("change", async function() { //carregar nos modelos de acordo com a marca selecionada
    const marcaID = this.value;
    inputModelo.innerHTML = '<option value="">Carregando...</option>';
    inputModelo.disabled = !marcaID;

    if (!marcaID) return;

    try {
        const resposta = await fetch(`${API_URL}/modelos/marca/${marcaID}`); 
        const modelos = await resposta.json();

        inputModelo.innerHTML = '<option value="">Selecione um modelo</option>';
        modelos.forEach(m => {
            const opt = document.createElement("option");
            opt.value = m.modeloID;
            opt.textContent = m.nomeModelo;
            inputModelo.appendChild(opt);
        });
    } catch (erro) {
        inputModelo.innerHTML = '<option value="">Erro ao carregar</option>';
    }
});

function guardar() { //função para guardar veiculos adicionados/modificados na LocalStorage
    console.log("Os dados são salvos automaticamente no banco de dados via API.");
}

//carregarVeiculos();


//FUNÇÕES AUXILIARES

function limparSelect(select, texto = "Todos") {
    select.innerHTML = `<option value="">${texto}</option>`;
}

function getListUnique(lista, k) {
    return [...new Set(lista.map(element => element[k]))];
}

function toInputDateLocal(d) {
    const pad = n => String(n).padStart(2, "0");
    return `${d.getFullYear()}-${pad(d.getMonth() + 1)}-${pad(d.getDate())}`;
}

function setDate(y, m, d) {
    let tmp = new Date(y, m, d);
    tmp.setMonth(tmp.getMonth() - 1);
    return tmp;
}


//PREENCHER FILTROS



function preencherComboMarca() {
    limparSelect(selectMarca, "Todas as marcas");

    const marcas = getListUnique(veiculos, "marca").sort();

    marcas.forEach(marca => {
        const opt = document.createElement("option");
        opt.value = marca;
        opt.textContent = marca;
        selectMarca.appendChild(opt);
    });
}

function preencherComboAno() {
    limparSelect(selectAno, "Todos os anos");

    const anos = getListUnique(veiculos, "ano").sort();

    anos.forEach(ano => {
        const opt = document.createElement("option");
        opt.value = ano;
        opt.textContent = ano;
        selectAno.appendChild(opt);
    });
}

function preencherComboStatus() {
    limparSelect(selectVendido, "Todos");

    const status = getListUnique(veiculos, "vendido").sort();

    status.forEach(valor => {
        const opt = document.createElement("option");
        opt.value = valor;
        opt.textContent = valor ? "Vendido" : "Disponível";
        selectVendido.appendChild(opt);
    });
}

function atualizarFiltros() { //Atualiza todos os filtros
    preencherComboMarca();
    preencherComboAno();
    preencherComboStatus();
}

//atualizarFiltros();


//TABELA

function inspecaoEstado(data) {
    const agora = new Date();
    const diffMeses = (agora - data) / (1000 * 60 * 60 * 24 * 30);

    if (diffMeses > 12) return '<span class="vendido">Expirada</span>';
    if (diffMeses > 10) return '<span class="aviso">A expirar</span>';
    return '<span class="ok">Válida</span>';
}

function preencherTabela() {
    tabela.innerHTML = "";

    veiculosFiltrados.forEach((veiculo, index) => {
        const tr = document.createElement("tr");

        const estadoInspecao = inspecaoEstado(veiculo.ultimaInspecao);

        tr.innerHTML = `
            <td>${veiculo.marca}</td>
            <td>${veiculo.modelo}</td>
            <td>${veiculo.ano}</td>
            <td>${toInputDateLocal(veiculo.ultimaInspecao)} | ${estadoInspecao}</td>
            <td>
                ${veiculo.vendido 
                    ? '<span class="status vendido">❌ Vendido</span>' 
                    : '<span class="status disponivel">✔ Disponível</span>'}
            </td>
            <td class="acoes">
                <button class="editar-btn" onclick="editar(${index})">Editar</button>
                <button class="excluir-btn" onclick="excluir(${index})">Excluir</button>
            </td>
        `;

        tabela.appendChild(tr);
    });
}


//RENDER - FILTROS

function render() {
    const marcaSelecionada  = selectMarca.value;
    const anoSelecionado    = selectAno.value;
    const statusSelecionado = selectVendido.value;

    veiculosFiltrados = veiculos.filter(veiculo => {

        if (marcaSelecionada && veiculo.marca !== marcaSelecionada) return false;
        if (anoSelecionado && veiculo.ano !== Number(anoSelecionado)) return false;

        if (statusSelecionado !== "") {
            const vendidoBool = statusSelecionado === "true";
            if (veiculo.vendido !== vendidoBool) return false;
        }
        console.log("Veículos carregados:", veiculos);
        return true;
    });

    preencherTabela();
}

[selectMarca, selectAno, selectVendido].forEach(f =>
    f.addEventListener("change", render)
);

//render();


//EDITAR - EXCLUIR

async function editar(i) {
    const v = veiculosFiltrados[i];

    
    editIndexInput.value = i;
    inputAno.value = v.ano;
    inputPlaca.value = v.Placa; 
    inputInspecao.value = toInputDateLocal(v.ultimaInspecao);
    inputVendido.checked = v.vendido;

    const opcaoMarca = [...inputMarca.options].find(opt => opt.text === v.marca);
    
    if (opcaoMarca) {
        inputMarca.value = opcaoMarca.value;
        await carregarModelosParaEdicao(opcaoMarca.value, v.modelo);
    }
}

// Função auxiliar para ajudar o 'editar' a selecionar o modelo certo após o fetch
async function carregarModelosParaEdicao(marcaID, nomeModeloDesejado) {
    inputModelo.disabled = false;
    inputModelo.innerHTML = '<option value="">Carregando...</option>';

    try {
        const resposta = await fetch(`${API_URL}/modelos/marca/${marcaID}`);
        const modelos = await resposta.json();

        inputModelo.innerHTML = '<option value="">Selecione um modelo</option>';
        modelos.forEach(m => {
            const opt = document.createElement("option");
            opt.value = m.modeloID;
            opt.textContent = m.nomeModelo;
            inputModelo.appendChild(opt);
        });

        // Após carregar todos, selecionamos o que o carro já tinha
        const opcaoModelo = [...inputModelo.options].find(opt => opt.text === nomeModeloDesejado);
        if (opcaoModelo) {
            inputModelo.value = opcaoModelo.value;
        }
    } catch (erro) {
        console.error("Erro ao carregar modelos na edição:", erro);
    }
}

async function excluir(i) {
const veiculoSelecionado = veiculosFiltrados[i];
    const confirmar = confirm(`Tens a certeza que queres excluir o veículo ${veiculoSelecionado.modelo}?`);
    
    if (!confirmar) return;

    try {
        const resposta = await fetch(`${API_URL}/carro/${veiculoSelecionado.id}`, {
            method: 'DELETE'
        });

        if (resposta.ok) {
            await carregarVeiculos(); // Recarrega a lista atualizada do banco
        } else {
            alert("Não foi possível excluir o veículo.");
        }
    } catch (erro) {
        console.error("Erro ao excluir:", erro);
    }
}


//FORMULÁRIO

form.addEventListener("submit", async function (e) { //evento para envio de formulário
    e.preventDefault();  //Impede o reload da página

    const indexEdicao = editIndexInput.value;

    const dadosCarro = {
        placa: inputPlaca.value, // Exemplo fixo por enquanto
        ano: Number(inputAno.value),
        ultimaInspecao: new Date(inputInspecao.value).toISOString(),
        vendido: inputVendido.checked,
        modeloID: Number(inputModelo.value) // Por enquanto fixo para bater com seu banco
    };

    try {
        let url = `${API_URL}/carros`;
        let metodo = 'POST';

        if (indexEdicao !== "") {
            const veiculoParaEditar = veiculosFiltrados[indexEdicao];
            url = `${API_URL}/carros/${veiculoParaEditar.id}`;
            metodo = 'PUT';
        }

        const resposta = await fetch(url, {
            method: metodo,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dadosCarro)
        });

        if (resposta.ok) {
            form.reset();
            editIndexInput.value = "";
            await carregarVeiculos();
        } else {
            const erroMsg = await resposta.text();
            alert("Erro na API: " + erroMsg);
        }
    } catch (erro) {
        console.error("Erro ao salvar:", erro);
    }
});


//LLIMPAR A BD - REINICIALIZAR A BD

/*document.getElementById("limparLS").addEventListener("click", () => {  //limpa tudo que tem na tabela e devolve a tabela vazia
    const confirmar = confirm("Tens certeza que queres excluir todos os registros existentes de veículos?");
    if (!confirmar) return; 

    veiculos = []; //devolve o array vazio, logo não preenche nada na tabela
    guardar();
    atualizarFiltros();
    render();
});

document.getElementById("carregarLS").addEventListener("click", () => { 
    localStorage.removeItem("veiculos");
    carregarVeiculos();
    atualizarFiltros();
    render();
});*/

carregarVeiculos();
carregarSelectMarcas();
atualizarFiltros();
render();
