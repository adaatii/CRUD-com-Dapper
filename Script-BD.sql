CREATE DATABASE Livros;

USE Livros;

CREATE TABLE Livros(
	id int primary key not null auto_increment,
	titulo varchar(50),
	autor varchar(50)	
);

