using System.Diagnostics;

public static class EmailNotificationExtensions
{
    public static void MapEmailNotificationEndpoints(this WebApplication app)
    {
        app.MapPost("/api/notify", async (string to, string body) =>
        {
            if (string.IsNullOrWhiteSpace(to) || string.IsNullOrWhiteSpace(body))
            {
                return Results.Text("0, message failed");
            }

            try
            {
                // Construct the shell command using sendmail
                string command = $"echo \"{EscapeShellArg(body)}\" | sendmail -s \"Notification from 547 Bikes\" -r reservations@547bikes.info {EscapeShellArg(to)}";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/bin/bash",
                        Arguments = $"-c \"{command}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                process.Start();
                await process.StandardOutput.ReadToEndAsync();
                await process.StandardError.ReadToEndAsync();
                process.WaitForExit();

                return process.ExitCode == 0
                    ? Results.Text($"1, {to}")
                    : Results.Text("0, message failed");
            }
            catch
            {
                return Results.Text("0, message failed");
            }
        });
    }

    private static string EscapeShellArg(string arg)
    {
        return arg.Replace("\"", "\\\"").Replace("$", "\\$").Replace("`", "\\`");
    }
}
