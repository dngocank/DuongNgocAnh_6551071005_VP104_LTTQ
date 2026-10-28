-- =============================================
-- CÂU 1: TẠO DATABASE VÀ TABLE THEO ĐỀ BÀI VP104
-- =============================================

USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DuLieu_NhanSu')
BEGIN
    CREATE DATABASE DuLieu_NhanSu;
END
GO

USE DuLieu_NhanSu;
GO

IF OBJECT_ID(N'dbo.NhanVien', N'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.NhanVien;
END
GO

CREATE TABLE NhanVien
(
    MaNhanVien NVARCHAR(10) NOT NULL PRIMARY KEY,
    TenNhanVien NTEXT NOT NULL,
    LuongCoBan NVARCHAR(12) NOT NULL
);
GO

-- Chèn dữ liệu mẫu như hình minh họa trong đề thi
INSERT INTO NhanVien (MaNhanVien, TenNhanVien, LuongCoBan)
VALUES 
    (N'NV001', N'Nguyễn Văn A', N'8,000,000'),
    (N'NV002', N'Trần Thị B', N'7,500,000'),
    (N'NV003', N'Lê Văn C', N'9,000,000'),
    (N'NV004', N'Phạm Thị D', N'6,800,000'),
    (N'NV005', N'Hoàng Văn E', N'8,200,000');
GO

-- Kiểm tra dữ liệu
SELECT * FROM NhanVien;
GO
