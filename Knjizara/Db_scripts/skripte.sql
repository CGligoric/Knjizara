CREATE TABLE knjiga (
	id INT PRIMARY KEY,
	naslov NVARCHAR(50),
	cena INT,
	zanr_id INT FOREIGN KEY (zanr_id) REFERENCES zanr(id)
);

CREATE TABLE zanr (
	id INT PRIMARY KEY,
	naziv NVARCHAR(50)
);

INSERT INTO zanr VALUES 
	(1,'Triler'),
	(2,'Roman'),
	(3,'Esej');

INSERT INTO knjiga VALUES
	(1,'To',800,1),
	(2,'Made in America',1200,3),
	(3,'Rat i mir',2000,2),
	(4,'Covek po imenu Uve',600,2);