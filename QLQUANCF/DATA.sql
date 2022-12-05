CREATE DATABASE QuanLyQuanCafe
GO

USE QuanLyQuanCafe
GO
   
-- Food
-- Table
-- FoodCategory
-- Account
-- Bill
-- BillInfo

CREATE TABLE TableFood
(
	idTableFood INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Bàn chưa có tên',
	status NVARCHAR(100) NOT NULL DEFAULT N'Trống'	-- Trống || Có người
)
GO

CREATE TABLE Account
(
	UserName NVARCHAR(100) PRIMARY KEY,	
	DisplayName NVARCHAR(100) NOT NULL DEFAULT N'Kter',
	PassWord NVARCHAR(1000) NOT NULL DEFAULT 0,
	Type INT NOT NULL  DEFAULT 0 -- 1: admin && 0: staff
)
GO



CREATE TABLE FoodCategory
(
	idFoodCategory INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

INSERT INTO	dbo.FoodCategory
(
    id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)

DROP TABLE FoodCategory
DROP TABLE Billinfo

VALUES
(N'HEO KHO CẢI CHUA' -- name - nvarchar(100)
    )

DELETE FoodCategory WHERE name =N'HEO KHO CẢI CHUA'

	SELECT * FROM dbo.FoodCategory

CREATE TABLE Food
(
	idFood INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	idFoodCategory INT NOT NULL,
	price FLOAT NOT NULL DEFAULT 0
	
	FOREIGN KEY (idFoodCategory) REFERENCES dbo.FoodCategory(idFoodCategory)
)
GO


INSERT INTO dbo.Food
(
    name,
    idCategory,
    price
)

drop TABLE FOOD

VALUES
(   N'HEO KHO CẢI CHUA', -- name - nvarchar(100)
    1,       -- idCategory - int
    40000  -- price - float
    )

SELECT * FROM dbo.Food WHERE dbo.Food.name LIKE N'%HEO%'




CREATE TABLE Bill
(
	idBill INT IDENTITY PRIMARY KEY,
	DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
	DateCheckOut DATE,
	idTableFood INT NOT NULL,
	status INT NOT NULL DEFAULT 0 -- 1: đã thanh toán && 0: chưa thanh toán
	
	FOREIGN KEY (idTableFood) REFERENCES dbo.TableFood(idTableFood)
)
GO

drop table bill
drop table tablefood
drop table account
drop table food


CREATE TABLE BillInfo
(
	idBillInfo INT IDENTITY PRIMARY KEY,
	UserName NVARCHAR(100),
	
	idBill INT NOT NULL,
	idFood INT NOT NULL,
	count INT NOT NULL DEFAULT 0

	FOREIGN KEY (idBill) REFERENCES Bill(idBill),
	FOREIGN KEY (idFood) REFERENCES Food(idFood), 
    foreign key (UserName) references Account (UserName)
)
GO



drop table billinfo

INSERT INTO dbo.Account
	(UserName ,
	DisplayName ,
	PassWord ,
	Type 
	)
VALUES
	(
	N'K9' , --UserName - nvarchar(100)
	N'RongK9' , --DisplayName - nvarchar(100)
	N'1' , --Password - nvarchar(100)
	1 --Type - int
	)


INSERT INTO dbo.Account
	(UserName ,
	DisplayName ,
	PassWord ,
	Type 
	)
VALUES
	(
	N'staff' , --UserName - nvarchar(100)
	N'staff' , --DisplayName - nvarchar(100)
	N'1' , --Password - nvarchar(100)
	0 --Type - int
	)

SELECT * FROM Account
go

alter PROC USP_GetAccountByUserName @userName NVARCHAR(100)
AS
	BEGIN
	SELECT * FROM dbo.Account WHERE UserName=@userName
	END
GO

EXEC USP_GetAccountByUserName @userName=N'K9'

SELECT * FROM dbo.Account WHERE UserName = N'K9' AND PassWord='1'


	--thêm ' trong '' để lỗi
	SELECT * FROM dbo.Account WHERE UserName= N'' OR 1=1-- '

CREATE PROC USP_Login @userName NVARCHAR(100),@passWord NVARCHAR(100)
AS
	BEGIN
	SELECT * FROM dbo.Account WHERE UserName=@userName AND PassWord=@passWord
	END
GO

SELECT * FROM dbo.Account WHERE UserName = '' AND PassWord = N'' OR 1=1--'



--DÙNG VÒNG LẶP THÊM DỮ LIỆU BÀN

DECLARE @I INT=0

WHILE @I <=10
BEGIN
	INSERT INTO DBO.TABLEFOOD (NAME) VALUES (N'BÀN ' + CAST(@I AS NVARCHAR(100)))--DÙNG NỐI CHUỖI ->KÊT NỐI VS I--DÙNG CAST ĐỂ ĐỔI I QUA STRING
	SET @I=@I+1
END



--TẠO PROC thêm bàn
CREATE PROC USP_GetTableList
AS
	SELECT * FROM TABLEFOOD
GO

EXEC USP_GetTableList

update TABLEFOOD SET STATUS = N'CÓ NGƯỜI' WHERE IDTABLEFOOD = 1


--TẠO PROC thêm foodcategory
INSERT INTO	foodcategory VALUES
(N'Thịt Heo'),
(N'Thịt Bò'),
(N'Nước'),

(N'Hải Sản')

--thêm món
INSERT INTO	food VALUES 
(N'Cơm chiên thập cẩm' ,1,55000),
(N'Mì ý sốt bò' ,3,50000),


(N'Cơm chiên hải sản' ,1,55000),
(N'Cơm Phần heo quay' ,2,45000),
(N'Cơm chiên bò lúc lắc' ,3,50000),
(N'Nước suối' ,4,10000)

--thêm bill
INSERT INTO	bill VALUES
(GETDATE() , NULL , 1 ,0),
(GETDATE() , NULL , 2 ,0),
(GETDATE() , GETDATE() , 2 ,1)

-- thêm billinffo
INSERT INTO	billinfo VALUES
('K9',1,1,2),
('K9',1,3,4),
('K9',1,5,1),
('K9',2,1,2),
('K9',2,6,2),
('K9',3,5,2)





select * from bill
go
select * from billinfo
go
select * from food
go
select * from foodcategory
GO

SELECT * FROM tablefood

select idbill from bill where idtablefood = 2 and status = 0

-- từ idbill lấy được list bill

select * from billinfo where idbill = 2




create Table Store
(
	UserName NVARCHAR(100) ,
	Material NVARCHAR(100),
	DateIn date ,
	Dateexpired date,--ngày hết hạn
	priceIn float ,
	amount int,
	category NVARCHAR(100)
	
	primary key (username , material),
	FOREIGN KEY (username) REFERENCES dbo.account(username)
)

create table salary
(
	UserName NVARCHAR(100),
	Type INT NOT NULL,
	WORKDAY int,
	restday int,
	wagelevel float,
	bonus float,

	punish float,
	total float,
	
	primary key (username , Type),
	foreign key (username) references dbo.account(username)
)

--alter table salary
--add constraint fk_type
--foreign key (type)
--references account(type)



drop table salary 


--hiển thị chi tiết hóa đơn bàn
SELECT F.NAME , BI.COUNT , F.PRICE,F.PRICE * BI.COUNT AS [TOTALPRICE] FROM BILLINFO BI ,BILL B,FOOD F WHERE BI.IDBILL=B.IDBILL AND BI.IDFOOD=F.IDFOOD AND B.IDTABLEFOOD = 2

<<<<<<< Updated upstream

--bai 11 them xoa hoa don
CREATE PROC USP_INSERTBILL
@IDTABLEFOOD INT
AS
	BEGIN
	INSERT INTO BILL VALUES (GETDATE(),NULL,@IDTABLEFOOD,0)
	END

--///////THÊM DISCOUNT B13
ALTER PROC USP_INSERTBILL
@IDTABLEFOOD INT
AS
	BEGIN
	INSERT INTO BILL VALUES (GETDATE(),NULL,@IDTABLEFOOD,0,0)
	END

CREATE PROC USP_InsertBillInfo
@TEN nvarchar(100),@idBill int , @idfood int , @count int
as
	BEGIN
	INSERT INTO BILLINFO VALUES(@TEN,@IDBILL,@IDFOOD,@COUNT)
	END

exec USP_InsertBillInfo 'k9',4,3,1

select max(idbill) from bill

drop proc USP_InsertBillInfo




ALTER PROC USP_InsertBillInfo
@TEN nvarchar(100),@idBill int , @idfood int , @count int
AS
	BEGIN
	DECLARE @isExitsBI int;
	DECLARE @FOODCOUNT INT =1

	select @isExitsBI = IDBILLINFO,@FOODCOUNT = B.COUNT from billinfo AS B where idBill=@idBill and idfood=@idfood 

	IF (@isExitsBI >0)
	BEGIN 
		DECLARE @NEWCOUNT INT = @FOODCOUNT + @COUNT
			IF(@NEWCOUNT >0)
			UPDATE BILLINFO SET COUNT = @FOODCOUNT+@COUNT WHERE idfood=@idfood 
			ELSE
			DELETE BILLINFO WHERE IDBILL=@IDBILL AND IDFOOD=@idfood
	END
	ELSE
	BEGIN
		INSERT INTO BILLINFO VALUES(@TEN,@IDBILL,@IDFOOD,@COUNT)
	END
	END
		



CREATE PROC USP_InsertBillInfo2
@idBill int , @idfood int , @count int
as
	BEGIN
	INSERT INTO BILLINFO VALUES('k9',@IDBILL,@IDFOOD,@COUNT)
	END
=======
--Quên Mật khẩu
CREATE proc forgetPass 
@username_input nvarchar(100)
AS
BEGIN
	SELECT @username_input FROM Account WHERE @username_input = UserName
END


exec forgetPass ''
go

select * from account

DROP FUNCTION forgetPass
drop proc forgetPass

--Sửa Mật Khẩu khi nhập đúng username
create proc updateAccount
@password nvarchar(100),
@userName nvarchar(100)
as
begin
    update Account
    set
        PassWord = @password
    where UserName = @userName
end
go

drop proc updateAccount
>>>>>>> Stashed changes

--bài 12
UPDATE BILL SET STATUS = 1 WHERE IDbill = 14

--khi bill hoặc bill info cập nhật thông tin có 2 TH :
--TABLE có bill -> có billinfo và bill chưa được thanh toán -> bàn đó có người
--Table ko có bill -> trống
-- khi update bill và billinfo
--> 2TH :th1 thêm billinfo -> bàn có người 
--> th2:xóa bớt ->bill vẫn nằm đó ->bàn vẫn có người nhưng ko có món 
-->th trống là khi thanh toán -> update bill

--tạo trigger insert update billinfo -> cập nhật thông tin thành bàn có người 
--tạo trigger update bill ->cập nhật thông tin trống

create TRIGGER UTG_UPDATEBILLINFO
ON BILLINFO FOR INSERT , UPDATE
AS
	BEGIN
		DECLARE @IDBILL INT
		SELECT @IDBILL = IDBILL FROM inserted
		DECLARE @IDTABLE INT
		SELECT @IDTABLE = IDTABLEFOOD FROM BILL WHERE IDBILL=@IDBILL AND STATUS =0 --CHƯA CHECK "0"

		DECLARE @COUNT INT
		SELECT @COUNT = COUNT(*) FROM BILLINFO WHERE IDBILL = @IDBILL

		IF(@COUNT>0)
		UPDATE TABLEFOOD SET STATUS = N'CÓ NGƯỜI' WHERE IDTABLEFOOD =@IDTABLE
		ELSE
		UPDATE TABLEFOOD SET STATUS = N'Trống' WHERE IDTABLEFOOD =@IDTABLE
	END




select status from tablefood

DELETE BILLINFO
DELETE BILL



SELECT COUNT(*) FROM BILL WHERE IDTABLEFOOD = 2 AND STATUS = 0

	DROP TRIGGER UTG_UPDATEBILL

create TRIGGER UTG_UPDATEBILL
ON BILL FOR INSERT, UPDATE 
AS
	BEGIN

	DECLARE @IDBILL INT
	SELECT @IDBILL = IDBILL FROM INSERTED
	DECLARE @IDTABLE INT
	SELECT @IDTABLE = IDTABLEFOOD FROM BILL WHERE IDBILL=@IDBILL
	DECLARE @COUNT INT =0
	SELECT @COUNT=COUNT(*) FROM BILL WHERE IDTABLEFOOD = @IDTABLE AND STATUS = 0

	IF(@COUNT = 0)
		UPDATE DBO.TableFood SET STATUS = N'Trống' WHERE IDTABLEFOOD = @IDTABLE
	END

UPDATE TABLEFOOD SET STATUS = N'Trống' where IDTABLEFOOD =1


--chua add dong goi
--////////////////////////////////bai13 : chuyen ban


alter table bill
add discount int

UPDATE BILL SET DISCOUNT = 0


DECLARE @IDBILLNEW INT = 7
SELECT IDBILLINFO into IDBILLINFOTABLE FROM BILLINFO WHERE IDBILL=@IDBILLNEW

DECLARE @IDBILLOLD INT = 1

update BILLINFO SET IDBILL = @IDBILLOLD WHERE IDBILLINFO IN (SELECT * FROM IDBILLINFOTABLE)

ALTER PROC USP_SWITCHTABLE
@IDTABLE1 INT , @IDTABLE2 INT  --phải xác định idbill hoặc bàn ->TRUYỀN VÀO IDTABL
AS
	BEGIN

	DECLARE @IDFIRSTBILL INT
	DECLARE @IDSECONDBILL INT

	DECLARE @ISFIRSTTABLEEMPTY INT =1
	DECLARE @ISSECONDTABLEEMPTY INT =1

	select @IDSECONDBILL= idbill from bill where idtablefood =@IDTABLE2 and status = 0 -- lấy id mặc định

	select @IDFIRSTBILL= idbill from bill where idtablefood =@IDTABLE1 and status = 0



	IF (@IDFIRSTBILL IS NULL)--LƯU Ý  :khi so sánh vơi null là (is null)
	BEGIN
		INSERT INTO BILL VALUES (GETDATE() ,NULL ,@IDTABLE1,0,0)
		select @IDFIRSTBILL= MAX(idbill) from bill where idtablefood =@IDTABLE1 and status = 0
	END
	SELECT @ISFIRSTTABLEEMPTY = COUNT (*) FROM BILLINFO WHERE IDBILL = @IDFIRSTBILL

	IF (@IDSECONDBILL IS NULL)
	BEGIN
		INSERT INTO BILL VALUES (GETDATE() ,NULL ,@IDTABLE2,0,0)
		select @IDSECONDBILL= MAX(idbill) from bill where idtablefood =@IDTABLE2 and status = 0
	END
	SELECT @ISSECONDTABLEEMPTY = COUNT (*) FROM BILLINFO WHERE IDBILL = @IDSECONDBILL	


	SELECT IDBILLINFO INTO IDBILLINFOTABLE FROM  BILLINFO WHERE IDBILL=@IDSECONDBILL -- TẠO BẢNG CHỨA TẤT CẢ IDBILLINFO CỦA BÀN THỨ 2(bàn chuyển)


	update BILLINFO SET IDBILL = @IDSECONDBILL WHERE IDBILL = @IDFIRSTBILL-- thay đổi tất cả bill bàn thứ 1 sang bàn 2 

	UPDATE BILLINFO SET IDBILL = @IDFIRSTBILL WHERE IDBILLINFO in (SELECT * FROM IDBILLINFOTABLE)--CHUYỂN tất cả bill từ bàn 2 qua bàn 1

	DROP TABLE IDBILLINFOTABLE

	IF(@ISFIRSTTABLEEMPTY = 0)
		UPDATE TABLEFOOD SET STATUS = N'Trống' where idtablefood = @IDTABLE2

	IF(@ISSECONDTABLEEMPTY = 0)
		UPDATE TABLEFOOD SET STATUS = N'Trống' where idtablefood = @IDTABLE1

	END



	--INSERT INTO BILL VALUES (GETDATE() ,GETDATE() ,1,0,50)

	exec USP_SWITCHTABLE 3,4

CREATE PROC sp_SwitchTable
@idtable1 INT, @idtable2 INT
AS
BEGIN
	DECLARE @idBillFirst INT
	DECLARE @idBillSecond INT

	SELECT @idBillFirst = id FROM dbo.HoaDon WHERE BanSo = @idtable1 AND ThanhToan = 0
	SELECT @idBillSecond = id FROM dbo.HoaDon WHERE BanSo = @idtable2 AND ThanhToan = 0

	IF (@idBillFirst IS NOT NULL AND @idBillSecond IS NOT NULL)
	BEGIN
	    SELECT id INTO IdBillInfoTable FROM dbo.TTHoaDon WHERE SoHoaDon = @idBillFirst

	UPDATE dbo.TTHoaDon SET SoHoaDon = @idBillFirst WHERE SoHoaDon = @idBillSecond
	UPDATE dbo.TTHoaDon SET SoHoaDon = @idBillSecond WHERE id IN (SELECT * FROM dbo.IdBillInfoTable)

	DROP TABLE dbo.IdBillInfoTable
	END

	IF (@idBillFirst IS NOT NULL AND @idBillSecond IS NULL)
	BEGIN
		INSERT dbo.HoaDon
		(
		    DateCheckIn,
		    DateCheckOut,
		    BanSo,
		    ThanhToan,
		    discount
		)
		VALUES
		(   GETDATE(), -- DateCheckIn - date
		    NULL, -- DateCheckOut - date
		    @idtable2,         -- BanSo - int
		    0,         -- ThanhToan - int
		    0          -- discount - int
		    )
		SELECT @idBillSecond = MAX(id) FROM dbo.HoaDon WHERE BanSo = @idtable2 AND ThanhToan = 0
	UPDATE dbo.TTHoaDon SET SoHoaDon = @idBillSecond WHERE SoHoaDon = @idBillFirst
	DELETE dbo.HoaDon WHERE BanSo = @idtable1 AND ThanhToan = 0
	END
END
GO


--//////////////////////bai15 : thông tin cá nhân
select * from account


alter PROC USP_UPDATEACCOUNT
@USERNAME NVARCHAR (100) , @DISPLAYNAME NVARCHAR (100) , @PASSWORD NVARCHAR(100) , @NEWPASS NVARCHAR (100)
AS 
	BEGIN
		DECLARE @CORRECTPASS INT =0

		SELECT @CORRECTPASS = COUNT (*) FROM ACCOUNT WHERE @USERNAME = USERNAME AND PASSWORD = @PASSWORD

		IF (@CORRECTPASS=1)
		BEGIN
			IF (@NEWPASS = NULL OR @NEWPASS ='')
			BEGIN
				UPDATE ACCOUNT SET DISPLAYNAME=@DISPLAYNAME WHERE USERNAME=@USERNAME 
			END
			ELSE
				UPDATE ACCOUNT SET DISPLAYNAME=@DISPLAYNAME,PASSWORD = @NEWPASS WHERE  USERNAME =@USERNAME 
		END
	END


--////////////B18: INSERT , DELETE , UPDATE FOOD

SELECT * FROM FOOD

update dbo.Food SET name = N'Cơm chiên thập cẩm ' , idFoodCategory = 1 , price =1111 WHERE idFood = 1

SELECT * FROM BILLINFO

-- Tạo Trigger khi xóa hết billinfo

CREATE TRIGGER UTG_DeleteBillInfo ON BILLINFO FOR DELETE
AS
	BEGIN
		DECLARE @IDBILLINFO INT
		DECLARE @IDBILL INT
		SELECT @IDBILLINFO=IDBILLINFO , @IDBILL=IDBILL  FROM deleted

		DECLARE @IDTABLE INT
		SELECT @IDTABLE=idTableFood FROM BILL WHERE idBill=@IDBILL

		DECLARE @COUNT INT = 0;
		SELECT @COUNT = COUNT (*) FROM BILLINFO BI , BILL B WHERE B.IDBILL = @IDBILL AND BI.IDBILL = B.IDBILL AND STATUS = 0

		IF (@COUNT=0)
			UPDATE TABLEFOOD SET STATUS = N'Trống' WHERE idTableFood = @IDTABLE
	END

-- Thống kê BillInfo
CREATE PROC [dbo].[SP_TOTALBILL] (@FROMDATE DATE, @TODATE DATE)
AS
BEGIN
	SELECT DATECHECKIN,DATECHECKOUT, SUM(BF.COUNT*PRICE) AS [TONGTIEN]
	FROM BILL B, BILLINFO BF, FOOD F
	WHERE @FROMDATE <= DATECHECKIN AND @TODATE >= DATECHECKOUT 
		AND B.IDBILL = BF.IDBILL 
		AND BF.IDFOOD = F.IDFOOD
	GROUP BY DATECHECKIN,DATECHECKOUT
	
END
GO

--Thống kê Store
CREATE PROC [dbo].[SP_STATISTICALSTORE] (@DATEIN DATE) AS
BEGIN
	SELECT DATEIN AS [NGÀY NHẬP], SUM(PRICEIN*AMOUNT) AS [TỔNG TIỀN]
	FROM STORE
	WHERE @DATEIN <= DATEIN
	GROUP BY DATEIN
END
GO

--19

--########## HÀM CHUYỂN ĐỔI CỤM TỪ, TỪ CÓ DẤU SANG KHÔNG DẤU
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) 
RETURNS NVARCHAR(4000) 
AS 
	BEGIN 
	IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput 
	
	DECLARE @RT NVARCHAR(4000) 
	DECLARE @SIGN_CHARS NCHAR(136)
	DECLARE @UNSIGN_CHARS NCHAR (136) 
	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) 
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
	DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) 
		BEGIN 
		SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
		BEGIN 
		IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
		BEGIN 
		IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
		ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK 
		END SET @COUNTER1 = @COUNTER1 +1 
		END SET @COUNTER = @COUNTER +1 
		END SET @strInput = replace(@strInput,' ','-') 
		RETURN @strInput 
		END

select * from food where name like N'%CơM%'

select * from food where dbo.fuConvertToUnsign1(name) like N'%COM%'

SELECT * FROM Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'com') + '%'








