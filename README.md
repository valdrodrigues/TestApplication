# Web API para calcular o desconto do INSS sobre o salário
WEB API REST | .Net Core 3.1 | DDD | TDD | Dependency Injection | NUnit | Moq | Swagger | MongoDB

Para este projeto foi criada uma data base no MongoDB chamada "INSS", com uma coleção chamada "TabelaDesconto". Na coleção foram inseridos manualmente os seguintes dados:

db.TabelaDesconto.insert( {
Ano: NumberInt(2011), 
Teto: 405.86,
FaixaSalarial: [
 { SalarioInicial: 1006.90, SalarioFinal: 1006.90, Aliquota: 8 }, 
 { SalarioInicial: 1006.91, SalarioFinal: 1844.83, Aliquota: 9 }, 
 { SalarioInicial: 1844.84, SalarioFinal: 3689.66, Aliquota: 11 }
 ] } );

db.TabelaDesconto.insert( {
Ano: NumberInt(2012), 
Teto: 500.00,
FaixaSalarial: [
 { SalarioInicial: 1000.00, SalarioFinal: 1000.00, Aliquota: 7 }, 
 { SalarioInicial: 1000.01, SalarioFinal: 1500.00, Aliquota: 8 }, 
 { SalarioInicial: 1500.01, SalarioFinal: 3000.00, Aliquota: 9 },
 { SalarioInicial: 3000.01, SalarioFinal: 4000.00, Aliquota: 11 }
 ] } );

Para executar o projeto, é necessário realizar a instalação do MongoDB e realizar os passos mencionados acima.
