CREATE TABLE [dbo].[Knjiga] (
	[id] INT IDENTITY(1,1) PRIMARY KEY,
	[naslov] NVARCHAR(50),
	[cena] INT,
	[zanr_id] INT FOREIGN KEY (zanr_id) REFERENCES dbo.zanr(id)
);