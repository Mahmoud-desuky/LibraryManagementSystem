using System.Net.Mail;
using System.Linq;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using LibraryManagementSystem.DTO;
using System.Threading.Channels;
namespace LibraryManagementSystem
{
    internal class Program
    {
        public static bool ValidEmail(string Email)
        {
            try
            {
                var email = new MailAddress(Email);
                return email.Address == Email.Trim();
            }
            catch
            {
                return false;
            }
        }
        public static void ShowAllBooks()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var allbooks = context.Books.ToList();
            foreach (var book in allbooks)
                Console.WriteLine(book.Title + " " + book.Author + " ", book.Year);
        }
        static void Main(string[] args)
        {
            UserLogin:
            Console.WriteLine("Welcome To The Library System");
            Console.WriteLine("Are you Reguler User or Librarian ");
            Console.WriteLine("1- Reguler User");
            Console.WriteLine("2- Librarian");
            int UserStatu = int.Parse(Console.ReadLine());
           Library library = new Library();
            ApplicationDbContext context = new ApplicationDbContext();

            if (UserStatu == 1)
            {
                Console.WriteLine("1- Login");
                Console.WriteLine("2- Sign up");
                int FoundStatu = int.Parse(Console.ReadLine());
                if (FoundStatu == 1)
                {
                    Console.WriteLine("Login Page ");
                    Console.WriteLine("Email");
                    string Email = Console.ReadLine();
                    Console.WriteLine("Passwor");
                    string Password = Console.ReadLine();
                    if (ValidEmail(Email) == false)
                    {
                        Console.WriteLine("Invalid Email Address");
                    }
                    else
                    {
                        var FoundUser = context.Users.FirstOrDefault(x => x.Email == Email);
                        if (FoundUser == null)
                        {
                            Console.WriteLine("Email Not Found");
                        }
                        else
                        {
                            if (context.Users.FirstOrDefault(x => x.ID == FoundUser.ID).Password == Password)
                            {
                                Console.WriteLine("Success Login");
                                Console.WriteLine("1-Show All Books");
                                Console.WriteLine("2-My Books");
                                int choise = int.Parse(Console.ReadLine());
                                if (choise == 1)
                                {
                                    ShowAllBooks();
                                    var allbooks = context.Books.ToList();
                                    Console.WriteLine("If you would like to Borrow Book Please Enter the Name otherwise NA");
                                    string BorrowB = Console.ReadLine();
                                    while (BorrowB != "NA")
                                    {
                                        var CorreectBook = allbooks.Find(x => x.Title == BorrowB);
                                        if (CorreectBook != null)
                                        {
                                            var CorreectBookId = CorreectBook.id;
                                        }
                                    }
                                }
                                else if (choise == 2)
                                {

                                    var MyBook = context.Books.Where(x => x.Useerid == FoundUser.ID).ToList();
                                    foreach (var book in MyBook)
                                        Console.WriteLine(book.Title + ' ' + book.Author + ' ' + book.Year);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choise");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Password");
                            }
                        }
                    }
                }
                else if (FoundStatu == 2)
                {
                    var newaccount = new User();
                    Console.WriteLine("Wellcom Let's Create New Account Please Enter you User Name");
                    string Name = Console.ReadLine();
                    newaccount.Name = Name;
                    Console.WriteLine("You Email");
                    string mail = Console.ReadLine();
                    while(ValidEmail(mail)==false)
                    {
                        Console.WriteLine("please Write Valid Email");
                        mail= Console.ReadLine();
                    }
                    var validmail = context.Users.FirstOrDefault(x => x.Email == mail);
                    bool stillRegiser = true;
                    while (validmail != null && stillRegiser)
                    {
                        Console.WriteLine("this Email has an account\n Enter L to Login");
                        mail = Console.ReadLine();
                        if (mail == "L")
                        {
                            stillRegiser = false;
                        }
                        else
                        {
                            validmail = context.Users.FirstOrDefault(x => x.Email == mail);
                        }
                    }
                    if (stillRegiser == false)
                        goto UserLogin;
                    newaccount.Email = mail;
                    Console.WriteLine("Enter Password");
                    string Passw = Console.ReadLine();
                    while (Passw.Length < 8)
                    {
                        Console.WriteLine("Password must contan 8 char or greater");
                        Passw = Console.ReadLine();
                    }
                    newaccount.Password = Passw;
                    context.Users.Add(newaccount);
                    context.SaveChanges();
                    Console.WriteLine("Account has been Created Please Login");
                    goto UserLogin;
                }
                else
                {
                    Console.WriteLine("Incorrect Choise Please Tray Again");
                }
            }
            else if (UserStatu == 2)
            {
                Admin:
                Console.WriteLine("Admain Page");
                Console.WriteLine("1-Add New Book");
                Console.WriteLine("2-Delete Book");
                Console.WriteLine("3-show All Book");
                Console.WriteLine("4-show Borrow Book");
                Console.WriteLine("5-show Data of User");
                int option = int.Parse(Console.ReadLine());
                if (option < 1 || option > 5)
                {
                    Console.WriteLine("Invalid Choise");
                    goto Admin;
                }
                if (option == 1)
                {
                    var addnewBook = new Book();
                    Console.WriteLine("the Name of Book");
                    addnewBook.Title = Console.ReadLine();
                    Console.WriteLine("the Author of Book");
                    addnewBook.Author = Console.ReadLine();
                    Console.WriteLine("the Book Year ");
                    addnewBook.Year = int.Parse(Console.ReadLine());
                    context.Add(addnewBook);
                    context.SaveChanges();
                }
                else if (option == 2)
                {
                    Console.WriteLine("id of Book you Would to Delete");
                    int id = int.Parse(Console.ReadLine());
                    var foundBook = context.Books.FirstOrDefault(x => x.id == id);
                    if (foundBook != null)
                    {
                        context.Books.Remove(foundBook);
                    } else
                        Console.WriteLine("id Not Found");
                }
                else if (option == 3)
                    ShowAllBooks();
                else if (option == 4)
                {
                    var BorrowBook = context.Books.Where(x => x.Useerid != null).ToList();
                    foreach (var book in BorrowBook)
                    {
                        var UserName = context.Users.FirstOrDefault(x => x.ID == book.Useerid);
                        Console.WriteLine(book.Title + "Has Borrowed to " + UserName.Name);
                    }
                }
                else
                {
                    var AllUsers = context.Users.ToList();
                    foreach(var user in AllUsers)
                    {
                        Console.WriteLine(user.Name+" "+ user.Email);
                    }
                }
            }
            else {
                Console.WriteLine("Incorrect Choise Please Tray Again");
                goto UserLogin;
            }
        }
    }
}
