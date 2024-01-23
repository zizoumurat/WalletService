# Cüzdan Uygulaması

Uygulamada TransactionApi ve Walletapi isimlerinde microservice'ler bulunmaktadır.


TransactionApi:
 - Clean architecture mimarisinde geliştirilmiştir.
 - Mongodb veritabanını kullanarak, hesap hareketlerinin kayıt ve güncelleme işlemlerini gerçekleştiren apidir.
 - docker-compose up -d ile ayağa kaldırıldığında http://localhost:4003/swagger/index.html adresi üzerinden swagger'a erişilebilmektedir.

WalletApi:
 - Clean architecture mimarisinde geliştirilmiştir.
 - Postgresql veritabanını kullanarak, cüzdanların kayıt, güncelleme ve silme işlemlerini gerçekleştiren apidir.
 - docker-compose up -d ile ayağa kaldırıldığında http://localhost:4001/swagger/index.html adresi üzerinden swagger'a erişilebilmektedir.


ApiGateWay:
- Apigateway/Ocelotapi projesi microservislere tek bir kaynaktan erişebilmek amacıyla geliştirilmiştir
- gelen istekleri ilgili mikroservicelere yönlendirmektedir.
- docker-compose up -d ile ayağa kaldırıldığında http://localhost:6001/swagger/index.html adresi üzerinden swagger'a erişilebilmektedir.

Client:
- docker-compose up -d ile ayağa kaldırıldığında http://localhost:4200 adresinden erişilebilmektedir.
- http://localhost:4200/wallets adresinde kayıtlı cüzdanlar görüntülünebilmekte, yeni cüzdan oluşturulabilmektedir.
- Para yatırma / çekme işlemleri bu sayfada gerçekleştirilir.
- http://localhost:4200/transactions adresinde hesap hareketleri görüntülenmeketdir. Listenin başında yer alan dropdowndan seçim yapılarak ilgili cüzdana ait hesap hareketleri görüntülenir. 

Para yatırma / çekme işlemleri ve hata durumunun yönetilmesi:
- Para yatırma / çekme işlemi başlatıldığında,  transactions tablosuna statusu "Pending" olan bir kayıt bırakılır. Kuyruğa bu kayıt ile ilgili mesaj bırakılır.
- Wallet api bu mesajı yakalar ve ilgili işlemin miktarı kadar cüzdana para ekler ya da çıkartır.
- Bu işlem hatalı olursa; kuyruğa mesaj bırakılır ve transaction api bu mesajı alarak ilgili transactionın statüsünü "Failed" olarak günceller.
- İşlem başarılı olursa; kuyruğa mesaj bırakılır ve transaction api bu mesajı alarak ilgili transactionın statüsünü "Complated" olarak günceller.
- İşlem sonucu kullanıcıya SignalR aracılığıyla iletilir.

Notlar:
- kök dizinde "docker-compose up -d" çalıştırılması yeterlidir. Migration işlemi otomatik yapılacaktır.
- monolitik yapıda UnitOfWork kullanımına örnek olması amacıyla WalletApi'ye UnitOfWork eklenmiştir.
