CREATE TABLE [Identity].[AspNetUserTokens] (
    [UserId]            UNIQUEIDENTIFIER NOT NULL,
    [LoginProvider]     nvarchar(450) NOT NULL,
    [Name]              nvarchar(450) NOT NULL,
    [Value]             nvarchar(max) NULL,

    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);