
--CREATE DATABASE FASM
--GO

--USE [FASM]
--GO

-- Create table Roles
CREATE TABLE [dbo].[Roles](
[RoleId] [int] IDENTITY(1,1) NOT NULL,[RoleName] [nvarchar](50) NOT NULL,[IsSysAdmin] [bit] NOT NULL CONSTRAINT [DF_Roles_IsSysAdmin]  DEFAULT ((0)),
CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([RoleId] ASC))

--Create table Permissions	
CREATE TABLE [dbo].[Permissions](	
[PermissionId] [int] IDENTITY(1,1) NOT NULL,[PermissionDescription] [nvarchar](100) NOT NULL,[Description] [nvarchar](100) NOT NULL,[ModName] [nvarchar](50) NULL,
CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED( [PermissionId] ASC))

-- Create Table MapRolePermission
CREATE TABLE [dbo].[MapRolePermission](
[RoleId] [int] NOT NULL,	[PermissionId] [int] NOT NULL,
CONSTRAINT [PK_MapRolePermission] PRIMARY KEY CLUSTERED( [RoleId] ASC,[PermissionId] ASC))
ALTER TABLE [dbo].[MapRolePermission]  WITH NOCHECK ADD  CONSTRAINT [FK_MapRolePermission_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
ALTER TABLE [dbo].[MapRolePermission] CHECK CONSTRAINT [FK_MapRolePermission_Permissions]
ALTER TABLE [dbo].[MapRolePermission]  WITH NOCHECK ADD  CONSTRAINT [FK_MapRolePermission_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ALTER TABLE [dbo].[MapRolePermission] CHECK CONSTRAINT [FK_MapRolePermission_Roles]	

-- Create table MapUserRole
CREATE TABLE [dbo].[MapUserRole](
[UserId] [int] NOT NULL,[RoleId] [int] NOT NULL, CONSTRAINT [PK_MapUserRole] PRIMARY KEY CLUSTERED([UserId] ASC, [RoleId] ASC))
ALTER TABLE [dbo].[MapUserRole]  WITH NOCHECK ADD  CONSTRAINT [FK_MapUserRole_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ALTER TABLE [dbo].[MapUserRole] CHECK CONSTRAINT [FK_MapUserRole_Roles]

-- Create table Category
CREATE TABLE [dbo].[Categories](
[CategoryId] [int] IDENTITY(1,1) NOT NULL,[CategoryName] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED([CategoryId] ASC))

-- Create table Location
CREATE TABLE [dbo].[Location](
[LocationId] [int] IDENTITY(1,1) NOT NULL,[LocationName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_AssetLocation] PRIMARY KEY CLUSTERED([LocationId] ASC)) 

-- Create table Users
CREATE TABLE [dbo].[Users](
[UserId] [bigint] IDENTITY(1,1) NOT NULL,[FirstName] [nvarchar](50) NOT NULL,[LastName] [nvarchar](50) NOT NULL,[Email] [nvarchar](250) NULL,
[PhoneNumber] [nvarchar](50) NOT NULL,[Username] [nvarchar](50) NOT NULL,[Password] [nvarchar](50) NOT NULL,
CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED([UserId] ASC)) 	

-- Create Table Regions
CREATE TABLE [dbo].[Regions](
[RegionId] [int] IDENTITY(1,1) NOT NULL,[RegionName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_Regions] PRIMARY KEY CLUSTERED ([RegionId] ASC))

-- Create table districts 
CREATE TABLE [dbo].[Districts](
[DistrictId] [int] IDENTITY(1,1) NOT NULL,	[DistrictName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED ([DistrictId] ASC))

--Create table suppliers
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,[SupplierFullName] [nvarchar](100) NOT NULL,[CompanyName] [nvarchar](100) NULL,
	[RegionId] [int] NOT NULL,[DistrictId] [int] NOT NULL,[Website] [nvarchar](100) NULL,[JobPosition] [nvarchar](30) NULL,
	[Phone] [nvarchar](20) NOT NULL,[Email] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED ([SupplierId] ASC))

ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Districts]

ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Regions] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Regions] ([RegionId])
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Regions]

-- Create table asset definition
CREATE TABLE [dbo].[AssetDefinition](
	[AssetDefinitionId] [bigint] IDENTITY(1,1) NOT NULL,[AssetName] [nvarchar](100) NOT NULL,
	[CategoryId] [int] NOT NULL,[BrandName] [nvarchar](50) NULL,[DepreciationMethod] [char](1) NOT NULL,
 CONSTRAINT [PK_AssetDefinition] PRIMARY KEY CLUSTERED ([AssetDefinitionId] ASC))

ALTER TABLE [dbo].[AssetDefinition]  WITH CHECK ADD  CONSTRAINT [FK_AssetDefinition_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ALTER TABLE [dbo].[AssetDefinition] CHECK CONSTRAINT [FK_AssetDefinition_Categories]

-- Create table Departments
CREATE TABLE [dbo].[Departments](
[DepartmentId] [int] IDENTITY(1,1) NOT NULL,[DepartmentName] [nvarchar](100) NOT NULL,
CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED ([DepartmentId] ASC))

-- Create table employees 
CREATE TABLE [dbo].[Employees](
[EmployeeId] [int] IDENTITY(1,1) NOT NULL,[FirstName] [nvarchar](30) NOT NULL,[MiddleName] [nvarchar](30) NULL,
[LastName] [nvarchar](30) NOT NULL,[Gender] [char](1) NOT NULL,[PhoneNumber] [nvarchar](20) NULL,
[Email] [nvarchar](100) NULL,
CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeId] ASC))

-- Create table Asset Register
CREATE TABLE [dbo].[AssetRegister](
[AssetRegisterId] [bigint] IDENTITY(1,1) NOT NULL,[SerialNumber] [nvarchar](100) NULL,[AssetCode] [nvarchar](200) NULL,
[AssetDefinitionId] [bigint] NOT NULL,[Model] [nvarchar](50) NULL,[Condition] [char](1) NOT NULL,[AssetStatus] [char](1) NOT NULL,
[SupplierId] [int] NULL,[PurchaseDate] [datetime] NULL,[PurchaseCost] [decimal](18, 2) NOT NULL,[WarrantyExpiry] [datetime] NULL,
[SalvageValue] [decimal](18, 2) NULL,[LocationId] [int] NOT NULL,[LifeTime] [int] NOT NULL,[DepartmentId] [int] NULL,
[CustodianId] [int] NULL,[DistributionDate] [datetime] NULL,[ReallocatedDate] [datetime] NULL,
CONSTRAINT [PK_AssetRegisterId] PRIMARY KEY CLUSTERED ([AssetRegisterId] ASC))
ALTER TABLE [dbo].[AssetRegister]  WITH CHECK ADD  CONSTRAINT [FK_AssetRegister_AssetDefinition] FOREIGN KEY([AssetDefinitionId])
REFERENCES [dbo].[AssetDefinition] ([AssetDefinitionId])
ALTER TABLE [dbo].[AssetRegister] CHECK CONSTRAINT [FK_AssetRegister_AssetDefinition]
ALTER TABLE [dbo].[AssetRegister]  WITH CHECK ADD  CONSTRAINT [FK_AssetRegister_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
ALTER TABLE [dbo].[AssetRegister] CHECK CONSTRAINT [FK_AssetRegister_Location]
ALTER TABLE [dbo].[AssetRegister]  WITH CHECK ADD  CONSTRAINT [FK_AssetRegister_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
ALTER TABLE [dbo].[AssetRegister] CHECK CONSTRAINT [FK_AssetRegister_Departments]
ALTER TABLE [dbo].[AssetRegister]  WITH CHECK ADD  CONSTRAINT [FK_AssetRegister_Employees] FOREIGN KEY([CustodianId])
REFERENCES [dbo].[Employees] ([EmployeeId])
ALTER TABLE [dbo].[AssetRegister] CHECK CONSTRAINT [FK_AssetRegister_Employees]
ALTER TABLE [dbo].[AssetRegister]  WITH CHECK ADD  CONSTRAINT [FK_AssetRegister_Suppliers] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
ALTER TABLE [dbo].[AssetRegister] CHECK CONSTRAINT [FK_AssetRegister_Suppliers]