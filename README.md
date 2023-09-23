# ClimaTempo API

A API ClimaTempo permite verificar o clima em determinadas cidades ou aeroportos. Este guia fornece instruções sobre como configurar e usar a API.

## Configuração Inicial

1. Modifique a conexão do servidor no arquivo `appsettings.json` localizado em `ClimaLocal\ClimaLocal.WebApi\appsettings.json` para apontar para seu próprio servidor do SQL Server. Importante destacar que por ser um container docker ao testar local, é necessário colocar como servidor IP.

2. Para criar as tabelas no banco de dados, execute as migrações do Entity Framework. Abra o Terminal no Visual Studio (Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes) e execute o comando `Update-Database` após configurar a conexão. Isso criará as tabelas no seu banco de dados.

3. Na pasta raiz do projeto, você encontrará um arquivo chamado `Build.bat`. Execute este arquivo para iniciar o Docker e provisionar a imagem necessária. Após a conclusão deste processo, a API estará totalmente operacional.

4. Agora basta acessar http://localhost:8000/swagger/index.html

## Uso da API

Após configurar a API, você pode começar a usá-la para obter informações climáticas. Aqui estão os endpoints disponíveis:

1. `/api/Clima/retorna-cidades`: Este endpoint retorna as cidades disponíveis e seus respectivos códigos. Isso pode ser útil para obter o código da cidade que você deseja usar nos próximos endpoints. (Ele não foi solicitado, mas entendi que seria importante para obtenção do código do cliente)

2. `/api/Clima/retorna-clima-cidade`: Use este endpoint para buscar informações climáticas de uma cidade específica, informando seu código.

3. `/api/Clima/retorna-clima-aeroporto`: Este endpoint permite buscar informações climáticas de um aeroporto específico, fornecendo seu código. Você pode encontrar uma lista de códigos ICAO de aeroportos [aqui](https://pt.wikipedia.org/wiki/Lista_de_aeroportos_do_Brasil_por_c%C3%B3digo_aeroportu%C3%A1rio_ICAO).


Exemplo de uso para teste: `/api/Clima/retorna-clima-aeroporto?codigoAeroporto=SBBH`.

## Observações Importantes

- O sistema foi desenvolvido em C# (.Net 6) e utiliza o Entity Framework com o SQL Server como sistema de gerenciamento de banco de dados. Porém Também possuo experiência com o uso do Dapper.

- O Docker é usado para tornar o sistema totalmente operacional e oferece a vantagem de reinicialização automática, garantindo a disponibilidade contínua do sistema, mesmo após reinicializações do servidor.

- Certifique-se de verificar a documentação da API para obter informações detalhadas sobre as solicitações e respostas esperadas.

## Contato

Se você tiver alguma dúvida ou encontrar problemas ao usar a API ClimaTempo, entre em contato conosco em [bruno.alexandre.tomm@gmail.com] para obter suporte.

Esperamos que esta documentação ajude você a configurar e usar a API ClimaTempo com facilidade.


