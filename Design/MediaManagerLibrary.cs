using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Design
{
/* Media Library Design Challenge
CRUD (Create, Read, Update, Delete) operations for different types of media, including videos, books, songs, and games. Each media type has specific attributes as described below.
The library should also support priority functionality and provide methods for querying the media collection by priority.

Media Types and Attributes:

Video
{
    id (unique identifier);
    title;
    director;
    release year;
}

Book
{
    id (unique identifier);
    title;
    author;
}

Functional Requirements:
    1. Implement CRUD operations for each media type:
        Create a new media item with the given attributes.
        Read an existing media item by its id.
        Update an existing media item's attributes
        Delete an existing media item by its id.
    2. Keep track of recently viewed items by weighting them higher in priority.
    3. Implement a method to retrieve the top-priority videos by year:
        - The method should accept a year as input and return a list of videos released in that year, sorted by their priority in descending order.
    4. Implement a method to search for media by title:
        - The method should accept a string as input and return a list of all media items that contain the given string in their title "Star" would return "Star Wars"
 */

    public class MediaManagerLibrary
    {
        public enum Type
        {
            Book, Video,
        }


    public class Temp
        {
            public int ID { get; set; }

            public int Year{ get; set; }
            public int Weight { get; internal set; }
        }

        public class Media
        {
            public int ID { get; set; }

            public string Title { get; set; }

            public Type Type { get; set; }

            public int Weight { get; set; } = 0;

            public Dictionary<string, object> customProps;
        }

        private List<Media> medias;

        public MediaManagerLibrary()
        {
            medias = new();
        }

        public void Test()
        {
            CreateMedia(1, "book1", "auth");
            CreateMedia(3, "book3", "auth");
            CreateMedia(4, "book4", "auth");
            CreateMedia(5, "book5", "auth");
            CreateMedia(6, "book6", "auth");

            CreateMedia(7, "video7", "director", 2024);
            CreateMedia(8, "video8", "director", 2024);
            CreateMedia(9, "video9", "director", 2022);
            CreateMedia(10, "video10", "director", 2025);
            CreateMedia(11, "video11", "director", 2024);
            CreateMedia(12, "video12", "director", 2024);


            ReadMedia(2);
            ReadMedia(6);
            ReadMedia(1);
            ReadMedia(7);
            ReadMedia(8);
            ReadMedia(9);
            ReadMedia(7);
            ReadMedia(7);
            ReadMedia(8);

            var rez = TopPriorityVideosByYear(2024);

            Console.WriteLine(string.Join(",", rez.Select(x => x.ID)));
        }

        public void CreateMedia(int id, string title, string director, int releaseYear)
        {
            medias.Add(new Media { ID = id, Title = title, Type = Type.Video, customProps = new() { { "director", director }, { "releaseYear", releaseYear } } });
        }

        public void CreateMedia(int id, string title, string author)
        {
            medias.Add(new Media { ID = id, Title = title, Type = Type.Book, customProps = new () { { "author", author }} });
        }

        public Media ReadMedia(int id)
        {

            var readMedia = medias.Where(x => x.ID == id).First();
            readMedia.Weight++;

            return readMedia;
        }

        public List<Temp> TopPriorityVideosByYear(int year)
        {
            var rez = medias
                .Where(x => x.Type == Type.Video)
                .Select(x =>
                {
                    var v = new Temp() { ID = x.ID, Year = (int)x.customProps["releaseYear"], Weight = x.Weight };
                    return v;
                })
                .Where(x => x.Year == year)
                .OrderByDescending(x => x.Weight).ToList();

            return rez;
        }

        // use Trie
        public List<Media> SearchMediaByTitle(string title)
        {
            return medias.Where(x => x.Title == title).ToList();
        }
    }
}
