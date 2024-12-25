using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình DbContext cho SQL Server
builder.Services.AddDbContext<Thietbi.Models.ThietBiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HOSTVN"))
);

// 2. Cấu hình Localization để hỗ trợ tiếng Việt
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("vi-VN") };
    options.DefaultRequestCulture = new RequestCulture("vi-VN");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// 3. Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20); // Thời gian tồn tại của session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 4. Thêm dịch vụ Xác thực Cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Đường dẫn tới trang đăng nhập
        options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn tới trang khi truy cập bị từ chối
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Thời gian sống của cookie
        options.SlidingExpiration = true; // Cập nhật thời gian sống của cookie khi có hoạt động
    });

// 5. Thêm dịch vụ Phân quyền
builder.Services.AddAuthorization();

// 6. Thêm các dịch vụ vào container với chính sách phân quyền mặc định
builder.Services.AddControllersWithViews(options =>
{
    // Tạo chính sách yêu cầu người dùng đã xác thực
    var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
    // Áp dụng chính sách này cho tất cả các Controller và Action
    options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

// 7. Cấu hình Pipeline HTTP

// a. Xử lý lỗi cho môi trường Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Bạn có thể thêm các middleware khác như HSTS ở đây
}

// b. Sử dụng các tệp tĩnh (Static Files)
app.UseStaticFiles();

// c. Kích hoạt Localization
app.UseRequestLocalization();

// d. Sử dụng Session trước khi Routing
app.UseSession();

// e. Routing
app.UseRouting();

// f. Thêm Middleware Xác thực và Phân quyền vào Pipeline
app.UseAuthentication();
app.UseAuthorization();

// g. Thiết lập Route Mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}"
);

app.Run();