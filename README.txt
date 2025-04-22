Passo a passo para rodar a aplicação e efetuar os testes integrados:

- Rodar os testes unitários antes de executar a aplicação


1- A Conexão com banco de dados SQL Server
  Ajustar ConnectionString para conectar a uma instância do SQL Server
  
2- A instância do SQL Server não pode ter um banco com o nome "DeveloperEvaluation"
   - Ao rodar a aplicação pela primeira vez, o banco e as tabelas serão criados

3- Criar um usuário na api "api/users" e guardar o email e a senha para gerar o token

4- Utilizar a api "api/auth" para gerar o token e copiar para a area de transferencia

5- Clicar no botão "Authorize" para adicionar no header o token que foi gerado

6- Testar os CRUD's de cada domínio

7- Criar as vendas que estão nos payloads abaixo

8- Consultar tabela Products para verificar as baixas de estoque

9- Para acompanhar os eventos gerados, segue abaixo os dados de acesso:
https://console.hivemq.cloud/clusters/8b7478c7cd2e4193827e89f0aa0e45d2/web-client
username: michelmedeiros10
password: 4Mb3v@2025




Seguem abaixo os payloads utilizados para testes:


CreateUser
{
  "username": "user",
  "password": "P4ssword!",
  "phone": "+5514996611545",
  "email": "user@gmail.com",
  "status": 1,
  "role": 1,
  "name": {
    "firstName": "User",
    "lastName": "Test"
  },
  "address": {
    "city": "Ourinhos",
    "street": "Rua minha",
    "number": 382,
    "zipcode": "19900",
    "geoLatitude": "-1,266588",
    "geoLongitude": "-1,25698"
  }
}


Authenticate
{
  "email": "user@gmail.com",
  "password": "P4ssword!"
}


UpdateUser
{
  "id": "218d4588-be6d-4fe9-8278-bf3a43914710",
  "username": "user-upd",
  "password": "P4ssword!!",
  "phone": "+5514996611546",
  "email": "user-upd@gmail.com",
  "status": 1,
  "role": 2,
  "name": {
    "firstName": "User-upd",
    "lastName": "Test-upd"
  },
  "address": {
    "city": "Ourinhos-SP",
    "street": "Rua minha upd",
    "number": 383,
    "zipcode": "19915-581",
    "geoLatitude": "-1,266581",
    "geoLongitude": "-1,25692"
  }
}



CreateProduct
{
  "title": "Product Test 3",
  "price": 300,
  "description": "Product Test 3",
  "category": "Category Test 3",
  "image": "http://img-link3",
  "rating": {
    "rate": 7.3,
    "count": 30
  }
}


UpdateProduct
{
  "id": "c0c58759-6c68-4a3a-e89a-08dd7e8829a8",
  "title": "Product Test 1-up",
  "price": 100,
  "description": "Product Test 1-up",
  "category": "Category Test 1-up",
  "image": "http://img-link-up",
  "rating": {
    "rate": 5.9,
    "count": 100
  }
}



CreateSale-NoDiscount
{
  "saleNumber": 1,
  "saleDate": "2025-04-18T05:51:49.070Z",
  "customerId": "3e2525ec-dcf9-4b88-810b-43edf2ff3928",
  "amount": 100,
  "branch": "Ourinhos-SP",
  "products": [
    {
      "productId": "c0c58759-6c68-4a3a-e89a-08dd7e8829a8",
      "price": 100,
      "quantity": 1,
      "discount": 0,
      "totalAmount": 100
    }
  ]
}

CreateSale-WithDiscount
{
  "saleNumber": 2,
  "saleDate": "2025-04-18T06:52:26.010Z",
  "customerId": "3e2525ec-dcf9-4b88-810b-43edf2ff3928",
  "amount": 2500,
  "branch": "Ourinhos-SP",
  "products": [
    {
      "productId": "c0c58759-6c68-4a3a-e89a-08dd7e8829a8",
      "price": 100,
      "quantity": 2,
      "discount": 0,
      "totalAmount": 200
    },
	{
      "productId": "c0c58759-6c68-4a3a-e89a-08dd7e8829a8",
      "price": 100,
      "quantity": 3,
      "discount": 0,
      "totalAmount": 300
    },
	{
      "productId": "4bda22c9-a344-49bf-15df-08dd7eb73725",
      "price": 200,
      "quantity": 5,
      "discount": 0,
      "totalAmount": 1000
    },
	{
      "productId": "4bda22c9-a344-49bf-15df-08dd7eb73725",
      "price": 200,
      "quantity": 5,
      "discount": 0,
      "totalAmount": 1000
    }	
  ]
}




