CREATE TABLE [Identity].[AspNetUserRoles] (
    [UserId]     uniqueidentifier NOT NULL,
    [RoleId]     uniqueidentifier NOT NULL,

    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [Identity].[AspNetUserRoles] ([RoleId]);