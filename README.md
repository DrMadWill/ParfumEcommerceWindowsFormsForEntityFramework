# Parfum Ecommerce Windows Forms For EntityFramework
> Bu repo mənim Windows Forms,MsSQL və Entity Framework istifadə bacarğımı göstərmək üçün yaradılmış lahiyyədir.Parfum satışı üçün hazırlanmış bir appdir. 2 modeli vardır.Biri bu (Entity Framework) model digəri isə [ADO.Net Version](https://github.com/DrMadWill/ParfumEcommerceWindowsFormsForADONetConnectedAndDisconneced) Bir Ecommerce saytina əsaslanaraq yardımışdır. App DataBese First-ə əsaslandığı üçün əsas olaraq live dataya önəm verir.
> 3 versiyası var və hər biri ayrıcalıqda bir appdir.
1. Admin panel  
2. Employee
3. User

## Admin panel 
> Userlərin və Employee şatışına nəzarət etmək,Userlərin və Employeenin accesslərini dəyərləndirmək üçün və lazım gəldikdə isə dəyişikliklər etmək(Delete and Update) üçün fikirləşilib.Təbiki Parfum yaratam və satış edə bilir. Admin Panel özü ayrıcalıqda bir appdir nəticə etibarı ilə təklikdə Admin Panel işlədilə bilir .
- Admin Paneldə qeydiyatdan keçmək mükündeyildir.
- Adminlər DataBase dən daxil edilir.
- Admin öz adı ilə satış etməsi mükün deyilir.(Admin Panelin məqsədi nəzarət üçün olduğunu diqqətə alaq.)
- Yalnız admin yeni `category`alar yarda və update bilər.
- Yalnız admin `Parfüm`ləri silə bilər.
- Yalnız admin (`Sale Price`) satış qiymətlərini silə bilər.
- Yalnız admin başqa şəxlərin adina olan (`Sale`)satışları editləyib vaxtini,miqdarını və qiyməti dişə bilər.
- Yalnız admin müəyyən vaxt aralığında olan `user` və `employee` (`Sale`) satış keçmişinə baxa bilər.
- Yalnız admin (`Parfum App`)də qeydiyyatdan keçən şəxslərə icazə verə bilər. 
- Yalnız admin qeydiyatdan keçmiş alıcıları(`User`) olan şəxləri (`employee`) işci olaraq daxil olmasına icazə verə bilər.
- Yalnız admin (`User`)alıcı və (`Employee`)işci (`Password`)sifrəsini editləyə bilər.
- [Preview](https://youtu.be/VxsUVER95KA)

## Employee
> Userlərin satışına baxmaq, Yeni parfumlərin yardılması və parfümlərin qəblulunu edə bilir və Satış edir.
- (`User`) alıcının müəyyən aralıqda nə aldığına baxa bilir.
- `Parfum` `create`yarada və `update`editləyə bilir.
- `Parfum`ə müəyyən qiymət verə bilir(`create`) və bu qiymətləri (`update`)editləyə bilir.
- `Parfum`ləri müəyən `category` lərə add edə bilir.
- (`Sale`) edə bilir.
- [Preview](https://www.youtube.com/watch?v=Y7aq-6C8eL8)

## User
> Satışda olan bütün parfümləri görür və alış edə bilir.Satışda olan bütün pafümləri axatarış edə bilir. Yardalışmış bütün parfümləri görür.
 - [Preview](https://www.youtube.com/watch?v=vjW_TNCPU1M)

## DataBase
> Database olaraq **MsSQL**dən istifadə eilib. Atfter delete və update triggerlərindən,Function,Procedure və Viewlardan istifadə edilib. Yuxarıda qeyd etiklərimdən bir neçəsini izahanı verirəm.After Updated Triggeri satış olduqda bu satışın miqdarı qədər `SalePrice Table`dan silinir.Əgər satış silinsə bu satış miqdarı qədər parfum `SalePrice Table`na qayıdır.DataBase gedən bütün əməliyyatlar `Parfume.sql` file'dadır.
#### Using Technology
- Trigger(After Delete and Insert)
- Function
- View
- Join
- Foreign Key
### Database Schema
<img src="https://i.postimg.cc/9XBJdgTn/Parfums-DBDesigners.png">

