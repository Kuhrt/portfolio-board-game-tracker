CREATE TABLE [Identity].[AspNetRoles] (
    [Id]                    uniqueidentifier NOT NULL,
    [Name]                  nvarchar(256) NULL,
    [NormalizedName]        nvarchar(256) NULL,
    [ConcurrencyStamp]      nvarchar(max) NULL,

	CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetRoles_NormalizedName] ON [Identity].[AspNetRoles]([NormalizedName]) WHERE ([NormalizedName] IS NOT NULL)