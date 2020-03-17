/* CREATE TABLE [Categories]
(
  [id] INT IDENTITY PRIMARY KEY,
  [name] VARCHAR(25)
);

CREATE TABLE [Books]
(
  [id] INT IDENTITY PRIMARY KEY,
  [name] VARCHAR(25),
  [pages] INT,
  [year_press] INT,
  [category_id] INT REFERENCES [Categories]([id])
); */

/*INSERT INTO [dbo].[Categories] ([name]) VALUES ('cat1'), ('cat2');

INSERT INTO [dbo].[Books] ([name], [pages], [year_press], [category_id])
  VALUES ('book1', 300, 2019, 1), ('book2', 350, 2018, 1), ('book3', 200, 2020, 2);*/

/*CREATE PROCEDURE [dbo].[sp_getXML]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id] "@id"
	,[Name] "name"
	, (
		SELECT [id] "@id"
      ,[Name] "name"
			,[Pages] "pages"
			,[Year_Press] "YearPress"
		FROM [Books] AS B
		WHERE B.[category_id] = C.[Id]
		FOR XML PATH('Book'), TYPE
	) AS [Books]
	FROM [Categories] AS C
	FOR XML PATH('Category'), ROOT('Categories')
END;*/

-- EXEC [dbo].[sp_getXML];

CREATE PROCEDURE [dbo].[sp_getJSON]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id] AS 'id'
	,[Name] AS 'name'
	, (
		SELECT [id] AS 'id'
      ,[Name] AS 'name'
			,[Pages] AS 'pages'
			,[Year_Press] AS 'year_press'
		FROM [Books] AS B
		WHERE B.[category_id] = C.[Id]
		FOR JSON PATH
	) AS Book
	FROM [Categories] AS C
	FOR JSON PATH, ROOT('Categories')
END;
