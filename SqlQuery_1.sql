CREATE TABLE GameApps(
		gameId		int			primary key			identity,
		name		varchar(max)		not null,
		genre		varchar(max)		not null,
		developer	varchar(max)		not null,
		platform	varchar(max)		not null,
		rating		float				not null
		)

BULK INSERT GameApps
FROM 'C:\Users\Jack\Downloads\data\data\GameApps.txt'
WITH  (
		FIELDTERMINATOR = ',',
		ROWTERMINATOR = '\n',
		FIRSTROW = 2,
		keepIDENTITY
)
GO

CREATE PROCEDURE spShowAllGames
AS
	SELECT * FROM GameApps
GO

select * from GameApps