using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGNDotNetTrainingBatch4.RestApi.Models;

[Table("Tbl_blog")]   // by doing this , can ensure c# BlogDto is equal with that table
public class BlogModel
{
    [Key]   // this is telling which one is primary key, we have to declare before primary key
    //putting Question mark(?) mean we are allowed null, but do not have to do for id as integer value is 0.
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }

}
