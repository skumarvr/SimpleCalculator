CREATE TABLE [dbo].[Diagnostics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Operation] NCHAR(10) NOT NULL, 
    [Result] INT NOT NULL
)
