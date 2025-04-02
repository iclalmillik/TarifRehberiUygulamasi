#  Tarif Rehberi Masaüstü Uygulaması

Bu projede kullanıcılar yemek tariflerini ekleyebilir, güncelleyebilir ve mevcut malzemelerine göre hangi yemekleri yapabileceklerini görüntüleyebilir.

##  Proje Amacı

- Dinamik arama ve filtreleme özelliklerine sahip kullanıcı dostu bir masaüstü uygulaması geliştirmek  
- Tarif ve malzeme yönetimi için veritabanı tasarımı ve ilişkisel yapı kurmak  
- Malzemeye göre yemek önerileri sunarak algoritma geliştirme becerisi kazanmak  


##  Kullanılan Teknolojiler

- **Programlama Dili:** C# (Windows Forms)
- **Veritabanı:** Microsoft SQL Server (MSSQL)


##  Temel Özellikler

###  Dinamik Arama

- **Tarif adına göre arama**
- **Malzeme bazlı arama** (eşleşme yüzdesine göre sıralama yapılır)

###  Tarif Önerisi

- Malzemeleri tam olan tarifler **yeşil**, eksik olanlar **kırmızı** renkte gösterilir
- Eksik tarifler için gereken malzeme maliyeti hesaplanır
- Arama ve filtreleme sırasında sürekli aktif olarak çalışır

###  Filtreleme ve Sıralama

- **Hazırlama süresi** (kısa → uzun / uzun → kısa)
- **Toplam maliyet** (artan / azalan)
- **Malzeme sayısı**, **kategori**, **fiyat aralığına** göre filtreleme

###  Tarif Yönetimi

- Tarif ekleme, düzenleme, silme
- Malzemeler tarif sırasında otomatik veritabanına eklenebilir
- Duplicate (aynı tarif) kontrolü yapılır

##  Kullanıcı Arayüzü

- **Ana Panel**: Tarif listesi (isim, kategori, süre, maliyet)
- **Detay Paneli**: Tarifin tam içeriği ve malzeme listesi
- **Arama ve filtreleme çubuğu**: Ekranın üst kısmında
- **Renk Kodlaması**: Yeşil = yapılabilir, Kırmızı = eksik malzeme
- **Menüler**: Tarif ekle/güncelle/sil


#  Recipe Guide Desktop Application
This project allows users to store recipes and view which meals can be prepared based on currently available ingredients.

##  Project Objectives

- Develop a desktop application with dynamic search and filtering features
- Enable filtering and sorting of items based on user-defined criteria
- Practice database design and algorithm development


##  Technologies Used

- Programming Language: **C# (Windows Forms)**
- Database: **MSSQL**


##  Application Features

###  Dynamic Search

- **By Recipe Name:** Users can search recipes by their name
- **By Ingredients:** Users can input available ingredients, and the app will list matching recipes based on ingredient match percentage

###  Recipe Suggestion Engine

- Recipes with all required ingredients are highlighted in **green**
- Recipes with missing or insufficient ingredients are highlighted in **red**
- For red recipes, missing ingredients and their total cost are calculated and shown

###  Sorting & Filtering

- Sort recipes by:
  - Preparation time (ascending/descending)
  - Total cost (ascending/descending)
- Filter recipes by:
  - Ingredient count
  - Category
  - Cost range

###  Recipe Management

- Add new recipes with instructions and ingredients
- Edit or delete existing recipes
- Prevent duplicate recipe entries

###  Real-Time Matching & UI Feedback

- Recipes are updated in real time as user inputs change
- Match percentage is calculated and shown
- Matching list updates dynamically with each user input

## User Interface Overview

- **Main Dashboard**: Displays list of all recipes with name, category, time, and cost
- **Recipe Detail View**: Shows full instructions and required ingredients
- **Search & Filter Bar**: Located at the top of the app
- **Color Coding**: Green = can be prepared, Red = missing ingredients
- **Menus**: Add, update, delete recipes



