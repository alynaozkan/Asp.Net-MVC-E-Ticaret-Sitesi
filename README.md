# Asp.Net-MVC-E-Ticaret-Sitesi

Çiçek satışını baz alan anlaşılır ve basit bir yapıya sahip bir projedir.

## Kullanılan Teknolojiler

- Asp.net MVC
- Entity Framework
- MS SQL Server
- Projede Yüklü Olan NuGet Paketleri

## Veri Tabanı İlişkileri
<img src="https://media.discordapp.net/attachments/1185269586086608966/1189343222015733801/image.png?ex=663221f1&is=6630d071&hm=a186e625be28112e3d132e1fa12cb7bb7b0ee2d2eb6b6530d56b8a360a83b5e2&=&format=webp&quality=lossless&width=1115&height=676">
</img>

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
<img src="https://media.discordapp.net/attachments/1185269586086608966/1189344138303373412/image.png?ex=663222cb&is=6630d14b&hm=76f308c167d49e6af1183dbbfef00f9db64b5adc6fec712f25ac8af489555ebd&=&format=webp&quality=lossless&width=1440&height=673">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189344344965124126/image.png?ex=663222fc&is=6630d17c&hm=b11be8552fc71e0ca238e0360ec0be126bd88dce5f3d33a5eb28b5d685628e7b&">
</img>

