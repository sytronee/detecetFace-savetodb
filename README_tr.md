Click [here](https://github.com/sytronee/detectFace-savetodb/blob/main/README.md) for the English document.
# Yüz Tanıma ve Veri Takip Sistemi

Bu proje, bir kamera kaynağından alınan görüntüleri gerçek zamanlı işleyerek yüz tespiti yapan ve elde edilen verileri (kamera ID, zaman damgası, güven skoru, bounding box) bir **PostgreSQL** veritabanına kaydeden uçtan uca bir sistemdir.

---

## 🏗 Proje Mimarisi
Sistem, Docker ile birbirinden izole edilmiş üç ana bileşenden oluşur:

1.  **Frontend (Görüntü İşleme):** `detectFace-savetodb/detectFace/PythonScript` altında çalışan, OpenCV tabanlı Python modülü.
2.  **Backend (API):** C# .NET Core ile geliştirilmiş, verileri veritabanına ileten ve yöneten API servisi.
3.  **Veritabanı:** PostgreSQL 15, verilerin kalıcı olarak saklandığı depolama alanı.

---

## 🛠 Teknik Gereksinimler
* **Docker Desktop:** Konteynerleri yönetmek için gereklidir.
* **Python 3.x:** Görüntü işleme scripti için gereklidir.
* **.NET 8.0 SDK:** API geliştirme ve derleme için gereklidir.
* **Veritabanı Arayüzü:** DBeaver (Veritabanı yönetim aracı)
---

## 📦 Kurulum Adımları

### 1. Projeyi Klonlayın
```bash
git clone https://github.com/sytronee/detectFace-savetodb
cd detectFace-savetodb
2. Python Ortamını Hazırlayın
Görüntü işleme scriptinin çalışması için gerekli kütüphaneleri kurun:

# Script dizinine gidin
cd detectFace/PythonScript

# Sanal ortamı oluşturun ve aktif edin
python -m venv venv
# Windows için:
venv\Scripts\activate
# Linux/Mac için:
source venv/bin/activate

# Gerekli bağımlılıkları yükleyin
pip install -r requirements.txt
3. Docker Servislerini Başlatın
Projenin ana dizinine geri dönün ve konteynerleri başlatın:

cd ../.. # Proje kök dizinine dönüş
docker-compose up --build
Bu komut PostgreSQL veritabanını, API'yi ve tüm gerekli bağımlılıkları otomatik olarak kurar.

🚀 Çalıştırma ve Kullanım
Docker servislerinin ayakta olduğundan emin olun (docker ps komutuyla kontrol edebilirsiniz).

detectFace/PythonScript dizinine tekrar girin.

Scripti çalıştırın:

python main.py # veya kullandığınız dosya adı
Script, görüntüleri işleyip http://localhost:8080 üzerindeki API'ye veri göndermeye başlayacaktır.

**Projeyi başlatmak isterseniz** detectFace-savetodb\detectFace yolundaki DetectFace kısayoluna tıklayabilirsiniz.
Kısayol, detectFace-savetodb\detectFace\Backend\detectFace_ui\detectFace_ui\bin\Debug yolundaki detectFace_ui.exe dosyasını çalıştırır.

📊 Veritabanı Yönetimi
Verileri görüntülemek veya yönetmek için (DBeaver kurulumu gereklidir.) :

Host: localhost

Port: 5432

DB Name: CameraDb

User/Pass: postgres / 123

⚠️ Önemli Not
Proje klasör yolunun tamamının sadece İngilizce karakterler (a-z, A-Z, 0-9) içerdiğinden emin olun. Klasör isimlerinde Türkçe karakter (ş, ı, ğ, ü, ç, ö) kullanılması; Docker, Python scriptleri ve dosya okuma işlemleri sırasında hatalara yol açar.

⚠️ Karşılaşılan Sorunlar ve Çözümleri
Port Çakışması: Eğer 8080 veya 5432 portu dolu uyarısı alırsanız, netstat -ano | findstr :8080 komutuyla süreci bulun ve taskkill /PID <PID_NUMARASI> /F ile sonlandırın.

Bağlantı Hatası: API veritabanını bulamıyorsa, .env veya docker-compose.yml içindeki ConnectionStrings ayarlarının Host=db olduğundan emin olun.

Dosya Yolu Hatası: Docker konteynerinde dosya bulunamadı hatası alırsanız, Dockerfile içindeki dosya kopyalama yollarının, mevcut klasör yapınızla (detectFace/Backend/...) eşleştiğini doğrulayın.

Bu proje, geliştirilmeye açıktır. Katkıda bulunmak isterseniz lütfen 'pull request' oluşturun.
