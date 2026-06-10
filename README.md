# Sistema de Gerenciamento de Revenda - GraGasJM

Sistema de gerenciamento de vendas e compras para revenda familiar desenvolvido em Blazor Server com .NET 8.

## 🚀 Tecnologias

- **Blazor Server** - Framework web para construção de aplicações interativas
- **.NET 8** - Plataforma de desenvolvimento
- **Entity Framework Core** - ORM para acesso a dados
- **SQLite** - Banco de dados leve e eficiente

## 📦 Produtos Gerenciados

- Botijão de Gás P13
- Galão de Água Mineral 20L
- Saco de Carvão

## 🎯 Funcionalidades

- ✅ Gerenciamento de produtos (CRUD completo)
- ✅ Controle de estoque
- ✅ Registro de vendas
- ✅ Registro de compras
- ✅ Alertas de estoque baixo
- ✅ Cálculo automático de totais
- ✅ Múltiplas formas de pagamento

## 🏗️ Estrutura do Projeto

```
GraGasJM/
├── Data/
│   └── ApplicationDbContext.cs    # Contexto do banco de dados
├── Models/
│   ├── Produto.cs                 # Modelo de produto
│   ├── Venda.cs                   # Modelo de venda
│   ├── ItemVenda.cs               # Item da venda
│   ├── Compra.cs                  # Modelo de compra
│   └── ItemCompra.cs              # Item da compra
├── Pages/
│   ├── Index.razor                # Dashboard principal
│   ├── Produtos.razor             # Lista de produtos
│   ├── ProdutoForm.razor          # Formulário de produtos
│   ├── Vendas.razor               # Lista de vendas
│   ├── VendaForm.razor            # Formulário de vendas
│   ├── Compras.razor              # Lista de compras
│   └── CompraForm.razor           # Formulário de compras
└── Shared/
    └── NavMenu.razor              # Menu de navegação
```

## 🚀 Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado

### Passos

1. **Clone ou navegue até o diretório do projeto**

2. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

3. **Execute o projeto**
   ```bash
   dotnet run --project GRAGAZBlazor/GraGasJM.csproj
   ```

4. **Acesse no navegador**
   - A aplicação estará disponível em: `https://localhost:5001` ou `http://localhost:5000`
   - O banco de dados SQLite será criado automaticamente na primeira execução

## 💾 Banco de Dados

O banco de dados SQLite (`gragas.db`) será criado automaticamente no diretório raiz do projeto na primeira execução. Ele já vem pré-populado com os três produtos principais:

- Botijão de Gás P13 - R$ 115,00
- Galão de Água Mineral 20L - R$ 18,00
- Saco de Carvão - R$ 25,00

## 📊 Funcionalidades Detalhadas

### Gerenciamento de Produtos
- Cadastro de novos produtos
- Edição de produtos existentes
- Controle de preços de venda e compra
- Definição de estoque mínimo
- Ativação/desativação de produtos

### Controle de Vendas
- Registro de vendas com múltiplos itens
- Atualização automática do estoque
- Suporte a diferentes formas de pagamento
- Registro de cliente (opcional)

### Controle de Compras
- Registro de compras de fornecedores
- Entrada automática no estoque
- Controle de custos
- Diferentes formas de pagamento

### Dashboard
- Visão geral dos produtos, vendas e compras
- Alertas de produtos com estoque baixo
- Estatísticas rápidas

## 🔧 Configurações

As configurações do banco de dados podem ser alteradas no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=gragas.db"
  }
}
```

## 📝 Notas

- Este é um sistema para uso interno de uma revenda familiar
- O banco de dados SQLite é ideal para operações de pequena escala
- Todos os valores monetários são formatados em Real (R$)
- O sistema mantém o histórico completo de vendas e compras

## 🤝 Suporte

Para dúvidas ou problemas, entre em contato com o desenvolvedor.

---

**GraGasJM** - Sistema de Gerenciamento de Revenda © 2026
