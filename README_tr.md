# Yüz Tanıma ve Veri Takip Sistemi

Bu proje, bir kamera kaynağından alınan görüntüleri işleyerek yüzleri tanımlayan ve tespit edilen verileri (kamera ID, zaman damgası, güven skoru ve bounding box bilgileri) bir **PostgreSQL** veritabanına otomatik olarak kaydeden uçtan uca bir sistemdir.

## 🚀 Proje Mimarisi
Sistem, konteynerleştirilmiş (Docker) yapılardan oluşmaktadır:
* **Frontend/UI:** Kamera görüntülerini işleyen Python scripti.
* **Backend API:** C# .NET Core tabanlı API, verileri yönetir.
* **Veritabanı:** PostgreSQL 15.



## 🛠 Kullanılan Teknolojiler
* **Dil:** C# (.NET Core), Python (OpenCV)
* **Veritabanı:** PostgreSQL 15
* **Orkestrasyon:** Docker & Docker Compose
* **ORM/Bağlantı:** Npgsql

## 📦 Kurulum ve Çalıştırma

### Ön Gereksinimler
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) bilgisayarınızda yüklü olmalıdır.

### Adımlar
1. **Depoyu Klonlayın:**
   ```bash
   git clone <https://github.com/sytronee/detectFace-savetodb>
   cd <proje-klasoru>
Servisleri Başlatın:
Proje klasörünün ana dizininde aşağıdaki komutu çalıştırın:

Bash

docker-compose up --build
Veritabanına Bağlanın:
DBeaver veya pgAdmin ile şu bilgilerle bağlanabilirsiniz:

Host: localhost | Port: 5432 | DB: CameraDb | User: postgres | Pass: 123

📊 Veri Yapısı
Sistem scanneddata tablosunda kamera kimliği, zaman damgası, güven skoru ve nesne kutusu (bbox) koordinatlarını saklar.
