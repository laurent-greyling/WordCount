﻿CREATE TABLE [Tv].[Cast]
(
	[Id] INT NOT NULL,
	[ShowId] INT NOT NULL,
	[Name] NVARCHAR(250) NOT NULL,
	[Birthday] DATE NOT NULL,
	CONSTRAINT [PK_Cast] PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT [FK_Cast_Shows] FOREIGN KEY ([ShowId]) REFERENCES [Tv].[Shows]([Id]) ON DELETE CASCADE
)