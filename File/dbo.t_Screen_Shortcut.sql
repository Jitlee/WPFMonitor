CREATE TABLE [dbo].[t_Screen_Shortcut] (
    [Id]                INT            NOT NULL IDENTITY,
    [ScreenId]          INT            NOT NULL,
    [ScreenName]        NVARCHAR (50)  NULL,
    [ScreenDescription] NVARCHAR (200) NULL,
    [ScreenImage]       IMAGE          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

