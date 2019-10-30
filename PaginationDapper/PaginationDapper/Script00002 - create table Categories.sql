create table Categories
(
	[Id] uniqueidentifier primary key identity(1,1),
	[CreationDate] datetime null,
	[DeletedDate] datetime null,
	[Name] nvarchar(MAX) null,
	[ImagePath] nvarchar(MAX) null
)