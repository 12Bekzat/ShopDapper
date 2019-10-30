create table Items 
(
	[Id] int primary key identity(1,1),
	[CreationDate] datetime null,
	[DeletedDate] datetime null,
	[Name] nvarchar(MAX) null,
	[ImagePath] nvarchar(MAX) null,
	[Price] int null,
	[Description] nvarchar(MAX) null,
	[CategoryId] int null
)