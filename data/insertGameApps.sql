/*
	@Description: SQL for creating database table and stored procedure
    @Author: Li Liu
    @Date: 04/22/2019
*/
DROP TABLE IF EXISTS GameApps
CREATE TABLE GameApps(
		gameId		int			primary key			identity,
		name		varchar(max)		not null,
		genre		varchar(max)		not null,
		developer	varchar(max)		not null,
		platform	varchar(max)		not null,
		rating		float				not null,
		description varchar(max)		not null
		)

/*
	@Description: GameApps.txt was created by Cooper
    @Author: Cooper Brown
    @Date: 04/12/2019
*/
--Before run the code, please change "FROM 'C:\Users\14260\Desktop\data\GameApps.txt'" to new address where your GameApps.txt file is.
BULK INSERT GameApps
FROM 'C:\Users\14260\Desktop\data\GameApps.txt'
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

CREATE PROCEDURE spSearchByName
		@name Varchar(max)
AS
SELECT *
FROM GameApps g
WHERE g.name = @name
GO

CREATE PROCEDURE spSearchByGenre
		@genre  Varchar(max)
AS
SELECT *
FROM GameApps g
WHERE g.genre = @genre
GO

CREATE PROCEDURE spSortByDeveloper
AS
SELECT *
FROM GameApps
ORDER BY developer
GO

CREATE PROCEDURE spSortByGenre
AS
SELECT *
FROM GameApps
ORDER BY genre
GO

CREATE PROCEDURE spSortByName
AS
SELECT *
FROM GameApps
ORDER BY name
GO

CREATE PROCEDURE spSortByRating
AS
SELECT *
FROM GameApps
ORDER BY rating DESC
GO