﻿USE [master]
GO
/****** Object:  Database [Notes-MarketPlace]    Script Date: 11-02-2021 18:07:37 ******/
CREATE DATABASE [Notes-MarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Notes-MarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Notes-MarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Notes-MarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Notes-MarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Notes-MarketPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Notes-MarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Notes-MarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Notes-MarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Notes-MarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Notes-MarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Notes-MarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [Notes-MarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [Notes-MarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Notes-MarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Notes-MarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Notes-MarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Notes-MarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Notes-MarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Notes-MarketPlace', N'ON'
GO
ALTER DATABASE [Notes-MarketPlace] SET QUERY_STORE = OFF
GO
USE [Notes-MarketPlace]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ID] [int] NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[EmailID] [varchar](50) NOT NULL,
	[Subject] [varchar](100) NOT NULL,
	[Comments/Questions] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ContactUs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [int] NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadedNotes]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadedNotes](
	[ID] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Buyer] [varchar](100) NOT NULL,
	[Seller] [varchar](100) NOT NULL,
	[SellType] [varchar](50) NULL,
	[Price] [varchar](25) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_DownloadedNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManageSystemConfiguration]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageSystemConfiguration](
	[ID] [int] NOT NULL,
	[SupportEmail] [varchar](100) NOT NULL,
	[SupportContactNumber] [varchar](15) NOT NULL,
	[EmailAddress] [varchar](100) NOT NULL,
	[FacebookURL] [varchar](100) NULL,
	[TwitterURL] [varchar](100) NULL,
	[LinkedInURL] [varchar](100) NULL,
	[DefaultNoteDisplayImage] [varbinary](max) NOT NULL,
	[DefaultProfilePicture] [varbinary](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_ManageSystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteDetails](
	[ID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[DisplayPicture] [varbinary](max) NULL,
	[NumberOfPages] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[University] [varchar](200) NULL,
	[InstitutionName] [varchar](200) NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor/Lecturer] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellPrice] [decimal](18, 0) NOT NULL,
	[NotePreview] [varbinary](max) NOT NULL,
	[Action] [varchar](50) NULL,
	[Remark] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_NoteDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteReview]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReview](
	[ID] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[Rate] [int] NOT NULL,
	[Comment] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_NoteReview] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesAttachments]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesAttachments](
	[ID] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[FileName] [varchar](100) NOT NULL,
	[FilePath] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SellerNotesAttachments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpamReports]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpamReports](
	[ID] [int] NOT NULL,
	[ReportedBy] [int] NOT NULL,
	[NoteID] [int] NOT NULL,
	[Remark] [varchar](200) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SpamReports] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Types]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Types](
	[ID] [int] NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Types] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [varchar](6) NULL,
	[PhoneNumber-CountryCode] [varchar](4) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varbinary](max) NULL,
	[AddressLine-1] [varchar](100) NOT NULL,
	[AddressLine-2] [varchar](100) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[ZipCode] [varchar](50) NOT NULL,
	[Country] [varchar](50) NOT NULL,
	[University] [varchar](100) NULL,
	[College] [varchar](100) NULL,
	[SubmittedDate] [datetime] NULL,
	[SubmittedBy] [int] NULL,
	[ModifieDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11-02-2021 18:07:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[Password] [varchar](24) NOT NULL,
	[PhoneNumber-CountryCode] [varchar](4) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[IsEmailVerified] [bit] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_EmailID]    Script Date: 11-02-2021 18:07:38 ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [UQ_Users_EmailID] UNIQUE NONCLUSTERED 
(
	[EmailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NoteDetails] ADD  CONSTRAINT [DF_NoteDetails_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[SellerNotesAttachments] ADD  CONSTRAINT [DF_SellerNotesAttachments_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsEmailVerified]  DEFAULT ((0)) FOR [IsEmailVerified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[DownloadedNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadedNotes_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[DownloadedNotes] CHECK CONSTRAINT [FK_DownloadedNotes_NoteID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CategoryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CountryID] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CountryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_TypeID] FOREIGN KEY([TypeID])
REFERENCES [dbo].[Types] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_TypeID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_UserID]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK_NoteReview_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK_NoteReview_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_NoteID]
GO
ALTER TABLE [dbo].[SpamReports]  WITH CHECK ADD  CONSTRAINT [FK_SpamReports_ReportedBy] FOREIGN KEY([ReportedBy])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SpamReports] CHECK CONSTRAINT [FK_SpamReports_ReportedBy]
GO
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_UserID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[UserRoles] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_RoleID]
GO
USE [master]
GO
ALTER DATABASE [Notes-MarketPlace] SET  READ_WRITE 
GO