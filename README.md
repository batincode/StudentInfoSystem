PostgreSQL Net Web API kullanarak bir öğrenci bilgi sistemi projesi geliştiriyorum. 
Öğrenciler, yalnızca ders aldıkları öğretmenlerin yetkisi dahilinde işlem yapabilecek. 
Öğrenciler derslere kayıt olabilecek, dersler öğretmenler tarafından sınıf bazında (örneğin, 1. sınıf 1. dönem dersi) açılacak. 
Öğretmenler, derslerine kayıtlı öğrencilere not verebilecek ve devamsızlık bilgisi ekleyebilecek; öğrenciler ise notlarını ve devamsızlık durumlarını görüntüleyebilecek.
Derslerin saatleri öğretmenler tarafından belirlenecek ve öğrenciler aynı saatteki çakışan derslere kayıt olamayacak. 
Çakışma durumunda, sistem bir uyarı verecek. Her öğrencinin bir danışman öğretmeni olacak ve seçilen dersler danışmanın onayına gidecek.
Danışman onayladıktan sonra öğrenci derslere kayıt olacak ve ders programı, saat bilgileriyle birlikte görüntülenecek.
Projede Entity Framework kullanıyorum. Repository paterni Mimari olarak Onion Architecture'ı kullanarak öğrenmeye çalışıyorum. 
Şuanda çok fazla eksiği var. bazı endpointler gerekli olmayabilir.Veritabanında güncellenmesi gerekecek Dto eklemeleri ve mapping işlemlerine ihtiyaç duydum onların güncellenmesi eklenmesi gerekli.
