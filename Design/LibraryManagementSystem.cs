using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record Member(int Id, string Name);

    public class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int TotalCopies { get; }
        public int AvailableCopies { get; private set; }

        public Book(int id, string title, string author, int totalCopies)
        {
            if (totalCopies <= 0)
                throw new ArgumentException("Total copies must be greater than zero.");

            Id = id;
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public bool IsAvailable()
        {
            return AvailableCopies > 0;
        }

        public void BorrowCopy()
        {
            if (AvailableCopies <= 0)
                throw new InvalidOperationException("No copies available.");

            AvailableCopies--;
        }

        public void ReturnCopy()
        {
            if (AvailableCopies >= TotalCopies)
                throw new InvalidOperationException("All copies are already in the library.");

            AvailableCopies++;
        }
    }

    public class BorrowRecord
    {
        public int BookId { get; }
        public int MemberId { get; }
        public DateTime BorrowedAt { get; }
        public bool IsReturned { get; private set; }

        public BorrowRecord(int bookId, int memberId)
        {
            BookId = bookId;
            MemberId = memberId;
            BorrowedAt = DateTime.Now;
        }

        public void MarkReturned()
        {
            IsReturned = true;
        }
    }

    public class Library
    {
        private readonly List<Book> _books = new();
        private readonly List<Member> _members = new();
        private readonly List<BorrowRecord> _borrowRecords = new();

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (_books.Any(b => b.Id == book.Id))
                throw new InvalidOperationException("Book with same Id already exists.");

            _books.Add(book);
        }

        public void RegisterMember(Member member)
        {
            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException("Member with same Id already exists.");

            _members.Add(member);
        }

        public void BorrowBook(int memberId, int bookId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (member == null)
                throw new InvalidOperationException("Member not found.");

            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            if (!book.IsAvailable())
                throw new InvalidOperationException("Book is not available.");

            book.BorrowCopy();
            _borrowRecords.Add(new BorrowRecord(bookId, memberId));
        }

        public void ReturnBook(int memberId, int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            var record = _borrowRecords.LastOrDefault(r =>
                r.BookId == bookId &&
                r.MemberId == memberId &&
                !r.IsReturned);

            if (record == null)
                throw new InvalidOperationException("No active borrow record found.");

            record.MarkReturned();
            book.ReturnCopy();
        }

        public bool IsBookAvailable(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            return book.IsAvailable();
        }

        public int GetAvailableCopies(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            return book.AvailableCopies;
        }

        public class Program
        {
            public static void Mai5n()
            {
                var library = new Library();

                library.AddBook(new Book(1, "Clean Code", "Robert Martin", 3));
                library.AddBook(new Book(2, "The Pragmatic Programmer", "Andrew Hunt", 2));

                library.RegisterMember(new Member(1, "Alice"));
                library.RegisterMember(new Member(2, "Bob"));

                library.BorrowBook(1, 1);
                library.BorrowBook(2, 1);

                Console.WriteLine($"Available copies of Book 1: {library.GetAvailableCopies(1)}");

                library.ReturnBook(1, 1);

                Console.WriteLine($"Available copies of Book 1 after return: {library.GetAvailableCopies(1)}");
            }
        }
    }
}

/*
 *  The key design difference is in BorrowBook and ReturnBook. These methods now return
BorrowBookResult and ReturnBookResult. That lets the caller handle outcomes cleanly
without needing try/catch for normal cases. For example, the UI can easily map
NoCopiesAvailable to a friendly message. This makes the service easier to use and keeps
business control flow separate from exception flow.

Exceptions are still reasonable for true programming problems such as null inputs or
invalid object construction. For example, creating a book with zero total copies is still
an invalid argument and should throw. But for normal domain outcomes, result enums are
cleaner and more expressive.

The key interview message is this: use exceptions for invalid inputs or broken
invariants, and use result values for expected business-rule outcomes. In this version,
Book still owns inventory behavior, BorrowRecord still tracks who borrowed what, and
Library still coordinates the workflow, but borrow and return operations now communicate
their outcomes more explicitly. 
 * 
 
using System;
using System.Collections.Generic;
using System.Linq;

namespace Design
{
    public record Member(int Id, string Name);

    public enum BorrowBookResult
    {
        Success,
        MemberNotFound,
        BookNotFound,
        NoCopiesAvailable
    }

    public enum ReturnBookResult
    {
        Success,
        BookNotFound,
        NoActiveBorrowRecord
    }

    public class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int TotalCopies { get; }
        public int AvailableCopies { get; private set; }

        public Book(int id, string title, string author, int totalCopies)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.", nameof(title));

            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.", nameof(author));

            if (totalCopies <= 0)
                throw new ArgumentException("Total copies must be greater than zero.", nameof(totalCopies));

            Id = id;
            Title = title;
            Author = author;
            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;
        }

        public bool IsAvailable()
        {
            return AvailableCopies > 0;
        }

        public bool TryBorrowCopy()
        {
            if (AvailableCopies <= 0)
                return false;

            AvailableCopies--;
            return true;
        }

        public bool TryReturnCopy()
        {
            if (AvailableCopies >= TotalCopies)
                return false;

            AvailableCopies++;
            return true;
        }
    }

    public class BorrowRecord
    {
        public int BookId { get; }
        public int MemberId { get; }
        public DateTime BorrowedAt { get; }
        public bool IsReturned { get; private set; }

        public BorrowRecord(int bookId, int memberId)
        {
            BookId = bookId;
            MemberId = memberId;
            BorrowedAt = DateTime.Now;
        }

        public void MarkReturned()
        {
            IsReturned = true;
        }
    }

    public class Library
    {
        private readonly List<Book> _books = new();
        private readonly List<Member> _members = new();
        private readonly List<BorrowRecord> _borrowRecords = new();

        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (_books.Any(b => b.Id == book.Id))
                throw new InvalidOperationException("Book with same Id already exists.");

            _books.Add(book);
        }

        public void RegisterMember(Member member)
        {
            if (_members.Any(m => m.Id == member.Id))
                throw new InvalidOperationException("Member with same Id already exists.");

            _members.Add(member);
        }

        public BorrowBookResult BorrowBook(int memberId, int bookId)
        {
            var member = _members.FirstOrDefault(m => m.Id == memberId);
            if (member == null)
                return BorrowBookResult.MemberNotFound;

            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                return BorrowBookResult.BookNotFound;

            if (!book.TryBorrowCopy())
                return BorrowBookResult.NoCopiesAvailable;

            _borrowRecords.Add(new BorrowRecord(bookId, memberId));
            return BorrowBookResult.Success;
        }

        public ReturnBookResult ReturnBook(int memberId, int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                return ReturnBookResult.BookNotFound;

            var record = _borrowRecords.LastOrDefault(r =>
                r.BookId == bookId &&
                r.MemberId == memberId &&
                !r.IsReturned);

            if (record == null)
                return ReturnBookResult.NoActiveBorrowRecord;

            record.MarkReturned();
            book.TryReturnCopy();

            return ReturnBookResult.Success;
        }

        public bool IsBookAvailable(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            return book.IsAvailable();
        }

        public int GetAvailableCopies(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book == null)
                throw new InvalidOperationException("Book not found.");

            return book.AvailableCopies;
        }
    }

    public class Program
    {
        public static void Test()
        {
            var library = new Library();

            library.AddBook(new Book(1, "Clean Code", "Robert Martin", 2));
            library.AddBook(new Book(2, "The Pragmatic Programmer", "Andrew Hunt", 1));

            library.RegisterMember(new Member(1, "Alice"));
            library.RegisterMember(new Member(2, "Bob"));

            var borrow1 = library.BorrowBook(1, 1);
            var borrow2 = library.BorrowBook(2, 1);
            var borrow3 = library.BorrowBook(2, 1); // no more copies

            Console.WriteLine($"Borrow result 1: {borrow1}");
            Console.WriteLine($"Borrow result 2: {borrow2}");
            Console.WriteLine($"Borrow result 3: {borrow3}");

            Console.WriteLine($"Available copies of Book 1: {library.GetAvailableCopies(1)}");

            var returnResult = library.ReturnBook(1, 1);
            Console.WriteLine($"Return result: {returnResult}");

            Console.WriteLine($"Available copies of Book 1 after return: {library.GetAvailableCopies(1)}");
        }
    }
}
*/

