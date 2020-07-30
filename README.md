# airport-finder

Aplicação baseada numa aplicação ensinada no curso da Alura de .Net e MongoDb. 
Fiz a minha implementação com uma API em Net Core e o Front em Angular, com o objetivo de listar os aeroportos próximos de alguma cidade dos EUA.

### Ferramentas usadas

* Net core
* MongoDB Driver (https://docs.mongodb.com/drivers/csharp/)
* Angular 10
* Angular Material
* Angular Maps (https://www.npmjs.com/package/@angular/google-maps)

### Dados para usar de exemplo

1. Local: Miami
2. Distancia: 185

### Criando o container do MongoDB no Docker

1. docker run --name mongo -p 27017:27017 -e MONGO_INITDB_ROOT_USERNAME=mongo -e MONGO_INITDB_ROOT_PASSWORD=1qaz2wsx -v /home/jonathas/Databases/MongoDB:/data/db -d mongo

### Copiando o BD geo para container

1. Na pasta data, descompactar a pasta base-mongodb-localizacoes-aeroportos-para-importar.zip.
2. Abrir o terminal e rodar: docker cp geo/ mongo:/geo

### Restaurando o BD geo no container Mongo

1. docker exec mongo mongorestore --host 127.0.0.1:27017 --username mongo --password 1qaz2wsx --authenticationDatabase admin -d geo geo

<img alt="Demonstração da aplicação" src="https://github.com/jonathastassi/airport-finder/blob/master/data/image.png" width="800">

<img alt="Demonstração da aplicação" src="https://github.com/jonathastassi/airport-finder/blob/master/data/image_menu.png" width="800">
