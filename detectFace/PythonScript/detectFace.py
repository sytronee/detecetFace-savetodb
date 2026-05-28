import cv2
import tkinter as tk
from PIL import Image, ImageTk
import requests
import threading
import urllib3
import pathlib

# SSL sertifika uyarılarını kapat
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# --- AYARLAR ---
API_URL = "https://localhost:7110/api/detection"
CAMERA_INDEX = 0

class FaceApp:
    def __init__(self, window):
        self.window = window
        self.window.title("Auto Detection")
        
        # Kamera ve Cascade Hazırlığı
        self.camera = cv2.VideoCapture(CAMERA_INDEX)
        self.camera.set(cv2.CAP_PROP_FPS, 60)
        
        casepath = pathlib.Path(cv2.__file__).parent.absolute() / "data/haarcascade_frontalface_default.xml"
        self.clf = cv2.CascadeClassifier(str(casepath))
        
        self.latest_frame = None

        # Görüntü alanı
        self.label = tk.Label(window)
        self.label.pack()

        self.update_stream()

    def update_stream(self):
        ret, frame = self.camera.read()
        if ret:
            frame = cv2.resize(frame, (640, 480))
            frame = cv2.flip(frame, 1)
            self.latest_frame = frame.copy()
            
            gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
            faces, _, levelWeights = self.clf.detectMultiScale3(gray, 1.1, 5, minSize=(80, 80), outputRejectLevels=True)

            for i, (x, y, w, h) in enumerate(faces):
                raw_confidence = levelWeights[i]
                percent_score = max(0, min(100, ((raw_confidence - 0.0) / (10.0 - 0.0)) * 100))
                
                cv2.rectangle(frame, (x, y), (x + w, y + h), (255, 255, 0), 2)
                cv2.putText(frame, f"Conf: %{percent_score:.1f}", (x, y-10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 0), 2)

                # --- OTOMATİK İŞLEM ---
                if percent_score > 80:
                    self.auto_submit(x, y, w, h, raw_confidence)
                    return # İşlem yapıldı, döngüyü kes

            img = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
            img = Image.fromarray(img)
            imgtk = ImageTk.PhotoImage(image=img)
            self.label.imgtk = imgtk
            self.label.config(image=imgtk)
            
        self.window.after(16, self.update_stream) # 60 FPS hedefi için ~16ms

    def auto_submit(self, x, y, w, h, conf):
        # Güven skorunu tekrar hesapla (log için)
        percent_score = max(0, min(100, ((conf - 0.0) / (10.0 - 0.0)) * 100))
        
        # 1. Görseli kaydet
        if self.latest_frame is not None:
            cv2.imwrite("Snapshot.jpg", self.latest_frame)
            # Burada 'Başarılı' yerine doğrudan skoru yazdırıyoruz
            print(f"Kayıt tamamlandı! Güven Skoru: %{percent_score:.1f}")
        
        # 2. API'ye gönder
        threading.Thread(target=self.send_to_api, args=(x, y, w, h, conf), daemon=True).start()
        
        # 3. Kapat
        print("İşlem tamamlandı, uygulama otomatik kapatılıyor...")
        self.window.quit()
        self.window.destroy()

    def send_to_api(self, x, y, w, h, confidence):
        min_val, max_val = 0.0, 10.0 
        percent_score = max(0, min(100, ((confidence - min_val) / (max_val - min_val)) * 100))
        payload = {
            "label": "face",
            "confidence": round(float(percent_score), 2),
            "bbox": {"x_degeri": int(x), "y_degeri": int(y), "width": int(w), "height": int(h)},
            "camera_id": str(CAMERA_INDEX)
        }
        try:
            requests.post(API_URL, json=payload, timeout=2, verify=False)
        except Exception as e:
            print(f"API Hatası: {e}")

root = tk.Tk()
app = FaceApp(root)
root.mainloop()
app.camera.release()