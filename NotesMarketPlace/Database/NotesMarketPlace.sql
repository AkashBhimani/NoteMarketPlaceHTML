USE [master]
GO
/****** Object:  Database [NotesMarketPlace]    Script Date: 27-03-2021 10:35:51 ******/
CREATE DATABASE [NotesMarketPlace]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Notes-MarketPlace', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Notes-MarketPlace.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Notes-MarketPlace_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Notes-MarketPlace_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NotesMarketPlace] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotesMarketPlace].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotesMarketPlace] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NotesMarketPlace] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotesMarketPlace] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET RECOVERY FULL 
GO
ALTER DATABASE [NotesMarketPlace] SET  MULTI_USER 
GO
ALTER DATABASE [NotesMarketPlace] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotesMarketPlace] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotesMarketPlace] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotesMarketPlace] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotesMarketPlace] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NotesMarketPlace] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NotesMarketPlace', N'ON'
GO
ALTER DATABASE [NotesMarketPlace] SET QUERY_STORE = OFF
GO
USE [NotesMarketPlace]
GO
/****** Object:  ColumnMasterKey [CMK_Auto1]    Script Date: 27-03-2021 10:35:52 ******/
CREATE COLUMN MASTER KEY [CMK_Auto1]
WITH
(
	KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',
	KEY_PATH = N'CurrentUser/my/6749FB08A30A6CC4E7AEB1B99687BACAE2410E67'
)
GO
/****** Object:  ColumnEncryptionKey [CEK_Auto1]    Script Date: 27-03-2021 10:35:52 ******/
CREATE COLUMN ENCRYPTION KEY [CEK_Auto1]
WITH VALUES
(
	COLUMN_MASTER_KEY = [CMK_Auto1],
	ALGORITHM = 'RSA_OAEP',
	ENCRYPTED_VALUE = 0x016E000001630075007200720065006E00740075007300650072002F006D0079002F0036003700340039006600620030003800610033003000610036006300630034006500370061006500620031006200390039003600380037006200610063006100650032003400310030006500360037001A37A7E3BB78FDF08C641828D082C07E342FB4EA5BD910D4FFB9623BB5936F0DC43CC95784684761C57E8A4DEC9608F2AF6EE366163296683828D472BB199793AE87291DC54A18BDD7A090A144F64E30FF1DE1FC147C967E9EF89CC16181368799D3FD8C9F03B13009C8C60E2E33A2543CD6DE23D723AF9A9F8DD72DBE334836DF17D468B631288A371F0591B763D42358F03E4C282C1F4481EE3862BFF22B9FFCFE983F72ADF45BCDB5A2F19C5ED7DCCF9C4367E87127206F3BF8AC1468B2619D9CAE1649B8ED95BCADB8994A8C39EB3990BFC9AB4DCB986A6120B307071C10B5D32EA0E86694D5C93958CDA6F80D0098D7D7F7E509A020B4884A47C729A14DAE41DA09B5470DECBEDF0A06EC613BA0CBCFB82C6CBB715521A487305B261CDA0FF5D470C671F7CC0A39A2061E5785DD73D1E4F70CC530AA2C99CAD4015EA0B2C5413B93FA813F29E9827AC919917727B9B325CA1C084705161F99516CA3BD893340E038185ADFBF54C0A5EF65F4F8E281D930F65DA7DC5E0E4820F4EDBE6A690F7A70D9727C4F3A572963F8E0F6391C428BC9ECC0CAF3E22EC73689FCFE34446C4061FC8338B9BA0860508213C6DABB40F2A237AB95284BF48C8D42776D8B87DE80B57E26D6FDB5EF4AC0369C1AB392C4B6200A141CA7D713F941DAC746BE57E32F1964D55A45A3213F30F14BB3EF96A8D0ACCE1CF1924C6B1F5F07A8641865
)
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 27-03-2021 10:35:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NOT NULL,
	[CountryCode] [varchar](10) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Add/EditCountry] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DownloadedNotes]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadedNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[Seller] [int] NOT NULL,
	[Buyer] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[IsSellerHasAllowedDownload] [bit] NOT NULL,
	[AttachmentPath] [varchar](max) NULL,
	[IsAttachmentDownloaded] [bit] NOT NULL,
	[AttachmentDownloadedDate] [datetime] NULL,
	[IsPaid] [bit] NOT NULL,
	[Price] [decimal](18, 0) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_DownloadedNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ManageSystemConfiguration]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ManageSystemConfiguration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteCategories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Add/EditCategory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
	[TypeID] [int] NOT NULL,
	[CountryId] [int] NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[DisplayPicture] [varchar](max) NULL,
	[UploadNote] [varchar](max) NOT NULL,
	[NumberOfPages] [int] NULL,
	[Description] [varchar](max) NOT NULL,
	[InstitutionName] [varchar](200) NULL,
	[Course] [varchar](100) NULL,
	[CourseCode] [varchar](100) NULL,
	[Professor] [varchar](100) NULL,
	[IsPaid] [bit] NOT NULL,
	[SellPrice] [decimal](18, 0) NULL,
	[NotePreview] [varchar](max) NULL,
	[Status] [varchar](50) NOT NULL,
	[ActionBy] [int] NULL,
	[Remarks] [varchar](max) NULL,
	[PublishedDate] [datetime] NULL,
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
/****** Object:  Table [dbo].[NoteReview]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReview](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReviewedByID] [int] NOT NULL,
	[AgainstDownloadsID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Add/EditTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SellerNotesAttachments]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesAttachments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[SellerNotesReportedIssues]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellerNotesReportedIssues](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[ReportedByID] [int] NOT NULL,
	[AgainstDownloadID] [int] NOT NULL,
	[Remarks] [varchar](max) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SellerNotesReportedIssues_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SpamReports]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SpamReports](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Gender] [varchar](6) NULL,
	[PhoneNumber-CountryCode] [varchar](4) NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[ProfilePicture] [varchar](max) NULL,
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Users]    Script Date: 27-03-2021 10:35:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[EmailID] [varchar](100) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[PhoneNumber-CountryCode] [varchar](4) NULL,
	[PhoneNumber] [varchar](20) NULL,
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
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'India', N'+91', NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Canada', N'+1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Germany', N'+49', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[DownloadedNotes] ON 

INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 2006, 2075, 2070, N'Computer Hardware', N'CS', 1, NULL, 1, CAST(N'2021-03-16T18:53:30.587' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), CAST(N'2021-03-16T18:53:30.593' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 2008, 2070, 2070, N'Artificial Intelligence', N'IT', 0, NULL, 0, CAST(N'2021-03-17T11:42:58.150' AS DateTime), 1, CAST(300 AS Decimal(18, 0)), CAST(N'2021-03-17T11:42:58.153' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (67, 1017, 2070, 2070, N'c#', N'IT', 1, NULL, 1, CAST(N'2021-03-17T18:57:39.650' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), CAST(N'2021-03-17T18:57:39.653' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (68, 1018, 2070, 2070, N'Basic Computer Engineering', N'CA', 1, NULL, 1, CAST(N'2021-03-17T18:58:02.777' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), CAST(N'2021-03-17T18:58:02.783' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (77, 2007, 2070, 2075, N'The Computer Book', N'IT', 1, N'/Members/2070/2007/Attachment/Attachment1_17-03-2021.pdf', 0, CAST(N'2021-03-21T12:09:37.880' AS DateTime), 1, CAST(300 AS Decimal(18, 0)), CAST(N'2021-03-21T12:09:37.893' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (78, 1018, 2070, 2071, N'Basic Computer Engineering', N'CA', 1, N'~/Members/2070/1018/Attachment/Attachment1_12-03-2021.pdf', 1, CAST(N'2021-03-23T10:12:48.907' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), CAST(N'2021-03-23T10:12:49.063' AS DateTime), 2071, NULL, NULL)
INSERT [dbo].[DownloadedNotes] ([ID], [NoteID], [Seller], [Buyer], [NoteTitle], [Category], [IsSellerHasAllowedDownload], [AttachmentPath], [IsAttachmentDownloaded], [AttachmentDownloadedDate], [IsPaid], [Price], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (79, 1017, 2070, 2070, N'c#', N'IT', 1, N'~/Members/2070/1017/Attachment/Attachment1_11-03-2021.pdf', 1, CAST(N'2021-03-25T10:25:49.290' AS DateTime), 0, CAST(0 AS Decimal(18, 0)), CAST(N'2021-03-25T10:25:49.293' AS DateTime), 2070, NULL, NULL)
SET IDENTITY_INSERT [dbo].[DownloadedNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'IT', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'CA', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'CS', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'MBA', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'MCA', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1017, 2070, 1, 2, 1, N'c#', NULL, N'sadlj', 120, N'C#', N'GEC', N'Computer Engineering', NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-11T19:35:35.327' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1018, 2070, 2, 2, 2, N'Basic Computer Engineering', N'F:\GitHub\NotesMarketPlace\Members\2070\1018\DP_12-03-2021.jpg', N'sadlj', NULL, N'Basic Computer Engineering', N'GTU', N'Information Technology', NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-12T11:43:08.357' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2006, 2075, 3, 8, 3, N'Computer Hardware', N'F:\GitHub\NotesMarketPlace\Members\2075\2006\DP_16-03-2021.jpg', N'sadlj', 120, N'Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.', N'GTU', N'demo', NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), N'F:\GitHub\NotesMarketPlace\Members\2075\2006\Preview_16-03-2021.pdf', N'Draft', NULL, NULL, NULL, CAST(N'2021-03-16T12:16:36.303' AS DateTime), 2075, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2007, 2070, 1, 8, 1, N'The Computer Book', N'F:\GitHub\NotesMarketPlace\Members\2070\2007\DP_17-03-2021.jpg', N'sadlj', 500, N'Once email is sent successfully – We can give user a message that “Your password has been changed
successfully and newly generated password is sent on your registered email address.” and then redirect
user to Login page
', N'VGEC', N'EC', NULL, NULL, 1, CAST(300 AS Decimal(18, 0)), N'F:\GitHub\NotesMarketPlace\Members\2070\2007\Preview_17-03-2021.pdf', N'Draft', NULL, NULL, NULL, CAST(N'2021-03-17T11:02:02.947' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2008, 2070, 1, 2, 1, N'Artificial Intelligence', NULL, N'sadlj', 1000, N' Click on this button we can ask confirmation box –
“Publishing this note will send note to administrator for review, once
administrator review and approve then this note will be published to
portal. Press yes to continue.” with Yes & Cancel buttons.', N'Gyan', N'Computer Engineering', NULL, NULL, 1, CAST(300 AS Decimal(18, 0)), N'F:\GitHub\NotesMarketPlace\Members\2070\2008\Preview_17-03-2021.pdf', N'Draft', NULL, NULL, NULL, CAST(N'2021-03-17T11:42:15.320' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2009, 2070, 2, 9, 2, N'Day Dreamer', NULL, N'sadlj', NULL, N'Seller can define number of pages document has – As it will be shown
to Downloader/Anonymous users when they search for any notes. It’s
allowed only numeric values', NULL, NULL, NULL, NULL, 1, CAST(1200 AS Decimal(18, 0)), N'F:\GitHub\NotesMarketPlace\Members\2070\2009\Preview_23-03-2021.jpg', N'Rejected', NULL, NULL, NULL, CAST(N'2021-03-23T11:10:24.177' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2010, 2077, 2, 8, 2, N'Java script', NULL, N'sadlj', 800, N'This screen is for Sellers to view all buyer requests and to acknowledge that he/she receive the payment
from buyer or not.', N'XYZ', N'ABC', N'32', N'PQR', 1, CAST(2000 AS Decimal(18, 0)), N'F:\GitHub\NotesMarketPlace\Members\2077\2010\Preview_25-03-2021.pdf', N'Rejected', NULL, NULL, NULL, CAST(N'2021-03-25T12:14:27.140' AS DateTime), 2077, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteReview] ON 

INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, 2007, 2070, 77, 4, N'mast', CAST(N'2021-03-22T13:59:40.063' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, 2007, 2070, 77, 2, N'Perfect.', CAST(N'2021-03-22T15:28:31.980' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, 1017, 2075, 67, 3, N'Not bad.', CAST(N'2021-03-22T16:14:00.583' AS DateTime), 2075, NULL, NULL)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 1017, 2070, 67, 3, N'Good Job.', CAST(N'2021-03-22T16:18:45.203' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, 2007, 2075, 77, 2, N'aa', CAST(N'2021-03-22T18:16:38.997' AS DateTime), 2075, NULL, NULL)
INSERT [dbo].[NoteReview] ([ID], [NoteID], [ReviewedByID], [AgainstDownloadsID], [Rate], [Comment], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, 2007, 2075, 77, 4, N'Excellent.', CAST(N'2021-03-25T10:50:29.497' AS DateTime), 2075, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteReview] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteTypes] ON 

INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Handwritten notes', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'University notes', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (8, N'Notebook', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (9, N'Novel', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] ON 

INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1013, 1017, N'Attachment1_11-03-2021.pdf;', N'/Members/2070/1017/Attachment/Attachment1_11-03-2021.pdf', CAST(N'2021-03-11T19:35:38.507' AS DateTime), 1017, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1014, 1018, N'Attachment1_12-03-2021.pdf;', N'/Members/2070/1018/Attachment/Attachment1_12-03-2021.pdf', CAST(N'2021-03-12T11:43:09.300' AS DateTime), 1018, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2002, 2006, N'Attachment1_16-03-2021.pdf;', N'/Members/2075/2006/Attachment/Attachment1_16-03-2021.pdf', CAST(N'2021-03-16T12:16:36.747' AS DateTime), 2006, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2003, 2007, N'Attachment1_17-03-2021.pdf;', N'/Members/2070/2007/Attachment/Attachment1_17-03-2021.pdf', CAST(N'2021-03-17T11:02:05.617' AS DateTime), 2007, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2004, 2008, N'Attachment1_17-03-2021.pdf;', N'/Members/2070/2008/Attachment/Attachment1_17-03-2021.pdf', CAST(N'2021-03-17T11:42:17.983' AS DateTime), 2008, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3002, 2009, N'Attachment1_23-03-2021.pdf;', N'/Members/2070/2009/Attachment/Attachment1_23-03-2021.pdf', CAST(N'2021-03-23T11:10:25.403' AS DateTime), 2009, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3003, 2010, N'Attachment1_25-03-2021.pdf;', N'/Members/2077/2010/Attachment/Attachment1_25-03-2021.pdf', CAST(N'2021-03-25T12:14:28.553' AS DateTime), 2010, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesReportedIssues] ON 

INSERT [dbo].[SellerNotesReportedIssues] ([ID], [NoteID], [ReportedByID], [AgainstDownloadID], [Remarks], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, 2007, 2075, 77, N'ddd', CAST(N'2021-03-22T18:48:13.550' AS DateTime), 2075, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SellerNotesReportedIssues] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([ID], [UserID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine-1], [AddressLine-2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifieDate], [ModifiedBy]) VALUES (1014, 2071, NULL, CAST(N'2021-04-03T00:00:00.0000000' AS DateTime2), N'male', N'+91', N'9099537253', NULL, N'Popatbhai ni wadi', N'Laltanki', N'Bhavnagar', N'Gujarat', N'364002', N'India', N'GTU', N'GEC', CAST(N'2021-03-11T19:42:08.410' AS DateTime), 2071, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine-1], [AddressLine-2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifieDate], [ModifiedBy]) VALUES (1015, 2070, NULL, CAST(N'2021-01-03T00:00:00.0000000' AS DateTime2), NULL, N'+91', N'9099537253', N'\Members\2070\DP_12-03-2021.jpg', N'Popatbhai ni wadi', N'Laltanki', N'akashpbhimani000@gmail.com', N'Gujarat', N'364002', N'India', N'GTU', N'GEC', CAST(N'2021-03-12T11:28:12.170' AS DateTime), 2070, NULL, NULL)
INSERT [dbo].[UserProfile] ([ID], [UserID], [SecondaryEmail], [DateOfBirth], [Gender], [PhoneNumber-CountryCode], [PhoneNumber], [ProfilePicture], [AddressLine-1], [AddressLine-2], [City], [State], [ZipCode], [Country], [University], [College], [SubmittedDate], [SubmittedBy], [ModifieDate], [ModifiedBy]) VALUES (2015, 2075, NULL, CAST(N'2021-12-03T00:00:00.0000000' AS DateTime2), N'male', N'+91', N'9099537253', NULL, N'Popatbhai ni wadi', N'Laltanki', N'Bhavnagar', N'Gujarat', N'364002', N'Germany', N'GTU', N'GEC', CAST(N'2021-03-16T12:13:34.850' AS DateTime), 2075, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 

INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, N'Super Admin', N'Super Admin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, N'Admin', N'Admin', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[UserRoles] ([ID], [Name], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, N'User', N'User', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2064, 3, N'Pinal', N'Bhimani', N'akashpbhimani123@gmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2070, 3, N'Pinal', N'Bhimani', N'akashpbhimani000@gmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2071, 3, N'Akash', N'Bhimani', N'akashpbhimani1245@yopmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2072, 3, N'nisha', N'Bhimani', N'nishabhimani@yopmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2075, 3, N'Akash', N'Bhimani', N'akashpbhimani12345@yopmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2076, 3, N'Akash', N'Parmar', N'akashpbhimani890@gmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 0, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Users] ([ID], [RoleID], [FirstName], [LastName], [EmailID], [Password], [PhoneNumber-CountryCode], [PhoneNumber], [IsEmailVerified], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2077, 3, N'Akash', N'Parmar', N'akashpbhimani890@yopmail.com', N'Edv1zipn0Q/eKKcJ4Edvxw==', NULL, NULL, 1, NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_EmailID]    Script Date: 27-03-2021 10:35:53 ******/
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
ALTER TABLE [dbo].[DownloadedNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadedNotes_Buyer] FOREIGN KEY([Buyer])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[DownloadedNotes] CHECK CONSTRAINT [FK_DownloadedNotes_Buyer]
GO
ALTER TABLE [dbo].[DownloadedNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadedNotes_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[DownloadedNotes] CHECK CONSTRAINT [FK_DownloadedNotes_NoteID]
GO
ALTER TABLE [dbo].[DownloadedNotes]  WITH CHECK ADD  CONSTRAINT [FK_DownloadedNotes_Seller] FOREIGN KEY([Seller])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[DownloadedNotes] CHECK CONSTRAINT [FK_DownloadedNotes_Seller]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[NoteCategories] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CategoryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_CountryID] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_CountryID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_TypeID] FOREIGN KEY([TypeID])
REFERENCES [dbo].[NoteTypes] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_TypeID]
GO
ALTER TABLE [dbo].[NoteDetails]  WITH CHECK ADD  CONSTRAINT [FK_NoteDetails_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteDetails] CHECK CONSTRAINT [FK_NoteDetails_UserID]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK_NoteReview_AgainstDownloadsID] FOREIGN KEY([AgainstDownloadsID])
REFERENCES [dbo].[DownloadedNotes] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK_NoteReview_AgainstDownloadsID]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK_NoteReview_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK_NoteReview_NoteID]
GO
ALTER TABLE [dbo].[NoteReview]  WITH CHECK ADD  CONSTRAINT [FK_NoteReview_ReviewedByID] FOREIGN KEY([ReviewedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[NoteReview] CHECK CONSTRAINT [FK_NoteReview_ReviewedByID]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_AgainstDownloadID] FOREIGN KEY([AgainstDownloadID])
REFERENCES [dbo].[DownloadedNotes] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_AgainstDownloadID]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_NoteID] FOREIGN KEY([NoteID])
REFERENCES [dbo].[NoteDetails] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_NoteID]
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues]  WITH CHECK ADD  CONSTRAINT [FK_SellerNotesReportedIssues_ReportedByID] FOREIGN KEY([ReportedByID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[SellerNotesReportedIssues] CHECK CONSTRAINT [FK_SellerNotesReportedIssues_ReportedByID]
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
ALTER DATABASE [NotesMarketPlace] SET  READ_WRITE 
GO
