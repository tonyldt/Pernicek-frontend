/* Drop Tables */
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[lego].[Category]') AND OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [lego].[Category]
GO

/* Create Tables */
CREATE TABLE [lego].[Category]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[MainColor] [nvarchar](50) NULL,
	[BackgroundColor] [nvarchar](50) NULL,
	[MainImage] [nvarchar](max) NULL,
	[LogoImage] [nvarchar](max) NULL,
	[TopBackgroundImage] [nvarchar](max) NULL,
	[BottomBackgroundImage] [nvarchar](max) NULL,
	[SEOName] [nvarchar](200) NOT NULL
)
GO

/* Create Primary Keys, Indexes, Uniques, Checks */
ALTER TABLE [lego].[Category] 
 ADD CONSTRAINT [PK_Category]
	PRIMARY KEY CLUSTERED ([Id])
GO


/* Default values */
ALTER TABLE [lego].[Category] ADD  DEFAULT ('ASDF') FOR [SEOName]
GO
