CREATE TABLE [Common].[ConfigurationSetting]
(
	  [ConfigurationSettingId] [BIGINT] IDENTITY(1,1)  NOT NULL,
    [Key] NVARCHAR(255) NOT NULL, 
    [Value] NVARCHAR(MAX) NOT NULL, 
    [CreatedBy] UNIQUEIDENTIFIER NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL, 
    [UpdatedBy] UNIQUEIDENTIFIER NULL, 
    [UpdatedOn] DATETIME2 NULL, 	
	  CONSTRAINT [PK_ConfigurationSettingId] PRIMARY KEY ([ConfigurationSettingId]),
)
