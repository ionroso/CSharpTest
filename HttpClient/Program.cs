using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

class Program
{
    /*
        {
          "userId": 1,
          "id": 1,
          "title": "delectus aut autem",
          "completed": false
        }
     */


    public record class Todo(
        int? UserId = null,
        int? Id = null,
        string? Title = null,
        bool? Completed = null);

    static async Task Main(string[] args)
    {
        using HttpClient client = new HttpClient();
        await GetFromJsonAsync(client);
        try
        {
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/todos/1");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    static async Task GetFromJsonAsync(HttpClient httpClient)
    {
        List<Todo>? todos = await httpClient.GetFromJsonAsync<List<Todo>>(
            "https://jsonplaceholder.typicode.com/todos?userId=1&completed=false");

        Console.WriteLine("GET https://jsonplaceholder.typicode.com/todos?userId=1&completed=false HTTP/1.1");
        todos?.ForEach(Console.WriteLine);
        Console.WriteLine();

        // Expected output:
        //   GET https://jsonplaceholder.typicode.com/todos?userId=1&completed=false HTTP/1.1
        //   Todo { UserId = 1, Id = 1, Title = delectus aut autem, Completed = False }
        //   Todo { UserId = 1, Id = 2, Title = quis ut nam facilis et officia qui, Completed = False }
        //   Todo { UserId = 1, Id = 3, Title = fugiat veniam minus, Completed = False }
        //   Todo { UserId = 1, Id = 5, Title = laboriosam mollitia et enim quasi adipisci quia provident illum, Completed = False }
        //   Todo { UserId = 1, Id = 6, Title = qui ullam ratione quibusdam voluptatem quia omnis, Completed = False }
        //   Todo { UserId = 1, Id = 7, Title = illo expedita consequatur quia in, Completed = False }
        //   Todo { UserId = 1, Id = 9, Title = molestiae perspiciatis ipsa, Completed = False }
        //   Todo { UserId = 1, Id = 13, Title = et doloremque nulla, Completed = False }
        //   Todo { UserId = 1, Id = 18, Title = dolorum est consequatur ea mollitia in culpa, Completed = False }
    }
}
