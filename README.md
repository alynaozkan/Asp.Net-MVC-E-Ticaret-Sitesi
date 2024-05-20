# Asp.Net-MVC-E-Ticaret-Sitesi

Çiçek satışını baz alan anlaşılır ve basit bir yapıya sahip bir projedir.

## Kullanılan Teknolojiler

- Asp.net MVC
- Entity Framework
- MS SQL Server
- Projede Yüklü Olan NuGet Paketleri

## Veri Tabanı İlişkileri
![image](https://github.com/ladyjfuhrer/Asp.Net-MVC-E-Ticaret-Sitesi/assets/116754739/8da701d5-f733-46bf-adbe-0634b58d4184)


## Proje İşleyişi
- Ürünlerle ilgili işlemlerin yapılması için kullanıcının admin (IsAdmin) değeri var mı ve bu 
değer true mu şeklinde kontrol edilir. Onaylanır ise işlemlere(create, delete, edit, details) izin 
verilir.
- Herhangi bir kategoriye ait bir ürünü "Sepete Ekle" işleminin kullanıcının sepet sayfasına 
yönlendirilmesi için kullanıcının siteye kayıtlı mı bilgisi sorgulanır.
Eğer kayıtlı değilse Register sayfasına yönlendirir. Kayıtlıysa ama oturum açık değilse Login 
sayfasına yönlendirir ve Hesabın aktifleşmesini sağlar. 
Hesabı açık olan kullanıcı sepete ürünleri ekleyebilir.
- Kullanıcının oturumunun açık işaretlenmesi için 'Proje altındaki' Web.Config sayfasına 
authentication mode ve timeout bilgisi eklenmiştir.
- Hesaptan çıkış yapmak isteyen kullanıcı navbarda bulunan user iconunun altındaki drop down 
kısmında bulunan Hesap sayfasından çıkış yapabilir.
- Sepete ürün ekleyen kullanıcı sipariş oluşturmak isterse ilgili butona tıkladığında öncelikle 
adres bilgisi var mı diye kontrol edilir, eğer varsa SecilenAdresiSec görünümüne 
yönlendirilerek teslimat adres bilgisini seçmesi istenir. Sayfada bulunan "Onayla ve Tamamla" 
butonu ile Siparisler/Index sayfasında kullanıcı oluşturduğu sipariş bilgilerini görüntüleyebilir.
Aynı zamanda admin oluşturulmuş siparişleri görüntüleyebilir.
- Data entity model kullanıldığı için UserInRole üzerinden değil Authorize ve ilgili kodlarla 
kullanıcı sadece kendi oturumu üzerinden ilgili işlemler yapıp görüntüler.
AdminPaneli görünüm sayfasında ise AdminController üzerinden kişiler listelenir

## Sitenin Arayüzü
![image](https://github.com/ladyjfuhrer/Asp.Net-MVC-E-Ticaret-Sitesi/assets/116754739/5c9991ff-cbd2-467c-9aa8-e80fda5aefd1)
![image](https://github.com/ladyjfuhrer/Asp.Net-MVC-E-Ticaret-Sitesi/assets/116754739/d7258959-70d1-45eb-8cd8-eb4b292b4daf)


