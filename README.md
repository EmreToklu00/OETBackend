<a name="readme-top"></a>

# .NET CORE WEB API TEMPLATE

(EN)  
######
This repository serves as the foundational infrastructure for a backend project built on .NET Core 6.0.  
Let's begin with the necessary installations:

## Installation Steps:

1. **Visual Studio 2022**: Download and install [Visual Studio 2022](https://visualstudio.microsoft.com/en/vs/community/). This integrated development environment (IDE) is essential for working on the project.

2. **.NET 6.0**: Next, download and install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0), which is the framework this project is built upon. It provides the tools and libraries needed for developing .NET Core applications.

3. **MySQL Database and Workbench**: You'll also need a MySQL database for data storage. Download [MySQL](https://downloads.mysql.com/archives/installer/) and [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) and follow the installation instructions.

4. **ASP.NET and Web Development**: Within the Visual Studio Installer, make sure to select and install the "ASP.NET and web development" workload. This workload includes the necessary components for developing web applications using ASP.NET.

   (Note: Please note that this repository was developed with compatibility for MySQL version 8.0.29.)

5. **Clone the Project**: Clone the project to your local computer using Git with the following command:

   ```bash
   git clone https://link-to-project
   ```

6. **Set Up the Database**: In the project directory, open the model using MySQL Workbench, which is located in the "Github" folder at "Github/MySQLDatabase/oet-model.mwb." Then, select "Database" from the MySQL Workbench menus, choose "Forward Engineer," and proceed with setting up the database.

7. **Start the Project**: You can open the project using Visual Studio and start it. Launch it with F5 or navigate to the project directory and execute the following commands:

   ```bash
   cd .\WebAPI\
   ```

   ```bash
   dotnet run
   ```

## Used Packages:

- Autofac => Business
- Autofac.Extensions.DependencyInjection => Business, WebAPI
- Autofac.Extras.DynamicProxy => Business
- Castle.Core => Core
- FluentValidation => Business, Core
- Microsoft.AspNetCore.Authentication.JwtBearer => WebAPI
- Microsoft.Extensions.Configuration => Core
- Microsoft.Extensions.Configuration.Json => Core
- Microsoft.IdentityModel.Tokens => Core
- Newtonsoft.Json => Core
- Pomelo.EntityFrameworkCore.MySql => DataAccess
- System.IdentityModel.Tokens.Jwt => Core

## CORE

- Encryption, Hashing, JWT infrastructure
- Interception infrastructure
- Inversion Of Control (IoC)
- Aspect Oriented Programming (AOP) for Caching, Performance, Transaction, Validation
- Core Module
- IEntity, IDto, User, OperationClaim
- IEntityRepository (GET, GETALL, ADD, UPDATE, DELETE)
- DatabaseHelper
- Helper modules for other layers, such as IResult and IDataResult structures to be sent to the frontend side in WebAPI

## Entity

- Models for product-based entities should be placed in this layer.
- DTOs for user returns should also be placed in this layer.

## DataAccess

- Database operations should be implemented in this layer.
- I've used the IEntityRepository I've implemented with EntityFrameworkCore as follows:

   ```csharp
   namespace DataAccess.Abstract
   {
       public interface IUserDal : IEntityRepository<User>
       {
           List<OperationClaim> GetClaims(User user);
       }
   }
   ```

- The database connection is in Contrete/EntityFramework/Context/OETContext.cs. I didn't use the OnModelCreating override because I preferred the Database First approach.
- New Contrete files should be defined as EfCoreEntityRepository<User, OETContext> and IUserDal like this:

   ```csharp
   public class EfUserDal : EfCoreEntityRepository<User, OETContext>, IUserDal
   ```

   This way, simple CRUD operations will be automatically filled, and the methods in IUserDal will be inherited with override.

## Business

- Business rules are placed in this layer.
- Project-specific DependencyResolvers and validation processes are done in this layer.
- Project-specific responses are kept in this layer, avoiding magic strings.
- The interfaces used are designed to return IResult and IDataResult types in a way compatible with WebAPI's return type.

   ```csharp
   IDataResult<User> Login(UserForLoginDto userForLoginDto);
   IResult UserExist(string email);
   ```

## WEBAPI

- All configurations are done in Program.cs and appsettings.json in this layer.

- Controllers in this layer return data using the IResult and IDataResult helpers we've defined in the Core layer. This makes it easier to handle both the data and its success status, along with any messages when returning responses.

- In the "Authorize" section, we've implemented Role-based access control (RBAC) alongside JWT (JSON Web Tokens) authentication. This allows you to manage access permissions based on roles efficiently.

This document outlines the essential setup and components of the .NET Core 6.0 backend project infrastructure provided by this repository. With the required installations and a clear understanding of the project structure, you can start developing your own backend applications.

If you have any further questions or  if you have any recommendations or suggestions regarding this infrastructure, please let me know!  
Happy coding!

<p align="right"><-<a href="#readme-top">back to top</a>-></p>


#
#
(TR)  
######

Bu depo, .NET Core 6.0 üzerine kurulu bir backend projesi için temel altyapı görevi görür.  
İhtiyaç duyulan kurulumlarla başlayalım:

## Kurulum Adımları:

1. **Visual Studio 2022**: [Visual Studio 2022'yi](https://visualstudio.microsoft.com/tr/vs/community/) indirin ve kurun. Bu entegre geliştirme ortamı (IDE), projede çalışmak için gereklidir.

2. **.NET 6.0**: Sonraki adımda [.NET 6.0'ı](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) indirin ve kurun. Bu proje üzerine kurulmuş olan çerçeve, .NET Core uygulamaları geliştirmek için gereken araçları ve kütüphaneleri sağlar.

3. **MySQL Veritabanı ve Workbench**: Veri depolama için bir MySQL veritabanına ihtiyacınız olacak. [MySQL](https://downloads.mysql.com/archives/installer/) ve [MySQL Workbench](https://dev.mysql.com/downloads/workbench/) indirin ve kurulum talimatlarını izleyin.

4. **ASP.NET ve Web Geliştirme**: Visual Studio Installer içinde "ASP.NET ve web geliştirme"'yi seçin ve kurulum yapın.

   (Not: Lütfen bu depo, MySQL sürüm 8.0.29 ile uyumlu olarak geliştirilmiştir.)

5. **Projeyi Klonlayın**: Projeyi aşağıdaki komut kullanarak yerel bilgisayarınıza klonlayın:

   ```bash
   git clone https://link-to-project
   ```

6. **Veritabanını Ayarlayın**: Proje dizininde, MySQL Workbench'i kullanarak modeli açın. Model, "Github" klasöründe "Github/MySQLDatabase/oet-model.mwb" konumundadır. Daha sonra MySQL Workbench menülerinden "Database" seçeneğini seçin, "Forward Engineer" seçeneğini seçin ve veritabanını kurma işlemine devam edin.

7. **Projeyi Başlatın**: Projeyi Visual Studio kullanarak açabilir ve başlatabilirsiniz. F5 tuşunu kullanarak başlatabilir veya projenin dizinine giderek aşağıdaki komutları çalıştırabilirsiniz:

   ```bash
   cd .\WebAPI\
   ```

   ```bash
   dotnet run
   ```

## Kullanılan Paketler:

- Autofac => Business
- Autofac.Extensions.DependencyInjection => Business, WebAPI
- Autofac.Extras.DynamicProxy => Business
- Castle.Core => Core
- FluentValidation => Business, Core
- Microsoft.AspNetCore.Authentication.JwtBearer => WebAPI
- Microsoft.Extensions.Configuration => Core
- Microsoft.Extensions.Configuration.Json => Core
- Microsoft.IdentityModel.Tokens => Core
- Newtonsoft.Json => Core
- Pomelo.EntityFrameworkCore.MySql => DataAccess
- System.IdentityModel.Tokens.Jwt => Core

## CORE

- Encryption, Hashing, JWT infrastructure
- Interception infrastructure
- Inversion Of Control (IoC)
- Aspect Oriented Programming (AOP) ile Caching, Performance, Transaction, Validation
- Core Module
- IEntity, IDto, User, OperationClaim
- IEntityRepository (GET, GETALL, ADD, UPDATE, DELETE)
- DatabaseHelper
- WebAPI'de Frontend'e gönderilecek IResult ve IDataResult yapıları gibi diğer katmanlara yardımcı modüller

## ENTITY

- Ürün tabanlı varlıklar için modeller bu katmana yerleştirilmelidir.
- Kullanıcı dönüşleri için DTO'lar da bu katmana yerleştirilmelidir.

## DATA ACCESS

- Veritabanı işlemleri bu katmanda uygulanmalıdır.
- IEntityRepository ile birlikte kullanmak üzere EntityFrameworkCore ile uyguladığım IEntityRepository'yi aşağıdaki gibi kullanmalısınız:

   ```csharp
   namespace DataAccess.Abstract
   {
       public interface IUserDal : IEntityRepository<User>
       {
           List<OperationClaim> GetClaims(User user);
       }
   }
   ```

- Veritabanı bağlantısı Contrete/EntityFramework/Context/OETContext.cs içindedir. Burada OnModelCreating metot'unu kullanmadım, çünkü Database First yaklaşımını tercih ettim.
- Yeni Contrete dosyaları EfCoreEntityRepository<User, OETContext> ve IUserDal olarak şu şekilde tanımlanmalıdır:

   ```csharp
   public class EfUserDal : EfCoreEntityRepository<User, OETContext>, IUserDal
   ```

   Bu sayede basit CRUD işlemleri otomatik olarak doldurulacak ve IUserDal'daki yöntemler miras alınacaktır.

## BUSINESS

- İş kuralları bu katmanda yer almaktadır.
- Proje özgü DependencyResolvers ve Validation bu katmanda gerçekleştirilir.
- Proje özgü yanıtlar, bu katmanda magic stringler ile tutulmaktadır.
- Kullanılan arayüzler, WebAPI'nin dönüş türü ile uyumlu bir şekilde IResult ve IDataResult türlerini döndürecek şekilde tasarlanmıştır.

   ```csharp
   IDataResult<User> Login(UserForLoginDto userForLoginDto);
   IResult UserExist(string email);
   ```

## WEBAPI

- Tüm yapılandırmalar bu katmanda Program.cs ve appsettings.json dosyalarında gerçekleştirilir.

- Bu katmandaki denetleyiciler, veriyi döndürmek için çekirdek katmanda tanımladığımız IResult ve IDataResult yardımcıları kullanır. Bu, veriyi ve başarının durumunu, yanıtları döndürürken ve yanıtlarken mesajları işlemenizi daha kolay hale getirir.

- "Authorize" bölümünde, JWT (JSON Web Token) kimlik doğrulamasıyla birlikte Rol tabanlı erişim kontrolünü (RBAC) uyguladım. Bu, roller temelinde erişim izinlerini verimli bir şekilde yönetmenizi sağlar.

Gerekli kurulumlar ve proje yapısının net bir anlayışı ile kendi backend uygulamalarınızı geliştirmeye başlayabilirsiniz.

Eğer daha fazla sorunuz varsa veya Bu altyapı hakkında herhangi bir tavsiyeniz veya öneriniz varsa,  lütfen bana bildirin!  
İyi kodlamalar dilerim!

<p align="right"><-<a href="#readme-top">yukarıya çık</a>-></p>
