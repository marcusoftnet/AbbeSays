
/****** Object:  Table [dbo].[Tags]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tags](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Tag] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Tags] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Parents]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Parents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Email] [varchar](200) NULL,
	[UserName] [varchar](200) NOT NULL,
	[Password] [varchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Kids]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Kids](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Bio] [text] NULL,
	[Picture] [image] NULL,
 CONSTRAINT [PK_Kids] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KidId] [int] NOT NULL,
	[Quote] [nvarchar](2000) NOT NULL,
	[SaidAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteTag]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteTag](
	[QuoteId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_QuoteTag] PRIMARY KEY CLUSTERED 
(
	[QuoteId] ASC,
	[TagId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/27/2011 22:22:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Comment] [nchar](2000) NOT NULL,
	[Commenter] [varchar](200) NOT NULL,
	[CommentedAt] [datetime] NOT NULL,
	[QuoteId] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Comments_Quotes]    Script Date: 12/27/2011 22:22:12 ******/
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Quotes] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quotes] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Quotes]
GO
/****** Object:  ForeignKey [FK_Kids_Users]    Script Date: 12/27/2011 22:22:12 ******/
ALTER TABLE [dbo].[Kids]  WITH CHECK ADD  CONSTRAINT [FK_Kids_Users] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Parents] ([Id])
GO
ALTER TABLE [dbo].[Kids] CHECK CONSTRAINT [FK_Kids_Users]
GO
/****** Object:  ForeignKey [FK_Quotes_Kids]    Script Date: 12/27/2011 22:22:12 ******/
ALTER TABLE [dbo].[Quotes]  WITH CHECK ADD  CONSTRAINT [FK_Quotes_Kids] FOREIGN KEY([KidId])
REFERENCES [dbo].[Kids] ([Id])
GO
ALTER TABLE [dbo].[Quotes] CHECK CONSTRAINT [FK_Quotes_Kids]
GO
/****** Object:  ForeignKey [FK_QuoteTag_Quotes]    Script Date: 12/27/2011 22:22:12 ******/
ALTER TABLE [dbo].[QuoteTag]  WITH CHECK ADD  CONSTRAINT [FK_QuoteTag_Quotes] FOREIGN KEY([QuoteId])
REFERENCES [dbo].[Quotes] ([Id])
GO
ALTER TABLE [dbo].[QuoteTag] CHECK CONSTRAINT [FK_QuoteTag_Quotes]
GO
/****** Object:  ForeignKey [FK_QuoteTag_Tags]    Script Date: 12/27/2011 22:22:12 ******/
ALTER TABLE [dbo].[QuoteTag]  WITH CHECK ADD  CONSTRAINT [FK_QuoteTag_Tags] FOREIGN KEY([TagId])
REFERENCES [dbo].[Tags] ([Id])
GO
ALTER TABLE [dbo].[QuoteTag] CHECK CONSTRAINT [FK_QuoteTag_Tags]
GO
