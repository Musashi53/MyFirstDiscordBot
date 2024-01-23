// https://discord.com/api/oauth2/authorize?client_id=1199431733540552845&permissions=8&scope=bot
// https://discord.com/channels/1199440438747205652/1199440438747205654
using Discord.WebSocket;

DotNetEnv.Env.Load();
var discordToken = Environment.GetEnvironmentVariable("DiscordToken");

async Task RunBotAsync() 
{
  // ...
  var client = new DiscordSocketClient(new DiscordSocketConfig{
    LogLevel = Discord.LogSeverity.Debug
  });

  await client.LoginAsync(Discord.TokenType.Bot, discordToken);

  Console.WriteLine(client.LoginState);

  await client.StartAsync();

  client.Ready += async () =>
  {
    var guild = client.GetGuild(1199440438747205652);
    var channel = guild.GetTextChannel(1199440438747205654);

    await channel.SendMessageAsync("Hello World!");

    await client.DisposeAsync();
  };

  client.Log += (log) =>
  {
    Console.WriteLine($"{DateTime.Now} => {log.Message}");
    return Task.CompletedTask;
  };
}

await RunBotAsync();
Console.ReadKey();