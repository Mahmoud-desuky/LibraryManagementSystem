using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int id {  get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public int Useerid { get; set; }
        public User UserBorrow { get; set; }
        
        public int libraryid {  get; set; }
        public Library Library { get; set; }
    }
}
