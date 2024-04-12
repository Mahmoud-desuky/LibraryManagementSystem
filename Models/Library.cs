using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    public class Library
    {
        [Key]
        public int Library_pin { get; set; }
       public List<Book> Books { get; set; }
      // public List<BorrowBook> BorrowBooks {  get; set; }


    }
}
