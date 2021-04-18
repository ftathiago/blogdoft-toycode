USE [master]
GO

/****** Object:  Database [SaleDatabase]    Script Date: 18/04/2021 12:32:26 ******/
CREATE DATABASE [SaleDatabase]
GO


USE [SaleDatabase]
GO

/****** Object:  Table [dbo].[Sales]    Script Date: 18/04/2021 12:39:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE SaleDatabase.dbo.Products (
	id uniqueidentifier NOT NULL,
	description varchar(250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	unit_value decimal(18,2) NOT NULL,
	active bit DEFAULT 1 NOT NULL,
	CONSTRAINT PK_Products PRIMARY KEY (id)
);

CREATE TABLE SaleDatabase.dbo.Sales (
	id uniqueidentifier NOT NULL,
	document_id char(14) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	sale_number char(10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	sale_date datetime NOT NULL,
	CONSTRAINT PK_Sales PRIMARY KEY (id)
);

CREATE TABLE SaleDatabase.dbo.SaleItems (
	id uniqueidentifier NOT NULL,
	sale_id uniqueidentifier NOT NULL,
	product_id uniqueidentifier NOT NULL,
	value numeric(18,2) NOT NULL,
	quantity numeric(18,4) NOT NULL,
	CONSTRAINT PK_SaleItem PRIMARY KEY (id,sale_id),
	CONSTRAINT SaleItems_FK FOREIGN KEY (sale_id) REFERENCES SaleDatabase.dbo.Sales(id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT SaleItems_FK_1 FOREIGN KEY (product_id) REFERENCES SaleDatabase.dbo.Products(id)
);