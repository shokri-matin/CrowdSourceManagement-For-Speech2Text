USE [master]
GO

CREATE DATABASE [SegamDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SegamDB', FILENAME = N'C:\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SegamDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SegamDB_log', FILENAME = N'C:\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\SegamDB_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SegamDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SegamDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SegamDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SegamDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SegamDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SegamDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SegamDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SegamDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SegamDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SegamDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SegamDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SegamDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SegamDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SegamDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SegamDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SegamDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SegamDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SegamDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SegamDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SegamDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SegamDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SegamDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SegamDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SegamDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SegamDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SegamDB] SET  MULTI_USER 
GO
ALTER DATABASE [SegamDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SegamDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SegamDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SegamDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SegamDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SegamDB', N'ON'
GO
ALTER DATABASE [SegamDB] SET QUERY_STORE = OFF
GO
USE [SegamDB]
GO
/****** Object:  Table [dbo].[AllocatedFiles]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllocatedFiles](
	[FileID] [uniqueidentifier] NOT NULL,
	[TaskID] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[IsSubmited] [bit] NOT NULL,
	[SubmitedTime] [datetime] NULL,
	[IsSupervised] [bit] NULL,
	[Gender] [int] NULL,
 CONSTRAINT [PK_dbo.AllocatedFiles] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC,
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExceptionsLogging]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExceptionsLogging](
	[LogId] [uniqueidentifier] NOT NULL,
	[PersonID] [int] NOT NULL,
	[ControllerName] [nvarchar](max) NULL,
	[ActionName] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[InnerException] [nvarchar](max) NULL,
	[LogDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ExceptionsLogging] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_dbo.Groups] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](100) NOT NULL,
	[PersonEmail] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CreatorId] [int] NULL,
 CONSTRAINT [PK_dbo.Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonActivityLog]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonActivityLog](
	[ID] [uniqueidentifier] NOT NULL,
	[PersonID] [int] NOT NULL,
	[LoginTime] [datetime] NOT NULL,
	[ActivityStatus] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.PersonActivityLog] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpeechFiles]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpeechFiles](
	[FileID] [uniqueidentifier] NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[GroupID] [int] NOT NULL,
	[FileType] [nvarchar](max) NOT NULL,
	[FileDuration] [float] NOT NULL,
	[SuggestedText] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatorId] [int] NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[SequenceID] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.SpeechFiles] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 6/18/2021 3:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskID] [uniqueidentifier] NOT NULL,
	[PersonID] [int] NOT NULL,
	[StartTaskDate] [datetime] NOT NULL,
	[EndTaskDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Task] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_FileID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_FileID] ON [dbo].[AllocatedFiles]
(
	[FileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_TaskID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_TaskID] ON [dbo].[AllocatedFiles]
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_PersonID] ON [dbo].[ExceptionsLogging]
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatorId]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_CreatorId] ON [dbo].[Person]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RoleID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleID] ON [dbo].[Person]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_PersonID] ON [dbo].[PersonActivityLog]
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CreatorId]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_CreatorId] ON [dbo].[SpeechFiles]
(
	[CreatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_GroupID] ON [dbo].[SpeechFiles]
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersonID]    Script Date: 6/18/2021 3:04:19 AM ******/
CREATE NONCLUSTERED INDEX [IX_PersonID] ON [dbo].[Task]
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ExceptionsLogging] ADD  DEFAULT (newsequentialid()) FOR [LogId]
GO
ALTER TABLE [dbo].[ExceptionsLogging] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [LogDate]
GO
ALTER TABLE [dbo].[PersonActivityLog] ADD  DEFAULT (newsequentialid()) FOR [ID]
GO
ALTER TABLE [dbo].[SpeechFiles] ADD  DEFAULT ((0)) FOR [CreatorId]
GO
ALTER TABLE [dbo].[SpeechFiles] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [PublishedDate]
GO
ALTER TABLE [dbo].[SpeechFiles] ADD  DEFAULT ('') FOR [SequenceID]
GO
ALTER TABLE [dbo].[Task] ADD  DEFAULT (newsequentialid()) FOR [TaskID]
GO
ALTER TABLE [dbo].[AllocatedFiles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AllocatedFiles_dbo.SpeechFiles_FileID] FOREIGN KEY([FileID])
REFERENCES [dbo].[SpeechFiles] ([FileID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AllocatedFiles] CHECK CONSTRAINT [FK_dbo.AllocatedFiles_dbo.SpeechFiles_FileID]
GO
ALTER TABLE [dbo].[AllocatedFiles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AllocatedFiles_dbo.Task_TaskID] FOREIGN KEY([TaskID])
REFERENCES [dbo].[Task] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AllocatedFiles] CHECK CONSTRAINT [FK_dbo.AllocatedFiles_dbo.Task_TaskID]
GO
ALTER TABLE [dbo].[ExceptionsLogging]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ExceptionsLogging_dbo.Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ExceptionsLogging] CHECK CONSTRAINT [FK_dbo.ExceptionsLogging_dbo.Person_PersonID]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Person_dbo.Person_AdminPerson_PersonID] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_dbo.Person_dbo.Person_AdminPerson_PersonID]
GO
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Person_dbo.Roles_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_dbo.Person_dbo.Roles_RoleID]
GO
ALTER TABLE [dbo].[PersonActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PersonActivityLog_dbo.Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PersonActivityLog] CHECK CONSTRAINT [FK_dbo.PersonActivityLog_dbo.Person_PersonID]
GO
ALTER TABLE [dbo].[SpeechFiles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SpeechFiles_dbo.Groups_GroupID] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Groups] ([GroupID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SpeechFiles] CHECK CONSTRAINT [FK_dbo.SpeechFiles_dbo.Groups_GroupID]
GO
ALTER TABLE [dbo].[SpeechFiles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SpeechFiles_dbo.Person_CreatorId] FOREIGN KEY([CreatorId])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[SpeechFiles] CHECK CONSTRAINT [FK_dbo.SpeechFiles_dbo.Person_CreatorId]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Task_dbo.Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_dbo.Task_dbo.Person_PersonID]
GO
USE [master]
GO
ALTER DATABASE [SegamDB] SET  READ_WRITE 
GO
