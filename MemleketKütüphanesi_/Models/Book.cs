using System;
using System.Collections.Generic;

namespace MemleketKütüphanesi_.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Name { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string PublishHouse { get; set; } = null!;

    public int? Page { get; set; }

    public DateTime DateOfIssued { get; set; }
}
