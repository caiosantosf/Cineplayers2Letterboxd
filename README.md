# Um scraper para usuários do Cineplayers.com

Consolida em um banco de dados mysql e fornece através de uma Rest API os seus dados pessoais no cineplayers (notas para filmes e filmes marcados "Pra Depois")

Este projeto não usa dados privados do Cineplayers.com, não acessa o seu banco de dados diretamente, e não tem nenhuma relação com os criadores do site.

Os dados são acessados através de scraping (leitura do HTML público).

## Como executar localmente

Você precisa ter o node na versão 12 e o docker atualizado instalados!

```console
npm install
docker-compose up -d
npm run dev
```

![Janela Indiscreta (Alfred Hitchcock, 1954](readme.jpg)
