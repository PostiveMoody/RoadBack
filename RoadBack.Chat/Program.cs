using RoadBack.Chat.Hubs;
using RoadBack.Chat.SignalRHubs;

namespace RoadBack.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ChatManager>();

            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.SetIsOriginAllowed(url => true);
                    policy.AllowCredentials();
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseCors();

            app.MapHub<CommunicationHub>("/chat");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
