using Grpc.Net.Client;
using Microsoft.EntityFrameworkCore;
using RecycleCoin.Grpc;
using static RecycleCoin.Grpc.BlokServis;
using static RecycleCoin.Grpc.GenelBilgi;
using static RecycleCoin.Grpc.HesapServis;
using static RecycleCoin.Grpc.IslemServis;

internal class Program
{
    public static GrpcChannel channel;
    public static HesapServisClient hesapServis;
    public static BlokServisClient blokServis;
    public static IslemServisClient islemServis;
    public static GenelBilgiClient genelBilgiServisi;
    public static PeriodicTimer sayac = new PeriodicTimer(TimeSpan.FromMinutes(2));
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        channel = GrpcChannel.ForAddress("http://localhost:5002");
        hesapServis = new HesapServisClient(channel);
        blokServis = new BlokServisClient(channel);
        islemServis= new IslemServisClient(channel);
        genelBilgiServisi = new GenelBilgiClient(channel);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
    }

    public static bool SunucuDurum()
    {
        if (channel.State != Grpc.Core.ConnectivityState.Ready)
        {
            channel.ConnectAsync();
        }
        if(channel.State == Grpc.Core.ConnectivityState.Ready) return true;
        else return false;
    }
}