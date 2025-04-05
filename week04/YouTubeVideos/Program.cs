using System;

using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {Text}";
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Length { get; set; } 
    private List<Comment> comments;

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return comments.Count;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Number of comments: {GetCommentCount()}");
        Console.WriteLine("Comments:");
        foreach (var comment in comments)
        {
            Console.WriteLine($"  - {comment}");
        }
        Console.WriteLine(new string('-', 40));
    }
}

class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video("Basic C#", "John Smith", 600);
        Video video2 = new Video("Explaining Abstraction", "Thomas Rodrigues", 720);
        Video video3 = new Video("Introducing Visual Studio Code", "Maria Takeda", 480);

        // Add comments to videos
        video1.AddComment(new Comment("User1", "Great explanation!"));
        video1.AddComment(new Comment("User2", "Very helpful, thanks!"));
        video1.AddComment(new Comment("User3", "Thanks for the tips!"));

        video2.AddComment(new Comment("User4", "This really clarified a lot of stuff."));
        video2.AddComment(new Comment("User5", "Nice examples!"));
        video2.AddComment(new Comment("User6", "Looking foward for the next video!"));

        video3.AddComment(new Comment("User7", "Good overview!"));
        video3.AddComment(new Comment("User8", "I wish it had more examples."));
        video3.AddComment(new Comment("User9", "Very clear!"));

        List<Video> videos = new List<Video> { video1, video2, video3 };

        foreach (var video in videos)
        {
            video.DisplayInfo();
        }
    }
}
