# Asp.Net-MVC-E-Ticaret-Sitesi

Çiçek satışını baz alan anlaşılır ve basit bir yapıya sahip bir projedir.

## Kullanılan Teknolojiler

- Asp.net MVC
- Entity Framework
- MS SQL Server
- Projede Yüklü Olan NuGet Paketleri

## Veri Tabanı İlişkileri
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189343222015733801/image.png?ex=659dd131&is=658b5c31&hm=f0108f5fc46d88543eba0888ed46b5742a2f1f5e25dad6c9321600053d1d3c92&">
</img>

## Veri Tabanı Table Bilgileri
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189345702627774604/image.png?ex=659dd380&is=658b5e80&hm=03a9279c31d8c7f411f795a0965614765eb176300c45954d459299dabb3a0dc3&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189345909981589544/image.png?ex=659dd3b1&is=658b5eb1&hm=4942be4c9386fc96c1135e8cb29924ab0393f196a211036b26e47fbc7bdec90e&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189346174159818862/image.png?ex=659dd3f0&is=658b5ef0&hm=16959b2ba933326ae4bde313c3bc01d6df6a75b62b2e007546e528f7673580a8&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189346541232717845/image.png?ex=659dd448&is=658b5f48&hm=2e2b86faa82ba33d7ca1d8f8676e19a2c1bbb1a0b597e8a45b62ac2f1ba03453&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189346272444944434/image.png?ex=659dd408&is=658b5f08&hm=4f92fa0459655275388dedbb4141dbc229c16cc6c4880b63905b278b545ee790&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189346366678384682/image.png?ex=659dd41e&is=658b5f1e&hm=2d0f6bdea6fe942ee0c4fcef84554cbaba1c76798ca3c2dd03f9fd59503e0e73&">
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
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189344138303373412/image.png?ex=659dd20b&is=658b5d0b&hm=21034706bce1b567fa98746136a146c40351ce2d2963159c69f7973766029054&">
</img>
<img src="https://cdn.discordapp.com/attachments/1185269586086608966/1189344344965124126/image.png?ex=659dd23c&is=658b5d3c&hm=c178a25ca3d1b47c3c7a4c515d48aa619302e7ec870e6afd4479a115ed52c835&">
</img>

