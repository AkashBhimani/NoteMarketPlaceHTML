USE [master]
GO
/****** Object:  Database [NotesMarketPlace]    Script Date: 08-03-2021 16:05:11 ******/
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
/****** Object:  ColumnMasterKey [CMK_Auto1]    Script Date: 08-03-2021 16:05:12 ******/
CREATE COLUMN MASTER KEY [CMK_Auto1]
WITH
(
	KEY_STORE_PROVIDER_NAME = N'MSSQL_CERTIFICATE_STORE',
	KEY_PATH = N'CurrentUser/my/6749FB08A30A6CC4E7AEB1B99687BACAE2410E67'
)
GO
/****** Object:  ColumnEncryptionKey [CEK_Auto1]    Script Date: 08-03-2021 16:05:12 ******/
CREATE COLUMN ENCRYPTION KEY [CEK_Auto1]
WITH VALUES
(
	COLUMN_MASTER_KEY = [CMK_Auto1],
	ALGORITHM = 'RSA_OAEP',
	ENCRYPTED_VALUE = 0x016E000001630075007200720065006E00740075007300650072002F006D0079002F0036003700340039006600620030003800610033003000610036006300630034006500370061006500620031006200390039003600380037006200610063006100650032003400310030006500360037001A37A7E3BB78FDF08C641828D082C07E342FB4EA5BD910D4FFB9623BB5936F0DC43CC95784684761C57E8A4DEC9608F2AF6EE366163296683828D472BB199793AE87291DC54A18BDD7A090A144F64E30FF1DE1FC147C967E9EF89CC16181368799D3FD8C9F03B13009C8C60E2E33A2543CD6DE23D723AF9A9F8DD72DBE334836DF17D468B631288A371F0591B763D42358F03E4C282C1F4481EE3862BFF22B9FFCFE983F72ADF45BCDB5A2F19C5ED7DCCF9C4367E87127206F3BF8AC1468B2619D9CAE1649B8ED95BCADB8994A8C39EB3990BFC9AB4DCB986A6120B307071C10B5D32EA0E86694D5C93958CDA6F80D0098D7D7F7E509A020B4884A47C729A14DAE41DA09B5470DECBEDF0A06EC613BA0CBCFB82C6CBB715521A487305B261CDA0FF5D470C671F7CC0A39A2061E5785DD73D1E4F70CC530AA2C99CAD4015EA0B2C5413B93FA813F29E9827AC919917727B9B325CA1C084705161F99516CA3BD893340E038185ADFBF54C0A5EF65F4F8E281D930F65DA7DC5E0E4820F4EDBE6A690F7A70D9727C4F3A572963F8E0F6391C428BC9ECC0CAF3E22EC73689FCFE34446C4061FC8338B9BA0860508213C6DABB40F2A237AB95284BF48C8D42776D8B87DE80B57E26D6FDB5EF4AC0369C1AB392C4B6200A141CA7D713F941DAC746BE57E32F1964D55A45A3213F30F14BB3EF96A8D0ACCE1CF1924C6B1F5F07A8641865
)
GO
/****** Object:  Table [dbo].[ContactUs]    Script Date: 08-03-2021 16:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactUs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[Countries]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[DownloadedNotes]    Script Date: 08-03-2021 16:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DownloadedNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[ManageSystemConfiguration]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[NoteCategories]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[NoteDetails]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[NoteReview]    Script Date: 08-03-2021 16:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NoteReview](
	[ID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[NoteTypes]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[SellerNotesAttachments]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[SpamReports]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 08-03-2021 16:05:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[SecondaryEmail] [varchar](100) NULL,
	[DateOfBirth] [datetime] NULL,
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
/****** Object:  Table [dbo].[UserRoles]    Script Date: 08-03-2021 16:05:13 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 08-03-2021 16:05:13 ******/
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
SET IDENTITY_INSERT [dbo].[ContactUs] ON 

INSERT [dbo].[ContactUs] ([ID], [FullName], [EmailID], [Subject], [Comments/Questions], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'adss', N'akash@123', N'asdd', N'ghf', NULL, NULL, NULL, NULL)
INSERT [dbo].[ContactUs] ([ID], [FullName], [EmailID], [Subject], [Comments/Questions], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'akash@123.GHJ', N'akash@123.GHJ', N'asdd', N'123', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ContactUs] OFF
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'India', N'+91', NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Canada', N'+1', NULL, NULL, NULL, NULL)
INSERT [dbo].[Countries] ([ID], [CountryName], [CountryCode], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Germany', N'+49', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteCategories] ON 

INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'IT', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Computer', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'Science', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N'History', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteCategories] ([ID], [CategoryName], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'Account', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteDetails] ON 

INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (11, 2070, 1, 1, 1, N'c#', NULL, N'F:\GitHub\NotesMarketPlace\Members\2070\DP.pdf', NULL, N'C#', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-03T13:34:48.500' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (12, 2071, 2, 2, 2, N'Python', NULL, N'sadlj', NULL, N'Pythonfgfhg', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T18:58:27.947' AS DateTime), 2071, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (16, 2070, 2, 1, 2, N'c++', NULL, N'F:\GitHub\NotesMarketPlace\Members\2070\DP.pdf', NULL, N'C++', NULL, NULL, NULL, NULL, 1, CAST(300 AS Decimal(18, 0)), NULL, N'In Review', NULL, NULL, NULL, CAST(N'2021-03-04T16:15:04.023' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (17, 2070, 1, 2, 1, N'c#', NULL, N'F:\GitHub\NotesMarketPlace\Members\2070\DP.pdf', NULL, N'C#', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'In Review', NULL, NULL, NULL, CAST(N'2021-03-04T18:03:32.563' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (18, 2070, 2, 1, 2, N'c#', NULL, N'F:\GitHub\NotesMarketPlace\Members\2070\DP.pdf', NULL, N'asd', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'In Review', NULL, NULL, NULL, CAST(N'2021-03-05T11:53:24.363' AS DateTime), NULL, NULL, NULL, 0)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (19, 2070, 1, 1, 1, N'c#', NULL, N'sadlj', NULL, N'c#', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T16:37:12.337' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (20, 2070, 2, 1, 2, N'c#', N'~/DefalutImages/tech-quote.jpg', N'sadlj', NULL, N'aaas', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T16:44:35.807' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (21, 2070, 1, 1, 1, N'c#', NULL, N'sadlj', NULL, N'aaa', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T16:53:44.463' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (22, 2070, 1, 1, 1, N'c#', NULL, N'sadlj', NULL, N'aaa', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T17:01:28.317' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (23, 2070, 1, 1, 1, N'c#', NULL, N'sadlj', NULL, N'dfgh', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T17:08:16.843' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (24, 2070, 1, 1, 1, N'Python', NULL, N'sadlj', NULL, N'aa', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T17:14:28.717' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (25, 2070, 2, 1, 2, N'c#', NULL, N'sadlj', NULL, N'gg', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-05T18:08:00.130' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (26, 2070, 1, 1, 1, N'AI', NULL, N'sadlj', NULL, N'AI', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-07T18:27:43.810' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (27, 2070, 2, 1, 2, N'JAVA', NULL, N'sadlj', NULL, N'JAVA Programming ', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-07T18:36:10.463' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (28, 2070, 2, 1, 2, N'Web Technology', NULL, N'sadlj', NULL, N'Web Technology', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-07T18:44:20.913' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (29, 2070, 2, 1, 2, N'C', NULL, N'sadlj', NULL, N'C Programming', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-07T18:51:05.967' AS DateTime), 2070, NULL, NULL, 1)
INSERT [dbo].[NoteDetails] ([ID], [UserID], [CategoryID], [TypeID], [CountryId], [NoteTitle], [DisplayPicture], [UploadNote], [NumberOfPages], [Description], [InstitutionName], [Course], [CourseCode], [Professor], [IsPaid], [SellPrice], [NotePreview], [Status], [ActionBy], [Remarks], [PublishedDate], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (30, 2070, 2, 1, 2, N'Java script', NULL, N'sadlj', NULL, N'Java Script ', NULL, NULL, NULL, NULL, 0, CAST(0 AS Decimal(18, 0)), NULL, N'Draft', NULL, NULL, NULL, CAST(N'2021-03-07T18:55:46.950' AS DateTime), 2070, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[NoteDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[NoteTypes] ON 

INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'Textbook', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
INSERT [dbo].[NoteTypes] ([ID], [Type], [Description], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (2, N'Storybook', N'Lorem Ipsum is simply dumay text.', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[NoteTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] ON 

INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (1, 23, N'Attachment1_05082021.pdf;', N'/Members/2070/23/Attachment/Attachment1_05082021.pdf', CAST(N'2021-03-05T17:08:41.843' AS DateTime), 23, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (2, 24, N'Attachment1_05142021.pdf;Attachment2_05142021.pdf;Attachment3_05142021.pdf;', N'/Members/2070/24/Attachment/Attachment1_05142021.pdf/Members/2070/24/Attachment/Attachment2_05142021.pdf/Members/2070/24/Attachment/Attachment3_05142021.pdf', CAST(N'2021-03-05T17:14:29.697' AS DateTime), 24, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (3, 25, N'Attachment1_05-03-2021.pdf;', N'/Members/2070/25/Attachment/Attachment1_05-03-2021.pdf', CAST(N'2021-03-05T18:08:01.947' AS DateTime), 25, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (4, 12, N'Attachment1_05-03-2021.pdf;', N'/Members/2071/12/Attachment/Attachment1_05-03-2021.pdf', CAST(N'2021-03-05T18:58:35.177' AS DateTime), 12, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (5, 26, N'Attachment1_07-03-2021.pdf;', N'/Members/2070/26/Attachment/Attachment1_07-03-2021.pdf', CAST(N'2021-03-07T18:27:45.227' AS DateTime), 26, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (6, 27, N'Attachment1_07-03-2021.pdf;', N'/Members/2070/27/Attachment/Attachment1_07-03-2021.pdf', CAST(N'2021-03-07T18:36:11.153' AS DateTime), 27, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (7, 28, N'Attachment1_07-03-2021.pdf;', N'/Members/2070/28/Attachment/Attachment1_07-03-2021.pdf', CAST(N'2021-03-07T18:44:21.483' AS DateTime), 28, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (8, 29, N'Attachment1_07-03-2021.pdf;', N'/Members/2070/29/Attachment/Attachment1_07-03-2021.pdf', CAST(N'2021-03-07T18:51:07.183' AS DateTime), 29, NULL, NULL, 1)
INSERT [dbo].[SellerNotesAttachments] ([ID], [NoteID], [FileName], [FilePath], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy], [IsActive]) VALUES (9, 30, N'Attachment1_07-03-2021.pdf;', N'/Members/2070/30/Attachment/Attachment1_07-03-2021.pdf', CAST(N'2021-03-07T18:55:47.323' AS DateTime), 30, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SellerNotesAttachments] OFF
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
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Users_EmailID]    Script Date: 08-03-2021 16:05:13 ******/
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
ALTER DATABASE [NotesMarketPlace] SET  READ_WRITE 
GO
