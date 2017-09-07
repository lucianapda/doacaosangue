CREATE TABLE hemocentros(
	id INTEGER PRIMARY KEY IDENTITY(1, 1),
	nome VARCHAR NOT NULL,
	descricao TEXT,
	estado VARCHAR(100),
	cidade VARCHAR(100),
	numero INTEGER(100),
	cep VARCHAR,
	complemento TEXT
);

CREATE TABLE doadores(
	id INTEGER PRIMARY KEY IDENTITY(1, 1),
	nome VARCHAR NOT NULL,
	sobrenome VARCHAR NOT NULL,
	data_nascimento DATE NOT NULL,
	tipo_sanguineo VARCHAR NOT NULL,
	peso FLOAT,
	altura FLOAT

);

CREATE TABLE perguntas(
	id INTEGER PRIMARY KEY IDENTITY(1, 1),
	nome VARCHAR NOT NULL,
	resposta BIT
);

CREATE TABLE doacoes(
	id INTEGER PRIMARY KEY IDENTITY(1, 1),

	id_doador INTEGER NOT NULL,
	FOREIGN KEY(id_doador) REFERENCES doadores(id),

	aceitavel BIT,
	atendente VARCHAR,
	litros FLOAT,
	data DATE
);

CREATE TABLE doacoes_perguntas(
	id INTEGER PRIMARY KEY IDENTITY(1, 1),
	id_doacao INTEGER NOT NULL,
	id_pergunta INTEGER NOT NULL,
	FOREIGN KEY(id_doacao) REFERENCES doacoes(id),
	FOREIGN KEY(id_pergunta) REFERENCES perguntas(id), 

	resposta BIT NOT NULL
);

CREATE TABLE hemocentros_doadores(

	id INTEGER PRIMARY KEY IDENTITY(1, 1),

	id_doador INTEGER NOT NULL,
	id_hemocentro INTEGER NOT NULL,
	FOREIGN KEY(id_doador) REFERENCES doadores(id),
	FOREIGN KEY(id_hemocentro) REFERENCES hemocentros(id)
);

