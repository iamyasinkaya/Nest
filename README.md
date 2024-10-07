# Nest Otel ve Raporlama Sistemi

Bu proje, .NET Core kullanılarak geliştirilmiş ve Soğan Mimarisi (Onion Architecture) desenini takip eden bir otel yönetimi ve raporlama sistemidir. İki ana servisten oluşmaktadır: Otel Servisi ve Rapor Servisi.

## Proje Yapısı

Proje, kaygıların ayrılmasını ve bakım kolaylığını destekleyen Soğan Mimarisi'ni takip etmektedir. Ana katmanlar şunlardır:

- Domain (Alan)
- Application (Uygulama)
- Infrastructure (Altyapı)
- API (Otel Servisi ve Rapor Servisi)

## Servisler

### Otel Servisi
Oda rezervasyonları, misafir bilgileri vb. otel ile ilgili işlemleri yönetir.

### Rapor Servisi
Otel verilerine dayalı raporlar oluşturur ve yönetir.

## Kullanılan Teknolojiler

- .NET Core
- Entity Framework Core
- RabbitMQ (mesajlaşma için)
- MySQL (veritabanı için)

## Kurulum

1. Depoyu (repository) klonlayın
2. .NET Core SDK'nın yüklü olduğundan emin olun
3. MySQL ve RabbitMQ'yu yükleyin

### Veritabanı Kurulumu

Package Manager Console'da aşağıdaki komutları çalıştırın:

```
Add-Migration InitialCreate -Context ApplicationReportDbContext -Project Nest.Infrastructure -OutputDir Migrations/Reports
Add-Migration InitialCreate -Context ApplicationHotelDbContext -Project Nest.Infrastructure -OutputDir Migrations/Hotels 
Update-Database -Context ApplicationReportDbContext
Update-Database -Context ApplicationHotelDbContext
```

### Konfigürasyon

Her iki servisin de `appsettings.json` dosyalarını veritabanı ve RabbitMQ kimlik bilgilerinizle güncelleyin:

```json
{
  "ConnectionStrings": {
    "HotelDb": "Server=localhost;Database=hotel_db;User ID=root;Password=Şifreniz;",
    "ReportingDb": "Server=localhost;Database=reporting_db;User ID=root;Password=Şifreniz;"
  },
  "RabbitMQSettings": {
    "HostName": "localhost",
    "UserName": "RabbitMQKullanıcıAdınız",
    "Password": "RabbitMQŞifreniz",
    "Queues": [
      "report-requests"
    ]
  }
}
```

## Uygulamayı Çalıştırma

1. RabbitMQ servisini başlatın
2. Otel Servisini çalıştırın
3. Rapor Servisini çalıştırın

## API Uç Noktaları



## Katkıda Bulunma

Lütfen davranış kurallarımız ve pull request gönderme süreci hakkında bilgi almak için CONTRIBUTING.md dosyasını okuyun.

## Lisans

Bu proje MIT Lisansı altında lisanslanmıştır - detaylar için LICENSE.md dosyasına bakın.