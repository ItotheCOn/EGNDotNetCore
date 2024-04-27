using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetCore.ConsoleApp.Dtos;

[Table("Tbl_blog")]   // by doing this , can ensure c# BlogDto is equal with that table
public class BlogDto
{
    [Key]   // this is telling which one is primary key, we have to declare before primary key
    public int BlogId { get; set; }
    public string BlogTitle { get; set; }
    public string BlogAuthor { get; set; }
    public string BlogContent { get; set; }

}
