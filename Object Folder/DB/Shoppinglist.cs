using System.ComponentModel.DataAnnotations;

namespace WG_Random_Task_assigner.Object_Folder.DB
{
public class Shoppinglist{

    [Key]
    public int id {get; set;}

    public string? name {get; set;}="";

    public int? quantity {get; set;} =1;
    
}
}