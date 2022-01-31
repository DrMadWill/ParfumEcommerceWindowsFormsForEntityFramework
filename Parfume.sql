
create view MidDetalParfume
as
select Parfume.Id,Parfume.Name,Parfume.Description,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density'
from Parfume.dbo.Parfume 
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

select * from MidDetalParfume

create view FullDetailParfum
as
select Parfume.Id,Parfume.Name,Parfume.Image,Parfume.Description,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density',Size.Size,y.Price,y.number 'Number' 
from ParfumeToSelePrice ps 
Join SalePrice y on ps.SalePriceId=y.Id
Join Size on y.SizeId=Size.Id
Join Parfume on ps.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

select Name from Density
select Name from Brend
select Name from Gender

select * from FullDetailParfum order by Id Asc

create procedure usp_AddParfum
@Name nvarchar(150),
@Descriptio ntext,
@Image varchar(100),
@Brend nvarchar(150),
@Gender nvarchar(10),
@Density nvarchar(70)
as
Insert into Parfume(Name,Image,Description,BrendId,GenderId,DensityId)values(@Name,@Image,@Descriptio,(select Id from Brend where Name=@Brend),(select Id from Gender where Name= @Gender),(select Id from Density where Name=@Density))

EXECUTE usp_AddParfum @Name,@Image,@Descriptio,@Brend, @Gender,@Density


Insert into Parfume(Name,Image,Description,BrendId,GenderId,DensityId)values('Silver Mountain Water','No Img','This is Grate Parfum',(select Id from Brend where Name='Creed'),(select Id from Gender where Name= 'Unisex'),(select Id from Density where Name='Eau De Parfum'))

select Size.Size,y.Price,y.number 'Number' 
from SalePrice y
Join Size on y.SizeId=Size.Id


create procedure usp_ShowMeCatogoryParfumId
@Id int
as
select * from FullDetailParfum where Id in (select ParfumId from CatogoryToParfum where Catogoryİd=@Id)

EXECUTE usp_ShowMeCatogoryParfum 'Ekskluziv'

EXECUTE usp_ShowMeCatogoryParfumId 2

select * from FullDetailParfum where Id in (select ParfumId from CatogoryToParfum where Catogoryİd=(select Id from Catogory where Name='Yeni'))

select * from FullDetailParfum where Id in (select ParfumId from CatogoryToParfum where Catogoryİd=1)


Insert into Parfume.dbo.Brend(Name,Decription)values('','')

create view SearchHead
as
select y.Id,y.Brend+' '+y.[Name] as 'Header' from MidDetalParfume y


select * from SearchHead
SELECT ROW_NUMBER() OVER(ORDER BY Name) AS MyIndex
FROM MidDetalParfume

select ROW_NUMBER() OVER() as 'number',Name from MidDetalParfume
select * from MidDetalParfume


select * from SearchHead

select ROW_NUMBER() OVER(ORDER BY Name) as 'Index',Name from Brend


create procedure usp_UpdateParfum
@Id int,
@Name nvarchar(150),
@Image image,
@Descriptio ntext,
@Brend nvarchar(150),
@Gender nvarchar(10),
@Density nvarchar(70)
as
Update Parfume set Name = @Name,Image=@Image,Description=@Descriptio,BrendId=(select Id from Brend where Name=@Brend),GenderId=(select Id from Gender where Name= @Gender),DensityId =(select Id from Density where Name=@Density) where Id =@Id

EXECUTE usp_UpdateParfum @Id=1 ,@Name = 'Miss Dior',@Image = 'No Image',@Descriptio='This Is Great Prarfum',@Brend='Christian Dior', @Gender='Female',@Density='Eau De Parfum'
EXECUTE usp_UpdateParfum @Id=1 ,@Name = 'Miss Dior',@Descriptio='This Is Great Prarfum',@Brend='Christian Dior', @Gender='Female',@Density='Eau De Parfum'
create procedure usp_DeleteParfum
@Id int
as
Delete Parfume.dbo.CatogoryToParfum where ParfumId = @Id
Delete Parfume.dbo.ParfumeToSelePrice where ParfumId = @Id
Delete Parfume.dbo.Parfume where Id = @Id

EXECUTE usp_DeleteParfum 6 

select * from Parfume.dbo.ParfumeToSelePrice
select * from Parfume.dbo.CatogoryToParfum
select * from MidDetalParfume

select * from SalePrice


Delete Parfume.dbo.ParfumeToSelePrice  where ParfumId=6

create trigger trgAfterDeletetSale on
Parfume.dbo.ParfumeToSelePrice  
for Delete
as
declare @Id int;
select @Id = u.SalePriceId from deleted u
Delete Parfume.dbo.SalePrice where SalePrice.Id = @Id
Print(' After Delete Trigger Sale Price')
go

select * from FullDetailParfum

select * from Size
ALTER TABLE Brend
ADD UNIQUE (Name);

delete Brend where Id = 24

select y.Id,Size.Size,y.Price,y.number 
from SalePrice y
Join Size on y.SizeId = Size.Id

create procedure usp_InsertSalePrice
@Size int ,
@Price int,
@Number int
as
Insert into SalePrice(SizeId,Price,number) values((select Id from Size where Size.Size=@Size),@Price,@Number)

EXECUTE usp_InsertSalePrice @Size= ,@Price= ,@Number =

create procedure usp_InsertParfumToSale
@ParfumId int,
@Size int ,
@Price int,
@Number int
as
insert into ParfumeToSelePrice(ParfumId,SalePriceId)values(@ParfumId,(select Id from SalePrice where SizeId=(select Id from Size where Size.Size=@Size) and Price=@Price and number=@Number ) )

EXECUTE usp_InsertParfumToSale @ParfumId=,@Size =,@Price =,@Number=

select Id from SalePrice where SizeId=(select Id from Size where Size.Size=30) and Price=18 and number =5  
select * from SalePrice

select Name from Brend group by (Name)

select * from SearchHead order by Header




create view FullDetailParfum
as
select Parfume.Id,Parfume.Name,Parfume.Description,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density',Size.Size,y.Price,y.number 'Number' 
from  SalePrice y
Join Size on y.SizeId=Size.Id
Join Parfume on y.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

create view SalePirceUI
as
select Size.Size,y.Price,y.number,y.ParfumId,y.Id from SalePrice y
Join Size on y.SizeId=Size.Id

create procedure usp_SelectIdSalePirce
@Id int
as
select y.Size 'SizeML',y.Price,y.number 'Count',y.Id from SalePirceUI y where ParfumId = @Id

EXECUTE usp_SelectIdSalePirce 9

create procedure usp_DeleteParfum
@Id int
as
Delete Parfume.dbo.CatogoryToParfum where ParfumId = @Id
Delete Parfume.dbo.SalePrice where ParfumId = @Id
Delete Parfume.dbo.Parfume where Id = @Id


create procedure usp_InsertSalePrice
@ParfumId int,
@Size int ,
@Price int,
@Number int
as
Insert into SalePrice(SizeId,Price,number,ParfumId) values((select Id from Size where Size.Size=@Size),@Price,@Number,@ParfumId)

EXECUTE usp_InsertSalePrice @ParfumId= 9, @Size=30 ,@Price=75 ,@Number =3


EXECUTE usp_InsertSalePrice @Size= ,@Price= ,@Number =@ParfumId


create view DeleteSalePirceUI
as
select y.Id,Size.Size,y.Price,y.number,y.ParfumId from SalePrice y
Join Size on y.SizeId=Size.Id

select * from DeleteSalePirceUI y where y.ParfumId=1 

Delete SalePrice where Id =

Insert into Size(Size)values()

select Size.Size from Size

alter table Size  add unique(Size)

Delete Size where Id=5



Create Function Fn_ToplamaYap(@sayi1 int,@sayi2 int)
Returns bit
As
Begin
Declare @IsAdded bit
Set @IsAdded = @sayi1+ @sayi2
return @IsAdded
End


select y.Id,y.Name,y.Image,y.Description,y.Brend,y.Gender,y.Density,y.Size,y.Price,y.Number from FullDetailParfum y

select * from DeleteSalePirceUI y where y.ParfumId=

create procedure usp_UpdateSalePrice
@PrId int,
@Size int,
@NewSize int,
@Price int,
@Count int
as
UPDATE SalePrice set SizeId=(select Id from Size where Size=@NewSize),Price=@Price,number=@Count where ParfumId = @PrId and SizeId=(select Id from Size where Size=@Size)

EXECUTE usp_UpdateSalePrice @PrId =1  ,@Size =75 ,@NewSize =50 ,@Price =35 ,@Count =2 



select Brend from MidDetalParfume group by Brend
select * from Catogory where Name=''

delete Brend where Name=''
update Brend set Name='',Decription='' where Name=''


select * from FullDetailParfum where Id in (select ParfumId from CatogoryToParfum where Catogoryİd=(select Id from Catogory where Name='Yeni'))

create view DeleteUpdateHeader
as
select Id,CONCAT(Left(Brend,59),' ',Left(Name,40)) as Header  from FullDetailParfum group by Id
select Id,Name,Brend from FullDetailParfum group by Id

select * from DeleteUpdateHeader y where y.Id=(select ParfumId from SalePrice GROUP BY ParfumId)

select Brend.Name,ParfumId 
from SalePrice
Join Parfume on SalePrice.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId=Brend.Id
GROUP BY ParfumId

select Id,Name,COUNT(Id) from FullDetailParfum  GROUP BY Id

create view DeleteHeaders
as
SELECT DISTINCT Id,Header
FROM DeleteUpdateHeader

select * from DeleteHeaders

select Id from Catogory where Name='Qadin'

create procedure usp_InsertCategoryToParfum
@Name nvarchar(150),
@ParfumId int 
as
Insert into CatogoryToParfum(ParfumId,Catogoryİd) values ((select Id from Catogory where Name=@Name),@ParfumId)

EXECUTE usp_InsertCategoryToParfum @Name ='Qadin',@ParfumId=4 

EXECUTE usp_ShowMeCatogoryParfum 'Qadin'

EXECUTE usp_InsertCategoryToParfum @Name ='Qadin',@ParfumId=29 

sp_help

Insert into CategoToParfum(ParfumId) values (5)
delete CategoToParfum where Id=1 (select Id from Catogory where Name='Qadin'),


create table CategoryToParfum(
Id int primary key identity(1,1),
ParfumId int ,
Foreign key(ParfumId) REFERENCES Parfume(Id),
CategoryId int ,
foreign key(CategoryId) REFERENCES Catogory(Id)
)



create procedure usp_InsertCategoryToParfum
@ParfumId int,
@Category nvarchar(150)
as
insert into CategoryToParfum(ParfumId,CategoryId) values (@ParfumId,(select Id from Catogory where Name=@Category))

EXECUTE usp_InsertCategoryToParfum @ParfumId=1,@Category='Qadin'

create view DeleteUpdateCategoryToParfum
as
select y.Id 'Ids',CONCAT(Brend.Name,' ',Parfume.Name) as 'Header',Catogory.Name 'Category' 
from CategoryToParfum y
Join Catogory on y.CategoryId=Catogory.Id
Join Parfume on y.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId = Brend.Id

create procedure usp_ShowCategoryParfums
@Name nvarchar(150)
as
select * from FullDetailParfum where Id in (select ParfumId from CategoryToParfum where CategoryId =(select Id from Catogory where Name=@Name))

EXECUTE usp_ShowCategoryParfums 'Qadin'


(select Id from Catogory where Name='')
create procedure usp_RemoveCategoryToParfume
@Header nvarchar(300),
@Category nvarchar(150)
as
 Delete CategoryToParfum where Id=(select Ids from DeleteUpdateCategoryToParfum where Category=@Category and Header=@Header)

Execute usp_RemoveCategoryToParfume @Header='' ,@Category=''

insert into Catogory(Name) values('')

Update Catogory set Name='' where Name=''
create view  ParfumLoginUsers
as
select * from Parfume.dbo.Users where IsActive=1


select * from ParfumLoginUsers
select COUNT(*) from LoginUsers

delete Parfume.dbo.Sale where Id=2

insert into Parfume.dbo.Users(FullName,[Password],IsUser,IsEmployee,IsActive)values('Medi','15665',0,1,0)


update Parfume.dbo.Users set FullName='Med',[Password]='4465465',IsUser=0,IsEmployee=1,IsActive=1 where Name=''
create view ActiveUserTable
as
select * from Parfume.dbo.Users where IsActive=1 and IsAdmin=0;

select * from ActiveUserTable

update Parfume.dbo.Users set FullName='Salim',[Password]='154456',IsUser=0,IsEmployee=1,IsActive=1 where FullName = 'Salim'

delete dbo.Users where FullName =''

alter table Parfume.dbo.Users add unique(FullName)
Create View SaleActivity
as
select y.Id,Users.FullName,CONCAT(Brend.Name ,' ', Parfume.Name) 'Parfum',Size.Size,SalePrice.Price,y.[Count],y.Total,y.[Date]
from Sale y
Join Users on y.UserId=Users.Id
Join SalePrice on y.SalePriceId = SalePrice.Id
Join Parfume on SalePrice.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId = Brend.Id
Join Size on SalePrice.SizeId=Size.Id

select * from SaleActivity where FullName='Med' and [Date] Between '2022-01-10' and '2022-01-23' 



select * from SaleActivity where FullName='Med' and [Date] Between '2022-01-20' and '2022-01-20'
select distinct Parfum from SaleActivity
select distinct FullName from SaleActivity
select * from SalePrice

alter table SalePrice ADd Check(number>=0)

ALTER TABLE SalePrice
DROP CONSTRAINT CK__SalePrice__numbe__2739D489;

Update Parfume.dbo.SalePrice set number=((select number from dbo.SalePrice where Id=1)-1)  where SalePrice.Id = 1

create trigger trgAfterInsert on
dbo.Sale
for Insert
as
declare @Id int;
declare @Count int;
select @Id = u.SalePriceId from inserted u
select @Count =u.[Count] from inserted u
Update Parfume.dbo.SalePrice set number=((select number from dbo.SalePrice where Id=@Id)-@Count)  where SalePrice.Id = @Id
Print(' After Delete Trigger Sale Price')
go

create trigger trgAfterDelete on
dbo.Sale
for Delete
as
declare @Id int;
declare @Count int;
select @Id = u.SalePriceId from deleted u
select @Count =u.[Count] from deleted u
Update Parfume.dbo.SalePrice set number=((select number from dbo.SalePrice where Id=@Id)+@Count)  where SalePrice.Id = @Id
Print(' After Delete Trigger Sale Price')
go



create view SaleDetailParfum
as
select Parfume.Id,y.Id'PriceId',Parfume.Name,Parfume.Description,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density',Size.Size,y.Price,y.number 'Number' 
from  SalePrice y
Join Size on y.SizeId=Size.Id
Join Parfume on y.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

create procedure usp_SaleShowCategoryParfums
@Name nvarchar(150)
as
select * from SaleDetailParfum where Id in (select ParfumId from CategoryToParfum where CategoryId =(select Id from Catogory where Name=@Name))

EXECUTE usp_SaleShowCategoryParfums 'Qadin'

select FullName from ActiveUserTable

select * from SaleActivityMonitor where FullName='{userName}' and [Date] Between '{startTime}' and '{lasttime}' 



create view SaleActivityMonitor
as
select x.Id'SaleId',z.FullName,Parfume.Name,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density',Size.Size,x.Total/x.[Count] 'Price',x.[Count] 'Sale Count',x.Total,x.Date 
from  Sale x
Join Users z on x.UserId=z.Id
Join SalePrice y on x.SalePriceId=y.Id
Join Size on y.SizeId=Size.Id
Join Parfume on y.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

select * from SaleActivityMonitor where FullName='Med' and [Date] Between '2022-01-10' and '2022-01-23' 

Create View SaleActivitys
as
select distinct Users.FullName,y.Id,y.SalePriceId
from Sale y
Join Users on y.UserId=Users.Id
Join SalePrice on y.SalePriceId = SalePrice.Id
Join Parfume on SalePrice.ParfumId=Parfume.Id
Join Brend on Parfume.BrendId = Brend.Id
Join Size on SalePrice.SizeId=Size.Id

select distinct FullName from SaleActivitys
select * from Users
select * from SalePrice



create procedure usp_InsertSaleAdmin 
@Name nvarchar(140),
@SalePrice int,
@Count int,
@Total int,
@Date date
as
Insert into Sale(UserId,SalePriceId,[Count],Total,[Date])values((Select Id from Users where FullName=@Name),@SalePrice,@Count,@Total,@Date)

-command = $"EXECUTE InsertSales @UserName='{UserName}',@SalePriceId={PriceId},@Count={count},@Total ={total}";
Select * from Sale where ParfumId 


select * from 


create procedure usp_DeleteParfum
@Id int
as
Delete Sale  where SalePriceId in (select Id from SalePrice where ParfumId=@Id)
print('Parfum Sale Table Delete ')
Delete Parfume.dbo.CategoryToParfum where ParfumId = @Id
print('Parfum Category Table Delete ')
Delete Parfume.dbo.SalePrice where ParfumId = @Id
print('Parfum SalePrice Table Delete ')
Delete Parfume.dbo.Parfume where Id = @Id
print('Parfum Parfume Table Delete ')

create procedure usp_DeletePrice
@Id int
as
Delete Sale where SalePriceId=@Id
Delete SalePrice where Id=@Id

EXECUTE usp_DeletePrice @Id=


select * from MidDetalParfume

select * from  CategoryToParfum where ParfumId = 1

create procedure usp_AddParfum
@Name nvarchar(150),
@Image image,
@Descriptio ntext,
@Brend nvarchar(150),
@Gender nvarchar(10),
@Density nvarchar(70)
as
Insert into Parfume(Name,Image,Description,BrendId,GenderId,DensityId)values(@Name,@Image,@Descriptio,(select Id from Brend where Name=@Brend),(select Id from Gender where Name= @Gender),(select Id from Density where Name=@Density))


create view MidDetalParfumeForUpdate
as
select Parfume.Id,Parfume.Name,Parfume.Image,Parfume.Description,Brend.Name 'Brend',Gender.Name 'Gender',Density.Name 'Density'
from Parfume.dbo.Parfume 
Join Brend on Parfume.BrendId=Brend.Id
Join Gender on Parfume.GenderId=Gender.Id
Join Density on Parfume.DensityId=Density.Id

select Image from Parfume where Id=1

create view CategoryListParfum
as
select y.ParfumId,Catogory.Name
from CategoryToParfum y
Join Catogory on y.CategoryId = Catogory.Id

select Name from CategoryListParfum where ParfumId=1

EXECUTE usp_InsertCategoryToParfum @ParfumId=44,@Category='Yeni'
select distinct FullName,IsActive from SaleActivitysIsUser


Create View SaleActivitysIsUser
as
select distinct Users.FullName,y.Id,y.SalePriceId,Users.IsActive
from Sale y
Join Users on y.UserId=Users.Id
Join SalePrice on y.SalePriceId = SalePrice.Id


select * from DeleteUpdateCategoryToParfum where Category='Yeni'
select  * from SaleActivityMonitor
