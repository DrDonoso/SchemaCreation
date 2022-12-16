using System.Text.Json;
using Azure;
using Azure.Data.SchemaRegistry;
using Azure.Identity;
using CommandDotNet;
using Spectre.Console;

namespace SchemaCreation;

public class Program
{
    static int Main(string[] args)
    {
        AnsiConsole.Write(new FigletText("EventHubSchemaCreation")
            .LeftAligned()
            .Color(Color.Red));

        return new AppRunner<Program>().Run(args);
    }

    [Command("Apply",
        Usage = "Apply -t <AADTenantId> -c <ClientId> -s <ClientSecretKey> -e <EventHub> -g <SchemaGroup> -f <File>",
        Description = "Connects to an EventHub and apply an avro file to a schema registry")]
    public int Apply(ApplyArguments arguments)
    {
        var credential = new ClientSecretCredential(arguments.AADTenantId, arguments.ClientId, arguments.ClientSecretKey);
        var client = new SchemaRegistryClient($"{arguments.EventHub}.servicebus.windows.net", credential);
        
        var fileContent = GetFileContent(arguments.File);
        var schemaName = Path.GetFileNameWithoutExtension(arguments.File);
        try
        {
            Response<SchemaProperties> schemaProperties = client.RegisterSchema(arguments.SchemaGroup, schemaName, fileContent, SchemaFormat.Avro);
            var response = JsonSerializer.Serialize(schemaProperties);
            Console.WriteLine("\nResponse:");
            Console.WriteLine(response);

            AnsiConsole.MarkupLine($"{Emoji.Known.Rocket} [green] Success![/]");
            
            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"{Emoji.Known.CrossMark} [red] Exception: {ex}[/]");

            return -1;
        }
    }

    private static bool ParameterError(string eventhub, string groupName, string schemaName)
    {
        return string.IsNullOrEmpty(eventhub) || string.IsNullOrEmpty(groupName) || string.IsNullOrEmpty(schemaName);
    }

    private static string GetFileContent(string fullPath)
    {
        try
        {
            var text = File.ReadAllText(fullPath);
            Console.WriteLine("\nThe file content is: ");
            Console.WriteLine(text);

            return text;
        }
        catch (Exception)
        {
            Console.WriteLine($"No such file: {fullPath}");
            return string.Empty;
        }
    }
}