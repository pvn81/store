﻿namespace Store;
public class Book
{
    public int Id { get; }
    public string Title { get; }
    public Book(int id, string title)
    {
        this.Id = id;
        this.Title = title;
    }

}
