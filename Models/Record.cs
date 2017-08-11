using System;
using System.ComponentModel.DataAnnotations;
 
namespace bank.Models
{
    public class Record : BaseEntity
    {
        public int RecordId {get; set;}
        public double Amount {get;set;}
        public DateTime Date {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get; set;}

    }
}