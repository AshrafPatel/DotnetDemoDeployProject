using Contacts.UI.Components;
using Contacts.UI.Services;

var builder = WebApplication.CreateBuilder(args);

var apiBase = builder.Configuration["Api:BaseUrl"];
if (string.IsNullOrWhiteSpace(apiBase) || !Uri.TryCreate(apiBase, UriKind.Absolute, out var apiUri))
{
    throw new InvalidOperationException("API_BASE_URL is missing or invalid. Set it in appsettings.json or environment variables.");
}

// Register typed HttpClient for ContactsApiClient
builder.Services.AddHttpClient<ContactsApiClient>(client =>
{
    client.BaseAddress = apiUri;
});

builder.Services.AddScoped<ContactsApiClient>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddScoped<ContactsApiClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAntiforgery();

app.UseStaticFiles();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
